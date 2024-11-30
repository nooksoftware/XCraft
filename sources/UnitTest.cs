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
    public class UT {
        public Game1 g; 
        public D d; 
        public A a;
        public UT(Game1 g, D d, A a) {
            this.g = g;
            this.d = d;
            this.a = a;
        }
        public virtual void Run() {}
    };
    public class UTs {
        public Dictionary<string, UT> uts;
        public List<UT> uts_list;
        public UTs() {
            uts = new Dictionary<string, UT>();
        }
        public void Add(string s, UT ut) {
            uts.Add(s,ut);
            uts_list.Add(ut);
        }
        public void Run() {
            foreach (UT ut in uts) {
                ut.Run();
            }
        }
    }
    public class GUI_UT1 : UT {
        //C
        public GUI_UT1(Game1 g, D d, A a) : base(g,d,a) {}
        public override void Run() {
            
        }
    }
}