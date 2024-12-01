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
    /*
    public class RadioboxGUIA : GUIA {
        public RadioboxGUIA(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {

        }
        public override void Activity() {
            base.Activity();

            RadioboxGUIE cparent = parent as RadioboxGUIE;
            if (cparent == null) {
                return;
            }

            StandardClickStateDetermineForClickable();

            if (ClickedOnArea()) {
                if (cparent.ticked) {
                    cparent.ticked = false;
                } else {
                    cparent.ticked = true;
                }
            }
        }
    };
    public class RadioboxGUIR : GUIR {
        public readonly Ri b_n = new Ri(0,0,0,0);
        public readonly Ri b_h = new Ri(0,0,0,0);
        public readonly Ri b_c = new Ri(0,0,0,0);
        public readonly Ri b_t = new Ri(0,0,0,0);

        public RadioboxGUIR(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            ButtonGUIE cparent = parent as ButtonGUIE;
            if (parent.isUniv && cparent != null) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}

                Microsoft.Xna.Framework.Vector2 textSize = cparent.font.MeasureString(cparent.text);

                int textX = (int)(cparent.rX() + b_n.w + 6);
                int textY = (int)(cparent.rY() + ((b_n.h - textSize.Y) / 2));
            
                spriteBatch.DrawString(cparent.font, cparent.text, new Microsoft.Xna.Framework.Vector2(textX, textY), Color.White);
            
                if (cparent.ticked) {
                    RenderSel(spriteBatch);
                }
            }   
        }
        
        protected void RenderN(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n, new Ri(rX, rY, b_n.w, b_n.h), Color.White);
        }
        protected void RenderH(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_h, new Ri(rX, rY, b_h.w, b_h.h), Color.White);
        }
        protected void RenderC(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c, new Ri(rX, rY, b_c.w, b_c.h), Color.White);
        }
        protected void RenderSel(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_t, new Ri(rX, rY, b_t.w, b_t.h), Color.White);
        }
    };
    public class RadioboxGUIE : GUIE {
        public SpriteFont font;
        public string text;
        public RadioboxGUIE(GUI gui, D d, A a, SpriteFont font, string text, int lX = 0, int lY = 0, int lW = -1, int lH = -1) : base(parent,gui,d,a) {
            this.font = font;
            this.text = text;
            guir = new RadioboxGUIR(this, gui, d, a);
            guia = new RadioboxGUIA(this, gui, d, a);   
            isUniv = true;
        }
        public bool ticked = false;
        public bool clicked = false;
        protected override void cWidth(int p = 37) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 37) {
            base.cHeight(p);
        }
        public bool IsTicked() {
            return ticked;
        }
    };
    */
}