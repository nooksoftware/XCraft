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


using MVector2 = Microsoft.Xna.Framework.Vector2;
namespace XCraft {
    public class A {
        public Game1 g;
        public D d;
        public A (Game1 gm, D d) {
            this.g = g;
            this.d = d;
        }
        public int xM {
            get {return (d.ms.X);}
        }
        public int yM {
            get {return (d.ms.Y);}
        }
        public bool LMBHold() {
            return d.ms.LeftButton == ButtonState.Pressed;
        }
        public bool RMBHold() {
            return d.ms.RightButton == ButtonState.Pressed;
        }
        public bool OneClickedLMB() {
            return (
                d.ms.LeftButton == ButtonState.Pressed && d.p_ms.LeftButton == ButtonState.Released
            );
        }
        public bool OneClickedRMB() {
            return (
                d.ms.RightButton == ButtonState.Pressed && d.p_ms.RightButton == ButtonState.Released
            );
        }
        public bool OneReleasedLMB() {
            return (
                d.ms.LeftButton == ButtonState.Released && d.p_ms.LeftButton == ButtonState.Pressed
            );
        }
        public bool OneReleasedRMB() {
            return (
                d.ms.RightButton == ButtonState.Released && d.p_ms.RightButton == ButtonState.Pressed
            );
        }
        public bool OnePressedA() {return (d.ks.IsKeyDown(Keys.A) && d.p_ks.IsKeyUp(Keys.A));}
        public bool OnePressedD() {return (d.ks.IsKeyDown(Keys.D) && d.p_ks.IsKeyUp(Keys.D));}
        public bool OnePressedW() {return (d.ks.IsKeyDown(Keys.W) && d.p_ks.IsKeyUp(Keys.W));}
        public bool OnePressedS() {return (d.ks.IsKeyDown(Keys.S) && d.p_ks.IsKeyUp(Keys.S));}
        public bool OnePressedLeft() {return (d.ks.IsKeyDown(Keys.Left) && d.p_ks.IsKeyUp(Keys.Left));}
        public bool OnePressedRight() {return (d.ks.IsKeyDown(Keys.Right) && d.p_ks.IsKeyUp(Keys.Right));}
        public bool OnePressedUp() {return (d.ks.IsKeyDown(Keys.Up) && d.p_ks.IsKeyUp(Keys.Up));}
        public bool OnePressedDown() {return (d.ks.IsKeyDown(Keys.Down) && d.p_ks.IsKeyUp(Keys.Down));}

        public bool OneReleasedA() {return (d.ks.IsKeyUp(Keys.A) && d.p_ks.IsKeyDown(Keys.A));}
        public bool OneReleasedD() {return (d.ks.IsKeyUp(Keys.D) && d.p_ks.IsKeyDown(Keys.D));}
        public bool OneReleasedW() {return (d.ks.IsKeyUp(Keys.W) && d.p_ks.IsKeyDown(Keys.W));}
        public bool OneReleasedS() {return (d.ks.IsKeyUp(Keys.S) && d.p_ks.IsKeyDown(Keys.S));}
        public bool OneReleasedLeft() {return (d.ks.IsKeyUp(Keys.Left) && d.p_ks.IsKeyDown(Keys.Left));}
        public bool OneReleasedRight() {return (d.ks.IsKeyUp(Keys.Right) && d.p_ks.IsKeyDown(Keys.Right));}
        public bool OneReleasedUp() {return (d.ks.IsKeyUp(Keys.Up) && d.p_ks.IsKeyDown(Keys.Up));}
        public bool OneReleasedDown() {return (d.ks.IsKeyUp(Keys.Down) && d.p_ks.IsKeyDown(Keys.Down));}

        public bool AHold() {return (d.ks.IsKeyDown(Keys.A));}
        public bool DHold() {return (d.ks.IsKeyDown(Keys.D));}
        public bool WHold() {return (d.ks.IsKeyDown(Keys.W));}
        public bool SHold() {return (d.ks.IsKeyDown(Keys.S));}
        public bool LeftHold() {return (d.ks.IsKeyDown(Keys.Left));}
        public bool RightHold() {return (d.ks.IsKeyDown(Keys.Right));}
        public bool UpHold() {return (d.ks.IsKeyDown(Keys.Up));}
        public bool DownHold() {return (d.ks.IsKeyDown(Keys.Down));}

