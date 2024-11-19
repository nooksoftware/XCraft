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
            this.tiletype= tiletype;

            if (tiletype != TileType.AIR) {
                if (tp_pos == null) {
                    Console.WriteLine("tp_pos nullptr");
                }
                if (Access.A.graphics.tp_positions == null) {
                    Console.WriteLine("tp_positions nullptr");
                }
                tp_pos._x = Access.A.graphics.tp_positions[tiletype].x;
                tp_pos._y = Access.A.graphics.tp_positions[tiletype].y;
                origin_rect = new Rectangle(
                    32*tp_pos._x,
                    32*tp_pos._y,
                    32,
                    32
                );
            }
       }
       public void Draw() {
            if (tiletype == TileType.AIR) {
                return;
            }
            Access access = Access.A;
            Texture2D tp = access.graphics.texture_pack;
            
            int screen_x = ((map_pos._x * 32) - Zoom._cameraX);
            int screen_y = ((map_pos._y * 32) - Zoom._cameraY);
            
            //int screen_x = System.Convert.ToInt32(((map_pos.X * tile_size - (access.window_width/2)) - Zoom.CameraX) * Zoom.CameraZoom);
            //int screen_y = System.Convert.ToInt32(((map_pos.Y * tile_size - (access.window_height/2)) - Zoom.CameraY) * Zoom.CameraZoom);
            int screen_w = System.Convert.ToInt32(System.Math.Ceiling(32*Zoom.CameraZoom));
            int screen_h = System.Convert.ToInt32(System.Math.Ceiling(32*Zoom.CameraZoom));

            Rectangle destination_rect = new Rectangle(
                screen_x,
                screen_y,
                screen_w,
                screen_h
            );

            SpriteBatch sprite_batch = access.sprite_batch;

            sprite_batch.Draw(tp, destination_rect, origin_rect, Color.White);
       }
       public void Activity() {
            Access access = Access.A;
       }
    };  
}