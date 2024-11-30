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
    public class Game1 : Game {
        public D d;
        public A a;
        private GraphicsDeviceManager _graphics;
        public UTs uts;
        public Editor editor;

        private SpriteBatch _spriteBatch;

        public D D {
            get {return d;}
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            this.d = new D(this);
            this.d.n = new N();
            this.a = new A(this, d);
            this.uts = new UTs();

            IsMouseVisible = true;

            IsFixedTimeStep = true;

            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadDefTextures();
            LoadDefFonts();
            EditorS editor_settings = new EditorS();
            this.d.gui = new GUI(this, d, a);
            this.editor = new Editor(this, d, a, d.gui, "editor", editor_settings);

            SetWindowSize(D.def_wW2, D.def_wH2);
            D.wW = D.def_wW2;
            D.wH = D.def_wH2;

            d.m = new M(d.mW, d.mH, d);
            d.m.GenerateDefault();
            d.n.zX = 32*256;
            d.n.zY = 32*128;

            D.gui = new GUI(this, d, a);
        }
        protected void LoadDefFonts() {
            d.Fon("DejaVuSans", Content.Load<SpriteFont>("DejaVuSans"));
        }
        protected void LoadDefTextures() {
            d.Tex("tp", Content.Load<Texture2D>("tp"));
            d.Tex("tpbt", Content.Load<Texture2D>("tpbt"));
            d.Tex("pl", Content.Load<Texture2D>("pl"));
            d.Tex("stel", Content.Load<Texture2D>("st_el"));
            d.Tex("guiuniv", Content.Load<Texture2D>("guiuniv"));


            d.Tex("button_play_hover", Content.Load<Texture2D>("button_play_hover"));
            d.Tex("button_play_clicked", Content.Load<Texture2D>("button_play_clicked"));
            d.Tex("button_settings_normal", Content.Load<Texture2D>("button_settings_normal"));
            d.Tex("button_settings_hover", Content.Load<Texture2D>("button_settings_hover"));
            d.Tex("button_settings_clicked", Content.Load<Texture2D>("button_settings_clicked"));
            d.Tex("button_credits_normal", Content.Load<Texture2D>("button_credits_normal"));
            d.Tex("button_credits_hover", Content.Load<Texture2D>("button_credits_hover"));
            d.Tex("button_credits_clicked", Content.Load<Texture2D>("button_credits_clicked"));
            d.Tex("button_exit_normal", Content.Load<Texture2D>("button_exit_normal"));
            d.Tex("button_exit_hover", Content.Load<Texture2D>("button_exit_hover"));
            d.Tex("button_exit_clicked", Content.Load<Texture2D>("button_exit_clicked"));
            d.Tex("button_singleplayer_normal", Content.Load<Texture2D>("button_singleplayer_normal"));
            d.Tex("button_singleplayer_hover", Content.Load<Texture2D>("button_singleplayer_hover"));
            d.Tex("button_singleplayer_clicked", Content.Load<Texture2D>("button_singleplayer_clicked"));
            d.Tex("button_multiplayer_normal", Content.Load<Texture2D>("button_multiplayer_normal"));
            d.Tex("button_multiplayer_hover", Content.Load<Texture2D>("button_multiplayer_hover"));
            d.Tex("button_multiplayer_clicked", Content.Load<Texture2D>("button_multiplayer_clicked"));
        }
        public void SetWindowSize(int w, int h) {
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;

            _graphics.ApplyChanges();
        }
        protected void KeyboardMouseInput() {
            d.p_ms = d.ms;
            d.ms = Mouse.GetState();
            d.p_ks = d.ks;
            d.ks = Keyboard.GetState();
        }
        protected void WASDArrowsNav() {
            bool s = a.LShiftHold();

            if (a.AHold() || a.LeftHold()) {
                if (!s) {d.n.zXAcc -= 5.0f;}
                else {d.n.zXAcc -= 15.0f;}
            }
            if (a.DHold() || a.RightHold()) {
                if (!s) {d.n.zXAcc += 5.0f;}
                else {d.n.zXAcc += 15.0f;}
            }
            if (a.WHold() || a.UpHold()) {
                if (!s) {d.n.zYAcc -= 5.0f;}
                else {d.n.zYAcc -= 15.0f;}
            }
            if (a.SHold() || a.DownHold()) {
                if (!s) {d.n.zYAcc += 5.0f;}
                else {d.n.zYAcc += 15.0f;}
            }
        }
        public bool ut_success = false;
        public bool ut_start = true;
        public void InitializeAllUnitTests() {
            uts.Add("gui_uta", new GUI_UT1("GUI Unit Test A", this, d, a));
            uts.Add("gui_utb", new GUI_UT2("GUI Unit Test B", this, d, a));

        }
        public void RunAllUnitTests() {
            ut_start = false;

            uts.Run();

            ut_success = true;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(ut_start) {
                InitializeAllUnitTests();
                RunAllUnitTests();
            }
            if (!ut_start && ut_success) {

            } else {
                return;
            }

            KeyboardMouseInput();
            WASDArrowsNav();
            d.n.ApplyNavAcc();
            Draw(gameTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // if (editor) {
            editor.Tick();
            // }

            D.m.Draw(_spriteBatch);
            D.gui.Draw(_spriteBatch);
            D.gui.Activity();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    };
}