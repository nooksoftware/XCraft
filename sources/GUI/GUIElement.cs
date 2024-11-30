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

        public bool HasParent() {
            if (parent != null) {
                return true;
            }
            return false;
        }
        public GUIE(GUI gui, D d, GUIT guit = GUIT.GUIELEMENT) {
            this.gui = gui;
            this.d = d;

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
}