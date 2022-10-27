using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mo.Math
{
    public class Plane
    {
        #region Types
        public enum ClampMode { None, ZeroOne, ZeroInfinity };
        #endregion

        #region Variables
        public Coords point;
        public Coords v;
        public Coords u;
        #endregion

        #region Constructors
        public Plane(Coords point, Coords vectorU, Coords vectorV)
        {
            this.point = point;
            u = vectorU;
            v = vectorV;
        }
        #endregion

        #region Builders
        /// <summary>
        /// Create a plane from 3 points
        /// Plane is create from PointA and with vector PointB - PointA and PointC - PointA
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns>Created Plane</returns>
        static public Plane FromPoints(Coords pointA, Coords pointB, Coords pointC)
        {
            return new Plane(pointA, pointB - pointA, pointC - pointA);
        }
        /// <summary>
        /// Create a plane from 3 points
        /// Plane is create from PointA and with vector PointB - PointA and PointC - PointA 
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns>Created Plane</returns>
        static public Plane FromPoints(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            return new Plane(pointA, (pointB - pointA), (pointC - pointA));
        }

        static public Plane FromPointAndVectors(Coords point, Coords vectorU, Coords vectorV)
        {
            return new Plane(point, vectorU, vectorV);
        }

        static public Plane FromPointAndVectors(Vector3 point, Vector3 vectorU, Vector3 vectorV)
        {
            return new Plane(point, vectorU, vectorV);
        }
        #endregion

        #region Methods
        public Coords Normal
        {
            get
            {
                return Math.Cross(u, v);
            }
        }

        public Coords Lerp(float t, float s, ClampMode clampMode = ClampMode.None)
        {
            if (clampMode == ClampMode.ZeroOne)
            {
                t = Mathf.Clamp01(t);
                s = Mathf.Clamp01(s);
            }
            else if (clampMode == ClampMode.ZeroInfinity)
            {
                t = Mathf.Clamp(t, 0, float.MaxValue);
                s = Mathf.Clamp(s, 0, float.MaxValue);
            }
            return point + u * t + v * s;
        }
        #endregion

    }
}