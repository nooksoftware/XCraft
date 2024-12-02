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
    public class ProgressbarGUIA : GUIA {
        public ProgressbarGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

        int c = 0;
        public override void Activity() {
            ProgressbarGUIE cparent = parent as ProgressbarGUIE;
            if (c >= 0 && c < 4) {
                c++;
            } else if (c == 4) {
                c = 0;
                cparent.value += 1;
                if (cparent.value > cparent.max_value) {
                    cparent.value = 0;
                }
            }
        }
    };
    public class ProgressbarGUIR : GUIR {

        public readonly Ri b_grad = new Ri(102, 83, 75, 18);
        public readonly Ri b_grad2 = new Ri(102, 102, 75, 18);
        public readonly Ri b_n1 = new Ri(102, 121, 13, 22);
        public readonly Ri b_n2 = new Ri(116, 121, 1, 22);
        public readonly Ri b_n3 = new Ri(118, 121, 13, 22);

        public ProgressbarGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {
            
        }
        public override void Render(SpriteBatch spriteBatch) {
            ProgressbarGUIE cparent = parent as ProgressbarGUIE;
            if (parent.isUniv && cparent != null) {
                base.Render(spriteBatch);


                RenderBg(spriteBatch);
                RenderNormalGrad(spriteBatch);
                RenderActiveGrad(spriteBatch);
            }
        }
        public void RenderBg(SpriteBatch spriteBatch) {
            ProgressbarGUIE cparent = parent as ProgressbarGUIE;

            int rX = cparent.rX(); 
            int rY = cparent.rY();
            int rW = cparent.rW();
            int rH = cparent.rH();

            int detX = rX;
            int detY = rY;
            int detX2 = detX + b_n1.w;
            int detY2 = rY;
            
            int detW = b_n1.w;
            int detH = b_n1.h;
            int detW2 = rW - b_n1.w - b_n3.w;
            int detH2 = b_n2.h;
            int detW3 = b_n3.w;
            int detH3 = b_n3.h;

            int detX3 = detX2 + detW2;
            int detY3 = rY;
            
            //int detX3 = detX2 

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n1, new Ri(detX, detY, detW, detH), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n2, new Ri(detX2, detY2, detW2, detH2), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n3, new Ri(detX3, detY3, detW3, detH3), Color.White);
        }
        public void RenderNormalGrad(SpriteBatch spriteBatch) {
            ProgressbarGUIE cparent = parent as ProgressbarGUIE;

            int detX = cparent.rX() + 2;
            int detY = cparent.rY() + 2;

            int detW = System.Convert.ToInt32(cparent.midareaX * ((double)cparent.value / (double)cparent.max_value));
            int detH = cparent.midareaY;

            if (detW > cparent.midareaX) {
                detW = cparent.midareaX;
            } else if (detW < 0) {
                detW = 0;
            }
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_grad, new Ri(detX, detY, detW, detH), Color.White);
        }
        public void RenderActiveGrad(SpriteBatch spriteBatch) {
            ProgressbarGUIE cparent = parent as ProgressbarGUIE;

            int detX = cparent.rX() + 2;
            int detY = cparent.rY() + 2;

            int detW = System.Convert.ToInt32(cparent.midareaX * ((double)cparent.value / (double)cparent.max_value));
            int detH = cparent.midareaY;

            if (detW > cparent.midareaX) {
                detW = cparent.midareaX;
            } else if (detW < 0) {
                detW = 0;
            }
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_grad2, new Ri(detX, detY, detW, detH), Color.White);
        }

    };
    public class ProgressbarGUIE : GUIE {
        public int midareaX = 0;
        public int midareaY = 18;

        public int max_value = 100;
        public int value = 50;

        public ProgressbarGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1, int? max_value = null, int? value = null) 
            : base(gui, d, a, GUIT.PROGRESSBAR, lX, lY, lW, lH)
        {

            guir = new ProgressbarGUIR(this, gui, d, a);
            guia = new ProgressbarGUIA(this, gui, d, a);

            isUniv = true;

            this.max_value = (max_value ?? this.max_value);
            this.value = (value ?? this.value);

  
            midareaX = lW - 2 - 2;
        }

        protected override void cWidth(int p = 27) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 22) {
            base.cHeight(p);
        }
    };
}

//   - 102 83 75 18 
//   - 102 102 75 18 
//   - 102 121 13 22
//   - 116 121 1 22
//   - 118 121 13 22