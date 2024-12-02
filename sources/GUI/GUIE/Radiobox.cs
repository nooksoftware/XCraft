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
    public class RadioboxGUIA : GUIA {
        public RadioboxGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}
        public override void Activity() {
            base.Activity();

            RadioboxGUIE cparent = parent as RadioboxGUIE;
            RadioboxGUIR cguir = cparent.guir as RadioboxGUIR;
            if (cparent == null) {
                return;
            }
            int rX = cparent.rX();
            int rY = cparent.rY();
            StandardClickStateDetermineForClickable(rX, rY, cguir.b_bn.w, cguir.b_bn.h);
            if (ClickedOnArea(rX, rY, cguir.b_bn.w, cguir.b_bn.h)) {
                cparent.SelectThisRadiobox();
            } else {
                
            }
        }
    };
    public class RadioboxGUIR : GUIR {
        public readonly Ri b_bn = new Ri(23, 109, 20, 20);  
        public readonly Ri b_bh = new Ri(44, 109, 20, 20);
        public readonly Ri b_bc = new Ri(65, 109, 20, 20);
        public readonly Ri b_t = new Ri(23, 130, 24, 24);
        public readonly Ri b_x = new Ri(48, 130, 24, 24);

        public RadioboxGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;

            if (parent.isUniv && cparent != null) {
                base.Render(spriteBatch);

                if (clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}

                RenderText(spriteBatch);

                if(cparent.isOneTicked) {
                    RenderTick(spriteBatch);
                } else if (cparent.showTransX) {
                    RenderTransX(spriteBatch);
                }
            }
        }
        public void RenderTick(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            rX = rX - ((b_t.w - b_bn.w) / 2);
            rY = rY - ((b_t.h - b_bn.h) / 2);

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_t, new Ri(rX, rY, b_t.w, b_t.h), Color.White);

        }
        public void RenderTransX(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            rX = rX - ((b_x.w - b_bn.w) / 2);
            rY = rY - ((b_x.h - b_bn.h) / 2);

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_x, new Ri(rX, rY, b_x.w, b_x.h), Color.White);

        }
        public void RenderN(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bn, new Ri(rX, rY, b_bn.w, b_bn.h), Color.White);
        }
        public void RenderH(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bh, new Ri(rX, rY, b_bh.w, b_bh.h), Color.White);
        }
        public void RenderC(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_bc, new Ri(rX, rY, b_bc.w, b_bc.h), Color.White);
        }
        public void RenderText(SpriteBatch spriteBatch) {
            RadioboxGUIE cparent = parent as RadioboxGUIE;
            int rX = parent.rX(); 
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();
            Microsoft.Xna.Framework.Vector2 textSize = cparent.f.MeasureString(cparent.text);

            int textX = rX + b_bn.w + 2;
            int textY = rY + (int)((b_bn.h - textSize.Y) / 2);

            spriteBatch.DrawString(cparent.f, cparent.text, new Microsoft.Xna.Framework.Vector2(textX, textY), Color.White);

        }
    };
    public class RadioboxGroup {
        public Dictionary<string, RadioboxGUIE> grouped;
        public string oneTicked = "None";

        public RadioboxGroup() {
            grouped = new Dictionary<string, RadioboxGUIE>();
        }
    };
    public class RadioboxGUIE : GUIE {
        public RadioboxGroup group;
        public SpriteFont f;
        public bool showTransX = true;
        public string text = "";
        public bool isOneTicked {
            get {return IsOneTicked();}
        }
        public RadioboxGUIE(GUI gui, D d, A a, SpriteFont f, string text, int lX = 0, int lY = 0, int lW = -1, int lH = -1, bool isTicked = false, bool showTransX = true, RadioboxGroup? radioboxGroup = null)
        : base(gui, d, a, GUIT.RADIOBOX, lX, lY, lW, lH)
        {
            this.text = text;
            this.f = f;
            guir = new RadioboxGUIR(this, gui, d, a);
            guia = new RadioboxGUIA(this, gui, d, a);
            isUniv = true;
            cWidth(); 
            RadioboxGUIR cguir = guir as RadioboxGUIR;
            group = radioboxGroup ?? null;
            if (group != null) {
                AddToGroup();
            } else {
                this.group = new RadioboxGroup();
                AddToGroup();
            }
            if (isTicked) {
                SelectThisRadiobox();
            }
       }
       public void SelectThisRadiobox() {
            if (group != null) {
                group.oneTicked = GetFullPath();
            }
       }
        protected override void cWidth(int p = 22) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 22) {
            base.cHeight(p);
        }
        public bool IsOneTicked() {
            if (group != null) {
                if (group.oneTicked == GetFullPath()) {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool AddToGroup() {
            if (group != null) {
                if (group.grouped != null) {
                    if (group.grouped.ContainsKey(GetFullPath())) {
                        group.grouped.Add(GetFullPath(), this);
                        return true;
                    }
                }
            }
            return false;
        }
    };
    /*
    public class RadioboxGUIA : GUIA {
        public RadioboxGUIA(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {

        }
        public override void Activity() {
            base.Activity();

            RadioboxGUIE cparent = parent as RadioboxGUIE;
            if (cparent == null) {
                return;
            }

            StandardClickStateDetermineForClickable();

            if (ClickedOnArea()) {
                if (cparent.ticked) {
                    cparent.ticked = false;
                } else {
                    cparent.ticked = true;
                }
            }
        }
    };
    public class RadioboxGUIR : GUIR {
        public readonly Ri b_n = new Ri(0,0,0,0);
        public readonly Ri b_h = new Ri(0,0,0,0);
        public readonly Ri b_c = new Ri(0,0,0,0);
        public readonly Ri b_t = new Ri(0,0,0,0);

        public RadioboxGUIR(GUIE parent, GUI gui, D d, A a) : base(parent,gui,d,a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            ButtonGUIE cparent = parent as ButtonGUIE;
            if (parent.isUniv && cparent != null) {
                if(clickState == 0) {RenderN(spriteBatch);}
                else if (clickState == 1) {RenderH(spriteBatch);}
                else if (clickState == 2) {RenderC(spriteBatch);}

                Microsoft.Xna.Framework.Vector2 textSize = cparent.font.MeasureString(cparent.text);

                int textX = (int)(cparent.rX() + b_n.w + 6);
                int textY = (int)(cparent.rY() + ((b_n.h - textSize.Y) / 2));
            
                spriteBatch.DrawString(cparent.font, cparent.text, new Microsoft.Xna.Framework.Vector2(textX, textY), Color.White);
            
                if (cparent.ticked) {
                    RenderSel(spriteBatch);
                }
            }   
        }
        
        protected void RenderN(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_n, new Ri(rX, rY, b_n.w, b_n.h), Color.White);
        }
        protected void RenderH(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_h, new Ri(rX, rY, b_h.w, b_h.h), Color.White);
        }
        protected void RenderC(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_c, new Ri(rX, rY, b_c.w, b_c.h), Color.White);
        }
        protected void RenderSel(SpriteBatch spriteBatch) {
            int rX = parent.rX();
            int rY = parent.rY();
            int rW = parent.rW();
            int rH = parent.rH();

            gui.DrawGUIUniversalTexture(spriteBatch, parent, b_t, new Ri(rX, rY, b_t.w, b_t.h), Color.White);
        }
    };
    public class RadioboxGUIE : GUIE {
        public SpriteFont font;
        public string text;
        public RadioboxGUIE(GUI gui, D d, A a, SpriteFont font, string text, int lX = 0, int lY = 0, int lW = -1, int lH = -1) : base(parent,gui,d,a) {
            this.font = font;
            this.text = text;
            guir = new RadioboxGUIR(this, gui, d, a);
            guia = new RadioboxGUIA(this, gui, d, a);   
            isUniv = true;
        }
        public bool ticked = false;
        public bool clicked = false;
        protected override void cWidth(int p = 37) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 37) {
            base.cHeight(p);
        }
        public bool IsTicked() {
            return ticked;
        }
    };
    */
}