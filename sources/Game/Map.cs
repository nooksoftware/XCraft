
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
            int map_tile_width,
            int map_tile_height,
            string map_title
        ) {
            this.map_tile_width = map_tile_width;            
            this.map_tile_height = map_tile_height;                
            this.map_title = map_title;        

            LoadDefaultGenMapSettings();
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
    public class MapGenerator {

    };
    public class MApAccess {
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
                    map_settings.map_tile_width, 
                    map_settings.map_tile_height, 
                    map_settings.map_title
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
            //if (x_begin_tile_render > map_settings.map_tile_width) {
//                x_begin_tile_render = map_settings.map_tile_width;
            //}
            //if (y_begin_tile_render > map_settings.map_tile_height ) {
//                y_begin_tile_render = map_settings.map_tile_height;
            //}
            //if (x_end_tile_render > map_settings.map_tile_width) {
//                x_end_tile_render = map_settings.map_tile_width;
            //}
            //if (y_end_tile_render > map_settings.map_tile_height ) {
//                y_end_tile_render = map_settings.map_tile_height;
            //}
        }
        public void DrawTitles(GameTime gameTime) {
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

            for (int x = 0; x < map_settings.map_tile_width; x++) {
                for (int y = 0; y < map_settings.map_tile_height; y++) {
                    Tile tile = tiles[x,y];
                    tile.Activity();
                }
            }
        }
    };
}