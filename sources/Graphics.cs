using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
//using FastNoiseLite;

namespace vgCSh {
    public class GraphicsSystem {
        public Texture2D texture_pack;
        public Dictionary<TileType, Vec2i> tp_positions;
        public GraphicsSystem() {
            tp_positions = new Dictionary<TileType, Vec2i>();
        }
        public void InitializeTPPositions(/* string tp */) {
            LoadTexturePackPos();
        }
        public Rectangle TileTextureBounds(TileType t) {
            Vec2i p = tp_positions[t];
            return new Rectangle(
                32*p._x,
                32*p._y,
                32,
                32
            );
        }
        protected void LoadTexturePackPos() {
            AddTPP(TileType.DIRT, 0, 0);
            AddTPP(TileType.GRASS, 0, 1);
            AddTPP(TileType.STONE, 0, 2);
            AddTPP(TileType.BEDROCK, 0, 3);
            AddTPP(TileType.SAND, 0, 4);
            AddTPP(TileType.CLAY, 0, 5);
            AddTPP(TileType.WATER, 0, 6);
            AddTPP(TileType.MUD, 0, 7);

            AddTPP(TileType.LOG1, 1, 0);
            AddTPP(TileType.LOG2, 1, 1);
            AddTPP(TileType.LOG3, 1, 2);
            AddTPP(TileType.LEAVES1, 1, 3);
            AddTPP(TileType.LEAVES2, 1, 4);
            AddTPP(TileType.LEAVES3, 1, 5);

            AddTPP(TileType.WOODEN_PL1, 2, 0);
            AddTPP(TileType.WOODEN_PL2, 2, 1);
            AddTPP(TileType.WOODEN_PL3, 2, 2);
            AddTPP(TileType.WOOD1, 2, 3);
            AddTPP(TileType.WOOD2, 2, 4);
            AddTPP(TileType.WOOD3, 2, 5);

            AddTPP(TileType.BRICKS, 2, 0);
            AddTPP(TileType.CONCRETE, 2, 1);
            AddTPP(TileType.STONE_BRICKS, 2, 2);
            AddTPP(TileType.METAL, 2, 3);
            AddTPP(TileType.WOODEN_BOX, 2, 4);
            AddTPP(TileType.METAL_BOX, 2, 5);
        }
        protected void AddTPP(TileType t, int x, int y) {
            AddTexturePackPos(t,x,y);
        }
        protected void AddTexturePackPos(TileType t, int x, int y) {
            tp_positions.Add(t, new Vec2i(x,y));
        }
    };  
}