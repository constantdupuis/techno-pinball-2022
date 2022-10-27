using UnityEngine;

namespace Mo.Math
{
    public class AABB
    {
        #region Contructors

        public AABB()
        {
        }

        public AABB(Coords min, Coords max)
        {
            this.Min = min;
            this.Max = max;
        }

        #endregion Contructors

        #region Properties

        public Coords Min { get; set; } = new Coords(float.PositiveInfinity);
        public Coords Max { get; set; } = new Coords(float.NegativeInfinity);

        #endregion Properties

        #region Methods

        /// <summary>
        /// "Add" a point in AABB, this will grow the AABB if needed
        /// </summary>
        /// <param name="point"></param>
        public void Add(Coords point)
        {
            if (point.x < Min.x) Min.x = point.x;
            if (point.x > Max.x) Max.x = point.x;

            if (point.y < Min.y) Min.y = point.y;
            if (point.y > Max.y) Max.y = point.y;

            if (point.z < Min.z) Min.z = point.z;
            if (point.z > Max.z) Max.z = point.z;
        }

        public void Add(Coords[] points)
        {
            foreach (Coords point in points)
            {
                Add(point);
            }
        }

        public void Add(Vector3[] points)
        {
            foreach (Vector3 point in points)
            {
                Add(point);
            }
        }

        public bool IsInside(Coords point)
        {
            return (point.x >= Min.x && point.x <= Max.x &&
                    point.y >= Min.y && point.y <= Max.y &&
                    point.z >= Min.z && point.z <= Max.z);
        }

        public void Reset()
        {
            Min = new Coords(float.PositiveInfinity);
            Max = new Coords(float.NegativeInfinity);
        }

        public Coords Center => (Min + Max) / 2f;
        public Coords SizeVector => Max - Min;
        public Coords RadiusVector => SizeVector / 2f;

        #endregion Methods
    }
}