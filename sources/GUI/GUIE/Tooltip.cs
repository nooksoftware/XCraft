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
    public class TooltipGUIA : GUIA {
        public TooltipGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Activity() {
            base.Activity();
            TooltipGUIE cparent = parent as TooltipGUIE;
            if (cparent == null) {
                return;
            }
            //StandardClickStateDetermineForClickable();
        }
    }
    public class TooltipGUIR : GUIR {
        public TooltipGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            TooltipGUIE cparent = parent as TooltipGUIE;

            //base.Render(spriteBatch);
        }
    };
    public class TooltipGUIE : GUIE {
        public TooltipGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1 ) : base(gui, d, a, GUIT.TOOLTIP, lH, lY, lW, lH) {
            guir = new TooltipGUIR(this, gui, d, a);
            guia = new TooltipGUIA(this, gui, d, a);
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