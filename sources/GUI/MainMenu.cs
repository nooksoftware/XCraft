using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Numerics;
using XCraftLib;
//using FastNoiseLite;

namespace XCraft {
    //Events
    public class PlayButton_EventOnClick : XCraftLib.EventOnClick {
        public PlayButton_EventOnClick(XCraftLib.GUI gui) : base(gui) {}
        public override void Do(GUIElement element) {
            base.Do(element);
        }
    };
}