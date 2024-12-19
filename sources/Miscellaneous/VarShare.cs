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
using System.ComponentModel.DataAnnotations;
//using FastNoiseLite;

using MVector = Microsoft.Xna.Framework.Vector2;

namespace XCraft {
    public class VarShare {
        public Dictionary<string, VarShare> children;
        public VarShare() {
            children = new Dictionary<string, VarShare>();
        }   
        public virtual string ValueToString() {
            return "";
        }
    };
}



/*

public enum VarShareT {
        INT,
        FLOAT,
        DOUBLE,
        BOOLEAN,
        VEC2I,
        VEC2F,
        RI,
        RF,
    };
    public class VarShare {
        public Color labelColor = Color.White;
        public Dictionary<string, VarShare> shares;
        public VarShareT t;
        public string id;
        public VarShare parent;

        public VarShare() {
            this.shares = new Dictionary<string, VarShare>();
        }

        public void Connect(VarShare parent) {
            this.parent = parent;
        }
        public void SetID(string id) {
            this.id = id;
        }
        public virtual string VarToString() {return "";}
        public void Add(string path, VarShare share) {
            string[] keys = path.Split('/');

            VarShare e = this;
            for (int i = 0; i < keys.Length; ++i) {
                string key = keys[i];

                if (e == null) {
                    return;
                } else if (i+1 == keys.Length) {
                    e.shares[key] = share;
                    share.Connect(e);
                    e.SetID(key);
                } else {
                    e = e.shares[key];
                }
            }
        }
        public VarShare Get(string path) {
            string[] keys = path.Split('/');

            VarShare e = this;
            for (int i = 0; i < keys.Length; i++) {
                string key = keys[i];

                if (e == null) {
                    return null;
                } else if (i+1 == keys.Length) {
                    e = e.shares[key];
                    return e;
                } else {
                    e = e.shares[key];
                }
            }
            return null;
        }
    };

    public class IntVarShare : VarShare {
        public int value = -1;
        public IntVarShare(int value) {
            this.value = value;
        }
        public override string VarToString() {
            return value.ToString();
        }
    }
    public class FloatVarShare : VarShare {
        public float value;
        public FloatVarShare(float value) {
            this.value = value;
        }
        public override string VarToString() {
            return value.ToString();
        }
    }
    public class DoubleVarShare : VarShare {
        public double value;
        public DoubleVarShare(double value) {
            this.value = value;
        }
        public override string VarToString() {
            return value.ToString();
        }
    }
    public class BooleanVarShare : VarShare {
        public bool value;
        public BooleanVarShare(bool value) {
            this.value = value;
        }
        public override string VarToString() {
            return (value ? "true" : "false");
        }
    }
    public class Vec2iVarShare : VarShare {
        public V2i value;
        public Vec2iVarShare(V2i value) {
            this.value = value;
        }
        public Vec2iVarShare(int x, int y) {
            this.value = new V2i(x,y);
        }
        public override string VarToString() {
            return ("(" + value.x.ToString() + ", " + value.y.ToString() + ")");
        }

    }
    public class Vec2fVarShare : VarShare {
        public V2f value;
        public Vec2fVarShare(V2f value) {
            this.value = value;
        }
        public override string VarToString() {
            return ("(" + value.x.ToString() + ", " + value.y.ToString() + ")");
        }
    }
    public class RiVarShare : VarShare {
        public Ri value;
        public RiVarShare(Ri value) {
            this.value = value;
        }
        public RiVarShare(int x, int y, int w, int h) {
            this.value = new Ri(x,y,w,h);

        }
        public override string VarToString() {
            return ("(" + value.x.ToString() + ", "
                        + value.y.ToString() + ", "
                        + value.w.ToString() + ", "
                        + value.h.ToString() + ", ");
        }
    }
    public class RfVarShare : VarShare {
        public Rf value;
        public RfVarShare(Rf value) {
            this.value = value;
        }
        public RfVarShare(float x, float y, float w, float h) {
            this.value = new Rf(x,y,w,h);
        }
        public override string VarToString() {
            return ("(" + value.x.ToString() + ", "
                        + value.y.ToString() + ", "
                        + value.w.ToString() + ", "
                        + value.h.ToString() + ", ");
        }
    }
    public class GAreaVArShare : VarShare {
        public GAreaVArShare(GArea value) {
            this.value = value;
        }
        public override string VarToString() {
            return value.VarToString();
        }
        GArea value;
    };
    

    public class VarOutlistA : GUIA {
        public VarOutlistA(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {

        }
    };
    public class VarOutlistR : GUIR {
        public VarOutlistR(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {
            VarOutlistE cparent = parent as VarOutlistE ;


        }
        public override void Render(SpriteBatch spriteBatch) {
            VarOutlistE cparent = parent as VarOutlistE;

            PanelGUIE panel = cparent.panel;
            panel.Draw(spriteBatch);

            RenderVarOutlists(spriteBatch);
        }
        public void RenderVarOutlists(SpriteBatch spriteBatch) {
            VarOutlistE cparent = parent as VarOutlistE;
            VarShare varShare = cparent.varShare;

            int level = 0;
            int x = cparent.rX() + 10;
            int y = cparent.rY() + 10;
            if (varShare != null) {
                VarShare current = varShare;
                // while (true) {
                    RenderVarOutlist(x,y,level,varShare, spriteBatch);
                //}
            }
        }
        public void RenderVarOutlist(int x, int y, int level, VarShare varShare, SpriteBatch spriteBatch) {
            if (varShare != null) {
                string text = varShare.id + "=" + varShare.VarToString(); 
                RenderText(x,y,text, varShare.labelColor, spriteBatch);
                foreach (var share in varShare.shares) {
                    if (share.Value != null) {
                        RenderVarOutlist(x+level*12,y,level+1, share.Value, spriteBatch);
                        y += 18;
                    }
                }
            } else {
                return;
            }
        }
        public void RenderText(int x, int y, string varText, Color textColor, SpriteBatch spriteBatch) {
            VarOutlistE cparent = parent as VarOutlistE;
            if (cparent != null) {
                SpriteFont font = cparent.font;
                
                spriteBatch.DrawString(font, varText, new MVector(x-2,y+2), Color.Black);
                spriteBatch.DrawString(font, varText, new MVector(x,y), textColor);
            }
        }
    };
    public class VarOutlistE : GUIE {
        public PanelGUIE panel;
        public VarShare varShare;
        public SpriteFont font;

        public VarOutlistE(
            GUI gui, D d, A a, SpriteFont font, int lX, int lY, int lW, int lH,
            VarShare varShare
        ) : base(gui, d, a, GUIT.VAROUTLISTE, lX, lY, lW, lH) {
            this.varShare = varShare;
            this.font = font;

            this.guia = new VarOutlistA(this, gui, d, a);
            this.guir = new VarOutlistR(this, gui, d, a);

            this.panel = new PanelGUIE(gui, d, a, lX, lY, lW, lH, false, null, null);
        }
       
    };


*/