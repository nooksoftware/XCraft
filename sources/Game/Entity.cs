
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
    public static class EntityIDDeterminer {
        public Dictonary<int, bool> _AllocatedIDS;
        public int last_id = 0;

        public int DetermineID() {
            foreach (var v in _AllocatedIDS) {
                if (!v.Value) {
                    v.Value = true;
                    return v.Key;
                }
            }
            last_id++;
            v.Add(last_id, true);
            return last_id;
        }
        public void FreeID(int id) {
            _AllocatedIDS[id] = false;
        }
    }
    public class Player : Entity {
        public Player() {

        }
    };
    public class Entity {
        public int id = -1;
        public string label;

        public Entity() {
            DetermineID();
        }
        ~Entity() {
            FreeID(id);
        }
        protected void DetermineID() {
            id = 0;
        }
    }
}