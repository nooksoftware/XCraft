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
        public static int navX = 256*32;
        public static int navY = 64*32;
        public static int windowWidth = 1600;
        public static int windowHeight = 900;
    };
    public enum EntityType {
        UNKNOWN = 0,
        PLAYER = 1,
        SLIMER = 2,
        BPLAYER = 3
    };
    public class Entity {
        public float x = 0;
        public float y = 0;
        public EntityType type = 0;
        public Texture2D t;

        public Entity(int x, int y, EntityType type, Texture2D t) {
            this.x = x;
            this.y = y;
            this.type = type;
            this.t = t;
        }
        public void Draw(SpriteBatch spriteBatch) {
            Rectangle d = new Rectangle(
                (int)x-Acc.navX,
                (int)y-Acc.navY,
                t.Width, t.Height);
            Rectangle o = new Rectangle(0,0,t.Width, t.Height);
            spriteBatch.Draw(t, d, o, Color.White);
        }
    };
    public class Player : Entity {
        public Player(int x, int y, Texture2D t) 
         : base(x,y,EntityType.PLAYER, t)
        {

        }
        public void Move(float x, float y) {
            this.x += x;
            this.y += y;
        }
    };
    public class Tile {
        public int x = 0;
        public int y = 0;
        public Texture2D t;
        public int tp_x = -1;
        public int tp_y = -1;
        public TileType type = TileType.UNKNOWN;
        public bool HasTp() {
            return (tp_x != -1) && (tp_y != -1);
        }
        public Tile(int x, int y, int tp_x, int tp_y, TileType type, Texture2D tex) {
            this.x = x;
            this.y = y;
            this.tp_x = tp_x;
            this.tp_y = tp_y;
            this.t = tex;
            this.type = type;
        }
        public bool IsntUnknown() {
            return (type != TileType.UNKNOWN || type != TileType.AIR) ;
        }
        public void Draw(SpriteBatch spriteBatch) {
            if (HasTp() && IsntUnknown()) {
                Rectangle d = new Rectangle(x*32 - Acc.navX, y*32 - Acc.navY,32,32);
                Rectangle o = new Rectangle(tp_x*x, tp_y*y, 32, 32);
                if (d.X < 0-32 || d.Y < 0-32) {
                    return;
                }
                if (d.X > 1600 || d.Y > 900) {
                    return;
                }
                spriteBatch.Draw(t, d, o, Color.White);
            }
        }
    };
    /*
    public class Tile {
        public int x;
        public int y;
        public int tp_pos_x = -1;
        public int tp_pos_y = -1;
        public TileType t;
        public Tile(int x, int y, TileType t, int tp_pos_x, int tp_pos_y) {
            this.x = x;
            this.y = y;
            this.t = t;
            this.tp_pos_x = tp_pos_x;
            this.tp_pos_y = tp_pos_y;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D tp) {
            if (tp_pos_x < 0 || tp_pos_y < 0) {
                if(t == TileType.AIR) {
                    //nothing
                }
            } else {
                Rectangle dest = new Rectangle(x*32 - Acc.navX, y*32 - Acc.navY, 32, 32);
                if (dest.X + 32 > 0 && dest.Y + 32 > 0 && dest.X < Acc.windowWidth && dest.Y < Acc.windowHeight) {
                    spriteBatch.Draw(tp, new Rectangle(32*tp_pos_x, 32*tp_pos_y, 32, 32), dest, Color.White);
                }
            }
        }
    };*/
    public class Game1 : Game
    {
        private Tile[,] tiles;
        private Player this_player;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<TileType, Vec2i> tp_pos;
        private Texture2D tp;
        private Texture2D player_t;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            random = new Random();
            _textures = new Dictionary<string, Texture2D>();
            tp_pos = new Dictionary<TileType, Vec2i>();

        }
        protected void LoadGraphicsTextures() {
            LoadTextures();
            tp = Content.Load<Texture2D>("tp");
            player_t = Content.Load<Texture2D>("button_play_normal");
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

        protected void AddTPP(TileType t, int x, int y) {
            this.tp_pos.Add(t, new Vec2i(x,y));
        }
        protected void LoadTPPos() {
            tp_pos = new Dictionary<TileType, Vec2i>();
        
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
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SetWindowSize(Acc.windowWidth, Acc.windowHeight);

            LoadGraphicsTextures();
            LoadTPPos();
            LoadMap();
        }
        public void SetWindowSize(int w, int h) {
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            _graphics.ApplyChanges();
        }
        private int mapWidth = 512;
        private int mapHeight = 256;
        private TileType[,] tiles_types;
        protected void LoadMap() {
            LoadMapTT();
            tiles = new Tile[mapWidth,mapHeight];
            for (int i = 0; i < mapWidth; i++) {
                for (int j = 0; j < mapHeight; j++) {
                    TileType tiletype = tiles_types[i,j];
                    if (tiletype != TileType.AIR && tiletype != TileType.UNKNOWN) {
                        int tpx = tp_pos[tiletype].x;
                        int tpy = tp_pos[tiletype].y;
                        tiles[i,j] = new Tile(i,j, tpx, tpy, tiletype, tp);
                    } else {
                        tiles[i,j] = new Tile(i,j, -1, -1, tiletype, tp);
                    }
                }
            }

            this_player = new Player(32*(256), 32*(128), player_t);
        }
        protected Random random;
        protected bool PercRandom(int perc) {
            int v = random.Next(100);
            return (v < perc);
        }
        protected void LoadMapTT() {
            

            tiles_types = new TileType[mapWidth,mapHeight];
            for (int x = 0; x < mapWidth; x++) {
                for (int y = 0; y < mapHeight; y++) {
                    if (y > mapHeight - 3) {
                        bool bis = PercRandom(80);
                        if (bis) tiles_types[x,y] = TileType.BEDROCK;
                        else {tiles_types[x,y] = TileType.STONE;}
                    } else if (y > 128) {
                        bool bis = PercRandom(90);
                        if (bis) tiles_types[x,y] = TileType.STONE;
                        else {tiles_types[x,y] = TileType.DIRT;}


                    } else if (y > 128-64) {
                        tiles_types[x,y] = TileType.DIRT;
                    } else if (y == 128-64) {
                        tiles_types[x,y] = TileType.GRASS;
                    } else {
                        tiles_types[x,y] = TileType.AIR;
                    }
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int mvX = 0;
            int mvY = 0;
            int ch = 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                mvX -= ch;
            } else if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                mvX += ch;
            } else if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                mvY -= ch;
            } else if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                mvY += ch;
            }
            this_player.Move(mvX, mvY);

            Acc.navX = (int)((-Acc.windowWidth / 2) + this_player.x);
            Acc.navY = (int)((-Acc.windowHeight / 2) + this_player.y);

            Draw(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            RenderTiles();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        protected void RenderTiles() {
            for (int i = 0; i < mapWidth; i++) {
                for (int j = 0; j < mapHeight; j++ ){
                    tiles[i,j].Draw(_spriteBatch);
                }
            }
            this.this_player.Draw(_spriteBatch);
        }
    };
}
