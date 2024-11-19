
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
        public static Dictionary<int, bool> _AllocatedIDS;
        public static int last_id = 0;

        public static void Initialize() {
            _AllocatedIDS = new Dictionary<int, bool>();
        }
        public static int DetermineID() {
            if (_AllocatedIDS == null) {
                Initialize();
            }
            foreach (var v in _AllocatedIDS) {
                if (!v.Value) {
                    _AllocatedIDS[v.Key] = true;
                    return v.Key;
                }
            }
            last_id++;
            _AllocatedIDS.Add(last_id, true);
            return last_id;
        }
        public static void FreeID(int id) {
            if (_AllocatedIDS != null) {
                _AllocatedIDS[id] = false;
            }
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
            EntityIDDeterminer.DetermineID();
        }
        ~Entity() {
            EntityIDDeterminer.FreeID(id);
        }
        protected void DetermineID() {
            id = 0;
        }
    }
}