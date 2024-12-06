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
//using FastNoiseLite;

namespace XCraft {
        public class G {
        FastNoiseLite lite;
        public D d;
        public M m;

        public Texture2D tp;

        int w = 512;
        int h = 256;

        int mH3 = 3;
        int mH2 = 96;
        int mH1 = 80;

        int iOp = 180; // 2200
        int gOp = 240; // 4500
        int dOp = 360; // 6000

        public G(D d, M m, int w, int h, int wH3, int wH2, int wH1, int iOp, int gOp, int dOp, bool gen = false) {
            this.d = d;
            this.tp = d.Tex("tp");
            lite = new FastNoiseLite();

            Set(m, w, h, mH3, mH2, mH1, iOp, gOp, dOp);
            random = new Random();
            if (gen) {
                Generate();
            }
        }
        public float NormalizeNoiseDef(float v) {
            v = (float)((v+1.0f) * 0.5f);
            if (v < 0.0f) { v = 0.0f;}
            else if (v > 1.0f) { v = 1.0f;}
            return v;
        }
        public float[] GenerateWidthNoise() {
            float[] terH = new float[w];
            lite.SetFrequency(0.04f);
            lite.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            for (int x = 0; x < w; x++) {
                float n = NormalizeNoiseDef(lite.GetNoise(((float)x), 1.0f));

                terH[x] = n;
            }
            return terH;
        }
        public int[] GenerateHeightForTerrain(float[] thNoise) {
            //int[] determinedBiomes; applications
            int[] heights = new int[w];

            for (int x = 0 ; x < w; x++) {
                heights[x] = System.Convert.ToInt32(thNoise[x] * mH1);

                //Console.WriteLine("   " + heights[x]); //ok
                if (heights[x] < 0) {
                    heights[x] = 0;
                } else if (heights[x] >= mH1) {
                    heights[x] = mH1-1;
                }
            }
            return heights;
        }
        public bool RandomPercentagic(int percentagic, int scope) {
            return (random.Next(percentagic) < scope);
        }
        public void GenerateSimpleHillLandscape(int h, int[] thHeights, int beginY, int endY) {
            for (int x = 0; x < w; x++) {
                int hValue = thHeights[x];
                for (int y = beginY; y < endY; y++) {
                    if (y-beginY == hValue) {
                        T(x,y,TT.GRASS);
                    } else if (y-beginY < hValue) {
                        T(x,y,TT.AIR);
                    } else if (y-beginY > hValue && y-beginY < hValue+3) {
                        T(x,y,TT.DIRT);
                    } else if (y-beginY > hValue) {
                        bool random3 = RandomPercentagic(endY - beginY, (y-beginY));
                        if (random3) {
                            T(x,y,TT.STONE);
                        } else {
                            T(x,y,TT.DIRT);
                        }
                    } else { //if y < hValue
                        T(x,y,TT.AIR);
                    }
                }
            }

        }
        public void GenerateSimpleUndergrounds(int beginY, int endY) {
            V2i[,] cavesCenters = new V2i[10,7];
            V2i[,] cavesSizes = new V2i[10,7];

            int height = endY - beginY;
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 7; j++) {
                    int locX = random.Next(w);
                    int locY = random.Next(endY-beginY);
                    int cavSizeX = random.Next(50);
                    int cavSizeY = random.Next(20);

                    locY = locY + 100;

                    cavesCenters[i,j] = new V2i(locX, locY);
                    cavesSizes[i,j] = new V2i(cavSizeX, cavSizeX);
                }
            }


