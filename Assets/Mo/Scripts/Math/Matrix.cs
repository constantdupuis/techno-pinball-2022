using System;
namespace Mo.Math { 
    public class Matrix
    {
        #region variables
        float[] values;
        int rows;
        int cols;
        #endregion

        #region Constructor
        public Matrix(int rows, int cols, float[] values) {
            this.rows = rows;
            this.cols = cols;
            this.values = new float[rows*cols];
            if (values.Length != rows * cols) throw new Exception($"Matrix sizes {rows}x{cols} and value count {values.Length} doesn't match ");
            Array.Copy(values, this.values, rows*cols);
        }

        public Matrix(Matrix source) : this( source.rows, source.cols, source.values) {}

        #endregion

        #region Operators
        static public Matrix operator +(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols) //throw new Exception($"Matrix + operator error : both matrices doesn't match.");
                return null;
            Matrix result = new Matrix(a);
            int lenght = a.rows * a.cols;
            for (int i = 0; i < lenght; i++)
            { 
                result.values[i] += b.values[i];
            }
            return result;
        }

        static public Matrix operator -(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols) //throw new Exception($"Matrix - operator error : both matrices doesn't match.");
                return null;
            Matrix result = new Matrix(a);
            int lenght = a.rows * a.cols;
            for (int i = 0; i < lenght; i++)
            {
                result.values[i] -= b.values[i];
            }
            return result;
        }

        static public Matrix operator *(Matrix a, float scalar)
        {
             Matrix result = new Matrix(a);
            int lenght = a.rows * a.cols;
            for (int i = 0; i < lenght; i++)
            {
                result.values[i] *= scalar;
            }
            return result;
        }

        static public Matrix operator *(Matrix a, Matrix b)
        {
            if (a.cols != b.rows) //throw new Exception($"Matrix * operator error : both matrices doesn't conform.");
                return null;
            int resultLength = a.rows * b.cols;
            float[] values = new float[resultLength];
            for (int r = 0; r < a.rows; r++)
            {
                for (int c = 0; c < b.cols; c++)
                {
                    float value = 0.0f;
                    for (int i = 0; i < a.cols; i++)
                    { 
                        value += a.values[r * a.cols + i] * b.values[ i * b.cols + c];
                    }
                    values[r * b.cols + c] = value;
                }
            }
            return new Matrix(a.rows, b.cols, values);
        }
        #endregion

        public void MirrorX()
        {
            if (rows != 4 || cols != 4) return;
            SetAt(0,0, -GetAt(0, 0));
        }

        public void MirrorY()
        {
            if (rows != 4 || cols != 4) return;
            SetAt(1, 1, -GetAt(1, 1));
        }

        public void MirrorZ()
        {
            if (rows != 4 || cols != 4) return;
            SetAt(2,2, -GetAt(2, 2));
        }

        public Coords AsCoords()
        {
            if (rows == 4 && cols == 1)
            {
                return new Coords(values[0], values[1], values[2], values[3]);
            }
            return null;
        }

        public float GetAt(uint row, uint col)
        {
            if (row >= rows || col >= cols) return float.NaN;
            return values[row * cols + col];
        }

        public void SetAt(uint row, uint col, float value)
        {
            if (row >= rows || col >= cols) return;
            values[row * cols + col] = value;
        }

        public override string ToString()
        {
            string matrix = "";
            for( int i = 0; i < values.Length; i++)
            {
                if (i % cols == 0) matrix += "\n";
                matrix += values[i].ToString().PadLeft(4,' ');
            }
            return matrix;
        }
    }

}
