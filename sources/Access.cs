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
using System.Security.Cryptography.X509Certificates;


namespace XCraft {
    public class A {
        public bool isSpectator = true;
        public bool isPlayer = false;

        public bool IsPlayer() {
            return isPlayer;
        }
        public bool IsSpectator() {
            return isSpectator;
        }

        public bool LeftMoveKeyboardHold() {
            return AHold() || LeftHold();
        }
        public bool RightMoveKeyboardHold() {
            return DHold() || RightHold();            
        }
        public bool UpMoveKeyboardHold() {
            return WHold() || UpHold();            
        }
        public bool DownMoveKeyboardHold() {
            return SHold() || DownHold();                
        }
        public bool ShiftHold() {
            return LShiftHold() || RShiftHold();
        }
        protected int previousMouseWheelValue = 0;

        public int MouseScrollChange() {
            previousMouseWheelValue = ms.ScrollWheelValue - previousMouseWheelValue;
            return previousMouseWheelValue;
        }
        public void PostTick() {
            previousMouseWheelValue = ms.ScrollWheelValue;
        }

        public readonly int windowWidth = 1280;
        public readonly int windowHeight = 720;

        public Dictionary<TT, V2i> tppos;
        public Dictionary<string, Texture2D> textures;
        public Dictionary<string, SpriteFont> fonts;
        public Dictionary<string, Effect> effects;
        public VarShare varShare;

        public bool WithinScreenBounds(Rectangle r, int xOffset, int yOffset, int x2Offset, int y2Offset) {
            return (r.X >= 0 + xOffset && r.X + r.Width <= windowWidth + x2Offset) && 
                    (r.Y >= 0 + yOffset && r.Y + r.Height <= windowHeight + y2Offset);
        }

        public Texture2D tp;

        public Game1 game;

        public M m;

        public N n;



        //public GUI gui;


        public readonly int tileSize = 16;

        public MouseState ms;
        public MouseState p_ms;
        public KeyboardState ks;
        public KeyboardState p_ks;
        
        public bool spectator = true;

        public int mW = 512;

        public int mH = 256;


        public bool serverGame = false;

        public readonly int def_mW = 512;
        public readonly int def_mH = 256;
        public readonly int def_wW = 1280;
        public readonly int def_wH = 720;
        public readonly int def_wW2 = 1600;
        public readonly int def_wH2 = 900;


