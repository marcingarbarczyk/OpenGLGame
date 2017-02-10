using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Part
    {
        public List<Element> elements = new List<Element>();
        public List<Medkit> medkits = new List<Medkit>();
        public List<Point> points = new List<Point>();
        public List<Enemy> enemies = new List<Enemy>();
    }
}
