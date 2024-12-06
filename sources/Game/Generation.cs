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
using System.Xml.Linq;
using System.Runtime.CompilerServices;
//using FastNoiseLite;

namespace XCraft {
    public enum BiomeTerrainType {
        NONE = -1,
        HILLS = 0,
        CLIFFS,
        FOREST
    };
    public enum CaveType {
        NONE = -1,
        NORMAL_CAVE = 0,
        WATER_CAVE
    };
    public enum BaseType {
        NONE = -1,
        MAIN_BASE = 0,
        OUTPUST
    };
    public class GBase {
        public int moduleA = 0;
        public int moduleB = 0;
        public bool isBaseNotOutpust = true;
        int centerX = 0;
        int centerY = 0;
        public void Center(int centerX, int centerY) {
            this.centerX = centerX;
            this.centerY = centerY;
        }
    };
    public class GBiomeTerrain {
        public BiomeTerrainType type;
        public GBiomeTerrain(BiomeTerrainType type) {
            this.type = type;
        }
    };
    public class GCliffsBiomeTerrain : GBiomeTerrain {
        int treesFrequency = 8;
        public GCliffsBiomeTerrain() : base(BiomeTerrainType.CLIFFS) {
    
        }
    };
    public class GForestBiomeTerrain : GBiomeTerrain {
        int treesFrequency = 50;
        public GForestBiomeTerrain() : base(BiomeTerrainType.FOREST) {
    
        }
    };
    public class GHillsBiomeTerrain : GBiomeTerrain {
        int treesFrequency = 15;
        public GHillsBiomeTerrain() : base(BiomeTerrainType.HILLS) {
    
        }
    };
    public class GMapArea {
        public Ri mapArea = null;
        public int[,] relPreciseAreaMatrix;
        public GMapArea(GMapArea cpy) {
            mapArea = new Ri(
                cpy.mapArea.x,
                cpy.mapArea.y,
                cpy.mapArea.w,
                cpy.mapArea.h
            );
            this.relPreciseAreaMatrix = cpy.relPreciseAreaMatrix;
        }
        public GMapArea(Ri mapArea) {
            this.mapArea = mapArea;
            int w = mapArea.w;
            int h = mapArea.h;
            this.relPreciseAreaMatrix = new int[w,h];
        }
        public GMapArea(Ri mapArea, int[,] prec) {
            this.mapArea = mapArea;
            int w = mapArea.w;
            int h = mapArea.h;
            this.relPreciseAreaMatrix = prec;
        }
    };
    public class GArea {
        public int id = 0;
        public bool isBiomeTerrain = false;
        public bool isCave = false;
        public bool isBase = false;
        public BiomeTerrainType biomeTerrainType = BiomeTerrainType.NONE;
        public GBiomeTerrain gBiomeTerrain = null;
        public CaveType caveType = CaveType.NONE;      
        public BaseType baseType = BaseType.NONE; 
        public GBase gBase = null; 

        public int[] preciseArea;

        public GMapArea gMapArea = null;
        public void SetID(int id) {
            this.id = id;
        }
        public GArea(GArea cpy, Ri mapArea) {
            isBiomeTerrain = cpy.isBiomeTerrain;
            isCave = cpy.isCave;
            isBase = cpy.isBase;
            biomeTerrainType = cpy.biomeTerrainType;
            gBiomeTerrain = cpy.gBiomeTerrain;
            caveType = cpy.caveType;
            baseType = cpy.baseType;
            gBase = cpy.gBase;
            preciseArea = cpy.preciseArea;
            gMapArea = new GMapArea(cpy.gMapArea);
            gMapArea.mapArea = mapArea;
        }
        public GArea(BiomeTerrainType biomeTerrainType, GBiomeTerrain s, int x, int y, int w, int h) {
            this.biomeTerrainType = biomeTerrainType;
            this.gBiomeTerrain = s;
            this.isBiomeTerrain = true;
            this.gMapArea = new GMapArea(new Ri(x,y,w,h));
        }
        public GArea(CaveType caveType, int x, int y, int w, int h) {
            this.caveType = caveType;
            this.isCave = true;
            this.gMapArea = new GMapArea(new Ri(x,y,w,h));
        }
        public GArea(BaseType baseType, GBase gBase, int x, int y, int w, int h) {
            this.baseType = baseType;
            this.gBase = gBase;
            this.isBase = true;
            this.gMapArea = new GMapArea(new Ri(x,y,w,h));
        }

    }
    public class GFloatIIndex {
        public float min = 0.0f;
        public float max = 1.0f;
        public int i = 0;
        public GFloatIIndex(float min, float max, int i) {
            this.min = min;
            this.max = max;
            this.i = i;
        }
    }
    public class G {
        public FastNoiseLite lite;
        public D d;
        public M m;
        public Texture2D tp;
        public int w = 512;
        public int h= 256;

