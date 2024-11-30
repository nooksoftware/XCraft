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
    public class N {
        public int zX = 0;
        public int zY = 0;
        public float zXAcc = 0.0f;
        public float zYAcc = 0.0f;
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
        public void ApplyNavAcc() {
            this.zX += System.Convert.ToInt32(zXAcc);
            this.zY += System.Convert.ToInt32(zYAcc);
            zXAcc *= 0.75f;
            zYAcc *= 0.75f;
            if (zXAcc > -0.05f && zXAcc < 0.05f) {
                zXAcc = 0.0f;
            }
            if (zYAcc > -0.1f && zYAcc < 0.1f) {
                zYAcc = 0.0f;
            }
        }
    };
}