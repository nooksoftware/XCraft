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

using MVector2 = Microsoft.Xna.Framework.Vector2;
namespace XCraft {
    public class NumberFieldGUIA : GUIA {
        public NumberFieldGUIA(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {}
        public override void Activity() {
            base.Activity();

            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            NumberFieldGUIR cguir = parent.guir as NumberFieldGUIR;

            if (cparent == null) {
                return;
            }
            int rX = cparent.rX();
            int rY = cparent.rY();
            ProvidedClickStateDetermineForClickable(ref cparent.clickStateLButton, 
                cparent.rb_LButton.x,
                cparent.rb_LButton.y,
                cparent.rb_LButton.w,
                cparent.rb_LButton.h
            );
            ProvidedClickStateDetermineForClickable(ref cparent.clickStateRButton, 
                cparent.rb_RButton.x,
                cparent.rb_RButton.y,
                cparent.rb_RButton.w,
                cparent.rb_RButton.h
            );
            ProvidedClickStateDetermineForClickable(ref cparent.clickStateMidNumbArea, 
                cparent.rb_MidNumbArea.x,
                cparent.rb_MidNumbArea.y,
                cparent.rb_MidNumbArea.w,
                cparent.rb_MidNumbArea.h
            );

            if (ClickedOnArea(
                cparent.rb_LButton.x,
                cparent.rb_LButton.y,
                cparent.rb_LButton.w,
                cparent.rb_LButton.h
            )) {
                cparent.LButtonClick();                                
            } else if (ClickedOnArea(
                cparent.rb_RButton.x,
                cparent.rb_RButton.y,
                cparent.rb_RButton.w,
                cparent.rb_RButton.h
            )) {
                cparent.RButtonClick();                            
            } else if (ClickedOnArea(
                cparent.rb_MidNumbArea.x,
                cparent.rb_MidNumbArea.y,
                cparent.rb_MidNumbArea.w,
                cparent.rb_MidNumbArea.h
            )) {
                cparent.MidButtonClick();                            
            }
        }
    };
    public class NumberFieldGUIR : GUIR {
        public readonly Ri b_lbn = new Ri(102, 165, 20, 20);
        public readonly Ri b_lbh = new Ri(123, 165, 20, 20);
        public readonly Ri b_lbc = new Ri(144, 165, 20, 20);

        public readonly Ri b_rbn = new Ri(102, 186, 20, 20);
        public readonly Ri b_rbh = new Ri(123, 186, 20, 20);
        public readonly Ri b_rbc = new Ri(144, 186, 20, 20);

        public readonly Ri b_midn1 = new Ri(102, 144, 10, 20);
        public readonly Ri b_midn2 = new Ri(113, 144, 1, 20);
        public readonly Ri b_midn3 = new Ri(115, 144, 10, 20);

        public readonly Ri b_midh1 = new Ri(126, 144, 10, 20);
        public readonly Ri b_midh2 = new Ri(137, 144, 1, 20);
        public readonly Ri b_midh3 = new Ri(139, 144, 10, 20);

        public readonly Ri b_midc1 = new Ri(150, 144, 10, 20);
        public readonly Ri b_midc2 = new Ri(161, 144, 1, 20);
        public readonly Ri b_midc3 = new Ri(163, 144, 10, 20);

        public NumberFieldGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;

