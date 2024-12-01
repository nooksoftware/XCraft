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
    public class TextGUIA : GUIA {
        public TextGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

    }
    public class TextGUIR : GUIR {
        public TextGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d,a ) {}
        public override void Render(SpriteBatch spriteBatch) {
            TextGUIE cparent = parent as TextGUIE;
            if (cparent.font != null && cparent != null) {
                spriteBatch.DrawString(cparent.font, cparent.text, new Microsoft.Xna.Framework.Vector2(cparent.rX(), cparent.rY()), Color.White);
            }
            //if (parent.isUniv) {
            //    base.Render(spriteBatch);
            //}
        }
    }
    public class TextGUIE : GUIE {
        public SpriteFont font;
        public string text;

        public TextGUIE(GUI gui, D d, A a, SpriteFont font, string text, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.TEXT_INPUT_FIELD, lX, lY, lW, lH) {
            guir = new TextGUIR(this, gui, d, a);
            guia = new TextGUIA(this, gui, d, a);
            this.font = font;
            this.text = text;
            isUniv = false;
        }
        protected override void cWidth(int p = 0) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 0) {
            base.cHeight(p);
        }
    };
    
}