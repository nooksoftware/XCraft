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
    public class Game1 : Game {
        public Access access;

        public GUISystem gui;
        public GraphicsSystem graphics;
        public AudioSystem audio;
        public NetworkSystem network;
        public GameplaySystem gameplay;
        public Zoom zoom;

        public Texture2D tp;
        public GraphicsDeviceManager graphics_device_manager;
        public SpriteBatch sprite_batch; 

        public Game1() {
            InitializeSystems();   

            graphics_device_manager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SetWindowSize(1280, 720);
        }
        private void InitializeSystems() {
            access = Access.A;
            gui = new GUISystem();
            graphics = new GraphicsSystem();
            audio = new AudioSystem();
            network = new NetworkSystem();
            gameplay = new GameplaySystem();
            zoom = new Zoom();
            
            graphics.InitializeTPPositions();

            access.gui = gui;
            access.graphics = graphics; 
            /*-*/ 
            access.tp_positions = graphics.tp_positions;
            access.audio = audio;
            access.network = network;
            access.gameplay = gameplay;
            access.zoom = zoom;
            
        }
        private void SetWindowSize(int w, int h) {
            graphics_device_manager.PreferredBackBufferWidth = w;
            graphics_device_manager.PreferredBackBufferHeight = h;
            graphics_device_manager.ApplyChanges();
        }
        protected override void Initialize() {
            base.Initialize();
        }
        protected override void LoadContent() {
            sprite_batch = new SpriteBatch(GraphicsDevice);
            access.sprite_batch = sprite_batch;

            LoadTextures();
            LoadDefaultMap();

            access.game_running = true;
        }
        private void LoadTextures() {
            tp = Content.Load<Texture2D>("tp");
            graphics.texture_pack = tp;
        }
        private void LoadDefaultMap() {
            gameplay.NewDefaultMap();
            access.current_map = gameplay.current_map;
        }
        protected bool IsZoomActive() {
            return true;
        }
        protected void OnLeftKey() {
            if (IsGameRunning()) {
                if (IsZoomActive()) {Zoom.Acc(-Zoom._acc, 0.0f);}
            }
        }
        protected void OnRightKey() {
            if (IsGameRunning()) {
                if (IsZoomActive()) {Zoom.Acc(Zoom._acc, 0.0f);}
            }
        }
        protected void OnUpKey() {
            if (IsGameRunning()) {
                if (IsZoomActive()) {Zoom.Acc(0.0f, -Zoom._acc);}
            }
        }
        protected void OnDownKey() {
            if (IsGameRunning()) {
                if (IsZoomActive()) {Zoom.Acc(0.0f, Zoom._acc);}
            }
        }
        protected void ThisPlayerMovement() {
            gameplay.GetThisPlayer();
        }
        protected void CameraAndZoom() {
            Zoom.Update();
        }
        protected void KeyboardArrowsAndWASD() {
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) {
                OnLeftKey();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) {
                OnRightKey();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) {
                OnUpKey();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) {
                OnDownKey();
            }
        }
        protected void GameUpdate(GameTime gameTime) {

        }
        protected bool IsGameRunning() {
            return access.game_running;
        }
        protected bool IsMenuRunning() {
            return access.menu_running;
        }
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {Exit();}

            KeyboardArrowsAndWASD();
            
            if (IsGameRunning()) {
                ThisPlayerMovement();
                CameraAndZoom();
            }
            if (access.game_running) {
                GameUpdate(gameTime);
            }

            Draw(gameTime);
            base.Update(gameTime);
        }
        protected void GameRender(GameTime gameTime) {
            gameplay.current_map.Draw(gameTime);
            //gameplay.current_map.Activity(gameTime);
        }
        protected void MenuRender(GameTime gameTime) {

        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);
            sprite_batch.Begin();
            if (IsGameRunning()) {
                GameRender(gameTime);
            } else if (IsMenuRunning()) {
                MenuRender(gameTime);
            }
            //gameplay.current_map.Draw(gameTime);
            //gameplay.current_map.Activity(gameTime);
            sprite_batch.End();
            base.Draw(gameTime);
        }
    };
}