        public Dictionary<int, GArea> gAreas;
        protected int biomesCount = 0;

        public readonly float groundProportion = 0.7f;
        public readonly float terrainProportion = 0.06f;

        public int airY1 = 0;
        public int airY2 = -1;
        public int terrainY1 = 0;
        public int terrainY2 = -1;
        public int groundY1 = 0;
        public int groundY2 = -1;
        public int bedrockY1 = 0;
        public int bedrockY2 = -1;


        public int groundH = -1;
        public int terrainH = -1;
        public int bedrockH = -1;
        public int airH = -1;

        public int iOp = 180; // 2200
        public int gOp = 240; // 4500
        public int dOp = 360; // 6000

        public Random random;

        public readonly int base1gAreaID = 0;
        public readonly int base2gAreaID = 1;
        public readonly int biomeTerrainAreaIDStart = 2; 

        public void GenerateTree(int posx, int type = 0) {
            int size = 7 + random.Next(5);

            int posX = posx;
            int posY = 0;
            for (int y = 0; y < terrainY2; y++) {
                TT tt = GetTT(posx,y);
                if (tt == TT.AIR //|| 
                    //tt == TT.LEAVES1 || tt == TT.LEAVES2 || tt == TT.LEAVES3 ||
                    //tt == TT.WOOD1 || tt == TT.WOOD2 || tt == TT.WOOD3
                    ) {
                    //continue search

                    posY = y;
                } else if (tt == TT.GRASS || tt == TT.STONE || tt == TT.DIRT) {
                    posY = y;
                    break;
                }
            }

            //generate wood
            TT woodT = TT.WOOD1;
            TT leavesT = TT.LEAVES1;

            if (type == 1) {
                woodT = TT.WOOD2;
                leavesT = TT.LEAVES2;
            } else if (type == 2) {
                woodT = TT.WOOD3;
                leavesT = TT.LEAVES3;
            }

            for (int y = posY - size; y < posY; y++) {
                SetT(posX,y, woodT);
            }

            int posY2 = posY-size;
            int beginX = posX - 3;
            int endX = posX + 4;
            int beginY = posY2 - 3;
            int endY = posY2 + 4;

            if (beginX < 0) {
                beginX = 0;
            } else if (beginX > w) {
                beginX = w;
            }
            if (endX < 0) {
                endX = 0;
            } else if (endX > w) {
                endX = w;
            }
            if (beginY < 0) {
                beginY = 0;
            } else if (beginY > h) {
                beginY = h;
            }
            if (endY < 0) {
                endY = 0;
            } else if (endY > h) {
                endY = h;
            }

            for (int x = beginX; x < endX; x++) {
                for (int y = beginY; y < endY; y++) {
                    bool l = RandomScopic(90,75);
                    if (l && GetTT(x,y) == TT.AIR) {
                        SetT(x,y,leavesT);
                    }
                }
            }
        }
        public void GenerateTrees(int xPos, int w, float freq, int type1Freq, int type2Freq, int type3Freq) {
            for (int x = xPos; x < xPos+w; x++) {
                float d = (float)random.NextDouble();
                if (d < freq) {
                    int randomNumber = random.Next(type1Freq+type2Freq+type3Freq);
                    int type = 0;
                    type = (randomNumber < type1Freq && randomNumber >= 0 ? 0 : 0);
                    type = (randomNumber < type1Freq + type2Freq && randomNumber > type1Freq ? 1 : type); 
                    type = (randomNumber < type1Freq + type2Freq + type3Freq && randomNumber > type1Freq + type2Freq ? 2 : type); 
                    GenerateTree(x, type);
                }
            }
        }

