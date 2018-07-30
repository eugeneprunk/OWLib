﻿using static CMFLib.Helpers;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_47946 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.BuildVersion - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            uint increment = kidx % 29;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
                buffer[i] ^= digest[SignedMod(kidx + header.EntryCount, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x95, 0x14, 0xA3, 0x79, 0xDC, 0x50, 0x66, 0x5D, 0x3B, 0xAC, 0x5A, 0xE0, 0x35, 0xD8, 0xF1, 0x0D, 
            0x91, 0x49, 0x84, 0x39, 0xAF, 0x64, 0xAC, 0x8F, 0xE6, 0xAC, 0x74, 0x43, 0xD5, 0xFF, 0x3D, 0xD6, 
            0x97, 0x56, 0x13, 0x41, 0xE8, 0x81, 0x54, 0x44, 0xB0, 0xAA, 0xE0, 0x39, 0x61, 0x9E, 0xD3, 0x00, 
            0xDC, 0x33, 0x7D, 0x8B, 0x7F, 0x50, 0xC5, 0xBA, 0x98, 0xFD, 0xF9, 0x89, 0xDF, 0x10, 0x76, 0x84, 
            0x8A, 0x04, 0x35, 0xE9, 0x3A, 0x91, 0x09, 0x38, 0x56, 0x2B, 0xB1, 0xBA, 0x07, 0xE2, 0x26, 0x19, 
            0xD8, 0x1A, 0xB7, 0x32, 0x61, 0xFC, 0x4D, 0x6E, 0xD4, 0x25, 0x00, 0x73, 0xE6, 0x71, 0x0D, 0x49, 
            0xBC, 0xF0, 0x6B, 0x35, 0x06, 0xBC, 0x23, 0xF4, 0x00, 0x28, 0x0D, 0x67, 0xF0, 0xD7, 0x7D, 0x70, 
            0x18, 0x4D, 0xCE, 0x54, 0x7A, 0x0F, 0x9C, 0x70, 0xB1, 0x4C, 0x36, 0xB3, 0x35, 0x76, 0x06, 0x79, 
            0xF5, 0x57, 0x78, 0x3B, 0x57, 0x41, 0x83, 0xFD, 0x38, 0x6B, 0xE4, 0x17, 0x92, 0x19, 0x07, 0x01, 
            0x6C, 0xE0, 0xD7, 0xCA, 0x51, 0xE7, 0x2C, 0x0E, 0x53, 0x00, 0x86, 0x6C, 0xED, 0x8A, 0x88, 0xBE, 
            0xE7, 0x21, 0xAC, 0x6C, 0x03, 0xD9, 0x45, 0xD1, 0x17, 0x40, 0xCD, 0xC0, 0x4B, 0xF0, 0xBD, 0x0F, 
            0x55, 0x8B, 0xDF, 0x3E, 0x4D, 0x4E, 0x11, 0x1D, 0xC2, 0xBF, 0xF9, 0x20, 0xF9, 0xF7, 0x2D, 0x3B, 
            0xB8, 0x8B, 0xB3, 0x86, 0x94, 0xAF, 0x03, 0x80, 0xFC, 0xDF, 0x6C, 0xAD, 0x80, 0x54, 0xBB, 0x28, 
            0xA4, 0x6C, 0x67, 0xDC, 0x21, 0xD7, 0x92, 0x37, 0xD2, 0xB9, 0xE7, 0x74, 0xEE, 0xEA, 0x72, 0x7E, 
            0x37, 0x1F, 0x87, 0x9F, 0x68, 0x88, 0xFB, 0x79, 0x84, 0xC1, 0xA1, 0x23, 0x9B, 0x2F, 0xFB, 0xE2, 
            0x86, 0xD4, 0xD3, 0x2E, 0x48, 0x5A, 0x49, 0x31, 0x0E, 0x73, 0x9A, 0x4E, 0xCC, 0xF4, 0x59, 0x1C, 
            0x82, 0x39, 0x8F, 0x18, 0x9C, 0x71, 0xB4, 0xC0, 0x6D, 0x4E, 0xB3, 0xF6, 0x21, 0xED, 0xB3, 0x82, 
            0x23, 0xFF, 0x4C, 0x32, 0x81, 0x6A, 0x96, 0xA9, 0xA7, 0x92, 0x83, 0x5F, 0xCD, 0x63, 0x4A, 0xC4, 
            0xA0, 0x3D, 0x1B, 0xAD, 0x38, 0xAB, 0xC0, 0x09, 0x93, 0x25, 0x96, 0x92, 0x67, 0x03, 0x83, 0x78, 
            0xFC, 0xA6, 0xFE, 0x04, 0xF7, 0xC8, 0xC6, 0x05, 0x6B, 0x30, 0x6A, 0x5F, 0xBE, 0x64, 0x9F, 0x7F, 
            0x6C, 0x9F, 0x80, 0xA2, 0x87, 0xE8, 0x1E, 0x05, 0x00, 0x16, 0x97, 0xF3, 0x23, 0x88, 0x45, 0xCE, 
            0x2C, 0x0F, 0xF8, 0xBC, 0x54, 0xD4, 0x1D, 0xCA, 0x23, 0x7D, 0x43, 0xDF, 0x05, 0xB9, 0xFE, 0xA9, 
            0x77, 0x35, 0xC3, 0x6C, 0x11, 0x76, 0xCE, 0x9C, 0xAD, 0x4F, 0x00, 0xCB, 0x5B, 0x39, 0xCE, 0xF2, 
            0x05, 0x03, 0xC5, 0xAA, 0xB6, 0x40, 0x4E, 0x1C, 0x3E, 0x79, 0xEE, 0x12, 0xBB, 0x9E, 0xE7, 0xE0, 
            0xF0, 0xD5, 0x3E, 0xCD, 0xC6, 0xEA, 0xDB, 0x3C, 0x6B, 0x49, 0x4C, 0x51, 0x59, 0x87, 0xEA, 0xEF, 
            0xA7, 0x0F, 0x26, 0xD6, 0x09, 0x6A, 0x69, 0x90, 0xD4, 0xC6, 0x44, 0xD6, 0x29, 0x53, 0x2D, 0xE1, 
            0xDF, 0xCC, 0x2D, 0xE5, 0xB8, 0x77, 0xE6, 0x8E, 0x61, 0x91, 0xF5, 0x87, 0x61, 0x26, 0x59, 0xFB, 
            0x4C, 0xA3, 0x23, 0x15, 0xD4, 0x72, 0x05, 0xE1, 0x13, 0x35, 0x12, 0x39, 0xE0, 0x71, 0x4D, 0x82, 
            0x16, 0xBF, 0xDE, 0xF2, 0x56, 0x10, 0x43, 0x98, 0xBC, 0x16, 0x13, 0x6F, 0x79, 0xE7, 0xF2, 0x21, 
            0x32, 0x0A, 0x49, 0x70, 0x9B, 0x0B, 0x42, 0x5B, 0xA8, 0xFD, 0xF5, 0x3A, 0xD2, 0xF9, 0x45, 0x37, 
            0x2F, 0x4E, 0x62, 0x79, 0x21, 0xA4, 0xD4, 0xFB, 0x72, 0x13, 0x4C, 0xF7, 0x2C, 0x25, 0xD0, 0x68, 
            0x93, 0x72, 0x33, 0x6F, 0x5F, 0xFE, 0xF2, 0x6A, 0x84, 0x92, 0x2E, 0xB5, 0x01, 0x6B, 0xA3, 0xDC
        };
    }
}