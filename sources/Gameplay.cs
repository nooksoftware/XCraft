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
    public enum GameModeType {
        SKIRMISH = 1
    };
    public class GameStateSettings {
        public MapSettings map_settings;
        public GameModeType gamemode_type;
    };
    public class GameState {
        public GameState(GameStateSettings settings) {
            map = new Map();
        }

        public Map map;
    }
    public class GameplaySystem {
        public GameplaySystem() {

        }
        protected bool _ready = false;
        public bool Ready {
            get {return _ready;}
        }
        public Map current_map;

        public void NewDefaultMap() {
            current_map = new Map();
            current_map.LoadAsDefaultMap();
            current_map.LoadEntities();
            _ready = true;
        }
        public Player GetThisPlayer() {            
            return current_map.GetThisPlayer();            
        }
    };  
}