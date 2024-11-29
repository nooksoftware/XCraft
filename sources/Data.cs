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
    public class D {
        public Game1 game;
        public M m;
        public NatAddressType n;

        public MouseState ms;
        public MouseState p_ms;
        public KeyboardState ks;
        public KeyboardState p_ks;
        
        public int mW = 512;
        public int mH = 256;
        public readonly float g = 9.0f;

        public D(Game1 game) {
            this.game = game;
        }
        public int xM {
            get {return (ms.X);}
        }
        public int yM {
            get {return (ms.Y);}
        }

        public readonly int def_mW = 512;
        public readonly int def_mH = 256;
        public readonly int def_wW = 1280;
        public readonly int def_wH = 720;
        public readonly int def_wW2 = 1600;
        public readonly int def_wH2 = 900;
    };
}