        public bool QHold() {return (d.ks.IsKeyDown(Keys.Q));}
        public bool EHold() {return (d.ks.IsKeyDown(Keys.E));}
        public bool RHold() {return (d.ks.IsKeyDown(Keys.R));}
        public bool THold() {return (d.ks.IsKeyDown(Keys.T));}
        public bool YHold() {return (d.ks.IsKeyDown(Keys.Y));}
        public bool UHold() {return (d.ks.IsKeyDown(Keys.U));}
        public bool IHold() {return (d.ks.IsKeyDown(Keys.I));}
        public bool OHold() {return (d.ks.IsKeyDown(Keys.O));}
        public bool PHold() {return (d.ks.IsKeyDown(Keys.P));}
        public bool FHold() {return (d.ks.IsKeyDown(Keys.F));}
        public bool GHold() {return (d.ks.IsKeyDown(Keys.G));}
        public bool HHold() {return (d.ks.IsKeyDown(Keys.H));}
        public bool JHold() {return (d.ks.IsKeyDown(Keys.J));}
        public bool KHold() {return (d.ks.IsKeyDown(Keys.K));}
        public bool LHold() {return (d.ks.IsKeyDown(Keys.L));}
        public bool ZHold() {return (d.ks.IsKeyDown(Keys.Z));}
        public bool XHold() {return (d.ks.IsKeyDown(Keys.X));}
        public bool CHold() {return (d.ks.IsKeyDown(Keys.C));}
        public bool VHold() {return (d.ks.IsKeyDown(Keys.V));}
        public bool BHold() {return (d.ks.IsKeyDown(Keys.B));}
        public bool NHold() {return (d.ks.IsKeyDown(Keys.N));}
        public bool MHold() {return (d.ks.IsKeyDown(Keys.M));}
        public bool N0Hold() {return (d.ks.IsKeyDown(Keys.D0));}
        public bool N1Hold() {return (d.ks.IsKeyDown(Keys.D1));}
        public bool N2Hold() {return (d.ks.IsKeyDown(Keys.D2));}
        public bool N3Hold() {return (d.ks.IsKeyDown(Keys.D3));}
        public bool N4Hold() {return (d.ks.IsKeyDown(Keys.D4));}
        public bool N5Hold() {return (d.ks.IsKeyDown(Keys.D5));}
        public bool N6Hold() {return (d.ks.IsKeyDown(Keys.D6));}
        public bool N7Hold() {return (d.ks.IsKeyDown(Keys.D7));}
        public bool N8Hold() {return (d.ks.IsKeyDown(Keys.D8));}
        public bool N9Hold() {return (d.ks.IsKeyDown(Keys.D9));}
        public bool EscHold() {return (d.ks.IsKeyDown(Keys.Escape));}
        public bool LCtrlHold() {return (d.ks.IsKeyDown(Keys.LeftControl));}
        public bool RCtrlHold() {return (d.ks.IsKeyDown(Keys.RightControl));}
        public bool LAltHold() {return (d.ks.IsKeyDown(Keys.LeftAlt));}
        public bool RAltHold() {return (d.ks.IsKeyDown(Keys.RightAlt));}
        public bool LShiftHold() {return (d.ks.IsKeyDown(Keys.LeftShift));}
        public bool RShiftHold() {return (d.ks.IsKeyDown(Keys.RightShift));}
        public bool BackspaceHold() {return (d.ks.IsKeyDown(Keys.Back));}

