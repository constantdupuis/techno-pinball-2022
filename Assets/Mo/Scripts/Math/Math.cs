
using UnityEngine;

namespace Mo.Math { 
    public class Math
    {
        static public float Square(float value)
        {
            return value * value;
        }

        static public float Distance(Coords point1, Coords point2)
        {
            ////if you are interested in how the Sqrt is calculated see
            ////https://www.codeproject.com/Articles/570700/SquareplusRootplusalgorithmplusforplusC
            //return squareRoot;
            return (point2 - point1).Length;

        }

        static public float SquaredDistance(Coords point1, Coords point2)
        {
            return (point2 - point1).SquaredLength;

        }

        // Lerp between point A and B
        static public Coords Lerp( Coords from, Coords to, float t)
        {
            Coords v = to - from;
            return from + v * t;
        }

        static public Coords Lerp01(Coords from, Coords to, float t)
        {
            t = Mathf.Clamp01( t );
            Coords v = to - from;
            return from + v * t;
        }

        static public Coords LerpRay(Coords from, Coords to, float t)
        {
            if (t < 0.0f) t = 0.0f;
            Coords v = to - from;
            return from + v * t;
        }

        // Lerp from A along direction v
        static public Coords LerpDirection( Coords origin, Coords v, float t)
        {
            return origin + v * t;
        }

        static public Coords LerpDirection01(Coords origin, Coords v, float t)
        {
            t = Mathf.Clamp01( t );
            return origin + v * t;
        }

        static public Coords LerpDirectionRay(Coords origin, Coords v, float t)
        {
            if (t < 0.0f) t = 0.0f;
            return origin + v * t;
        }

        static public float Length(Coords vector)
        {
            //float sumSquared = Square(vector.x) + Square(vector.y) + Square(vector.z);
            //return Mathf.Sqrt(sumSquared);
            return vector.Length;
        }

        static public float SquaredLength(Coords vector)
        {
            //float sumSquared = Square(vector.x) + Square(vector.y) + Square(vector.z);
            //return Mathf.Sqrt(sumSquared);
            return vector.SquaredLength;
        }

        static public Coords GetNormal(Coords vector)
        {
            //float length = Distance(new Coords(0, 0, 0), vector);
            //vector.x /= length;
            //vector.y /= length;
            //vector.z /= length;

            return vector.Normalized;
        }

        static public float Dot(Coords vector1, Coords vector2)
        {
            return (vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z);
        }

        static public float Angle(Coords vector1, Coords vector2)
        {
            //float dotDivide = Dot(vector1, vector2) / (Distance(new Coords(0, 0, 0), vector1) * Distance(new Coords(0, 0, 0), vector2));
            //float dotDivide = Dot(vector1, vector2) / (Length(vector1) * Length(vector2));
            float dotDivide = Dot(vector1, vector2) / (vector1.Length * vector2.Length);
            return Mathf.Acos(dotDivide);  //radians.  For degrees * 180/Mathf.PI;
        }

        static public Coords Cross(Coords vector1, Coords vector2)
        {
            float iMult = vector1.y * vector2.z - vector1.z * vector2.y;
            float jMult = vector1.z * vector2.x - vector1.x * vector2.z;
            float kMult = vector1.x * vector2.y - vector1.y * vector2.x;
            return  new Coords(iMult, jMult, kMult);
        }

        static public Coords Rotate2D(Coords position, float angle, bool clockwise) // angle in radians
        {
            if (clockwise)
                angle = 2*Mathf.PI - angle;

            float sinAngle = Mathf.Sin(angle);
            float cosAngle = Mathf.Cos(angle);

            float xVal = position.x * cosAngle - position.y * sinAngle;
            float yVal = position.x * sinAngle + position.y * cosAngle;
            return new Coords(xVal, yVal, 0); 
        }

        static public Coords LookAt2D(Coords currentDirection, Coords from, Coords target)
        {
            var newDirection = target - from;
            //var normalizedDirection = newDirection.Normalized;
            float angleToRotate = Angle(currentDirection, newDirection);

            bool clockWize = false;
            if( Math.Cross(currentDirection, newDirection).z < 0){
                clockWize = true;   
            }

            return  Math.Rotate2D(currentDirection, angleToRotate, clockWize);
        }

