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
    public class GUIE {
        public Dictionary<string, GUIE> children;
        public GUI gui;
        public D d;
        public A a;

        public GUIE parent;
        public GUIR guir;
        public GUIA guia;
        public GUIT guit = GUIT.GUIELEMENT;

        public bool isUniv = true;
        public int click_state = 0; //normal, hover, clicked - 0, 1, 2
        public Color clicked_color;
        public Color hover_color;
        public Color normal_color;

        public Dictionary<int, Ri> clickStateGUIUnivB;
        public Dictionary<int, Color> clickStateGUIUnivC;

        public int lX = 0;
        public int lY = 0;
        public int lW = -1;
        public int lH = -1;
        public int lOx = 0;
        public int lOy = 0;

        public void CenterOxy() {
            lOx = lW /2;
            lOy = lH /2;
        }

        public int rX() {
            if (parent != null) {
                return lX-lOx + parent.rX();
            } else {
                return lX-lOx;
            }
        }
        public int rY() {
            if (parent != null) {
                return lY-lOy + parent.rY();
            } else {
                return lY-lOy;
            }
        }
        public int rW() {
            return lW;
        }
        public int rH() {
            return lH;
        }
        public bool HasParent() {
            if (parent != null) {
                return true;
            }
            return false;
        }
        public void InitializeGUIUnivB(
            int x0, int y0, int w0, int h0,
            int x1, int y1, int w1, int h1,
            int x2, int y2, int w2, int h2
        ) {
            clickStateGUIUnivB.Add(0, new Ri(x0, y0, w0, h0));
            clickStateGUIUnivB.Add(1, new Ri(x1, y1, w1, h1));
            clickStateGUIUnivB.Add(2, new Ri(x2, y2, w2, h2));
        }
        public void InitializeGUIUnivC(Color n, Color h, Color c) {
            clickStateGUIUnivC.Add(0, n);
            clickStateGUIUnivC.Add(1, h);
            clickStateGUIUnivC.Add(2, c);
        }
        protected virtual void cWidth(int p) {
            lW = (p != -1 ? p : 0);
        }
        protected virtual void cHeight(int p) {
            lH = (p != -1 ? p : 0);
        }
        public GUIE(GUI gui, D d, A a, GUIT guit = GUIT.GUIELEMENT, int lX = 0, int lY = 0, int lW = -1, int lH = -1) {
            
            this.lX = lX;
            this.lY = lY;
            this.lW = lW;
            this.lH = lH;

            if (lW == -1) {cWidth(0);}
            if (lH == -1) {cHeight(0);}
            
            this.gui = gui;
            this.d = d;
            this.a = a;
            this.clickStateGUIUnivB = new Dictionary<int, Ri>();
            this.clickStateGUIUnivC = new Dictionary<int, Color>();

            this.clickStateGUIUnivC.Add(0, Color.White);
            this.clickStateGUIUnivC.Add(1, Color.White);
            this.clickStateGUIUnivC.Add(2, Color.White);

            this.children = new Dictionary<string, GUIE>();

            guir = new GUIR(this, gui, d, a);
            guia = new GUIA(this, gui, d, a);
        }
        public void Connect(GUIE e) {
            e.parent = this;
        }
        public void Add(string path, GUIE add) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                
                if (e == null) {
                    return;
                } else if (i+1 == keys.Length) {
                    e.children[key] = add;
                    add.Connect(e);
                } else {
                    e = e.children[key];
                }
            }
        }
        public GuiET Get2<GuiET>(string path) where GuiET : class {
            GUIE e = Get(path);
            if (e != null) {
                GuiET e2 = e as GuiET;
                if (e2 != null) {
                    return e2;
                }
            }
            return default(GuiET);
        }
        public GUIE Get(string path) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                if (e == null) {
                    return null;
                } else if (i+1 == keys.Length) {
                    GUIE el = e.children[key];
                    return el;
                } else {
                    e = e.children[key];
                }
            }
            return null;
        }
        public bool Exists(string path) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                if (e == null) {
                    return false;
                } else if (i+1 == keys.Length) {
                    return true;
                } else {
                    e = e.children[key];
                }
            }
            return (e != null);
        }
        public void Draw(SpriteBatch spriteBatch) {
            if(guir != null) {
                guir.Render(spriteBatch);
            }
            foreach (var el in children) {
                el.Value.Draw(spriteBatch);
            }
        }
        public void Activity() {
            if (guia != null) {
                guia.Activity();
            }
        }
    };
    public enum GUIT {
        GUIELEMENT,
        BUTTON,
        SLIDER,
        PANEL,
        TEXT_INPUT_FIELD
    };
    public class GUIR {
        public GUIE parent; public GUI gui; public D d; public A a;
        public GUIR(GUIE parent, GUI gui, D d, A a) {
            this.parent = parent;
            this.gui = gui;
            this.d = d;
            this.a = a;
        }
        public int clickState {
            get {return parent.click_state;}
        }
        public Dictionary<int, Ri> clickStateGUIUnivB {
            get {return parent.clickStateGUIUnivB; }
        }
        public Dictionary<int, Color> clickStateGUIUnivC {
            get {return parent.clickStateGUIUnivC; }
        }
        public virtual void Render(SpriteBatch spriteBatch) {
            if (parent.guit == GUIT.GUIELEMENT) {
                return;
            }
            if (parent.isUniv) {
                Color c = clickStateGUIUnivC[clickState];
                if (c == null) {
                    c = Color.White;
                }
                Ri ri;
                if (clickState == 0 || clickState == 1 || clickState == 2) {
                    ri = clickStateGUIUnivB[clickState];
                } else {    
                    ri = new Ri(0,0,0,0);
                }
                gui.DrawGUIUnivTClickState(spriteBatch, parent, ri, c); 
            }
        }
    };
    public class GUIA {
        public GUIE parent; public GUI gui; public D d; public A a;
        public GUIA(GUIE parent, GUI gui, D d, A a) {
            this.parent = parent;
            this.gui = gui;
            this.d = d;
            this.a = a;
        }
        public bool MouseOnArea() {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();
            int mX = d.ms.X;
            int mY = d.ms.Y;
            return ((mX > rX && mX < rX + rW) && (mY > rY && mY < rY + rH));
        }
        public bool Clicked() {
            return a.OneReleasedLMB();
        }
        public bool LMBHold() {
            return a.LMBHold();
        }
        public void StandardClickStateDetermineForClickable() {
            if (parent.click_state == 2 && LMBHold()) {
                parent.click_state = 2;
                return;
            }
            if (MouseOnArea()) {
                if (Clicked()) {
                    parent.click_state = 2;    
                } else {
                    parent.click_state = 1;
                }
            } else {
                parent.click_state = 0;
            }
        }
        public bool ClickedOnArea() {
            return (Clicked() && MouseOnArea());
        }
        public virtual void Activity() {

        }
        
    };

    /*

    */
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
    public class PanelGUIA : GUIA {
        public PanelGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

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
        
        public PanelGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}
                //base.Render(spriteBatch);
            }
        }
        public void RenderN(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_n1);
            RenderP(spriteBatch, b_n2);
            RenderP(spriteBatch, b_n3);
            RenderP(spriteBatch, b_n4);
            RenderP(spriteBatch, b_n5);
            RenderP(spriteBatch, b_n6);
            RenderP(spriteBatch, b_n7);
            RenderP(spriteBatch, b_n8);
        }
        public void RenderH(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_h1);
            RenderP(spriteBatch, b_h2);
            RenderP(spriteBatch, b_h3);
            RenderP(spriteBatch, b_h4);
            RenderP(spriteBatch, b_h5);
            RenderP(spriteBatch, b_h6);
            RenderP(spriteBatch, b_h7);
            RenderP(spriteBatch, b_h8);
        }
        public void RenderC(SpriteBatch spriteBatch) {
            RenderP(spriteBatch, b_c1);
            RenderP(spriteBatch, b_c2);
            RenderP(spriteBatch, b_c3);
            RenderP(spriteBatch, b_c4);
            RenderP(spriteBatch, b_c5);
            RenderP(spriteBatch, b_c6);
            RenderP(spriteBatch, b_c7);
            RenderP(spriteBatch, b_c8);
        }
        public void RenderP(SpriteBatch spriteBatch, Ri bounds) {
            gui.DrawGUIUnivTClickState(spriteBatch, parent, bounds, Color.White);
        }
    }
    public class PanelGUIE : GUIE {
        public PanelGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.PANEL, lX, lY, lW, lH) {
            guir = new PanelGUIR(this, gui, d, a);
            guia = new PanelGUIA(this, gui, d, a);
            isUniv = true;
        }
        protected override void cWidth(int p = 57) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 57) {
            base.cHeight(p);
        }
    };

    /*
        0, 0, 13, 35
        14, 0, 13, 35
        28, 0, 13, 35
    */
    public class ButtonGUIA : GUIA {
        public ButtonGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Activity() {
            ButtonGUIE cparent = parent as ButtonGUIE;
            if (cparent == null) {
                return;
            }
            if (ClickedOnArea()) {
                cparent.clicked = true;
            } else {
                cparent.clicked = false;
            }
        }
    }
    public class ButtonGUIR : GUIR {
        public readonly Ri b_n= new Ri(0, 0, 13, 35);
        public readonly Ri b_h= new Ri(14, 0, 13, 35);
        public readonly Ri b_c= new Ri(28, 0, 13, 35);

        public ButtonGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}
                //base.Render(spriteBatch);
            }
        }
        public void RenderN(SpriteBatch spriteBatch) {
            gui.DrawGUIUnivTClickState(spriteBatch, parent, b_n, Color.White);
        }

        public void RenderH(SpriteBatch spriteBatch) {
            gui.DrawGUIUnivTClickState(spriteBatch, parent, b_h, Color.White);
        }

        public void RenderC(SpriteBatch spriteBatch) {
            gui.DrawGUIUnivTClickState(spriteBatch, parent, b_c, Color.White);
        }

    }
    public class ButtonGUIE : GUIE {
        public ButtonGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.BUTTON, lX, lY, lW, lH) {
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
    public class SliderGUIA : GUIA {
        public SliderGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
    }
    public class SliderGUIR : GUIR {
        public SliderGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                base.Render(spriteBatch);
            }
        }
    }
    public class SliderGUIE : GUIE {
        public SliderGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.SLIDER, lX, lY, lW, lH) {
            guir = new SliderGUIR(this, gui, d, a);
            guia = new SliderGUIA(this, gui, d, a);
            isUniv = true;
        }
        protected override void cWidth(int p = 43) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 22) {
            base.cHeight(p);
        }
    };
    public class TextInputFieldGUIA : GUIA {
        public TextInputFieldGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

    }
    public class TextInputFieldGUIR : GUIR {
        public TextInputFieldGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d,a ) {}
        public override void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                base.Render(spriteBatch);
            }
        }
    }
    public class TextInputFieldGUIE : GUIE {
        public TextInputFieldGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.TEXT_INPUT_FIELD, lX, lY, lW, lH) {
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
}