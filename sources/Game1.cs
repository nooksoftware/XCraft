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

using MVector2 = Microsoft.Xna.Framework.Vector2;
using System.Net.Security;
using System.Dynamic;

//////////////// Equilibrium System of Discipline - For a lack of a better title
////////////////     I believe, that the system of discipline should be strict, and also very cognitive in itself.
////////////////     Tapas principles does apply - but very effortfuly, deliberately, and purposefuly - quickly, minimally (habit beyond 2 minutes)
////////////////     Therefore, such a system may be composed of extreme focus, and unwavering determination. It also must imply, watchfulness, like in yoga. It cannot be though be detached from feelings - feelings should be acted upon, through the honest, and mature lens - approach.
////////////////    Enneagram (emotion vector) - 5w4/5w6 -> 1w7
////////////////    Simplistic model, still.



//////////////// Write down all habits, cognitions, emotions, feelings, everything, in rehersal way, focus, read; something. Kant

//////////////// Implement:
////////////////  - Holistic (Analytical-Observant-Ennegram5-Deeply i) Approach of programming, Approach every aspect of programming with feelings
////////////////  - Feeling (Purely Maunic, or Intensively Mystical; Rehersal) Approach of programming
////////////////  - Aspects of integrated system of Purusha, Prakti, in Mahamudra aspects
////////////////  - Given ways of programming - Implement, using various wills, approaches. Create plugins, to automate given programming aspects.
///////////////       - Move them, onto other scopes as well (Unlimited to programming c# or others, itself)
///////////////              - [possible scopes: [Programming iterative; integrative], [Multi-aspected [compilatory, inter-related between various programming languages, such as C#, PHP, JS, Lua among others]], [Intensively Mastery related [deep inner algorithmic or otherwise work]]
//////////////        - If VSCode is not enough, develop own IDE.
//////////////   - XCraft itself; project, of course.

///////////// General, in rehersal, programming - Use as much rehersal, and neutral self-awareness, coupled with positive, feeling [positive] self-awareness, as possible, and use Tapas Principles to develop code, and mastery. Focus on effort, primarely, on attributions, on attributility, inner-connectedness of code, everything, that is not compartmentalized; or cannot be.
///////////// Write down everything, including relating to negative emotions, to see through; transmute through, see through, and develop inner-compassion, love, tapas, pursuit, drive, 
///////////// Write down comments, code, among others, all in one folder, or several of folders (sorted, through the rehersal titles) - in order to have them organized. Ignore the issue of privacy, for now - It is a matter of truth, not avoidance. See through, oneself, awareness. Even, if it's just C#.

//////////// Enlist Possible concept art concepts, but also sketch the possible graphics, ideas, and implement initial rehersal; remember, Tapas.
//////////// Try to see through the common themes in programming, code, and integrate them holistically, like enneagram 5's most mature levels.
//////////// Diary, document everything.

/////////// Names: Combine Whole names (i.e. XCraft, GameRunMode, Game, Map, Tile, TileType, etc.) - with the long whole names (i.e. entitiesInTheVisibleMap, or something of that sort), along with short names (i.e. i, n, m, j, x, y, nX, nY, nZ, among others) - and treat them rehersaly. If there is a better way to conceptualize, or strategize the code (i.e. transforming the navigation variables, nX, nY, nZ, in the N class - then, apply changes, but do this in a way, that's integrating past labels, past names, so it may be something like nX1, nXzZZ, etc. But with a better names)
////////// Functions & data: For simplicity, and in sandbox mode, use all public functions and public variables.
////////// Unit Tests & Debug & Compile Debug: Create some sort of simple unit tests, that can be run through the command line, or something like that, or even gui system. Check if there is a possibility to create compilation debug (debug during compilation process) [chatgpt]
///////// Memorize (Self-reherse) everything possible. And use packages of pursuit, but don't limit Tapas in any way. Do it, through the Tapas, not through the Mindfulness itself; or balance between mindfulness, DMN, or Flow states.
///////// Write down everything, every thought, every emotion, give it an outlet - self-reherse to the maximum. Try, to use as integrative; attributable, and transcending language, as possible. Don't write in negative self-awareness - this; transform, through the Tapas itself (emotions are repressed, and rehersal of negative emotions reinforces them.)
///////// Check the keywords and metadata in C# (using, namespace, explicit/implicit, abstract, interface, etc.)
///////// Reherse knowledge regarding C# programming (study as much as possible). Write down rehersaly, and use good, but very clear understanding formatting, with the depth feelings. Organize, and if there is a given topic regarding given syntax (or software engineering) of C# - Add, (Push_Back/Front) this given understanding, to the pre-existing folder (md file(s); ideally one md.)
/////////         - Compile the MD files, so I have them both in one place, and in organized manner, the same. Create some sort of rehersal system, either manual, or automatic, or semi, to save these md files, or organize them, or even format.
///////// Write down integrated, more holistic, or more feeling approach regarding given rehersal aspects. Don't delete, or remove comments, or code - place it, delibarety, in order to see, memorize, and feel, the code, previous, next, past, future, present, etc.
///////// Organize given modules, sections, and algorithms placeholder, but don't do this in isolatory, or compartmentalizing, or abstract (universal naming) way - Reherse as much as possible given aspect of the code.
///////// Don't use, if possible, the default documentation.
///////// Emoji Switching Script, if compiler?

