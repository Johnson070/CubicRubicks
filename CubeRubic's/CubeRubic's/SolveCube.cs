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

            foreach (var masks in Layer1_masks)
            {
                bool ans = true;
                foreach (DictionaryEntry mask in masks) 
                {
                    if ((int)mask.Key == -1) break;
                    int[] coords = (int[])mask.Value;
                    //var a = cube[(int)mask.Key][coords[0], coords[1]];
                    if (cube[(int)mask.Key >> 1][coords[0], coords[1]] != cube[(int)mask.Key >> 1][coords[0], coords[1]])
                    {
                        ans &= false;
                        break;
                    }
                }
                if (ans)
                    foreach (var step in (RotateType[])(masks[-1]))
                    {
                        steps.Add(step);
                    }
            }

            return steps;
        }

        /*
         Решение первого слоя
         */
        List<ListDictionary> Layer1_masks = new List<ListDictionary>() { Layer1_1 };


        static ListDictionary Layer1_1 = new ListDictionary() {
            { RotateType.Front, new int[] { 1, 1 } },
            { RotateType.Right, new int[] { 1, 2} },
            { -1, new RotateType[] { RotateType.Right, RotateType.Down_, RotateType.Right_, RotateType.Front, RotateType.Front } }
        };
    }
}
