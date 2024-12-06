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
    public class ColorPickerGUIA : GUIA {
        public ColorPickerGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Activity() {
            base.Activity();
            ColorPickerGUIE cparent = parent as ColorPickerGUIE;
            if (cparent == null) {
                return;
            }
            //StandardClickStateDetermineForClickable();
        }
    }
    public class ColorPickerGUIR : GUIR {
        public ColorPickerGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {

        }
        public override void Render(SpriteBatch spriteBatch) {
            ColorPickerGUIE cparent = parent as ColorPickerGUIE;

            //base.Render(spriteBatch);
        }
    };
    public class ColorPickerGUIE : GUIE {
        public ColorPickerGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1 ) : base(gui, d, a, GUIT.COLORPICKER, lH, lY, lW, lH) {
            guir = new ColorPickerGUIR(this, gui, d, a);
            guia = new ColorPickerGUIA(this, gui, d, a);
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