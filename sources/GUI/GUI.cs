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
    public class GUI {
        public GUIE main;
        public Game1 g;
        public A a;
        public D d;
        public GUI(Game1 g, D d, A a) {
            this.d = d;
            this.g = g;
            this.a = a;
            main = new GUIE(this, d, GUIT.GUIELEMENT);
        }

        public void Draw(SpriteBatch spriteBatch) {
            main.Draw(spriteBatch);
        }
    };
}