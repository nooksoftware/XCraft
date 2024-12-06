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
        public D d;


        public TT[,] tts;
        public T[,] ts;
        public T[,] ts_visible;

        public Dictionary<int, GArea> gAreas;

        public Dictionary<int, E> entities;

        public Player thisPlayer;

        public M(int w, int h, D d) {
            this.w = w;
            this.h = h;
            this.d = d;
            this.tts = new TT[w,h];
            this.ts = new T[w,h];
            this.ts_visible = new T[w,h];
            gAreas = new Dictionary<int, GArea>();
            entities = new Dictionary<int, E>();
            //thisPlayer = new PlayeR();
        }
        public void GenerateDefault() {
            G g = new G(d, this, 512, 256, 180, 240, 360, true);
        }

        public TT TT(int x, int y) {
            if (tts[x,y] != null) 
                return tts[x,y];

            return XCraft.TT.UNKNOWN;
        }
        public T T(int x, int y) {
            if (ts[x,y] != null) 
                return ts[x,y];

            return null;
        }
        protected void ClearVisibleTilesFrame() {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    ts_visible[x,y] = null;
                }
            }
        }
        public void Draw(SpriteBatch sp) {
            ClearVisibleTilesFrame();
            for (int x = 0; x < w ; x++) {
                for (int y = 0; y < h; y++) {
                    bool v = T(x,y).Draw(sp);
                    if (v) {
                        ts_visible[x,y] = T(x,y);
                    }
                }
            }
        }
    };
}