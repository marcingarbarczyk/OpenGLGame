using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Model
    {
        public List<ModelCoords.XYZ> lista_xyz = new List<ModelCoords.XYZ>();
        public List<ModelCoords.UV> lista_uv = new List<ModelCoords.UV>();
        public List<ModelCoords.XYZ> lista_norm = new List<ModelCoords.XYZ>();
        public List<List<ModelCoords.Vertex>> lista_f = new List<List<ModelCoords.Vertex>>();
    }
}
