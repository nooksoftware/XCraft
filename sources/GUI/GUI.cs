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
        public A a;

        //public GUIE main;

        public Texture2D guiUniv2DT;

        public string current_menu = "gamemenu";

        public Game1 g;

        public GUI(A a) {
            this.a = a;
        }
        protected void LoadDefaultGUI() {
            SpriteFont f = a.Fon("DejaVuSans");


        }
           
    };
}