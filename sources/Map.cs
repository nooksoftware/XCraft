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
    public class M {
        private FastNoiseLite lite;
        public int w = 0;
        public int h = 0;


        public TT[,] tts;
        public T[,] ts;
        public T[,] ts_visible;

        public Map(int w, int h) {
            this.w = w;
            this.h = h;
            this.tts = new TT[w,h];
            this.ts = new T[w,h];
            this.ts_visible = null;
        }
        public void GenerateDefault() {
            G g = new G((this, 512, 256, -1, -1, -1, -1, -1, -1, true));
        }

        public TT TT(int x, int y) {
            if (tts[x,y] != null) 
                return tts[x,y];

            return null;
        }
        public T T(int x, int y) {
            if (ts[x,y] != null) 
                return ts[x,y];

            return null;
        }
    };
}