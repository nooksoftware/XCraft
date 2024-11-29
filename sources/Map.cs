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

    };
    public enum TT {

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

    };
    public class M {

    };
}