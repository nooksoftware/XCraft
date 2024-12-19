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
    public enum GUIT {
        GUIELEMENT,
        BUTTON,
        SLIDER,
        PANEL,
        TEXT_INPUT_FIELD,
        PROGRESSBAR,
        CHECKBOX,
        RADIOBOX,
        DROPDOWN,
        TEXT_AREA_FIELD,
        NUMBERFIELD,
        PASSWORD_TEXT_INPUT_FIELD,
        TOOLTIP,
        SEARCHBAR,
        POPUP,
        MENUPOPUP,
        COLORPICKER,
        DATEPICKER,
        TIMEPICKER,
        SCROLLABLE,
        LAYOUTS,
        GRAPHICBUTTON,
        TEXTAREAFIELD,
        VAROUTLISTE
    };
    public class GUIE {
        public Dictionary<string, GUIE> children;
        public GUIT guit = GUIT.GUIELEMENT;

        public GUI gui;
        public A a;
        public int x;
        public int y;
        public int w;
        public int h;

        public Color clicked_color;
        public Color hover_color;
        public Color normal_color;
        public GUIE(GUI gui, A a, int x, int y, int w, int h) {

        }
    };
}    