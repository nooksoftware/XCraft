
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
    public enum OreType {
        NONE = 0,
        COAL,
        IRON,
        GOLD,
        DIAMONDS,
        LAMBDA,
        SIGMA,
        ALPHA
    };
    public enum BiomeType {
        BIOMETYPE_HILLS = 0,
        BIOMETYPE_FLAT,
        BIOMETYPE_CLIFFS,
        BIOMETYPE_LAKE_WATERS,
        BIOMETYPE_DESERT
    };
    public class MapLoadSettings {
        public string title;
        public string filepath;
        public int width = 0;
        public int height = 0;
        bool ready = false;

        public MapLoadSettings() {

        }
    };
    public class ThisGenMapLoadSettings : MapLoadSettings {
        public int _bedrock_layer_height = 4;
        public int _ground_layer_height = 64;
        public int _outside_terrain_ground_layer_height_variety = 32;
        public Dictionary<BiomeType, int> _biome_frequency;
        public Dictionary<BiomeType, KeyValuePair<int, int>> _biomes_x;
        public int _iron_ore_frequency = 1000;
        public int _coal_ore_frequency = 700;
        public int _gold_ore_frequency = 2000;
        public int _diamond_ore_frequency = 3500;

        public ThisGenMapLoadSettings(
            int width,
            int height,
            string title
        ) {
            this.width = width;            
            this.height = height;                
            this.title = title;        

            LoadDefaultGenMapSettings();
        }
        protected void LoadDefaultGenMapSettings() {

        }
    };
    public class FileMapLoadSettings : MapLoadSettings {

    };
    public enum MapGenerationType {
        GEN_MAP = 0,
        MAP_FILE_LOAD
    };
    public class FtoIIndex {

    };
    public class Noise {

    };
    public class MapGeneratorNoiseGen {

    };
    public class MapGenerator {
        private MapData _map_data;
        private MapAccess _map_access;
        private Map _map;
        private MapGeneratorNoiseGen _noise_gen;

        protected Random ore_random;

        public MapGenerationType GenType {
            get {return _gen_type;}
        }
        protected MapGenerationType _gen_type = MapGenerationType.GEN_MAP;
        protected ThisGenMapLoadSettings gen_map;

        public MapGenerator(ThisGenMapLoadSettings gen_map) {
            this.gen_map = gen_map;
            _gen_type = MapGenerationType.GEN_MAP;
        }
        public bool Generate(Map map) {
            Console.WriteLine("Pre-generation stage");

            ThisGenMapLoadSettings s = this.gen_map;
            
            MapData map_data = map.map_data;
            MapAccess map_access = map_data.map_access;
        
            _map_data = map_data;
            _map_access = map_access;
            _map = map;

            MapGeneratorNoiseGen noise_gen = new MapGeneratorNoiseGen();
            this._noise_gen = noise_gen;

            Console.WriteLine("Generating new map ("+map.map_settings.title+")");
            Console.WriteLine("---- Generating blank tile matrix");
            map_data.InitializeClearTilesMapFromMapSize(s.width, s.height);


            //Step: Generate basic terrain
            Console.WriteLine("---- Generating terrain");
            GenerateBasicTerrain();

            
            //Step: Generate Ores
            Console.WriteLine("---- Generating ores");
            GenerateOres();
            Console.WriteLine("Success!");
            return true;
        }
        public bool GenerateBasicTerrain() {
            //todo
            return true;
        }

        protected void GenerateOreArea(OreType[,] map, Vec2iKey pos, OreType ore, int amount) {
            //todo
        }

        protected void GenerateOre(OreType[,] map, OreType ore, int min, int max, int frequency, int width, int height) {
            //todo
        }

        public bool GenerateOres() {
            //todo
            return true;
        }

    };
    public class MapSettings {
        public MapSettings() {

        }
        public void LoadDefaultMapSettings() {
            width = 512;
            height = 128+64;
            load_settings = new ThisGenMapLoadSettings(width, height, "The_Defalt_Map");

            _ready = true;
        }
        protected bool _ready = false;
        public bool Ready {
            get {return _ready;}
        }
        public string title;
        public int width = 512;
        public int height = 128+64;

        public MapLoadSettings load_settings;
    }
    public class MapAccess {
        public MapAccess(MapData mapdata) {
            this.mapdata = mapdata;
        }
        protected MapData mapdata;

        public bool EmplaceEntity(Entity entity) {
            int id = entity.id;
            string label = entity.label;

            try {
                entities_by_id.Add(id, entity);
                entities_by_label.Add(label, entity);
            } catch (ArgumentException) {
                return false;
            }

            return true;
        }
        public bool RemoveEntity(Entity entity) {
            int id = entity.id;
            string label = entity.label;
            
            bool a = entities_by_id.Remove(id);
            bool a2= entities_by_label.Remove(label);

            return a && a2;
        }
        public Entity GetEntity(int id) {
            return entities_by_id[id];
        }
        public Entity GetEntity(string label) {
            return entities_by_label[label];
        }

        public Dictionary<int, Entity> entities_by_id;
        public Dictionary<string, Entity> entities_by_label;
    };
    public class MapData {
        protected Map parent;

        public Tile[,] tiles;
        public Entity[] entities;

        public MapAccess map_access;

        public MapData(Map parent) {
            this.parent = parent;
        }
        public Tile tile(int x, int y) {
            return tiles[x,y];
        }
        public void InitializeClearTilesMapFromMapSize(int w, int h) {
            tiles = new Tile[w,h];
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    //default tile
                    tiles[x,y] = new Tile(x,y,TileType.AIR);
                }
            }
        }
    };
    public class Map {
        public MapSettings map_settings;
        public MapData map_data;
        protected MapGenerator map_generator;

        protected Player this_player;

        public Player GetThisPlayer() {
            return this_player;
        }
        public Map() {
            map_settings = new MapSettings();

            map_data = new MapData(this);
        }
        public void LoadAsDefaultMap() {
            map_settings.LoadDefaultMapSettings();
            ThisGenMapLoadSettings load_settings 
                = new ThisGenMapLoadSettings(
                    map_settings.width, 
                    map_settings.height, 
                    map_settings.title
                );
            map_generator = new MapGenerator(load_settings);
            map_generator.Generate(this);

        }
        public void LoadEntities() {
            
        }
        public void Draw(GameTime gameTime) {
            //DetermineVisibleTiles(); //todo
            DrawTiles(gameTime);
        }
        protected int x_begin_tile_render = 0; //todo
        protected int y_begin_tile_render = 0; //todo
        protected int x_end_tile_render = 0; //todo
        protected int y_end_tile_render = 0; //todo
        public void DetermineVisibleTiles() {
            //double x_beginf = (double)(Zoom._cameraX-(Access.A.window_width/2))-((Zoom._cameraZoom)*(Access.A.window_width/2));
            //double y_beginf = (double)(Zoom._cameraY-(Access.A.window_height/2))-((Zoom._cameraZoom)*(Access.A.window_height/2));
            //int x_begin = System.Convert.ToInt32(x_beginf);
            //int y_begin = System.Convert.ToInt32(y_beginf);
            //int x_end = 
//                System.Convert.ToInt32(
//                (x_begin + (Access.A.window_width/2* (1.0f/Zoom._cameraZoom))));
            //int y_end = 
//                System.Convert.ToInt32(
//                (y_begin + (Access.A.window_height/2* (1.0f/Zoom._cameraZoom))));
//
            //int tile_size = 32;
            //x_begin_tile_render = x_begin / tile_size;
            //y_begin_tile_render = y_begin / tile_size;
            //x_end_tile_render = x_end / tile_size;
            //y_end_tile_render = y_end / tile_size;
//
            //if (x_begin_tile_render < 0) {
//                x_begin_tile_render = 0;
            //}
            //if (y_begin_tile_render < 0 ) {
//                y_begin_tile_render = 0;
            //}
            //if (x_end_tile_render < 0) {
//                x_end_tile_render = 0;
            //}
            //if (y_end_tile_render < 0 ) {
//                y_end_tile_render = 0;
            //}
            //if (x_begin_tile_render > map_settings.width) {
//                x_begin_tile_render = map_settings.width;
            //}
            //if (y_begin_tile_render > map_settings.height ) {
//                y_begin_tile_render = map_settings.height;
            //}
            //if (x_end_tile_render > map_settings.width) {
//                x_end_tile_render = map_settings.width;
            //}
            //if (y_end_tile_render > map_settings.height ) {
//                y_end_tile_render = map_settings.height;
            //}
        }
        public void DrawTiles(GameTime gameTime) {
            GameAccess gameaccess = GameAccess.A;
            Tile[,] tiles = map_data.tiles;

            for (int x = x_begin_tile_render; x < x_end_tile_render; x++) {
                for (int y = y_begin_tile_render; y < y_end_tile_render; y++) {
                    Tile tile = tiles[x,y];
                    tile.Draw();
                }
            }            
        }
        public void Activity(GameTime gameTime) {
            GameAccess gameaccess = GameAccess.A;
            Tile[,] tiles = map_data.tiles;

            for (int x = 0; x < map_settings.width; x++) {
                for (int y = 0; y < map_settings.height; y++) {
                    Tile tile = tiles[x,y];
                    tile.Activity();
                }
            }
        }
    };
}