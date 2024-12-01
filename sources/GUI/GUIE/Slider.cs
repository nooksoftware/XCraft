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
    public class SliderGUIA : GUIA {
        public int xMousePosClickBegin = -1;
        public SliderGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Activity() {

            SliderGUIE cparent = parent as SliderGUIE;

            bool holdFrame = a.LMBHold();
            bool clickFrame = a.OneClickedLMB();
            bool releaseFrame = a.OneReleasedLMB();


            if (cparent != null) {

                int rX = parent.rX();
                int rY = parent.rY();

                int areaX = rX+cparent.offX;
                int areaY = rY;
                int areaW = 43;
                int areaH = 22;

                StandardClickStateDetermineForClickable(
                    areaX,
                    areaY,
                    areaW,
                    areaH
                );

                if (clickFrame && MouseOnArea(areaX, areaY, areaW, areaH)) {
                    xMousePosClickBegin = d.ms.X;
                    cparent.holding = true;

                } else if (holdFrame && cparent.holding) {
                    int delta = d.ms.X - xMousePosClickBegin;

                    cparent.offXR = cparent.offX + delta;
                    if (cparent.offXR < 0) {
                        cparent.offXR = 0;
                    } else if (cparent.offXR > cparent.maxOffX) {
                        cparent.offXR = cparent.maxOffX;
                    }
                } else if (releaseFrame && cparent.holding == true) {
                    xMousePosClickBegin = -1;
                    cparent.holding = false;
                    cparent.offX = cparent.offXR;

                    if (cparent.offX < 0) {
                        cparent.offX = 0;
                    } else if (cparent.offX > cparent.maxOffX) {
                        cparent.offX = cparent.maxOffX;
                    }
                }
                
            }   
        }
    }
    public class SliderGUIR : GUIR {
        public readonly Ri b_sl1 = new Ri(0, 36, 21, 22);
        public readonly Ri b_sl2 = new Ri(22, 36, 1, 22);
        public readonly Ri b_sl3 = new Ri(24, 36, 21, 22);

        public readonly Ri b_bn1 = new Ri(0, 60, 13, 22);
        public readonly Ri b_bn2 = new Ri(14, 60, 1, 22);
        public readonly Ri b_bn3 = new Ri(16, 60, 13, 22);
        
        public readonly Ri b_bh1 = new Ri(29, 60, 13, 22);
        public readonly Ri b_bh2 = new Ri(43, 60, 1, 22);
        public readonly Ri b_bh3 = new Ri(59, 60, 13, 22);
        
        public readonly Ri b_bc1 = new Ri(88, 60, 13, 22);
        public readonly Ri b_bc2 = new Ri(102, 60, 1, 22);
        public readonly Ri b_bc3 = new Ri(118, 60, 13, 22);

        public SliderGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Render(SpriteBatch spriteBatch) {
            SliderGUIE cparent = parent as SliderGUIE;
            
            if (parent.isUniv && cparent != null) {
                base.Render(spriteBatch);

                RenderSl(spriteBatch);
                if (clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}


            }
        }
        public void RenderSl(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            int mWAr = b_sl2.w*(rW-b_sl1.w-b_sl3.w);

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_sl1, new Ri(rX, rY, b_sl1.w, b_sl1.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_sl2, new Ri(rX+b_sl1.w, rY, mWAr, b_sl2.h), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_sl3, new Ri(rX+b_sl1.w+mWAr, rY, b_sl3.w, b_sl3.h), Color.White);
        }
        public void RenderN(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();
            SliderGUIE cparent = parent as SliderGUIE;
            int offX = cparent.offX;

            int detX = rX+offX; 
            if (cparent.holding) {
                detX = rX+cparent.offXR;
            }
            int detY = rY; 
            int detX2 = detX + b_bn1.w;
            int detY2 = detY;
            int detX3 = detX2 + b_bn2.w;
            int detY3 = detY;

            int detW = b_bn1.w; 
            int detH = b_bn1.h;
            int detW2 = b_bn2.w;
            int detH2 = b_bn2.h;
            int detW3 = b_bn3.w;
            int detH3 = b_bn3.h;

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bn1, new Ri(detX, detY, detW, detH), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bn2, new Ri(detX2, detY2, detW2, detH2), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bn3, new Ri(detX3, detY3, detW3, detH3), Color.White);
        }
        public void RenderH(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();
            SliderGUIE cparent = parent as SliderGUIE;
            int offX = cparent.offX;

            int detX = rX+offX; 
            if (cparent.holding) {
                detX = rX+cparent.offXR;
            }
            int detY = rY; 
            int detX2 = detX + b_bh1.w;
            int detY2 = detY;
            int detX3 = detX2 + b_bh2.w;
            int detY3 = detY;

            int detW = b_bh1.w; 
            int detH = b_bh1.h;
            int detW2 = b_bh2.w;
            int detH2 = b_bh2.h;
            int detW3 = b_bh3.w;
            int detH3 = b_bh3.h;

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bh1, new Ri(detX, detY, detW, detH), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bh2, new Ri(detX2, detY2, detW2, detH2), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bh3, new Ri(detX3, detY3, detW3, detH3), Color.White);
        }
        public void RenderC(SpriteBatch spriteBatch) {
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();
            SliderGUIE cparent = parent as SliderGUIE;
            int offX = cparent.offX;

            int detX = rX+offX; 
            if (cparent.holding) {
                detX = rX+cparent.offXR;
            }
            int detY = rY; 
            int detX2 = detX + b_bc1.w;
            int detY2 = detY;
            int detX3 = detX2 + b_bc2.w;
            int detY3 = detY;
            int detW = b_bc1.w; 
            int detH = b_bc1.h;
            int detW2 = b_bc2.w;
            int detH2 = b_bc2.h;
            int detW3 = b_bc3.w;
            int detH3 = b_bc3.h;

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bc1, new Ri(detX, detY, detW, detH), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bc2, new Ri(detX2, detY2, detW2, detH2), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bc3, new Ri(detX3, detY3, detW3, detH3), Color.White);
        }
        
    }
    public class SliderGUIE : GUIE {
        public bool holding = false;
        public int offX = 0;
        public int offXR = 0;
        public int maxOffX = 0;


        public SliderGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.SLIDER, lX, lY, lW, lH) {
            guir = new SliderGUIR(this, gui, d, a);
            guia = new SliderGUIA(this, gui, d, a);
            isUniv = true;
            cHeight(22);
            SliderGUIR cguir = guir as SliderGUIR;
            maxOffX = lW - (cguir.b_bn1.w + cguir.b_bn3.w);
        }
        protected override void cWidth(int p = 43) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 22) {
            base.cHeight(p);
        }
    };
}

    /*
        slideline
        0, 36, 21, 22
        22, 36, 1, 22
        24, 36, 21, 22

        Button
        0, 60, 13, 22
        14, 60, 1, 22
        16, 60, 13, 22
    */
























