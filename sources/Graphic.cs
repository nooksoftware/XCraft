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
    public class Graphic {
        public Texture2D texture;
        public Color color;

        public int x = 0;
        public int y = 0;
        public int w = 0;
        public int h = 0;

        public Graphic(Texture2D texture) {
            this.texture = texture;
            this.w = texture.Width;
            this.h = texture.Height;
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, new Rectangle(0,0,w,h), new Rectangle(x,y,w,h), color);
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y, int w, int h, Color c) {
            spriteBatch.Draw(texture, new Rectangle(0,0,this.w,this.h), new Rectangle(x,y,w,h), c);
        }

    }
}