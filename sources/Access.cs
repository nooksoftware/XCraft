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
    
    public class Access {
        private static Access _instance;
        public static Access GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Access();
            }
            return _instance;
        }
       public static Access A {
            get {return GetInstance();}
       }
       public Zoom zoom;
       public GUISystem gui;
       public GraphicsSystem graphics;
       public AudioSystem audio;
       public NetworkSystem network;
       public GameplaySystem gameplay;

       public SpriteBatch sprite_batch;
       public GraphicsDeviceManager graphics_device_manager;

       public Dictionary<TileType, Vec2i> tp_positions;
       public const int tile_size = 32;
       public int window_width = 1280;
       public int window_height = 720;

       public Map current_map;
       public bool game_running = false;
       public bool menu_running = false;
    };  
}