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
        public Texture2D guiUniv2DT;

        


        public Game1 g;
        public A a;
        public D d;
        public Texture2D GUIUnivT {
            get {return guiUniv2DT;}
        }
        public void DrawGUIUnivTClickState(SpriteBatch s, Ri ri, Color c) {
            Rectangle s = new Rectangle(0,0,ri.w,ri.h);
            Rectangle o = new Rectangle(ri.x, ri.y, ri.w, ri.h);
            Rectangle de = new Rectangle(rX(), rY(), s.Width, s.Height);
            s.Draw(GUIUnivT, de, o, c);
        }
        public GUI(Game1 g, D d, A a) {
            this.d = d;
            this.g = g;
            this.a = a;
            guiUniv2DT = d.Tex("guiuniv");
            main = new GUIE(this, d, GUIT.GUIELEMENT);
        }

        public void Draw(SpriteBatch spriteBatch) {
            main.Draw(spriteBatch);
        }
    };
}