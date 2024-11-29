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
    public class T {
        public x = 0;
        public y = 0;
        public TT tt;

        public T(int x, int y, TT tt) {
            this.x = x;
            this.y = y;
            this.tt = tt;
        }
    };
    public enum TT {
        UNKNOWN = -1,
        AIR = 0,
        DIRT,
        GRASS,
        STONE,
        BEDROCK,
        SAND,
        CLAY,
        WATER,
        MUD,
        LOG1,
        LOG2,
        LOG3,
        LEAVES1,
        LEAVES2,
        LEAVES3,
        WOODEN_PL1,
        WOODEN_PL2,
        WOODEN_PL3,
        WOOD1,
        WOOD2,
        WOOD3,
        BRICKS,
        CONCRETE,
        STONE_BRICKS,
        METAL,
        WOODEN_BOX,
        METAL_BOX,
        IRON_ORE,
        GOLD_ORE,
        DIA_ORE,
        ORE1,
        ORE2,
        ORE3,
    };
    public class S {

    };
    
    public class N {
        public int zX = 0;
        public int zY = 0;
        public int zZ = 100; //50-200
        public int zZS = 1;
        public int nX = 0;
        public int nY = 0;

        public N() {
            ns = new Dictionary<int, N>();
        }
        public Dictionary<int, N> ns;

        //ver
        public N this[int i] {
            get {N n = null; ns.GetValueOrDefault(i, n); return n;}
            set {if (ns[i] != null) {ns[i] = new N();} else { ns.Add(i, new N());}}
        }
    };
    public class ER {

    };
    public class EI {

    };
    public class PR : ER {

    };
    public class PI : EI {

    };
    public class P {

    };
    public class E {

    };
    public class Ev {

    };
    public class GUI {

    };
    public class GUIE {

    };
    public class GUIT {

    };
    public class GUIR {

    };
    public class GUIA {

    };
    public class G {
        public M m;

        int w = 512;
        int h = 256;

        int mH3 = 3;
        int mH2 = 96;
        int mH1 = 160;

        int iOp = 2200;
        int gOp = 4500;
        int dOp = 6000;

        public G() {

        }
    };
    public class M {
        TT[,] tts;
        T[,] ts;

        public TT TT(int x, int y) {
            if (tts[x,y] != null) 
                return tts[x,y];

            return null;
        }
        public T T(int x, int y) {
            if (ts[x,y] != null) 
                return ts[x,y];

            return null;
        }
    };
}