        public A(Game1 game) {
            tppos = new Dictionary<TT, V2i>();
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            effects = new Dictionary<string, Effect>();
            varShare = new VarShare();
            n = new N(this);

            LoadDefTpPos();
        }
        public void Eff(string id, Effect f) {
            effects.Add(id, f);
        }
        public Effect Eff(string id) {
            Effect f;
            effects.TryGetValue(id, out f);
            return f;
        }        
        public void Fon(string id, SpriteFont f) {
            fonts.Add(id, f);
        }
        public SpriteFont Fon(string id) {
            SpriteFont f;
            fonts.TryGetValue(id, out f);
            return f;
        }        
        public void Tex(string id, Texture2D tex) {
            textures.Add(id, tex);
        }
        public Texture2D Tex(string id) {
            Texture2D t;
            textures.TryGetValue(id, out t);
            return t;
        }
        public void LoadDefTpPos() {
            LoadTPPos(TT.STONE, 0, 0);
            LoadTPPos(TT.ROUGHSTONE, 1, 0);
            LoadTPPos(TT.BEDROCK, 2, 0);
            LoadTPPos(TT.COBBLESTONE, 3, 0);
            LoadTPPos(TT.CLEARSTONE, 4, 0);
            LoadTPPos(TT.SAND, 5, 0);
            LoadTPPos(TT.CLAY, 6, 0);
            LoadTPPos(TT.WATER, 7, 0);
            LoadTPPos(TT.IRON_ORE, 0, 1);
            LoadTPPos(TT.GOLD_ORE, 1, 1);
            LoadTPPos(TT.DIA_ORE, 2, 1);
            LoadTPPos(TT.ORE1, 3, 1);
            LoadTPPos(TT.ORE2, 4, 1);
            LoadTPPos(TT.ORE3, 5, 1);
            LoadTPPos(TT.DIRT, 0, 2);
            LoadTPPos(TT.GRASS, 1, 2);
            LoadTPPos(TT.WOOD1, 2, 2);
            LoadTPPos(TT.WOOD2, 3, 2);
            LoadTPPos(TT.LEAVES1, 4, 2);
            LoadTPPos(TT.LEAVES2, 5, 2);
            LoadTPPos(TT.PLANKS1, 0, 3);
            LoadTPPos(TT.PLANKS2, 1, 3);
            LoadTPPos(TT.PLANKS3, 2, 3);
            LoadTPPos(TT.BRICKS, 3, 3);
            LoadTPPos(TT.COBBLESTONE_BRICKS, 4, 3);
            LoadTPPos(TT.PLANKS1_B, 0, 4);
            LoadTPPos(TT.PLANKS2_B, 1, 4);
            LoadTPPos(TT.PLANKS3_B, 2, 4);

            LoadTPPos(TT.WOODEN_VERTICAL, 6, 2);
            LoadTPPos(TT.WOODEN_HORIZONTAL, 7, 2);
            LoadTPPos(TT.WOODEN_BOXAL, 8, 2);
            LoadTPPos(TT.STONE_VERTICAL, 6, 3);
            LoadTPPos(TT.STONE_HORIZONTAL, 7, 3);
            LoadTPPos(TT.STONE_BOXAL, 8, 3);
            LoadTPPos(TT.IRON_VERTICAL, 6, 4);
            LoadTPPos(TT.IRON_HORIZONTAL, 7, 4);
            LoadTPPos(TT.IRON_BOXAL, 8, 4);
            
            LoadTPPos(TT.WOODEN_DOOR_TOP, 6, 5);
            LoadTPPos(TT.STONE_DOOR_TOP, 7, 5);
            LoadTPPos(TT.IRON_DOOR_TOP, 8, 5);

            LoadTPPos(TT.WOODEN_DOOR, 6, 6);
            LoadTPPos(TT.STONE_DOOR, 7, 6);
            LoadTPPos(TT.IRON_DOOR, 8, 6);

            LoadTPPos(TT.WOODEN_DOOR_END, 6, 7);
            LoadTPPos(TT.STONE_DOOR_END, 7, 7);
            LoadTPPos(TT.IRON_DOOR_END, 8, 7);

            //LoadTPPos(TT.WOODEN_VERTICAL, 6, 8);
            //LoadTPPos(TT.WOODEN_HORIZONTAL, 7, 8);
            //LoadTPPos(TT.WOODEN_BOXAL, 8, 8);

           // LoadTPPos(TT.WOODEN_VERTICAL, 6, 9);
           // LoadTPPos(TT.WOODEN_HORIZONTAL, 7, 9);
           // LoadTPPos(TT.WOODEN_BOXAL, 8, 9);

            //LoadTPPos(TT.WOODEN_VERTICAL, 6, 10);
            //LoadTPPos(TT.WOODEN_HORIZONTAL, 7, 10);
            //LoadTPPos(TT.WOODEN_BOXAL, 8, 10);

            //LoadTPPos(TT.WOODEN_VERTICAL, 6, 11);
            //LoadTPPos(TT.WOODEN_HORIZONTAL, 7, 11);
            //LoadTPPos(TT.WOODEN_BOXAL, 8, 11);


/*
            LoadTPPos(TT.DIRT, 0, 0);
            LoadTPPos(TT.GRASS, 1, 0);
            LoadTPPos(TT.STONE, 2, 0);
            LoadTPPos(TT.BEDROCK, 3, 0);
            LoadTPPos(TT.SAND, 4, 0);
            LoadTPPos(TT.CLAY, 5, 0);
            LoadTPPos(TT.WATER, 6, 0);
            LoadTPPos(TT.MUD, 7, 0);
            LoadTPPos(TT.LOG1, 0, 1);
            LoadTPPos(TT.LOG2, 1, 1);
            LoadTPPos(TT.LOG3, 2, 1);
            LoadTPPos(TT.LEAVES1, 3, 1);
            LoadTPPos(TT.LEAVES2, 4, 1);
            LoadTPPos(TT.LEAVES3, 5, 1);
            LoadTPPos(TT.WOODEN_PL1, 0, 2);
            LoadTPPos(TT.WOODEN_PL2, 1, 2);
            LoadTPPos(TT.WOODEN_PL3, 2, 2);
            LoadTPPos(TT.WOOD1, 3, 2);
            LoadTPPos(TT.WOOD2, 4, 2);
            LoadTPPos(TT.WOOD3, 5, 2);
            LoadTPPos(TT.BRICKS, 0, 3);
            LoadTPPos(TT.CONCRETE, 1, 3);
            LoadTPPos(TT.STONE_BRICKS, 2, 3);
            LoadTPPos(TT.METAL, 3, 3);
            LoadTPPos(TT.WOODEN_BOX, 4, 3);
            LoadTPPos(TT.METAL_BOX, 5, 3);
            LoadTPPos(TT.IRON_ORE, 0, 4);
            LoadTPPos(TT.GOLD_ORE, 1, 4);
            LoadTPPos(TT.DIA_ORE, 2, 4);
            LoadTPPos(TT.COBBLESTONE, 3, 4);

            LoadTPPos(TT.ORE1, 0, 5);
            LoadTPPos(TT.ORE2, 1, 5);
            LoadTPPos(TT.ORE3, 2, 5);
*/
        }
        protected void LoadTPPos(TT tt, int x, int y) {
            tppos.Add(tt, new V2i(x,y));
        }
        public int clickTimerLMB = 0;
        public int clickTimerRMB = 0;
        public int keyTimer = 0;

