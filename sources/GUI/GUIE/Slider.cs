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
}


























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