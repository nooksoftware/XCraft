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
using System.Net;
//using FastNoiseLite;

using MVector2 = Microsoft.Xna.Framework.Vector2;
using System.Net.Security;


namespace XCraft {
    
    public class N {
        //Navigation's Zoom Position
        public int zX = 0;
        public int zY = 0;
        public float zXAcc = 0.0f;
        public float zYAcc = 0.0f;
        public float zZ = 1.0f;
        public float zZAcc = 0.0f;
        public int zZS = 1;
        //Navigation positions
        public int nX = 0;
        public int nY = 0;

        

        public int RNavX() {
            return zX;
        }
        public int RNavY() {
            return zY;
        }
        public A a;
        public N(A a) {
            this.a = a;
        }
        public void Tick() {
            this.zX += System.Convert.ToInt32(zXAcc);
            this.zY += System.Convert.ToInt32(zYAcc);
            zXAcc *= 0.87f;
            zYAcc *= 0.87f;
            if (zXAcc > -0.05f && zXAcc < 0.05f) {
                zXAcc = 0.0f;
            }
            if (zYAcc > -0.05f && zYAcc < 0.05f) {
                zYAcc = 0.0f;
            }

            if (a.MouseScrollChange() > 0) // Scroll up
                this.zZAcc += 0.06f;
            else if (a.MouseScrollChange() < 0) // Scroll down
                this.zZAcc -= 0.06f;

            this.zZ += this.zZAcc;
            this.zZAcc *= 0.85f;
            if (this.zZAcc < 0.01f && this.zZAcc > -0.01f) {
                this.zZAcc = 0f;
            }
            if (this.zZ > 2.0f) {
                this.zZ = 2.0f;
            } else if (this.zZ < 0.5f) {
                this.zZ = 0.5f;
            }
        }
    };
}