        public G(D d, M m, int w, int h, int iOp, int gOp, int dOp, bool gen = false) {
            this.d = d;
            this.tp = d.Tex("tp");
            lite = new FastNoiseLite();

            gAreas = new Dictionary<int, GArea>();

            Set(m, w, h, iOp, gOp, dOp);

            random = new Random();
            if (gen) {
                Generate();
            }
        }
        public float NoiseValue(float x, float y) {
            float v = (lite.GetNoise(x,y));
            v = (float)((v+1.0f) *0.5f);
            if (v < 0.0f) { v = 0.0f;}
            else if (v > 1.0f) { v = 1.0f;}
            return v;
        }
        public bool RandomScopic(int percentagic, int scope) {
            return (random.Next(percentagic) < scope);
        }
        public void Set(M m, int w, int h, int iOp, int gOp, int dOp) {

            if (m != null) {this.m = m;}
            if (w != -1) {this.w = w;}
            if (h != -1) {this.h = h;}
            if (iOp != -1) {this.iOp = iOp;}
            if (gOp != -1) {this.gOp = gOp;}
            if (dOp != -1) {this.dOp = dOp;}

            CalculateHeights();
        }
        protected void CalculateHeights() {

            groundH = System.Convert.ToInt32(h*groundProportion);
            terrainH = System.Convert.ToInt32(h*terrainProportion);
            bedrockH = 5; //constant
            airH = h - groundH - terrainH - bedrockH;

            airY1 = 0;
            airY2 = airH;
            terrainY1 = airY2;
            terrainY2 = terrainY1 + terrainH;
            groundY1 = terrainY2;
            groundY2 = groundY1 + groundH;
            bedrockY1 = groundY2;
            bedrockY2 = h;
        }
        public void SetT(int x, int y, TT tt) {
            m.tts[x,y] = tt;
            m.ts[x,y] = new T(x,y,tt,tp,d);
        }
        public T GetT(int x, int y) {
            return m.ts[x,y];
        }
        public TT GetTT(int x, int y) {
            return m.tts[x,y];
        }
        public void GenerateBaseAreas() {
            //Base Generation

            int base1X = 0, base1Y = 0, base1W = 0, base1H = 0;
            int base2X = 0, base2Y = 0, base2W = 0, base2H = 0;

            int windowWidth = w;
            base1W = 18 + random.Next(12);
            base2W = base1W;

            base1X = 6 + random.Next(12);
            base1H = base1W - 12;
            base2H = base1H;

            base1Y = 0;
            base2Y = 0;

            base2X = windowWidth - base1X - base2W;

            GArea base1gArea = new GArea(BaseType.MAIN_BASE, new GBase(), base1X, base1Y, base1W, base1H);
            GArea base2gArea = new GArea(BaseType.MAIN_BASE, new GBase(), base2X, base2Y, base2W, base2H);

            gAreas.Add(base1gAreaID, base1gArea);
            gAreas.Add(base2gAreaID, base2gArea); 
        }
        protected BiomeTerrainType RandomBiomeTerrainType() {
            return (BiomeTerrainType)(random.Next(2));
        }
        protected GBiomeTerrain AllocateDefaultGBiomeTerrain(BiomeTerrainType biomeType) {
            if (biomeType == BiomeTerrainType.HILLS) {
                return new GHillsBiomeTerrain();
            } else if (biomeType == BiomeTerrainType.CLIFFS) {
                return new GCliffsBiomeTerrain();
            } else if (biomeType == BiomeTerrainType.FOREST) {
                return new GForestBiomeTerrain();
            }
            return new GHillsBiomeTerrain();
        }
        public void GenerateBiomeAreas() {
            int beginI = biomeTerrainAreaIDStart;
            int mapMiddleW = w/2;
            int modulo = mapMiddleW % 2;
            mapMiddleW += modulo;
            
            int biomeI = 0;
            int biomeW = 24+random.Next(48);
            int beginBiomeX = 0;
            int endBiomeX = biomeW;
            BiomeTerrainType biomeType = RandomBiomeTerrainType();
            GBiomeTerrain gBiomeTerrain = AllocateDefaultGBiomeTerrain(biomeType);
            gAreas.Add(beginI + biomeI, new GArea(biomeType, gBiomeTerrain, beginBiomeX, 0, biomeW, terrainH));
            gAreas.TryGetValue(beginI + biomeI, out GArea gArea);
            biomesCount++;

            for (int x = 0; x < mapMiddleW; x++) {
                if (x >= beginBiomeX && x < endBiomeX) {
                    //define gArea constants for the biome
                } else {
                    biomeI++;
                    beginBiomeX = beginBiomeX + biomeW;
                    biomeW = 24 + random.Next(48);
                    endBiomeX = beginBiomeX + biomeW;
                    if (beginBiomeX + biomeW > mapMiddleW) {
                        biomeW = mapMiddleW - beginBiomeX;
                    }
                    biomeType = RandomBiomeTerrainType();
                    gBiomeTerrain = AllocateDefaultGBiomeTerrain(biomeType);
                    gAreas.Add(beginI + biomeI, new GArea(biomeType, gBiomeTerrain, beginBiomeX, 0, biomeW, terrainH));
                    gArea = gAreas[beginI + biomeI];
                    biomesCount++;
                }
            }

            MirrorBiomeAreas();
        }
        //fix
        protected void MirrorBiomeAreas() {
            int beginI = biomeTerrainAreaIDStart;
            int endI = beginI + biomesCount;


            for (int i = beginI ; i < endI; i++) {
                int relI = i - beginI;
                GArea gArea = gAreas[i];
                //bool s = gAreas.TryGetValue(i, out GArea gArea);
                if (gArea != null) {
                    Ri mapArea = gArea.gMapArea.mapArea;

                    int endX = w-mapArea.x;
                    int beginX = endX - mapArea.w;
                    int width = mapArea.w;
                    int height = mapArea.h;

                    Ri mirroredMapArea = new Ri(beginX, mapArea.y, width, height);

                    GArea mirr = new GArea(gArea, mirroredMapArea);
                    
                    gAreas.Add(endI+relI, mirr);
                    biomesCount++;
                }
            }
        }
        public void GenerateEssentialGAreas() {
            GenerateBaseAreas();
            GenerateBiomeAreas();
        }
        protected bool IsWithinGArea(GArea gArea, int x, int y = -1) {
            Ri mapArea = gArea.gMapArea.mapArea;
            if (y == -1) {
                return (x > mapArea.x && x < mapArea.x + mapArea.w);
            }
            return (x > mapArea.x && x < mapArea.x + mapArea.w && y > mapArea.y && y < mapArea.y + mapArea.h);
        }
        public void GenerateHillsBiomeTerrain(GArea gA) {
            GHillsBiomeTerrain gForestBiomeTerrain = gA.gBiomeTerrain as GHillsBiomeTerrain;

            Ri mapArea = gA.gMapArea.mapArea;

            float[,] heightMap = 
                GeneratePerlinNoise(mapArea.w, 1, 0.03f, random.Next(100000));

            int [,] heightMap2 = generateIntNoiseFromFloat(heightMap, mapArea.w, 1, terrainH);
            
            for (int x = mapArea.x; x < mapArea.x + mapArea.w; x++) {
                int relX = x - mapArea.x;
                int mapH = heightMap2[relX,0];
                for (int y = terrainY1; y < terrainY2; y++) {
                    int relY = y - terrainY1;
                    
                    if (relY < mapH) {
                        SetT(x,y,TT.AIR);
                    } else if (relY == mapH) {
                        SetT(x,y,TT.GRASS);
                    } else if (relY > mapH) {
                        SetT(x,y,TT.DIRT);
                    }
                }
            }

            GenerateTrees(mapArea.x, mapArea.w, 0.2f, 7, 2, 1);
        }
        public void GenerateForestBiomeTerrain(GArea gA) {
            GForestBiomeTerrain gForestBiomeTerrain = gA.gBiomeTerrain as GForestBiomeTerrain;


            Ri mapArea = gA.gMapArea.mapArea;
            for (int x = mapArea.x; x < mapArea.x + mapArea.w; x++) {
                for (int y = terrainY1; y < terrainY2; y++) {
                    int relY = y - terrainY1;
                    
                }
            }

            GenerateTrees(mapArea.x, mapArea.w, 0.4f, 7, 2, 1);
        }
        public void GenerateCliffsBiomeTerrain(GArea gA) {
            GCliffsBiomeTerrain gForestBiomeTerrain = gA.gBiomeTerrain as GCliffsBiomeTerrain;

            Ri mapArea = gA.gMapArea.mapArea;

            float[,] cliffsNoise = 
                GeneratePerlinNoise(mapArea.w, 1, 0.04f,/* 
                FastNoiseLite.NoiseType.Perlin, FastNoiseLite.FractalType.,
                4, 2.0f, 0.4f*/ random.Next(100000));

            int [,] cliffsNoiseI = generateIntNoiseIndex(cliffsNoise,  mapArea.w, 1, 
                new GFloatIIndex[]{
                    new GFloatIIndex(0.0f, 0.6f, 0),
                    new GFloatIIndex(0.6f, 1.0f, 1),
                }
            );

            float[,] cobblestoneNoise = 
                GeneratePerlinNoise(mapArea.w, mapArea.h, 0.08f,/* 
                FastNoiseLite.NoiseType.Cellular, FastNoiseLite.FractalType.FBm,
                4, 2.0f, 0.4f, */random.Next(100000));

            int [,] cobbleStoneNoiseI = generateIntNoiseIndex(cobblestoneNoise, mapArea.w, mapArea.h, 
                new GFloatIIndex[]{
                    new GFloatIIndex(0.0f, 0.6f, 0),
                    new GFloatIIndex(0.6f, 1.0f, 1),
                }
            );
            
            for (int x = 0; x < mapArea.w; x++){
                int cliffI = cliffsNoiseI[x, 0];
                if (cliffI == 1) {
                    float v = cliffsNoise[x,0];
                    v *= 1.5f;
                    if (v > 1.0f) {
                        v = 1.0f;
                    } else if (v < 0.0f) {
                        v = 0.0f;
                    }
                    cliffsNoise[x,0] = v;
                }
            }
            int [,] cliffsNoiseI2 = generateIntNoiseFromFloat(cliffsNoise, mapArea.w, 1, terrainH);
            for (int x = mapArea.x; x < mapArea.x + mapArea.w; x++) {
                int relX = x - mapArea.x;

                int cliffsH = cliffsNoiseI2[relX,0];

                for (int y = terrainY1; y < terrainY2; y++) {
                    int relY = y - terrainY1;

                    if (relY > cliffsH) {
                        if (cobbleStoneNoiseI[relX,relY] == 1) {
                            SetT(x,y, TT.COBBLESTONE);
                        } else {
                            SetT(x,y, TT.STONE);
                        }
                    } else if (relY == cliffsH) {
                        SetT(x,y, TT.GRASS);
                    } else if (relY < cliffsH) {
                        SetT(x,y, TT.AIR);
                    }
                }
            }

            GenerateTrees(mapArea.x, mapArea.w, 0.08f, 7, 2, 1);
        }

