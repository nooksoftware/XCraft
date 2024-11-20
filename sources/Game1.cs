
using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
using XCraftLib;
//using FastNoiseLite;

namespace XCraft {
    public enum Switch {
        InventorySwitch = 1,
        MinimapExpandSwitch,
        MinimapHideSwitch,
    };
    public enum Value {
        InventoryItemSelected = 1,
        SelectedItem = 2
    };
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
        public GameAccess() {

        }
        public static GameAccess GetCInstance()
        {
            if (_iaccess == null)
            {
                _iaccess = new GameAccess();
            }
            return _iaccess as GameAccess;
        }
       public static GameAccess A {
            get {return GetCInstance();}
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

        public GameAccess GameAccess {
            get {return game_access;}
        }

        public Game1()
        {
            Console.ForegroundColor = System.ConsoleColor.White;
            
            game_access = GameAccess.GetCInstance();

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

            textures.Add("button_singleplayer_normal", Content.Load<Texture2D>("button_singleplayer_normal"));
            textures.Add("button_singleplayer_hover", Content.Load<Texture2D>("button_singleplayer_hover"));
            textures.Add("button_singleplayer_clicked", Content.Load<Texture2D>("button_singleplayer_clicked"));
            textures.Add("button_multiplayer_normal", Content.Load<Texture2D>("button_multiplayer_normal"));
            textures.Add("button_multiplayer_hover", Content.Load<Texture2D>("button_multiplayer_hover"));
            textures.Add("button_multiplayer_clicked", Content.Load<Texture2D>("button_multiplayer_clicked"));

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
            xlib_gui = new XCraftLib.GUI(game_access, xlib_graphics, new GUIActivityAndRenderRules(this));
            //xlib_gui.AddMenu(0, "main_menu");
            //xlib_gui.AddMenu(1, "main_menu_single_multiplayer");
            //xlib_gui.AddMenu(2, "main_menu_settings");
            //xlib_gui.AddMenu(3, "main_menu_credits");
            //xlib_gui.AddMenu(4, "singleplayer_menu");
            //xlib_gui.AddMenu(5, "multiplayer_menu");

            //xlib_gui.AddMenu(6, "game_menu");
            //xlib_gui.SetSwitch("game_menu", (int)Switch.InventorySwitch, false);
            //xlib_gui.SetSwitch("game_menu", (int)Switch.MinimapExpandSwitch, false);
            //xlib_gui.SetSwitch("game_menu", (int)Switch.MinimapHideSwitch, false);
            //xlib_gui.SetValue("game_menu", (int)Value.InventoryItemSelected, 1);


            xlib_audio = new XCraftLib.Audio();

            zoom = new Zoom();
            game_access.Add("zoom", zoom);
            gameplay = new Gameplay(this);
            network = new XCraftLib.Network(game_access, XCraftLib.NetworkType.CLIENT);
            

            LoadGUI();

            this.GameAccess.clientMenuSelected = XCraftLib.ClientMenuSelected.mainMenu;
            //this.GameAccess.gameMenuSelected = XCraftLib.GameMenuSelected;
            // TODO: use this.Content to load your game content here
        }
        protected void LoadGUI() {
            int begin_y_offset = 44*4;
            int w = game_access.Get<int>("window_width");
            int h = game_access.Get<int>("window_height");
                        
            XCraftLib.GUIMenu main_menu = xlib_gui.NewGUIMenu("main_menu", new GUIMenuSettings(0,0,1280,720));
            XCraftLib.GUIMenu main_menu_main_menu = xlib_gui.NewGUIMenu("main_menu/main", new GUIMenuSettings(0,0,1280,720));
            XCraftLib.GUIMenu main_menu_singleplayer_multiplayer_menu = xlib_gui.NewGUIMenu("main_menu/singleplayer_multiplayer_menu", new GUIMenuSettings(0,0,1280,720));

            XCraftLib.GUISprite logo_sprite = xlib_gui.NewGUISprite("main_menu/main/logo", new XCraftLib.GUISpriteSettings(w/2, 150, -1, -1,
                "logo"
            ));
            logo_sprite.SetMidOrigin();

            XCraftLib.Button play_button = xlib_gui.NewButton("main_menu/main/play", new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.5), -1, -1, 
                "button_play_normal",
                "button_play_hover",
                "button_play_clicked",
                new GUIEventChange()
            ));
            play_button.SetMidOrigin();
            play_button.on_clicked_change.clientMenuSelectedChange = XCraftLib.ClientMenuSelected.singleplayerMultiplayerMenu;
                
