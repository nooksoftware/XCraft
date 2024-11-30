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

        public string current_menu = "editor";


        public Game1 g;
        public A a;
        public D d;
        public Texture2D GUIUnivT {
            get {return guiUniv2DT;}
        }
        public void DrawGUIUniversalTexture(
            SpriteBatch spriteBatch,
            GUIE guiElement,
            Ri origin_texture,
            Ri destination,
            Color c
        ) {
            Rectangle origin = new Rectangle(
                origin_texture.x,
                origin_texture.y,
                origin_texture.w,
                origin_texture.h
            );
            Rectangle dest = new Rectangle(
                destination.x,
                destination.y,
                destination.w,
                destination.h
            );
            spriteBatch.Draw(GUIUnivT, dest, origin, c);
        }

        public void DrawGUIUnivT(SpriteBatch sp, GUIE e, Ri o, Ri de, Color c, int mX = 0, int mY = 0) {
            Rectangle origin = new Rectangle(o.x, o.y, o.w, o.h);
            Rectangle dest = new Rectangle(mX+de.x+e.rX(), mY+de.y+e.rY(), de.w, de.h);
            
            sp.Draw(GUIUnivT, dest, origin, c);
        }
        public void DrawGUIUnivTClickState(SpriteBatch sp, GUIE e, Ri ri, Color c, int mX = 0, int mY = 0) {
            Rectangle s = new Rectangle(0,0,ri.w,ri.h);
            Rectangle o = new Rectangle(ri.x, ri.y, ri.w, ri.h);
            Rectangle de = new Rectangle(e.rX() + mX, e.rY() + mY, s.Width, s.Height);
            sp.Draw(GUIUnivT, de, o, c);
        }
        public GUI(Game1 g, D d, A a) {
            this.d = d;
            this.g = g;
            this.a = a;
            guiUniv2DT = d.Tex("guiuniv");
            main = new GUIE(this, d, a, GUIT.GUIELEMENT);

            LoadDefaultGUI();
        }
        protected void LoadDefaultGUI() {
            SpriteFont f = d.Fon("DejaVuSans");

            main.Add("editor", new GUIE(this, d, a, GUIT.GUIELEMENT));
            main.Add("mainmenu", new GUIE(this, d, a, GUIT.GUIELEMENT));
            main.Add("gamemenu", new GUIE(this, d, a, GUIT.GUIELEMENT));

            main.Add("editor/mappanel", new PanelGUIE(this, d, a, 20, 500, 500, 240));
            main.Add("editor/mappanel/generate", new ButtonGUIE(this, d, a, f, "Generate Map", 20, 20, 250, 35));
        }


        public void Draw(SpriteBatch spriteBatch) {
            if (main.Exists(current_menu)) {
                main.Get(current_menu).Draw(spriteBatch);
            };
        }
        public void Activity() {
            if (main.Exists(current_menu)) {
                main.Get(current_menu).Activity();
            
                if (main.Get2<ButtonGUIE>("editor/mappanel/generate").Clicked()) {
                    d.m.GenerateDefault();
                }
            }

            
        }
    };
}