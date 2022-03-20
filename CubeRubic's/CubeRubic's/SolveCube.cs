using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
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

            bool white = CheckStateContinue(cube, find_white, ref steps);
            if (!white) return steps;

            if (!CheckState(cube, find_cross))
            {
                GetSolution(cube, Layer1_masks, ref steps, true);
                if (!white) return steps;
            }

            return steps;
        }

        private static void GetSolution(ColorsCube[][,] cube, mask[] masks, ref List<RotateType> steps, bool mirror = false)
        {
            foreach (mask mask in masks)
            {
                bool ans = CheckSolution(cube, mask);

                if (ans)
                {
                    foreach (var step in mask.solution)
                        steps.Add(step);
                    return;
                }
            }

            if (mirror && steps.Count == 0)
                foreach (mask mask in masks)
                {
                    bool ans = CheckSolution(cube, mask, true);

                    if (ans)
                    {
                        foreach (var step in mask.solution.GetMirror())
                            steps.Add(step);
                        return;
                    }
                }
        }

        private static bool CheckStateContinue(ColorsCube[][,] cube, mask[] masks, ref List<RotateType> steps)
        {
            foreach (mask mask in masks)
            {
                bool finded = CheckState(cube, mask);

                if (!finded) continue;

                if (mask.solution != null)
                    foreach (var step in mask.solution)
                        steps.Add(step);
                return false;
            }
            return true;
        }

        private static bool CheckSolution(ColorsCube[][,] cube, mask mask, bool reverse = false)
        {
            bool ans = true;

            foreach (var byColor in mask.maskOneColor)
                foreach (var check in byColor.points)
                    foreach (int[] coords in check.coords)
                        if (reverse && cube[(int)(check.side == RotateType.Right ? RotateType.Left : check.side) >> 1][coords[0], 2 - coords[1]] != byColor.color)
                        {
                            ans = false;
                            break;
                        }
                        else if (!reverse && cube[(int)check.side >> 1][coords[0], coords[1]] != byColor.color)
                        {
                            ans = false;
                            break;
                        }
                        
            if (ans && mask.maskCoordsOneColor != null && mask.maskCoordsOneColor.Count > 0)
            {
                ColorsCube color = cube[(int)(mask.maskCoordsOneColor[0].side == RotateType.Right ? RotateType.Left : mask.maskCoordsOneColor[0].side) >> 1][mask.maskCoordsOneColor[0].coords[0][0], (reverse ? 2 - mask.maskCoordsOneColor[0].coords[0][1] : mask.maskCoordsOneColor[0].coords[0][1])];
                foreach (var byCoords in mask.maskCoordsOneColor)
                    foreach (int[] coords in byCoords.coords)
                        if (reverse && cube[(int)(byCoords.side == RotateType.Right ? RotateType.Left : byCoords.side) >> 1][coords[0], 2 - coords[1]] != color)
                        {
                            ans = false;
                            break;
                        }
                        else if (!reverse && cube[(int)byCoords.side >> 1][coords[0], coords[1]] != color)
                        {
                            ans = false;
                            break;
                        }
            }

            return ans;
        }

        private static bool CheckState(ColorsCube[][,] cube, mask mask)
        {
            foreach (var byColor in mask.maskOneColor)
                foreach (var check in byColor.points)
                    foreach (int[] coords in check.coords)
                        if (cube[(int)check.side  >> 1][coords[0], coords[1]] != byColor.color)
                        {
                            return false;
                        }
            return true;
        }
        /*
         Решение первого слоя
         */

        mask[] find_white = new mask[]
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
                                    coords = new int[][] { new int[] { 1, 1 } }, 
                                    side = RotateType.Right 
                                } 
                            } 
                        } 
                    },
                solution: new RotateType[] { RotateType.z_}
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
                                    coords = new int[][] { new int[] { 1, 1 } },
                                    side = RotateType.Left
                                }
                            }
                        }
                    },
                solution: new RotateType[] { RotateType.z}
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
                                    coords = new int[][] { new int[] { 1, 1 } },
                                    side = RotateType.Front
                                }
                            }
                        }
                    },
                solution: new RotateType[] { RotateType.x}
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
                                    coords = new int[][] { new int[] { 1, 1 } },
                                    side = RotateType.Back
                                }
                            }
                        }
                    },
                solution: new RotateType[] { RotateType.x_}
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
                                    coords = new int[][] { new int[] { 1, 1 } },
                                    side = RotateType.Down
                                }
                            }
                        }
                    },
                solution: new RotateType[] { RotateType.x, RotateType.x}
                )
        };

        mask[] Layer1_masks = new mask[]
        {
            // R
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
                                        coords =  new int[][] { new int[] { 1, 2 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1, 1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[][] { new int[] { 1, 2 } }
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
                                        coords =  new int[][] { new int[] { 1, 2 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Back,
                            coords = new int[][] { new int[] { 1,2 } }
                        },
                    },
                    new RotateType[] { RotateType.Right, RotateType.Right, RotateType.Front_, RotateType.Right, RotateType.Right }
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
                                        side = RotateType.Front,
                                        coords =  new int[][] { new int[] { 1, 2 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[][] { new int[] { 1,0 } }
                        },
                    },
                    new RotateType[] { RotateType.Right_, RotateType.Down_, RotateType.Right, RotateType.Front, RotateType.Front }
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
                                        coords =  new int[][] { new int[] { 1, 0 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,2 } }
                        },
                    },
                    new RotateType[] { RotateType.Front_ }
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
                                        coords =  new int[][] { new int[] { 0, 1} }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Up,
                            coords = new int[][] { new int[] { 1,2 } }
                        },
                    },
                    new RotateType[] { RotateType.Right_, RotateType.Front_}
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
                                        side = RotateType.Up,
                                        coords =  new int[][] { new int[] { 1, 2 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[][] { new int[] { 0,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Right, RotateType.Right, RotateType.Down_, RotateType.Front, RotateType.Front }
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
                                        side = RotateType.Down,
                                        coords =  new int[][] { new int[] { 1, 2 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[][] { new int[] { 2,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Down_, RotateType.Front, RotateType.Front }
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
                                        coords =  new int[][] { new int[] { 2,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Down,
                            coords = new int[][] { new int[] { 1,2 } }
                        },
                    },
                    new RotateType[] { RotateType.Right, RotateType.Front_, RotateType.Right_}
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
                                        side = RotateType.Down,
                                        coords =  new int[][] { new int[] { 2,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 2,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Front, RotateType.Front}
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
                                        side = RotateType.Front,
                                        coords =  new int[][] { new int[] { 2,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Down,
                            coords = new int[][] { new int[] { 2,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Down, RotateType.Right, RotateType.Front_, RotateType.Right_}
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
                                        side = RotateType.Front,
                                        coords =  new int[][] { new int[] { 0,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Up,
                            coords = new int[][] { new int[] { 2,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Up_, RotateType.Right_, RotateType.Up, RotateType.Front_}
                ),
            
            //
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
                                        side = RotateType.Down,
                                        coords =  new int[][] { new int[] { 0,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Back,
                            coords = new int[][] { new int[] { 2,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Down_}
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
                                        side = RotateType.Back,
                                        coords =  new int[][] { new int[] { 2,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Down,
                            coords = new int[][] { new int[] { 0,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Down_}
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
                                        side = RotateType.Back,
                                        coords =  new int[][] { new int[] { 0,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Up,
                            coords = new int[][] { new int[] { 0,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Back_}
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
                                        side = RotateType.Up,
                                        coords =  new int[][] { new int[] { 0,1 } }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][] { new int[] { 1,1 } }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Back,
                            coords = new int[][] { new int[] { 0,1 } }
                        },
                    },
                    new RotateType[] { RotateType.Back_}
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
                                side = RotateType.Up,
                                coords =  new int[][] { new int[] { 2, 1 } }
                            }
                        }
                    }
                },
                new List<mask.maskCoords>()
                {
                    new mask.maskCoords()
                    {
                        side = RotateType.Front,
                        coords = new int[][] {
                            new int[] { 0, 1},
                            new int[] { 1, 1}
                        }
                    }
                },
                new RotateType[] { RotateType.y }
            )
    };

        mask find_cross =
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
                                        side = RotateType.Up,
                                        coords =  new int[][]
                                        {
                                            new int[] { 1, 1 },
                                            new int[] { 0, 1 },
                                            new int[] { 2, 1 },
                                            new int[] { 1, 2 },
                                            new int[] { 1, 0 }
                                        }
                                    }
                                }
                        }
                    },
                    new List<mask.maskCoords>()
                    {
                        new mask.maskCoords()
                        {
                            side = RotateType.Front,
                            coords = new int[][]
                            {
                                new int[] { 1, 1 },
                                new int[] { 0, 1 }
                            }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Left,
                            coords = new int[][]
                            {
                                new int[] { 1, 1 },
                                new int[] { 0, 1 }
                            }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Back,
                            coords = new int[][]
                            {
                                new int[] { 1, 1 },
                                new int[] { 0, 1 }
                            }
                        },
                        new mask.maskCoords()
                        {
                            side = RotateType.Right,
                            coords = new int[][]
                            {
                                new int[] { 1, 1 },
                                new int[] { 0, 1 }
                            }
                        },
                    }
                );

        public class mask
        {
            public List<maskByColor> maskOneColor;
            public List<maskCoords> maskCoordsOneColor;
            public RotateType[] solution;

            public mask(List<maskByColor> maskOneColor = null, List<maskCoords> maskCoordsOneColor = null, RotateType[] solution = null)
            {
                this.maskOneColor = maskOneColor;
                this.maskCoordsOneColor = maskCoordsOneColor;
                this.solution = solution;
            }

            public mask() { }

            public class maskByColor
            {
                public ColorsCube color;
                public List<maskCoords> points;
            }
            public class maskCoords
            {
                public RotateType side;
                public int[][] coords;
            }
        }
    }

    public static class RotateTypeExt
    {
        public static RotateType[] GetMirror(this RotateType[] rt)
        {
            var rtOut = new RotateType[rt.Length];

            for (int i = 0; i < rt.Length; i++)
            {
                if (rt[i] == RotateType.Right) rtOut[i] = RotateType.Left_;
                else if (rt[i] == RotateType.Right_) rtOut[i] = RotateType.Left;
                else if (rt[i] == RotateType.Down_) rtOut[i] = RotateType.Down;
                else if (rt[i] == RotateType.Down) rtOut[i] = RotateType.Down_;
                else if (rt[i] == RotateType.Front) rtOut[i] = RotateType.Front_;
                else if (rt[i] == RotateType.Front_) rtOut[i] = RotateType.Front;
            }
                
            for (int i = 0; i < rt.Length - 1; i++)
                if (rtOut[i] == rtOut[i + 1] && ((byte)rtOut[i] & 0b0000_1) == 0b1)
                {
                    rtOut[i] = (RotateType)((byte)rtOut[i] & 0b1111_0);
                    rtOut[i+1] = (RotateType)((byte)rtOut[i+1] & 0b1111_0);
                    i++;
                }

            return rtOut;
        }
    }
}
