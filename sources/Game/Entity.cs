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

namespace vgCSh {
    public class Player : Entity {
        public Player() {}

        
    };
    public class Entity {
        public int id = -1;
        public string label;

        public Entity() {
            DetermineID();
        }
        protected void DetermineID() {
            id = 0;
        }
    };  
}