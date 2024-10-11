using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos png|*.png|Arcvhivos jpg|*.jpg";

            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Archivos png|*.png|Arcvhivos jpg|*.jpg";

            openFileDialog2.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap edgeDetectedImage = SobelEdgeDetection(bmp);
                pictureBox2.Image = edgeDetectedImage;
            }
            else
            {
                MessageBox.Show("Primero carga una imagen.");
            }
        }

        private Bitmap SobelEdgeDetection(Bitmap bmp)
        {
            Bitmap b = new Bitmap(bmp.Width, bmp.Height);
            int[,] gx = new int[3, 3] 
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };

            int[,] gy = new int[3, 3]
            {
                { -1, -2, -1 },
                { 0, 0, 0 },
                { 1, 2, 1 }
            };

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    int new_x = 0, new_y = 0;
                    Color pixel;

                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            pixel = bmp.GetPixel(i + x, j + y);
                            int intensity = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                            new_x += gx[x + 1, y + 1] * intensity;
                            new_y += gy[x + 1, y + 1] * intensity;
                        }
                    }

                    int magnitude = (int)Math.Sqrt(new_x * new_x + new_y * new_y);
                    magnitude = Math.Min(255, Math.Max(0, magnitude));

                    b.SetPixel(i, j, Color.FromArgb(magnitude, magnitude, magnitude));
                }
            }
            return b;
        }

    }
}
