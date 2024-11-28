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
        Air = 0,
        Dirt = 1,
        Grass = 2,
        Sand = 3,
        Cobblestone = 4
    };
    public class T {
        public int x = 0;
        public int y = 0;
        TT tt;
        public T(int x, int y, TT tt) {
            this.x = x;
            this.y = y;
            this.tt = tt;
        }
        public void Set(TT tt) {
            this.tt = tt;
        }
    };
    public class M {
        T[,] t;
        public D d;
        
        public M(D d)  {
            this.d = d;
            this.t = new T[d.mW, d.mH];
            Fill();
        }
        public void Set(int x, int y, TT tt) {
            if (t[x,y] != null) {
                t[x,y].Set(tt);
            } else { 
                t[x,y] = new T(x,y,tt);
            }
        }
        public void Fill() {
            for (int i = 0; i < d.mW; ++i) {
                for (int j = 0; j < d.mH; ++j) {
                    t[i,j] = new T(i,j, TT.A);
                }
            }
        }
        public void FillH(int bH, int eH, TT tt) {
            for (int x = 0; x < d.mW; x++) {
                for (int y = bH; y < eH; y++) {
                    Set(x,y,tt);
                }
            }
        }
        public void Gen() {
            int mgH2 = d.mgH2;
            int mgH1 = d.mgH1;
            int mgH0 = d.mgH0;

        }
    };
    public class D { //Data
        public Game1 g;
        public int mW = 512;
        public int mH = 256;
        public float zX = 0.0f;
        public float zY = 0.0f;
        public float z = 1.0f;
        public int zS = 2;

        public int mgH2 = 256-3;
        public int mgH1 = 256-96;
        public int mgH0 = 256-128-32;

        public M m;
        

        public D(Game1 g) {
            this.g = g;
            Init();
        }
        public void Init() {
            mW = 512;
            mH = 256;
            zX = mW/2.0f;
            zY = mH/2.0f;
            z = 1.0f;
            zS = 2;
            mgH2 = 256-3;
            mgH1 = 256-96;
            mgH0 = 256-128-32;
        }

    };       
    public class I {
        public I() {

        }
    };
    public class MP {
        public Game1 g;
        public MP(Game1 g) {
            this.g = g;
        }   
    };
    
    public class Game1 : Game {
        public D d;
        public I i;
        public MP mp;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<TileType, Vec2i> tp_pos;
        private Texture2D tp;
        private Texture2D player_t;

        private Gameplay gameplay;


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SetWindowSize(Acc.windowWidth, Acc.windowHeight);

            LoadGraphicsTextures();
            LoadTPPos();
            LoadMap();
            

            gameplay = new Gameplay(
                this,
                this_player,
                _graphics,
                _spriteBatch,
                _textures,
                tp_pos,
                tp,
                player_t,
                mapWidth,
                mapHeight
            );
        }
        public void SetWindowSize(int w, int h) {
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

            Draw(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
    };
}