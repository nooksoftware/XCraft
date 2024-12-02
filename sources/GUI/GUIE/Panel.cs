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
    public class PanelGUIA : GUIA {
        public PanelGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

        public override void Activity() {
            PanelGUIE cparent = parent as PanelGUIE;
            if (cparent.navigable && cparent.navArea != null && cparent.navSlider != null) {
                ActivitySlider();
            }
        }
        public void ActivitySlider() {
            PanelGUIE cparent = parent as PanelGUIE;
            cparent.navSlider.Activity();

            double scrollRatioY = cparent.navSlider.ValueF();

            cparent.navArea.y = (int)((cparent.navArea.h - cparent.rH()) * scrollRatioY);
        }

    }
    public class PanelGUIR : GUIR {
        public readonly Ri b_n1 = new Ri(90, 0, 3, 3);
        public readonly Ri b_n2 = new Ri(94, 0, 1, 3);
        public readonly Ri b_n3 = new Ri(96, 0, 3, 3);
        public readonly Ri b_n4 = new Ri(90, 4, 3, 1);
        public readonly Ri b_n5 = new Ri(96, 4, 3, 1);
        public readonly Ri b_n6 = new Ri(90, 6, 3, 3);
        public readonly Ri b_n7 = new Ri(94, 6, 1, 3);
        public readonly Ri b_n8 = new Ri(96, 6, 3, 3);

        public readonly Ri b_h1 = new Ri(100, 0, 4, 4 );
        public readonly Ri b_h2 = new Ri(105, 0, 1, 4);
        public readonly Ri b_h3 = new Ri(107, 0, 4, 4);
        public readonly Ri b_h4 = new Ri(100, 5, 4, 1 );
        public readonly Ri b_h5 = new Ri(107, 5, 4, 1);
        public readonly Ri b_h6 = new Ri(100, 7, 4, 4);
        public readonly Ri b_h7 = new Ri(105, 7, 1, 4);
        public readonly Ri b_h8 = new Ri(107, 7, 4, 4);

        public readonly Ri b_c1 = new Ri(112, 0, 6, 6 );
        public readonly Ri b_c2 = new Ri(119, 0, 1, 6);
        public readonly Ri b_c3 = new Ri(121, 0, 6, 6);
        public readonly Ri b_c4 = new Ri(112, 7, 6, 1 );
        public readonly Ri b_c5 = new Ri(121, 7, 6, 1);
        public readonly Ri b_c6 = new Ri(112, 9, 6, 6);
        public readonly Ri b_c7 = new Ri(119, 9, 1, 6);
        public readonly Ri b_c8 = new Ri(121, 9, 6, 6);

        public readonly Ri b_grad = new Ri(96, 26, 50, 50);
        
        protected int pW = 0;
        protected int pH = 0;



        public PanelGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {
            PanelGUIE cparent = parent as PanelGUIE;
            if (cparent != null) {
                pW = cparent.lW - (b_n1.w + 1 + b_n1.w);
                pH = cparent.lH - (b_n1.h + 1 + b_n1.h);
            }
        }
        public override void Render(SpriteBatch spriteBatch) {
            PanelGUIE cparent = parent as PanelGUIE;
            if (parent.isUniv) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}
                //base.Render(spriteBatch);

                if (cparent.navigable && cparent.navArea != null && cparent.navSlider != null) {
                    RenderSlider(spriteBatch);
                }
            }
        }
        public void RenderSlider(SpriteBatch spriteBatch) {
            PanelGUIE cparent = parent as PanelGUIE;
            cparent.navSlider.Draw(spriteBatch);
        }
        public void RenderN(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            PanelGUIE cparent = parent as PanelGUIE;
            if (cparent.navigable) {
                rX -= cparent.navArea.x;
                rY -= cparent.navArea.y;
            }
            int midareaX = rW - b_c1.w - b_c3.w;
            int midareaY = rH - b_c1.h - b_c6.h;

            int el1X = rX, el1Y = rY, el1W = b_c1.w, el1H = b_c1.h;
            int el2X = rX+el1W, el2Y = rY, el2W = midareaX, el2H = b_c2.h;
            int el3X = rX+el1W+midareaX, el3Y = rY, el3W = b_c3.w, el3H = b_c3.h;
            int el4X = rX, el4Y = rY+el1H, el4W = b_c4.w, el4H = midareaY;
            int elGradX = rX+el4W, elGradY = rY+el1H, elGradW = midareaX, elGradH = midareaY;
            int el5X = rX+el4W+midareaX, el5Y = rY+el1H, el5W = b_c5.w, el5H = midareaY;
            int el6X = rX, el6Y = rY+el1H+midareaY, el6W = b_c6.w, el6H = b_c6.h;
            int el7X = rX+el6W, el7Y = rY+el1H+midareaY, el7W = midareaX, el7H = b_c7.h;
            int el8X = rX+el6W+midareaX, el8Y = rY+el1H+midareaY, el8W = b_c8.w, el8H = b_c8.h;
        
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c1,   new Ri(el1X, el1Y, el1W, el1H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c2,   new Ri(el2X, el2Y, el2W, el2H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c3,   new Ri(el3X, el3Y, el3W, el3H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c4,   new Ri(el4X, el4Y, el4W, el4H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c5,   new Ri(el5X, el5Y, el5W, el5H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c6,   new Ri(el6X, el6Y, el6W, el6H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c7,   new Ri(el7X, el7Y, el7W, el7H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c8,   new Ri(el8X, el8Y, el8W, el8H), Color.White);
            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_grad, new Ri(elGradX, elGradY, elGradW, elGradH), Color.White);
        }
        public void RenderH(SpriteBatch spriteBatch) {

        }
        public void RenderC(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            PanelGUIE cparent = parent as PanelGUIE;
            if (cparent.navigable) {
                rX -= cparent.navArea.x;
                rY -= cparent.navArea.y;
            }
            int midareaX = rW - b_c1.w - b_c3.w;
            int midareaY = rH - b_c1.h - b_c6.h;



            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c1,   new Ri(el1X, el1Y, el1W, el1H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c2,   new Ri(el2X, el2Y, el2W, el2H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c3,   new Ri(el3X, el3Y, el3W, el3H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c4,   new Ri(el4X, el4Y, el4W, el4H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c5,   new Ri(el5X, el5Y, el5W, el5H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c6,   new Ri(el6X, el6Y, el6W, el6H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c7,   new Ri(el7X, el7Y, el7W, el7H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c8,   new Ri(el8X, el8Y, el8W, el8H), Color.White);
            // gui.DrawGUIUniversalTexture(spriteBatch, parent, b_grad, new Ri(el9X, el9Y, el9W, el9H), Color.White);
        }
        /*public void RenderN(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_n1, 0, 0);
            RenderP(spriteBatch, b_n2, b_n1.w, 0);
            RenderP(spriteBatch, b_n3, b_n1.w+pW, 0);
            RenderP(spriteBatch, b_n4, 0, b_n1.h);
            RenderP(spriteBatch, b_n5, b_n1.w+pW, b_n1.h);
            RenderP(spriteBatch, b_n6, 0, b_n1.h+pH);
            RenderP(spriteBatch, b_n7, b_n1.w, b_n1.h+pH);
            RenderP(spriteBatch, b_n8, b_n1.w+pW, b_n1.h+pH);
            RenderG(spriteBatch, b_grad, b_n1.w, b_n1.h);
        }
        public void RenderH(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_h1, 0, 0);
            RenderP(spriteBatch, b_h2, b_n1.w, 0);
            RenderP(spriteBatch, b_h3, b_n1.w+pW, 0);
            RenderP(spriteBatch, b_h4, 0, b_n1.h);
            RenderP(spriteBatch, b_h5, b_n1.w+pW, b_n1.h);
            RenderP(spriteBatch, b_h6, 0, b_n1.h+pH);
            RenderP(spriteBatch, b_h7, b_n1.w, b_n1.h+pH);
            RenderP(spriteBatch, b_h8, b_n1.w+pW, b_n1.h+pH);
            RenderG(spriteBatch, b_grad, b_n1.w, b_n1.h);
        }
        public void RenderC(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_c1, 0, 0);
            RenderP(spriteBatch, b_c2, b_n1.w, 0);
            RenderP(spriteBatch, b_c3, b_n1.w+pW, 0);
            RenderP(spriteBatch, b_c4, 0, b_n1.h);
            RenderP(spriteBatch, b_c5, b_n1.w+pW, b_n1.h);
            RenderP(spriteBatch, b_c6, 0, b_n1.h+pH);
            RenderP(spriteBatch, b_c7, b_n1.w, b_n1.h+pH);
            RenderP(spriteBatch, b_c8, b_n1.w+pW, b_n1.h+pH);
            RenderG(spriteBatch, b_grad, b_n1.w, b_n1.h);
        }
        public void RenderP(SpriteBatch spriteBatch, Ri bounds, int mx, int my) {
            gui.DrawGUIUnivTClickState(spriteBatch, parent, bounds, Color.White, mx, my);
        }
        public void RenderG(SpriteBatch spriteBatch, Ri bounds, int mx, int my) {
            gui.DrawGUIUnivT(spriteBatch, parent, b_grad, new Ri(0,0,pW,pH), Color.White, mx, my);
        }*/
    }
    public class PanelGUIE : GUIE {
        public Ti navA = null;
        public bool navigable = false;
        public SliderGUIE navSlider = null;

        public PanelGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1, 
                         bool? navigable = null, Ti? navigableArea = null, SliderGUIE? navSlider = null) 
        : base(gui, d, a, GUIT.PANEL, lX, lY, lW, lH) {
            guir = new PanelGUIR(this, gui, d, a);
            guia = new PanelGUIA(this, gui, d, a);
            isUniv = true;


            this.navArea = navigableArea ?? null;
            this.navigable = navigable ?? false;
            this.hasNavArea = navigable ?? false;
            
            if (this.navArea != null && this.navigable == true) {
                this.navSlider = navSlider ?? null;

                if (this.navSlider != null) {
                    this.navSlider.Connect(this);
                }
            }
        }
        protected override void cWidth(int p = 57) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 57) {
            base.cHeight(p);
        }
    };
    
}








/*
    Frames 
    Topleft, Top, Topright, Left, Right, BottomLeft, Bottom, BottomRight
        {Normal, Hover, Click}
    
    FrameA
        90, 0, 3, 3
        94, 0, 1, 3
        96, 0, 3, 3
        90, 4, 3, 1
        96, 4, 3, 1
        90, 6, 3, 3
        94, 6, 1, 3
        96, 6, 3, 3

        100, 0, 4, 4 
        105, 0, 1, 4
        107, 0, 4, 4
        100, 5, 4, 1 
        107, 5, 4, 1
        100, 7, 4, 4
        105, 7, 1, 4
        107, 7, 4, 4

        112, 0, 6, 6 
        119, 0, 1, 6
        121, 0, 6, 6
        112, 7, 6, 1 
        121, 7, 6, 1
        112, 9, 6, 6
        119, 9, 1, 6
        121, 9, 6, 6

    Gradient
        96, 26, 50, 50
*/



