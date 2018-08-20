﻿using static CMFLib.Helpers;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_37415 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            uint increment = 3;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(header.BuildVersion * length);
            uint increment = header.BuildVersion * header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - 73) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x23, 0x6E, 0x8F, 0x25, 0x0E, 0xE0, 0x5A, 0xC5, 0x0A, 0xEF, 0xAB, 0xCE, 0x4B, 0x56, 0x9F, 0x7A,
            0xA1, 0x5A, 0x96, 0xF5, 0x76, 0x4D, 0x15, 0x71, 0x59, 0x51, 0x15, 0x6D, 0x28, 0x22, 0x66, 0xC6,
            0x9A, 0xDA, 0xC1, 0xEA, 0x09, 0xFF, 0x81, 0x9F, 0xFB, 0x55, 0xC5, 0x5C, 0x7B, 0xB6, 0xBF, 0x72,
            0xA4, 0x31, 0x5F, 0x1D, 0x5A, 0x33, 0xD6, 0xE7, 0x62, 0x4B, 0xC1, 0xF0, 0xD7, 0x25, 0x73, 0x60,
            0xED, 0xFE, 0x4A, 0xDE, 0x82, 0xD7, 0xF1, 0xB5, 0xED, 0x9E, 0x21, 0x7A, 0xBA, 0x48, 0x7B, 0x62,
            0x4C, 0x0E, 0x85, 0x7B, 0x34, 0x6E, 0x45, 0x6A, 0x80, 0xE0, 0xB3, 0xD4, 0x16, 0x8D, 0x00, 0x01,
            0xEA, 0xED, 0xD1, 0x5F, 0x42, 0xDF, 0x37, 0xA9, 0x7B, 0xDC, 0xC8, 0x10, 0x1E, 0xC3, 0x4D, 0x35,
            0xED, 0x9B, 0x6D, 0x58, 0xCB, 0x99, 0x81, 0xCA, 0x1C, 0x9E, 0xCE, 0x01, 0x40, 0x11, 0x5B, 0x38,
            0x41, 0xED, 0x78, 0x63, 0xE4, 0xD7, 0x8E, 0x2D, 0x70, 0xEF, 0xD3, 0x4B, 0xF9, 0x51, 0x93, 0x7C,
            0x91, 0x8D, 0x4F, 0x93, 0xB3, 0x6A, 0xFE, 0x86, 0x09, 0xE9, 0x97, 0x53, 0xF5, 0xE8, 0xFB, 0xB1,
            0x64, 0x50, 0x66, 0x2D, 0x96, 0xF9, 0x3B, 0xFB, 0x11, 0xFC, 0xF3, 0x66, 0xCE, 0x40, 0xA0, 0x4C,
            0x59, 0x34, 0xC3, 0x5C, 0x06, 0x90, 0xD7, 0x74, 0x48, 0xF2, 0x97, 0x35, 0xB4, 0xC1, 0xAE, 0xEA,
            0xF8, 0x8D, 0xCC, 0x1F, 0x25, 0xD0, 0x7D, 0x0B, 0x8D, 0x6E, 0x18, 0x73, 0x81, 0x2F, 0x9A, 0xA3,
            0x31, 0x3E, 0x6B, 0x2F, 0x5D, 0xEF, 0x06, 0x40, 0x28, 0x49, 0x85, 0xAD, 0x5F, 0x71, 0xB6, 0xF2,
            0x02, 0x45, 0x28, 0x8F, 0x56, 0x88, 0xBA, 0x5E, 0x6B, 0x07, 0xE8, 0xDE, 0x8A, 0x3A, 0x5D, 0x0A,
            0x50, 0xD3, 0x89, 0xE4, 0xB7, 0xD8, 0x65, 0x91, 0x2D, 0x47, 0x65, 0x1D, 0xD8, 0x04, 0xB2, 0x53,
            0xB3, 0x57, 0x1F, 0x96, 0xE6, 0xA3, 0xEC, 0x8A, 0xC1, 0x43, 0x3D, 0xA8, 0x5A, 0x7F, 0xE5, 0x28,
            0x75, 0x5D, 0x87, 0x4D, 0x0D, 0x6F, 0xA2, 0x53, 0xE2, 0x99, 0x0D, 0x9C, 0x26, 0x2F, 0x9D, 0x06,
            0x0B, 0x62, 0x62, 0xF5, 0xA8, 0x9C, 0x6F, 0xE5, 0x20, 0x8D, 0x28, 0x8D, 0x53, 0x86, 0xE7, 0x53,
            0xDC, 0xDC, 0x63, 0xF1, 0x44, 0x93, 0x9C, 0x67, 0x65, 0x1E, 0x2A, 0x35, 0xE6, 0xC7, 0x9E, 0x0D,
            0xEF, 0x29, 0x30, 0x3F, 0x07, 0x69, 0xC8, 0xE4, 0x3D, 0xA7, 0x65, 0xA4, 0x25, 0x94, 0xCD, 0x82,
            0x14, 0xCB, 0x69, 0x4C, 0xCD, 0x8B, 0xE3, 0xA5, 0x84, 0x47, 0xF1, 0xB2, 0x42, 0x24, 0x6E, 0xAC,
            0xAF, 0xE5, 0xF9, 0xDC, 0x43, 0x5F, 0x89, 0xAB, 0xA4, 0x65, 0x93, 0x2A, 0xF5, 0xC6, 0xBA, 0xCE,
            0x3E, 0xF3, 0xEA, 0xF2, 0x17, 0xAA, 0x7D, 0x5C, 0x3B, 0x1B, 0xFC, 0x85, 0xD0, 0xE3, 0xBF, 0x55,
            0x2C, 0xC2, 0xE7, 0x54, 0xD6, 0x13, 0x7D, 0xDD, 0xB5, 0xE5, 0x61, 0x65, 0xA1, 0x1F, 0x50, 0x98,
            0xFB, 0x11, 0xDC, 0x94, 0x26, 0x83, 0x9B, 0x02, 0xE2, 0x73, 0xF7, 0xC1, 0x34, 0x9C, 0x41, 0x77,
            0x28, 0x81, 0xB4, 0x8B, 0x98, 0xFA, 0x45, 0x0E, 0x2F, 0x19, 0x6E, 0xEA, 0xDA, 0x20, 0xE0, 0x5C,
            0x89, 0x13, 0x18, 0xE5, 0xCF, 0xC8, 0x94, 0xB2, 0x48, 0xCC, 0x16, 0xB7, 0xB6, 0x00, 0x43, 0x6F,
            0x61, 0x52, 0xD9, 0x92, 0x22, 0xC7, 0xC5, 0xA1, 0x0A, 0x2F, 0x33, 0x01, 0xCC, 0xA3, 0xDE, 0x0B,
            0x29, 0x6A, 0xD4, 0x4C, 0x7B, 0x06, 0xDB, 0x4F, 0x2D, 0x26, 0x53, 0xD7, 0xA9, 0xED, 0x58, 0xE9,
            0x54, 0xF4, 0x01, 0x32, 0x44, 0xC1, 0xB8, 0x76, 0xF4, 0x6A, 0xC0, 0x46, 0x44, 0x3E, 0xEA, 0x2E,
            0xA4, 0x85, 0x5F, 0xEF, 0x57, 0xD5, 0x77, 0xB6, 0x4D, 0x99, 0xA2, 0x6D, 0x83, 0xB5, 0xA0, 0x2A
        };
    }
}