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

        protected bool noiseOutput = false;
        public Game1(/*string[] args*/)
        {
            /*
            foreach(var arg in args) {
                if (arg == "noise") {
                    noiseOutput = true;
                }
            }*/

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
        protected void LoadDefEffects() {
            //d.Eff("MotionBlur", Content.Load<Effect>("MotionBlur"));
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
        protected int previousMouseWheelValue = 0;
        int pressSDelay = 0;

        protected void WASDArrowsMouseNav() {
            bool spectatorModeKeyPressed = false;

            if (a.OnePressedP() && pressSDelay == 0) {
                spectatorModeKeyPressed = true;
                pressSDelay = 40;
            }
            if (pressSDelay > 0) {
                pressSDelay = pressSDelay - 1;
            } else {
                pressSDelay = 0;
            }

            if (spectatorModeKeyPressed) {
                d.spectator = !d.spectator;
            }

            bool s = a.LShiftHold();

            previousMouseWheelValue = D.ms.ScrollWheelValue - previousMouseWheelValue;

            if (previousMouseWheelValue > 0) // Scroll up
                d.n.zZAcc += 0.06f;
            else if (previousMouseWheelValue < 0) // Scroll down
                d.n.zZAcc -= 0.06f;

            D.n.zZ += d.n.zZAcc;
            D.n.zZAcc *= 0.85f;
            if (D.n.zZAcc < 0.01f && D.n.zZAcc > -0.01f) {
                D.n.zZAcc = 0f;
            }
            if (D.n.zZ > 2.0f) {
                D.n.zZ = 2.0f;
            } else if (d.n.zZ < 0.5f) {
                D.n.zZ = 0.5f;
            }

            previousMouseWheelValue = D.ms.ScrollWheelValue;
            if (D.spectator) {
                if (a.AHold() || a.LeftHold()) {
                    if (!s) {d.n.zXAcc -= 2.5f;}
                    else {d.n.zXAcc -= 6.0f;}
                }
                if (a.DHold() || a.RightHold()) {
                    if (!s) {d.n.zXAcc += 2.5f;}
                    else {d.n.zXAcc += 6.0f;}
                }
                if (a.WHold() || a.UpHold()) {
                    if (!s) {d.n.zYAcc -= 2.5f;}
                    else {d.n.zYAcc -= 6.0f;}
                }
                if (a.SHold() || a.DownHold()) {
                    if (!s) {d.n.zYAcc += 5.0f;}
                    else {d.n.zYAcc += 6.0f;}
                }
            } else {
                
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
            WASDArrowsMouseNav();
            d.n.ApplyNavAcc();
            Draw(gameTime);

            base.Update(gameTime);
        }
        protected void DrawSimpleShapesTest(GameTime gametime) {
            SpriteBatch s = _spriteBatch;

            a.pixel = new Texture2D(GraphicsDevice, 1, 1);
            a.pixel.SetData(new[] {Color.White});

            s.Begin();

            a.DrawRectangleOutline1px(s, 10, 10, 40, 10, Color.White);
            a.DrawRectangleOutline1px(s, 10, 22, 40, 5, Color.Yellow);

            a.DrawLineAtoB(s, 80, 20, 160, 40, Color.White);
            a.DrawLineAtoB(s, 160, 40, 80, 20, Color.White);
            a.DrawLineAtoB(s, 80, 20, 160, 20, Color.Yellow);

            a.DrawRectangleFilledBG(s, 10, 80, 50, 10, Color.White);
            a.DrawRectangleFilledBG(s, 10, 85, 40, 20, Color.White);

            a.DrawRectagleOutlineAndFilledBG(s, 10, 120, 90, 40, Color.White, Color.Black);
            a.DrawRectagleOutlineAndFilledBG(s, 10, 180, 80, 30, Color.Black, Color.White);

            a.DrawCircleOutline1px(s, 200, 30, 40, Color.White);
            a.DrawCircleOutline1px(s, 210, 30, 40, Color.Yellow);

            a.DrawCircleFilledBG(s, 200, 100, 30, Color.White);

            a.DrawCircleFilledBGandOutline1px(s, 300, 20, 50, Color.White, Color.Black);

            //DrawRectangleOutline1px(s, x, y, w, h, outline_color)
            //DrawLineAtoB(s, x, y, x2, yw, line_color)
            //DrawRectangleFilledBG(s, x, y, w, h, bg_color)
            //DrawRectagleOutlineAndFilledBG(s, x, y, w, h, bg_color, outline_color)
            //DrawCircleOutline1px(s, x, y, anglesize, outline_color)
            //DrawCircleFilledBG(s, x, y, anglesize, bg_color)
            //DrawCircleFilledBGandOutline1px(s, x, y, anglesize, bg_color, outline_color)

            s.End();

            //DrawTex2DMotionBlur(d.Tex("tp"), 400, 400, s);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // if (editor) {
            editor.Tick();
            // }

            D.m.Draw(_spriteBatch);
            //D.gui.DrawGT(_spriteBatch);
            //D.gui.ActivityGT();
            _spriteBatch.End();
            
            //DrawSimpleShapesTest(gameTime);
            base.Draw(gameTime);
        }
    };
}