        public readonly int constantTimerOutFramesLimit = 15;
        public readonly int constantTimerOutFramesLimit2 = 5;

        public void Tick() {
            
            if (clickTimerLMB < 0) {
                clickTimerLMB = 0;
            } else {
                clickTimerLMB--;
            }
            if (clickTimerRMB < 0) {
                clickTimerRMB = 0;
            } else {
                clickTimerRMB--;
            }

            if (keyTimer < 0) {
                keyTimer = 0;
            } else {
                keyTimer--;
            }

            p_ms = ms;
            ms = Mouse.GetState();
            p_ks = ks;
            ks = Keyboard.GetState();

        }
        public bool LMBHold() {
            return ms.LeftButton == ButtonState.Pressed;
        }
        public bool RMBHold() {
            return ms.RightButton == ButtonState.Pressed;
        }
        public bool OneClickedLMB() {
            clickTimerLMB = constantTimerOutFramesLimit;
            return (
                ms.LeftButton == ButtonState.Pressed && p_ms.LeftButton == ButtonState.Released
            );
        }
        public bool OneClickedRMB() {
            clickTimerRMB = constantTimerOutFramesLimit;
            return (
                ms.RightButton == ButtonState.Pressed && p_ms.RightButton == ButtonState.Released
            );
        }
        public bool OneReleasedLMB() {
            return (
                ms.LeftButton == ButtonState.Released && p_ms.LeftButton == ButtonState.Pressed
            );
        }
        public bool OneReleasedRMB() {
            return (
                ms.RightButton == ButtonState.Released && p_ms.RightButton == ButtonState.Pressed
            );
        }

