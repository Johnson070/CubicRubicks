using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using static CubeRubic_s.ModelCube;
using static CubeRubic_s.SubCubeRubic;

namespace CubeRubic_s
{
    public class SubCubeRubic
    {
        //public ColorsCube ColorUp = ColorsCube.BLACK;
        //public ColorsCube ColorDown = ColorsCube.BLACK;
        //public ColorsCube ColorRight = ColorsCube.BLACK;
        //public ColorsCube ColorLeft = ColorsCube.BLACK;
        //public ColorsCube ColorFront = ColorsCube.BLACK;
        //public ColorsCube ColorBack = ColorsCube.BLACK;

        public ColorSidesCube ColorSides3D = new ColorSidesCube();
        public ColorSidesCube ColorSidesMap = new ColorSidesCube();

        public static float sizeSubCube = 1.0f;

        public float[] pos;
        public float[][][] posPoint;

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

        public void SetColor(ColorsCube color, type type)
        {
            if (type == type.Up) this.ColorSides3D.Up = color;
            if (type == type.Down) this.ColorSides3D.Down = color;
            if (type == type.Right) this.ColorSides3D.Right = color;
            if (type == type.Left) this.ColorSides3D.Left = color;
            if (type == type.Front) this.ColorSides3D.Front = color;
            if (type == type.Back) this.ColorSides3D.Back = color;

            this.ColorSidesMap = this.ColorSides3D.DeepCopy();
        }

        public class ColorSidesCube
        {
            public ColorsCube Up = ColorsCube.BLACK;
            public ColorsCube Down = ColorsCube.BLACK;
            public ColorsCube Right = ColorsCube.BLACK;
            public ColorsCube Left = ColorsCube.BLACK;
            public ColorsCube Front = ColorsCube.BLACK;
            public ColorsCube Back = ColorsCube.BLACK;

            //public static bool operator != (ColorSidesCube cb1, ColorSidesCube cb2)
            //{
            //    return (cb1.Up == cb2.Up) && (cb1.Down == cb2.Down) && (cb1.Left == cb2.Left) && (cb1.Right == cb2.Right) && (cb1.Front == cb2.Front) && (cb1.Back == cb2.Back);
            //}

            //public static bool operator == (ColorSidesCube cb1, ColorSidesCube cb2)
            //{
            //    return (cb1.Up != cb2.Up) || (cb1.Down != cb2.Down) || (cb1.Left != cb2.Left) || (cb1.Right != cb2.Right) || (cb1.Front != cb2.Front) || (cb1.Back != cb2.Back);
            //}

            public ColorSidesCube(ColorsCube Up, ColorsCube Down, ColorsCube Right, ColorsCube Left, ColorsCube Front, ColorsCube Back)
            {
                this.Up = Up;
                this.Down = Down;
                this.Right = Right;
                this.Left = Left;
                this.Front = Front;
                this.Back = Back;
            }

            public ColorSidesCube() { }

            public ColorSidesCube RotateXY(bool reverse) => 
                reverse ? 
                new ColorSidesCube() { Down = Front, Back = Down, Up = Back, Front = Up, Left = Left, Right = Right} :
                new ColorSidesCube() { Back = Up, Down = Back, Front = Down, Up = Front, Right = Right, Left = Left};

            public ColorSidesCube RotateYZ(bool reverse) =>
                reverse ?
                new ColorSidesCube() { Left = Up, Down = Left, Right = Down, Up = Right, Back = Back, Front = Front} :
                new ColorSidesCube() { Right = Up, Down = Right, Left = Down, Up = Left, Back = Back, Front = Front };

            public ColorSidesCube RotateXZ(bool reverse) =>
                reverse ?
                new ColorSidesCube() { Right = Front, Back = Right, Left = Back, Front = Left, Down = Down, Up = Up} :
                new ColorSidesCube() { Right = Back, Front = Right, Left = Front, Back = Left, Down = Down, Up = Up };

            public ColorSidesCube DeepCopy() => new ColorSidesCube(Up, Down, Right, Left, Front, Back);
        }

        //public float[] GetCenterSubCube() => new float[] { (posPoint[0][0][0] + posPoint[0][1][0])/2.0f, (posPoint[0][1][1] + posPoint[1][1][1]) / 2.0f, (posPoint[0][1][2] + posPoint[0][2][2]) / 2.0f };

        //public float MinWall(WallCube cube)
        //{
        //    float minimum = 0.0f;
        //    for (int i = 0; i < posPoint.Length; i++)
        //        for (int j = 0; j < posPoint[i].Length; j++)
        //            minimum = Math.Min(minimum,posPoint[i][j][(int)cube]);
        //    return minimum;
        //}

        //public float MaxWall(WallCube cube)
        //{
        //    float maximum = 0.0f;
        //    for (int i = 0; i < posPoint.Length; i++)
        //        for (int j = 0; j < posPoint[i].Length; j++)
        //            maximum = Math.Max(maximum, posPoint[i][j][(int)cube]);
        //    return maximum;
        //}

        //public void FillColor(ColorsCube col)
        //{
        //    ColorUp = col;
        //    ColorDown = col;
        //    ColorLeft = col;
        //    ColorRight = col;
        //    ColorFront = col;
        //    ColorBack = col;
        //}

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

    public static class CubeEXT
    {
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

        public static float[] SetRadiusCamera(this float[] camera, float addRadius)
        {
            double R = Math.Sqrt(Math.Pow(camera[0], 2.0f) + Math.Pow(camera[1], 2.0f) + Math.Pow(camera[2], 2.0f));

            if (R == 0.0f)
                return camera;

            double theta = Math.PI / 2 - Math.Asin(camera[1] / R);
            double phi = Math.Acos(camera[2] / R);

            R += addRadius;

            //if (theta < 0) theta = Math.PI - theta;
            float x = (float)Math.Round(Convert.ToSingle(R * Math.Sin(theta) * Math.Cos(phi)), 10);
            float y = (float)Math.Round(Convert.ToSingle(R * Math.Sin(theta) * Math.Sin(phi)), 10);
            float z = (float)Math.Round(Convert.ToSingle(R * Math.Cos(theta)), 4);

            return new float[] { x, y, z };
        }
        public static float[] GetFroatColor(this ColorsCube color)
        {
            Color outColor;
            switch (color)  
            {
                case ColorsCube.RED:
                    outColor = Color.Red;
                    break;
                case ColorsCube.WHITE:
                    outColor = Color.White;
                    break;
                case ColorsCube.ORANGE:
                    outColor = Color.Orange;
                    break;
                case ColorsCube.YELLOW:
                    outColor = Color.Yellow;
                    break;
                case ColorsCube.GREEN:
                    outColor = Color.Green;
                    break;
                case ColorsCube.BLUE:
                    outColor = Color.Blue;
                    break;
                default:
                    outColor = Color.Black;
                    break;
            }
            return new float[] { outColor.R / 255.0f, outColor.G / 255.0f, outColor.B / 255.0f };
        }

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

        //public ColorSidesCube GetColorsSubBlock(this float[] pos)
        //{
        //    ColorSidesCube outSides = new ColorSidesCube();



        //    return outSides;
        //}
    }
}
