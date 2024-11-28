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
//using FastNoiseLite;

namespace XCraft {


    public class D { //Data
        public Game1 g;
        public int mW = 512;
        public int mH = 256;
        public float zX = 0.0f;
        public float zY = 0.0f;
        public float z = 1.0f;
        public int zS = 2;

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