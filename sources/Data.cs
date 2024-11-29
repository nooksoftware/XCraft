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
        public NatAddressType n;

        public Dictionary<TileType, V2i> tpPos;
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
            this.tpPos = new Dictionary<TileType, V2i>();
            this.textures = new Dictionary<string, Texture2D>();
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
            LoadTPPos(TileType.DIRT, 0, 0);
            LoadTPPos(TileType.GRASS, 0, 1);
            LoadTPPos(TileType.STONE, 0, 2);
            LoadTPPos(TileType.BEDROCK, 0, 3);
            LoadTPPos(TileType.SAND, 0, 4);
            LoadTPPos(TileType.CLAY, 0, 5);
            LoadTPPos(TileType.WATER, 0, 6);
            LoadTPPos(TileType.MUD, 0, 7);

            LoadTPPos(TileType.LOG1, 1, 0);
            LoadTPPos(TileType.LOG2, 1, 1);
            LoadTPPos(TileType.LOG3, 1, 2);
            LoadTPPos(TileType.LEAVES1, 1, 3);
            LoadTPPos(TileType.LEAVES2, 1, 4);
            LoadTPPos(TileType.LEAVES3, 1, 5);

            LoadTPPos(TileType.WOODEN_PL1, 2, 0);
            LoadTPPos(TileType.WOODEN_PL2, 2, 1);
            LoadTPPos(TileType.WOODEN_PL3, 2, 2);
            LoadTPPos(TileType.WOOD1, 2, 3);
            LoadTPPos(TileType.WOOD2, 2, 4);
            LoadTPPos(TileType.WOOD3, 2, 5);

            LoadTPPos(TileType.BRICKS, 3, 0);
            LoadTPPos(TileType.CONCRETE, 3, 1);
            LoadTPPos(TileType.STONE_BRICKS, 3, 2);
            LoadTPPos(TileType.METAL, 3, 3);
            LoadTPPos(TileType.WOODEN_BOX, 3, 4);
            LoadTPPos(TileType.METAL_BOX, 3, 5);

            LoadTPPos(TileType.IRON_ORE, 4, 0);
            LoadTPPos(TileType.GOLD_ORE, 4, 1);
            LoadTPPos(TileType.DIA_ORE, 4, 2);
        }
        protected void LoadTPPos(TT tt, int x, int y) {
            tpPos.Add(t, new V2i(x,y));
        }
        protected void LoadDefTextures() {
            
        }
        public int xM {
            get {return (ms.X);}
        }
        public int yM {
            get {return (ms.Y);}
        }

        public readonly int def_mW = 512;
        public readonly int def_mH = 256;
        public readonly int def_wW = 1280;
        public readonly int def_wH = 720;
        public readonly int def_wW2 = 1600;
        public readonly int def_wH2 = 900;
    };
}