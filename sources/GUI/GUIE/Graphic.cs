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
    public class GraphicGUIA : GUIA {
        public GraphicGUIA(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d, a) {}

    }
    public class GraphicGUIR : GUIR {
        public GraphicGUIR(GUIE parent, GUI gui, D d, A a) : base(parent, gui, d,a ) {}
        public override void Render(SpriteBatch spriteBatch) {
            GraphicGUIE cparent = parent as GraphicGUIE;
            if (cparent.graphic != null && cparent != null) {
                cparent.graphic.Draw(spriteBatch, cparent.rX(), cparent.rY(), cparent.rW(), cparent.rH(), Color.White);
            }
            //if (parent.isUniv) {
            //    base.Render(spriteBatch);
            //}
        }
    }
    public class GraphicGUIE : GUIE {
        public Graphic graphic;

        public GraphicGUIE(GUI gui, D d, A a, Graphic graphic, int lX = 0, int lY = 0, int lW = -1, int lH = -1) 
        : base(gui, d, a, GUIT.TEXT_INPUT_FIELD, lX, lY, lW, lH) {
            guir = new GraphicGUIR(this, gui, d, a);
            guia = new GraphicGUIA(this, gui, d, a);
            this.graphic = graphic;
            isUniv = false;
        }
        protected override void cWidth(int p = 0) {
            base.cWidth(p);
        }
        protected override void cHeight(int p = 0) {
            base.cHeight(p);
        }
    };
}