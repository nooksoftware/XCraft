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
    public enum _BT {
        UNKNOWN,
        BASE,
        OUTPUST,
        ARMORY,
        SHOP,
        ASSEMBLY,
        STRUCTURE_PLACEHOLDER
    };
    public class _B : E {
        public _BT bt = _BT.UNKNOWN;
        public Texture2D tpbt;
        public D d;
        public Ri tpbtB;

        public _B(D d, int x, int y, ET et, _BT bt) : base(x,y,et) {
            this.d = d;
            this.bt = bt;
            if (bt == _BT.STRUCTURE_PLACEHOLDER) {
                tpbt = d.Tex("stel");
            } else {
                tpbt = d.Tex("tpbt");
            }
            tpbtB = d.btTpBounds[bt];
            this.w = tpbtB.w;
            this.h = tpbtB.h;
            this.ox = w/2;
            this.oy = h;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Rectangle de = new Rectangle(
                (int)(x-ox - d.n.zX),
                (int)(y-oy - d.n.zY),
                w,
                h
            );
            Rectangle o = new Rectangle(
                tpbtB.x,
                tpbtB.y,
                tpbtB.w,
                tpbtB.h
            );
            spriteBatch.Draw(tpbt,de,o, Color.White);
        }
    };
}