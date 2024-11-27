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
        METAL_BOX,
        IRON_ORE,
        GOLD_ORE,
        DIA_ORE
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

        public static bool RectContains(Rectangle r, int x, int y) {
            return (x > r.X && x < r.X+r.Width && y > r.Y && y < r.Y+r.Height);
        }
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
    public class Scenario {};
    
    public class Tile {
        public int x = 0;
        public int y = 0;
        public Texture2D t;
        public int tp_x = -1;
        public int tp_y = -1;
        public TileType type = TileType.UNKNOWN;
        public bool HasTp() {
            return (tp_x != -1) || (tp_y != -1);
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
        public Rectangle draw_d;
        public bool Draw(SpriteBatch spriteBatch) {
            draw_d.X = 0;
            draw_d.Y = 0;
            draw_d.Width = 0;
            draw_d.Height = 0;
            if (HasTp() && IsntUnknown()) {
                Rectangle d = new Rectangle(x*32 - Acc.navX, y*32 - Acc.navY,32,32);
                Rectangle o = new Rectangle(tp_x*32, tp_y*32, 32, 32);
                if (d.X < 0-32 || d.Y < 0-32) {
                    return false;
                }
                if (d.X > 1600 || d.Y > 900) {
                    return false;
                }
                spriteBatch.Draw(t, d, o, Color.White);
                draw_d = d;
                return true;
            } else
            if (type == TileType.AIR) {
                Rectangle d = new Rectangle(x*32 - Acc.navX, y*32 - Acc.navY,32,32);

                if (d.X < 0-32 || d.Y < 0-32) {
                    return false;
                }
                if (d.X > 1600 || d.Y > 900) {
                    return false;
                }
                draw_d = d;
                return true;
            }
            return false;

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
    public class Map {
        public int w = 0;
        public int h = 0;
        public int u_h = 0;
        public int t_h = 0;
        public float ore = 0;
        Dictionary<TileType, Vec2i> tp_pos;
        Tile[,] tiles;
        Tile[,] visible_tiles_frame;

        TileType[,] tile_types;
        Texture2D tp;
        public Map(
            int w, int h, 
            int u_h, int t_h,
            float ore, Dictionary<TileType, Vec2i> tp_pos,
            Texture2D tp
        ) {
            this.w = w;
            this.h = h;
            this.u_h = u_h;
            this.t_h = t_h;
            this.ore = ore;
            this.tp_pos = tp_pos;
            this.tp = tp;

            random = new Random();

            tiles = new Tile[w,h];
            tile_types = new TileType[w,h];
            visible_tiles_frame = new Tile[w,h];
            ClearVisibleTilesFrame();

            Gen();
        }
        protected void ClearVisibleTilesFrame() {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    visible_tiles_frame[x,y] = null;
                }
            }
        }
        protected Random random;
        protected int ToPerc(float v) {
            return (int)(v * 100);
        }
        protected bool PercRandom(int perc) {
            int v = random.Next(100);
            return (v < perc);
        }
        protected bool Random(int f) {
            return (random.Next(f) == 0);
        }
        protected bool LinPercRandom(int b, int e, int v) {
            return PercRandom(ToPerc((float)(v-b) / (float)(b-e)));
        }
        protected float LinPerc(int b, int e, int v) {
            return (float)(v-b) / (float)(b-e);
        }
        protected float LinPercInv(int b, int e, int v) {
            //todo
            return (float)(v-b) / (float)(b-e);
        }
        protected TileType SwitchSet(bool s, TileType a, TileType b) {
            if (s) {
                return a;
            }
            return b;
        }
        protected TileType Determined(int x, int y) {
            if (y > h-3) {
                bool r = LinPercRandom(h-3, h, y);
                return SwitchSet(r, TileType.BEDROCK, TileType.STONE);
            } else if (y > u_h) {
                bool h_linear = LinPercRandom(u_h, t_h+1, y);
                
                if (h_linear) {
                    bool ore1 = Random((int)(180*(ore)));
                    if (ore1) {
                        return TileType.IRON_ORE;                            
                    }
                    bool ore2 = Random((int)(240*(ore)));
                    if (ore2) {
                        return TileType.GOLD_ORE;                        
                    }
                    bool ore3 = Random((int)(360*(ore)));
                    if (ore3) {
                        return TileType.DIA_ORE;                        
                    }
                }

                TileType t = SwitchSet(h_linear, TileType.STONE, TileType.DIRT);
                return t;
            } else if (y == t_h) {
                return TileType.GRASS;
            } else if (y > t_h) {
                return TileType.DIRT;
            } else {
                return TileType.AIR;
            }
        }
        public bool IsntTPApplicable(TileType t) {
            return (t == TileType.UNKNOWN || t == TileType.AIR);
        }
        protected void Gen (){
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    tile_types[x,y] = Determined(x,y);
                }
            }
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    TileType tt = tile_types[x,y];
                    if (IsntTPApplicable(tt)) {
                        tiles[x,y] = new Tile(x,y,-1, -1, tt, tp);
                    } else {
                        int tp_x = tp_pos[tt].x;
                        int tp_y = tp_pos[tt].y;
                        tiles[x,y] = new Tile(x,y,tp_x, tp_y, tt, tp);
                    }
                }
            }
        }
        public void Draw(SpriteBatch sp) {
            ClearVisibleTilesFrame();
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    bool tile_visible = tiles[x,y].Draw(sp);
                    if (tile_visible) {
                        visible_tiles_frame[x,y] = tiles[x,y];
                    }
                }
            }
            
        }
        public Vec2i TilePoint(int x, int y) {
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    if (visible_tiles_frame[i,j] != null) {
                        Tile t = visible_tiles_frame[i,j];
                        if(Acc.RectContains(t.draw_d, x, y)) {
                            return new (i,j);
                        }
                    }
                }
            }
            return null;
        }
        public bool IsAir(int x, int y) {
            return (tiles[x,y].type == TileType.AIR);
        }
        public void AirTile(int x, int y) {
            if (tiles[x,y] != null) {
                tiles[x,y].type = TileType.AIR;
                tiles[x,y].tp_x = -1;
                tiles[x,y].tp_y = -1;
            }
        }
        public void SetTile(int x, int y, TileType tt) {
            int tp_x = tp_pos[tt].x;
            int tp_y = tp_pos[tt].y;
            tiles[x,y] = new Tile(x,y,tp_x, tp_y, tt, tp);
        }
    };
    public class Game1 : Game
    {
        private Map map;
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

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
            
            
        }
        protected void LoadGraphicsTextures() {
            LoadTextures();
            tp = Content.Load<Texture2D>("tp");
            player_t = Content.Load<Texture2D>("pl");
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

        protected void AddTPP(TileType t, int y, int x) {
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

            AddTPP(TileType.BRICKS, 3, 0);
            AddTPP(TileType.CONCRETE, 3, 1);
            AddTPP(TileType.STONE_BRICKS, 3, 2);
            AddTPP(TileType.METAL, 3, 3);
            AddTPP(TileType.WOODEN_BOX, 3, 4);
            AddTPP(TileType.METAL_BOX, 3, 5);

            AddTPP(TileType.IRON_ORE, 4, 0);
            AddTPP(TileType.GOLD_ORE, 4, 1);
            AddTPP(TileType.DIA_ORE, 4, 2);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SetWindowSize(Acc.windowWidth, Acc.windowHeight);

            LoadGraphicsTextures();
            LoadTPPos();
            LoadMap();
            prev_ms = Mouse.GetState();
            ms = Mouse.GetState();
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
            this.map = new Map(mapWidth, mapHeight, (int)(mapHeight*0.75f), (int)(mapHeight*0.5f), 1.0f, tp_pos, tp);
            this_player = new Player(32*(256), 32*(128), player_t);
            /*LoadMapTT();
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

            
            */
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
        private float mvXf = 0.0f;
        private float mvYf = 0.0f;
        private MouseState prev_ms;
        private MouseState ms;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int mvX = 0;
            int mvY = 0;

            int ch = 10;
            bool shift = false;

            
            prev_ms = ms;
            ms = Mouse.GetState();
            int mouse_x = ms.X;
            int mouse_y = ms.Y;
            
            bool lmb_click = prev_ms.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed;
            bool rmb_click = prev_ms.RightButton == ButtonState.Released && ms.RightButton == ButtonState.Pressed;

            if (lmb_click) {
                Vec2i p = map.TilePoint(mouse_x, mouse_y);
                if (p != null) {
                    if (map.IsAir(p.x, p.y)) {
                        map.SetTile(p.x, p.y, TileType.STONE);
                    } else {
                        map.AirTile(p.x, p.y);
                    }
                }
            }
            else if (rmb_click) {
                Vec2i p = map.TilePoint(mouse_x, mouse_y);
                if (p != null) {
                    if (map.IsAir(p.x, p.y)) {
                        map.SetTile(p.x, p.y, TileType.STONE_BRICKS);
                    } else {
                        map.AirTile(p.x, p.y);
                    }
                }
            }
            

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) {
                shift = true;
            }
            if (shift) {
                ch = 25;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                mvX -= ch;
                mvXf -= ch/5.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                mvX += ch;
                mvXf += ch/5.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                mvY -= ch;
                mvYf -= ch/5.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                mvY += ch;
                mvYf += ch/5.0f;
            }
            this_player.Move(mvXf, mvYf);

            if (mvXf < 0.05f && mvXf > -0.05f) {
                mvXf = 0.0f;
            } else {
                mvXf *= 0.8f;
            }
            if (mvYf < 0.05f && mvYf > -0.05f) {
                mvYf = 0.0f;
            } else {
                mvYf *= 0.8f;
            }

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
            map.Draw(_spriteBatch);
            this.this_player.Draw(_spriteBatch);
        }
    };
}
