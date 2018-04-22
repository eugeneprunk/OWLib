﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TankLib;
using TankView.ViewModel;
using DirectXTexNet;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace TankView.Helper
{
    public static class ImageHelper
    {
        [Flags]
        public enum CoInit : uint
        {
            MultiThreaded = 0x00,
            ApartmentThreaded = 0x02,
            DisableOLE1DDE = 0x04,
            SpeedOverMemory = 0x08
        }

        [DllImport("Ole32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int CoInitializeEx([In, Optional] IntPtr pvReserved, [In] CoInit dwCoInit);

        public enum ImageFormat
        {
            TGA,
            PNG,
            TIF,
            GIF,
            JPEG,
            BMP
        }

        public static byte[] ConvertDDS(GUIDEntry value, DXGI_FORMAT targetFormat, ImageFormat imageFormat, int frame)
        {
            try
            {
                if(value == null || value.GUID == 0)
                {
                    return null;
                }
                ushort type = teResourceGUID.Type(value.GUID);
                if (type != 0x004 && type != 0x0F1)
                {
                    return null;
                }
                teTexture texture = new teTexture(IOHelper.OpenFile(value));
                if (texture.PayloadRequired)
                {
                    ulong payload = texture.GetPayloadGUID(value.GUID);
                    if (value.APM.FirstOccurence.ContainsKey(payload))
                    {
                        texture.LoadPayload(IOHelper.OpenFile(value.APM.FirstOccurence[payload]));
                    }
                    else
                    {
                        return null;
                    }
                }

                Stream ms = texture.SaveToDDS();

                CoInitializeEx(IntPtr.Zero, CoInit.MultiThreaded | CoInit.SpeedOverMemory);

                byte[] data = new byte[ms.Length];
                ms.Read(data, 0, data.Length);
                ScratchImage scratch = null;
                try
                {
                    unsafe
                    {
                        fixed (byte* dataPin = data)
                        {
                            scratch = TexHelper.Instance.LoadFromDDSMemory((IntPtr)dataPin, data.Length, DDS_FLAGS.NONE);
                            TexMetadata info = scratch.GetMetadata();
                            if (TexHelper.Instance.IsCompressed(info.Format))
                            {
                                ScratchImage temp = scratch.Decompress(frame, DXGI_FORMAT.UNKNOWN);
                                scratch.Dispose();
                                scratch = temp;
                            }
                            info = scratch.GetMetadata();

                            if (info.Format != targetFormat)
                            {
                                ScratchImage temp = scratch.Convert(targetFormat, TEX_FILTER_FLAGS.DEFAULT, 0.5f);
                                scratch.Dispose();
                                scratch = temp;
                            }

                            UnmanagedMemoryStream stream = null;
                            if (imageFormat == ImageFormat.TGA)
                            {
                                stream = scratch.SaveToTGAMemory(frame < 0 ? 0 : frame);
                            }
                            else
                            {
                                WICCodecs codec = WICCodecs.PNG;
                                bool isMultiframe = false;
                                switch (imageFormat)
                                {
                                    case ImageFormat.BMP:
                                        codec = WICCodecs.BMP;
                                        break;
                                    case ImageFormat.GIF:
                                        codec = WICCodecs.GIF;
                                        isMultiframe = true;
                                        break;
                                    case ImageFormat.JPEG:
                                        codec = WICCodecs.JPEG;
                                        break;
                                    case ImageFormat.PNG:
                                        codec = WICCodecs.PNG;
                                        break;
                                    case ImageFormat.TIF:
                                        codec = WICCodecs.TIFF;
                                        isMultiframe = true;
                                        break;
                                }

                                if (frame < 0)
                                {
                                    if (!isMultiframe)
                                    {
                                        frame = 0;
                                    }
                                    else
                                    {
                                        stream = scratch.SaveToWICMemory(0, info.ArraySize, WIC_FLAGS.ALL_FRAMES, TexHelper.Instance.GetWICCodec(codec));
                                    }
                                }
                                if (frame >= 0)
                                {
                                    stream = scratch.SaveToWICMemory(frame, WIC_FLAGS.NONE, TexHelper.Instance.GetWICCodec(codec));
                                }
                                
                                byte[] tex = new byte[stream.Length];
                                stream.Read(tex, 0, tex.Length);
                                scratch.Dispose();
                                return tex;
                            }
                        }
                    }
                }
                catch
                {
                    if (scratch != null && scratch.IsDisposed == false)
                    {
                        scratch?.Dispose();
                    }
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