        static public Coords Translate(Coords position, Coords translation)
        {
            //float[] scaleValues = { 0, 0, 0, translation.x,
            //                        0, 0, 0, translation.y,
            //                        0, 0, 0, translation.z,
            //                        0, 0, 0, 1};
            //var translateMatrix = new Matrix(4, 4, scaleValues);
            var translateMatrix = MatrixBuilder.Make4x4Translate(translation);
            var result = translateMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords Translate(Coords position, Matrix translation)
        {
            var result = translation * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords Scale(Coords position, Coords scale)
        {
            //float[] scaleValues = { scale.x, 0, 0, 0,
            //                        0, scale.y, 0, 0,
            //                        0, 0, scale.y, 0,
            //                        0, 0, 0, 1};
            //var scaleMatrix = new Matrix(4, 4, scaleValues);
            var scaleMatrix = MatrixBuilder.Make4x4Scale(scale);
            var result = scaleMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords Scale(Coords position, Matrix scaleMatrix)
        {
            var result = scaleMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        /// <summary>
        /// Not working !
        /// </summary>
        /// <param name="position"></param>
        /// <param name="shearX"></param>
        /// <param name="shearY"></param>
        /// <param name="shearZ"></param>
        /// <returns></returns>
        static public Coords Shear(Coords position, float shearX, float shearY, float shearZ)
        {
            //float[] shearValues = { 1, shearY, 0, 0,
            //                        0, 1, 0, 0,
            //                        0, 0, 1, 0,
            //                        0, 0, 0, 1};
            //var shearMatrix = new Matrix(4, 4, shearValues);
            var shearMatrix = MatrixBuilder.Make4x4Shear(new Coords(shearX, shearY, shearZ));
            var result = shearMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords Shear(Coords position, Matrix shearMatrix)
        {
            var result = shearMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords Mirror(Coords position, bool mirrorX, bool mirrorY, bool mirrorZ)
        { 
            var indentityMatrix = MatrixBuilder.Make4x4Identity();
            if (mirrorX) indentityMatrix.MirrorX();
            if (mirrorY) indentityMatrix.MirrorY();
            if (mirrorZ) indentityMatrix.MirrorZ();
            var result = indentityMatrix * position.AsColumnMatrix();
            return result.AsCoords();
        }

        static public Coords EulerRotate(Coords position, float angleX, bool clockwiseX,
                                                     float angleY, bool clockwiseY,
                                                     float angleZ, bool clockwiseZ)
        {
            Matrix result;

            var rotationMatrix = Math.GetRotationMatrix( angleX, clockwiseX,
                                                                 angleY, clockwiseY,
                                                                 angleZ, clockwiseZ);
            result = rotationMatrix * position.AsColumnMatrix();

            return result.AsCoords();
        }

        static public Matrix GetRotationMatrix(float angleX, bool clockwiseX,
                                               float angleY, bool clockwiseY,
                                               float angleZ, bool clockwiseZ)
        {
            return MatrixBuilder.Make4x4Rotation(angleX, clockwiseX, angleY, clockwiseY, angleZ, clockwiseZ);
        }

        /// <summary>
        /// Return the rotation angle from a rotation matrix
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns>Rotation angle in radian</returns>
        static public float GetRotationAxisAngle(Matrix rotation)
        {
            float angle = 0;
            angle = Mathf.Acos( 0.5f * (rotation.GetAt(0, 0) + 
                                        rotation.GetAt(1, 1) + 
                                        rotation.GetAt(2, 2) + 
                                        rotation.GetAt(3, 3) - 2));
            return angle;
        }

        /// <summary>
        /// Return the rotation axis from a rotation matrix
        /// </summary>
        /// <param name="rotation"></param>
        /// <param name="angleInRadian"></param>
        /// <returns></returns>
        static public Coords GetRotationAxis(Matrix rotation, float angleInRadian)
        {
            Coords axis = new Coords(0,0,0);
            float twoSin = 2 * Mathf.Sin(angleInRadian);
            axis.x = (rotation.GetAt(2, 1) - rotation.GetAt(1, 2)) / twoSin;
            axis.y = (rotation.GetAt(0, 2) - rotation.GetAt(2, 0)) / twoSin;
            axis.z = (rotation.GetAt(1, 0) - rotation.GetAt(0, 1)) / twoSin;
            return axis;
        }

        /// <summary>
        /// Return Quaternion values for a rotation of angleInDegrees angle around the axis axis 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angleInDegrees">Rotation angle in degrees</param>
        /// <returns></returns>
        static public Coords Quaternion(Coords axis, float angleInDegree)
        {
            Coords aN = axis.Normalized;
            var angleInRadienBy2 = (angleInDegree * Mathf.Deg2Rad) / 2f;
            float w = Mathf.Cos(angleInRadienBy2);
            float s = Mathf.Sin(angleInRadienBy2);
            Coords q = new Coords(aN.x * s, aN.y * s, aN.z * s, w);
            return q;
        }

        static public Coords IdentityQuaternion()
        {
            return new Coords(0f,0f,0f, 1f);
        }

        static public Coords QuaternionRotate(Coords position, Coords quaternion)
        {
            float x = quaternion.x;
            float y = quaternion.y;
            float z = quaternion.z;
            float w = quaternion.w;

            float[] values =
            {
                1 - 2*y*y - 2*z*z, 2*x*y - 2*w*z, 2*x*z + 2*w*y, 0,
                2*x*y + 2*w*z, 1 - 2*x*x - 2*z*z, 2*y*z - 2*w*x, 0,
                2*x*z - 2*w*y, 2*y*z + 2*w*x, 1 - 2*x*x - 2*y*y,0,
                0,0,0,1
            };

            Matrix rotationMatrix = new Matrix(4, 4, values);
            var result = rotationMatrix * position.AsColumnMatrix();

            return result.AsCoords();
        }

        static public Coords QuaternionRotate(Vector3 position, Vector3 axis, float angleInDegree)
        {
            return QuaternionRotate(new Coords(position), new Coords(axis), angleInDegree);
        }

        static public Coords QuaternionRotate(Coords position, Coords axis, float angleInDegree)
        {
            var normalizedAxis = axis.Normalized;
            var angleInRadienBy2 = (angleInDegree * Mathf.Deg2Rad) / 2f;
            float w = Mathf.Cos(angleInRadienBy2);
            float s = Mathf.Sin(angleInRadienBy2);
            Vector3 q = (normalizedAxis * s).Vector3;
            float x = q.x;
            float y = q.y;
            float z = q.z;

            float[] values =
            {
                1 - 2*y*y - 2*z*z, 2*x*y - 2*w*z, 2*x*z + 2*w*y, 0,
                2*x*y + 2*w*z, 1 - 2*x*x - 2*z*z, 2*y*z - 2*w*x, 0,
                2*x*z - 2*w*y, 2*y*z + 2*w*x, 1 - 2*x*x - 2*y*y,0,
                0,0,0,1
            };

            Matrix rotationMatrix = new Matrix(4, 4, values);
            var result = rotationMatrix * position.AsColumnMatrix();

            return result.AsCoords();
        }
    }

}
