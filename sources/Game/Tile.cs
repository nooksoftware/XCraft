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
    public enum TT {
        UNKNOWN = -1,
        AIR = 0,
        STONE = 1,
        ROUGHSTONE,
        BEDROCK,
        COBBLESTONE,
        CLEARSTONE,
        SAND,
        CLAY,
        WATER,
        IRON_ORE,
        GOLD_ORE,
        DIA_ORE,
        ORE1,
        ORE2,
        ORE3,
        DIRT,
        GRASS,
        WOOD1,
        WOOD2,
        LEAVES1,
        LEAVES2,
        PLANKS1,
        PLANKS2,
        PLANKS3,
        BRICKS,
        COBBLESTONE_BRICKS,
        PLANKS1_B,
        PLANKS2_B,
        PLANKS3_B,
        WOODEN_VERTICAL,
        WOODEN_HORIZONTAL,
        WOODEN_BOXAL,
        STONE_VERTICAL,
        STONE_HORIZONTAL,
        STONE_BOXAL,
        IRON_VERTICAL,
        IRON_HORIZONTAL,
        IRON_BOXAL,
        WOODEN_DOOR_TOP,
        STONE_DOOR_TOP,
        IRON_DOOR_TOP,
        WOODEN_DOOR,
        STONE_DOOR,
        IRON_DOOR,
        WOODEN_DOOR_END,
        STONE_DOOR_END,
        IRON_DOOR_END,
        SUPERDOOR_TOP,
        SUPERDOOR,
        SUPERDOOR_END,
        SUPERDOOR2_TOP,
        SUPERDOOR2,
        SUPERDOOR2_END
    };
    /*public enum TT {
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
        COBBLESTONE
    };*/
    public class T {
        public int tpPosX = -1;
        public int tpPosY = -1;
        public int x = 0;
        public int y = 0;
        public TT tt = TT.AIR;
        public A a;
        public T(int x, int y, TT tt, A a) {
            this.x = x;
            this.y = y;
            this.tt = tt;
            this.a = a;

            bool s = a.tppos.TryGetValue(tt, out V2i tpos);
            if (s) {
                tpPosX = tpos.x;
                tpPosY = tpos.y;
            }
        }

        public int ScreenXPos() {
            return
                System.Convert.ToInt32
                ((this.x*a.tileSize - a.n.zX) * (a.n.zZ) + (a.windowWidth/2));
        }
        public int ScreenYPos() {
            return
                System.Convert.ToInt32
                ((this.y*a.tileSize - a.n.zY) * (a.n.zZ) + (a.windowHeight/2));
        }
        public int ScreenWidthHeightSize() {
            return System.Convert.ToInt32(a.tileSize * a.n.zZ);
        }
        public Rectangle RenderOrigin() {
            if (tt == TT.AIR || tt == TT.UNKNOWN) {
                return new Rectangle(-1,-1,-1,-1);
            }
            return new Rectangle(
                System.Convert.ToInt32(tpPosX * a.tileSize),
                System.Convert.ToInt32(tpPosY * a.tileSize),
                System.Convert.ToInt32(a.tileSize),
                System.Convert.ToInt32(a.tileSize)
            );

        }
        public Rectangle RenderDestination() {
            return new Rectangle(
                System.Convert.ToInt32
                ((this.x*a.tileSize - a.n.zX)*a.n.zZ + (a.windowWidth/2)),
                System.Convert.ToInt32
                ((this.y*a.tileSize - a.n.zY)*a.n.zZ + (a.windowHeight/2)),
                System.Convert.ToInt32(a.tileSize * a.n.zZ),
                System.Convert.ToInt32(a.tileSize * a.n.zZ)
            );
        }

        public void Tick(GameTime gameTime) {

        }
        public bool Render(SpriteBatch spriteBatch) {
            if (tt == TT.AIR || tt == TT.UNKNOWN) {
                return false;
            }
            if (tpPosX == -1 || tpPosY == -1) {
                return false;
            }
            //Rectangle o = RenderOrigin();
            //Rectangle d = RenderDestination();

            Rectangle o = 

            new Rectangle(
                System.Convert.ToInt32(tpPosX * a.tileSize),
                System.Convert.ToInt32(tpPosY * a.tileSize),
                System.Convert.ToInt32(a.tileSize),
                System.Convert.ToInt32(a.tileSize)
            );

            Rectangle d = 

            new Rectangle(
                System.Convert.ToInt32
                ((this.x*a.tileSize - a.n.zX)*a.n.zZ + (a.windowWidth/2)),
                System.Convert.ToInt32
                ((this.y*a.tileSize - a.n.zY)*a.n.zZ + (a.windowHeight/2)),
                System.Convert.ToInt32(a.tileSize * a.n.zZ),
                System.Convert.ToInt32(a.tileSize * a.n.zZ)
            );

            

            if (/*a.WithinScreenBounds(d, -24,-24,24,24)*/true) {

                spriteBatch.Draw(a.tp, d, o, Color.White);
                return true;
            } else {
                return false;
            }
        }
    };
}