        public void GenerateBiomeTerrain(GArea gA) {
            if (gA.biomeTerrainType == BiomeTerrainType.HILLS) {
                GenerateHillsBiomeTerrain(gA);
            } else if (gA.biomeTerrainType == BiomeTerrainType.FOREST) {
                //GenerateHillsBiomeTerrain(gA);
                GenerateForestBiomeTerrain(gA);
            } else if (gA.biomeTerrainType == BiomeTerrainType.CLIFFS) {
                GenerateCliffsBiomeTerrain(gA);
            }
        }
        public void GenerateBaseTerrains() {
            GArea teamA = gAreas[0];
            GArea teamB = gAreas[1];

            
        }
        public void GenerateTerrain() {
            int beginI = biomeTerrainAreaIDStart;
            int biomeI = 0;
            
            for (int i = 0; i < biomesCount; i++) {
                GArea gA = gAreas[beginI + biomeI];    
                GenerateBiomeTerrain(gA);
                biomeI++;
            }
            GenerateBaseTerrains();
            /*for (int x = 0; x < w; x++) {
                bool isThisBiome = IsWithinGArea(terrainBiomeGArea, x);
                if (!isThisBiome) {
                    biomeI++;
                    terrainBiomeGArea = gAreas[beginI + biomeI];
                }
                
                for (int y = terrainY1; y < terrainY2; y++) {

                }
            }*/
        }
        public void GenerateGround() {

            float[,] mapNoise = GenerateNoise(w,h,0.1f,
                FastNoiseLite.NoiseType.Perlin, FastNoiseLite.FractalType.FBm,
                4, 2.0f, 0.5f, random.Next(10000000));

            int [,] terrainMap1 = generateIntNoiseIndex(mapNoise, w, h, new GFloatIIndex[]{
                new GFloatIIndex(0.0f, 0.3f, 0),
                new GFloatIIndex(0.0f, 0.6f, 1),
                new GFloatIIndex(0.6f, 1.0f, 2)
            });

            for (int x = 0; x < w; x++) {
                for (int y = terrainY1; y < h; y++) {
                    int tiv = terrainMap1[x,y];
                    if (tiv == 2) {
                        SetT(x,y,TT.STONE);
                    } else if (tiv == 1) {
                        SetT(x,y,TT.STONE);
                    } else if (tiv == 0) {
                        SetT(x,y,TT.DIRT);
                    }
                }
            }
        }
        public void GenerateBedrock() {
            for (int x = 0; x < w; x++) {
            for (int y = bedrockY1; y < bedrockY2; y++) {
                SetT(x,y,TT.BEDROCK);
            }   
            }
        }
        public void Generate() {
            biomesCount = 0;
            //General Fill
            float[,] mapNoise = GenerateNoise(w,h,0.1f,
                FastNoiseLite.NoiseType.Perlin, FastNoiseLite.FractalType.FBm,
                4, 2.0f, 0.5f, random.Next(10000000));

            generateIntNoiseIndex(mapNoise, w, h, new GFloatIIndex[]{
                new GFloatIIndex(0.0f, 0.6f, 0),
                new GFloatIIndex(0.6f, 1.0f, 1)
            });

            GenerateFullAir();
            GenerateEssentialGAreas();
            GenerateGround();
            GenerateTerrain();

            PostProcessGAreas();
        }
        protected void PostProcessGAreas() {
            foreach (var gAreaP in gAreas) {
                int id = gAreaP.Key;
                GArea gArea = gAreaP.Value;

                m.gAreas.Add(id, gArea);
            }
        }
        protected void GenerateFullAir() {
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    SetT(x,y,TT.AIR);
                }
            }
        }
        public bool resetLite = false;
        public int DetermineIndex(float noise, GFloatIIndex[] indexes) {
            for (int i = 0; i < indexes.Length; i++) {
                GFloatIIndex index = indexes[i];
                if (noise > index.min && noise < index.max) {
                    return index.i;
                }
            }
            return -1;
        }
        public int[,] generateIntNoiseIndex(float [,] noise, int w, int h, GFloatIIndex[] indexes) {
            int[,] _indexes = new int[w,h];
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    float n = noise[x,y];
                    _indexes[x,y] = DetermineIndex(n, indexes);
                }
            }
            return _indexes;
        }
        
        public int[,] generateIntNoiseFromFloat(float [,] noise, int w, int h, int scopeH) {
            int[,] intNoise = new int[w,h];
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    float v = noise[x,y];
                    int i = System.Convert.ToInt32(v*scopeH);
                    if (i < 0) { i = 0;}
                    if (i > scopeH) { i = h;}
                    intNoise[x,y] = i;
                }
            }
            return intNoise;
        }
        public float[,] GenerateNoise(int w, int h, float frequency, 
            FastNoiseLite.NoiseType noiseType, FastNoiseLite.FractalType fractalType,
            int fractalOctaves = 1, float fractalLacunarity = 1.0f, float fractalGain = 0.5f, int seed = -1
        ) {
            float[,] noise = new float[w,h];
            if (seed != -1) {lite.SetSeed(seed);}
            lite.SetFrequency(frequency);
            lite.SetNoiseType(noiseType);
            lite.SetFractalType(fractalType);
            lite.SetFractalOctaves(fractalOctaves);
            lite.SetFractalLacunarity(fractalLacunarity);
            lite.SetFractalGain(fractalGain);

            for (int x = 0; x < w; x++) {
                for (int y = 0 ; y< h ; y++) {
                    noise[x,y] = NoiseValue(x,y);
                }
            }

            resetLite = true;

            return noise;
        }
        public float[,] GeneratePerlinNoise(int w, int h, float frequency, int seed = -1) {
            if (resetLite) {
                lite = new FastNoiseLite();
            }
            
            lite.SetFrequency(frequency);
            if (seed != -1) {lite.SetSeed(seed);}
            
            float[,] noise = new float[w,h];

            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    noise[x,y] = NoiseValue(x,y);
                }
            }

            return noise;
        }
    };
}