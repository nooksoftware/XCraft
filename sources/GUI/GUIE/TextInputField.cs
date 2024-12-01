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
    public class TextInputFieldGUIA : GUIA {
        public TextInputFieldGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

        public override void Activity() {
            base.Activity();
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;
            if (cparent == null) {
                return;
            }
            StandardClickStateDetermineForClickable();
            if (ClickedOnArea()) {
                cparent.selected = !cparent.selected;
                cparent.clicked = true;
            } else {
                cparent.clicked = false;
            }

            if (cparent.selected) {
                TextInput();
            }
        }
        protected void TextInput() {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;
            if (cparent == null) {
                return;
            }
            if (a.OnePressedQ()) {cparent.text += 'q';}
            if (a.OnePressedE()) {cparent.text += 'e';}
            if (a.OnePressedR()) {cparent.text += 'r';}
            if (a.OnePressedT()) {cparent.text += 't';}
            if (a.OnePressedY()) {cparent.text += 'y';}
            if (a.OnePressedU()) {cparent.text += 'u';}
            if (a.OnePressedI()) {cparent.text += 'i';}
            if (a.OnePressedO()) {cparent.text += 'o';}
            if (a.OnePressedP()) {cparent.text += 'p';}
            if (a.OnePressedF()) {cparent.text += 'f';}
            if (a.OnePressedG()) {cparent.text += 'g';}
            if (a.OnePressedH()) {cparent.text += 'h';}
            if (a.OnePressedJ()) {cparent.text += 'j';}
            if (a.OnePressedK()) {cparent.text += 'k';}
            if (a.OnePressedL()) {cparent.text += 'l';}
            if (a.OnePressedZ()) {cparent.text += 'z';}
            if (a.OnePressedX()) {cparent.text += 'x';}
            if (a.OnePressedC()) {cparent.text += 'c';}
            if (a.OnePressedV()) {cparent.text += 'v';}
            if (a.OnePressedB()) {cparent.text += 'b';}
            if (a.OnePressedN()) {cparent.text += 'n';}
            if (a.OnePressedM()) {cparent.text += 'm';}
            if (a.OnePressed0()) {cparent.text += '0';}
            if (a.OnePressed1()) {cparent.text += '1';}
            if (a.OnePressed2()) {cparent.text += '2';}
            if (a.OnePressed3()) {cparent.text += '3';}
            if (a.OnePressed4()) {cparent.text += '4';}
            if (a.OnePressed5()) {cparent.text += '5';}
            if (a.OnePressed6()) {cparent.text += '6';}
            if (a.OnePressed7()) {cparent.text += '7';}
            if (a.OnePressed8()) {cparent.text += '8';}
            if (a.OnePressed9()) {cparent.text += '9';}
            if (a.OnePressedBackspace()) { cparent.text = cparent.text.Substring(0, cparent.text.Length - 1); }
        }
    }
    public class TextInputFieldGUIR : GUIR {
        public readonly Ri b_n1 = new Ri(0, 83, 15, 11);
        public readonly Ri b_n2 = new Ri(16, 83, 1, 11);
        public readonly Ri b_n3 = new Ri(18, 83, 15, 11);
        public readonly Ri b_n4 = new Ri(0, 95, 15, 1);
        public readonly Ri b_n5 = new Ri(18, 95, 15, 1);
        public readonly Ri b_n6 = new Ri(0, 97, 15, 11);
        public readonly Ri b_n7 = new Ri(16, 97, 1, 11);
        public readonly Ri b_n8 = new Ri(18, 97, 15, 11);
        public readonly Ri b_n9 = new Ri(18, 95, 1, 1);

        public readonly Ri b_h1 = new Ri(34+0, 83, 15, 11);
        public readonly Ri b_h2 = new Ri(34+16, 83, 1, 11);
        public readonly Ri b_h3 = new Ri(34+18, 83, 15, 11);
        public readonly Ri b_h4 = new Ri(34+0, 95, 15, 1);
        public readonly Ri b_h5 = new Ri(34+18, 95, 15, 1);
        public readonly Ri b_h6 = new Ri(34+0, 97, 15, 11);
        public readonly Ri b_h7 = new Ri(34+16, 97, 1, 11);
        public readonly Ri b_h8 = new Ri(34+18, 97, 15, 11);
        public readonly Ri b_h9 = new Ri(34+18, 95, 1, 1);

        public readonly Ri b_c1 = new Ri(68+0, 83, 15, 11);
        public readonly Ri b_c2 = new Ri(68+16, 83, 1, 11);
        public readonly Ri b_c3 = new Ri(68+18, 83, 15, 11);
        public readonly Ri b_c4 = new Ri(68+0, 95, 15, 1);
        public readonly Ri b_c5 = new Ri(68+18, 95, 15, 1);
        public readonly Ri b_c6 = new Ri(68+0, 97, 15, 11);
        public readonly Ri b_c7 = new Ri(68+16, 97, 1, 11);
        public readonly Ri b_c8 = new Ri(68+18, 97, 15, 11);
        public readonly Ri b_c9 = new Ri(68+18, 95, 1, 1);

        protected Ri n_dest1;
        protected Ri n_dest2;
        protected Ri n_dest3;
        protected Ri n_dest4;
        protected Ri n_dest5;
        protected Ri n_dest6;
        protected Ri n_dest7;
        protected Ri n_dest8;
        protected Ri n_dest9;
        
        protected Ri h_dest1;
        protected Ri h_dest2;
        protected Ri h_dest3;
        protected Ri h_dest4;
        protected Ri h_dest5;
        protected Ri h_dest6;
        protected Ri h_dest7;
        protected Ri h_dest8;
        protected Ri h_dest9;
        
        protected Ri c_dest1;
        protected Ri c_dest2;
        protected Ri c_dest3;
        protected Ri c_dest4;
        protected Ri c_dest5;
        protected Ri c_dest6;
        protected Ri c_dest7;
        protected Ri c_dest8;
        protected Ri c_dest9;

        protected V2i midsize;

        public TextInputFieldGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d,a ) {
            SetDestRects();
        }
        protected void SetDestRects() {
            int rW = parent.rW();
            int rH = parent.rH();
            midsize = new V2i(rW - 1 - b_n1.w - b_n3.w, rH - 1 - b_n1.h - b_n6.h);

            n_dest1 = new Ri(0, 0, b_n1.w, b_n1.h);
            n_dest2 = new Ri(b_n1.w, 0, midsize.x, b_n2.h);
            n_dest3 = new Ri(b_n1.w+midsize.x, 0, b_n3.w, b_n3.h);
            n_dest4 = new Ri(0, b_n1.h, b_n4.w, midsize.y);
            n_dest5 = new Ri(b_n4.w+midsize.x, b_n1.h, b_n5.w, midsize.y);
            n_dest6 = new Ri(0, b_n1.h+midsize.y, b_n6.w, b_n6.h);
            n_dest7 = new Ri(b_n6.w, b_n1.h+midsize.y, midsize.x, b_n7.h);
            n_dest8 = new Ri(b_n6.w+midsize.x, b_n1.h+midsize.y, b_n8.w, b_n8.h);
            n_dest9 = new Ri(b_n1.w, b_n1.h, midsize.x, midsize.y);

            h_dest1 = new Ri(0, 0, b_h1.w, b_h1.h);
            h_dest2 = new Ri(b_h1.w, 0, midsize.x, b_h2.h);
            h_dest3 = new Ri(b_h1.w+midsize.x, 0, b_h3.w, b_h3.h);
            h_dest4 = new Ri(0, b_h1.h, b_h4.w, midsize.y);
            h_dest5 = new Ri(b_h4.w+midsize.x, b_h1.h, b_h5.w, midsize.y);
            h_dest6 = new Ri(0, b_h1.h+midsize.y, b_h6.w, b_h6.h);
            h_dest7 = new Ri(b_h6.w, b_h1.h+midsize.y, midsize.x, b_h7.h);
            h_dest8 = new Ri(b_h6.w+midsize.x, b_h1.h+midsize.y, b_h8.w, b_h8.h);
            h_dest9 = new Ri(b_h1.w, b_h1.h, midsize.x, midsize.y);

            c_dest1 = new Ri(0, 0, b_c1.w, b_c1.h);
            c_dest2 = new Ri(b_c1.w, 0, midsize.x, b_c2.h);
            c_dest3 = new Ri(b_c1.w+midsize.x, 0, b_c3.w, b_c3.h);
            c_dest4 = new Ri(0, b_c1.h, b_c4.w, midsize.y);
            c_dest5 = new Ri(b_c4.w+midsize.x, b_c1.h, b_c5.w, midsize.y);
            c_dest6 = new Ri(0, b_c1.h+midsize.y, b_c6.w, b_c6.h);
            c_dest7 = new Ri(b_c6.w, b_c1.h+midsize.y, midsize.x, b_c7.h);
            c_dest8 = new Ri(b_c6.w+midsize.x, b_c1.h+midsize.y, b_c8.w, b_c8.h);
            c_dest9 = new Ri(b_c1.w, b_c1.h, midsize.x, midsize.y);
        }
        public override void Render(SpriteBatch spriteBatch) {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;

            if (parent.isUniv && cparent != null) {
                if(clickState == 0 && cparent.selected != true) {RenderN(spriteBatch);}
                else if (clickState == 1 || cparent.selected == true) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}
                RenderText(spriteBatch);
            }

            //if (parent.isUniv) {
            //    base.Render(spriteBatch);
            //}
        }
        public void RenderN(SpriteBatch spriteBatch) {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;

            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            //b_n1 n_dest1
            //b_n2 n_dest2
            //b_n3 n_dest3
            //b_n4 n_dest4
            //b_n5 n_dest5
            //b_n6 n_dest6
            //b_n7 n_dest7
            //b_n8 n_dest8
            //b_n9 n_dest9

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n1,
                new Ri(n_dest1.x+rX, n_dest1.y+rY, n_dest1.w, n_dest1.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n2,
                new Ri(n_dest2.x+rX, n_dest2.y+rY, n_dest2.w, n_dest2.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n3,
                new Ri(n_dest3.x+rX, n_dest3.y+rY, n_dest3.w, n_dest3.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n4,
                new Ri(n_dest4.x+rX, n_dest4.y+rY, n_dest4.w, n_dest4.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n5,
                new Ri(n_dest5.x+rX, n_dest5.y+rY, n_dest5.w, n_dest5.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n6,
                new Ri(n_dest6.x+rX, n_dest6.y+rY, n_dest6.w, n_dest6.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n7,
                new Ri(n_dest7.x+rX, n_dest7.y+rY, n_dest7.w, n_dest7.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n8,
                new Ri(n_dest8.x+rX, n_dest8.y+rY, n_dest8.w, n_dest8.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_n9,
                new Ri(n_dest9.x+rX, n_dest9.y+rY, n_dest9.w, n_dest9.h),
                Color.White
            );
        }
        public void RenderH(SpriteBatch spriteBatch) {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;

            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            //b_h1 h_dest1
            //b_h2 h_dest2
            //b_h3 h_dest3
            //b_h4 h_dest4
            //b_h5 h_dest5
            //b_h6 h_dest6
            //b_h7 h_dest7
            //b_h8 h_dest8
            //b_h9 h_dest9

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h1,
                new Ri(h_dest1.x+rX, h_dest1.y+rY, h_dest1.w, h_dest1.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h2,
                new Ri(h_dest2.x+rX, h_dest2.y+rY, h_dest2.w, h_dest2.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h3,
                new Ri(h_dest3.x+rX, h_dest3.y+rY, h_dest3.w, h_dest3.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h4,
                new Ri(h_dest4.x+rX, h_dest4.y+rY, h_dest4.w, h_dest4.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h5,
                new Ri(h_dest5.x+rX, h_dest5.y+rY, h_dest5.w, h_dest5.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h6,
                new Ri(h_dest6.x+rX, h_dest6.y+rY, h_dest6.w, h_dest6.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h7,
                new Ri(h_dest7.x+rX, h_dest7.y+rY, h_dest7.w, h_dest7.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h8,
                new Ri(h_dest8.x+rX, h_dest8.y+rY, h_dest8.w, h_dest8.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_h9,
                new Ri(h_dest9.x+rX, h_dest9.y+rY, h_dest9.w, h_dest9.h),
                Color.White
            );
        }
        public void RenderC(SpriteBatch spriteBatch) {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;

            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            //b_c1 c_dest1
            //b_c2 c_dest2
            //b_c3 c_dest3
            //b_c4 c_dest4
            //b_c5 c_dest5
            //b_c6 c_dest6
            //b_c7 c_dest7
            //b_c8 c_dest8
            //b_c9 c_dest9

            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c1,
                new Ri(c_dest1.x+rX, c_dest1.y+rY, c_dest1.w, c_dest1.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c2,
                new Ri(c_dest2.x+rX, c_dest2.y+rY, c_dest2.w, c_dest2.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c3,
                new Ri(c_dest3.x+rX, c_dest3.y+rY, c_dest3.w, c_dest3.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c4,
                new Ri(c_dest4.x+rX, c_dest4.y+rY, c_dest4.w, c_dest4.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c5,
                new Ri(c_dest5.x+rX, c_dest5.y+rY, c_dest5.w, c_dest5.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c6,
                new Ri(c_dest6.x+rX, c_dest6.y+rY, c_dest6.w, c_dest6.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c7,
                new Ri(c_dest7.x+rX, c_dest7.y+rY, c_dest7.w, c_dest7.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c8,
                new Ri(c_dest8.x+rX, c_dest8.y+rY, c_dest8.w, c_dest8.h),
                Color.White
            );
            gui.DrawGUIUniversalTexture(spriteBatch, cparent,
                b_c9,
                new Ri(c_dest9.x+rX, c_dest9.y+rY, c_dest9.w, c_dest9.h),
                Color.White
            );
        }
        public void RenderText(SpriteBatch spriteBatch) {
            TextInputFieldGUIE cparent = parent as TextInputFieldGUIE;

            Microsoft.Xna.Framework.Vector2 textSize = cparent.font.MeasureString(cparent.text);

            int textX = (int)(cparent.rX() + ((cparent.rW() - textSize.X) / 2));
            int textY = (int)(cparent.rY() + ((cparent.rH() - textSize.Y) / 2));


            spriteBatch.DrawString(cparent.font, cparent.text, new Microsoft.Xna.Framework.Vector2(textX, textY), Color.White);
        }
    }
    public class TextInputFieldGUIE : GUIE {
        public string text = "";
        public SpriteFont font;
        public bool selected = false;
        public bool clicked = false;
        public bool numericOnly = false;

        public TextInputFieldGUIE(GUI gui, D d, A a, SpriteFont font, string def_text = "", int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.TEXT_INPUT_FIELD, lX, lY, lW, lH) {
            this.text = def_text;
            this.font = font;
            guir = new TextInputFieldGUIR(this, gui, d, a);
            guia = new TextInputFieldGUIA(this, gui, d, a);
            isUniv = true;
        }
        protected override void cWidth(int p = 31) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 23) {
            base.cHeight(p);
        }
    };
}
































    /*
        Textinputfield

        Topleft, Top, Topright, Left, Right, BottomLeft, Bottom, BottomRight
         {Normal, Hover, Click}

        0, 83, 15, 11
        17, 83, 1, 11,
        19, 83, 15, 11
        0, 95, 15, 1
        19, 95, 15, 1
        0, 97, 15, 11
        17, 97, 1, 11
        19, 97, 15, 11

        0, 83, 34+15, 11
        17, 83, 134+, 11,
        19, 83, 34+15, 11
        0, 95, 34+15, 1
        19, 95, 34+15, 1
        0, 97, 34+15, 11
        17, 97, 34+1, 11
        19, 97, 34+15, 11

        0, 83, 68+15, 11
        17, 83, 168+, 11,
        19, 83, 68+15, 11
        0, 95, 68+15, 1
        19, 95, 68+15, 1
        0, 97, 68+15, 11
        17, 97, 68+1, 11
        19, 97, 68+15, 11
    */