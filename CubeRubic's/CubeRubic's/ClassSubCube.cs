using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace CubeRubic_s
{
    public class SubCubeRubic
    {
        public Color ColorUp = Color.Black;
        public Color ColorDown = Color.Black;
        public Color ColorRight = Color.Black;
        public Color ColorLeft = Color.Black;
        public Color ColorFront = Color.Black;
        public Color ColorBack = Color.Black;

        static float sizeSubCube = 1.0f;

        public float[] pos;
        public float[][][] posPoint;

        public enum Colors
        {
            Red,
            Blue,
            Orange,
            White,
            Yellow,
            Green
        }

        public enum type
        {
            Up,
            Down,
            Right,
            Left,
            Front,
            Back
        }

        public enum WallCube : int
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        public SubCubeRubic() { }

        public SubCubeRubic(float[] pos)
        {
            this.pos = new float[] { pos[0], pos[1], pos[2] };
            
            Recalculate();
        }

        private void Recalculate() 
        {
            float get2Cube = (sizeSubCube / 2f);

            posPoint = new float[2][][];

            for (int i = 0; i < posPoint.Length; i++)
            {
                posPoint[i] = new float[4][];

                posPoint[i][0] = new float[3];
                posPoint[i][0][0] = pos[0] - get2Cube;
                posPoint[i][0][1] = pos[1] + (get2Cube * (i == 0 ? 1 : -1));
                posPoint[i][0][2] = pos[2] - get2Cube;

                posPoint[i][1] = new float[3];
                posPoint[i][1][0] = pos[0] + get2Cube;
                posPoint[i][1][1] = pos[1] + (get2Cube * (i == 0 ? 1 : -1));
                posPoint[i][1][2] = pos[2] - get2Cube;

                posPoint[i][2] = new float[3];
                posPoint[i][2][0] = pos[0] + get2Cube;
                posPoint[i][2][1] = pos[1] + (get2Cube * (i == 0 ? 1 : -1));
                posPoint[i][2][2] = pos[2] + get2Cube;

                posPoint[i][3] = new float[3];
                posPoint[i][3][0] = pos[0] - get2Cube;
                posPoint[i][3][1] = pos[1] + (get2Cube * (i == 0 ? 1 : -1));
                posPoint[i][3][2] = pos[2] + get2Cube;
            }
        }

        public void SetColor(Colors color, type type)
        {
            Color selectColor = Color.Black;
            switch (color)
            {
                case Colors.Red:
                    selectColor = Color.Red;
                    break;
                case Colors.Blue:
                    selectColor = Color.Blue;
                    break;
                case Colors.Orange:
                    selectColor = Color.Orange;
                    break;
                case Colors.White:
                    selectColor = Color.White;
                    break;
                case Colors.Yellow:
                    selectColor = Color.Yellow;
                    break;
                case Colors.Green:
                    selectColor = Color.Green;
                    break;
                default:
                    break;
            }

            if (type == type.Up) this.ColorUp = selectColor;
            if (type == type.Down) this.ColorDown = selectColor;
            if (type == type.Right) this.ColorRight = selectColor;
            if (type == type.Left) this.ColorLeft = selectColor;
            if (type == type.Front) this.ColorFront = selectColor;
            if (type == type.Back) this.ColorBack = selectColor;
        }

        public float[] GetCenterSubCube() => new float[] { (posPoint[0][0][0] + posPoint[0][1][0])/2.0f, (posPoint[0][1][1] + posPoint[1][1][1]) / 2.0f, (posPoint[0][1][2] + posPoint[0][2][2]) / 2.0f };

        public float MinWall(WallCube cube)
        {
            float minimum = 0.0f;
            for (int i = 0; i < posPoint.Length; i++)
                for (int j = 0; j < posPoint[i].Length; j++)
                    minimum = Math.Min(minimum,posPoint[i][j][(int)cube]);
            return minimum;
        }

        public float MaxWall(WallCube cube)
        {
            float maximum = 0.0f;
            for (int i = 0; i < posPoint.Length; i++)
                for (int j = 0; j < posPoint[i].Length; j++)
                    maximum = Math.Max(maximum, posPoint[i][j][(int)cube]);
            return maximum;
        }

        public void FillColor(Color col)
        {
            ColorUp = col;
            ColorDown = col;
            ColorLeft = col;
            ColorRight = col;
            ColorFront = col;
            ColorBack = col;
        }
        
        //public CubeRubics(float[] pos, Colors color, type type) 
        //{
        //    Color selectColor = Color.Black;
        //    switch (color)
        //    {
        //        case Colors.Red:
        //            selectColor = Color.Red;
        //            break;
        //        case Colors.Blue:
        //            selectColor = Color.Blue;
        //            break;
        //        case Colors.Orange:
        //            selectColor = Color.Orange;
        //            break;
        //        case Colors.White:
        //            selectColor = Color.White;
        //            break;
        //        case Colors.Yellow:
        //            selectColor = Color.Yellow;
        //            break;
        //        case Colors.Green:
        //            selectColor = Color.Green;
        //            break;
        //        default:
        //            break;
        //    }

        //    this.pos = new float[] { pos[0], pos[1], pos[2] };
        //    if (type == type.Up) this.ColorUp = selectColor;
        //    if (type == type.Down) this.ColorDown = selectColor;
        //    if (type == type.Right) this.ColorRight = selectColor;
        //    if (type == type.Left) this.ColorLeft = selectColor;
        //    if (type == type.Front) this.ColorFront = selectColor;
        //    if (type == type.Back) this.ColorBack = selectColor;
        //}

        //public void RotateRL(CubeRubics[][][] cr)
        //{
        //    CubeRubics[][][] Cube = new CubeRubics[][][]
        //{
        //    new CubeRubics[][]
        //    {
        //        new CubeRubics[] {},
        //        new CubeRubics[] {},
        //        new CubeRubics[] {}
        //    },
        //    new CubeRubics[][]
        //    {
        //        new CubeRubics[] {},
        //        new CubeRubics[] {},
        //        new CubeRubics[] {}
        //    },
        //    new CubeRubics[][]
        //    {
        //        new CubeRubics[] {},
        //        new CubeRubics[] {},
        //        new CubeRubics[] {}
        //    }
        //};
        //}
    }

    public static class ColorExt
    {
        //public static float[] GetCenter(this float[][][] points)
        //{
        //    float[] maximum = new float[] { 0.0f,0.0f,0.0f};
        //    float[] minimum = new float[] { 0.0f, 0.0f, 0.0f };
        //    for (int i = 0; i < points.Length; i++)
        //        for (int j = 0; j < points[i].Length; j++)
        //            for (int k = 0; k < 3; k++)
        //            {
        //                maximum[k] = Math.Max(maximum[k], points[i][j][k]);
        //                minimum[k] = Math.Min(maximum[k], points[i][j][k]);
        //            }
        //    return new float[] { (minimum[0] + maximum[0]) / 2, (minimum[1] + maximum[1]) / 2, (minimum[2] + maximum[2]) / 2 };
        //}

        private static float[] getRotatePoint(float x0, float y0, float angle)
        {
            double R = Math.Sqrt(Math.Pow(x0, 2.0f) + Math.Pow(y0, 2.0f));

            if (R == 0.0f)
                return new float[] { x0, y0 };

            double angle0 = Math.Asin((y0 / R));
            //if (angle0 < 0) angle0 += Math.PI * 2.0f;
            if (x0 < 0) angle0 = Math.PI - angle0;
            float x = (float)Math.Round(Convert.ToSingle(Math.Cos((angle * Math.PI) / 180f + angle0) * R),4);
            float y = (float)Math.Round(Convert.ToSingle(Math.Sin((angle * Math.PI) / 180f + angle0) * R), 4);

            return new float[] { x, y };
        }
        public static float[] GetFroatColor(this Color color) => new float[] { color.R / 255.0f, color.G / 255.0f, color.B / 255.0f };

        public static float[] RotateXY(this float[] pos, float angle)
        {
            float[] pos_next = getRotatePoint(pos[0], pos[1], angle);
            return new float[] { pos_next[0], pos_next[1], pos[2] };
        }
        public static float[] RotateXZ(this float[] pos, float angle)
        {
            float[] pos_next = getRotatePoint(pos[0], pos[2], angle);
            return new float[] { pos_next[0], pos[1], pos_next[1] };
        }
        public static float[] RotateYZ(this float[] pos, float angle)
        {
            float[] pos_next = getRotatePoint(pos[1], pos[2], angle);
            return new float[] { pos[0], pos_next[0], pos_next[1] };
        }
    }
}
