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
            this.parent = e;
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
            foreach (var el in children) {
                el.Value.Activity();
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
*/
}