        public bool OnePressedQ() {return (d.ks.IsKeyDown(Keys.Q) && d.p_ks.IsKeyUp(Keys.Q));}
        public bool OnePressedE() {return (d.ks.IsKeyDown(Keys.E) && d.p_ks.IsKeyUp(Keys.E));}
        public bool OnePressedR() {return (d.ks.IsKeyDown(Keys.R) && d.p_ks.IsKeyUp(Keys.R));}
        public bool OnePressedT() {return (d.ks.IsKeyDown(Keys.T) && d.p_ks.IsKeyUp(Keys.T));}
        public bool OnePressedY() {return (d.ks.IsKeyDown(Keys.Y) && d.p_ks.IsKeyUp(Keys.Y));}
        public bool OnePressedU() {return (d.ks.IsKeyDown(Keys.U) && d.p_ks.IsKeyUp(Keys.U));}
        public bool OnePressedI() {return (d.ks.IsKeyDown(Keys.I) && d.p_ks.IsKeyUp(Keys.I));}
        public bool OnePressedO() {return (d.ks.IsKeyDown(Keys.O) && d.p_ks.IsKeyUp(Keys.O));}
        public bool OnePressedP() {return (d.ks.IsKeyDown(Keys.P) && d.p_ks.IsKeyUp(Keys.P));}
        public bool OnePressedF() {return (d.ks.IsKeyDown(Keys.F) && d.p_ks.IsKeyUp(Keys.F));}
        public bool OnePressedG() {return (d.ks.IsKeyDown(Keys.G) && d.p_ks.IsKeyUp(Keys.G));}
        public bool OnePressedH() {return (d.ks.IsKeyDown(Keys.H) && d.p_ks.IsKeyUp(Keys.H));}
        public bool OnePressedJ() {return (d.ks.IsKeyDown(Keys.J) && d.p_ks.IsKeyUp(Keys.J));}
        public bool OnePressedK() {return (d.ks.IsKeyDown(Keys.K) && d.p_ks.IsKeyUp(Keys.K));}
        public bool OnePressedL() {return (d.ks.IsKeyDown(Keys.L) && d.p_ks.IsKeyUp(Keys.L));}
        public bool OnePressedZ() {return (d.ks.IsKeyDown(Keys.Z) && d.p_ks.IsKeyUp(Keys.Z));}
        public bool OnePressedX() {return (d.ks.IsKeyDown(Keys.X) && d.p_ks.IsKeyUp(Keys.X));}
        public bool OnePressedC() {return (d.ks.IsKeyDown(Keys.C) && d.p_ks.IsKeyUp(Keys.C));}
        public bool OnePressedV() {return (d.ks.IsKeyDown(Keys.V) && d.p_ks.IsKeyUp(Keys.V));}
        public bool OnePressedB() {return (d.ks.IsKeyDown(Keys.B) && d.p_ks.IsKeyUp(Keys.B));}
        public bool OnePressedN() {return (d.ks.IsKeyDown(Keys.N) && d.p_ks.IsKeyUp(Keys.N));}
        public bool OnePressedM() {return (d.ks.IsKeyDown(Keys.M) && d.p_ks.IsKeyUp(Keys.M));}
        public bool OnePressed0() {return (d.ks.IsKeyDown(Keys.D0) && d.p_ks.IsKeyUp(Keys.D0));}
        public bool OnePressed1() {return (d.ks.IsKeyDown(Keys.D1) && d.p_ks.IsKeyUp(Keys.D1));}
        public bool OnePressed2() {return (d.ks.IsKeyDown(Keys.D2) && d.p_ks.IsKeyUp(Keys.D2));}
        public bool OnePressed3() {return (d.ks.IsKeyDown(Keys.D3) && d.p_ks.IsKeyUp(Keys.D3));}
        public bool OnePressed4() {return (d.ks.IsKeyDown(Keys.D4) && d.p_ks.IsKeyUp(Keys.D4));}
        public bool OnePressed5() {return (d.ks.IsKeyDown(Keys.D5) && d.p_ks.IsKeyUp(Keys.D5));}
        public bool OnePressed6() {return (d.ks.IsKeyDown(Keys.D6) && d.p_ks.IsKeyUp(Keys.D6));}
        public bool OnePressed7() {return (d.ks.IsKeyDown(Keys.D7) && d.p_ks.IsKeyUp(Keys.D7));}
        public bool OnePressed8() {return (d.ks.IsKeyDown(Keys.D8) && d.p_ks.IsKeyUp(Keys.D8));}
        public bool OnePressed9() {return (d.ks.IsKeyDown(Keys.D9) && d.p_ks.IsKeyUp(Keys.D9));}
        public bool OnePressedEsc() {return (d.ks.IsKeyDown(Keys.Escape) && d.p_ks.IsKeyUp(Keys.Escape));}
        public bool OnePressedLCtrl() {return (d.ks.IsKeyDown(Keys.LeftControl) && d.p_ks.IsKeyUp(Keys.LeftControl));}
        public bool OnePressedRCtrl() {return (d.ks.IsKeyDown(Keys.RightControl) && d.p_ks.IsKeyUp(Keys.RightControl));}
        public bool OnePressedLAlt() {return (d.ks.IsKeyDown(Keys.LeftAlt) && d.p_ks.IsKeyUp(Keys.LeftAlt));}
        public bool OnePressedRAlt() {return (d.ks.IsKeyDown(Keys.RightAlt) && d.p_ks.IsKeyUp(Keys.RightAlt));}
        public bool OnePressedLShift() {return (d.ks.IsKeyDown(Keys.LeftShift) && d.p_ks.IsKeyUp(Keys.LeftShift));}
        public bool OnePressedRShift() {return (d.ks.IsKeyDown(Keys.RightShift) && d.p_ks.IsKeyUp(Keys.RightShift));}
        public bool OnePressedBackspace() {return (d.ks.IsKeyDown(Keys.Back) && d.p_ks.IsKeyUp(Keys.Back));}

