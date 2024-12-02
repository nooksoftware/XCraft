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

    //create AccT
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
    public class SBoundsi {
        public int ls = 0;
        public int rs = 0;
        public int ts = 0;
        public int bs = 0;
        public bool l = false;
        public bool r = false;
        public bool t = false;
        public bool b = false;
    };
    public class SBoundsf {
        public float ls = 0.0f;
        public float rs = 0.0f;
        public float ts = 0.0f;
        public float bs = 0.0f;
        public bool l = false;
        public bool r = false;
        public bool t = false;
        public bool b = false;
    };
    public class V2iPaths {
        Dictionary<int, V2i> points;
        
        
        public V2iPaths() {
            points = new Dictionary<int, V2i>();
        }


    };
    //Move to class A
    public class PrimGraphicS {
        Color fg;
        Color bg;
        int outline = 0;
    };
    public class UnivG {
        /*
        public static bool spriteBatchBegin(SpriteBatch sprB) {
            
        }
        public static bool spriteBatchEnd(SpriteBatch sprB) {

        }

        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, int x, int y, int x2, int y2) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, int x, int y, int w, int h) {}
        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, float x, float y, float x2, float y2) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, float x, float y, float w, float h) {}
        public static void DrawCircle(SpriteBatch sprB, PrimGraphicS s, int cx, int cy, int angleSize) {}

        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, Ti ti) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, Ti ti) {}
        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, Ti ti) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, Ti ti) {}
        public static void DrawCircle(SpriteBatch sprB, PrimGraphicS s, Ti ti) {}

        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, Tf tf) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, Tf tf) {}
        public static void DrawL(SpriteBatch sprB, PrimGraphicS s, Tf tf) {}
        public static void DrawRect(SpriteBatch sprB, PrimGraphicS s, Tf tf) {}
        public static void DrawCircle(SpriteBatch sprB, PrimGraphicS s, Tf tf) {}

        public static void DrawArrow(SpriteBatch sprB, PrimGraphicS s, int x, int y, int x2, int y2) {}
        public static void DrawArrow(SpriteBatch sprB, PrimGraphicS s, float x, float y, float x2, float y2) {}
        public static void DrawArrow(SpriteBatch sprB, PrimGraphicS s, V2i a, V2i b) {}
        public static void DrawArrow(SpriteBatch sprB, PrimGraphicS s, V2f a, V2f b) {}

        public static void DrawTex2D(SpriteBatch sprB, Texture2D t, Ti ti, Color color) {}
        public static void DrawTex2D(SpriteBatch sprB, Texture2D t, Tf tf, Color color) {}
        public static void DrawTex2D(SpriteBatch sprB, Texture2D t, Ti d, Ti o, Color color) {}
        public static void DrawTex2D(SpriteBatch sprB, Texture2D t, Tf d, Tf o, Color color) {}
        public static void DrawTex2D(SpriteBatch sprB, Texture2D t, Tf d, Tf o, Shader s, Color color) {}
    
        public static void DrawText(Spritebatch sprB, SpriteFont f, V2i p, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, V2f p, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, V2i p, Shader s, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, V2f p, Shader s, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, Ti ti, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, Tf tf, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, Ti ti, Shader s, Color color) {}
        public static void DrawText(Spritebatch sprB, SpriteFont f, Tf tf, Shader s, Color color) {}
        */
    };
    public class MathPhysics {
        public MathPhysics() {

        }
        /*
        public static double AngleRad(int x, int y, int x2, int y2) {}
        public static double AngleDeg(int x, int y, int x2, int y2) {}
        public static double AngleRad(float x, float y, float x2, float y2) {}
        public static double AngleDeg(float x, float y, float x2, float y2) {}
        public static V2f PtoPMoveVec(int x, int y, int x2, int y2, float a) {}
        public static V2f PtoPMoveVec(float x, float y, float x2, float y2, float a) {}
        public static V2i LinePtoP(int x, int y, int x2, int y2) {}
        public static V2f LinePtoP(float x, float y, float x2, float y2) {}
        public static V2i[] APathfind(V2iPaths paths) {}
        public static SBoundsi Bounds(Ti a, Ti b) {}
        public static SBoundsf Bounds(Tf a2, Tf b2) {}

        //water


        public static float[,] GenTerrainNoise(int w, int h) { //regex

        } 
        public static int[,] FloatToIntNoise(float[,] n, int w, int h, KeyValuePair<float, int> ind) {

        }
        */
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