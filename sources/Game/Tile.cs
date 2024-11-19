
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

namespace XCraft {
    public static class A{
        public static Rectangle Copy(Rectangle b) {
            return new Rectangle(b.X, b.Y, b.Width, b.Height);
        }
        public static Vec2i Copy(Vec2i b) {
            return new Vec2i(b._x, b._y);
        }
    };
    public enum TileType {
        AIR = 256,
        DIRT = 0, GRASS, STONE, COBBLESTONE, SAND, CLAY, WATER, MUD,
        LOG1, LOG2, LOG3, LEAVES1, LEAVES2, LEAVES3, _TT_U_1, _TT_U_2,
        WOODEN_PL1, WOODEN_PL2, WOODEN_PL3, WOOD1, WOOD2, WOOD3, FLATWOOD1, FLATWOOD2,
        BRICKS, CONCRETE, STONE_BRICKS, METAL, WOODEN_BOX, METAL_BOX,
        BEDROCK
    };
    public class Tile {
        public TileType tiletype = TileType.AIR;
        protected Vec2i tp_pos;
        protected Vec2i map_pos;
        protected Rectangle origin_rect;
        public Tile(int x, int y, TileType tiletype) {
            this.tp_pos = new Vec2i(0,0); //determine
            this.map_pos = new Vec2i(x,y);
            this.tiletype = tiletype;
            Set(tiletype);
            
       }
        public void Set(TileType tiletype) {
            this.tiletype = tiletype;

            if (tiletype != TileType.AIR) {
                if (tp_pos == null) {
                    Console.WriteLine("tp_pos nullptr");
                }
                if (GameAccess.A.TexturePackPositions() == null) {
                    Console.WriteLine("tp_positions nullptr");
                }
                tp_pos = A.Copy(
                    GameAccess.A.TexturePackPos(tiletype)
                );
                int tile_size = GameAccess.A.TileSize();
                origin_rect = new Rectangle(
                    tile_size*tp_pos._x,
                    tile_size*tp_pos._y,
                    tile_size,
                    tile_size
                );
            }
        }
        public void Draw() {
            if (tiletype == TileType.AIR) {
                return;
            }
            GameAccess gameaccess = GameAccess.A;
            Texture2D tp = gameaccess.TexturePack();

            Zoom zoom = gameaccess.Zoom();

            int screen_x = (int)(-Zoom._cameraX + map_pos._x + gameaccess.TileSize());
            int screen_y = (int)(-Zoom._cameraY + map_pos._y + gameaccess.TileSize());
        
            int screen_w = gameaccess.TileSize();
            int screen_h = gameaccess.TileSize();

            Rectangle d_rect = new Rectangle(
                screen_x,
                screen_y,
                screen_w,
                screen_h
            );

            SpriteBatch sprite_batch = gameaccess.SpriteBatch();

            sprite_batch.Draw(tp, d_rect, origin_rect, Color.White);

        }
        public void Activity() {

        }
    };
}