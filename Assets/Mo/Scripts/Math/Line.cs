using UnityEngine;

namespace Mo.Math
{
    public class Line
    {
        #region Types
        public enum LineType { Line, Segment, Ray };
        #endregion

        #region Variables
        public Coords A;
        public Coords B;
        public Coords v;

        LineType lineType = LineType.Line;
        #endregion

        #region Constructors
        public Line(Coords PointA, Coords PointB, LineType type = LineType.Line)
        {
            A = PointA;
            B = PointB;
            v = PointB - PointA;
            lineType = type;
        }
        #endregion

        #region Builders

        public static Line FromPoints(Coords PointA, Coords PointB, LineType type = LineType.Line)
        {
            return new Line(PointA, PointB, type);
        }

        public static Line FromPoints(Vector3 PointA, Vector3 PointB, LineType type = LineType.Line)
        {
            return new Line(PointA, PointB, type);
        }

        public static Line SegmentFromPoints(Coords PointA, Coords PointB)
        {
            return new Line(PointA, PointB, LineType.Segment);
        }

        public static Line SegmentFromPoints(Vector3 PointA, Vector3 PointB)
        {
            return new Line(PointA, PointB, LineType.Segment);
        }

        public static Line RayFromPoints(Coords PointA, Coords PointB)
        {
            return new Line(PointA, PointB, LineType.Ray);
        }

        public static Line RayFromPoints(Vector3 PointA, Vector3 PointB)
        {
            return new Line(PointA, PointB, LineType.Ray);
        }

        public static Line FromPointAndVector(Coords PointA, Coords Vector, LineType type = LineType.Line)
        {
            return new Line(PointA, PointA + Vector, type);
        }

        public static Line FromPointAndVector(Vector3 PointA, Vector3 Vector, LineType type = LineType.Line)
        {
            return new Line(PointA, PointA + Vector, type);
        }

        public static Line SegmentFromPointAndVector(Coords PointA, Coords Vector)
        {
            return new Line(PointA, PointA + Vector, LineType.Segment);
        }

        public static Line RayFromPointAndVector(Coords PointA, Coords Vector)
        {
            return new Line(PointA, PointA + Vector, LineType.Ray);
        }
        #endregion

        #region Methods
        public Coords Lerp(float t)
        {
            if (lineType == LineType.Segment)
            {
                t = Mathf.Clamp01(t);
            }
            else if (lineType == LineType.Ray)
            {
                t = Mathf.Clamp(t, 0, float.MaxValue);
            }
            return A + (v * t);
        }

        public Coords NotClampedLerp(float t)
        {
            return A + (v * t);
        }
        public Coords Reflect(Coords normal)
        {
            // Calculate r = a - 2(a.n)n
            // r reflected vector based on normal
            // a normalized incident vector, here v.Normalized
            // n normalized normal
            var normalizedNormal = normal.Normalized;
            var incidentNormalized = v.Normalized;
            var dotProduct = Math.Dot(incidentNormalized, normalizedNormal);

            if (dotProduct == 0) return v; // if incident and normal are perpendicular

            float vn2 = 2.0f * dotProduct; // 2(a.n)
            Coords r = incidentNormalized - normalizedNormal * vn2;
            return r;
        }

        public float IntersectsAt(Plane p)
        {
            if (Math.Dot(p.Normal, v) == 0)
                return float.NaN; // line is parallel to plane, no intersection exists
                                  //return HolisticMath.Dot(p.Normal * -1, A - p.point) / HolisticMath.Dot(p.Normal, v);
            return InnerIntersectAt(p);
        }

        public float IntersectsAtSameSide(Plane p)
        {
            var dot = Math.Dot(p.Normal, v);
            if (dot >= 0)
                return float.NaN;
            return InnerIntersectAt(p);
        }
        private float InnerIntersectAt(Plane p)
        {
            return Math.Dot(p.Normal * -1, A - p.point) / Math.Dot(p.Normal, v);
        }

        public float IntersectsAt(Line l)
        {
            if (Math.Dot(v.Perp2D, l.v) == 0) return float.NaN; // if both line are parallel, 
            var c = l.A - A;
            var vPerp = l.v.Perp2D;
            float t = Math.Dot(vPerp, c) / Math.Dot(vPerp, v);
            if (((t < 0.0f || t > 1.0f) && lineType == LineType.Segment) || (t < 0.0f && lineType == LineType.Ray)) return float.NaN;
            return t;
        }
        #endregion

        #region Debug
        public void Draw(float width, Color color)
        {
            Coords.DrawLine(A, B, width, color);
        }
        #endregion

    }
}
