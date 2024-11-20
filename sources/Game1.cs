
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
    public enum ZoomState {
        ZS_1_0 = 0,
        ZS_2_0,
        ZS_0_5
    };
    public class Zoom {
        static public float _cameraZoom = 1.0f;
        static public ZoomState _zoom_state = ZoomState.ZS_1_0;
        static public float _cameraX = 0;
        static public float _cameraY = 0;
        static public float _cameraXAcc = 0.0f;
        static public float _cameraYAcc = 0.0f;
        static public float _maxAcc = 15.0f;
        static public float _acc = 1.0f;
        static public float _deacceleration = 0.7f;
        static protected bool _centered = true;
        static public int CameraXi {
            get {return (int)(_cameraX);}
            // System.Convert.ToInt32(
        }
        static public int CameraYi {
            get {return (int)(_cameraY);}
        }

        static public void Update() {
            Add(_cameraXAcc, _cameraYAcc);
            ZoomUpdate();
        }
        static public void Acc(float x, float y) {
            _cameraXAcc += x;
            _cameraYAcc += y;
        }
        static public void Add(float x, float y) {
            _cameraX += x;
            _cameraY += y;
        }
        static public void Add(int x, int y) {
            _cameraX += (float)x;
            _cameraY += (float)y;
        }
        static public void ZoomUpdate() {
            if (_cameraXAcc > 0.01f || _cameraXAcc < -0.01f)
            {_cameraXAcc *= _deacceleration;} else {_cameraXAcc = 0.0f;}

            if (_cameraYAcc > 0.01f || _cameraYAcc < -0.01f)
            {_cameraYAcc *= _deacceleration;} else {_cameraYAcc = 0.0f;}

            if (_centered == false) {
            if (_zoom_state == ZoomState.ZS_1_0) {
                MoveChangeTo1_0();
            } else if (_zoom_state == ZoomState.ZS_2_0) {
                MoveChangeTo2_0();
            } else if (_zoom_state == ZoomState.ZS_0_5) {
                MoveChangeTo0_5();
            }
            }
        }
        static public void MoveChangeTo1_0() {
            
            float desired_zoom = 1.0f;
            float diff = desired_zoom - _cameraZoom;
            _cameraZoom += diff * 0.2f;
            if (_cameraZoom < 1.05f && _cameraZoom > 0.95f) {
                _cameraZoom = 1.0f;
                _centered = true;
            }
        }
        static public void MoveChangeTo2_0() {
            float desired_zoom = 2.0f;
            float diff = desired_zoom - _cameraZoom;
            _cameraZoom += diff * 0.2f;
            if (_cameraZoom > 1.95f) {
                _cameraZoom = 2.0f;
                _centered = true;
            }
        }
        static public void MoveChangeTo0_5() {
            float desired_zoom = 0.5f;
            float diff = desired_zoom - _cameraZoom;
            _cameraZoom += diff * 0.2f;
            if (_cameraZoom < 0.55f) {
                _cameraZoom = 0.5f;
                _centered = true;
            }
        }
        public static void Change(ZoomState o) {
            if (_zoom_state == 0) {
                return;
            }
            _zoom_state = o;
            _centered = false;
        }
    };
    public class GameAccess : XCraftLib.IAccess {
        public static GameAccess _instance;
        public GameAccess() {

        }
        public static GameAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameAccess();
            }
            return _instance;
        }
       public static GameAccess A {
            get {return GetInstance();}
       }
        public override bool IsClientAccess() {
            return true;
        }   
        public override bool IsServerAccess() {
            return false;
        }
        public Dictionary<TileType, Vec2i> TexturePackPositions() {
            return Get<Dictionary<TileType, Vec2i>>("tp_pos");
        }
        public Vec2i TexturePackPos(TileType type) {
            Dictionary<TileType, Vec2i> tp_pos = TexturePackPositions();
            return tp_pos[type];
        }
        public int TileSize() {
            return 32;
        }
        public Texture2D TexturePack() {
            return Get<Texture2D>("tp");
        }
        public Texture2D GUITexturePack() {
            return Get<Texture2D>("gui");
        }
        public SpriteBatch SpriteBatch() {
            return Get<SpriteBatch>("sprite_batch");
        }
        public Zoom Zoom() {
            return Get<Zoom>("zoom");
        }
    };
    public enum GameModeType {
        SKIRMISH = 1
    };
    public class Gameplay {
        protected Game1 parent;
        public Gameplay(Game1 parent) {
            this.parent = parent;
        }
        protected bool _ready= false;
        public bool Ready {
            get {return _ready;}
        }
        public Map current_map;
        public void NewDefaultmap() {
            current_map = new Map();
            current_map.LoadAsDefaultMap();
            current_map.LoadEntities();
            _ready = true;
        }
        public Player GetThisPlayer() {
            return current_map.GetThisPlayer();
        }
    };
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private XCraftLib.Graphics xlib_graphics;
        private XCraftLib.GUI xlib_gui;
        private XCraftLib.Audio xlib_audio;
        private XCraftLib.Network network;
        private GameAccess game_access;
        private Gameplay gameplay;
        private Zoom zoom;
        private Map current_map;

        public Game1()
        {
            Console.ForegroundColor = System.ConsoleColor.White;
            
            game_access = GameAccess.GetInstance();

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            game_access.Add("graphics_device_manager", _graphics);
        }
        protected void LoadGraphicsTextures() {
            Dictionary<string, Texture2D> textures = 
                xlib_graphics._textures;

            textures.Add("tp", Content.Load<Texture2D>("tp"));
            textures.Add("gui", Content.Load<Texture2D>("gui"));
            xlib_graphics.gui_texture = textures["gui"];
            xlib_graphics.gui_texture_label="gui";
            xlib_graphics.tp_texture = textures["tp"];
            xlib_graphics.tp_label = "tp";

            textures.Add("logo", Content.Load<Texture2D>("logo"));

            LoadGUITextures();
        }
        protected void LoadGUITextures() {
            Dictionary<string, Texture2D> textures = 
                xlib_graphics._textures;

            textures.Add("button_play_normal", Content.Load<Texture2D>("button_play_normal"));
            textures.Add("button_play_hover", Content.Load<Texture2D>("button_play_hover"));
            textures.Add("button_play_clicked", Content.Load<Texture2D>("button_play_clicked"));
            textures.Add("button_settings_normal", Content.Load<Texture2D>("button_settings_normal"));
            textures.Add("button_settings_hover", Content.Load<Texture2D>("button_settings_hover"));
            textures.Add("button_settings_clicked", Content.Load<Texture2D>("button_settings_clicked"));
            textures.Add("button_credits_normal", Content.Load<Texture2D>("button_credits_normal"));
            textures.Add("button_credits_hover", Content.Load<Texture2D>("button_credits_hover"));
            textures.Add("button_credits_clicked", Content.Load<Texture2D>("button_credits_clicked"));
            textures.Add("button_exit_normal", Content.Load<Texture2D>("button_exit_normal"));
            textures.Add("button_exit_hover", Content.Load<Texture2D>("button_exit_hover"));
            textures.Add("button_exit_clicked", Content.Load<Texture2D>("button_exit_clicked"));

            //todo
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
            Dictionary<TileType, Vec2i> tp_pos = game_access.Get<Dictionary<TileType, Vec2i>>("tp_pos");
        
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
            game_access.Add("sprite_batch", _spriteBatch);

            game_access.Add("window_width", 1280);
            game_access.Add("window_height", 720);
            int w = game_access.Get<int>("window_width");
            int h = game_access.Get<int>("window_height");
            SetWindowSize(w,h);
            game_access.Add<int>("tile_size", 32);
            game_access.Add<Map>("current_map", current_map);

            game_access.Add("tp_pos", new Dictionary<TileType, Vec2i>());
            LoadTPPos();

            xlib_graphics = new XCraftLib.Graphics(
                game_access,
                _spriteBatch
            );
            
        
            LoadGraphicsTextures();
            xlib_gui = new XCraftLib.GUI(game_access, xlib_graphics);
            xlib_audio = new XCraftLib.Audio();

            zoom = new Zoom();
            game_access.Add("zoom", zoom);
            gameplay = new Gameplay(this);
            network = new XCraftLib.Network(game_access, XCraftLib.NetworkType.CLIENT);
            

            int begin_y_offset = 44*4;
                        
            XCraftLib.GUISprite logo_sprite = xlib_gui.NewGUISprite("logo", new XCraftLib.GUISpriteSettings(w/2, 150, -1, -1,
                "logo"
            ));
            logo_sprite.SetMidOrigin();

            XCraftLib.Button play_button = xlib_gui.NewButton("play", new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.5), -1, -1, 
                "button_play_normal",
                "button_play_hover",
                "button_play_clicked",
                new XCraftLib.ActionOnClick()
            ));
            play_button.SetMidOrigin();
                
            XCraftLib.Button settings_button = xlib_gui.NewButton("settings", new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.25), -1, -1, 
                "button_settings_normal",
                "button_settings_hover",
                "button_settings_clicked",
                new XCraftLib.ActionOnClick()
            ));
            settings_button.SetMidOrigin();
                
            XCraftLib.Button credits_button = xlib_gui.NewButton("credits", new XCraftLib.ButtonSettings(w/2, h/2, -1, -1, 
                "button_credits_normal",
                "button_credits_hover",
                "button_credits_clicked",
                new XCraftLib.ActionOnClick()
            ));
            credits_button.SetMidOrigin();
                
            XCraftLib.Button exit_button = xlib_gui.NewButton("exit", new XCraftLib.ButtonSettings(w/2, h/2+(int)(begin_y_offset*0.25), -1, -1, 
                "button_exit_normal",
                "button_exit_hover",
                "button_exit_clicked",
                new XCraftLib.ActionOnClick()
            ));
            exit_button.SetMidOrigin();
            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

            Draw(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            xlib_gui.Tick();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
