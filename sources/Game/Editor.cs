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
    public class EditorS {

    };
    public class Editor {
        public Game1 g;
        public D d;
        public A a;
        public GUI gui;
        public string gui_main_l;
        public EditorS settings;

        protected bool loaded_guie = false;
        public GUIE editor_guie;
        public Editor(Game1 g, D d, A a, GUI gui, string gui_main_l, EditorS settings) {
            this.g = g;
            this.d = d;
            this.a = a;
            this.gui = gui;
            this.gui_main_l = gui_main_l;
            this.settings = settings;
        }

        public void Tick() {
            if (this.g != null && this.d != null && this.a != null && this.gui != null && this.settings != null )
            {
                if (loaded_guie == false) {
                    editor_guie = gui.main.Get("editor");
                    loaded_guie = true;
                }

                PanelGUIE panel = editor_guie.Get2<PanelGUIE>("mappanel");
                ButtonGUIE generateB = panel.Get2<ButtonGUIE>("generate");

                if (generateB.Clicked()) {
                    ReloadMap();
                }
            } else {
                return;
            }

        }

        public void ReloadMap() {

        }
    };
}