//////// XCraft could be a good namespace name. Are there any better, given that XCraft name could be applied in other contexts?
//////// I think I shouldn't use "impl" namespace, like I did before, it gives additional programming time.

///////// Brave Move: Consider moving this project to open source, even, if it's going to be commercial. This could add a more approachable, towards programming, programmers, [github] git communities, and even career, if I'm going to apply for a job, anytime.

//////// Emotions, Feelings, Positive Feelings, Awe, Courage, Hope, Trust, Feeling, Happiness, Peace.
//////// Reherse negative thoughts, feel them. Completely, in everything, the drive the will, the peace.

/////// GIMP: Luna Luminence/Darkness Blend Mode {{{{{(Or Lightness I don't remember [are there two of these blend modes in GIMP}}}}}?]) is very good at making a very cartoon, yet realistic, yet smooth gradients. Possibly, additionally, I could check how this algorithm of blend mode in GIMP is written, and implement in C#, or other programming language.
/////// GIMP: See possible additional patterns, of art making, but also reherse as much as possible, and Tapas of course, yes.
/////// Luna Luminence/Darkness Blend Mode, can be definitely used to make very deliberate, smooth shaders, gradients, and various shapes, of graphics - They could be used literally in any graphics, textures. This makes, GIMP itself, a powerful possibility. If, I could find a way to additionaly alter the blend modes, so I could apply 2, 3, or even 4 blend modes, in a given combinations, possibly through the written algorithm in C# (I think it's the best for now) - I could really make enormous progress in graphic design. 
/////// Luna Luminescence/Darkness Blend Modes, used holistically (gradients, edges) can make definitely very smooth, and cartonish, realistic, shapes. Possibly, if there is a way to use both, in some different combinations - it could make it able to create realistic, very realistic textures. Applying additional textures, gradients, blend modes, could make very polished texture

////// 🤸🏿‍♀️ Emoji, C# 

/////////// Explore Tapas; Extensively - Focus,; you can possibly take it as the enneagram 8, 2, 4, 1, ->, 5->8, 9->3, former the most important. I do believe, personally, that I am enneagram 8 or 2, i used to be 5. I know the way, I do. Know, 
/////////// How?
/////////// -> 7 -> 5 {6}

/////////// Math

/////////// Emotional Dyads (several given emotions; conscious) -> effort [tapas {as } & all yamas & niyamas] -> Transformation of all emotional dyads. Enneagrams are emotional cathexed fears - values, 
///

/////////// 
















/////////// Quick Tasks to do:
///////////   - Reherse every possible source code space (this project C#)
///////////   - Create concept art for XCraft
///////////   - Create graphics (complete polished version)
///////////   - Create Audio (polished version)
///////////   - Implement prototype of XCraft
///////////   - Think through.
///////////   - Write down GIMP-related things - try to conceptualize the c# program to draw blended modes altogether (design either a program similar to GIMP, or a GIMP with the possibility to generate graphics through, for instance, lua scripts. Possibly other scripting languages, or written - like hybrid of GLSL/other shader languages.)