        public bool OneReleasedQ() {return (d.ks.IsKeyUp(Keys.Q) && d.p_ks.IsKeyDown(Keys.Q));}
        public bool OneReleasedE() {return (d.ks.IsKeyUp(Keys.E) && d.p_ks.IsKeyDown(Keys.E));}
        public bool OneReleasedR() {return (d.ks.IsKeyUp(Keys.R) && d.p_ks.IsKeyDown(Keys.R));}
        public bool OneReleasedT() {return (d.ks.IsKeyUp(Keys.T) && d.p_ks.IsKeyDown(Keys.T));}
        public bool OneReleasedY() {return (d.ks.IsKeyUp(Keys.Y) && d.p_ks.IsKeyDown(Keys.Y));}
        public bool OneReleasedU() {return (d.ks.IsKeyUp(Keys.U) && d.p_ks.IsKeyDown(Keys.U));}
        public bool OneReleasedI() {return (d.ks.IsKeyUp(Keys.I) && d.p_ks.IsKeyDown(Keys.I));}
        public bool OneReleasedO() {return (d.ks.IsKeyUp(Keys.O) && d.p_ks.IsKeyDown(Keys.O));}
        public bool OneReleasedP() {return (d.ks.IsKeyUp(Keys.P) && d.p_ks.IsKeyDown(Keys.P));}
        public bool OneReleasedF() {return (d.ks.IsKeyUp(Keys.F) && d.p_ks.IsKeyDown(Keys.F));}
        public bool OneReleasedG() {return (d.ks.IsKeyUp(Keys.G) && d.p_ks.IsKeyDown(Keys.G));}
        public bool OneReleasedH() {return (d.ks.IsKeyUp(Keys.H) && d.p_ks.IsKeyDown(Keys.H));}
        public bool OneReleasedJ() {return (d.ks.IsKeyUp(Keys.J) && d.p_ks.IsKeyDown(Keys.J));}
        public bool OneReleasedK() {return (d.ks.IsKeyUp(Keys.K) && d.p_ks.IsKeyDown(Keys.K));}
        public bool OneReleasedL() {return (d.ks.IsKeyUp(Keys.L) && d.p_ks.IsKeyDown(Keys.L));}
        public bool OneReleasedZ() {return (d.ks.IsKeyUp(Keys.Z) && d.p_ks.IsKeyDown(Keys.Z));}
        public bool OneReleasedX() {return (d.ks.IsKeyUp(Keys.X) && d.p_ks.IsKeyDown(Keys.X));}
        public bool OneReleasedC() {return (d.ks.IsKeyUp(Keys.C) && d.p_ks.IsKeyDown(Keys.C));}
        public bool OneReleasedV() {return (d.ks.IsKeyUp(Keys.V) && d.p_ks.IsKeyDown(Keys.V));}
        public bool OneReleasedB() {return (d.ks.IsKeyUp(Keys.B) && d.p_ks.IsKeyDown(Keys.B));}
        public bool OneReleasedN() {return (d.ks.IsKeyUp(Keys.N) && d.p_ks.IsKeyDown(Keys.N));}
        public bool OneReleasedM() {return (d.ks.IsKeyUp(Keys.M) && d.p_ks.IsKeyDown(Keys.M));}
        public bool OneReleased0() {return (d.ks.IsKeyUp(Keys.D0) && d.p_ks.IsKeyDown(Keys.D0));}
        public bool OneReleased1() {return (d.ks.IsKeyUp(Keys.D1) && d.p_ks.IsKeyDown(Keys.D1));}
        public bool OneReleased2() {return (d.ks.IsKeyUp(Keys.D2) && d.p_ks.IsKeyDown(Keys.D2));}
        public bool OneReleased3() {return (d.ks.IsKeyUp(Keys.D3) && d.p_ks.IsKeyDown(Keys.D3));}
        public bool OneReleased4() {return (d.ks.IsKeyUp(Keys.D4) && d.p_ks.IsKeyDown(Keys.D4));}
        public bool OneReleased5() {return (d.ks.IsKeyUp(Keys.D5) && d.p_ks.IsKeyDown(Keys.D5));}
        public bool OneReleased6() {return (d.ks.IsKeyUp(Keys.D6) && d.p_ks.IsKeyDown(Keys.D6));}
        public bool OneReleased7() {return (d.ks.IsKeyUp(Keys.D7) && d.p_ks.IsKeyDown(Keys.D7));}
        public bool OneReleased8() {return (d.ks.IsKeyUp(Keys.D8) && d.p_ks.IsKeyDown(Keys.D8));}
        public bool OneReleased9() {return (d.ks.IsKeyUp(Keys.D9) && d.p_ks.IsKeyDown(Keys.D9));}
        public bool OneReleasedEsc() {return (d.ks.IsKeyUp(Keys.Escape) && d.p_ks.IsKeyDown(Keys.Escape));}
        public bool OneReleasedLCtrl() {return (d.ks.IsKeyUp(Keys.LeftControl) && d.p_ks.IsKeyDown(Keys.LeftControl));}
        public bool OneReleasedRCtrl() {return (d.ks.IsKeyUp(Keys.RightControl) && d.p_ks.IsKeyDown(Keys.RightControl));}
        public bool OneReleasedLAlt() {return (d.ks.IsKeyUp(Keys.LeftShift) && d.p_ks.IsKeyDown(Keys.LeftShift));}
        public bool OneReleasedRAlt() {return (d.ks.IsKeyUp(Keys.RightShift) && d.p_ks.IsKeyDown(Keys.RightShift));}
        public bool OneReleasedLShift() {return (d.ks.IsKeyUp(Keys.LeftAlt) && d.p_ks.IsKeyDown(Keys.LeftAlt));}
        public bool OneReleasedRShift() {return (d.ks.IsKeyUp(Keys.RightAlt) && d.p_ks.IsKeyDown(Keys.RightAlt));}
        public bool OneReleasedBackspace() {return (d.ks.IsKeyUp(Keys.Back) && d.p_ks.IsKeyDown(Keys.Back));}
    
