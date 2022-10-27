using UnityEngine;

namespace Mo.Math
{
    public class Coords
    {
        #region variables

        #region Public Variables

        public float x;
        public float y;
        public float z;
        public float w;

        #endregion Public Variables

        #endregion variables

        #region Constructors

        public Coords()
        {
            this.x = this.y = this.z = this.w = 0f;
        }

        public Coords(float same)
        {
            this.x = this.y = this.z = this.w = same;
        }

        public Coords(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = -1;
        }

        public Coords(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Coords(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Coords(Vector3 vecpos)
        {
            x = vecpos.x;
            y = vecpos.y;
            z = vecpos.z;
        }

        public Coords(Vector3 vecpos, float w)
        {
            this.x = vecpos.x;
            this.y = vecpos.y;
            this.z = vecpos.z;
            this.w = w;
        }

        public Coords(Coords source)
        {
            this.x = source.x;
            this.y = source.y;
            this.z = source.z;
            this.w = source.w;
        }



        #region Named Constructor

        public static Coords FromXY(float x, float y)
        {
            return new Coords(x, y);
        }

        public static Coords FromXYZ(float x, float y, float z)
        {
            return new Coords(x, y, z);
        }

        public static Coords FromXYZW(float x, float y, float z, float w)
        {
            return new Coords(x, y, z, w);
        }

        public static Coords MakeAPos(float x, float y, float z)
        {
            return new Coords(x, y, z, 1);
        }

        public static Coords MakeAVector(float x, float y, float z)
        {
            return new Coords(x, y, z, 0);
        }

        public static Coords MakeAPos(Vector3 vec3)
        {
            return new Coords(vec3.x, vec3.y, vec3.z, 1);
        }

        public static Coords MakeAVector(Vector3 vec3)
        {
            return new Coords(vec3.x, vec3.y, vec3.z, 0);
        }

        public static Coords Zero
        {
            get
            {
                return new Coords(0, 0, 0);
            }
        }

        #endregion Named Constructor

        #endregion Constructors

        public override string ToString()
        {
            return "(" + x + "," + y + "," + z + ")";
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }

        public Vector3 Vector3
        {
            get => new Vector3(x, y, z);
        }

        public float[] AsFloats()
        {
            float[] ret = { x, y, z, w };
            return ret;
        }

        public Matrix AsColumnMatrix()
        {
            return new Matrix(4, 1, new float[] { x, y, z, w });
        }

        public Matrix AsRowMatrix()
        {
            return new Matrix(1, 4, new float[] { x, y, z, w });
        }

        #region operators

        public static Coords operator +(Coords a, Coords b) =>
            new Coords(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Coords operator -(Coords a, Coords b) =>
            new Coords(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Coords operator *(Coords a, float factor) =>
            new Coords(a.x * factor, a.y * factor, a.z * factor);

        public static Coords operator /(Coords a, float divider) =>
            new Coords(a.x / divider, a.y / divider, a.z / divider);

        public static implicit operator Vector3(Coords c) => new Vector3(c.x, c.y, c.z);

        public static implicit operator Coords(Vector3 v) => new Coords(v.x, v.y, v.z);

        public static implicit operator Quaternion(Coords c) => new Quaternion(c.x, c.y, c.z, c.w);

        public static implicit operator Coords(Quaternion c) => new Coords(c.x, c.y, c.z, c.w);

        #endregion operators

        public float Length
        {
            get
            {
                return Mathf.Sqrt(x * x + y * y + z * z);
            }
        }

        public float SquaredLength
        {
            get
            {
                return (x * x + y * y + z * z);
            }
        }

        public Coords Normalized
        {
            get
            {
                return this / Length;
            }
        }

        public Coords Perp2D
        {
            get => new Coords(-y, x);
        }

        public Coords PerpAnother2D(Coords source)
        {
            return new Coords(-source.y, source.x);
        }

        public void Normalize()
        {
            var length = Length;
            x /= length;
            y /= length;
            z /= length;
        }

        #region Debug

        public static void DrawLine(Coords startPoint, Coords endPoint, float width, Color colour)
        {
            GameObject line = new GameObject("Line_" + startPoint.ToString() + "_" + endPoint.ToString());
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = colour;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(startPoint.x, startPoint.y, startPoint.z));
            lineRenderer.SetPosition(1, new Vector3(endPoint.x, endPoint.y, endPoint.z));
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        public static void DrawPoint(Coords position, float width, Color colour)
        {
            GameObject line = new GameObject("Point_" + position.ToString());
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = colour;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(position.x - width / 3.0f, position.y - width / 3.0f, position.z));
            lineRenderer.SetPosition(1, new Vector3(position.x + width / 3.0f, position.y + width / 3.0f, position.z));
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        #endregion Debug
    }
}