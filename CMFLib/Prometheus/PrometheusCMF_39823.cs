﻿using static CMFLib.Helpers;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_39823 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = header.BuildVersion * (uint) length;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += 3;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = header.BuildVersion * (uint) length;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx = header.BuildVersion - kidx;
                buffer[i] ^= digest[(i + kidx) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x91, 0x27, 0x29, 0xDA, 0x64, 0x19, 0x01, 0x37, 0x28, 0x19, 0x83, 0xEA, 0xA8, 0x0A, 0xDE, 0x5B,
            0x77, 0xC2, 0xAD, 0xFA, 0xD6, 0x84, 0x04, 0xBC, 0x2D, 0xC1, 0x69, 0xC7, 0x9E, 0x32, 0xC3, 0x92,
            0xD4, 0xFD, 0xDE, 0x4C, 0x07, 0x75, 0x17, 0xE1, 0x79, 0x86, 0xEB, 0x74, 0xF7, 0xF7, 0x0A, 0xC3,
            0x11, 0xD9, 0xBF, 0xCC, 0x34, 0xF5, 0xB4, 0xA6, 0x56, 0xA5, 0x05, 0x45, 0x52, 0x10, 0xA0, 0xEA,
            0xDC, 0x25, 0x24, 0xA3, 0xAE, 0x13, 0x93, 0x6F, 0xAD, 0x44, 0x7D, 0x3D, 0xB4, 0x6B, 0x15, 0xAC,
            0x33, 0xD3, 0x11, 0x65, 0xA2, 0xCD, 0x29, 0xAB, 0x76, 0xDA, 0x04, 0xD9, 0x10, 0x44, 0x49, 0x82,
            0xA9, 0x1D, 0xF9, 0xD7, 0x3F, 0xD1, 0x60, 0x8C, 0x7B, 0x89, 0x26, 0x77, 0x76, 0x29, 0xAC, 0x1B,
            0xBB, 0xED, 0x99, 0xD6, 0x1C, 0xC8, 0xFC, 0x39, 0x05, 0x62, 0x9A, 0x5A, 0x14, 0xE8, 0x4D, 0x6C,
            0x06, 0xA5, 0x91, 0xD5, 0x9C, 0x53, 0x4A, 0x62, 0x5D, 0x65, 0x9A, 0x4E, 0x8A, 0xE8, 0x6F, 0x15,
            0x51, 0xE5, 0xCF, 0xDA, 0x7E, 0x78, 0xAF, 0xC2, 0x48, 0x23, 0xF2, 0x0C, 0x9F, 0x1B, 0x64, 0xA5,
            0x59, 0x44, 0x92, 0xDF, 0x5D, 0xFA, 0xDE, 0x23, 0x37, 0x19, 0x47, 0xA1, 0xE7, 0x5C, 0x8C, 0x0E,
            0xDF, 0xA7, 0x71, 0xAC, 0xF2, 0xF3, 0xD5, 0x53, 0xFC, 0xA3, 0x54, 0xE8, 0x1A, 0xBE, 0x91, 0x50,
            0x61, 0x2B, 0x0D, 0x96, 0xDB, 0x99, 0xA3, 0xE9, 0x34, 0x86, 0x24, 0x09, 0xBF, 0x20, 0x09, 0xA2,
            0x71, 0x2E, 0xAE, 0x44, 0xCA, 0x51, 0xCB, 0x6E, 0xE5, 0xD7, 0xAF, 0xD3, 0x5F, 0x45, 0x0F, 0xEC,
            0x8C, 0xD9, 0x83, 0x5F, 0x0E, 0x8D, 0xA6, 0x83, 0x1A, 0x9C, 0x10, 0x73, 0x2E, 0x3C, 0x1A, 0xA6,
            0x41, 0xCE, 0x40, 0x88, 0xC4, 0x2D, 0x73, 0x79, 0x9F, 0x84, 0x34, 0xA9, 0xF0, 0x1A, 0x41, 0x94,
            0xFB, 0xFE, 0xDD, 0xAC, 0x37, 0xBE, 0x70, 0x1D, 0x72, 0xD9, 0xE8, 0xE9, 0xF7, 0x1D, 0x4A, 0xDA,
            0xD8, 0x40, 0xA5, 0xA8, 0xBB, 0x80, 0x25, 0x7B, 0x76, 0x0C, 0xA6, 0x4F, 0xC8, 0x2B, 0xA7, 0x29,
            0x71, 0xCB, 0x37, 0x03, 0x64, 0x36, 0xC1, 0x05, 0x6B, 0xD3, 0x63, 0x86, 0xFD, 0x69, 0x57, 0x01,
            0x6A, 0xBE, 0x00, 0x25, 0x92, 0xDF, 0xA4, 0x88, 0xB5, 0x8C, 0x9D, 0xF1, 0xE4, 0x89, 0x43, 0x39,
            0xA6, 0x5D, 0xDF, 0x4E, 0x2C, 0xCA, 0xAC, 0x56, 0xAB, 0x84, 0x11, 0x89, 0xFA, 0xD0, 0xD4, 0x74,
            0xB2, 0x05, 0xCD, 0x69, 0x4E, 0x08, 0x11, 0xEB, 0xDB, 0xAF, 0x80, 0xD0, 0xF5, 0x85, 0xE9, 0x50,
            0x4B, 0xFB, 0x9F, 0x75, 0xCC, 0xDA, 0x27, 0xE7, 0xC1, 0x6A, 0x93, 0xBE, 0xDA, 0x21, 0x8B, 0xF8,
            0x58, 0x81, 0xE7, 0x55, 0xDE, 0x63, 0xB8, 0xA2, 0xAB, 0xD6, 0x69, 0x0B, 0x59, 0xA7, 0x4F, 0x2A,
            0x87, 0xA1, 0x2F, 0x93, 0xE1, 0x0C, 0xFD, 0x96, 0xD9, 0xEB, 0x2D, 0x1C, 0x9A, 0x8F, 0x3F, 0x0B,
            0x08, 0xA4, 0x98, 0x72, 0xA4, 0x10, 0x6F, 0x34, 0x05, 0x2E, 0x14, 0x4F, 0xA0, 0x9A, 0x50, 0x8D,
            0x64, 0xDB, 0x86, 0xF5, 0xB5, 0x12, 0xC0, 0x96, 0xF2, 0x54, 0xE7, 0x60, 0xC2, 0xD4, 0x61, 0x75,
            0xFB, 0xE5, 0x2D, 0x8C, 0x6A, 0x2B, 0xF1, 0xFC, 0xAB, 0x97, 0xA9, 0x6C, 0x8D, 0xEC, 0x04, 0xB7,
            0x80, 0x88, 0x3E, 0x8C, 0xA1, 0x0E, 0x15, 0x44, 0x3D, 0x29, 0xAD, 0x32, 0xB1, 0xF5, 0xB2, 0x6F,
            0xC7, 0x71, 0xCD, 0xC0, 0xD4, 0xC4, 0x3C, 0x5D, 0x9B, 0x80, 0xC3, 0x67, 0xE6, 0x21, 0xB5, 0x01,
            0x6B, 0x9C, 0x4F, 0xA1, 0x5B, 0x03, 0xB2, 0x17, 0x28, 0xE6, 0xA6, 0x69, 0x39, 0xEC, 0xA0, 0x8E,
            0xF0, 0xD7, 0x12, 0x3F, 0xF7, 0x5C, 0xB6, 0xA5, 0x4B, 0xAD, 0x71, 0xFC, 0x9C, 0x20, 0x2B, 0xB9
        };
    }
}