using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeRubic_s.ModelCube;

namespace CubeRubic_s
{
    public class ModelCube
    {
        public enum ColorsCube
        {
            RED,
            WHITE,
            ORANGE,
            YELLOW,
            GREEN,
            BLUE,
            BLACK
        }

        public ColorsCube[][,] ColoredCube
        {
            get
            {
                return new ColorsCube[][,] { this.up, down, right, left, front, back };
            }
            set
            {
                up = value[0];
                down = value[1];
                right = value[2];
                left = value[3];
                front = value[4];
                back = value[5];
            }
        }

        public ModelCube()
        {
            up = new ColorsCube[3,3];
            down = new ColorsCube[3,3];
            right = new ColorsCube[3,3];
            left = new ColorsCube[3,3];
            front = new ColorsCube[3,3];
            back = new ColorsCube[3,3];
        }

        public void SetColorsMatrix(SubCubeRubic[][][] Cube)
        {
            foreach (var slice in Cube)
                foreach (var row in slice)
                    foreach (var subCube in row)
                    {
                        if (subCube == null) continue;

                        //for (int i = 0; i < subCube.posPoint.Length; i++)
                        //    for (int j = 0; j < subCube.posPoint[i].Length; j++)
                        //    {
                                if (subCube.pos[2] == -SubCubeRubic.sizeSubCube) // right
                                    right[1 - (int)subCube.pos[1],1 - (int)subCube.pos[0]] = subCube.ColorSidesMap.Right;

                                if (subCube.pos[2] == SubCubeRubic.sizeSubCube) //left
                                    left[1 - (int)subCube.pos[1], 1 + (int)subCube.pos[0]] = subCube.ColorSidesMap.Left;

                                if (subCube.pos[1] == SubCubeRubic.sizeSubCube) //UP
                                    up[1 + (int)subCube.pos[0],1 - (int)subCube.pos[2]] = subCube.ColorSidesMap.Up;

                                if (subCube.pos[1] == -SubCubeRubic.sizeSubCube) //down
                                    down[1 + (int)subCube.pos[0], 1 - (int)subCube.pos[2]] = subCube.ColorSidesMap.Down;

                                if (subCube.pos[0] == SubCubeRubic.sizeSubCube) //Front
                                    front[1 - (int)subCube.pos[1],1 - (int)subCube.pos[2]] = subCube.ColorSidesMap.Front;

                                if (subCube.pos[0] == -SubCubeRubic.sizeSubCube) //back
                                    back[1 - (int)subCube.pos[1],1 - (int)subCube.pos[2]] = subCube.ColorSidesMap.Back;
                           //}

                    }
        }

        ColorsCube[,] up;
        ColorsCube[,] down;
        ColorsCube[,] left;
        ColorsCube[,] right;
        ColorsCube[,] front;
        ColorsCube[,] back;
    }
}
