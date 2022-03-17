using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeRubic_s
{
    public class CubeWork
    {
        public const float sizeSubCube = 1.0f;
        public enum RotateType : int
        {
            Up = 0b000_0,
            Down = 0b001_0,
            Right = 0b010_0,
            Left = 0b011_0,
            Front = 0b100_0,
            Back = 0b101_0,

            Up_ = Up | 0b000_1,
            Down_ = Down | 0b000_1,
            Right_ = Right | 0b000_1,
            Left_ = Left | 0b000_1,
            Front_ = Front | 0b000_1,
            Back_ = Back | 0b000_1,

            reverse = 0b000_1
        }

        public SubCubeRubic[][][] Cube = new SubCubeRubic[][][]
        {
            new SubCubeRubic[][]
            {
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, sizeSubCube, -sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, sizeSubCube, -sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, sizeSubCube, -sizeSubCube })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, sizeSubCube, 0.0f }), new SubCubeRubic(new float[] { 0.0f, sizeSubCube, 0.0f }),new SubCubeRubic(new float[] { sizeSubCube, sizeSubCube, 0.0f })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, sizeSubCube, sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, sizeSubCube, sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, sizeSubCube, sizeSubCube })}
            },
            new SubCubeRubic[][]
            {
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, 0.0f, -sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, 0.0f, -sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, 0.0f, -sizeSubCube })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, 0.0f, 0.0f }), null, new SubCubeRubic(new float[] { sizeSubCube, 0.0f, 0.0f })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, 0.0f, sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, 0.0f, sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, 0.0f, sizeSubCube })}
            },
            new SubCubeRubic[][]
            {
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, -sizeSubCube, -sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, -sizeSubCube, -sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, -sizeSubCube, -sizeSubCube })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, -sizeSubCube, 0.0f }), new SubCubeRubic(new float[] { 0.0f, -sizeSubCube, 0.0f }),new SubCubeRubic(new float[] { sizeSubCube, -sizeSubCube, 0.0f })},
                new SubCubeRubic[] { new SubCubeRubic(new float[] { -sizeSubCube, -sizeSubCube, sizeSubCube }), new SubCubeRubic(new float[] { 0.0f, -sizeSubCube, sizeSubCube }),new SubCubeRubic(new float[] { sizeSubCube, -sizeSubCube, sizeSubCube })}
            }
        };

        public CubeWork()
        {
            for (int i = 0; i < Cube.Length; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < Cube[i].Length; j++)
                    {
                        for (int k = 0; k < Cube[i][j].Length; k++)
                        {
                            Cube[i][j][k].SetColor(ModelCube.ColorsCube.WHITE, SubCubeRubic.type.Up);
                        }

                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);
                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);
                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                    }
                }
                else if (i == 1)
                {
                    for (int j = 0; j < Cube[i].Length; j++)
                    {
                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);
                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);
                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                    }
                }
                else if (i == 2)
                {
                    for (int j = 0; j < Cube[i].Length; j++)
                    {
                        for (int k = 0; k < Cube[i][j].Length; k++)
                        {
                            Cube[i][j][k].SetColor(ModelCube.ColorsCube.YELLOW, SubCubeRubic.type.Down);
                        }

                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);

                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.BLUE, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.ORANGE, SubCubeRubic.type.Back);
                            Cube[i][j][0].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);

                            Cube[i][j][1].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.GREEN, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(ModelCube.ColorsCube.RED, SubCubeRubic.type.Front);
                        }

                    }
                }
            }

            //Cube[0][0][0].FillColor(System.Drawing.Color.Purple);
        }

        public void RotateCube(RotateType rt, bool reverse = false, float angle = 90)
        {
            if (((byte)rt & 0b0001) == 0b1)
            {
                reverse = true;
                rt = (RotateType)((byte)rt & 0b1110);
            }

            foreach (var slice in Cube)
                foreach (var row in slice)
                    foreach (var subCube in row)
                    {
                        if (subCube == null) continue;

                        for (int i = 0; i < subCube.posPoint.Length; i++)
                            for (int j = 0; j < subCube.posPoint[i].Length; j++)
                            {
                                if (rt == RotateType.Right && subCube.pos[2] == -sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateXY(angle * (reverse ? -1 : 1));

                                if (rt == RotateType.Left && subCube.pos[2] == sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateXY(angle * (!reverse ? -1 : 1));

                                if (rt == RotateType.Up && subCube.pos[1] == sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateXZ(angle * (reverse ? -1 : 1));

                                if (rt == RotateType.Down && subCube.pos[1] == -sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateXZ(angle * (!reverse ? -1 : 1));

                                if (rt == RotateType.Front && subCube.pos[0] == sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateYZ(angle * (!reverse ? -1 : 1));

                                if (rt == RotateType.Back && subCube.pos[0] == -sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateYZ(angle * (reverse ? -1 : 1));
                            }

                        if (rt == RotateType.Right && subCube.pos[2] == -sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateXY(angle * (reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateXY(reverse);
                        }

                        if (rt == RotateType.Left && subCube.pos[2] == sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateXY(angle * (!reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateXY(!reverse);
                        }

                        if (rt == RotateType.Up && subCube.pos[1] == sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateXZ(angle * (reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateXZ(reverse);
                        }
                        if (rt == RotateType.Down && subCube.pos[1] == -sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateXZ(angle * (!reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateXZ(!reverse);
                        }
                        if (rt == RotateType.Front && subCube.pos[0] == sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateYZ(angle * (!reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateYZ(reverse);
                        }
                        if (rt == RotateType.Back && subCube.pos[0] == -sizeSubCube)
                        {
                            subCube.pos = subCube.pos.RotateYZ(angle * (reverse ? -1 : 1));
                            subCube.ColorSidesMap = subCube.ColorSidesMap.RotateYZ(!reverse);
                        }
                    }

        }
    }
}