            XCraftLib.Button settings_button = xlib_gui.NewButton("main_menu/main/settings", new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.25), -1, -1, 
                "button_settings_normal",
                "button_settings_hover",
                "button_settings_clicked",
                new GUIEventChange()
            ));
            settings_button.SetMidOrigin();
            settings_button.on_clicked_change.clientMenuSelectedChange = XCraftLib.ClientMenuSelected.settingsMenu;
                
            XCraftLib.Button credits_button = xlib_gui.NewButton("main_menu/main/credits", new XCraftLib.ButtonSettings(w/2, h/2, -1, -1, 
                "button_credits_normal",
                "button_credits_hover",
                "button_credits_clicked",
                new GUIEventChange()
            ));
            credits_button.SetMidOrigin();
            credits_button.on_clicked_change.clientMenuSelectedChange = XCraftLib.ClientMenuSelected.creditsMenu;
                
            XCraftLib.Button exit_button = xlib_gui.NewButton("main_menu/main/exit", new XCraftLib.ButtonSettings(w/2, h/2+(int)(begin_y_offset*0.25), -1, -1, 
                "button_exit_normal",
                "button_exit_hover",
                "button_exit_clicked",
                null
            ));
            exit_button.SetMidOrigin();

            XCraftLib.Button singleplayer_button = xlib_gui.NewButton("main_menu/singleplayer_multiplayer_menu/singleplayer_button",
            new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.5f), -1, -1,
                "button_singleplayer_normal",
                "button_singleplayer_hover",
                "button_singleplayer_clicked",
                new GUIEventChange()
            ));
            singleplayer_button.SetMidOrigin();
            singleplayer_button.on_clicked_change.clientMenuSelectedChange = XCraftLib.ClientMenuSelected.singleplayerStartMenu;
            

            XCraftLib.Button multiplayer_button = xlib_gui.NewButton("main_menu/singleplayer_multiplayer_menu/multiplayer_button",
            new XCraftLib.ButtonSettings(w/2, h/2-(int)(begin_y_offset*0.25f), -1, -1,
                "button_multiplayer_normal",
                "button_multiplayer_hover",
                "button_multiplayer_clicked",
                new GUIEventChange()
            ));
            multiplayer_button.SetMidOrigin();
            multiplayer_button.on_clicked_change.clientMenuSelectedChange = XCraftLib.ClientMenuSelected.multiplayerStartMenu;
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
            xlib_gui.Render();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class GUIActivityAndRenderRules : XCraftLib.GUIActivityAndRenderRules {
        public GUIActivityAndRenderRules(Game1 game1) {
            this.game1 = game1;
            this.access = game1.GameAccess;
        }
        public override void Render() {
            //menu
            if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.mainMenu) {RenderMainMenu();}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.singleplayerMultiplayerMenu) {RenderSingleplayerMultiplayerMenu();}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.singleplayerStartMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.multiplayerStartMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.settingsMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.creditsMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.gameSettingsMenu) {}
            //game menu
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.gameMenu) {
                if (access.gameMenuSelected == XCraftLib.GameMenuSelected.menu) {

                } else if (access.gameMenuSelected == XCraftLib.GameMenuSelected.settings_menu) {

                } 
                //inventory
                if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.nonselected) {} 
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.inventory) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.roleMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.shopMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.assemblyMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.minerMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.outpustMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.miniOutpustMenu) {}
                //minimap
                if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.standardMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.hiddenMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.expandedMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.invisibleMinimap)  {}
            }
        }
        public override void Activity()
        {
            //menu
            if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.mainMenu) {ActivityMainMenu();}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.singleplayerMultiplayerMenu) {ActivitySingleplayerMultiplayerMenu();}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.singleplayerStartMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.multiplayerStartMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.settingsMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.creditsMenu) {}
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.gameSettingsMenu) {}
            //game menu
            else if (access.clientMenuSelected == XCraftLib.ClientMenuSelected.gameMenu) {
                if (access.gameMenuSelected == XCraftLib.GameMenuSelected.menu) {

                } else if (access.gameMenuSelected == XCraftLib.GameMenuSelected.settings_menu) {

                } 
                //inventory
                if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.nonselected) {} 
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.inventory) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.roleMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.shopMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.assemblyMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.minerMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.outpustMenu) {}
                else if (access.clientInventorySelected == XCraftLib.ClientInventorySelected.miniOutpustMenu) {}
                //minimap
                if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.standardMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.hiddenMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.expandedMinimap)  {}
                else if (access.clientMinimapSelected == XCraftLib.ClientMinimapSelected.invisibleMinimap)  {}
            }
        }

        protected void RenderMainMenu() {
            this.gui.Render("main_menu/main");
        }
        protected void ActivityMainMenu() {
            this.gui.Activity("main_menu/main");
        }

        protected void RenderSingleplayerMultiplayerMenu() {
            this.gui.Render("main_menu/singleplayer_multiplayer_menu");
        }
        protected void ActivitySingleplayerMultiplayerMenu() {
            this.gui.Activity("main_menu/singleplayer_multiplayer_menu"); 
        }

        protected GameAccess access;
        protected Game1 game1;
    };
}
