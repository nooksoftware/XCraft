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

    public class ButtonGUIA : GUIA {
        public ButtonGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Activity() {
            base.Activity();

            ButtonGUIE cparent = parent as ButtonGUIE;
            if (cparent == null) {
                return;
            }
            //StandardClickStateDetermineForClickable();
            StandardClickStateDetermineForClickable();
            //if (ClickedOnArea()) {
            if (ClickedOnArea()) {
                cparent.clicked = true;
            } else {
                cparent.clicked = false;
            }
        }
    };
    public class ButtonGUIR : GUIR {
        public readonly Ri b_n1 = new Ri(0, 0, 13, 35);
        public readonly Ri b_n2 = new Ri(14, 0, 1, 35);
        public readonly Ri b_n3 = new Ri(16, 0, 13, 35);

        public readonly Ri b_h1 = new Ri(29, 0, 13, 35);
        public readonly Ri b_h2 = new Ri(43, 0, 1, 35);
        public readonly Ri b_h3 = new Ri(45, 0, 13, 35);

        public readonly Ri b_c1 = new Ri(58, 0, 13, 35);
        public readonly Ri b_c2 = new Ri(72, 0, 1, 35);
        public readonly Ri b_c3 = new Ri(74, 0, 13, 35);

        public ButtonGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Render(SpriteBatch spriteBatch) {
            ButtonGUIE cparent = parent as ButtonGUIE;
            if (parent.isUniv && cparent != null) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}

                Microsoft.Xna.Framework.Vector2 textSize = cparent.font.MeasureString(cparent.text);

                int textX = (int)(cparent.rX() + ((cparent.rW() - textSize.X) / 2));
                int textY = (int)(cparent.rY() + ((cparent.rH() - textSize.Y) / 2));

                spriteBatch.DrawString(cparent.font, cparent.text, new Microsoft.Xna.Framework.Vector2(textX, textY), Color.White);
                //base.Render(spriteBatch);
            }
        }
        public void RenderN(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n1, new Ri(rX, rY, b_n1.w, b_n1.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n2, new Ri(rX+b_n1.w, rY, b_n2.w*rW-b_n1.w-b_n3.w, b_n2.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n3, new Ri(rX+b_n1.w+b_n2.w*rW-b_n1.w-b_n3.w, rY, b_n3.w, b_n3.h), Color.White);
        }

        public void RenderH(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_h1, new Ri(rX, rY, b_h1.w, b_h1.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_h2, new Ri(rX+b_h1.w, rY, b_h2.w*rW-b_h1.w-b_h3.w, b_h2.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_h3, new Ri(rX+b_h1.w+b_h2.w*rW-b_h1.w-b_h3.w, rY, b_h3.w, b_h3.h), Color.White);
        }

        public void RenderC(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c1, new Ri(rX, rY, b_c1.w, b_c1.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c2, new Ri(rX+b_c1.w, rY, b_c2.w*rW-b_c1.w-b_c3.w, b_c2.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c3, new Ri(rX+b_c1.w+b_c2.w*rW-b_c1.w-b_c3.w, rY, b_c3.w, b_c3.h), Color.White);
        }

    }
    public class ButtonGUIE : GUIE {
        public SpriteFont font;
        public string text;

        public ButtonGUIE(GUI gui, D d, A a, SpriteFont font, string text, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.BUTTON, lX, lY, lW, lH) {
            this.font = font;
            this.text = text;
            guir = new ButtonGUIR(this, gui, d, a);
            guia = new ButtonGUIA(this, gui, d, a);
            isUniv = true;
        }
        public bool clicked = false;
        protected override void cWidth(int p = 27) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 35) {
            base.cHeight(p);
        }
        public bool Clicked() {
            return clicked;
        }
    };
}















/*
    0, 0, 13, 35
    14, 0, 13, 35
    28, 0, 13, 35
*/