/////////// Concept Art (Graphics-Gameplay) : 
///////////   - Possibly for major gameplay (survival-sandbox-action):
///////////   -     Base, Teleporter (or some sort of station), Major Construction City (All shops, with modules; around 800-1200 px), Package Modules (Electical Boxes, and Rounded Electrical Cylinders), Electrical (all) items, Major map (4096-16900 something wide)
///           -     Create the same style buildings.
///           -     Create scenaiors (graphics) - Trees, Bushes, Leaves, Flowers, Clouds, Mountains, sun, moon, stars, universe-alike backgrounds (sky), sky, 
///           -     Create Starship, mothership, cyber-planets, space stations, 
///////////   - Writting (keyboard) - Incorporate writing with programming, music, etc. 


namespace XCraft {
    //////////// Game Run Mode enum, could be completed, contemplated upon.
    //////////// Object Pool - Class, giving exact details, in string, ints, bools, and outputing the given GameRunMode in formats?
    //////////// JSON Serialization on GameRunMode.
    public enum GameRunMode {
        SERVER_CLIENT = 0,
        CLIENT,
        OFFLINE
    };
    /////// Rename
    public class Game1 : Game {
        public A a;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public bool serverGame = false;
        public Game1(bool serverGame) {
            this.serverGame = serverGame;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (serverGame) {
                InitializeServerClient();
            } else {
                InitializeThisClient();
            }
            this.a = new A(this);
            this.a.serverGame = serverGame;

            this.a.n = new N(a);

            if (serverGame) {
                //G g = new G(a.m, a);
            }

            this.a.varShare = new VarShare();

            IsMouseVisible = true;

            IsFixedTimeStep = true;

            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 120.0);

        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadDefaultTextures();
            LoadDefaultFonts();
            LoadDefaultEffects();

            //a.gui = new GUI(this, a);
            SetWindowSize(a.windowWidth, a.windowHeight);

            
            if (serverGame) {
                a.m = new M(a.mW, a.mH, a);
                G g = new G(a.m, a);
                g.Generate();
            } else {
                a.m = new M(a.mW, a.mH, a);
                G g = new G(a.m, a);
                g.Generate();
            }

        }
        protected void LoadDefaultFonts() {
            a.Fon("DejaVuSans", Content.Load<SpriteFont>("DejaVuSans"));
        }
        protected void LoadDefaultEffects() {
            //a.Eff("MotionBlur", Content.Load<Effect>("MotionBlur"));
        }
        protected void LoadDefaultTextures() {
            Texture2D pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new Color[] { Color.White });

            a.Tex("pixel", pixelTexture);
            

            a.Tex("tp", Content.Load<Texture2D>("tp"));
            a.Tex("tpbt", Content.Load<Texture2D>("tpbt"));
            a.Tex("pl", Content.Load<Texture2D>("pl"));
            a.Tex("stel", Content.Load<Texture2D>("st_el"));
            a.Tex("guiuniv", Content.Load<Texture2D>("guiuniv"));

            // Memorize all graphics, add new ones, and implement them in the game
            a.Tex("dt1_b_ar25h", Content.Load<Texture2D>("devtextures1/b_ar25h.png"));
            a.Tex("dt1_box", Content.Load<Texture2D>("devtextures1/box.png"));
            a.Tex("dt1_g2", Content.Load<Texture2D>("devtextures1/g2.png"));
            a.Tex("dt1_leg1", Content.Load<Texture2D>("devtextures1/leg1.png"));
            a.Tex("dt1_majorbase_25h", Content.Load<Texture2D>("devtextures1/majorbase_25h.png"));
            a.Tex("dt1_majorghead", Content.Load<Texture2D>("devtextures1/majorghead.png"));
            a.Tex("dt1_topp", Content.Load<Texture2D>("devtextures1/topp.png"));
            a.Tex("dt1_tp_37h", Content.Load<Texture2D>("devtextures1/tp_37h.pn"));
            a.Tex("dt1_base_25h", Content.Load<Texture2D>("devtextures1/base_25h.png"));
            a.Tex("dt1_g1", Content.Load<Texture2D>("devtextures1/g1.png"));
            a.Tex("dt1_jetpack", Content.Load<Texture2D>("devtextures1/jetpack.png"));
            a.Tex("dt1_leg2", Content.Load<Texture2D>("devtextures1/leg2.png"));
            a.Tex("dt1_majorgbottom", Content.Load<Texture2D>("devtextures1/majorgbottom.png"));
            a.Tex("dt1_player", Content.Load<Texture2D>("devtextures1/player.png"));
            a.Tex("dt1_torso", Content.Load<Texture2D>("devtextures1/torso.png"));
            a.Tex("dt1_tp", Content.Load<Texture2D>("devtextures1/tp.pn"));

