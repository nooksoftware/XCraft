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
    public class Player : E {
        public Player(A a, int x, int y) : base(a,x,y,ET.PLAYER) {

        }
        public void ThisPlayerNavigation() {
            if (a.LeftMoveKeyboardHold() && a.RightMoveKeyboardHold()) {
                //...
            } else if (a.LeftMoveKeyboardHold()) {
                if (FreeSpaceOnLeft()) {
                    MoveThisPlayerLeft();
                }
            } else if (a.RightMoveKeyboardHold()) {
                if (FreeSpaceOnRight()) {
                    MoveThisPlayerRight();
                }
            }
            if (a.OnePressedA() || a.OnePressedUp()) {
                if (IsOnGround()) {
                    JumpThisPlayer();
                }
            }
            
        }
        public bool FreeSpaceOnLeft() {
            return true;
        }
        public bool FreeSpaceOnRight() {
            return true;
        }
        public bool IsOnGround() {
            return true;
        }
        public void MoveThisPlayerLeft() {

        }
        public void MoveThisPlayerRight() {

        }
        public void JumpThisPlayer() {

        }
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }
        public override void Tick(SpriteBatch spriteBatch) {
            base.Tick(spriteBatch);
        }
    };
}