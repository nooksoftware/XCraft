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

        public D(Game1 g) {
            this.g = g;
        }

    };       
    public class I {

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