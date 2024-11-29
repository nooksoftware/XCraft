using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
using System.Security.Cryptography;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
//using FastNoiseLite;


namespace XCraft {
        public class G {
        public M m;

        int w = 512;
        int h = 256;

        int mH3 = 3;
        int mH2 = 96;
        int mH1 = 160;

        int iOp = 2200;
        int gOp = 4500;
        int dOp = 6000;

        public G(Map m, int w, int h, int wH3, int wH2, int wH1, int iOp, int gOp, int dOp, bool gen = false) {
            Set(m, w, h, mH3, mH2, mH1, iOp, gOp, dOp);
            if (gen) {
                Generate();
            }
        }
        public void Generate() {

        }
        public void Set(Map m, int w, int h, int wH3, int wH2, int wH1, int iOp, int gOp, int dOp) {
            if (m != null) {this.m = m;}
            if (w != -1) {this.w = w;}
            if (h != -1) {this.h = h;}
            if (mH3 != -1) {this.mH3 = mH3;}
            if (mH2 != -1) {this.mH2 = mH2;}
            if (mH1 != -1) {this.mH1 = mH1;}
            if (iOp != -1) {this.iOp = iOp;}
            if (gOp != -1) {this.gOp = gOp;}
            if (dOp != -1) {this.dOp = dOp;}
        }
    };
}