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
    public class E {
        public float x = 0.0f;
        public float y = 0.0f;
        public int ox = 0;
        public int oy = 0;
        public int w = 0.0f;
        public int h = 0.0f;
        public float accX = 0.0f;
        public float accY = 0.0f;
        public bool accXb = false;
        public bool accYb = false;

        public ET et = ET.NONE;
        //public Texture2D t;
        public ER er;
        public EI ei;
        public D d;

        public bool gravity = true;
        public bool solid = true;
        public bool physics = true;

        public E(int x, int y, ET et/*/ Texture2D t*/) {
            this.x = x;
            this.y = y;
            this.et = et;
            this.ox = 0;
            this.oy = 0;
            this.w = 0;
            this.h = 0;
        }
        public void Draw(SpriteBatch spriteBatch) {
            //scale (zoom)
            Rectangle d = new Rectangle(x-ox - d.z.zX, x-oy - d.z.zY, w, h);
            Rectangle o = new Rectangle(0, 0, w, h);
            //spriteBatch.Draw(t, d, o, Color.White);
        }
        public void Acc(float accX, float accY) {
            this.accX = this.accX + accX;
            this.accY = this.accY + accY;
            if (this.accX < 0.035f && this.accX > -0.035f) {
                this.accX = 0.0f;
                this.accXb = false;
            } else {
                this.accXb = true;
            }
            if (this.accY < 0.035f && this.accY > -0.035f) {
                this.accY = 0.0f;
                this.accYb = false;
            } else {
                this.accYb = true;
            }
        }
        protected void PhysicsTransform() {
            if (gravity) {
                Acc(0, d.g);
            } 
            if (solid && physics) {
                /*
                  E[] prox_entities = a.ProxEntities(x,y);
                  T[] prox_tiles = a.ProxTiles(x,y);

                  foreach (e : prox_entities as E) {
                    PSolidhysics(this,e );
                  }
                  foreach (t : prox_entities as T) {
                    SolidPhysics(this,t);
                  }
                */
            }
        }
    };
    public enum ET {
        NONE,
        PLAYER
    };
    public class ER {

    };
    public class EI {

    };
}