﻿using static CMFLib.Helpers;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_39241 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            uint increment = kidx % 61;

            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = (header.DataCount + digest[7]) & 511;
            uint increment = header.BuildVersion + header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - 73) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x0A, 0x04, 0x5A, 0x81, 0xEA, 0x38, 0x65, 0x16, 0xEE, 0x53, 0x52, 0x6E, 0xAB, 0xCF, 0x50, 0xCB,
            0x9E, 0xEC, 0x02, 0x26, 0x90, 0x1A, 0xB2, 0xE3, 0xAF, 0xA7, 0x79, 0x63, 0xE1, 0x0E, 0x5F, 0xDB,
            0x6E, 0xA8, 0x21, 0x8B, 0xF6, 0x86, 0x1A, 0x95, 0xC3, 0xF7, 0xC0, 0xDA, 0xB3, 0xA1, 0xEB, 0xBA,
            0xAD, 0x39, 0xD9, 0xA8, 0x09, 0x21, 0x1C, 0xB8, 0xC7, 0x5D, 0xF4, 0xD8, 0x03, 0xCF, 0x5D, 0x67,
            0xDD, 0xB2, 0x8D, 0xEE, 0x83, 0x91, 0x29, 0x05, 0x7C, 0x21, 0x1D, 0xB5, 0x0D, 0xE2, 0xE6, 0x70,
            0xD8, 0x3D, 0xC9, 0xAE, 0x52, 0x31, 0x31, 0x2F, 0xB0, 0xB2, 0x9B, 0x82, 0xB3, 0x38, 0xDC, 0xD0,
            0xD6, 0x64, 0x12, 0xCF, 0xB1, 0xBA, 0x8D, 0x96, 0x1A, 0xFF, 0x67, 0xA6, 0x78, 0x91, 0xD3, 0xDC,
            0xA4, 0x1D, 0x03, 0x39, 0xA8, 0x3D, 0x1F, 0x47, 0x2D, 0xFD, 0x9F, 0x10, 0x84, 0xCD, 0x4B, 0x78,
            0x9A, 0x88, 0x86, 0xAD, 0x97, 0xC9, 0x71, 0x54, 0xA7, 0xBF, 0x4F, 0xC0, 0x0F, 0xCF, 0xD2, 0x38,
            0x75, 0x4D, 0x7A, 0xBB, 0x6F, 0x78, 0x3B, 0x1C, 0xA5, 0x4E, 0x55, 0xA6, 0x7A, 0x32, 0x08, 0x69,
            0x8B, 0xD2, 0x90, 0x17, 0x53, 0xDD, 0x4C, 0x90, 0xC5, 0x51, 0xA8, 0x11, 0xC6, 0x26, 0x5E, 0xE0,
            0x61, 0x54, 0xCC, 0xB1, 0xD7, 0x74, 0x5A, 0x25, 0x31, 0xCC, 0x3B, 0x5F, 0x7C, 0x9F, 0x58, 0x67,
            0x34, 0x25, 0x93, 0xB6, 0x43, 0x7F, 0xE8, 0x9D, 0xBB, 0xD0, 0xA5, 0x25, 0xDB, 0x7C, 0xD8, 0x0C,
            0x5C, 0x96, 0xBE, 0xCB, 0xD5, 0xF9, 0x07, 0xFE, 0xBB, 0x1D, 0x9D, 0x54, 0x6A, 0xA1, 0x7B, 0xA2,
            0x6B, 0xFD, 0x7B, 0x5B, 0xE4, 0x9B, 0x7F, 0x38, 0x56, 0xBA, 0xEC, 0x1B, 0x69, 0xE0, 0x40, 0xCB,
            0xDA, 0xC5, 0x7C, 0x39, 0xB0, 0x23, 0xBD, 0x95, 0x12, 0x8B, 0x18, 0x19, 0x03, 0xD9, 0xDA, 0x3E,
            0x64, 0xF5, 0x9A, 0x03, 0xE5, 0x02, 0x84, 0x24, 0xF6, 0x5E, 0xFD, 0x13, 0x00, 0x12, 0x3B, 0x0C,
            0x9C, 0x24, 0x93, 0x8D, 0x75, 0xBB, 0x90, 0x78, 0xB6, 0x90, 0x6E, 0xA2, 0x55, 0x18, 0x41, 0x4A,
            0x6D, 0x06, 0x7F, 0x3C, 0x50, 0xC1, 0xF4, 0x40, 0x02, 0x87, 0xC1, 0xC8, 0x0F, 0x13, 0xC8, 0x90,
            0xAB, 0x8A, 0xE0, 0xF6, 0xE2, 0x03, 0xA5, 0x74, 0x58, 0x70, 0xAC, 0x92, 0x9C, 0xB0, 0xC1, 0x8E,
            0x70, 0xE1, 0x34, 0xCD, 0x60, 0x35, 0x7E, 0x9D, 0xB0, 0xB7, 0x61, 0xD6, 0xEC, 0x1B, 0x73, 0xF4,
            0xD1, 0x98, 0x12, 0x8A, 0xA2, 0x94, 0x75, 0x9A, 0xAC, 0xBD, 0x27, 0x13, 0xBA, 0x8C, 0xBC, 0x65,
            0x72, 0x42, 0xD2, 0x67, 0xB3, 0x2E, 0xA9, 0xF9, 0xDF, 0x6A, 0x0D, 0x0D, 0xAE, 0x25, 0x1E, 0x04,
            0x78, 0xAC, 0x73, 0xC7, 0xA3, 0xE4, 0xEB, 0xDA, 0x02, 0xF6, 0x52, 0x44, 0xF8, 0xF9, 0xBE, 0xB6,
            0x98, 0x5B, 0xD5, 0xDB, 0x09, 0x74, 0x58, 0x54, 0xDF, 0xFE, 0x5C, 0xD5, 0xEF, 0x87, 0x89, 0xF3,
            0xFF, 0x7A, 0xDE, 0x48, 0x0C, 0x56, 0x96, 0xFF, 0x34, 0x0C, 0x4A, 0xD1, 0xEB, 0x5E, 0xA0, 0x60,
            0x68, 0xE1, 0xE5, 0xF6, 0x41, 0xF7, 0x17, 0xC0, 0xBB, 0xB0, 0x08, 0xCE, 0x9A, 0xFC, 0xA1, 0xEB,
            0x0B, 0x5B, 0xD5, 0xC1, 0x34, 0x4E, 0x2E, 0x14, 0x69, 0xA7, 0x39, 0x90, 0x5A, 0x56, 0x91, 0xBE,
            0xDE, 0x61, 0xA9, 0x83, 0x2B, 0x8E, 0x9D, 0xD1, 0x9B, 0xE1, 0xAD, 0x57, 0x45, 0x61, 0x68, 0x1A,
            0x62, 0x88, 0xEE, 0xA8, 0x73, 0x8B, 0x01, 0x7D, 0x3B, 0xF9, 0x2F, 0x52, 0xA4, 0x0A, 0xF2, 0x8C,
            0x71, 0xE2, 0x4C, 0x35, 0x72, 0xF2, 0x30, 0x29, 0x14, 0x02, 0x94, 0x67, 0x0B, 0xCA, 0xD8, 0xFD,
            0x49, 0x68, 0x85, 0x46, 0xB2, 0x77, 0x59, 0x50, 0x91, 0xA7, 0xE9, 0xC9, 0x4D, 0x1C, 0x6B, 0x5C
        };
    }
}