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
using System.Net;
//using FastNoiseLite;

using MVector2 = Microsoft.Xna.Framework.Vector2;
using System.Net.Security;

namespace XCraft {
    public class M {
        public int w = 0;
        public int h = 0;
        public A a;

        public T[,] tiles;
        public Dictionary<int, E> entities;

        public Player thisPlayerInstance;
        public Dictionary<long, Player> otherPlayersInstances;

        public M(int w, int h, A a) {
            this.w = w;
            this.h = h;
            this.a = a;

            this.tiles = new T[w,h];
            this.entities = new Dictionary<int, E>();
            this.otherPlayersInstances = new Dictionary<long, Player>();
        }

        public void SetT(int x, int y, TT tt) {
            this.tiles[x,y] = new T(x,y,tt,a);
        }

        public void Render(SpriteBatch spriteBatch) {
            int tileSize = a.tileSize;

            int zX = a.n.zX;
            int zY = a.n.zY;

            int wW = a.windowWidth;
            int wH = a.windowHeight;

            int mW = a.m.w;
            int mH = a.m.h;

            //int beginX = (int)(((zX-wW/2)-tileSize) / tileSize);
            //int beginY = (int)(((zY-wH/2)-tileSize) / tileSize);
            //int endX = (int)((zX-wW/2)+wW+tileSize) / tileSize;
            //int endY = (int)((zY-wH/2)+wH+tileSize) / tileSize;

            int beginX = -1;
            int beginY = -1;
            int endX = -1;
            int endY = -1;

            for (int x = 0; x < w; x++) {
                Rectangle d = this.tiles[x,0].RenderDestination();

                if (d.X > -96 && beginX == -1) {
                    beginX = x;
                    break;
                }

            }
            for (int x = w-1; x >= 0; x-- ){
                Rectangle d = this.tiles[x,0].RenderDestination();

                if (d.X+d.Width < wW+96 && endX == -1) {
                    endX = x;
                    break;
                } 
            }
            for (int y = 0; y < h; y++) {
                Rectangle d = this.tiles[0,y].RenderDestination();

                if (d.Y > -96 && beginY == -1) {
                    beginY = y;
                    break;
                }

            }
            for (int y = h-1; y >= 0; y-- ){
                Rectangle d = this.tiles[0,y].RenderDestination();

                if (d.Y+d.Height < wH+96 && endY == -1) {
                    endY = y;
                    break;
                } 
            }

            if (beginX < 0) {
                beginX = 0;
            } else if (beginX > mW) {
                beginX = mW;
            }
            if (beginY < 0) {
                beginY = 0;
            } else if (beginY > mH) {
                beginY = mH;
            }
            if (endX < 0) {
                endX = 0;
            } else if (endX > mW) {
                endX = mW;
            }
            if (endY < 0) {
                endY = 0;
            } else if (endY > mH) {
                endY = mH;
            }

            for (int x = beginX; x < endX; x++) {
                for (int y = beginY; y < endY; y++) {
                    this.tiles[x,y].Render(spriteBatch);
                }
            }
        }
        public TT GetTT(int x, int y) {
            if (this.tiles[x,y] != null) {
                return this.tiles[x,y].tt;
            } else {
                return XCraft.TT.UNKNOWN;
            }
        }
        public T GetT(int x, int y) {
            if (this.tiles[x,y] != null) {
                return this.tiles[x,y];
            }
            return null;
        }
    };
}