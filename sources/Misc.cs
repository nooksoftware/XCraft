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

}