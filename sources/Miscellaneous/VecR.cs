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
    public class Ti {
        public int x = 0;
        public int y = 0;
        public int w = 0;
        public int h = 0;
        public int ox = 0;
        public int oy = 0;

        public Ti parentTi;

        public bool fcent = false;

        public Ti() {}
        public Ti(int x, int y, int w, int h) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public Ti(int x, int y, int w, int h, int ox, int oy) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.ox = ox;
            this.oy = oy;
        }

        public void SetParentTi(Ti parentTi) {
            this.parentTi = parentTi;
        }
        public void SetOrAsC() {
            fcent = true;
            ox = w/2;
            oy = h/2;
        }
        public void UnsetOrAsC(int ox = 0, int oy = 0) {
            fcent = false;
            this.ox = ox;
            this.oy = oy;
        }
        public virtual int rX() {
            return (parentTi != null) ? x-ox : (x-ox + parentTi.rX());
        }
        public virtual int rY() {
            return (parentTi != null) ? x-oy : (x-oy + parentTi.rY());
        }
        public virtual int rX(Ti rel) {
            return (parentTi != null) ? x-ox + rel.rX() : (x-ox + parentTi.rX() + rel.rX());
        }
        public virtual int rY(Ti rel) {
            return (parentTi != null) ? x-oy + rel.rY() : (x-oy + parentTi.rY() + rel.rY());
        }
        public virtual int rW() {
            return w;
        }
        public virtual int rH() {
            return h;
        }
        public virtual int rOX() {
            return ox;
        }
        public virtual int rOY() {
            return oy;
        }
        public void Move(V2i m) {
            this.x += m.x;
            this.y += m.y;
        }
        public void Move(int x, int y) {
            this.x = this.x + x;
            this.y = this.x + y;
        }
        public void MoveO(V2i o) {
            this.ox = this.ox + o.x;
            this.oy = this.oy + o.y;
        }
        public void MoveO(int x, int y) {
            this.ox = this.ox + x;
            this.oy = this.oy + y;
        }
//        public int rTopLeftX() {
//
//        }
//        public int rTopLeftY() {
//            
//        }
//        public int rTopRightX() {
//            
//        }
//        public int rTopRightY() {
//            
//        }
//        public int rBottomLeftX() {
//            
//        }
//        public int rBottomLeftY() {
//            
//        }
//        public int rBottomRightX() {
//            
//        }
//        public int rBottomRightY() {
//            
//        }
//        public int rCenX() {
//
//        }
//        public int rCenY() {
//
//        }
//        public double rAngleOriRad(Ri b) {
//            return System.Math.Atan2(b.rOY() - rOY(), b.rOX() - rOY());
//        }
//        public double rAngleOriDeg(Ri b) {
//            return (AngleOriRad(b) * (180 / System.Math.PI));
//        }
//
//        public double rAngleTopLeftRad(Ri b) {
//            return System.Math.Atan2(b.rTopLeftX() - rTopLeftX(), b.rTopLeftY() - rTopLeftY());
//        }
//        public double rAngleTopLeftDeg(Ri b) {
//            return (rAngleTopLeftRad(b) * (180 / System.Math.PI));
//        }
//        public double rAngleTopRightRad(Ri b) {
//            return System.Math.Atan2(b.rTopRightX() - rTopRightX(), b.rTopRightY() - rTopRightY());
//        }
//        public double rAngleTopRightDeg(Ri b) {
//            return (rAngleTopRightRad(b) * (180 / System.Math.PI));
//        }
//        public double rAngleBottomLeftRad(Ri b) {
//            return System.Math.Atan2(b.rBottomLeftX() - rBottomLeftX(), b.rBottomLeftY() - rBottomLeftY());
//        }
//        public double rAngleBottomLeftDeg(Ri b) {
//            return (rAngleBottomLeftRad(b) * (180 / System.Math.PI));
//        }
//        public double rAngleBottomRightRad(Ri b) {
//            return System.Math.Atan2(b.rBottomRightX() - rBottomRightX(), b.rBottomRightY() - rBottomRightY());
//        }
//        public double rAngleBottomRightDeg(Ri b) {
//            return (rAngleBottomRightRad(b) * (180 / System.Math.PI));
//        }
//        public double rAngleCenRad(Ri b) {
//            return System.Math.Atan2(b.rCenX() - rCenX(), b.rCenY() - rCenY());
//        }
//        public double rAngleCenDeg(Ri b) {
//            return (rAngleCeRad(b) * (180 / System.Math.PI));
//        }

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
        public double AngleRad(V2i b) {
            return System.Math.Atan2(b.y - y, b.x - y);
        }
        public double AngleDeg(V2i b) {
            return (AngleRad(b) * (180 / System.Math.PI));
        }
    };
    public class V2f {
        public float x = 0.0f;
        public float y = 0.0f;
        public V2f(float x, float y) {
            this.x = x;
            this.y = y;
        }
        public double AngleRad(V2i b) {
            return System.Math.Atan2(b.y - y, b.x - y);
        }
        public double AngleDeg(V2i b) {
            return (AngleRad(b) * (180 / System.Math.PI));
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