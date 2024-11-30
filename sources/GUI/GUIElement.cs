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

        public GUIE parent;
        public GUIR r;
        public GUIA a;
        public GUIT guit = GUIT.GUIELEMENT;

        public bool isUniv = true;
        public int click_state = 0; //normal, hover, clicked - 0, 1, 2
        public Color clicked_color;
        public Color hover_color;
        public Color normal_color;

        public Dictionary<int, Ri> clickStateGUIUnivB;
        public Dictionary<int, Color> clickStateGUIUnivC;

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
            clickStateGUIUnivC.Add(0, x0, y0, w0, h0);
            clickStateGUIUnivC.Add(1, x1, y1, w1, h1);
            clickStateGUIUnivC.Add(2, x2, y2, w2, h2);
        }
        public void InitializeGUIUnivC(Color n, Color h, Color c) {
            clickStateGUIUnivC.Add(0, n);
            clickStateGUIUnivC.Add(1, h);
            clickStateGUIUnivC.Add(2, c);
        }
        public GUIE(GUI gui, D d, GUIT guit = GUIT.GUIELEMENT) {
            this.gui = gui;
            this.d = d;
            this.clickStateGUIUnivB = new Dictionary<int, Ri>();
            this.clickStateGUIUnivC = new Dictionary<int, Color>();

            this.clickStateGUIUnivC.Add(0, Color.White);
            this.clickStateGUIUnivC.Add(1, Color.White);
            this.clickStateGUIUnivC.Add(2, Color.White);

            this.children = new Dictionary<string, GUIE>();

            r = new GUIR(this, gui, d);
            a = new GUIA(this, gui, d);
        }
        public void Connect(GUIE e) {
            e.parent = this;
        }
        public void Add(string path, GUIE add) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                e = e.children[key];
                if (e == null) {
                    return;
                } else if (i+1 == keys.Length) {
                    e.children[key] = add;
                    add.Connect(e);

                }
            }
        }
        public GUIE Get(string path) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                e = e.children[key];
                if (e == null) {
                    return null;
                } else if (i+1 == keys.Length) {
                    GUIE el = e.children[key];
                    return el;
                }
            }
            return null;
        }
        public bool Exists(string path) {
            string[] keys = path.Split('/');

            GUIE e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                e = e.children[key];

                if (e == null) {
                    return false;
                } else if (i+1 == keys.Length) {
                    return true;
                }
            }
            return (e != null);
        }
        public void Draw(SpriteBatch spriteBatch) {
            if(r != null) {
                r.Render(spriteBatch);
            }
            foreach (var el in children) {
                el.Value.Draw(spriteBatch);
            }
        }
    };
    public enum GUIT {
        GUIELEMENT,
        BUTTON,
        CHECKBOX,
        SLIDER,
        PANEL
    };
    public class GUIR {
        public GUIE parent; public GUI gui; public D d;
        public GUIR(GUIE parent, GUI gui, D d) {
            this.parent = parent;
            this.gui = gui;
            this.d = d;
        }
        public void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                Color c = clickStateGUIUnivC[click_state];
                if (c == null) {
                    c = Color.White;
                }
                Ri ri;
                if (color_state == 0 || color_state == 1 || color_state == 2) {
                    ri = clickStateGUIUnivB[color_state];
                } else {    
                    ri = new Ri(0,0,0,0);
                }
                gui.DrawGUIUnivTClickState(spriteBatch, ri, c); 
            }
        }
    };
    public class GUIA {
        public GUIE parent; public GUI gui; public D d;
        public GUIA(GUIE parent, GUI gui, D d) {
            this.parent = parent;
            this.gui = gui;
            this.d = d;
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
    public class PanelGUIE : GUIE {};

    /*
        0, 0, 13, 35
        14, 0, 13, 35
        28, 0, 13, 35
    */
    public class ButtonGUIE : GUIE {};

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
    public class SliderGUIE : GUIE {};

    public class TextInputFieldGUIE : GUI {};
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