            a.Tex("button_play_hover", Content.Load<Texture2D>("button_play_hover"));
            a.Tex("button_play_clicked", Content.Load<Texture2D>("button_play_clicked"));
            a.Tex("button_settings_normal", Content.Load<Texture2D>("button_settings_normal"));
            a.Tex("button_settings_hover", Content.Load<Texture2D>("button_settings_hover"));
            a.Tex("button_settings_clicked", Content.Load<Texture2D>("button_settings_clicked"));
            a.Tex("button_credits_normal", Content.Load<Texture2D>("button_credits_normal"));
            a.Tex("button_credits_hover", Content.Load<Texture2D>("button_credits_hover"));
            a.Tex("button_credits_clicked", Content.Load<Texture2D>("button_credits_clicked"));
            a.Tex("button_exit_normal", Content.Load<Texture2D>("button_exit_normal"));
            a.Tex("button_exit_hover", Content.Load<Texture2D>("button_exit_hover"));
            a.Tex("button_exit_clicked", Content.Load<Texture2D>("button_exit_clicked"));
            a.Tex("button_singleplayer_normal", Content.Load<Texture2D>("button_singleplayer_normal"));
            a.Tex("button_singleplayer_hover", Content.Load<Texture2D>("button_singleplayer_hover"));
            a.Tex("button_singleplayer_clicked", Content.Load<Texture2D>("button_singleplayer_clicked"));
            a.Tex("button_multiplayer_normal", Content.Load<Texture2D>("button_multiplayer_normal"));
            a.Tex("button_multiplayer_hover", Content.Load<Texture2D>("button_multiplayer_hover"));
            a.Tex("button_multiplayer_clicked", Content.Load<Texture2D>("button_multiplayer_clicked"));
        
            a.tp = a.Tex("tp");
            
        }
        protected void SetWindowSize(int w, int h) {
            graphics.PreferredBackBufferWidth = w;
            graphics.PreferredBackBufferHeight = h;

            graphics.ApplyChanges();
        }
        protected void KeyboardMouseInput() {
            if (a.IsSpectator()) {
                bool shift = a.ShiftHold();

                if (a.LeftMoveKeyboardHold()) {
                    a.n.zXAcc -= 2.0f * (shift ? 2.5f : 1.0f);
                }
                if (a.RightMoveKeyboardHold()) {
                    a.n.zXAcc += 2.0f * (shift ? 2.5f : 1.0f);
                }
                if (a.UpMoveKeyboardHold()) {
                    a.n.zYAcc -= 2.0f * (shift ? 2.5f : 1.0f);
                }
                if (a.DownMoveKeyboardHold()) {
                    a.n.zYAcc += 2.0f * (shift ? 2.5f : 1.0f);
                }
                
                

            } else if (a.IsPlayer()) {

            }
        }
        ///////// What is GameTime?
        protected override void Update(GameTime gameTime) {
            //////////// What is GamePay, Keyboard, Mouse, etc.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            a.Tick();
            KeyboardMouseInput();
            a.n.Tick();

            Draw(gameTime);

            a.PostTick();

            base.Update(gameTime);
        }
        ////////// This function is default - it is implemented in Game by MonoGame. Therefore, Draw, and Update (or tick)
        ////////// should be analyze in forms of, what happens in the MonoEngine, after using GraphicsDevice.Clear, and base.Draw(), base.Update(), and, if there are any other MonoGame applicable API functions - how are they relating, to these two, and how, to make complete, integrative system of tick update.
        ///////// Additionaly: How to handle networking, and at what points, of, possibly (most imporatntly) Update.
        ///////// Networking: Object Pooling? Possibly faster, better
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //////////// Shaders
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            a.m.Render(spriteBatch);

            //////////// Analyze how to output, delibaterly to console.
            System.Console.WriteLine(""+a.n.RNavX() + "  " + a.n.RNavY());

            //////////// Shaders (2)
            spriteBatch.End();
            base.Draw(gameTime);
        }








        protected void InitializeServerClient() {

        }
        protected void InitializeThisClient() {

        }
    };
}

//////////// Import all graphics from gimp, start implementing procedurally generated terrain
//////////// Create simple navigation system.