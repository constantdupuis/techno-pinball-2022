using UnityEngine;

namespace Mo.Math
{
    public class MatrixBuilder
    {
        public static Matrix Make4x4Zero()
        {
            float[] values = { 0, 0, 0, 0,
                           0, 0, 0, 0,
                           0, 0, 0, 0,
                           0, 0, 0, 0};
            var zeroMatrix = new Matrix(4, 4, values);
            return zeroMatrix;
        }

        public static Matrix Make4x4Identity()
        {
            float[] values = { 0, 0, 0, 0,
                           0, 0, 0, 0,
                           0, 0, 0, 0,
                           0, 0, 0, 0};
            var identityMatrix = new Matrix(4, 4, values);
            return identityMatrix;
        }

        public static Matrix Make4x4Translate(Coords translation)
        {
            float[] scaleValues = { 0, 0, 0, translation.x,
                                0, 0, 0, translation.y,
                                0, 0, 0, translation.z,
                                0, 0, 0, 1};
            var translateMatrix = new Matrix(4, 4, scaleValues);
            return translateMatrix;
        }

        public static Matrix Make4x4Scale(Coords scale)
        {
            float[] scaleValues = { scale.x, 0, 0, 0,
                                0, scale.y, 0, 0,
                                0, 0, scale.y, 0,
                                0, 0, 0, 1};
            var scaleMatrix = new Matrix(4, 4, scaleValues);
            return scaleMatrix;
        }

        public static Matrix Make4x4Shear(Coords shear)
        {
            float[] shearValues = { 1, shear.y, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1};
            var shearMatrix = new Matrix(4, 4, shearValues);
            return shearMatrix;
        }

        public static Matrix Make4x4Rotation(float angleXInRadian, bool clockwiseX,
                                             float angleYInRadian, bool clockwiseY,
                                             float angleZInRadian, bool clockwiseZ)
        {
            if (!clockwiseX) angleXInRadian = -angleXInRadian;
            float angleXSin = Mathf.Sin(angleXInRadian);
            float angleXCos = Mathf.Cos(angleXInRadian);
            float[] xRotationValues = { 1, 0, 0, 0,
                                    0, angleXCos, -angleXSin, 0,
                                    0, angleXSin, angleXCos, 0,
                                    0, 0, 0, 1};
            var xRotationMatrix = new Matrix(4, 4, xRotationValues);

            if (!clockwiseY) angleYInRadian = -angleYInRadian;
            float angleYSin = Mathf.Sin(angleYInRadian);
            float angleYCos = Mathf.Cos(angleYInRadian);
            float[] yRotationValues = { angleYCos, 0, angleYSin, 0,
                                    0, 1, 0, 0,
                                    -angleYSin, 0, angleYCos, 0,
                                    0, 0, 0, 1};
            var yRotationMatrix = new Matrix(4, 4, yRotationValues);

            if (!clockwiseZ) angleZInRadian = -angleZInRadian;
            float angleZSin = Mathf.Sin(angleZInRadian);
            float angleZCos = Mathf.Cos(angleZInRadian);
            float[] zRotationValues = { angleZCos, -angleZSin, 0, 0,
                                    angleZSin, angleZCos, 0, 0,
                                    0, 0, 1, 0,
                                    0, 0, 0, 1};
            var zRotationMatrix = new Matrix(4, 4, zRotationValues);

            return zRotationMatrix * yRotationMatrix * xRotationMatrix;
        }
    }
}