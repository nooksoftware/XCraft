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
using System.Net;
//using FastNoiseLite;

using MVector2 = Microsoft.Xna.Framework.Vector2;
using System.Net.Security;

namespace XCraft {
    public class G {
        public M m;
        public A a;
        public G(M m, A a) {
            this.m = m;
            this.a = a;
            this.tp = a.Tex("tp");

            lite = new FastNoiseLite();
            random = new Random();
        }
        public void Generate() {
            
            float[,] noise1 = 
            GenerateNoise(w,h-64,0.1f, FastNoiseLite.NoiseType.Cellular,
            FastNoiseLite.FractalType.FBm, 2, 2.0f, 2.0f, random.Next(10000));

            float[,] noise2 = 
            GeneratePerlinNoise(w,h-64,0.1f, random.Next(10000));

            float[,] noise3 = MultiplyNoises(noise1, noise2);
            for (int x = 0; x < w; x++) {
                for (int y = 64; y < h; y++) {
                    float v = noise3[x,y-64];

                    if (v < 0.3f) {
                        m.SetT(x,y,TT.CLEARSTONE);    
                    } else if (v < 0.5f) {
                        m.SetT(x,y,TT.STONE);    
                    } else if (v < 0.65f) {
                        m.SetT(x,y,TT.COBBLESTONE);    
                    } else if (v < 0.75f) {
                        m.SetT(x,y,TT.DIRT);    
                    } else if (v < 0.85f) {
                        m.SetT(x,y,TT.SAND);    
                    } else if (v < 0.92f) {
                        m.SetT(x,y,TT.CLAY);    
                    } else if (v <= 1.0f) {
                        m.SetT(x,y,TT.ROUGHSTONE);    
                    }
                    
                }
            }

            GenerateOre();
            GenerateTerrain();
            //GenerateStructures();
            //GenerateTrees();
            
        }
        public void GenerateTree(int posx, int type = 0) {
            int size = 7 + random.Next(5);

            int posX = posx;
            int posY = 0;
            for (int y = 0; y < h; y++) {
                TT tt = m.GetTT(posx,y);
                if (tt == TT.AIR //|| 
                    //tt == TT.LEAVES1 || tt == TT.LEAVES2 || tt == TT.LEAVES3 ||
                    //tt == TT.WOOD1 || tt == TT.WOOD2 || tt == TT.WOOD3
                    ) {
                    //continue search

                    posY = y;
                } else if (tt != TT.AIR && (tt != TT.WOOD1 || tt != TT.WOOD2 || tt != TT.LEAVES1 || tt != TT.LEAVES2)) {
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
            } 

            for (int y = posY - size; y < posY; y++) {
                m.SetT(posX,y, woodT);
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
                    if (l && m.GetTT(x,y) == TT.AIR) {
                        m.SetT(x,y,leavesT);
                    }
                }
            }
        }
        public void GenerateTrees() {
            float frequency = 0.15f;
            int i = 15;
            for (int x = 0; x < w; x++) {
                float d = (float)random.NextDouble();

                if (d < frequency) {
                    GenerateTree(x, random.Next(1));
                }

                if (i < 0) {
                    i = 10 + random.Next(12);
                    frequency = 0.03f + ((float)(random.NextDouble())*0.2f);
                } else {
                    i--;
                }
            }
        }
        public void GenerateTerrain() {
            float [,] terrainHeightNoise = GeneratePerlinNoise(w,1,0.1f, random.Next(10000));

            int terrainHeight = 32;
            int [,] heightNoise = new int[w,1];

            for (int x = 0; x < w; x++) {
                heightNoise[x,0] = System.Convert.ToInt32(terrainHeightNoise[x,0] * terrainHeight);
            }

            for (int x = 0; x < w; x++) {
                int height = heightNoise[x,0];
                
                for (int y = 0; y < 64; y++) {
                    int invY = 64-y;
                    if (invY < height) {
                        m.SetT(x,y,TT.ROUGHSTONE);
                    } else if (invY == height) {
                        m.SetT(x,y,TT.GRASS);
                    } else if (invY > height) {
                        m.SetT(x,y,TT.AIR);
                    }
                }
            }
        }
        protected void GenerateOre(TT tileType, int x, int y, int w, int h) {
            for (int i = x; i < x+w; i++) {
                for (int j = y; j < y+h; j++) {
                    bool ore = RandomScopic(8, 5);
                    
                    if (ore) {
                        if (i > 0 && i < this.w && j > 0 && j < this.h) {
                            TT tt = m.GetTT(i,j);
                            if (tt == TT.STONE || tt == TT.COBBLESTONE || tt == TT.CLEARSTONE || tt == TT.ROUGHSTONE) {
                                m.SetT(i, j, tileType);
                            }
                        }
                    }
                }
            }
        }
        protected void GenerateOre() {
            int ironOresX = w/12;
            int ironOresY = h/12;
            int goldOresX = w/24;
            int goldOresY = h/24;
            int diaOresX = w/36;
            int diaOresY = h/36;
            int ore1X = w/24;
            int ore1Y = h/24;
            int ore2X = w/36;
            int ore2Y = h/36;
            int ore3X = w/48;
            int ore3Y = h/48;

            for (int i = 0; i < ironOresX; i++) {
                for (int j = 0; j < ironOresY; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.IRON_ORE, x,y, 2+random.Next(4), 2+random.Next(1));
                }
            }
            for (int i = 0; i < goldOresX; i++) {
                for (int j = 0; j < goldOresY; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.GOLD_ORE, x,y, 2+random.Next(2), 2+random.Next(1));
                }
            }
            for (int i = 0; i < diaOresX; i++) {
                for (int j = 0; j < diaOresY; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.DIA_ORE, x,y, 2+random.Next(1), 2+random.Next(1));
                }
            }
            for (int i = 0; i < ore1X; i++) {
                for (int j = 0; j < ore1Y; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.ORE1, x,y, 2+random.Next(3), 2+random.Next(1));
                }
            }
            for (int i = 0; i < ore2X; i++) {
                for (int j = 0; j < ore2Y; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.ORE2, x,y, 2+random.Next(2), 2+random.Next(1));
                }
            }
            for (int i = 0; i < ore3X; i++) {
                for (int j = 0; j < ore3Y; j++) {
                    int x = random.Next(w);
                    int y = random.Next(h);

                    GenerateOre(TT.ORE3, x,y, 1+random.Next(1), 1+random.Next(1));
                }
            }
        }
        public FastNoiseLite lite;
        public Texture2D tp;
        public int w = 512;
        public int h= 256;
        public Random random;


        //////////////////////////////////////////////
        /// Random & Math Functions
        //////////////////////////////////////////////

        public bool RandomScopic(int percentagic, int scope) {
            return (random.Next(percentagic) < scope);
        }
        //////////////////////////////////////////////
        /// Noise Generations
        //////////////////////////////////////////////
        protected bool resetLite = false;

        public float NoiseValue(float x, float y) {
            float v = (lite.GetNoise(x,y));
            v = (float)((v+1.0f) *0.5f);
            if (v < 0.0f) { v = 0.0f;}
            else if (v > 1.0f) { v = 1.0f;}
            return v;
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



        public float[,] MultiplyNoises(float[,] a, float[,] b) {
            int nW = a.GetLength(0);
            int nH = a.GetLength(1);
            float [,] n = new float[nW, nH];

            float minV = n[0,0];
            float maxV = n[0,0];
            for (int x = 0; x < nW; x++) {
                for (int y = 0; y < nH; y++) {
                    float v = a[x,y] * b[x,y];
                    if (v < minV) minV = v;
                    if (v > maxV) maxV = v;
                    n[x,y] = v;
                }
            }
            float l = minV - minV;
            for (int x = 0; x < nW; x++) {
                for (int y = 0; y < nH; y++) {
                    
                    n[x,y] = (n[x,y] - minV) / (maxV - minV);
                }
            }
            return n;
        }

    };
}