        public Texture2D pixel;

        public void DrawRectangleOutline1px(SpriteBatch spriteBatch, int x, int y, int w, int h, Color outline_color) {
            spriteBatch.Draw(pixel, new Rectangle(x, y, w, 1), outline_color); // Top
            spriteBatch.Draw(pixel, new Rectangle(x, y + h - 1, w, 1), outline_color); // Bottom
            spriteBatch.Draw(pixel, new Rectangle(x, y, 1, h), outline_color); // Left
            spriteBatch.Draw(pixel, new Rectangle(x + w - 1, y, 1, h), outline_color); // Right
        }
        public void DrawLineAtoB(SpriteBatch spriteBatch, float x, float y, float x2, float y2, Color line_color) {
            float d = MVector2.Distance(new MVector2(x, y), new MVector2(x2, y2));
            float ang = (float)Math.Atan2(y2 - y, x2 - x);
            spriteBatch.Draw(pixel, new MVector2(x,y), null, line_color, ang, MVector2.Zero, new MVector2(d, 1.0f), SpriteEffects.None, 0);
        }
        public void DrawRectangleFilledBG(SpriteBatch spriteBatch, int x, int y, int w, int h, Color bg_color) {
            spriteBatch.Draw(pixel, new Rectangle(x,y,w,h), bg_color);
        }
        public void DrawRectagleOutlineAndFilledBG(SpriteBatch spriteBatch, int x, int y, int w, int h, Color bg_color, Color outline_color) {
            DrawRectangleOutline1px(spriteBatch, x, y, w, h, outline_color);
            DrawRectangleFilledBG(spriteBatch, x, y, w, h, bg_color);
        }
        public void DrawCircleOutline1px(SpriteBatch spriteBatch, int x, int y, int radius, Color outline_color, int seg = 24) {
            float inc = MathHelper.TwoPi / seg;
            MVector2 center = new MVector2(x,y);
            MVector2 prev = center + radius * new MVector2((float)Math.Cos(0), (float)Math.Sin(0));

            for (int i = 1; i <= seg; i++)
            {
                float angle = i * inc;
                MVector2 next = center + radius * new MVector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                DrawLineAtoB(spriteBatch, prev.X, prev.Y, next.X, next.Y, outline_color);
                prev = next;
            }
        }
        public void DrawCircleFilledBG(SpriteBatch spriteBatch, int xp, int yp, float radius, Color bg_color) {
            MVector2 center = new MVector2(xp, yp);
            for (int y = -(int)radius; y <= radius; y++)
            {
                for (int x = -(int)radius; x <= radius; x++)
                {
                    if (x * x + y * y <= radius * radius)
                    {
                        spriteBatch.Draw(pixel, center + new MVector2(x, y), bg_color);
                    }
                }
            }
        }
        public void DrawCircleFilledBGandOutline1px(SpriteBatch spriteBatch, int x, int y, int anglesize, Color bg_color, Color outline_color) {
            DrawCircleFilledBG(spriteBatch, x, y, anglesize, bg_color);
            DrawCircleOutline1px(spriteBatch, x, y, anglesize, outline_color);
        }
        public int spriteBatchS = 2; // 0 - begin, 1 - end, 2 - not
        public bool IsSpriteBatchBegin() {
            return spriteBatchS == 0;
        }
        public bool IsSpriteBatchEnd() {
            return spriteBatchS == 1;
        }
        public void SpriteBatchBegin(SpriteBatch spriteBatch) {
            if (spriteBatchS == 1) {
                spriteBatch.Begin();
            } else if (spriteBatchS == 2) {
                spriteBatch.Begin();
            } else {
                string state = "";
                if (spriteBatchS == 2) {
                    state = "none";
                } else if (spriteBatchS == 1) {
                    state = "End()";
                } else if (spriteBatchS == 0) {
                    state = "Begin()";
                } 
                throw new Exception("Inappropriate SpriteBatchBegin(), spriteBatchS is at " + state);
            }
        }
        public void SpriteBatchEnd(SpriteBatch spriteBatch) {
            if (spriteBatchS == 0) {
                spriteBatch.End();
            } else {
                string state = "";
                if (spriteBatchS == 2) {
                    state = "none";
                } else if (spriteBatchS == 1) {
                    state = "End()";
                } else if (spriteBatchS == 0) {
                    state = "Begin()";
                } 
                throw new Exception("Inappropriate SpriteBatchBegin(), spriteBatchS is at " + state);
            }
        }
        public void DrawTex2DMotionBlur(Texture2D t, int x, int y, SpriteBatch spriteBatch) {
            Effect motionblur = d.Eff("MotionBlur");
        
            motionblur.Parameters["motionDirection"].SetValue(new MVector2(1,1));
            motionblur.Parameters["blurStrength"].SetValue(10.0f);

            if (IsSpriteBatchBegin()) {
                SpriteBatchEnd(spriteBatch);
            }
            spriteBatch.Begin(effect: motionblur);
            spriteBatch.Draw(t, new MVector2(x,y), Color.White);
            spriteBatch.End();
        }
    };
}