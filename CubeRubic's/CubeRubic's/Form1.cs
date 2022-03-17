﻿using System;
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
using static CubeRubic_s.CubeWork;

namespace CubeRubic_s
{
    public partial class Form1 : Form
    {
        //static float sizeSubCube = 1.0f;
        float rotation = 0.0f;
        float rotation_cube = 0;
        float rotation_const = 4.5f;
        OpenGL gl;
        CubeWork cube;
        CameraClass camera = new CameraClass();
        ModelCube mc;

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(openGLControl1_MouseMove);
            cube = new CubeWork();

            mc = new ModelCube();
            mc.SetColorsMatrix(cube.Cube);
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            gl = openGLControl1.OpenGL;

            //  Очищаем буфер цвета и глубины
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Загружаем единичную матрицу
            gl.LoadIdentity();

            //  Указываем оси вращения (x, y, z)
            //
            //gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

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

            DrawRubic();

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

        void DrawRubic()
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
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Up.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][3]);
                        gl.End();

                        //down
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Down.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][3]);
                        gl.End();

                        //Right
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Right.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][0]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][0]);
                        gl.End();

                        //left
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Left.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][3]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        gl.End();

                        //front
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Front.GetFroatColor());
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][1]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[1][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][2]);
                        gl.Vertex(cube.Cube[i][j][k].posPoint[0][1]);
                        gl.End();

                        //back
                        gl.Begin(OpenGL.GL_QUADS);
                        gl.Color(cube.Cube[i][j][k].ColorSides3D.Back.GetFroatColor());
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
            gl.LookAt(camera.x, camera.y, camera.z,    // Позиция самой камеры (x, y, z)
                        camera.c_x, camera.c_y, camera.c_z,     // Направление, куда мы смотрим
                        0, 1, 0);    // Верх камеры

            //  Зададим модель отображения
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void RotateBtnClick(object sender, EventArgs e)
        {
            if (((Button)sender).Tag.ToString() == "0") cube.RotateCube(CubeWork.RotateType.Up);
            if (((Button)sender).Tag.ToString() == "1") cube.RotateCube(CubeWork.RotateType.Down);
            if (((Button)sender).Tag.ToString() == "2") cube.RotateCube(CubeWork.RotateType.Left);
            if (((Button)sender).Tag.ToString() == "3") cube.RotateCube(CubeWork.RotateType.Right);
            if (((Button)sender).Tag.ToString() == "4") cube.RotateCube(CubeWork.RotateType.Front);
            if (((Button)sender).Tag.ToString() == "5") cube.RotateCube(CubeWork.RotateType.Back);

            mc.SetColorsMatrix(cube.Cube);
            var x = mc.ColoredCube[(int)RotateType.Up];
            Console.WriteLine($"" +
                $"{x[0,0]} {x[0, 1]} {x[0, 2]}\n" +
                $"{x[1, 0]} {x[1, 1]} {x[1, 2]}\n" +
                $"{x[2, 0]} {x[2, 1]} {x[2, 2]}\n");
        }

        bool mouseHold = false;
        float xMouse, yMouse;
        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseHold = true;
                xMouse = e.X;
                yMouse = e.Y;
            }
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                camera.coords = camera.coords.SetRadiusCamera(-e.Delta/120.0f);
                openGLControl1_Resized(null, null);
            }
            if (mouseHold)
            {
                float absX = e.X - xMouse;
                float absY = e.Y - yMouse;

                camera.coords = camera.coords.RotateXZ(absX/2);
                camera.coords = camera.coords.RotateXY(absY/2);

                xMouse = e.X;
                yMouse = e.Y;

                openGLControl1_Resized(null, null);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                cube.RotateCube((CubeWork.RotateType)rnd.Next(0,6));
            }
            mc.SetColorsMatrix(cube.Cube);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SolveCube cb = new SolveCube();
            var a = cb.SolveStep(mc.ColoredCube);

            foreach (var step in a)
            {
                cube.RotateCube(step);
            }
            
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHold = false;
        }
    }
}
