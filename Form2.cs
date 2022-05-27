using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Курсачик
{
    public partial class Form2 : Form
    {


        Pen myPen = new Pen(Color.Black);
        public Form2()
        {

            InitializeComponent();

            //создание раздела "Файл"
            ToolStripMenuItem fileitem = new ToolStripMenuItem("Файл");


            //создание раздела "Сохранить", который является выпадающим элементом
            ToolStripMenuItem save = new ToolStripMenuItem("Сохранить");

            //создание раздела "Об авторе"
            ToolStripMenuItem about_author = new ToolStripMenuItem("Об авторе");

            //создание раздела "Инструкция"
            ToolStripMenuItem instruction = new ToolStripMenuItem("Инструкция");

            //событие для сохранения в файл
            save.Click += save_Click;
            fileitem.DropDownItems.Add(save);


            //Добавление в меню раздела "Файл" 
            menuStrip1.Items.Add(fileitem);

            //событие для добавления раздела "Инструкция"
            instruction.Click += instruction_Click;
            //Добавления данного раздела в меню
            menuStrip1.Items.Add(instruction);

            //событие для предоставлении информации об авторе
            about_author.Click += about_author_Click;

            //Добавление в меню раздела "Об авторе"
            menuStrip1.Items.Add(about_author);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ris();
        }

        //переменные для площадей фигур
        float S_triangle = 0;
        float S_square = 0;
        float S_ALL = 0;
        float S_rectangle = 0;
        float S_parallelogram = 0;
        float S_rhomb = 0;
        float S_trapezoid = 0;
        float S_circle = 0;

        float S_SQUARE_SUPER = 0;


        //массив сторон и координат треугольника для проверки на повторение
        float[] ab_t2 = new float[100];
        float[] bc_t2 = new float[100];
        float[] ac_t2 = new float[100];

        float[] tax = new float[100];
        float[] tay = new float[100];
        float[] tbx = new float[100];
        float[] tby = new float[100];
        float[] tcx = new float[100];
        float[] tcy = new float[100];
        float s1_sq = 0;
        float s2_sq = 0;
        float s3_sq = 0;
        float[] max_S_t = new float[100];


        int i_t = 0;

        //массив сторон и координат прямоугольника для проверки на повторение

        float[] rax = new float[100];
        float[] ray = new float[100];
        float[] rbx = new float[100];
        float[] rby = new float[100];
        float[] rcx = new float[100];
        float[] rcy = new float[100];
        float[] rdx = new float[100];
        float[] rdy = new float[100];



        float[] ab_r = new float[100];
        float[] bc_r = new float[100];

        int i_r = 0;
        float[] max_S_r = new float[100];

        //массив сторон и координат квадрата для проверки на повторение

        float[] ab_sq2 = new float[100];

        float[] sax = new float[100];
        float[] say = new float[100];
        float[] sbx = new float[100];
        float[] sby = new float[100];
        float[] scx = new float[100];
        float[] scy = new float[100];
        float[] sdx = new float[100];
        float[] sdy = new float[100];

        float[] s_points = new float[8];

        float[] max_S_s = new float[100];

        int i_sq = 0;

        //массив сторон и координат трапеции для проверки на повторение

        float[] tzax = new float[100];
        float[] tzay = new float[100];
        float[] tzbx = new float[100];
        float[] tzby = new float[100];
        float[] tzcx = new float[100];
        float[] tzcy = new float[100];
        float[] tzdx = new float[100];
        float[] tzdy = new float[100];

        float[] ab_tz = new float[100];
        float[] bc_tz = new float[100];
        float[] cd_tz = new float[100];
        float[] da_tz = new float[100];

        float[] max_S_tz = new float[100];

        int i_tz = 0;

        //массив сторон и координат ромба для проверки на повторение

        float[] rhax = new float[100];
        float[] rhay = new float[100];
        float[] rhbx = new float[100];
        float[] rhby = new float[100];
        float[] rhcx = new float[100];
        float[] rhcy = new float[100];
        float[] rhdx = new float[100];
        float[] rhdy = new float[100];

        float[] ab_rh = new float[100];
        float[] bc_rh = new float[100];

        float[] max_S_rh = new float[100];

        int i_rh = 0;

        //массив сторон и координат круга для проверки на повторение
        float[] cax = new float[100];
        float[] cay = new float[100];
        float[] cbx = new float[100];
        float[] cby = new float[100];
        float[] rad = new float[100];

        float[] max_S_c = new float[100];

        int i_c = 0;

        //массив сторон и координат параллелограмма для проверки на повторение

        float[] pax = new float[100];
        float[] pay = new float[100];
        float[] pbx = new float[100];
        float[] pby = new float[100];
        float[] pcx = new float[100];
        float[] pcy = new float[100];
        float[] pdx = new float[100];
        float[] pdy = new float[100];

        float[] ab_p = new float[100];
        float[] bc_p = new float[100];
        float[] cd_p = new float[100];
        float[] ad_p = new float[100];

        float[] max_S_p = new float[100];

        int i_p = 0;

        bool t = false;

        bool S_S_create = false;
        float SQ = 0;

        bool ris_SS = false;
        //проверка нажатия кнопки
        bool check_t = false;
        bool check_sq = false;
        bool check_r = false;
        bool check_p = false;
        bool check_tz = false;
        bool check_rh = false;
        bool check_c = false;

        bool replace = true;

        public void ris()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);

                //создаем сетку
                for (double i = 0; i < pictureBox1.Width; i = i + pictureBox1.Width / 31.9999)
                {
                    g.DrawLine(new Pen(Color.DarkSeaGreen, 1), (float)i, 0, (float)i, pictureBox1.Height);
                    g.DrawLine(new Pen(Color.Black, 1), (float)i, pictureBox1.Height / 2 + 3, (float)i, pictureBox1.Height / 2 - 3);

                }

                for (int i = 0; i < pictureBox1.Height; i = i + pictureBox1.Height / 24)
                {
                    g.DrawLine(new Pen(Color.DarkSeaGreen, 1), 0, i, pictureBox1.Width, i);
                    g.DrawLine(new Pen(Color.Black, 1), pictureBox1.Width / 2 + 3, i, pictureBox1.Width / 2 - 3, i);

                }

                int k = 0;
                int r = 0;
                int i1 = -11;
                int i2 = -15;
                // создаем отметки координат
                Font f = new System.Drawing.Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                SolidBrush f1 = new SolidBrush(Color.Black);

                for (k = 0; k < 24; k++)
                {
                    if (i1 < 12)
                    {
                        g.DrawString($"{i1}", f, f1, pictureBox1.Width / 2 + 5, (float)(pictureBox1.Width / 31.9999 + 368 - 17 * k));
                        i1++;
                    }
                }

                for (r = 0; r < 15; r++)
                {
                    if (i2 < 16)
                    {
                        g.DrawString($"{i2}", f, f1, (float)(pictureBox1.Width / 24 - 15 + 16.3 * r), pictureBox1.Height / 2 + 5);
                        i2++;
                    }
                }
                i2 = 1;
                for (r = 17; (r > 16) & (r < 32); r++)
                {
                    if (i2 < 16)
                    {
                        g.DrawString($"{i2}", f, f1, (float)(pictureBox1.Width / 24 - 27 + 16.2 * r), pictureBox1.Height / 2 + 5);
                        i2++;
                    }
                }

                g.DrawLine(new Pen(Color.Black, 1), pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
                g.DrawLine(new Pen(Color.Black, 1), 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);



                g.Dispose();

            }

            if (t == false)
            {
                pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                using (Graphics g = Graphics.FromImage(pictureBox2.Image))
                {
                    g.Clear(Color.White);

                    //создаем сетку
                    for (double i = 0; i < pictureBox2.Width; i = i + pictureBox2.Width / 31.9999)
                    {
                        g.DrawLine(new Pen(Color.DarkSeaGreen, 1), (float)i, 0, (float)i, pictureBox2.Height);
                        g.DrawLine(new Pen(Color.Black, 1), (float)i, pictureBox2.Height / 2 + 3, (float)i, pictureBox2.Height / 2 - 3);

                    }

                    for (int i = 0; i < pictureBox2.Height; i = i + pictureBox2.Height / 24)
                    {
                        g.DrawLine(new Pen(Color.DarkSeaGreen, 1), 0, i, pictureBox2.Width, i);
                        g.DrawLine(new Pen(Color.Black, 1), pictureBox2.Width / 2 + 3, i, pictureBox2.Width / 2 - 3, i);

                    }

                    int k = 0;
                    int r = 0;
                    int i1 = -11;
                    int i2 = -15;
                    // создаем отметки координат
                    Font f = new System.Drawing.Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                    SolidBrush f1 = new SolidBrush(Color.Black);

                    for (k = 0; k < 24; k++)
                    {
                        if (i1 < 12)
                        {
                            g.DrawString($"{i1}", f, f1, pictureBox2.Width / 2 + 5, (float)(pictureBox2.Width / 31.9999 + 368 - 17 * k));
                            i1++;
                        }
                    }

                    for (r = 0; r < 15; r++)
                    {
                        if (i2 < 16)
                        {
                            g.DrawString($"{i2}", f, f1, (float)(pictureBox2.Width / 24 - 15 + 16.3 * r), pictureBox2.Height / 2 + 5);
                            i2++;
                        }
                    }
                    i2 = 1;
                    for (r = 17; (r > 16) & (r < 32); r++)
                    {
                        if (i2 < 16)
                        {
                            g.DrawString($"{i2}", f, f1, (float)(pictureBox2.Width / 24 - 27 + 16.2 * r), pictureBox2.Height / 2 + 5);
                            i2++;
                        }
                    }

                    g.DrawLine(new Pen(Color.Black, 1), pictureBox2.Width / 2, 0, pictureBox2.Width / 2, pictureBox2.Height);
                    g.DrawLine(new Pen(Color.Black, 1), 0, pictureBox2.Height / 2, pictureBox1.Width, pictureBox2.Height / 2);

                    g.Dispose();
                    t = true;
                }

            }
        }
        public void risPB2()
        {
            {
                pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                using (Graphics g = Graphics.FromImage(pictureBox2.Image))
                {
                    g.Clear(Color.White);

                    //создаем сетку
                    for (double i = 0; i < pictureBox2.Width; i = i + pictureBox2.Width / 31.9999)
                    {
                        g.DrawLine(new Pen(Color.DarkSeaGreen, 1), (float)i, 0, (float)i, pictureBox2.Height);
                        g.DrawLine(new Pen(Color.Black, 1), (float)i, pictureBox2.Height / 2 + 3, (float)i, pictureBox2.Height / 2 - 3);

                    }

                    for (int i = 0; i < pictureBox2.Height; i = i + pictureBox2.Height / 24)
                    {
                        g.DrawLine(new Pen(Color.DarkSeaGreen, 1), 0, i, pictureBox2.Width, i);
                        g.DrawLine(new Pen(Color.Black, 1), pictureBox2.Width / 2 + 3, i, pictureBox2.Width / 2 - 3, i);

                    }

                    int k = 0;
                    int r = 0;
                    int i1 = -11;
                    int i2 = -15;
                    // создаем отметки координат
                    Font f = new System.Drawing.Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                    SolidBrush f1 = new SolidBrush(Color.Black);

                    for (k = 0; k < 24; k++)
                    {
                        if (i1 < 12)
                        {
                            g.DrawString($"{i1}", f, f1, pictureBox2.Width / 2 + 5, (float)(pictureBox2.Width / 31.9999 + 368 - 17 * k));
                            i1++;
                        }
                    }

                    for (r = 0; r < 15; r++)
                    {
                        if (i2 < 16)
                        {
                            g.DrawString($"{i2}", f, f1, (float)(pictureBox2.Width / 24 - 15 + 16.3 * r), pictureBox2.Height / 2 + 5);
                            i2++;
                        }
                    }
                    i2 = 1;
                    for (r = 17; (r > 16) & (r < 32); r++)
                    {
                        if (i2 < 16)
                        {
                            g.DrawString($"{i2}", f, f1, (float)(pictureBox2.Width / 24 - 27 + 16.2 * r), pictureBox2.Height / 2 + 5);
                            i2++;
                        }
                    }

                    g.DrawLine(new Pen(Color.Black, 1), pictureBox2.Width / 2, 0, pictureBox2.Width / 2, pictureBox2.Height);
                    g.DrawLine(new Pen(Color.Black, 1), 0, pictureBox2.Height / 2, pictureBox1.Width, pictureBox2.Height / 2);

                    g.Dispose();
                    t = true;
                }

            }
        }
        public void risPB1()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);

                //создаем сетку
                for (double i = 0; i < pictureBox1.Width; i = i + pictureBox1.Width / 31.9999)
                {
                    g.DrawLine(new Pen(Color.DarkSeaGreen, 1), (float)i, 0, (float)i, pictureBox1.Height);
                    g.DrawLine(new Pen(Color.Black, 1), (float)i, pictureBox1.Height / 2 + 3, (float)i, pictureBox1.Height / 2 - 3);

                }

                for (int i = 0; i < pictureBox1.Height; i = i + pictureBox1.Height / 24)
                {
                    g.DrawLine(new Pen(Color.DarkSeaGreen, 1), 0, i, pictureBox1.Width, i);
                    g.DrawLine(new Pen(Color.Black, 1), pictureBox1.Width / 2 + 3, i, pictureBox1.Width / 2 - 3, i);

                }

                int k = 0;
                int r = 0;
                int i1 = -11;
                int i2 = -15;
                // создаем отметки координат
                Font f = new System.Drawing.Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                SolidBrush f1 = new SolidBrush(Color.Black);

                for (k = 0; k < 24; k++)
                {
                    if (i1 < 12)
                    {
                        g.DrawString($"{i1}", f, f1, pictureBox1.Width / 2 + 5, (float)(pictureBox1.Width / 31.9999 + 368 - 17 * k));
                        i1++;
                    }
                }

                for (r = 0; r < 15; r++)
                {
                    if (i2 < 16)
                    {
                        g.DrawString($"{i2}", f, f1, (float)(pictureBox1.Width / 24 - 15 + 16.3 * r), pictureBox1.Height / 2 + 5);
                        i2++;
                    }
                }
                i2 = 1;
                for (r = 17; (r > 16) & (r < 32); r++)
                {
                    if (i2 < 16)
                    {
                        g.DrawString($"{i2}", f, f1, (float)(pictureBox1.Width / 24 - 27 + 16.2 * r), pictureBox1.Height / 2 + 5);
                        i2++;
                    }
                }

                g.DrawLine(new Pen(Color.Black, 1), pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
                g.DrawLine(new Pen(Color.Black, 1), 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);



                g.Dispose();

            }
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image.Save(saveFileDialog.FileName);
                }
            }

        }

        void instruction_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }
        void about_author_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работу выполнил Жилин И. А.\nГруппа № 3043.\nПочта: Ilyazhilinne@gmail.com.");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_t = true;
            check_sq = false;
            check_r = false;
            check_p = false;
            check_tz = false;
            check_rh = false;
            check_c = false;

            textBox42.Clear();
            if (radioButton1.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton1.Checked = false;
                triangle();

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check_t = false;
            check_sq = true;
            check_r = false;
            check_p = false;
            check_tz = false;
            check_rh = false;
            check_c = false;

            textBox43.Clear();
            if (radioButton2.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton2.Checked = false;
                square();
                S_ALL += Figure.S(0, S_square, 0, 0, 0, 0, 0);
                textBox49.Text = ($"{S_ALL}");

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check_t = false;
            check_sq = false;
            check_r = true;
            check_p = false;
            check_tz = false;
            check_rh = false;
            check_c = false;

            textBox44.Clear();
            if (radioButton3.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton3.Checked = false;
                rectangle();
                S_ALL += Figure.S(0, 0, S_rectangle, 0, 0, 0, 0);
                textBox49.Text = ($"{S_ALL}");

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check_t = false;
            check_sq = false;
            check_r = false;
            check_p = true;
            check_tz = false;
            check_rh = false;
            check_c = false;
            textBox53.Clear();
            if (radioButton4.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton4.Checked = false;
                parallelogram();
                S_ALL += Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0);
                textBox49.Text = ($"{S_ALL}");

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            check_t = false;
            check_sq = false;
            check_r = false;
            check_p = false;
            check_tz = false;
            check_rh = true;
            check_c = false;

            textBox46.Clear();
            if (radioButton5.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton5.Checked = false;
                rhomb();
                S_ALL += Figure.S(0, 0, 0, 0, S_rhomb, 0, 0);
                textBox49.Text = ($"{S_ALL}");

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            check_t = false;
            check_sq = false;
            check_r = false;
            check_p = false;
            check_tz = true;
            check_rh = false;
            check_c = false;

            textBox47.Clear();
            if (radioButton6.Checked == true)
            {
                var Figure = new Figure();
                risPB1();
                radioButton6.Checked = false;

                trapezoid();
                S_ALL += Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                textBox49.Text = ($"{S_ALL}");

            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                check_t = false;
                check_sq = false;
                check_r = false;
                check_p = false;
                check_tz = false;
                check_rh = false;
                check_c = true;

            
            
                for (int k = 0; i_c < numericUpDown2.Value; k++)
                {
                    radioButton7.Checked = true;
                    var Figure = new Figure();
                    
                    replace = true;
                    circle();
                    ris_C();
                    textBox48.Clear();
                    S_ALL += Figure.S(0, 0, 0, 0, 0, 0, S_circle);
                    textBox49.Text = ($"{S_ALL}");
                }
                radioButton7.Checked = false;
            }
            else
                MessageBox.Show("Вы не выбрали тип фигуры!");
        }

        //рисование треугольника по координатам



        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();

            using (k = Graphics.FromImage(btmp1))
            {

                int j = 0;

                try
                {
                    if (i_t < 100)
                    {
                        j = i_t - 1;
                        Point[] Points = { new Point((int)(tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                               new Point((int)(tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                               new Point((int)(tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Red);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else if (i_t == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                               new Point((int)(tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                               new Point((int)(tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Red);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2, tax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2, -tay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные треугольники");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали треугольник");
                    }

                }

                k.Dispose();

                pictureBox1.Image = btmp1;

            }
        }
        
        public void ris_triangle()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();

            using (k = Graphics.FromImage(btmp1))
            {

                int j = 0;

                try
                {
                    if (i_t < 100)
                    {
                        j = i_t - 1;
                        Point[] Points = { new Point((int)(tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                               new Point((int)(tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                               new Point((int)(tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Red);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_t == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                               new Point((int)(tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                               new Point((int)(tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Red);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2, 
                            tax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2, -tay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные треугольники");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали треугольник");
                    }

                }

                k.Dispose();

                pictureBox2.Image = btmp1;

            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_sq < 100)
                    {
                        j = i_sq - 1;
                        Point[] Points = { new Point((int)(sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Yellow);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                    }
                    else if (i_sq == 100)
                    {
                        j = 99;
                        Point[] Points = {  new Point((int)(sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Yellow);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), sbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), scx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -scy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), sdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -sdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       sax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -say[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные квадраты");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали квадрат");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }
        public void ris_square()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_sq < 100)
                    {
                        j = i_sq - 1;
                        Point[] Points = { new Point((int)(sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Yellow);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_sq == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Yellow);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), sbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), scx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -scy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), sdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -sdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       sax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -say[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные квадраты");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали квадрат");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_r < 100)
                    {
                        j = i_r - 1;
                        Point[] Points = { new Point((int)(rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Green);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else if (i_sq == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Green);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       rax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -ray[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные прямоугольники");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали прямоугольник");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }

        public void ris_rectangle()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_r < 100)
                    {
                        j = i_r - 1;
                        Point[] Points = { new Point((int)(rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Green);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_sq == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Green);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       rax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -ray[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные прямоугольники");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали прямоугольник");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_p < 100)
                    {
                        j = i_p - 1;
                        Point[] Points = { new Point((int)(pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Blue);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else if (i_p == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Blue);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), pdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -pdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       pax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -pay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные параллелограммы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали параллелограмм");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }

        public void ris_parallelogram()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_p < 100)
                    {
                        j = i_p - 1;
                        Point[] Points = { new Point((int)(pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Blue);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_p == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Blue);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -pby[j] * (float)pictureBox2.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), pcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), pdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -pdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       pax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -pay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные параллелограммы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали параллелограмм");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_rh < 100)
                    {
                        j = i_rh - 1;
                        Point[] Points = { new Point((int)(rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Purple);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else if (i_rh == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Purple);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rhdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rhdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       rhax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -rhay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные ромбы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали ромб");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }

        public void ris_rhomb()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_rh < 100)
                    {
                        j = i_rh - 1;
                        Point[] Points = { new Point((int)(rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Purple);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_rh == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                            (int)(-rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Purple);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), rhcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), rhdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rhdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       rhax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -rhay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные ромбы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали ромб");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_tz < 100)
                    {
                        j = i_tz - 1;
                        Point[] Points = { new Point((int)(tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Violet);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else if (i_tz == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2)),
                                           new Point((int)(tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2), (int)(-tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Violet);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzbx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzby[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzcx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzcy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                   tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                   -tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);
                        k.DrawLine(new Pen(Color.Black, 2), tzdx[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -tzdy[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2,
                                       tzax[j] * (float)(pictureBox1.Width / 31.9999) + (float)pictureBox1.Width / 2,
                                       -tzay[j] * (float)pictureBox1.Height / 24 + pictureBox1.Height / 2);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные ромбы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали ромб");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }

        public void ris_trapezoid()
        {
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_tz < 100)
                    {
                        j = i_tz - 1;
                        Point[] Points = { new Point((int)(tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), 
                                           (int)(-tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Violet);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else if (i_tz == 100)
                    {
                        j = 99;
                        Point[] Points = { new Point((int)(tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2)),
                                           new Point((int)(tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2), (int)(-tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2))};

                        SolidBrush Ta = new SolidBrush(Color.Violet);

                        k.FillPolygon(Ta, Points);

                        k.DrawLine(new Pen(Color.Black, 2), tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzbx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzby[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzcx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzcy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                   tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                   -tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                        k.DrawLine(new Pen(Color.Black, 2), tzdx[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -tzdy[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                                       tzax[j] * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                                       -tzay[j] * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные ромбы");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали ромб");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Bitmap btmp1 = new Bitmap(pictureBox1.Image);
            //clear();

            Graphics k = pictureBox1.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_c < numericUpDown2.Value)
                    {
                        j = i_c - 1;


                        SolidBrush Ta = new SolidBrush(Color.BlueViolet);
                        float l = (float)Math.Pow(rad[j], 2);
                        l = 2 * l;
                        l = (float)Math.Pow(l, 0.5);

                        RectangleF rect = new RectangleF(
                            (pictureBox1.Width / 2 - rad[j] * (float)(pictureBox1.Width / 31.9999) + cax[j] * (float)(pictureBox1.Width / 31.9999)),
                            (pictureBox1.Height / 2 - rad[j] * (float)pictureBox1.Height / 24 - cay[j] * (float)(pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Height / 24)));

                        RectangleF rect1 = new RectangleF(0.0F, 0.0F, 200.0F, 100.0F);

                        k.FillEllipse(Ta, rect);
                        k.DrawEllipse(new Pen(Color.Black, 2), rect);


                    }
                    else if (i_tz == 100)
                    {
                        j = 99;
                        SolidBrush Ta = new SolidBrush(Color.BlueViolet);

                        RectangleF rect = new RectangleF(
                            (pictureBox1.Width / 2 - rad[j] * (float)(pictureBox1.Width / 31.9999) + cax[j] * (float)(pictureBox1.Width / 31.9999)),
                            (pictureBox1.Height / 2 - rad[j] * (float)pictureBox1.Height / 24 - cay[j] * (float)(pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Height / 24)));

                        RectangleF rect1 = new RectangleF(0.0F, 0.0F, 200.0F, 100.0F);

                        k.FillEllipse(Ta, rect);
                        k.DrawEllipse(new Pen(Color.Black, 2), rect);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные круги");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали круг");
                    }

                }

                k.Dispose();
                pictureBox1.Image = btmp1;
            }
        }

        // нахождение площади треугольника и проверка на координаты
        public void triangle()
        {
            var Triangle = new Triangle();
            var Figure = new Figure();
            float PreS = 0, ab_t1 = 0, bc_t1 = 0, ac_t1 = 0, h_t = 0;
            Random rand = new Random();
            try
            {
                //конвертиуем значения координат

                tax[i_t] = Convert.ToSingle(textBox1.Text);
                tay[i_t] = Convert.ToSingle(textBox2.Text);
                tbx[i_t] = Convert.ToSingle(textBox3.Text);
                tby[i_t] = Convert.ToSingle(textBox4.Text);
                tcx[i_t] = Convert.ToSingle(textBox5.Text);
                tcy[i_t] = Convert.ToSingle(textBox6.Text);

            }

            catch
            {

                MessageBox.Show("Вы ввели некорректное значение координаты");

            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if ((tax[i_t] >= -SQ / 2) & (tax[i_t] <= SQ / 2) & (tbx[i_t] >= -SQ / 2) & (tbx[i_t] <= SQ / 2) & (tcx[i_t] >= -SQ / 2) & (tcx[i_t] <= SQ / 2) &
                            (tay[i_t] >= -SQ / 2) & (tay[i_t] <= SQ / 2) & (tby[i_t] >= -SQ / 2) & (tby[i_t] <= SQ / 2) & (tcy[i_t] >= -SQ / 2) & (tcy[i_t] <= SQ / 2))
                        {

                            //найдем стороны треугольника для проверки правила: сумма двух любых сторон больше третьей (как расстояние между двумя точками)
                            if (i_t < numericUpDown1.Value)
                            {
                                ab_t1 = (tax[i_t] - tbx[i_t]) * (tax[i_t] - tbx[i_t]) + (tay[i_t] - tby[i_t]) * (tay[i_t] - tby[i_t]);
                                bc_t1 = (tbx[i_t] - tcx[i_t]) * (tbx[i_t] - tcx[i_t]) + (tby[i_t] - tcy[i_t]) * (tby[i_t] - tcy[i_t]);
                                ac_t1 = (tcx[i_t] - tax[i_t]) * (tcx[i_t] - tax[i_t]) + (tcy[i_t] - tay[i_t]) * (tcy[i_t] - tay[i_t]);

                                ab_t2[i_t] = (float)Math.Pow(ab_t1, 0.5);
                                bc_t2[i_t] = (float)Math.Pow(bc_t1, 0.5);
                                ac_t2[i_t] = (float)Math.Pow(ac_t1, 0.5);



                                float ab_t2_sq = ab_t2[i_t] * ab_t2[i_t];
                                float bc_t2_sq = bc_t2[i_t] * bc_t2[i_t];
                                float ac_t2_sq = ac_t2[i_t] * ac_t2[i_t];

                                s1_sq = bc_t2_sq + ac_t2_sq;
                                s2_sq = ab_t2_sq + ac_t2_sq;
                                s3_sq = bc_t2_sq + ab_t2_sq;



                                if ((ab_t2[i_t] + bc_t2[i_t] < ac_t2[i_t]) | (ab_t2[i_t] + ac_t2[i_t] < bc_t2[i_t]) | (bc_t2[i_t] + ac_t2[i_t] < ab_t2[i_t]))
                                {
                                    MessageBox.Show("Сумма двух любых сторон должна быть больше третьей");
                                }
                                else if ((ab_t2[i_t] != 0) & (bc_t2[i_t] != 0) & (ac_t2[i_t] != 0))
                                {

                                    PreS = Triangle.S(tax[i_t], tay[i_t], tbx[i_t], tby[i_t], tcx[i_t], tcy[i_t]);
                                    PreS = PreS * 2;
                                    if (PreS > 0)
                                    {
                                        S_triangle = PreS / 2;



                                        textBox42.Text = $"{(S_triangle)}";

                                        if (((ab_t2[i_t] == bc_t2[i_t]) | (ab_t2[i_t] == ac_t2[i_t]) | (bc_t2[i_t] == ac_t2[i_t])) & (!((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] == ac_t2[i_t]) & (bc_t2[i_t] == ac_t2[i_t]))))
                                        {
                                            check_t = true;
                                            replace = false;

                                            if ((ab_t2[i_t] == (float)Math.Pow((s1_sq), 0.5)) | (bc_t2[i_t] == (float)Math.Pow((s2_sq), 0.5)) | (ac_t2[i_t] == (float)Math.Pow((s3_sq), 0.5)))
                                            {

                                                if (ab_t2[i_t] == (float)Math.Pow((s1_sq), 0.5))
                                                {
                                                    if (bc_t2[i_t] > ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < bc_t2[i_t])
                                                            max_S_t[i_t] = bc_t2[i_t];

                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }
                                                    if (bc_t2[i_t] == ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }

                                                }
                                                else if (bc_t2[i_t] == (float)Math.Pow((s2_sq), 0.5))
                                                {
                                                    if (ac_t2[i_t] > ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];

                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                    if (ab_t2[i_t] == ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                }
                                                else if (ac_t2[i_t] == (float)Math.Pow((s3_sq), 0.5))
                                                {
                                                    if (bc_t2[i_t] > ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < bc_t2[i_t])
                                                            max_S_t[i_t] = bc_t2[i_t];

                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                    if (bc_t2[i_t] == ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                }
                                                i_t++;
                                            }
                                            else
                                            {
                                                if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] > ac_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / ac_t2[i_t];
                                                    if (max_S_t[i_t] < h_t)
                                                        max_S_t[i_t] = h_t;

                                                }
                                                if ((ac_t2[i_t] == bc_t2[i_t]) & (ac_t2[i_t] > ab_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / ab_t2[i_t];
                                                    max_S_t[i_t] = h_t;

                                                }
                                                if ((ab_t2[i_t] == ac_t2[i_t]) & (ac_t2[i_t] > bc_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / bc_t2[i_t];
                                                    if (max_S_t[i_t] < h_t)
                                                        max_S_t[i_t] = h_t;

                                                }

                                                if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] < ac_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < ac_t2[i_t])
                                                        max_S_t[i_t] = ac_t2[i_t];

                                                }
                                                if ((ac_t2[i_t] == bc_t2[i_t]) & (bc_t2[i_t] < ab_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < ab_t2[i_t])
                                                        max_S_t[i_t] = ab_t2[i_t];

                                                }
                                                if ((ab_t2[i_t] == ac_t2[i_t]) & (ac_t2[i_t] < bc_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < bc_t2[i_t])
                                                        max_S_t[i_t] = bc_t2[i_t];

                                                }
                                                i_t++;
                                            }
                                        }

                                        else if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] == ac_t2[i_t]) & (bc_t2[i_t] == ac_t2[i_t]))
                                        {
                                            check_t = true;
                                            replace = false;

                                            if (max_S_t[i_t] < ab_t2[i_t])
                                                max_S_t[i_t] = ab_t2[i_t];

                                            i_t++;
                                        }


                                        else
                                        {
                                            check_t = false;

                                            MessageBox.Show("Данный вид треугольника не обрабатывается в этом решении");
                                            textBox1.Clear();
                                            textBox2.Clear();
                                            textBox3.Clear();
                                            textBox4.Clear();
                                            textBox5.Clear();
                                            textBox6.Clear();
                                            textBox42.Clear();
                                            S_ALL -= Figure.S(S_triangle, 0, 0, 0, 0, 0, 0);

                                        }

                                    }
                                    else if (PreS < 0)
                                    {
                                        PreS = -PreS;
                                        S_triangle = PreS / 2;
                                        textBox42.Text = $"{(S_triangle)}";

                                        if ((ab_t2[i_t] == bc_t2[i_t]) | (ab_t2[i_t] == ac_t2[i_t]) | (bc_t2[i_t] == ac_t2[i_t]) & (!((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] == ac_t2[i_t]) & (bc_t2[i_t] == ac_t2[i_t]))))
                                        {
                                            check_t = true;
                                            replace = false;

                                            if ((ab_t2[i_t] == (float)Math.Pow((s1_sq), 0.5)) | (bc_t2[i_t] == (float)Math.Pow((s2_sq), 0.5)) | (ac_t2[i_t] == (float)Math.Pow((s3_sq), 0.5)))
                                            {

                                                if (ab_t2[i_t] == (float)Math.Pow((s1_sq), 0.5))
                                                {
                                                    if (bc_t2[i_t] > ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < bc_t2[i_t])
                                                            max_S_t[i_t] = bc_t2[i_t];

                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }
                                                    if (bc_t2[i_t] == ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }

                                                }
                                                else if (bc_t2[i_t] == (float)Math.Pow((s2_sq), 0.5))
                                                {
                                                    if (ac_t2[i_t] > ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];

                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                    if (ab_t2[i_t] == ac_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ac_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }

                                                }
                                                else if (ac_t2[i_t] == (float)Math.Pow((s3_sq), 0.5))
                                                {
                                                    if (bc_t2[i_t] > ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < bc_t2[i_t])
                                                            max_S_t[i_t] = bc_t2[i_t];

                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ab_t2[i_t];
                                                    }
                                                    if (bc_t2[i_t] == ab_t2[i_t])
                                                    {
                                                        if (max_S_t[i_t] < ab_t2[i_t])
                                                            max_S_t[i_t] = ac_t2[i_t];
                                                    }

                                                }
                                                i_t++;
                                            }
                                            else
                                            {
                                                if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] > ac_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / ac_t2[i_t];
                                                    if (max_S_t[i_t] < h_t)
                                                        max_S_t[i_t] = h_t;

                                                }
                                                if ((ac_t2[i_t] == bc_t2[i_t]) & (ac_t2[i_t] > ab_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / ab_t2[i_t];
                                                    if (max_S_t[i_t] < h_t)
                                                        max_S_t[i_t] = h_t;

                                                }
                                                if ((ab_t2[i_t] == ac_t2[i_t]) & (ac_t2[i_t] > bc_t2[i_t]))
                                                {
                                                    h_t = 2 * S_triangle / bc_t2[i_t];
                                                    if (max_S_t[i_t] < h_t)
                                                        max_S_t[i_t] = h_t;

                                                }

                                                if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] < ac_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < ac_t2[i_t])
                                                        max_S_t[i_t] = ac_t2[i_t];

                                                }
                                                if ((ac_t2[i_t] == bc_t2[i_t]) & (bc_t2[i_t] < ab_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < ab_t2[i_t])
                                                        max_S_t[i_t] = ab_t2[i_t];

                                                }
                                                else if ((ab_t2[i_t] == ac_t2[i_t]) & (ac_t2[i_t] < bc_t2[i_t]))
                                                {
                                                    if (max_S_t[i_t] < bc_t2[i_t])
                                                        max_S_t[i_t] = bc_t2[i_t];

                                                }
                                                i_t++;
                                            }
                                        }

                                        else if ((ab_t2[i_t] == bc_t2[i_t]) & (ab_t2[i_t] == ac_t2[i_t]) & (bc_t2[i_t] == ac_t2[i_t]))
                                        {
                                            check_t = true;
                                            replace = false;

                                            max_S_t[i_t] = ab_t2[i_t];
                                            i_t++;
                                        }


                                        else
                                        {
                                            check_t = false;
                                            replace = true;

                                            MessageBox.Show("Данный вид треугольника не обрабатывается в этом решении");
                                            textBox1.Clear();
                                            textBox2.Clear();
                                            textBox3.Clear();
                                            textBox4.Clear();
                                            textBox5.Clear();
                                            textBox6.Clear();
                                            textBox42.Clear();
                                            S_ALL -= Figure.S(S_triangle, 0, 0, 0, 0, 0, 0);
                                        }
                                    }
                                    else
                                    {
                                        check_t = false;
                                        replace = true;

                                        MessageBox.Show("Вы ввели некорректные координаты, при которых площадь треугольника равна 0");
                                        if (S_triangle != 0)
                                        {
                                            S_ALL -= Figure.S(S_triangle, 0, 0, 0, 0, 0, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    check_t = false;
                                    replace = true;

                                    MessageBox.Show("Вы ввели некорректные координаты, при которых площадь треугольника равна 0");
                                    if (S_triangle != 0)
                                    {
                                        S_ALL -= Figure.S(S_triangle, 0, 0, 0, 0, 0, 0);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Вы использовали максимальное количество треугольников");
                                check_t = false;
                                replace = true;
                            }
                        }
                        else
                        {
                            check_t = false;
                            replace = true;

                            MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не переместили фигуру");
                    }
                }
            }

        }

        // нахождение площади квадрата, являются ли три введеных + вычисленная координаты квадратом
        public void square()
        {
            var Square = new Square();
            var Figure = new Figure();
            float vab = 0, vac = 0, vbc = 0, ab_sq1 = 0, bc_sq1 = 0, bc_sq2 = 0;
            try
            {
                //конвертиуем значения координат

                sax[i_sq] = Convert.ToSingle(textBox7.Text);
                say[i_sq] = Convert.ToSingle(textBox8.Text);
                sbx[i_sq] = Convert.ToSingle(textBox9.Text);
                sby[i_sq] = Convert.ToSingle(textBox10.Text);
                scx[i_sq] = Convert.ToSingle(textBox11.Text);
                scy[i_sq] = Convert.ToSingle(textBox12.Text);

            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное знаение координаты");
            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {

                    if (replace == true)
                    {
                        if ((sax[i_sq] >= -SQ / 2) & (sax[i_sq] <= SQ / 2) & (sbx[i_sq] >= -SQ / 2) & (sbx[i_sq] <= SQ / 2) & (scx[i_sq] >= -SQ / 2) & (scx[i_sq] <= SQ / 2) &
                            (say[i_sq] >= -SQ / 2) & (say[i_sq] <= SQ / 2) & (sby[i_sq] >= -SQ / 2) & (sby[i_sq] <= SQ / 2) & (scy[i_sq] >= -SQ / 2) & (scy[i_sq] <= SQ / 2))
                        {
                            check_sq = true;
                            replace = false;

                            if (i_sq < 100)
                            {
                                //нахождение 4 координаты с помощью скалярных векторов
                                vab = (sax[i_sq] - sbx[i_sq]) * (say[i_sq] - sby[i_sq]);
                                vac = (sax[i_sq] - scx[i_sq]) * (say[i_sq] - scy[i_sq]);
                                vbc = (sbx[i_sq] - scx[i_sq]) * (sby[i_sq] - scy[i_sq]);



                                if (vab == -vac)
                                {
                                    sdx[i_sq] = sbx[i_sq] + scx[i_sq] - sax[i_sq];
                                    sdy[i_sq] = sby[i_sq] + scy[i_sq] - say[i_sq];

                                    ab_sq1 = (sax[i_sq] - sbx[i_sq]) * (sax[i_sq] - sbx[i_sq]) + (say[i_sq] - sby[i_sq]) * (say[i_sq] - sby[i_sq]);
                                    ab_sq2[i_sq] = (float)Math.Pow(ab_sq1, 0.5);

                                    bc_sq1 = (sbx[i_sq] - scx[i_sq]) * (sbx[i_sq] - scx[i_sq]) + (sby[i_sq] - scy[i_sq]) * (sby[i_sq] - scy[i_sq]);
                                    bc_sq2 = (float)Math.Pow(bc_sq1, 0.5);
                                    if ((ab_sq2[i_t] == bc_sq2) & (ab_sq2[i_t] != 0))
                                    {
                                        textBox56.Text = ($"{sdx[i_sq]}");
                                        textBox57.Text = ($"{sdy[i_sq]}");
                                        if (max_S_s[i_sq] < ab_sq2[i_sq])
                                        {
                                            max_S_s[i_sq] = ab_sq2[i_sq];
                                        }

                                        S_square = Square.S(sax[i_sq], say[i_sq], sbx[i_sq], sby[i_sq]);
                                        textBox43.Text = ($"{(S_square)}");

                                        i_sq++;
                                    }
                                    else
                                    {
                                        check_sq = false;
                                        
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется квадрат");
                                        S_ALL -= Figure.S(0, S_square, 0, 0, 0, 0, 0);
                                    }
                                }
                                else if (vab == -vbc)
                                {
                                    sdx[i_sq] = sax[i_sq] + scx[i_sq] - sbx[i_sq];
                                    sdy[i_sq] = say[i_sq] + scy[i_sq] - sby[i_sq];

                                    ab_sq1 = (sax[i_sq] - sbx[i_sq]) * (sax[i_sq] - sbx[i_sq]) + (say[i_sq] - sby[i_sq]) * (say[i_sq] - sby[i_sq]);
                                    ab_sq2[i_sq] = (float)Math.Pow(ab_sq1, 0.5);

                                    bc_sq1 = (sbx[i_sq] - scx[i_sq]) * (sbx[i_sq] - scx[i_sq]) + (sby[i_sq] - scy[i_sq]) * (sby[i_sq] - scy[i_sq]);
                                    bc_sq2 = (float)Math.Pow(bc_sq1, 0.5);
                                    if ((ab_sq2[i_sq] == bc_sq2) & (ab_sq2[i_sq] != 0))
                                    {
                                        textBox56.Text = ($"{sdx[i_sq]}");
                                        textBox57.Text = ($"{sdy[i_sq]}");
                                        if (max_S_s[i_sq] < ab_sq2[i_sq])
                                        {
                                            max_S_s[i_sq] = ab_sq2[i_sq];
                                        }

                                        S_square = Square.S(sax[i_sq], say[i_sq], sbx[i_sq], sby[i_sq]);
                                        textBox43.Text = ($"{(S_square)}");

                                        i_sq++;
                                    }
                                    else
                                    {
                                        check_sq = false;
                                       
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется квадрат");
                                        S_ALL -= Figure.S(0, S_square, 0, 0, 0, 0, 0);
                                    }
                                }
                                else if (vbc == -vac)
                                {
                                    sdx[i_sq] = sax[i_sq] + sbx[i_sq] - scx[i_sq];
                                    sdy[i_sq] = say[i_sq] + sby[i_sq] - scy[i_sq];

                                    ab_sq1 = (sax[i_sq] - sbx[i_sq]) * (sax[i_sq] - sbx[i_sq]) + (say[i_sq] - sby[i_sq]) * (say[i_sq] - sby[i_sq]);
                                    ab_sq2[i_sq] = (float)Math.Pow(ab_sq1, 0.5);

                                    bc_sq1 = (sbx[i_sq] - scx[i_sq]) * (sbx[i_sq] - scx[i_sq]) + (sby[i_sq] - scy[i_sq]) * (sby[i_sq] - scy[i_sq]);
                                    bc_sq2 = (float)Math.Pow(bc_sq1, 0.5);
                                    if ((ab_sq2[i_sq] == bc_sq2) & (ab_sq2[i_sq] != 0))
                                    {
                                        textBox56.Text = ($"{sdx[i_sq]}");
                                        textBox57.Text = ($"{sdy[i_sq]}");
                                        if (max_S_s[i_sq] < ab_sq2[i_sq])
                                        {
                                            max_S_s[i_sq] = ab_sq2[i_sq];
                                        }

                                        S_square = Square.S(sax[i_sq], say[i_sq], sbx[i_sq], sby[i_sq]);
                                        textBox43.Text = ($"{(S_square)}");

                                        i_sq++;
                                    }
                                    else
                                    {

                                        check_sq = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется квадрат");
                                        S_ALL -= Figure.S(0, S_square, 0, 0, 0, 0, 0);
                                    }
                                }
                                else
                                {

                                    check_sq = false;
                                    replace = true;
                                    MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется квадрат");
                                    if (S_square != 0)
                                    {
                                        S_ALL -= Figure.S(0, S_square, 0, 0, 0, 0, 0);
                                    }
                                }
                            }
                            else
                            {

                                check_sq = false;

                                replace = true;
                                MessageBox.Show("Вы использовали максимальное количество квадратов");
                            }
                        }
                        else
                        {

                            check_sq = false;
                            replace = true;

                            MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                        }
                    }
                    else
                        MessageBox.Show("Вы не переместили фигуру");
                }
            }
        }

        //нахождение площади прямоугольника и 4-ой координаты

        public void rectangle()
        {
            var Rectangle = new Rectangle();
            var Figure = new Figure();

            float vab = 0, vac = 0, vbc = 0, preSr1 = 0, preSr2 = 0;
            try
            {
                //конвертиуем значения координат

                rax[i_r] = Convert.ToSingle(textBox13.Text);
                ray[i_r] = Convert.ToSingle(textBox14.Text);
                rbx[i_r] = Convert.ToSingle(textBox15.Text);
                rby[i_r] = Convert.ToSingle(textBox16.Text);
                rcx[i_r] = Convert.ToSingle(textBox17.Text);
                rcy[i_r] = Convert.ToSingle(textBox18.Text);

            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное знаение координаты");
            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if ((rax[i_r] >= -SQ / 2) & (rax[i_r] <= SQ / 2) & (rbx[i_r] >= -SQ / 2) & (rbx[i_r] <= SQ / 2) & (rcx[i_r] >= -SQ / 2) & (rcx[i_r] <= SQ / 2) &
                            (ray[i_r] >= -SQ / 2) & (ray[i_r] <= SQ / 2) & (rby[i_r] >= -SQ / 2) & (rby[i_r] <= SQ / 2) & (rcy[i_r] >= -SQ / 2) & (rcy[i_r] <= SQ / 2))
                        {
                            check_r = true;
                            replace = false;

                            if (i_r < 100)
                            {
                                //нахождение 4 координаты с помощью скалярных векторов путем нахождения вектора, перпендикулярному двум заданным векторам
                                vab = (rbx[i_r] - rax[i_r]) * (rcx[i_r] - rax[i_r]) + (rby[i_r] - ray[i_r]) * (rcy[i_r] - ray[i_r]);
                                vac = (rax[i_r] - rbx[i_r]) * (rcx[i_r] - rbx[i_r]) + (ray[i_r] - rby[i_r]) * (rcy[i_r] - rby[i_r]);
                                vbc = (rax[i_r] - rcx[i_r]) * (rbx[i_r] - rcx[i_r]) + (ray[i_r] - rcy[i_r]) * (rby[i_r] - rcy[i_r]);


                                if (vab == 0)
                                {
                                    rdx[i_r] = rcx[i_r] + rbx[i_r] - rax[i_r];
                                    rdy[i_r] = rby[i_r] + rcy[i_r] - ray[i_r];

                                    preSr1 = (float)Math.Pow((rax[i_r] - rbx[i_r]), 2) + (float)Math.Pow((rby[i_r] - ray[i_r]), 2);
                                    preSr2 = (float)Math.Pow((rbx[i_r] - rcx[i_r]), 2) + (float)Math.Pow((rby[i_r] - rcy[i_r]), 2);

                                    ab_r[i_r] = (rax[i_r] - sbx[i_r]) * (rax[i_r] - rbx[i_r]) + (ray[i_r] - rby[i_r]) * (ray[i_r] - rby[i_r]);
                                    ab_r[i_r] = (float)Math.Pow(ab_r[i_r], 0.5);

                                    bc_r[i_r] = (rbx[i_r] - rcx[i_r]) * (rbx[i_r] - rcx[i_r]) + (rby[i_r] - rcy[i_r]) * (rby[i_r] - rcy[i_r]);
                                    bc_r[i_r] = (float)Math.Pow(bc_r[i_r], 0.5);
                                    if ((ab_r[i_r] != 0) & (bc_r[i_r] != 0))
                                    {
                                        textBox58.Text = ($"{rdx[i_r]}");
                                        textBox59.Text = ($"{rdy[i_r]}");

                                        if (max_S_r[i_r] < ab_r[i_r])
                                        {
                                            max_S_r[i_r] = ab_r[i_r];
                                        }

                                        if (max_S_r[i_r] < bc_r[i_r])
                                        {
                                            max_S_r[i_r] = bc_r[i_r];
                                        }

                                        if (ab_r[i_r] == bc_r[i_r])
                                            MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                        S_rectangle = (Rectangle.S(preSr1, preSr2));
                                        textBox44.Text = ($"{(S_rectangle)}");

                                        i_r++;
                                    }
                                    else
                                    {
                                        check_r = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели координаты, при которых площаадь прямоугольника равна нулю");
                                    }
                                }
                                else if (vac == 0)
                                {
                                    rdx[i_r] = rax[i_r] + rcx[i_r] - rbx[i_r];
                                    rdy[i_r] = ray[i_r] + rcy[i_r] - rby[i_r];

                                    //произошло нахождение сторон
                                    preSr1 = (float)Math.Pow((rax[i_r] - rbx[i_r]), 2) + (float)Math.Pow((rby[i_r] - ray[i_r]), 2);
                                    preSr2 = (float)Math.Pow((rbx[i_r] - rcx[i_r]), 2) + (float)Math.Pow((rby[i_r] - rcy[i_r]), 2);
                                    ab_r[i_r] = (float)Math.Pow(preSr1, 0.5);
                                    bc_r[i_r] = (float)Math.Pow(preSr2, 0.5);

                                    if ((ab_r[i_r] != 0) & (bc_r[i_r] != 0))
                                    {
                                        textBox58.Text = ($"{rdx[i_r]}");
                                        textBox59.Text = ($"{rdy[i_r]}");

                                        if (max_S_r[i_r] < ab_r[i_r])
                                        {
                                            max_S_r[i_r] = ab_r[i_r];
                                        }

                                        if (max_S_r[i_r] < bc_r[i_r])
                                        {
                                            max_S_r[i_r] = bc_r[i_r];
                                        }

                                        if (ab_r[i_r] == bc_r[i_r])
                                            MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                        S_rectangle = Rectangle.S(preSr1, preSr2);
                                        textBox44.Text = ($"{(S_rectangle)}");

                                        i_r++;
                                    }
                                    else
                                    {
                                        check_r = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели координаты, при которых площаадь прямоугольника равна нулю");
                                    }
                                }
                                else if (vbc == 0)
                                {
                                    rdx[i_r] = rax[i_r] + rbx[i_r] - rcx[i_r];
                                    rdy[i_r] = ray[i_r] + rby[i_r] - rcy[i_r];

                                    preSr1 = (float)Math.Pow((rax[i_r] - rbx[i_r]), 2) + (float)Math.Pow((rby[i_r] - ray[i_r]), 2);
                                    preSr2 = (float)Math.Pow((rbx[i_r] - rcx[i_r]), 2) + (float)Math.Pow((rby[i_r] - rcy[i_r]), 2);
                                    ab_r[i_r] = (float)Math.Pow(preSr1, 0.5);
                                    bc_r[i_r] = (float)Math.Pow(preSr2, 0.5);
                                    if ((ab_r[i_r] != 0) & (bc_r[i_r] != 0))
                                    {
                                        textBox58.Text = ($"{rdx[i_r]}");
                                        textBox59.Text = ($"{rdy[i_r]}");

                                        if (max_S_r[i_r] < ab_r[i_r])
                                        {
                                            max_S_r[i_r] = ab_r[i_r];
                                        }

                                        if (max_S_r[i_r] < bc_r[i_r])
                                        {
                                            max_S_r[i_r] = bc_r[i_r];
                                        }

                                        if (ab_r[i_r] == bc_r[i_r])
                                            MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                        S_rectangle = Rectangle.S(preSr1, preSr2);
                                        textBox44.Text = ($"{(S_rectangle)}");
                                        S_ALL += Figure.S(0, 0, S_rectangle, 0, 0, 0, 0);
                                        i_r++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется прямоугольник");
                                        check_r = false;
                                        if (S_rectangle != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, S_rectangle, 0, 0, 0, 0);
                                        }
                                    }

                                }

                                else
                                {
                                    MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется прямоугольник");
                                    check_r = false;
                                    replace = true;

                                    if (S_rectangle != 0)
                                    {
                                        S_ALL -= Figure.S(0, 0, S_rectangle, 0, 0, 0, 0);
                                    }
                                }
                            }
                            else
                            {
                                
                                replace = true;
                                MessageBox.Show("Вы использовали максимальное количество прямоугольников");
                                check_r = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                            check_r = false;
                            replace = true;
                        }
                    }
                    else
                        MessageBox.Show("Вы не поместили фигуру");

                }
            }
        }

        //нахождение площади и 4 вершины параллелограмма
        public void parallelogram()
        {
            float par_ab_cd_x = 0, par_ab_cd_y = 0, par_ad_bc_x = 0, par_ad_bc_y = 0, abx = 0, bcx = 0, cdx = 0, dax = 0, aby = 0, bcy = 0, cdy = 0, day = 0;
            var Parallelogram = new Parallelogram();
            var Figure = new Figure();
            try
            {
                //конвертиуем значения координат

                pax[i_p] = Convert.ToSingle(textBox19.Text);
                pay[i_p] = Convert.ToSingle(textBox20.Text);
                pbx[i_p] = Convert.ToSingle(textBox21.Text);
                pby[i_p] = Convert.ToSingle(textBox22.Text);
                pcx[i_p] = Convert.ToSingle(textBox23.Text);
                pcy[i_p] = Convert.ToSingle(textBox24.Text);

            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное знаение координаты");
            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if (i_p < 100)
                        {
                            if ((pax[i_p] >= -SQ / 2) & (pax[i_p] <= SQ / 2) & (pbx[i_p] >= -SQ / 2) & (pbx[i_p] <= SQ / 2) & (pcx[i_p] >= -SQ / 2) & (pcx[i_p] <= SQ / 2) &
                                (pay[i_p] >= -SQ / 2) & (pay[i_p] <= SQ / 2) & (pby[i_p] >= -SQ / 2) & (pby[i_p] <= SQ / 2) & (pcy[i_p] >= -SQ / 2) & (pcy[i_p] <= SQ / 2))
                            {

                                check_p = true;
                                replace = false;

                                pdx[i_p] = pax[i_p] + pcx[i_p] - pbx[i_p];
                                pdy[i_p] = pay[i_p] + pcy[i_p] - pby[i_p];
                                // нахождение координатов векторов

                                abx = pbx[i_p] - pax[i_p];
                                aby = pby[i_p] - pay[i_p];
                                bcx = pcx[i_p] - pbx[i_p];
                                bcy = pcy[i_p] - pby[i_p];
                                cdx = pdx[i_p] - pcx[i_p];
                                cdy = pdy[i_p] - pcy[i_p];
                                dax = pax[i_p] - pdx[i_p];
                                day = pay[i_p] - pdy[i_p];

                                //проверка параллельности сторон

                                if ((abx == 0) & (cdx == 0))
                                {
                                    abx = aby;
                                    cdx = cdy;
                                }

                                if ((aby == 0) & (cdy == 0))
                                {
                                    aby = abx;
                                    cdx = cdy;
                                }

                                if ((bcx == 0) & (dax == 0))
                                {
                                    bcx = bcy;
                                    dax = day;
                                }

                                if ((bcy == 0) & (day == 0))
                                {
                                    bcy = bcx;
                                    day = dax;
                                }

                                par_ab_cd_x = abx / cdx;
                                par_ab_cd_y = aby / cdy;
                                par_ad_bc_x = bcx / dax;
                                par_ad_bc_y = bcy / day;

                                abx = pbx[i_p] - pax[i_p];
                                aby = pby[i_p] - pay[i_p];
                                bcx = pcx[i_p] - pbx[i_p];
                                bcy = pcy[i_p] - pby[i_p];
                                cdx = pdx[i_p] - pcx[i_p];
                                cdy = pdy[i_p] - pcy[i_p];
                                dax = pax[i_p] - pdx[i_p];
                                day = pay[i_p] - pdy[i_p];

                                //нахождение сторон

                                float predia1 = 0, predia2 = 0, preSp1 = 0, preSp2 = 0, preSp3 = 0, preSp4 = 0, ox, oy, bo = 0, co = 0;

                                if ((par_ab_cd_x == par_ab_cd_y) && (par_ad_bc_x == par_ad_bc_y))
                                {

                                    S_parallelogram = Parallelogram.S(pax[i_p], pbx[i_p], pcx[i_p], pay[i_p], pby[i_p], pcy[i_p]);
                                    ox = Math.Abs(pax[i_p] - pcx[i_p]) / 2;
                                    oy = Math.Abs(pdy[i_p] - pby[i_p]) / 2;

                                    preSp1 = (float)Math.Pow((abx), 2) + (float)Math.Pow((aby), 2);
                                    preSp2 = (float)Math.Pow((bcx), 2) + (float)Math.Pow((bcy), 2);
                                    preSp3 = (float)Math.Pow((cdx), 2) + (float)Math.Pow((cdy), 2);
                                    preSp4 = (float)Math.Pow((dax), 2) + (float)Math.Pow((day), 2);

                                    ab_p[i_p] = (float)Math.Pow(preSp1, 0.5);
                                    bc_p[i_p] = (float)Math.Pow(preSp2, 0.5);
                                    cd_p[i_p] = (float)Math.Pow(preSp3, 0.5);
                                    ad_p[i_p] = (float)Math.Pow(preSp4, 0.5);

                                    if (S_parallelogram != 0)
                                    {
                                        textBox60.Text = ($"{pdx[i_p]}");
                                        textBox61.Text = ($"{pdy[i_p]}");
                                        if (max_S_p[i_p] < ab_p[i_p])
                                        {
                                            max_S_p[i_p] = ab_p[i_p];

                                        }

                                        if (max_S_p[i_p] < bc_p[i_p])
                                        {
                                            max_S_p[i_p] = bc_p[i_p];

                                        }
                                        if (ab_p[i_p] > bc_p[i_p])
                                        {
                                            float h = S_parallelogram / bc_p[i_p];
                                            if (max_S_p[i_p] < h)
                                            {
                                                max_S_p[i_p] = h;

                                            }
                                        }
                                        if (ab_p[i_p] < bc_p[i_p])
                                        {
                                            float h = S_parallelogram / ab_p[i_p];
                                            if (max_S_p[i_p] < h)
                                            {
                                                max_S_p[i_p] = h;

                                            }
                                        }

                                        if (max_S_p[i_p] < (Math.Abs((ox * 2))))
                                        {
                                            max_S_p[i_p] = Math.Abs((ox * 2));
                                        }
                                        if (max_S_p[i_p] < (Math.Abs((oy * 2))))
                                        {
                                            max_S_p[i_p] = Math.Abs((oy * 2));
                                        }


                                        predia1 = (float)Math.Pow((pbx[i_p] - ox), 2) + (float)Math.Pow((pby[i_p] - oy), 2);
                                        predia2 = (float)Math.Pow((pcx[i_p] - ox), 2) + (float)Math.Pow((pcy[i_p] - oy), 2);
                                        bo = (float)Math.Pow(predia1, 0.5);
                                        co = (float)Math.Pow(predia2, 0.5);

                                        if (ab_p[i_p] == bc_p[i_p])
                                            MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                        if (bo == co)
                                            MessageBox.Show("Вы ввели координаты точек, при которых образуется прямоугольник");
                                        if (S_parallelogram < 0)
                                        {
                                            S_parallelogram = -S_parallelogram;

                                        }

                                        textBox53.Text = ($"{(S_parallelogram)}");

                                        i_p++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм ");
                                        check_p = false;
                                        replace = true;

                                        if (S_parallelogram != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                        }
                                    }
                                }
                                else if (abx == 0 & cdx == 0)
                                {
                                    if (par_ab_cd_y != 0)
                                    {
                                        S_parallelogram = (pbx[i_p] - pax[i_p]) * (pcy[i_p] - pay[i_p]) - (pcx[i_p] - pax[i_p]) * (pby[i_p] - pay[i_p]);

                                        ox = (pax[i_p] + pcx[i_p]) / 2;
                                        oy = (pdy[i_p] + pby[i_p]) / 2;

                                        preSp1 = (float)Math.Pow((abx), 2) + (float)Math.Pow((aby), 2);
                                        preSp2 = (float)Math.Pow((bcx), 2) + (float)Math.Pow((bcy), 2);
                                        preSp3 = (float)Math.Pow((cdx), 2) + (float)Math.Pow((cdy), 2);
                                        preSp4 = (float)Math.Pow((dax), 2) + (float)Math.Pow((day), 2);

                                        ab_p[i_p] = (float)Math.Pow(preSp1, 0.5);
                                        bc_p[i_p] = (float)Math.Pow(preSp2, 0.5);
                                        cd_p[i_p] = (float)Math.Pow(preSp3, 0.5);
                                        ad_p[i_p] = (float)Math.Pow(preSp4, 0.5);

                                        if (S_parallelogram != 0)
                                        {
                                            textBox60.Text = ($"{pdx[i_p]}");
                                            textBox61.Text = ($"{pdy[i_p]}");
                                            if (max_S_p[i_p] < ab_p[i_p])
                                            {
                                                max_S_p[i_p] = ab_p[i_p];

                                            }

                                            if (max_S_p[i_p] < bc_p[i_p])
                                            {
                                                max_S_p[i_p] = bc_p[i_p];

                                            }
                                            if (ab_p[i_p] > bc_p[i_p])
                                            {
                                                float h = S_parallelogram / bc_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (ab_p[i_p] < bc_p[i_p])
                                            {
                                                float h = S_parallelogram / ab_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((ox * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((ox * 2));
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((oy * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((oy * 2));
                                            }

                                            predia1 = (float)Math.Pow((pbx[i_p] - ox), 2) + (float)Math.Pow((pby[i_p] - oy), 2);
                                            predia2 = (float)Math.Pow((pcx[i_p] - ox), 2) + (float)Math.Pow((pcy[i_p] - oy), 2);
                                            bo = (float)Math.Pow(predia1, 0.5);
                                            co = (float)Math.Pow(predia2, 0.5);

                                            if (ab_p[i_p] == bc_p[i_p])
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                            else if (bo == co)
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется прямоугольник");

                                            if (S_parallelogram < 0)
                                            {

                                                S_parallelogram = -S_parallelogram;
                                            }
                                            textBox53.Text = ($"{(S_parallelogram)}");
                                            i_p++;

                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм");

                                            check_p = false;
                                            replace = true;
                                            if (S_parallelogram != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                            }
                                        }
                                    }

                                }
                                else if (aby == 0 & cdy == 0)
                                {
                                    if (par_ab_cd_x != 0)
                                    {

                                        S_parallelogram = (pbx[i_p] - pax[i_p]) * (pcy[i_p] - pay[i_p]) - (pcx[i_p] - pax[i_p]) * (pby[i_p] - pay[i_p]);
                                        ox = (pax[i_p] + pcx[i_p]) / 2;
                                        oy = (pdy[i_p] + pby[i_p]) / 2;

                                        preSp1 = (float)Math.Pow((abx), 2) + (float)Math.Pow((aby), 2);
                                        preSp2 = (float)Math.Pow((bcx), 2) + (float)Math.Pow((bcy), 2);
                                        preSp3 = (float)Math.Pow((cdx), 2) + (float)Math.Pow((cdy), 2);
                                        preSp4 = (float)Math.Pow((dax), 2) + (float)Math.Pow((day), 2);

                                        ab_p[i_p] = (float)Math.Pow(preSp1, 0.5);
                                        bc_p[i_p] = (float)Math.Pow(preSp2, 0.5);
                                        cd_p[i_p] = (float)Math.Pow(preSp3, 0.5);
                                        ad_p[i_p] = (float)Math.Pow(preSp4, 0.5);

                                        if ((S_parallelogram != 0) & (ab_p[i_p] != 0) & (bc_p[i_p] != 0) & (cd_p[i_p] != 0) & (ad_p[i_p] != 0))
                                        {
                                            textBox60.Text = ($"{pdx[i_p]}");
                                            textBox61.Text = ($"{pdy[i_p]}");
                                            if (max_S_p[i_p] < ab_p[i_p])
                                            {
                                                max_S_p[i_p] = ab_p[i_p];

                                            }

                                            if (max_S_p[i_p] < bc_p[i_p])
                                            {
                                                max_S_p[i_p] = bc_p[i_p];

                                            }
                                            if (ab_p[i_p] > bc_p[i_p])
                                            {
                                                float h = S_parallelogram / bc_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (ab_p[i_p] < bc_p[i_p])
                                            {
                                                float h = S_parallelogram / ab_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }

                                            if (max_S_p[i_p] < (Math.Abs((ox * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((ox * 2));
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((oy * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((oy * 2));
                                            }

                                            predia1 = (float)Math.Pow((pbx[i_p] - ox), 2) + (float)Math.Pow((pby[i_p] - oy), 2);
                                            predia2 = (float)Math.Pow((pcx[i_p] - ox), 2) + (float)Math.Pow((pcy[i_p] - oy), 2);
                                            bo = (float)Math.Pow(predia1, 0.5);
                                            co = (float)Math.Pow(predia2, 0.5);

                                            if (ab_p[i_p] == bc_p[i_p])
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                            else if (bo == co)
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется прямоугольник");

                                            if (S_parallelogram < 0)
                                            {

                                                S_parallelogram = -S_parallelogram;
                                            }
                                            textBox53.Text = ($"{(S_parallelogram)}");
                                            i_p++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм");
                                            check_p = false;
                                            replace = true;

                                            if (S_parallelogram != 0)
                                            {

                                                S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                            }
                                        }
                                    }

                                }
                                else if (bcx == 0 & dax == 0)
                                {
                                    if (par_ad_bc_y != 0)
                                    {

                                        S_parallelogram = (pbx[i_p] - pax[i_p]) * (pcy[i_p] - pay[i_p]) - (pcx[i_p] - pax[i_p]) * (pby[i_p] - pay[i_p]);
                                        ox = (pax[i_p] + pcx[i_p]) / 2;
                                        oy = (pdy[i_p] + pby[i_p]) / 2;

                                        preSp1 = (float)Math.Pow((abx), 2) + (float)Math.Pow((aby), 2);
                                        preSp2 = (float)Math.Pow((bcx), 2) + (float)Math.Pow((bcy), 2);
                                        preSp3 = (float)Math.Pow((cdx), 2) + (float)Math.Pow((cdy), 2);
                                        preSp4 = (float)Math.Pow((dax), 2) + (float)Math.Pow((day), 2);

                                        ab_p[i_p] = (float)Math.Pow(preSp1, 0.5);
                                        bc_p[i_p] = (float)Math.Pow(preSp2, 0.5);
                                        cd_p[i_p] = (float)Math.Pow(preSp3, 0.5);
                                        ad_p[i_p] = (float)Math.Pow(preSp4, 0.5);

                                        if ((S_parallelogram != 0) & (ab_p[i_p] != 0) & (bc_p[i_p] != 0) & (cd_p[i_p] != 0) & (ad_p[i_p] != 0))
                                        {
                                            textBox60.Text = ($"{pdx[i_p]}");
                                            textBox61.Text = ($"{pdy[i_p]}");

                                            predia1 = (float)Math.Pow((pbx[i_p] - ox), 2) + (float)Math.Pow((pby[i_p] - oy), 2);
                                            predia2 = (float)Math.Pow((pcx[i_p] - ox), 2) + (float)Math.Pow((pcy[i_p] - oy), 2);
                                            bo = (float)Math.Pow(predia1, 0.5);
                                            co = (float)Math.Pow(predia2, 0.5);

                                            if (ab_p[i_p] == bc_p[i_p])
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                            else if (bo == co)
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется прямоугольник");

                                            if (S_parallelogram < 0)
                                            {
                                                S_parallelogram = -S_parallelogram;
                                            }
                                            textBox53.Text = ($"{(S_parallelogram)}");


                                            if (max_S_p[i_p] < ab_p[i_p])
                                            {
                                                max_S_p[i_p] = ab_p[i_p];

                                            }

                                            if (max_S_p[i_p] < bc_p[i_p])
                                            {
                                                max_S_p[i_p] = bc_p[i_p];

                                            }
                                            if (ab_p[i_p] > bc_p[i_p])
                                            {
                                                float h = S_parallelogram / bc_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (ab_p[i_p] < bc_p[i_p])
                                            {
                                                float h = S_parallelogram / ab_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((ox * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((ox * 2));
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((oy * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((oy * 2));
                                            }

                                            i_p++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм 5");
                                            check_p = false;
                                            replace = true;

                                            if (S_parallelogram != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                            }
                                        }
                                    }

                                }
                                else if (bcx == 0 & dax == 0)
                                {
                                    if (par_ad_bc_x != 0)
                                    {
                                        S_parallelogram = (pbx[i_p] - pax[i_p]) * (pcy[i_p] - pay[i_p]) - (pcx[i_p] - pax[i_p]) * (pby[i_p] - pay[i_p]);
                                        ox = (pax[i_p] + pcx[i_p]) / 2;
                                        oy = (pdy[i_p] + pby[i_p]) / 2;

                                        preSp1 = (float)Math.Pow((abx), 2) + (float)Math.Pow((aby), 2);
                                        preSp2 = (float)Math.Pow((bcx), 2) + (float)Math.Pow((bcy), 2);
                                        preSp3 = (float)Math.Pow((cdx), 2) + (float)Math.Pow((cdy), 2);
                                        preSp4 = (float)Math.Pow((dax), 2) + (float)Math.Pow((day), 2);

                                        ab_p[i_p] = (float)Math.Pow(preSp1, 0.5);
                                        bc_p[i_p] = (float)Math.Pow(preSp2, 0.5);
                                        cd_p[i_p] = (float)Math.Pow(preSp3, 0.5);
                                        ad_p[i_p] = (float)Math.Pow(preSp4, 0.5);

                                        if ((S_parallelogram != 0) & (ab_p[i_p] != 0) & (bc_p[i_p] != 0) & (cd_p[i_p] != 0) & (ad_p[i_p] != 0))
                                        {
                                            textBox60.Text = ($"{pdx[i_p]}");
                                            textBox61.Text = ($"{pdy[i_p]}");

                                            if (max_S_p[i_p] < ab_p[i_p])
                                            {
                                                max_S_p[i_p] = ab_p[i_p];

                                            }

                                            if (max_S_p[i_p] < bc_p[i_p])
                                            {
                                                max_S_p[i_p] = bc_p[i_p];

                                            }
                                            if (ab_p[i_p] > bc_p[i_p])
                                            {
                                                float h = S_parallelogram / bc_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (ab_p[i_p] < bc_p[i_p])
                                            {
                                                float h = S_parallelogram / ab_p[i_p];
                                                if (max_S_p[i_p] < h)
                                                {
                                                    max_S_p[i_p] = h;

                                                }
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((ox * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((ox * 2));
                                            }
                                            if (max_S_p[i_p] < (Math.Abs((oy * 2))))
                                            {
                                                max_S_p[i_p] = Math.Abs((oy * 2));
                                            }

                                            predia1 = (float)Math.Pow((pbx[i_p] - ox), 2) + (float)Math.Pow((pby[i_p] - oy), 2);
                                            predia2 = (float)Math.Pow((pcx[i_p] - ox), 2) + (float)Math.Pow((pcy[i_p] - oy), 2);
                                            bo = (float)Math.Pow(predia1, 0.5);
                                            co = (float)Math.Pow(predia2, 0.5);

                                            if (ab_p[i_p] == bc_p[i_p])
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется квадрат");
                                            else if (bo == co)
                                                MessageBox.Show("Вы ввели координаты точек, при которых образуется прямоугольник");
                                            if (S_parallelogram < 0)
                                            {
                                                S_parallelogram = -S_parallelogram;
                                            }
                                            textBox53.Text = ($"{(S_parallelogram)}");
                                            i_p++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм");
                                            check_p = false;
                                            replace = true;

                                            if (S_parallelogram != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                            }
                                        }
                                    }

                                }

                                else
                                {
                                    MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется параллелограмм");
                                    check_p = false;
                                    replace = true;

                                    if (S_parallelogram != 0)
                                    {
                                        S_ALL -= Figure.S(0, 0, 0, S_parallelogram, 0, 0, 0); ;
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");

                                replace = true;
                                check_p = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы использовали максимальное количество параллелограммов");
                            check_p = false;
                            replace = true;
                        }
                
                    }
                    else
                    {
                        MessageBox.Show("Вы не поместили фигуру");
                    }
                }

            }
        }

        public void rhomb()
        {
            float ox, oy, predia1, predia2, ao, bo;

            var Rhomb = new Rhomb();
            var Figure = new Figure();

            try
            {
                //конвертиуем значения координат

                rhax[i_rh] = Convert.ToSingle(textBox25.Text);
                rhay[i_rh] = Convert.ToSingle(textBox26.Text);
                rhbx[i_rh] = Convert.ToSingle(textBox27.Text);
                rhby[i_rh] = Convert.ToSingle(textBox28.Text);
                rhcx[i_rh] = Convert.ToSingle(textBox29.Text);
                rhcy[i_rh] = Convert.ToSingle(textBox30.Text);

            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное значение координаты");
            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if (i_rh < 100)
                        {

                            if ((rhax[i_rh] >= -SQ / 2) & (rhax[i_rh] <= SQ / 2) & (rhbx[i_rh] >= -SQ / 2) & (rhbx[i_rh] <= SQ / 2) & (rhcx[i_rh] >= -SQ / 2) & (rhcx[i_rh] <= SQ / 2) &
                                    (rhay[i_rh] >= -SQ / 2) & (rhay[i_rh] <= SQ / 2) & (rhby[i_rh] >= -SQ / 2) & (rhby[i_rh] <= SQ / 2) & (rhcy[i_rh] >= -SQ / 2) & (rhcy[i_rh] <= SQ / 2))
                            {
                                check_rh = true;
                                replace = false;

                                ox = (rhax[i_rh] + rhcx[i_rh]) / 2;
                                oy = (rhay[i_rh] + rhcy[i_rh]) / 2;
                                predia1 = (float)Math.Pow((rhax[i_rh] - ox), 2) + (float)Math.Pow((rhay[i_rh] - oy), 2);
                                predia2 = (float)Math.Pow((rhbx[i_rh] - ox), 2) + (float)Math.Pow((rhby[i_rh] - oy), 2);
                                ao = (float)Math.Pow(predia1, 0.5);
                                bo = (float)Math.Pow(predia2, 0.5);
                                rhdx[i_rh] = ox * 2 - rhbx[i_rh];
                                rhdy[i_rh] = oy * 2 - rhby[i_rh];

                                ab_rh[i_rh] = (float)Math.Pow((rhbx[i_rh] - rhax[i_rh]), 2) + (float)Math.Pow((rhby[i_rh] - rhay[i_rh]), 2);
                                bc_rh[i_rh] = (float)Math.Pow((rhcx[i_rh] - rhbx[i_rh]), 2) + (float)Math.Pow((rhcy[i_rh] - rhby[i_rh]), 2);
                                ab_rh[i_rh] = (float)Math.Pow(ab_rh[i_rh], 0.5);
                                bc_rh[i_rh] = (float)Math.Pow(bc_rh[i_rh], 0.5);

                                if ((rhax[i_rh] - ox) * (rhbx[i_rh] - ox) + (rhay[i_rh] - oy) * (rhby[i_rh] - oy) == 0)
                                {
                                    S_rhomb = Rhomb.S(ao, bo);
                                    if ((S_rhomb != 0) & (ab_rh[i_rh] != 0) & (bc_rh[i_rh] != 0))
                                    {
                                        
                                        textBox62.Text = ($"{rhdx[i_rh]}");
                                        textBox63.Text = ($"{rhdy[i_rh]}");


                                        if (S_rhomb < 0)
                                        {
                                            S_rhomb = -S_rhomb;
                                        }

                                        if (max_S_rh[i_rh] < ao * 2)
                                        {
                                            float ao1 = (float)Math.Pow(ao * 2, 2);
                                            ao1 = ao1 / 2;
                                            ao1 = (float)Math.Pow(ao1, 0.5);

                                            max_S_rh[i_rh] = ao1;
                                        }

                                        if (max_S_rh[i_rh] < bo * 2)
                                        {
                                            float bo1 = (float)Math.Pow(bo * 2, 2);
                                            bo1 = bo1 / 2;
                                            bo1 = (float)Math.Pow(bo1, 0.5);
                                            max_S_rh[i_rh] = bo * 2;
                                        }
                                        if (ao == bo)
                                        {
                                            if (max_S_rh[i_rh] < ab_rh[i_rh])
                                                max_S_rh[i_rh] = ab_rh[i_rh];
                                            MessageBox.Show("Вы образовали квадрат");
                                        }
                                        textBox46.Text = ($"{(S_rhomb)}");

                                        i_rh++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется ромб");
                                        check_rh = false;
                                        replace = true;
                    
                                        if (S_parallelogram != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, S_rhomb, 0, 0);
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется ромб");
                                    check_rh = false;
                                    replace = true;

                                    if (S_rhomb != 0)
                                    {
                                        S_ALL -= Figure.S(0, 0, 0, 0, S_rhomb, 0, 0);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                                check_rh = false;
                                replace = true;
                            }
                        }

                        else
                        {
                            MessageBox.Show("Вы использовали максимальное количество прямоугольников");
                            check_rh = false;
                            replace = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не поместили фигуру");
                    }
                }
            }
        }

        public void trapezoid()
        {
            var Trapezoid = new Trapezoid();
            var Figure = new Figure();
            float par_ad_bc_x, par_ad_bc_y, par_ab_cd_x, par_ab_cd_y, preS, preS1;
            try
            {
                //конвертиуем значения координат

                tzax[i_tz] = Convert.ToSingle(textBox31.Text);
                tzay[i_tz] = Convert.ToSingle(textBox32.Text);
                tzbx[i_tz] = Convert.ToSingle(textBox33.Text);
                tzby[i_tz] = Convert.ToSingle(textBox34.Text);
                tzcx[i_tz] = Convert.ToSingle(textBox35.Text);
                tzcy[i_tz] = Convert.ToSingle(textBox36.Text);
                tzdx[i_tz] = Convert.ToSingle(textBox37.Text);
                tzdy[i_tz] = Convert.ToSingle(textBox38.Text);

            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное знаение координаты");
            }

            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if (i_tz < 100)
                        {
                            if ((tzax[i_tz] >= -SQ / 2) & (tzax[i_tz] <= SQ / 2) & (tzbx[i_tz] >= -SQ / 2) & (tzbx[i_tz] <= SQ / 2) & (tzcx[i_tz] >= -SQ / 2) & (tzcx[i_tz] <= SQ / 2) &
                                        (tzay[i_tz] >= -SQ / 2) & (tzay[i_tz] <= SQ / 2) & (tzby[i_tz] >= -SQ / 2) & (tzby[i_tz] <= SQ / 2) & (tzcy[i_tz] >= -SQ / 2) & (tzcy[i_tz] <= SQ / 2))
                            {
                                ab_tz[i_tz] = (float)Math.Pow((tzbx[i_tz] - tzax[i_tz]), 2) + (float)Math.Pow((tzby[i_tz] - tzay[i_tz]), 2);
                                bc_tz[i_tz] = (float)Math.Pow((tzcx[i_tz] - tzbx[i_tz]), 2) + (float)Math.Pow((tzcy[i_tz] - tzby[i_tz]), 2);
                                cd_tz[i_tz] = (float)Math.Pow((tzdx[i_tz] - tzcx[i_tz]), 2) + (float)Math.Pow((tzdy[i_tz] - tzcy[i_tz]), 2);
                                da_tz[i_tz] = (float)Math.Pow((tzax[i_tz] - tzdx[i_tz]), 2) + (float)Math.Pow((tzay[i_tz] - tzdy[i_tz]), 2);

                                ab_tz[i_tz] = (float)Math.Pow((ab_tz[i_tz]), 0.5);
                                bc_tz[i_tz] = (float)Math.Pow((bc_tz[i_tz]), 0.5);
                                cd_tz[i_tz] = (float)Math.Pow((cd_tz[i_tz]), 0.5);
                                da_tz[i_tz] = (float)Math.Pow((da_tz[i_tz]), 0.5);

                                par_ad_bc_x = (tzcx[i_tz] - tzbx[i_tz]) / (tzax[i_tz] - tzdx[i_tz]);
                                par_ad_bc_y = (tzcy[i_tz] - tzby[i_tz]) / (tzay[i_tz] - tzdy[i_tz]);

                                par_ab_cd_x = (tzbx[i_tz] - tzax[i_tz]) / (tzdx[i_tz] - tzcx[i_tz]);
                                par_ab_cd_y = (tzby[i_tz] - tzay[i_tz]) / (tzdy[i_tz] - tzcy[i_tz]);

                                if (par_ad_bc_x == par_ad_bc_y)
                                {

                                    if (da_tz[i_tz] != bc_tz[i_tz])
                                    {
                                        if (ab_tz[i_tz] > cd_tz[i_tz])
                                        {
                                            preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((cd_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                            preS1 = (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                            S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);

                                        }
                                        else
                                        {
                                            preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((ab_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                            preS1 = (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                            S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);

                                        }

                                        if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                        {
                                            if (S_trapezoid < 0)
                                            {
                                                S_trapezoid = -S_trapezoid;
                                            }

                                            if (max_S_tz[i_tz] < bc_tz[i_tz])
                                            {
                                                max_S_tz[i_tz] = bc_tz[i_tz];
                                            }

                                            if (max_S_tz[i_tz] < da_tz[i_tz])
                                            {
                                                max_S_tz[i_tz] = da_tz[i_tz];
                                            }
                                            if (max_S_tz[i_tz] < preS1)
                                            {
                                                max_S_tz[i_tz] = preS1;
                                            }


                                            textBox47.Text = ($"{(S_trapezoid)}");
                                            i_tz++;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            check_tz = false;
                                            replace = true;
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        check_tz = false;
                                        replace = true;
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }

                                else if ((tzcx[i_tz] - tzbx[i_tz] == 0) & (tzax[i_tz] - tzdx[i_tz] == 0))
                                {
                                    if (par_ad_bc_y != 0)
                                    {
                                        if (da_tz[i_tz] != bc_tz[i_tz])
                                        {
                                            if (ab_tz[i_tz] > cd_tz[i_tz])
                                            {
                                                preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((cd_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                                preS1 = (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);

                                            }
                                            else
                                            {
                                                preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((ab_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                                preS1 = (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);
                                            }

                                            if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                            {
                                                if (S_trapezoid < 0)
                                                {
                                                    S_trapezoid = -S_trapezoid;
                                                }

                                                if (max_S_tz[i_tz] < bc_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = bc_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < da_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = da_tz[i_tz];
                                                }
                                                if (max_S_tz[i_tz] < (float)Math.Pow(preS1, 0.5))
                                                {
                                                    max_S_tz[i_tz] = (float)Math.Pow(preS1, 0.5);
                                                }
                                                textBox47.Text = ($"{(S_trapezoid)}");
                                                i_tz++;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                                check_tz = false;
                                                replace = true;
                                                if (S_trapezoid != 0)
                                                {
                                                    S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            check_tz = false;
                                            replace = true;
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else if ((tzbx[i_tz] - tzax[i_tz] == 0) & (tzdx[i_tz] - tzcx[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }

                                    else if ((tzby[i_tz] - tzay[i_tz] == 0) & (tzdy[i_tz] - tzcy[i_tz] == 0))
                                    {
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        check_tz = false;
                                        replace = true;
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                    else
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }

                                else if ((tzcy[i_tz] - tzby[i_tz] == 0) & (tzay[i_tz] - tzdy[i_tz] == 0))
                                {
                                    if (par_ad_bc_x != 0)
                                    {
                                        if (da_tz[i_tz] != bc_tz[i_tz])
                                        {
                                            if (ab_tz[i_tz] > cd_tz[i_tz])
                                            {
                                                preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((cd_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                                preS1 = (float)Math.Pow((ab_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);

                                            }
                                            else
                                            {
                                                preS = ((float)Math.Pow((da_tz[i_tz] - bc_tz[i_tz]), 2) + (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((ab_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(da_tz[i_tz] - bc_tz[i_tz]))); ;
                                                preS1 = (float)Math.Pow((cd_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(da_tz[i_tz], bc_tz[i_tz], preS1);

                                            }

                                            if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                            {

                                                if (S_trapezoid < 0)
                                                {
                                                    S_trapezoid = -S_trapezoid;
                                                }

                                                if (max_S_tz[i_tz] < bc_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = bc_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < da_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = da_tz[i_tz];
                                                }
                                                if (max_S_tz[i_tz] < (float)Math.Pow(preS1, 0.5))
                                                {
                                                    max_S_tz[i_tz] = (float)Math.Pow(preS1, 0.5);
                                                }

                                                textBox47.Text = ($"{(S_trapezoid)}");
                                                i_tz++;
                                            }
                                            else
                                            {
                                                check_tz = false;
                                                replace = true;
                                                MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                                if (S_trapezoid != 0)
                                                {
                                                    S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            check_tz = false;
                                            replace = true;
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else if ((tzbx[i_tz] - tzax[i_tz] == 0) & (tzdx[i_tz] - tzcx[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }

                                    else if ((tzby[i_tz] - tzay[i_tz] == 0) & (tzdy[i_tz] - tzcy[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                    else
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }

                                else if (par_ab_cd_x == par_ab_cd_y)
                                {
                                    float h_tz = 0;
                                    h_tz = Math.Abs(tzay[i_tz] - tzby[i_tz]);

                                    if (ab_tz[i_tz] != cd_tz[i_tz])
                                    {

                                        if (da_tz[i_tz] > bc_tz[i_tz])
                                        {

                                            preS = ((float)Math.Pow((ab_tz[i_tz] - cd_tz[i_tz]), 2) + (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((bc_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                            preS1 = (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                            S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);
                                        }
                                        else
                                        {
                                            preS = ((float)Math.Pow((ab_tz[i_tz] - cd_tz[i_tz]), 2) + (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((da_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                            preS1 = (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                            S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);
                                        }

                                        if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                        {

                                            if (S_trapezoid < 0)
                                            {
                                                S_trapezoid = -S_trapezoid;
                                            }

                                            if (max_S_tz[i_tz] < ab_tz[i_tz])
                                            {
                                                max_S_tz[i_tz] = ab_tz[i_tz];
                                            }

                                            if (max_S_tz[i_tz] < cd_tz[i_tz])
                                            {
                                                max_S_tz[i_tz] = cd_tz[i_tz];
                                            }

                                            if (max_S_tz[i_tz] < (float)Math.Pow(preS1, 0.5))
                                            {
                                                max_S_tz[i_tz] = (float)Math.Pow(preS1, 0.5);
                                            }

                                            textBox47.Text = ($"{(S_trapezoid)}");
                                            i_tz++;
                                        }
                                        else
                                        {
                                            check_tz = false;
                                            replace = true;
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }
                                else if ((tzbx[i_tz] - tzax[i_tz] == 0) && (tzdx[i_tz] - tzcx[i_tz] == 0))
                                {
                                    if (par_ab_cd_y != 0)
                                    {
                                        if (ab_tz[i_tz] != cd_tz[i_tz])
                                        {
                                            if (da_tz[i_tz] > bc_tz[i_tz])
                                            {

                                                preS = ((float)Math.Pow((ab_tz[i_tz] - cd_tz[i_tz]), 2) + (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((bc_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                                preS1 = (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);

                                            }
                                            else
                                            {
                                                preS = ((float)Math.Pow((ab_tz[i_tz] - cd_tz[i_tz]), 2) + (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((da_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                                preS1 = (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);

                                            }
                                            if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                            {

                                                if (S_trapezoid < 0)
                                                {
                                                    S_trapezoid = -S_trapezoid;
                                                }

                                                if (max_S_tz[i_tz] < ab_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = ab_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < cd_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = cd_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < (float)Math.Pow(preS1, 0.5))
                                                {
                                                    max_S_tz[i_tz] = (float)Math.Pow(preS1, 0.5);
                                                }

                                                textBox47.Text = ($"{(S_trapezoid)}");
                                                i_tz++;
                                            }
                                            else
                                            {
                                                check_tz = false;
                                                replace = true;
                                                MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                                if (S_trapezoid != 0)
                                                {
                                                    S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            check_tz = false;
                                            replace = true;
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else if ((tzcx[i_tz] - tzbx[i_tz] == 0) & (tzax[i_tz] - tzdx[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }

                                    else if ((tzcy[i_tz] - tzby[i_tz] == 0) & (tzay[i_tz] - tzdy[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                    else
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }
                                else if ((tzby[i_tz] - tzay[i_tz] == 0) && (tzdy[i_tz] - tzcy[i_tz] == 0))
                                {
                                    if (par_ab_cd_x != 0)
                                    {
                                        if (ab_tz[i_tz] != cd_tz[i_tz])
                                        {
                                            if (da_tz[i_tz] > bc_tz[i_tz])
                                            {

                                                preS = ((float)Math.Pow(Math.Abs(cd_tz[i_tz] - ab_tz[i_tz]), 2) + (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((bc_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                                preS1 = (float)Math.Pow((da_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);

                                            }
                                            else
                                            {
                                                preS = ((float)Math.Pow(Math.Abs(ab_tz[i_tz] - cd_tz[i_tz]), 2) + (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((da_tz[i_tz]), 2)) / (2 * ((float)Math.Abs(ab_tz[i_tz] - cd_tz[i_tz])));
                                                preS1 = (float)Math.Pow((bc_tz[i_tz]), 2) - (float)Math.Pow((preS), 2);
                                                S_trapezoid = Trapezoid.S(ab_tz[i_tz], cd_tz[i_tz], preS1);

                                            }

                                            if ((S_trapezoid != 0) & (ab_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (bc_tz[i_tz] != 0) & (cd_tz[i_tz] != 0) & (da_tz[i_tz] != 0))
                                            {

                                                if (S_trapezoid < 0)
                                                {
                                                    S_trapezoid = -S_trapezoid;
                                                }

                                                if (max_S_tz[i_tz] < ab_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = ab_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < cd_tz[i_tz])
                                                {
                                                    max_S_tz[i_tz] = cd_tz[i_tz];
                                                }

                                                if (max_S_tz[i_tz] < (float)Math.Pow(preS1, 0.5))
                                                {
                                                    max_S_tz[i_tz] = (float)Math.Pow(preS1, 0.5);
                                                }

                                                textBox47.Text = ($"{(S_trapezoid)}");
                                                i_tz++;
                                            }
                                            else
                                            {
                                                check_tz = false;
                                                replace = true;
                                                MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                                if (S_trapezoid != 0)
                                                {
                                                    S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            check_tz = false;
                                            replace = true;
                                            MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                            if (S_trapezoid != 0)
                                            {
                                                S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                            }
                                        }
                                    }
                                    else if ((tzcx[i_tz] - tzbx[i_tz] == 0) & (tzax[i_tz] - tzdx[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }

                                    else if ((tzcy[i_tz] - tzby[i_tz] == 0) & (tzay[i_tz] - tzdy[i_tz] == 0))
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                    else
                                    {
                                        check_tz = false;
                                        replace = true;
                                        MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                        if (S_trapezoid != 0)
                                        {
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    check_tz = false;
                                    replace = true;
                                    MessageBox.Show("Вы ввели некорректные координаты, при которых не образуется трапеция");
                                    if (S_trapezoid != 0)
                                    {
                                        S_ALL -= Figure.S(0, 0, 0, 0, 0, S_trapezoid, 0);
                                    }
                                }
                            }
                            else
                            {
                                check_tz = false;
                                replace = true;
                                MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                            }
                        }

                        else
                        {
                            check_tz = false;
                            replace = true;
                            MessageBox.Show("Вы использовали максимальное количество прямоугольников");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не поместили фигуру");
                    }
                }
                
            }

        }

        public void circle()
        {
            var Figure = new Figure();
            var Circle = new Circle();
            Random rand = new Random();
            try
            {
                //конвертиуем значения координат

                cax[i_c] = rand.Next((int)-SQ / 2, (int)SQ / 2);
                cay[i_c] = rand.Next((int)-SQ / 2, (int)SQ / 2);
            }

            catch
            {
                MessageBox.Show("Вы ввели некорректное знаение координаты");
            }
            finally
            {
                if (S_S_create == false)
                    MessageBox.Show("Сначала укажите сторону квадрата, в который будете укладывать!");
                else
                {
                    if (replace == true)
                    {
                        if (i_c < numericUpDown2.Value)
                        {
                            if ((cax[i_c] >= -SQ / 2) & (cax[i_c] <= SQ / 2) &
                                        (cay[i_c] >= -SQ / 2) & (cay[i_c] <= SQ / 2))
                            {
                                
                                //float prerad = (float)Math.Pow((cbx[i_c] - cax[i_c]), 2) + (float)Math.Pow((cby[i_c] - cay[i_c]), 2);
                                rad[i_c] = rand.Next(0, (int)SQ / 2);
                                bool pr = false;
                                int k = 0;

                                if ((cax[i_c] + rad[i_c] <= SQ/2) & (cax[i_c] - rad[i_c] >= -SQ/2) & (cay[i_c] + rad[i_c] <= SQ/2) & (cay[i_c] - rad[i_c] >= -SQ/2))
                                {
                                    pr = true;
                                }
                                else
                                {

                                    for (k = 0; ((cax[i_c] + rad[i_c] <= SQ/2) & (cax[i_c] - rad[i_c] >= -SQ/2) & (cay[i_c] + rad[i_c] <= SQ/2) & (cay[i_c] - rad[i_c] >= -SQ/2)); k++)
                                    {
                                        rad[i_c] = rand.Next((int) 0, (int)SQ / 2);
                                    }
                                    pr = true;
                                    

                                }

                                if ((cax[i_c] - rad[i_c] >= -SQ / 2) & (cax[i_c] + rad[i_c] <= SQ / 2) & (cay[i_c] - rad[i_c] >= -SQ / 2) & (cay[i_c] + rad[i_c] <= SQ / 2) & (pr == true))
                                {
                                    check_c = true;
                                    replace = false;

                                    pr = false;

                                    cbx[i_c] = cax[i_c] + rad[i_c];
                                    cby[i_c] = cay[i_c] ;

                                    textBox39.Text = ($"{cax[i_c]}");
                                    textBox40.Text = ($"{cay[i_c]}");
                                    textBox41.Text = ($"{cbx[i_c]}");
                                    textBox55.Text = ($"{cby[i_c]}");

                                    S_circle = Circle.S(rad[i_c]);

                                    if (S_circle < 0)
                                    {
                                        S_circle = -S_circle;
                                    }
                                    if (rad[i_c] != 0)
                                    {

                                        if (max_S_c[i_c] < rad[i_c] * 2)
                                        {
                                            max_S_c[i_c] = rad[i_c] * 2;
                                        }

                                        textBox48.Text = ($"{(S_circle)}");

                                        MessageBox.Show($"{rad[i_c]}");
                                        i_c++;
                                    }

                                    else if ((cax[i_c] == cbx[i_c]) & (cay[i_c] == cby[i_c]))
                                    {

                                        MessageBox.Show("Радиус в таком случае = 0");
                                        if (S_circle != 0)
                                        {
                                            check_c = false;
                                            replace = true;
                                            S_ALL -= Figure.S(0, 0, 0, 0, 0, 0, S_circle);
                                        }
                                    }

                                }
                                
                            }
                            else
                            {
                                check_c = false;
                                replace = true;
                                MessageBox.Show("Вы указали координаты больше доступных, посмотрите на координатные прямые");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Вы использовали максимальное количество кругов");
                            check_c = false;
                            replace = true;
                        }
                    }
                     else
                    {
                        MessageBox.Show("Вы не поместили фигуру");
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SQUARE_SUPER();
        }

        public void SQUARE_SUPER()
        {
            var SQ_S = new SUPERSQUARE();

            SQ = 0;
            try
            {
                SQ = Convert.ToInt32(textBox45.Text);
            }

            catch
            {
                MessageBox.Show("Вы неверно указали сторону квадрата, она является числом");
            }
            finally
            {
                if ((SQ <= 22) & SQ >= 1)
                {
                    risPB2();
                    textBox45.ReadOnly = true;
                    S_S_create = true;
                    S_SQUARE_SUPER = SQ_S.S(SQ);
                    textBox50.Text = ($"{S_SQUARE_SUPER}");
                }
                else if (SQ > 22)
                    MessageBox.Show("Вы указали сторону больше доступной, посмотрите на координатные прямые");
                else if (SQ < 1)
                {
                    MessageBox.Show("Вы указали сторону меньше доступной, посмотрите на координатные прямые");
                }

            }
        }
        public void ris_S()
        {
            ris_SS = true;
            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            Graphics k = pictureBox2.CreateGraphics();

            using (k = Graphics.FromImage(btmp1))
            {

                k.DrawLine(new Pen(Color.Black, 2), SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                            -SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                            SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                            SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                k.DrawLine(new Pen(Color.Black, 2), SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                           SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                           -SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                           SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                k.DrawLine(new Pen(Color.Black, 2), -SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                           SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                           -SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                           -SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);
                k.DrawLine(new Pen(Color.Black, 2), -SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                               -SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2,
                               SQ / 2 * (float)(pictureBox2.Width / 31.9999) + (float)pictureBox2.Width / 2,
                               -SQ / 2 * (float)pictureBox2.Height / 24 + pictureBox2.Height / 2);

                k.Dispose();

            }

            pictureBox2.Image = btmp1;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (S_S_create == true)
            {
                if (ris_SS == false)
                {
                    ris_S();
                }
                else
                    MessageBox.Show("Вы уже отрисовали выбранный вами квадрат");
            }
            else
                MessageBox.Show("Вы не создали квадрат квадрат");

        }
        private void button24_Click(object sender, EventArgs e)
        {
            if (ris_SS == false)
            {
                MessageBox.Show("Сначала нарисуйте квадрат, в который будете укладывать");
            }
            else
            {
                if ((i_t == 0) & (i_sq == 0) & (i_r == 0) & (i_p == 0) & (i_rh == 0) & (i_tz == 0) & (i_c == 0))
                {
                    MessageBox.Show("Вам пока нечего помещать");
                }
                else
                {
                    if ((check_t == true) & (replace == false))
                    {
                        
                        ris_triangle();
                        replace = true;
                    }
                    else if ((check_sq == true) & (replace == false))
                    {
                        
                        ris_square();
                        replace = true;
                    }
                    else if ((check_r == true) & (replace == false))
                    {
                        
                        ris_rectangle();
                        replace = true;
                    }
                    else if (check_p == true)
                    {
                        
                        ris_parallelogram();
                        replace = true;
                    }
                    else if (check_rh == true)
                    {
                        ris_rhomb();
                        replace = true;
                    }
                    else if (check_tz == true)
                    {
                        
                        ris_trapezoid();
                        replace = true;
                    }
                    else if (check_c == true)
                    {
                       
                            ris_C();
                            replace = true;
                       
                    }

                }
            }

        }

        public void ris_C()
        {

            Bitmap btmp1 = new Bitmap(pictureBox2.Image);
            //clear();

            Graphics k = pictureBox2.CreateGraphics();



            using (k = Graphics.FromImage(btmp1))
            {
                int j = 0;

                try
                {
                    if (i_c < numericUpDown2.Value)
                    {
                        j = i_c - 1;


                        SolidBrush Ta = new SolidBrush(Color.BlueViolet);
                        float l = (float)Math.Pow(rad[j], 2);
                        l = 2 * l;
                        l = (float)Math.Pow(l, 0.5);

                        RectangleF rect = new RectangleF(
                            (pictureBox2.Width / 2 - rad[j] * (float)(pictureBox2.Width / 31.9999) + cax[j] * (float)(pictureBox2.Width / 31.9999)),
                            (pictureBox2.Height / 2 - rad[j] * (float)pictureBox2.Height / 24 - cay[j] * (float)(pictureBox2.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox2.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox2.Height / 24)));

                       
                        k.FillEllipse(Ta, rect);
                        k.DrawEllipse(new Pen(Color.Black, 2), rect);


                    }
                    else if (i_c == numericUpDown2.Value)
                    {
                        j = (int)numericUpDown2.Value -1;
                        SolidBrush Ta = new SolidBrush(Color.BlueViolet);

                        RectangleF rect = new RectangleF(
                            (pictureBox1.Width / 2 - rad[j] * (float)(pictureBox1.Width / 31.9999) + cax[j] * (float)(pictureBox1.Width / 31.9999)),
                            (pictureBox1.Height / 2 - rad[j] * (float)pictureBox1.Height / 24 - cay[j] * (float)(pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Width / 31.9999)),
                            ((float)(rad[j] * 2 * (float)pictureBox1.Height / 24)));

                        RectangleF rect1 = new RectangleF(0.0F, 0.0F, 200.0F, 100.0F);

                        k.FillEllipse(Ta, rect);
                        k.DrawEllipse(new Pen(Color.Black, 2), rect);

                    }
                    else
                        MessageBox.Show("Вы отрисовали все возможные круги");
                }
                catch
                {
                    if (j == -1)
                    {
                        MessageBox.Show("Вы не создали круг");
                    }

                }

                k.Dispose();
                pictureBox2.Image = btmp1;
            }
        
            pictureBox2.Image = btmp1;
        }
    


    private void button22_Click(object sender, EventArgs e)
        {
            textBox39.Clear();
            textBox40.Clear();
            textBox41.Clear();
            textBox55.Clear();
            textBox48.Clear();

        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox31.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();
            textBox38.Clear();
            textBox47.Clear();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox28.Clear();
            textBox29.Clear();
            textBox30.Clear();
            textBox46.Clear();
            textBox62.Clear();
            textBox63.Clear();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox53.Clear();
            textBox60.Clear();
            textBox61.Clear();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox44.Clear();
            textBox58.Clear();
            textBox59.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox43.Clear();
            textBox56.Clear();
            textBox57.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox42.Clear();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Graphics.FromImage(pictureBox1.Image).Clear(Color.White);
            risPB1();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