            TT[,] ores = new TT[w,h];
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < h; y++) {
                    ores[x,y] = TT.UNKNOWN;
                }
            }

            for (int i = 0; i < 50; i++) {
                for (int j = 0; j < 30; j++) {
                    int oreX = random.Next(w);
                    int oreY = random.Next(h);
                    ores[oreX, oreY] = TT.IRON_ORE;
                }
            }
            for (int i = 0; i < 50; i++) {
                for (int j = 0; j < 30; j++) {
                    int oreX = random.Next(w);
                    int oreY = random.Next(h);
                    ores[oreX, oreY] = TT.GOLD_ORE;
                }
            }
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 8; j++) {
                    int oreX = random.Next(w);
                    int oreY = random.Next(h);
                    ores[oreX, oreY] = TT.DIA_ORE;
                }
            }

            for (int x = 0; x < w ; x++ ) {
                for (int y = 0; y < height; y++) {
                    T(x,y,TT.STONE);
                    if (ores[x,y] != TT.UNKNOWN) {
                        T(x,y, ores[x,y]);
                    }
                }
            }

            Ri[,] cavesBounds = new Ri[10,7];

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 7; j++) {
                    int caveOY = cavesCenters[i,j].y;
                    int caveOX = cavesCenters[i,j].x;
                    int caveWidth = cavesSizes[i,j].x;
                    int caveHeight = cavesSizes[i,j].y;


                    cavesBounds[i,j] = new Ri(caveOX - caveWidth/2, caveOY - caveHeight / 2, caveWidth, caveHeight);
                }
            }

            //for (int i = 0; i < 10; i++) {
            //    for (int j = 0; j < 7; j++) {
            //        Ri caveBounds = cavesBounds[i,j];
