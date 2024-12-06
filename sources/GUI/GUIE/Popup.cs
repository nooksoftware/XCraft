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
    public class PopupGUIA : GUIA {
        public PopupGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Activity() {
            base.Activity();
            PopupGUIE cparent = parent as PopupGUIE;
            if (cparent == null) {
                return;
            }
            //StandardClickStateDetermineForClickable();
        }
    }
    public class PopupGUIR : GUIR {
        public PopupGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            PopupGUIE cparent = parent as PopupGUIE;

            //base.Render(spriteBatch);
        }
    };
    public class PopupGUIE : GUIE {
        public PopupGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1 ) : base(gui, d, a, GUIT.POPUP, lH, lY, lW, lH) {
            guir = new PopupGUIR(this, gui, d, a);
            guia = new PopupGUIA(this, gui, d, a);
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