        public bool OnePressedA() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
                return false;
                }
            return (ks.IsKeyDown(Keys.A) && p_ks.IsKeyUp(Keys.A));}
        public bool OnePressedD() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.D) && p_ks.IsKeyUp(Keys.D));}
        public bool OnePressedW() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.W) && p_ks.IsKeyUp(Keys.W));}
        public bool OnePressedS() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.S) && p_ks.IsKeyUp(Keys.S));}
        public bool OnePressedLeft() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.Left) && p_ks.IsKeyUp(Keys.Left));}
        public bool OnePressedRight() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.Right) && p_ks.IsKeyUp(Keys.Right));}
        public bool OnePressedUp() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.Up) && p_ks.IsKeyUp(Keys.Up));}
        public bool OnePressedDown() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyDown(Keys.Down) && p_ks.IsKeyUp(Keys.Down));}
        public bool OneReleasedA() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.A) && p_ks.IsKeyDown(Keys.A));}
        public bool OneReleasedD() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.D) && p_ks.IsKeyDown(Keys.D));}
        public bool OneReleasedW() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.W) && p_ks.IsKeyDown(Keys.W));}
        public bool OneReleasedS() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.S) && p_ks.IsKeyDown(Keys.S));}
        public bool OneReleasedLeft() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.Left) && p_ks.IsKeyDown(Keys.Left));}
        public bool OneReleasedRight() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.Right) && p_ks.IsKeyDown(Keys.Right));}
        public bool OneReleasedUp() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.Up) && p_ks.IsKeyDown(Keys.Up));}
        public bool OneReleasedDown() {
            keyTimer = constantTimerOutFramesLimit2;
            if (keyTimer != 0) {
            return false;
            }
            return (ks.IsKeyUp(Keys.Down) && p_ks.IsKeyDown(Keys.Down));}
    

        public bool AHold() {return (ks.IsKeyDown(Keys.A));}
        public bool DHold() {return (ks.IsKeyDown(Keys.D));}
        public bool WHold() {return (ks.IsKeyDown(Keys.W));}
        public bool SHold() {return (ks.IsKeyDown(Keys.S));}
        public bool LeftHold() {return (ks.IsKeyDown(Keys.Left));}
        public bool RightHold() {return (ks.IsKeyDown(Keys.Right));}
        public bool UpHold() {return (ks.IsKeyDown(Keys.Up));}
        public bool DownHold() {return (ks.IsKeyDown(Keys.Down));}

        public bool QHold() {return (ks.IsKeyDown(Keys.Q));}
        public bool EHold() {return (ks.IsKeyDown(Keys.E));}
        public bool RHold() {return (ks.IsKeyDown(Keys.R));}
        public bool THold() {return (ks.IsKeyDown(Keys.T));}
        public bool YHold() {return (ks.IsKeyDown(Keys.Y));}
        public bool UHold() {return (ks.IsKeyDown(Keys.U));}
        public bool IHold() {return (ks.IsKeyDown(Keys.I));}
        public bool OHold() {return (ks.IsKeyDown(Keys.O));}
        public bool PHold() {return (ks.IsKeyDown(Keys.P));}
        public bool FHold() {return (ks.IsKeyDown(Keys.F));}
        public bool GHold() {return (ks.IsKeyDown(Keys.G));}
        public bool HHold() {return (ks.IsKeyDown(Keys.H));}
        public bool JHold() {return (ks.IsKeyDown(Keys.J));}
        public bool KHold() {return (ks.IsKeyDown(Keys.K));}
        public bool LHold() {return (ks.IsKeyDown(Keys.L));}
        public bool ZHold() {return (ks.IsKeyDown(Keys.Z));}
        public bool XHold() {return (ks.IsKeyDown(Keys.X));}
        public bool CHold() {return (ks.IsKeyDown(Keys.C));}
        public bool VHold() {return (ks.IsKeyDown(Keys.V));}
        public bool BHold() {return (ks.IsKeyDown(Keys.B));}
        public bool NHold() {return (ks.IsKeyDown(Keys.N));}
        public bool MHold() {return (ks.IsKeyDown(Keys.M));}
        public bool N0Hold() {return (ks.IsKeyDown(Keys.D0));}
        public bool N1Hold() {return (ks.IsKeyDown(Keys.D1));}
        public bool N2Hold() {return (ks.IsKeyDown(Keys.D2));}
        public bool N3Hold() {return (ks.IsKeyDown(Keys.D3));}
        public bool N4Hold() {return (ks.IsKeyDown(Keys.D4));}
        public bool N5Hold() {return (ks.IsKeyDown(Keys.D5));}
        public bool N6Hold() {return (ks.IsKeyDown(Keys.D6));}
        public bool N7Hold() {return (ks.IsKeyDown(Keys.D7));}
        public bool N8Hold() {return (ks.IsKeyDown(Keys.D8));}
        public bool N9Hold() {return (ks.IsKeyDown(Keys.D9));}
        public bool EscHold() {return (ks.IsKeyDown(Keys.Escape));}
        public bool LCtrlHold() {return (ks.IsKeyDown(Keys.LeftControl));}
        public bool RCtrlHold() {return (ks.IsKeyDown(Keys.RightControl));}
        public bool LAltHold() {return (ks.IsKeyDown(Keys.LeftAlt));}
        public bool RAltHold() {return (ks.IsKeyDown(Keys.RightAlt));}
        public bool LShiftHold() {return (ks.IsKeyDown(Keys.LeftShift));}
        public bool RShiftHold() {return (ks.IsKeyDown(Keys.RightShift));}
        public bool BackspaceHold() {return (ks.IsKeyDown(Keys.Back));}
    

        public string ConvertKeyToStr(Keys key) {
            if (key >= Keys.A && key <= Keys.Z) {
                return ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift)
                    ? key.ToString()
                    : key.ToString().ToLower();
            }
            if (key >= Keys.D0 && key <= Keys.D9) {
                return (key - Keys.D0).ToString();
            }
            return string.Empty;
        }
        public Texture2D pixel;

    };
}