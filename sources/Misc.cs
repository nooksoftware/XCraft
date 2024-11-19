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

namespace vgCSh {
    public class Vec2iKey : IEquatable<Vec2iKey> {
        public int X { get; }
        public int Y { get; }
        public Vec2iKey(int x, int y) {
            X = x;
            Y = y;
        }
         public override bool Equals(object obj)
        {
            return Equals(obj as Vec2iKey);
        }

        public bool Equals(Vec2iKey other)
        {
            if (other is null) return false;
            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            unchecked // Allow overflow
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
    public class Vec2f {
        public Vec2f() {
            _x = 0.0f;
            _y = 0.0f;
        }
        public Vec2f(float a, float b) {
            x = a;
            y = b;
        }
        public float _x = 0.0f;
        public float _y = 0.0f;

        public float x {
            get {return _x;}
            set {this._x = x;}
        }
        public float y {
            get {return _y;}
            set {this._y = y;}
        }
        public float X {
            get {return _x;}
            set {this._x = X;}
        }
        public float Y {
            get {return _y;}
            set {this._y = Y;}
        }
    }
    public class Vec2i {
        public Vec2i() {
            _x = 0;
            _y = 0;
        }
        public Vec2i(int a, int b) {
            _x = a;
            _y = b;
        }
        public int _x = 0;
        public int _y = 0;
        public int x {
            get {return _x;}
            set {this._x = x;}
        }
        public int y {
            get {return _y;}
            set {this._y = y;}
        }
        public int X {
            get {return _x;}
            set {this._x = X;}
        }
        public int Y {
            get {return _y;}
            set {this._y = Y;}
        }
    };
    public class Vec3i {
        public Vec3i() {
            _x = 0;
            _y = 0;
            _z = 0;
        }
        public Vec3i(int a, int b, int c) {
            x = a;
            y = b;
            z = c;
        }
        protected int _x = 0;
        protected int _y = 0;
        protected int _z = 0;
        public int x {
            get {return _x;}
            set {this._x = x;}
        }
        public int y {
            get {return _y;}
            set {this._y = y;}
        }
        public int z {
            get {return _z;}
            set {this._z = z;}
        }
    };
}