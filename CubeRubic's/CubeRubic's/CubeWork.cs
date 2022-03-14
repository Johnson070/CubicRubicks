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
        public enum RotateType
        {
            Up,
            Down,
            Right,
            Left,
            Back,
            Front
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
                            Cube[i][j][k].SetColor(SubCubeRubic.Colors.White, SubCubeRubic.type.Up);
                        }

                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);
                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);
                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                    }
                }
                else if (i == 1)
                {
                    for (int j = 0; j < Cube[i].Length; j++)
                    {
                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);
                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);

                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);
                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                    }
                }
                else if (i == 2)
                {
                    for (int j = 0; j < Cube[i].Length; j++)
                    {
                        for (int k = 0; k < Cube[i][j].Length; k++)
                        {
                            Cube[i][j][k].SetColor(SubCubeRubic.Colors.Yellow, SubCubeRubic.type.Down);
                        }

                        if (j == 0)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);

                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Blue, SubCubeRubic.type.Right);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                        if (j == 1)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }



                        if (j == 2)
                        {
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Orange, SubCubeRubic.type.Back);
                            Cube[i][j][0].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);

                            Cube[i][j][1].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);

                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Green, SubCubeRubic.type.Left);
                            Cube[i][j][2].SetColor(SubCubeRubic.Colors.Red, SubCubeRubic.type.Front);
                        }

                    }
                }
            }

            //Cube[0][0][0].FillColor(System.Drawing.Color.Purple);
        }

        public void RotateCube(RotateType rt, bool reverse = false, float angle = 90)
        {


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
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateYZ(angle * (reverse ? -1 : 1));

                                if (rt == RotateType.Back && subCube.pos[0] == -sizeSubCube)
                                    subCube.posPoint[i][j] = subCube.posPoint[i][j].RotateYZ(angle * (!reverse ? -1 : 1));
                            }

                        if (rt == RotateType.Right && subCube.pos[2] == -sizeSubCube)
                            subCube.pos = subCube.pos.RotateXY(angle * (reverse ? -1 : 1));

                        if (rt == RotateType.Left && subCube.pos[2] == sizeSubCube)
                            subCube.pos = subCube.pos.RotateXY(angle * (!reverse ? -1 : 1));

                        if (rt == RotateType.Up && subCube.pos[1] == sizeSubCube)
                            subCube.pos = subCube.pos.RotateXZ(angle * (reverse ? -1 : 1));

                        if (rt == RotateType.Down && subCube.pos[1] == -sizeSubCube)
                            subCube.pos = subCube.pos.RotateXZ(angle * (!reverse ? -1 : 1));

                        if (rt == RotateType.Front && subCube.pos[0] == sizeSubCube)
                            subCube.pos = subCube.pos.RotateYZ(angle * (reverse ? -1 : 1));

                        if (rt == RotateType.Back && subCube.pos[0] == -sizeSubCube)
                            subCube.pos = subCube.pos.RotateYZ(angle * (!reverse ? -1 : 1));
                    }
        }
    }
}