//
            //        for (int x = caveBounds.x; x < caveBounds.w + caveBounds.x; x++) {
            //            for (int y = caveBounds.y; y < caveBounds.h + caveBounds.y; y++) { 
            //                if (x > 0 && x < w && y > 0 && y < h) {
            //                    T(x,y, TT.AIR);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public void Generate() {
            Verify();
            lite.SetSeed(random.Next(1000000));
            //GenerateBasicTerrain();
            //GenerateBasicOre();
        
            int begY1 = h - (mH3 + mH2 + mH1);
            int begY2 = h - (mH3 + mH2);
            int begY3 = h - (mH3);

            float[] tHNoise = GenerateWidthNoise();
            int[] thHeights = GenerateHeightForTerrain(tHNoise);

            GenerateSimpleHillLandscape(mH1, thHeights, begY1, begY2);
            GenerateSimpleUndergrounds(begY2, begY3);

            for (int x = 0; x < w; x++) {
                for (int y = 0 ; y < h; y++) {  
                    if (y >= 0 && y < begY1) {
                        T(x,y, TT.AIR);
                    } else if (y >= begY1 && y < begY2) {

                    } else if (y >= begY2 && y < begY3) {
                        T(x,y, TT.STONE);
                    } else if (y >= begY3 && y < h) {
                        T(x,y, TT.BEDROCK);
                    }
                }
            }


            w = 512;
            h = 256;    

        }


        public void VerifyError(string message) {
            Console.WriteLine("Error while generating map " + message);
        }
        public void Verify() {
            if(d == null) {
                VerifyError("d == null");
            }
            if(m == null) {
                VerifyError("m == null");

            }
            if(tp == null) {
                VerifyError("tp == null");

            }
            if(w < 32) {
                VerifyError("w < 32");

            }
            if(h < 32) {
                VerifyError("h < 32");

            }
            if(mH3 <= 0) {
                VerifyError("mH3 <= 0");

            }
            if(mH2 <= 8) {
                VerifyError("mH2 <= 8");

            }
            if(mH1 <= 0) {
                VerifyError("mH1 <= 0");

            }
            if(iOp < 0) {
                VerifyError("iOp < 0");

            }
            if(gOp < 0) {
                VerifyError("gOp < 0");

            }
            if(dOp < 0) {
                VerifyError("dOp < 0");

            }
        }
        protected Random random;
        
        public bool RandomIP(int perc) {
            return random.Next(100) < perc;
        }
        public bool RandomL(int max, int v) {
            return RandomIP(System.Convert.ToInt32(100*((float)v/(float)max)));
        }
        public bool Random(int f) {
            return random.Next(f) == 0;
        }
        //Deprec
        protected void GenerateBasicTerrain() {
            int bedrock_y1 = h-mH3;
            int bedrock_y2 = h;
            int ground_y1 = h-mH2-mH3;
            int ground_y2 = h-mH3;
            int terrain_y1 = h-mH1-mH2-mH3;
            int terrain_y2 = h-mH2-mH3;

        // int mH3 = 3;
        // int mH2 = 96;
        // int mH1 = 49;

            for (int x = 0; x < w; x++) {
                for (int y = bedrock_y1; y < bedrock_y2; y++) {
                    int yr = y-bedrock_y1;
                    float perc = (float)yr / (float)(bedrock_y2 - bedrock_y1);

                    bool b = Random(bedrock_y2 - bedrock_y1 - yr);
                    if (b) {
                        T(x,y, TT.BEDROCK);
                    } else {
                        T(x,y, TT.STONE);
                    }
                }
            }
            for (int x = 0; x < w; x++) {
                for (int y = ground_y1; y < ground_y2; y++) {
                    int yr = y-ground_y1;
                    bool b = RandomL(ground_y2 - ground_y1, yr);
                    if (b) {
                        T(x,y, TT.STONE);
                    } else {
                        T(x,y, TT.DIRT);
                    }
                }
            }

            float[,] terH = new float[w,1];
            lite.SetFrequency(0.04f);
            lite.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            for (int x = 0; x < w; x++) {
                float n = NormalizeNoiseDef(lite.GetNoise(((float)x), 1.0f));

                terH[x,0] = n;
            }

            for (int x = 0; x < w; x++) {
                int terrH = 
                System.Convert.ToInt32(
                    terH[x,0]*(terrain_y2-terrain_y1)
                );
                for (int y = terrain_y1; y < terrain_y2; y++) {
                    int yr = y-terrain_y1;
                    
                    if (yr == terrH) {
                        T(x,y, TT.GRASS);
                    } else if (yr > terrH) {
                        T(x,y, TT.DIRT);

                    } else if (yr < terrH) {
                        T(x,y, TT.AIR);
                    }
                }
            }
            for (int x = 0; x < w; x++) {
                for (int y = 0; y < terrain_y1; y++) {
                    T(x,y,TT.AIR);
                }
            }
        }
        //Deprec
        protected void GenerateBasicOre() {
            int ground_y1 = h-mH2-mH3;
            int ground_y2 = h-mH3;
            for (int x = 0; x < w; x++) {
                for (int y = ground_y1; y < ground_y2; y++) {
                    bool ore1 = Random((int)(iOp));
                    if (ore1) {
                        T(x,y,TT.IRON_ORE);
                    }
                    bool ore2 = Random((int)(gOp));
                    if (ore2) {
                        T(x,y,TT.GOLD_ORE);
                    }
                    bool ore3 = Random((int)(dOp));
                    if (ore3) {
                        T(x,y,TT.DIA_ORE);
                    }
                }
            }
        }
        public void T(int x, int y, TT tt) {
            m.tts[x,y] = tt;
            m.ts[x,y] = new T(x, y, tt, tp, d);
        }
        public void Set(M m, int w, int h, int wH3, int wH2, int wH1, int iOp, int gOp, int dOp) {
            if (m != null) {this.m = m;}
            if (w != -1) {this.w = w;}
            if (h != -1) {this.h = h;}
            if (mH3 != -1) {this.mH3 = mH3;}
            if (mH2 != -1) {this.mH2 = mH2;}
            if (mH1 != -1) {this.mH1 = mH1;}
            if (iOp != -1) {this.iOp = iOp;}
            if (gOp != -1) {this.gOp = gOp;}
            if (dOp != -1) {this.dOp = dOp;}
        }
    };
}