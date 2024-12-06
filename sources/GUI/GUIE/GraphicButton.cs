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
    public class GraphicButtonGUIA : GUIA {
        public GraphicButtonGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Activity() {
            base.Activity();
            GraphicButtonGUIE cparent = parent as GraphicButtonGUIE;
            if (cparent == null) {
                return;
            }
            //StandardClickStateDetermineForClickable();
        }
    }
    public class GraphicButtonGUIR : GUIR {
        public GraphicButtonGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            GraphicButtonGUIE cparent = parent as GraphicButtonGUIE;

            //base.Render(spriteBatch);
        }
    };
    public class GraphicButtonGUIE : GUIE {
        public Texture2D tn;
        public Rectangle tboundsNormal;
        public Rectangle tboundsHover;
        public Rectangle tboundsClicked;
        public GraphicButtonGUIE(GUI gui, D d, A a, Texture2D t, Rectangle? tbounds = null, int lX = 0, int lY = 0, int lW = -1, int lH = -1 ) : base(gui, d, a, GUIT.GRAPHICBUTTON, lH, lY, lW, lH) {
            guir = new GraphicButtonGUIR(this, gui, d, a);
            guia = new GraphicButtonGUIA(this, gui, d, a);
            this.tn = t;
            // ?
            //this.tboundsNormal = (tboundsNormal ?? new Rectangle(-1,-1,-1,-1));

            //cWidth();
            //cHeight();
            isUniv = true;
        }
        protected override void cWidth(int p = 0) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 0) {
            base.cHeight(p);
        }
    };
}