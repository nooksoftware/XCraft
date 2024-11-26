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
    public enum TileType {
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
        METAL_BOX
    };
    public enum ZoomState {
        ZS_1_0 = 0,
        ZS_2_0,
        ZS_0_5
    };
    public static class Acc {
        public static int navX = 0;
        public static int navY = 0;
    };
    public class Tile {
        public int x;
        public int y;
        public int tp_pos_x = 0;
        public int tp_pos_y = 0;
        public TileType t;
        public Tile(int x, int y, TileType t, int tp_pos_x, int tp_pos_y) {
            this.x = x;
            this.y = y;
            this.t = t;
            this.tp_pos_x = tp_pos_x;
            this.tp_pos_y = tp_pos_y;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D tp) {
            spriteBatch.Draw(tp, new Rectangle(32*tp_pos_x, 32*tp_pos_y, 32, 32), new Rectangle(x*32 + Acc.navX, y*32 + Acc.navY), Color.White);
        }
    };
    public class Game1 : Game
    {
        private Tile[,] tiles;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<TileType, Vec2i> tp_pos;
        private Texture2D tp;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected void LoadGraphicsTextures() {
            LoadTextures();
            tp = Content.Load<Texture2D>("tp");
        }
        protected void LoadTextures() {
            LoadTex2D("button_play_normal");
            LoadTex2D("button_play_hover");
            LoadTex2D("button_play_clicked");
            LoadTex2D("button_settings_normal");
            LoadTex2D("button_settings_hover");
            LoadTex2D("button_settings_clicked");
            LoadTex2D("button_credits_normal");
            LoadTex2D("button_credits_hover");
            LoadTex2D("button_credits_clicked");
            LoadTex2D("button_exit_normal");
            LoadTex2D("button_exit_hover");
            LoadTex2D("button_exit_clicked");
            LoadTex2D("button_singleplayer_normal");
            LoadTex2D("button_singleplayer_hover");
            LoadTex2D("button_singleplayer_clicked");
            LoadTex2D("button_multiplayer_normal");
            LoadTex2D("button_multiplayer_hover");
            LoadTex2D("button_multiplayer_clicked");
        }
        protected void LoadTex2D(string t) {
            _textures.Add(t, Content.Load<Texture2D>(t));
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected void AddTPP(Dictionary<TileType, Vec2i> tp_pos, TileType t, int x, int y) {
            tp_pos.Add(t, new Vec2i(x,y));
        }
        protected void LoadTPPos() {
            tp_pos = new Dictionary<TileType, Vec2i>();
        
            AddTPP(tp_pos, TileType.DIRT, 0, 0);
            AddTPP(tp_pos, TileType.GRASS, 0, 1);
            AddTPP(tp_pos, TileType.STONE, 0, 2);
            AddTPP(tp_pos, TileType.BEDROCK, 0, 3);
            AddTPP(tp_pos, TileType.SAND, 0, 4);
            AddTPP(tp_pos, TileType.CLAY, 0, 5);
            AddTPP(tp_pos, TileType.WATER, 0, 6);
            AddTPP(tp_pos, TileType.MUD, 0, 7);

            AddTPP(tp_pos, TileType.LOG1, 1, 0);
            AddTPP(tp_pos, TileType.LOG2, 1, 1);
            AddTPP(tp_pos, TileType.LOG3, 1, 2);
            AddTPP(tp_pos, TileType.LEAVES1, 1, 3);
            AddTPP(tp_pos, TileType.LEAVES2, 1, 4);
            AddTPP(tp_pos, TileType.LEAVES3, 1, 5);

            AddTPP(tp_pos, TileType.WOODEN_PL1, 2, 0);
            AddTPP(tp_pos, TileType.WOODEN_PL2, 2, 1);
            AddTPP(tp_pos, TileType.WOODEN_PL3, 2, 2);
            AddTPP(tp_pos, TileType.WOOD1, 2, 3);
            AddTPP(tp_pos, TileType.WOOD2, 2, 4);
            AddTPP(tp_pos, TileType.WOOD3, 2, 5);

            AddTPP(tp_pos, TileType.BRICKS, 2, 0);
            AddTPP(tp_pos, TileType.CONCRETE, 2, 1);
            AddTPP(tp_pos, TileType.STONE_BRICKS, 2, 2);
            AddTPP(tp_pos, TileType.METAL, 2, 3);
            AddTPP(tp_pos, TileType.WOODEN_BOX, 2, 4);
            AddTPP(tp_pos, TileType.METAL_BOX, 2, 5);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SetWindowSize(1600, 900);

            LoadGraphicsTextures();

            LoadMap();
        }
        public void SetWindowSize(int w, int h) {
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
        }
        protected void LoadMap() {
            tiles = new Tile[512,256];
            for (int i = 0; i < 512; i++) {
                for (int j = 0; j < 256; j++) {
                    tiles[i,j] = new Tile(i,j,TileType.AIR);
                }
            }
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

            _spriteBatch.Begin();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
