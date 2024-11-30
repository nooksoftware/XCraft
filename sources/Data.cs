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
    public class D {
        public Game1 game;
        public M m;
        public N n;

        public Dictionary<TT, V2i> tpPos;
        public Dictionary<string, Texture2D> textures;

        public MouseState ms;
        public MouseState p_ms;
        public KeyboardState ks;
        public KeyboardState p_ks;
        
        public int mW = 512;
        public int mH = 256;
        public readonly float g = 9.0f;

        public D(Game1 game) {
            this.game = game;
            this.tpPos = new Dictionary<TT, V2i>();
            this.textures = new Dictionary<string, Texture2D>();
            LoadDefTpPos();
        }
        public void Tex(string id, Texture2D tex) {
            textures.Add(id, tex);
        }
        public Texture2D Tex(string id) {
            Texture2D t;
            textures.TryGetValue(id, out t);
            return t;
        }
        public void LoadDefTpPos() {
            LoadTPPos(TT.DIRT, 0, 0);
            LoadTPPos(TT.GRASS, 1, 0);
            LoadTPPos(TT.STONE, 2, 0);
            LoadTPPos(TT.BEDROCK, 3, 0);
            LoadTPPos(TT.SAND, 4, 0);
            LoadTPPos(TT.CLAY, 5, 0);
            LoadTPPos(TT.WATER, 6, 0);
            LoadTPPos(TT.MUD, 7, 0);
            LoadTPPos(TT.LOG1, 0, 1);
            LoadTPPos(TT.LOG2, 1, 1);
            LoadTPPos(TT.LOG3, 2, 1);
            LoadTPPos(TT.LEAVES1, 3, 1);
            LoadTPPos(TT.LEAVES2, 4, 1);
            LoadTPPos(TT.LEAVES3, 5, 1);
            LoadTPPos(TT.WOODEN_PL1, 0, 2);
            LoadTPPos(TT.WOODEN_PL2, 1, 2);
            LoadTPPos(TT.WOODEN_PL3, 2, 2);
            LoadTPPos(TT.WOOD1, 3, 2);
            LoadTPPos(TT.WOOD2, 4, 2);
            LoadTPPos(TT.WOOD3, 5, 2);
            LoadTPPos(TT.BRICKS, 0, 3);
            LoadTPPos(TT.CONCRETE, 1, 3);
            LoadTPPos(TT.STONE_BRICKS, 2, 3);
            LoadTPPos(TT.METAL, 3, 3);
            LoadTPPos(TT.WOODEN_BOX, 4, 3);
            LoadTPPos(TT.METAL_BOX, 5, 3);
            LoadTPPos(TT.IRON_ORE, 0, 4);
            LoadTPPos(TT.GOLD_ORE, 1, 4);
            LoadTPPos(TT.DIA_ORE, 2, 4);
        }
        protected void LoadTPPos(TT tt, int x, int y) {
            tpPos.Add(tt, new V2i(x,y));
        }
        public int xM {
            get {return (ms.X);}
        }
        public int yM {
            get {return (ms.Y);}
        }

        public int wW = 1280;
        public int wH = 720;
        public readonly int def_mW = 512;
        public readonly int def_mH = 256;
        public readonly int def_wW = 1280;
        public readonly int def_wH = 720;
        public readonly int def_wW2 = 1600;
        public readonly int def_wH2 = 900;
    };
}