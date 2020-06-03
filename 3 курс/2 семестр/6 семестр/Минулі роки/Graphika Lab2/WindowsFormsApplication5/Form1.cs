using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// для работы с библиотекой OpenGL
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        private float rot_1, rot_2;
        float an1 = 0;
        float an2 = 0;
        double a;
        double col11, col22, col33, col44, col55, col66, op1;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация Glut
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // очитка окна
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соотвествии с размерами элемента anT
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);


            // настройка проекции
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // настройка параметров OpenGL для визуализации
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            // активация таймера
            RenderTimer.Start();


        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            {

                // вызываем функцию, отвечающей за отрисовку сцены
                Draw();                            

            }
        }
        // функция отрисовки сцены
        private void Draw()
        {
            // два параметра, которые мы будем использовать для непрерывного вращения сцены вокруг 2 координатных осей
            rot_1 -= 3 + an1;
            rot_2 -= 5 + an2;
           // rot_3 = (float)Math.Sin(rot_1) + (float)Math.Cos(rot_1) + (float)Math.Sin(rot_1);
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы
            Gl.glLoadIdentity();

            // установка положения камеры (наблюдателя). Как видно из кода
            // дополнительно на полложение наблюдателя по оси Z влияет значение
            // установленное в ползунке, доступном для пользователя.

            // таким образом, при перемещении ползунка, наблюдатель будет отдалятся или приближатся к объекту наблюдения
            Gl.glTranslated(0, 0, -160 + a);
            // 2 поворота (углы rot_1 и rot_2)
            Gl.glRotated(rot_1, 40, 1, 15);
            Gl.glRotated(rot_2, rot_1 + rot_2, 5, 20);
            //Gl.glRotated(20, -20, 0, 0);

            //Буква П
            //лицевая сторона
            Gl.glColor4d(1.0, 0.3, 0.7, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-35.0, 25.0, 0.0);
            Gl.glVertex3d(-30.0, 25.0, 0.0);
            Gl.glVertex3d(-30.0, -20.0, 0.0);
            Gl.glVertex3d(-35.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-30.0, 25.0, 0.0);
            Gl.glVertex3d(-15.0, 25.0, 0.0);
            Gl.glVertex3d(-15.0, 20.0, 0.0);
            Gl.glVertex3d(-30.0, 20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-15.0, 25.0, 0.0);
            Gl.glVertex3d(-10.0, 25.0, 0.0);
            Gl.glVertex3d(-10.0, -20.0, 0.0);
            Gl.glVertex3d(-15.0, -20.0, 0.0);
            Gl.glEnd();

            //задняя сторона буквы П
            Gl.glColor4d(0.0, 0.3, 0.7, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-35.0, 25.0, -5.0);
            Gl.glVertex3d(-30.0, 25.0, -5.0);
            Gl.glVertex3d(-30.0, -20.0, -5.0);
            Gl.glVertex3d(-35.0, -20.0, -5.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-30.0, 25.0, -5.0);
            Gl.glVertex3d(-15.0, 25.0, -5.0);
            Gl.glVertex3d(-15.0, 20.0, -5.0);
            Gl.glVertex3d(-30.0, 20.0, -5.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-15.0, 25.0, -5.0);
            Gl.glVertex3d(-10.0, 25.0, -5.0);
            Gl.glVertex3d(-10.0, -20.0, -5.0);
            Gl.glVertex3d(-15.0, -20.0, -5.0);
            Gl.glEnd();

            //обводка буквы П
            Gl.glColor4d(0.8, 0.0, 0.7, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-35.0, 25.0, 0.0);
            Gl.glVertex3d(-35.0, 25.0, -5.0);
            Gl.glVertex3d(-35.0, -20.0, -5.0);
            Gl.glVertex3d(-35.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-35.0, 25.0, 0.0);
            Gl.glVertex3d(-35.0, 25.0, -5.0);
            Gl.glVertex3d(-10.0, 25.0, -5.0);
            Gl.glVertex3d(-10.0, 25.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-10.0, 25.0, 0.0);
            Gl.glVertex3d(-10.0, 25.0, -5.0);
            Gl.glVertex3d(-10.0, -20.0, -5.0);
            Gl.glVertex3d(-10.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glColor4d(0.8, 0.3, 0.3, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-15.0, -20.0, 0.0);
            Gl.glVertex3d(-15.0, -20.0, -5.0);
            Gl.glVertex3d(-10.0, -20.0, -5.0);
            Gl.glVertex3d(-10.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-15.0, 20.0, 0.0);
            Gl.glVertex3d(-15.0, 20.0, -5.0);
            Gl.glVertex3d(-15.0, -20.0, -5.0);
            Gl.glVertex3d(-15.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-30.0, 20.0, 0.0);
            Gl.glVertex3d(-30.0, 20.0, -5.0);
            Gl.glVertex3d(-15.0, 20.0, -5.0);
            Gl.glVertex3d(-15.0, 20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-30.0, 20.0, 0.0);
            Gl.glVertex3d(-30.0, 20.0, -5.0);
            Gl.glVertex3d(-30.0, -20.0, -5.0);
            Gl.glVertex3d(-30.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-35.0, -20.0, 0.0);
            Gl.glVertex3d(-35.0, -20.0, -5.0);
            Gl.glVertex3d(-30.0, -20.0, -5.0);
            Gl.glVertex3d(-30.0, -20.0, 0.0);
            Gl.glEnd();


            Gl.glRotated(rot_1, 10, 6, 5);
            Gl.glRotated(rot_2, 30, 10, 30);
            //Буква М
            //лицевая сторона
            Gl.glColor4d(0.3, 0.6, 0.3, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(5.0, 25.0, 0.0);
            Gl.glVertex3d(10.0, 25.0, 0.0);
            Gl.glVertex3d(10.0, -20.0, 0.0);
            Gl.glVertex3d(5.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, 25.0, 0.0);
            Gl.glVertex3d(35.0, 25.0, 0.0);
            Gl.glVertex3d(35.0, -20.0, 0.0);
            Gl.glVertex3d(30.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(10.0, 25.0, 0.0);
            Gl.glVertex3d(20.0, 10.0, 0.0);
            Gl.glVertex3d(20.0, 3.0, 0.0);
            Gl.glVertex3d(10.0, 17.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(20.0, 10.0, 0.0);
            Gl.glVertex3d(30.0, 25.0, 0.0);
            Gl.glVertex3d(30.0, 17.0, 0.0);
            Gl.glVertex3d(20.0, 3.0, 0.0);
            Gl.glEnd();

            //Тыл буквы М
            Gl.glColor4d(0.5, 0.5, 1.0, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(5.0, 25.0, -5.0);
            Gl.glVertex3d(10.0, 25.0, -5.0);
            Gl.glVertex3d(10.0, -20.0, -5.0);
            Gl.glVertex3d(5.0, -20.0, -5.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, 25.0, -5.0);
            Gl.glVertex3d(35.0, 25.0, -5.0);
            Gl.glVertex3d(35.0, -20.0, -5.0);
            Gl.glVertex3d(30.0, -20.0, -5.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(10.0, 25.0, -5.0);
            Gl.glVertex3d(20.0, 10.0, -5.0);
            Gl.glVertex3d(20.0, 3.0, -5.0);
            Gl.glVertex3d(10.0, 17.0, -5.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(20.0, 10.0, -5.0);
            Gl.glVertex3d(30.0, 25.0, -5.0);
            Gl.glVertex3d(30.0, 17.0, -5.0);
            Gl.glVertex3d(20.0, 3.0, -5.0);
            Gl.glEnd();

            //обводка буквы М
            Gl.glColor4d(0.1, 1.0, 0.5, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(5.0, 25.0, 0.0);
            Gl.glVertex3d(5.0, 25.0, -5.0);
            Gl.glVertex3d(5.0, -20.0, -5.0);
            Gl.glVertex3d(5.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(5.0, 25.0, 0.0);
            Gl.glVertex3d(5.0, 25.0, -5.0);
            Gl.glVertex3d(10.0, 25.0, -5.0);
            Gl.glVertex3d(10.0, 25.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(10.0, 25.0, 0.0);
            Gl.glVertex3d(10.0, 25.0, -5.0);
            Gl.glVertex3d(20.0, 10.0, -5.0);
            Gl.glVertex3d(20.0, 10.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(20.0, 10.0, 0.0);
            Gl.glVertex3d(20.0, 10.0, -5.0);
            Gl.glVertex3d(30.0, 25.0, -5.0);
            Gl.glVertex3d(30.0, 25.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, 25.0, 0.0);
            Gl.glVertex3d(30.0, 25.0, -5.0);
            Gl.glVertex3d(35.0, 25.0, -5.0);
            Gl.glVertex3d(35.0, 25.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(35.0, 25.0, 0.0);
            Gl.glVertex3d(35.0, 25.0, -5.0);
            Gl.glVertex3d(35.0, -20.0, -5.0);
            Gl.glVertex3d(35.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glColor4d(0.6, 0.9, 0.0, 1 + op1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, -20.0, 0.0);
            Gl.glVertex3d(30.0, -20.0, -5.0);
            Gl.glVertex3d(35.0, -20.0, -5.0);
            Gl.glVertex3d(35.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, 17.0, 0.0);
            Gl.glVertex3d(30.0, 17.0, -5.0);
            Gl.glVertex3d(30.0, -20.0, -5.0);
            Gl.glVertex3d(30.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(30.0, 17.0, 0.0);
            Gl.glVertex3d(30.0, 17.0, -5.0);
            Gl.glVertex3d(20.0, 3.0, -5.0);
            Gl.glVertex3d(20.0, 3.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(20.0, 3.0, 0.0);
            Gl.glVertex3d(20.0, 3.0, -5.0);
            Gl.glVertex3d(10.0, 17.0, -5.0);
            Gl.glVertex3d(10.0, 17.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(10.0, 17.0, 0.0);
            Gl.glVertex3d(10.0, 17.0, -5.0);
            Gl.glVertex3d(10.0, -20.0, -5.0);
            Gl.glVertex3d(10.0, -20.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(5.0, -20.0, 0.0);
            Gl.glVertex3d(5.0, -20.0, -5.0);
            Gl.glVertex3d(10.0, -20.0, -5.0);
            Gl.glVertex3d(10.0, -20.0, 0.0);
            Gl.glEnd();
            // возвращаем сохраненную матрицу
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();

            // обновляем элемент AnT
            AnT.Invalidate();
        }
       

        private void zoom_Scroll(object sender, EventArgs e)
        {
            a = (double)zoom.Value;
            label1.Text = a.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            an1 = (float)trackBar1.Value;
            angle1.Text = an1.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            an2 = (float)trackBar2.Value;
            angle2.Text = an2.ToString();
        }

       

        private void Op_Scroll(object sender, EventArgs e)
        {
            op1 = (double)Op.Value / 100 - 1;
            label3.Text = Op.Value.ToString();
        }
    }
}
