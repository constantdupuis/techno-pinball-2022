using UnityEngine;

namespace Mo.Builders { 
    public class PrimitiveBuilder
    {
        private GameObject go;

        public PrimitiveBuilder(PrimitiveType type = PrimitiveType.Cube)
        {
            go = GameObject.CreatePrimitive(type);
        }

        public PrimitiveBuilder(Transform parent, PrimitiveType type = PrimitiveType.Cube)
        {
            go = GameObject.CreatePrimitive(type);
            go.transform.parent = parent;
        }

        public PrimitiveBuilder Parent(Transform parent)
        {
            go.transform.parent = parent;
            return this;
        }

        public PrimitiveBuilder Name(string name)
        {
            go.name = name;
            return this;
        }

        public PrimitiveBuilder Pos(Vector3 pos)
        {
            go.transform.position = pos;
            return this;
        }

        public PrimitiveBuilder LocalScale(Vector3 scale)
        {
            go.transform.localScale = scale;
            return this;
        }

        public PrimitiveBuilder Color(Color color)
        {
            go.GetComponent<MeshRenderer>().material.color = color;
            return this;
        }

        public GameObject Build()
        {
            return go;
        }
    }

}