            if (parent.isUniv && cparent != null) {
                if(cparent.clickStateLButton == 0) {RenderLeftBN(spriteBatch);}
                else if (cparent.clickStateLButton == 1) {RenderLeftBH(spriteBatch);}
                else if (cparent.clickStateLButton == 2) {RenderLeftBC(spriteBatch);}

                if(cparent.clickStateRButton == 0) {RenderRightBN(spriteBatch);}
                else if (cparent.clickStateRButton == 1) {RenderRightBH(spriteBatch);}
                else if (cparent.clickStateRButton == 2) {RenderRightBC(spriteBatch);}

                if(cparent.clickStateMidNumbArea == 0) {RenderMidN(spriteBatch);}
                else if (cparent.clickStateMidNumbArea == 1) {RenderMidH(spriteBatch);}
                else if (cparent.clickStateMidNumbArea == 2) {RenderMidC(spriteBatch);}

                RenderNumber(spriteBatch);
            }
        }
        public void RenderLeftBN(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX;
            int detY = rY;
            int detW = b_lbn.w;
            int detH = b_lbn.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_lbn, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderLeftBH(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX;
            int detY = rY;
            int detW = b_lbh.w;
            int detH = b_lbh.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_lbh, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderLeftBC(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX;
            int detY = rY;
            int detW = b_lbc.w;
            int detH = b_lbc.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_lbc, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderRightBN(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX+b_lbn.w + (rW - b_lbn.w - b_rbn.w);
            int detY = rY;
            int detW = b_rbn.w;
            int detH = b_rbn.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_rbn, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderRightBH(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();


            int detX = rX+b_lbh.w + (rW - b_lbh.w - b_rbh.w);
            int detY = rY;
            int detW = b_rbh.w;
            int detH = b_rbh.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_rbh, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderRightBC(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();


            int detX = rX+b_lbc.w + (rW - b_lbc.w - b_rbc.w);
            int detY = rY;
            int detW = b_rbc.w;
            int detH = b_rbc.h;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_rbc, new Ri(detX, detY, detW, detH), Color.White
            );
        }
        public void RenderMidN(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX + b_lbn.w;
            int detY = rY;
            int detW = b_midn1.w;
            int detH = b_midn1.h;
            int detW2 = rW - b_lbn.w - b_rbn.w - b_midn1.w - b_midn3.w;
            int detH2 = b_midn2.h;
            int detW3 = b_midn3.w;
            int detH3 = b_midn3.h;
            int detX2 = detX + b_midn1.w;
            int detY2 = rY;
            int detX3 = detX2 + detW2;
            int detY3 = rY;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midn1, new Ri(detX, detY, detW, detH), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midn2, new Ri(detX2, detY2, detW2, detH2), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midn3, new Ri(detX3, detY3, detW3, detH3), Color.White
            );
        }
        public void RenderMidH(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX + b_lbn.w;
            int detY = rY;
            int detW = b_midh1.w;
            int detH = b_midh1.h;
            int detW2 = rW - b_lbn.w - b_rbn.w - b_midh1.w - b_midh3.w;
            int detH2 = b_midh2.h;
            int detW3 = b_midh3.w;
            int detH3 = b_midh3.h;
            int detX2 = detX + b_midh1.w;
            int detY2 = rY;
            int detX3 = detX2 + detW2;
            int detY3 = rY;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midh1, new Ri(detX, detY, detW, detH), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midh2, new Ri(detX2, detY2, detW2, detH2), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midh3, new Ri(detX3, detY3, detW3, detH3), Color.White
            );
        }
        public void RenderMidC(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int detX = rX + b_lbn.w;
            int detY = rY;
            int detW = b_midc1.w;
            int detH = b_midc1.h;
            int detW2 = rW - b_lbn.w - b_rbn.w - b_midc1.w - b_midc3.w;
            int detH2 = b_midc2.h;
            int detW3 = b_midc3.w;
            int detH3 = b_midc3.h;
            int detX2 = detX + b_midc1.w;
            int detY2 = rY;
            int detX3 = detX2 + detW2;
            int detY3 = rY;

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midc1, new Ri(detX, detY, detW, detH), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midc2, new Ri(detX2, detY2, detW2, detH2), Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_midc3, new Ri(detX3, detY3, detW3, detH3), Color.White
            );
        }
        public void RenderNumber(SpriteBatch spriteBatch) {
            NumberFieldGUIE cparent = parent as NumberFieldGUIE;
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int numberTextAreaW = rW - b_lbn.w - b_rbn.w - 2 - 2;
            int numberTextAreaH = 20;
            Microsoft.Xna.Framework.Vector2 textSize = cparent.f.MeasureString((cparent.number.ToString()));

            int textX = 
                (int)(rX + ((numberTextAreaW - textSize.X)/2));
            int textY =
                (int)(rY + ((numberTextAreaW - textSize.Y)/2));

            spriteBatch.DrawString(cparent.f, (cparent.number.ToString()), new MVector2(textX, textY), cparent.numberColor);
        }

    }
    public class NumberFieldGUIE : GUIE {
        public int clickStateLButton = 0;
        public int clickStateRButton = 0;
        public int clickStateMidNumbArea = 0;

        public int number = 0;
        public int max_number = 0;
        public int min_number = 0;

        public Ri rb_LButton = null;
        public Ri rb_RButton = null;
        public Ri rb_MidNumbArea = null;

        public SpriteFont f;
        public Color numberColor;

        public NumberFieldGUIE(GUI gui, D d, A a, SpriteFont font, int number, int min_number = 0, int max_number = 255, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
         : base(gui, d, a, GUIT.NUMBERFIELD, lX, lY, lW, lH)
        {
            this.number = number;
            this.f = font;
            this.min_number = min_number;
            this.max_number = max_number;
            guir = new NumberFieldGUIR(this, gui, d, a);
            guia = new NumberFieldGUIA(this, gui, d, a);
            isUniv = true;
            numberColor = Color.White;

            cHeight();

            rBounds();

        }
        protected void rBounds() {
            
            NumberFieldGUIR cguir = guir as NumberFieldGUIR;

            if (cguir != null) {
                int _rX = rX();
                int _rY = rY();
                int _rW = rW();
                //int _rH = rH();

                int det2W = (_rW - cguir.b_lbn.w - cguir.b_rbn.w);
                rb_LButton = new Ri(_rX, _rY, cguir.b_lbn.w, cguir.b_lbn.h);
                rb_RButton = new Ri(_rX + cguir.b_lbn.w + det2W, _rY, cguir.b_rbn.w, cguir.b_rbn.h);
                rb_MidNumbArea = new Ri(_rX + cguir.b_lbn.w, _rX, det2W, cguir.b_midn2.h);
            }
            
        }
        protected override void cWidth(int p = 90) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 20) {
            base.cHeight(p);
        }
        public void LButtonClick() {
            number -= 1;
            if (number < min_number) {
                number = min_number;
            }
        }
        public void RButtonClick() {
            number += 1;
            if (number > max_number) {
                number = max_number;
            }
        }
        public void MidButtonClick() {
            //todo
        }
    };
}