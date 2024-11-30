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
    public class TextInputFieldGUIA : GUIA {
        public TextInputFieldGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

    }
    public class TextInputFieldGUIR : GUIR {
        public TextInputFieldGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d,a ) {}
        public override void Render(SpriteBatch spriteBatch) {
            if (parent.isUniv) {
                base.Render(spriteBatch);
            }
        }
    }
    public class TextInputFieldGUIE : GUIE {
        public TextInputFieldGUIE(GUI gui, D d, A a, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.TEXT_INPUT_FIELD, lX, lY, lW, lH) {
            guir = new TextInputFieldGUIR(this, gui, d, a);
            guia = new TextInputFieldGUIA(this, gui, d, a);
            isUniv = true;
        }
        protected override void cWidth(int p = 31) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 23) {
            base.cHeight(p);
        }
    };
}
































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