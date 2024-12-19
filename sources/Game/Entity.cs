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
    public enum ET {
        UNKNOWN = -1,
        NONE = 0,
        PLAYER = 1
    };
    public class E {
        public float x = 0.0f;
        public float y = 0.0f;
        public int ox = 0;
        public int oy = 0;
        public int w = 0;
        public int h = 0;
        public float accX = 0.0f;
        public float accY = 0.0f;

        public Texture2D entitiesTp;

        public ET et = ET.NONE;
        public A a;
        public bool isPhysicsEntity = true;

        public E(A a, int x, int y, ET et) {
            this.x = x;
            this.y = y;
            this.et = et;
            this.ox = 0;
            this.oy = 0;
            this.w = 0;
            this.h = 0;

            this.a = a;
            this.entitiesTp = this.a.Tex("entities");
        }

        public virtual int screenXPos() {
            return System.Convert.ToInt32(
                this.x - this.ox - a.n.RNavX());
        }
        public virtual int screenYPos() {
            return System.Convert.ToInt32(
                this.y - this.oy - a.n.RNavY());
        }
        public virtual int graphicSizeX() {
            return 32;
        }
        public virtual int graphicSizeY() {
            return 42;
        }
        public virtual int OriginX() {
            return graphicSizeX() / 2;
        }
        public virtual int OriginY() {
            return graphicSizeY() / 2;
        }   
        public virtual int OriginW() {
            return 32;

        }
        public virtual int OriginH() {
            return 42;

        }

        public virtual Rectangle DestinationRectangle() {
            return new Rectangle(
                screenXPos(),
                screenYPos(),
                graphicSizeX(),
                graphicSizeY()
            );
        }
        public virtual Rectangle OriginRectangle() {
            return new Rectangle(
                OriginX(),
                OriginY(),
                OriginW(),
                OriginH()
            );
        }
        public Color renderColor = new Color(255,255,255,255);
        public virtual Color RenderColor() {
            return renderColor;
        }
        public virtual void Draw(SpriteBatch spriteBatch) {
            Rectangle d = DestinationRectangle();
            Rectangle o = OriginRectangle();

            spriteBatch.Draw(entitiesTp, d, o, RenderColor());
        }
        public virtual void Tick(SpriteBatch spriteBatch) {
            if (isPhysicsEntity) {
                PhysicsEntityTick(spriteBatch);
            }
        }
        public virtual void PhysicsEntityTick(SpriteBatch spriteBatch) {

        }
    };
}