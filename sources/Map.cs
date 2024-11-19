using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
using System.Xml.Serialization;
//using FastNoiseLite;

namespace vgCSh {
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
        public string map_title;
        public string map_filepath;
        public int map_tile_width = 0;
        public int map_tile_height = 0;
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
        protected void LoadDefaultGenMapSettings() {

        }
    };
    public class FileMapLoadSettings : MapLoadSettings {

    };
    public enum MapGenerationType {
        GEN_MAP = 0,
        MAP_FILE_LOAD
    };
    public class NoiseFloatConvertToIntIndex {
        public NoiseFloatConvertToIntIndex() {
            _empty = true;
        }
        void Add(float begin_h, int i) {
            _empty = false;
            //todo //Indexes.(begin_h, i);
        }

        public Vector<KeyValuePair<float, int>> Indexes {
            get {return _indexes;}
            set {this._indexes = Indexes;}
        }
        public int FloatToInt(float v) {
            //todo;

            int vi = 0;
            return vi;
        }
        protected Vector<KeyValuePair<float, int>> _indexes;
        protected bool _empty = false;
        public bool Empty {
            get {return _empty;}
        }
    };
    public class Noise {
        private FastNoiseLite n;
        public FastNoiseLite N {
            get {return n;}
        }

        protected FastNoiseLite.NoiseType _noise_type;
        protected float _frequency;

        protected float[,] _data_f;
        protected int[,] _data_i;
        protected NoiseFloatConvertToIntIndex _f_to_i_index;

        public float[,] Dataf {
            get {return _data_f;}
        }
        public int[,] Datai {
            get {return _data_i;}
        }

        protected float _min = 0.0f;
        protected float _max = 1.0f;
        public float min {
            get {return _min;}
            set {this._min = min;}
        }
        public float max {
            get {return _max;}
            set {this._max = max;}
        }

        protected int _w = 512;
        protected int _h = 512;
        public int w {
            get {return _w;}
            set {_w = w;}
        }
        public int h {
            get {return _h;}
            set {_h = h;}
        }

        public Noise(int w, int h, float min, float max, FastNoiseLite.NoiseType noise_type, NoiseFloatConvertToIntIndex conv, float frequency = 0.01f) {
            this._w = w;
            this._h = h;
            this._min = min;
            this._max = max;
            this._noise_type = noise_type;
            this._frequency = frequency;
            n = new FastNoiseLite();
            n.SetNoiseType(noise_type);
            n.SetFrequency(frequency);

            _data_f = new float[w,h];
            _data_i = new int[w,h];
        }
        public void Generate() {
            Console.WriteLine("Noise.Generate(), h = "+$"{h}"+", w = "+$"{h}");
            try {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    
                    _data_f[x,y] = NoiseValue(x,y); //n.GetNoise(x,y);
                }
            }
            } catch (System.IndexOutOfRangeException e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Access out of bounds");
                Console.ForegroundColor = ConsoleColor.White;
                
            }

            if (_f_to_i_index != null) {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    float v = _data_f[x,y];
                    int v2 = _f_to_i_index.FloatToInt(v);
                    _data_i[x,y] = v2;
                }
            }
            }
        }
        public float NoiseValue(int x, int y) {
            float v = n.GetNoise(x,y);

            v = (v + 1.0f) / 2.0f;
            v = min + (v * (max - min));

            return v;
        }
        public static float Normalize(float value)
        {
            return Math.Clamp(value, 0.0f, 1.0f);
        }
        public bool NoiseAdd(Noise noise2, bool intupdate = false) {
            if (w == noise2.w && h == noise2.h) {

            } else {
                return false;
            }
            float[,] noise2_f = noise2.Dataf;
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    _data_f[x,y] = Normalize(_data_f[x,y] + noise2_f[x,y]);
                }
            }

            if (intupdate) {
                UpdateInts();
            }

            return true;
        }
        public bool NoiseSubtract(Noise noise2, bool intupdate = false) {
            if (w == noise2.w && h == noise2.h) {

            } else {
                return false;
            }
            float[,] noise2_f = noise2.Dataf;
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    _data_f[x,y] = Normalize(_data_f[x,y] - noise2_f[x,y]);
                }
            }

            if (intupdate) {
                UpdateInts();
            }

            return true;
        }
        public bool NoiseMultiply(Noise noise2, bool intupdate = false) {
            if (w == noise2.w && h == noise2.h) {

            } else {
                return false;
            }
            float[,] noise2_f = noise2.Dataf;
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    _data_f[x,y] = Normalize(_data_f[x,y] * noise2_f[x,y]);
                }
            }

            if (intupdate) {
                UpdateInts();
            }

            return true;
        }
        public bool NoiseDivide(Noise noise2, bool intupdate = false) {
            if (w == noise2.w && h == noise2.h) {

            } else {
                return false;
            }
            float[,] noise2_f = noise2.Dataf;
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    _data_f[x,y] = Normalize(_data_f[x,y] / (noise2_f[x, y] != 0 ? noise2_f[x, y] : 1.0f));
                }
            }

            if (intupdate) {
                UpdateInts();
            }

            return true;
        }
        public bool UpdateInts() {
            if (_f_to_i_index != null) {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    float v = _data_f[x,y];
                    int v2 = _f_to_i_index.FloatToInt(v);
                    _data_i[x,y] = v2;
                }
            }
            } else {
                return false;
            }
            return true;
        }
        public void Generate(int seed) {
            n.SetSeed(seed);
            Generate();
        }
    };
    public class MapGeneratorNoiseGen {
        public Dictionary<string, Noise> _raw_noises;

        public MapGeneratorNoiseGen() {
            _raw_noises = new Dictionary<string, Noise>();
        }
        public bool New(string noise_name, Noise noise) {
            _raw_noises.Add(noise_name, noise);
            return true;
        }
        public Noise Get(string noise_name) {
            return _raw_noises[noise_name];
        }
    };
    public class MapGenerator {
        private MapData _map_data;
        private MapAccess _map_access;
        private Map _map;
        private MapGeneratorNoiseGen _noise_gen;

        public MapGenerator(ThisGenMapLoadSettings gen_map) {
            this.gen_map = gen_map;
            _gen_type = MapGenerationType.GEN_MAP;
        }
        public bool Generate(Map map) {
            Console.WriteLine("Pre-generation stage");
            //if (_gen_type != MapGenerationType.GEN_MAP || gen_map == null) {
            //    return false;
            //}

            ThisGenMapLoadSettings s = this.gen_map;
            
            MapData map_data = map.map_data;
            MapAccess map_access = map_data.map_access;

            //Pre-step: nullptr checks
            //if (s == null) {
            //    return false;
            //}
            //if (map_data == null) {
            //    return false;
            //}
            //if (map_access == null) {
            //    return false;
            //}
            //if (map == null) {
            //    return false;
            //}
            _map_data = map_data;
            _map_access = map_access;
            _map = map;
            //TODO: Assertions


            
            MapGeneratorNoiseGen noise_gen = new MapGeneratorNoiseGen();
            this._noise_gen = noise_gen;

            Console.WriteLine("Generating new map ("+map.map_settings.map_title+")");
            //Step: Implement clear tile matrix with air tiles
            Console.WriteLine("---- Generating blank tile matrix");
            map_data.InitializeClearTilesMapFromMapSize(s.map_tile_width, s.map_tile_height);

            
            //Step: Generate basic terrain
            Console.WriteLine("---- Generating terrain");
            GenerateBasicTerrain();

            
            //Step: Generate Ores
            //Console.WriteLine("---- Generating ores");
            //GenerateOres();
            Console.WriteLine("Success!");
            return true;
        }
        public bool GenerateBasicTerrain() {
            MapData map_data = this._map_data;
            MapAccess map_access = this._map_access;
            Map map = this._map;
            MapGeneratorNoiseGen noise_gen = this._noise_gen;
            ThisGenMapLoadSettings s = this.gen_map;
            Random random = new Random();

            noise_gen.New(
                "terrain1",
                new Noise(
                    s.map_tile_width, s.map_tile_height,
                    0.0f, 1.0f,
                    FastNoiseLite.NoiseType.Perlin,
                    new NoiseFloatConvertToIntIndex(),
                    0.03f
                )
            ) ;
            Noise terrain1 = noise_gen.Get("terrain1");
            Noise cellular = new Noise(
                    s.map_tile_width, s.map_tile_height,
                    0.7f, 1.0f,
                    FastNoiseLite.NoiseType.Cellular,
                    new NoiseFloatConvertToIntIndex(),
                    0.05f
                );
            terrain1.Generate(0);
            cellular.Generate(0);
            terrain1.NoiseMultiply(cellular);
            noise_gen.New(
                "plain_outside_terrain1",
                new Noise(
                    s.map_tile_width, 1,
                    0.0f, 1.0f,
                    FastNoiseLite.NoiseType.Perlin,
                    new NoiseFloatConvertToIntIndex(),
                    0.1f
                )
            );
            Noise plain_outside_terrain_1 = noise_gen.Get("plain_outside_terrain1");
            plain_outside_terrain_1.Generate(5);

            int h_top_bedrock = s.map_tile_height - s._bedrock_layer_height;
            int h_bottom_bedrock = s.map_tile_height;

            int h_bottom_ground = h_bottom_bedrock - 1;
            int h_top_ground = h_bottom_ground - s._ground_layer_height;
            
            int _h_bottom_outside_terrain_ground = h_top_ground - 1;
            int _h_top_outside_terrain_ground = _h_bottom_outside_terrain_ground - s._outside_terrain_ground_layer_height_variety;

            int terrain_blocks = 0;
            for (int x = 0; x < s.map_tile_width; x++) {
                for (int y = h_top_bedrock; y < h_bottom_bedrock; y++) {
                    float bedrock_den = (float)y / (float)(h_bottom_bedrock - h_top_bedrock);

                    bedrock_den = Noise.Normalize(bedrock_den);

                    float random_f = 10.0f / (float)(random.Next(10));
                    if (bedrock_den-random_f > bedrock_den) {
                        map_data.tile(x,y).Set(TileType.BEDROCK);
                    } else {
                        map_data.tile(x,y).Set(TileType.STONE);
                    }
                    terrain_blocks++;
                }
            }


            for (int x = 0; x < s.map_tile_width; x++) {
                for (int y = h_top_ground; y < h_bottom_ground; y++) {
                    float v = terrain1.Dataf[x,y];
                    if (v < 0.2f) {
                        map_data.tile(x,y).Set(TileType.DIRT);
                    } else if (v < 0.4f) {
                        map_data.tile(x,y).Set(TileType.STONE);
                    } else if (v < 1.0f) {
                        map_data.tile(x,y).Set(TileType.STONE);
                    } else {
                        map_data.tile(x,y).Set(TileType.STONE);
                    }
                    terrain_blocks++;
                }
            }

            for (int x = 0; x < s.map_tile_width; x++) {
                for (int y = _h_top_outside_terrain_ground; y < _h_bottom_outside_terrain_ground; y++) {
                    int terrain_height = (y - _h_top_outside_terrain_ground);
                    int terrain_max_height = _h_bottom_outside_terrain_ground - _h_top_outside_terrain_ground;

                    float terrain_height_f = (float)terrain_height / (float)terrain_max_height;

                    if (terrain_height_f < plain_outside_terrain_1.Dataf[x,0]) {
                        map_data.tile(x,y).Set(TileType.DIRT);
                        terrain_blocks++;
                    }
                }
            }

            Console.WriteLine("Generated " +terrain_blocks+" terrain blocks");

            return true;
        }
        protected Random ore_random;
        protected void GenerateOreArea(OreType[,] map, Vec2iKey pos, OreType ore, int amount) {
            int area_size = (int)Math.Ceiling(Math.Sqrt(amount));
            int diff_x = area_size/2;
            int diff_y = area_size/2;

            Vec2i begin = new Vec2i(pos.X - diff_x, pos.Y - diff_y);
            Vec2i end = new Vec2i(begin.X + area_size, begin.Y + area_size);

            map[pos.X, pos.Y] = OreType.NONE;

            
            int area_amount = area_size * area_size;
            int inv_frequency = area_amount - amount;
            for (int x = begin.X; x < end.X; x++) {
                for (int y = begin.Y; y < end.Y; y++) {
                    map[x,y] = ore;
                }
            }

            for (int i = 0; i < inv_frequency; i++) {
                int x = begin.X + ore_random.Next(area_size);
                int y = begin.Y + ore_random.Next(area_size);
                if (map[x,y] == OreType.NONE) {
                    map[x,y] = OreType.NONE;
                } else {
                    i--;
                }
            }
        }
        protected void GenerateOre(OreType[,] map, OreType ore, int min, int max, int frequency, int width, int height) {
            
            Dictionary<Vec2iKey, OreType> points = new Dictionary<Vec2iKey, OreType>();

            for (int i = 0; i < frequency; i++) {
                int x = ore_random.Next(width);
                int y = ore_random.Next(height);
                if (map[x,y] == OreType.NONE) {
                    map[x,y] = ore;
                    points.Add(new Vec2iKey(x,y), ore);
                } else {
                    i--;
                }
            }
            foreach (var kvp in points) {
                Vec2iKey pos = kvp.Key;
                
                int amount = min+(ore_random.Next(max-min));

                GenerateOreArea(map, pos, ore, amount);
            }
            
        }
        public bool GenerateOres() {
            MapData map_data = this._map_data;
            MapAccess map_access = this._map_access;
            Map map = this._map;
            MapGeneratorNoiseGen noise_gen = this._noise_gen;
            ThisGenMapLoadSettings s = this.gen_map;
            Random random = new Random();
            ore_random = new Random();

            int h_top_bedrock = s.map_tile_height - s._bedrock_layer_height;
            int h_bottom_bedrock = s.map_tile_height;

            int h_bottom_ground = h_bottom_bedrock - 1;
            int h_top_ground = h_bottom_ground - s._ground_layer_height;
            
            int _h_bottom_outside_terrain_ground = h_top_ground - 1;
            int _h_top_outside_terrain_ground = _h_bottom_outside_terrain_ground - s._outside_terrain_ground_layer_height_variety;

            OreType[,] ore_gen_points = new OreType[s.map_tile_width, s.map_tile_height];
            for (int x = 0; x < s.map_tile_width; x++) {
                for (int y = 0; y < s.map_tile_height; y++) {
                    ore_gen_points[x,y] = OreType.NONE;
                }
            }

            int ground_size = (h_bottom_ground - h_top_ground) * s.map_tile_width;
            int coal_ores_amount = ground_size / s._coal_ore_frequency;
            int iron_ores_amount = ground_size / s._iron_ore_frequency;
            int gold_ores_amount = ground_size / s._gold_ore_frequency;
            int diamond_ores_amount = ground_size / s._diamond_ore_frequency;

            GenerateOre(ore_gen_points, OreType.COAL, 5, 15, coal_ores_amount, s.map_tile_width, s.map_tile_height);
            GenerateOre(ore_gen_points, OreType.IRON, 3, 10, iron_ores_amount, s.map_tile_width, s.map_tile_height);
            GenerateOre(ore_gen_points, OreType.GOLD, 3, 6, gold_ores_amount, s.map_tile_width, s.map_tile_height);
            GenerateOre(ore_gen_points, OreType.DIAMONDS, 1, 7, diamond_ores_amount, s.map_tile_width, s.map_tile_height);

            //for (int x = 0; x < s.map_tile_width; x++) {
            //    for (int y = _h_top_outside_terrain_ground; y < s.map_tile_height; y++) {
            //        
            //    }
            //}

            return true;
        }

        public MapGenerationType GenType {
            get {return _gen_type;}
        }
        protected MapGenerationType _gen_type = MapGenerationType.GEN_MAP;
        protected ThisGenMapLoadSettings gen_map;
    };
    public class MapSettings {
        public MapSettings() {

        }
        public void LoadDefaultMapSettings() {
            map_tile_width = 512;
            map_tile_height = 128+64;
            load_settings = new ThisGenMapLoadSettings(map_tile_width, map_tile_height, "The_Defalt_Map");

            _ready = true;
        }

        protected bool _ready = false;
        public bool Ready {
            get {return _ready;}
        }
        public string map_title;
        public int map_tile_width = 512;
        public int map_tile_height = 128+64;

        public MapLoadSettings load_settings;
    };
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
            DetermineVisibleTiles();
            DrawTiles(gameTime);
        }
        protected int x_begin_tile_render = 0;
        protected int y_begin_tile_render = 0;
        protected int x_end_tile_render = 0;
        protected int y_end_tile_render = 0;
        public void DetermineVisibleTiles() {
            double x_beginf = (double)(Zoom._cameraX-(Access.A.window_width/2))-((Zoom._cameraZoom)*(Access.A.window_width/2));
            double y_beginf = (double)(Zoom._cameraY-(Access.A.window_height/2))-((Zoom._cameraZoom)*(Access.A.window_height/2));
            int x_begin = System.Convert.ToInt32(x_beginf);
            int y_begin = System.Convert.ToInt32(y_beginf);
            int x_end = 
                System.Convert.ToInt32(
                (x_begin + (Access.A.window_width/2* (1.0f/Zoom._cameraZoom))));
            int y_end = 
                System.Convert.ToInt32(
                (y_begin + (Access.A.window_height/2* (1.0f/Zoom._cameraZoom))));

            int tile_size = 32;
            x_begin_tile_render = x_begin / tile_size;
            y_begin_tile_render = y_begin / tile_size;
            x_end_tile_render = x_end / tile_size;
            y_end_tile_render = y_end / tile_size;

            if (x_begin_tile_render < 0) {
                x_begin_tile_render = 0;
            }
            if (y_begin_tile_render < 0 ) {
                y_begin_tile_render = 0;
            }
            if (x_end_tile_render < 0) {
                x_end_tile_render = 0;
            }
            if (y_end_tile_render < 0 ) {
                y_end_tile_render = 0;
            }
            if (x_begin_tile_render > map_settings.map_tile_width) {
                x_begin_tile_render = map_settings.map_tile_width;
            }
            if (y_begin_tile_render > map_settings.map_tile_height ) {
                y_begin_tile_render = map_settings.map_tile_height;
            }
            if (x_end_tile_render > map_settings.map_tile_width) {
                x_end_tile_render = map_settings.map_tile_width;
            }
            if (y_end_tile_render > map_settings.map_tile_height ) {
                y_end_tile_render = map_settings.map_tile_height;
            }

        }
        public void DrawTiles(GameTime gameTime) {
            Access access = Access.A;
            Tile[,] tiles = map_data.tiles;

            for (int x = x_begin_tile_render; x < x_end_tile_render; x++) {
                for (int y = y_begin_tile_render; y < y_end_tile_render; y++) {
                    Tile tile = tiles[x,y];
                    tile.Draw();
                }
            }

            //for (int x = 0; x < map_settings.map_tile_width; x++) {
            //    for (int y = 0; y < map_settings.map_tile_height; y++) {
            //        Tile tile = tiles[x,y];
            //        tile.Draw();
            //    }
            //}

            
        }
        public void Activity(GameTime gameTime) {
            Access access = Access.A;
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