using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CubeRubic_s.CubeWork;
using static CubeRubic_s.ModelCube;

namespace CubeRubic_s
{
    public class SolveCube
    {
        public List<RotateType> SolveStep(ColorsCube[][,] cube)
        {
            List<RotateType> steps = new List<RotateType>();

            foreach (mask mask in Layer1_masks)
            {
                bool ans = true;

                foreach (var byColor in mask.maskOneColor)
                    foreach (var check in byColor.points)
                        if (cube[(int)check.side >> 1][check.coords[0], check.coords[1]] != byColor.color)
                        {
                            ans = false;
                            break;
                        }
                if (ans && mask.maskCoordsOneColor != null && mask.maskCoordsOneColor.Count > 0)
                {
                    ColorsCube color = cube[(int)mask.maskCoordsOneColor[0].side >> 1][mask.maskCoordsOneColor[0].coords[0], mask.maskCoordsOneColor[0].coords[1]];
                    foreach (var byCoords in mask.maskCoordsOneColor)
                        if (cube[(int)byCoords.side >> 1][byCoords.coords[0], byCoords.coords[1]] != color)
                        {
                            ans = false;
                            break;
                        }
                }

                if (ans)
                    foreach (var step in mask.solution)
                        steps.Add(step);  
            }
            

            return steps;
        }

        /*
         Решение первого слоя
         */

        List<mask> Layer1_masks = new List<mask>() 
        { 
            new mask(
                    new List<mask.maskByColor>() 
                    { 
                        new mask.maskByColor() 
                        { 
                            color = ColorsCube.WHITE, 
                            points = new List<mask.maskCoords>() 
                                { 
                                    new mask.maskCoords() 
                                    { 
                                        side = RotateType.Back,
                                        coords =  new int[] { 1, 2 } 
                                    } 
                                } 
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[] { 1,1 }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[] { 1,2 }
                        },
                    },
                    new RotateType[] { RotateType.Right, RotateType.Down_,RotateType.Right_,RotateType.Front, RotateType.Front }
                ),
            new mask(
                    new List<mask.maskByColor>()
                    {
                        new mask.maskByColor()
                        {
                            color = ColorsCube.WHITE,
                            points = new List<mask.maskCoords>()
                                {
                                    new mask.maskCoords()
                                    {
                                        side = RotateType.Right,
                                        coords =  new int[] { 1, 2 }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[] { 1,1 }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Back,
                            coords = new int[] { 1,2 }
                        },
                    },
                    new RotateType[] { RotateType.Right, RotateType.Right, RotateType.Front_, RotateType.Right, RotateType.Right }
                )
        };

        public class mask
        {
            public List<maskByColor> maskOneColor;
            public List<maskCoords> maskCoordsOneColor;
            public RotateType[] solution;

            public mask(List<maskByColor> maskOneColor, List<maskCoords> maskCoordsOneColor, RotateType[] solution)
            {
                this.maskOneColor = maskOneColor;
                this.maskCoordsOneColor = maskCoordsOneColor;
                this.solution = solution;
            }

            public struct maskByColor
            {
                public ColorsCube color;
                public List<maskCoords> points;
            }
            public struct maskCoords
            {
                public RotateType side;
                public int[] coords;
            }
        }
        //List<ListDictionary> Layer1_masks = new List<ListDictionary>() { Layer1_1 };


        //static ListDictionary Layer1_1 = new ListDictionary() {
        //    { RotateType.Front, new int[] { 1, 1 } },
        //    { RotateType.Right, new int[] { 1, 2} },
        //    { -1, new RotateType[] { RotateType.Right, RotateType.Down_, RotateType.Right_, RotateType.Front, RotateType.Front } }
        //};
    }
}
