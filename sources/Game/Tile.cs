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
    public class T {
        public x = 0;
        public y = 0;
        public TT tt;
        public Texture2D tp;
        public int tpX = -1;
        public int tpY = -1;
        public D d;

        public bool IsSolid() {
            return (tt != TileType.UNKNOWN || tt != TileType.AIR || tt != TileType.WATER);
        }
        public bool HasTP() {
            return (tpX != -1) || (tpY != -1); || (tp == null);
        }
        public T(int x, int y, TT tt, Texture2D tp, D d) {
            this.x = x;
            this.y = y;
            this.tt = tt;
            this.tp = tp;
            this.d = d;
        }
    };
    public enum TT {
        UNKNOWN = -1,
        AIR = 0,
        DIRT,
        GRASS,
        STONE,
        BEDROCK,
        SAND,
        CLAY,
        WATER,
        MUD,
        LOG1,
        LOG2,
        LOG3,
        LEAVES1,
        LEAVES2,
        LEAVES3,
        WOODEN_PL1,
        WOODEN_PL2,
        WOODEN_PL3,
        WOOD1,
        WOOD2,
        WOOD3,
        BRICKS,
        CONCRETE,
        STONE_BRICKS,
        METAL,
        WOODEN_BOX,
        METAL_BOX,
        IRON_ORE,
        GOLD_ORE,
        DIA_ORE,
        ORE1,
        ORE2,
        ORE3,
    };
}