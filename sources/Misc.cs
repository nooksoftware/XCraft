using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
//using FastNoiseLite;

namespace XCraft {
    public class Ti {
    public Ti() {}
    };
    public class Tf {
        public Tf() {}
    };
    public class Rf {
        public float x = 0.0f; 
        public float y = 0.0f; 
        public float w = 0.0f; 
        public float h = 0.0f;
        public Rf(float x, float y, float w, float h) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    };
    public class Ri {
        public int x = 0; 
        public int y = 0; 
        public int w = 0; 
        public int h = 0;
        public Ri(int x, int y, int w, int h) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    };
    public class V2i {
        public int x = 0;
        public int y = 0;
        public V2i(int x, int y) {
            this.x = x;
            this.y = y;
        }
    };
    public class V2f {
        public float x = 0.0f;
        public float y = 0.0f;
        public V2f(float x, float y) {
            this.x = x;
            this.y = y;
        }
    };
    public class V3i {
        public int x = 0;
        public int y = 0;
        public int z = 0;
        public V3i(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;

        }
    };
    public class V3f {
        public float x = 0.0f;
        public float y = 0.0f;
        public float z = 0.0f;
        public V3f(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };
    public class SBounds {
        public int ls = 0;
        public int rs = 0;
        public int ts = 0;
        public int bs = 0;
        public bool l = false;
        public bool r = false;
        public bool t = false;
        public bool b = false;

        public bool Check(SBounds b) {
            //todo
        }
    };

//    public static bool RectContains(Rectangle r, int x, int y) {
//        return (x > r.X && x < r.X+r.Width && y > r.Y && y < r.Y+r.Height);
//    }
//    public static SideBounds SideBounds(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2) {
//        SideBounds b = new SideBounds();
//
//     b.l = (x1 == x2 + w2 || x2 == x1 + w1);
//        b.r = (x1 + w1 == x2 || x2 + w2 == x1);
//        b.t = (y1 == y2 + h2 || y2 == y1 + h1);
//        b.b = (y1 + h1 == y2 || y2 + h2 == y1);
//
//     return b;
//    }


}