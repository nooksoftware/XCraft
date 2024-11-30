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
        string ut_name;
        public List<bool> _assertions;
        public List<string> _assertions_labels;
        public UT(string ut_name, Game1 g, D d, A a) {
            this.g = g;
            this.d = d;
            this.a = a;
            this.ut_name = ut_name;
            _assertions = new List<bool>();
            _assertions_labels = new List<string>();
        }
        public virtual void Run() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Running Unit Test: " + ut_name + " ...");
        }
        public virtual void OutputResults() {
            for (int i = 0; i < _assertions.Count; i++) {
                if (_assertions[i]) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Assertion no." + i + " (" + _assertions_labels[i] + ") Succeded");
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Assertion no." + i + " (" + _assertions_labels[i] + ") Failed");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Assert(string l, bool a) {
            _assertions.Add(a);
            _assertions_labels.Add(l);
        }
    };
    public class UTs {
        public Dictionary<string, UT> uts;
        public List<UT> uts_list;
        public UTs() {
            uts = new Dictionary<string, UT>();
            uts_list = new List<UT>();
        }
        public void Add(string s, UT ut) {
            uts.Add(s,ut);
            uts_list.Add(ut);
        }
        public void Run() {
            foreach (var ut in uts) {
                ut.Value.Run();
            }
        }
    };


    public class ExampleUT : UT {
        //C
        public ExampleUT(string l, Game1 g, D d, A a) : base(l,g,d,a) {}
        public override void Run() {
            base.Run();

            OutputResults();
        }
    };

    public class GUI_UT1 : UT {
        //C
        public GUI_UT1(string l, Game1 g, D d, A a) : base(l,g,d,a) {}
        public override void Run() {
            base.Run();

            Assert("assert a", true);
            Assert("assert b", true);
            Assert("assert c", true);

            OutputResults();
        }
    };
    public class GUI_UT2 : UT {
        //C
        public GUI_UT2(string l, Game1 g, D d, A a) : base(l,g,d,a) {}
        public override void Run() {
            base.Run();

            Assert("assert a", false);
            Assert("assert b", true);
            Assert("assert c", true);

            OutputResults();
        }
    };
}