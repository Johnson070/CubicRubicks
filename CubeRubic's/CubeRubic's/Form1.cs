using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using CubeRubic_s;

namespace CubeRubic_s
{
    public partial class Form1 : Form
    {
        //static float sizeSubCube = 1.0f;
        float rotation = 0.0f;
        float rotation_cube = 0;
        float rotation_const = 4.5f;
        CubeWork cube;

        void RotateCubic(CubeWork.RotateType rt)
        {
            
        }

        public Form1()
        {
            InitializeComponent();
            cube = new CubeWork();
        }

        float rotateOne = 0;
        CubeWork.RotateType rt = CubeWork.RotateType.Right;
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;

            //  Очищаем буфер цвета и глубины
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Загружаем единичную матрицу
            gl.LoadIdentity();

            //  Указываем оси вращения (x, y, z)
            //
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.LineWidth(2);
            gl.Color(1f, 0f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(10,0,0);
            gl.End();

            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.LineWidth(2);
            gl.Color(0f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 10, 0);
            gl.End();

            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.LineWidth(2);
            gl.Color(0f, 0f, 1f);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 10);
            gl.End();

            //cube.RotateCube(CubeWork.RotateType.Front,false, rotation_const);

            DrawRubic(gl);

            // рисуем землю            
            //gl.Begin(OpenGL.GL_POLYGON);
            //gl.Color(0f, 1f, 0f);
            //gl.Vertex(-10f, -3f, -10f);
            //gl.Vertex(10f, -3f, -10f);
            //gl.Vertex(10f, -3f, 10f);
            //gl.Vertex(-10f, -3f, 10f);
            //gl.End();

            rotation += 0.5f; // угол разворота за 1 кадр
        }

        void DrawRubic(OpenGL gl)
        { 
            for (int i = 0; i < cube.Cube.Length; i++)
            {
                for (int j = 0; j < cube.Cube[i].Length; j++)
                {
                    for (int k = 0; k < cube.Cube[i][j].Length; k++)
                    {
                        if (cube.Cube[i][j][k] == null) continue;

                        //gl.Color(1f, cube.Cube[i][j][k].ColorBack.R / 255.0f, 0);
                        //gl.PointSize(10f);
                        //gl.Begin(OpenGL.GL_POINTS);
                        //gl.Vertex(cube.Cube[i][j][k].pos);
                        //gl.End();
                        // up
                        //gl.Begin(OpenGL.GL_QUADS);
                        //gl.Color(cube.Cube[i][j][k].ColorUp.GetFroatColor());
                        //gl.Vertex(cube.Cube[i][j][k].posPoint[0][0]);
                        //gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        //gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        //gl.Vertex(cube.Cube[i][j][k].posPoint[0][3]);
                        //gl.End();

                        //down
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorDown.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][3]);
                        gl.End();


                        //Right
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorRight.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][0]);
                        gl.End();

                        //left
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorLeft.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        gl.End();

                        //front
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorFront.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        gl.End();

                        //back
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorBack.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][0]);
                        gl.End();
                    }
                }
            }
           

            gl.End();
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            //  Фоновый цвет по умолчанию (в данном случае цвет голубой)
            gl.ClearColor(0.1f, 0.5f, 1.0f, 0);

            //gl.LookAt(6, 6, -6,    // Позиция самой камеры (x, y, z)
            //            0, 1, 0,     // Направление, куда мы смотрим
            //            0, 2, 0);    // Верх камеры
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            //  Возьмём OpenGL объект
            OpenGL gl = openGLControl1.OpenGL;

            //  Зададим матрицу проекции
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Единичная матрица для последующих преобразований
            gl.LoadIdentity();

            //  Преобразование
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Данная функция позволяет установить камеру и её положение
            gl.LookAt(6, 6, -6,    // Позиция самой камеры (x, y, z)
                        0, 1, 0,     // Направление, куда мы смотрим
                        0, 1, 0);    // Верх камеры

            //  Зададим модель отображения
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Up);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Down);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Left);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Right);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Front);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cube.RotateCube(CubeWork.RotateType.Back);
        }
    }
}
