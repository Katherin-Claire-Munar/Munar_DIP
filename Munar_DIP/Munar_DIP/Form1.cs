using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Munar_DIP
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFile;
        SaveFileDialog saveFile;
        Bitmap newImage, convertedImage;
        public Form1()
        {
            InitializeComponent();

            openFile = new OpenFileDialog();
            openFile.RestoreDirectory = true;
            openFile.InitialDirectory = "C:\\";
            openFile.FilterIndex = 1;
            openFile.Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png ";

            saveFile = new SaveFileDialog();
            saveFile.RestoreDirectory = true;
            saveFile.InitialDirectory = "C:\\";
            saveFile.FilterIndex = 1;
            saveFile.Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png";
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFile.ShowDialog())
            {
                Image img = (Image)convertedImage;
                img.Save(saveFile.FileName);
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFile.ShowDialog())
            {
                newImage = new Bitmap(openFile.FileName);
                pictureBox1.Image = newImage;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            convertedImage = new Bitmap(newImage.Width, newImage.Height);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    Color p = newImage.GetPixel(x, y);
                    convertedImage.SetPixel(x, y, p);
                }
            }
            pictureBox2.Image = convertedImage;
        }


        private void btnGrayscale_Click(object sender, EventArgs e)
        {
            convertedImage = new Bitmap(newImage.Width, newImage.Height);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    Color p = newImage.GetPixel(x, y);
                    int grey = (p.R + p.G + p.B) / 3;
                    convertedImage.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }
            pictureBox2.Image = convertedImage;
        }


        private void btnNegative_Click(object sender, EventArgs e)
        {
            convertedImage = new Bitmap(newImage.Width, newImage.Height);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    Color p = newImage.GetPixel(x, y);
                    int grey = (p.R + p.G + p.B) / 3;
                    convertedImage.SetPixel(x, y, Color.FromArgb(255 - p.R, 255 - p.G, 255 - p.B));
                }
            }
            pictureBox2.Image = convertedImage;
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            Color p;

            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    p = newImage.GetPixel(x, y);
                    int grey = (p.R + p.G + p.B) / 3;
                    newImage.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }

            int[] hisdata = new int[256];
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    p = newImage.GetPixel(x, y);
                    hisdata[p.R]++;
                }
            }

            convertedImage = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    convertedImage.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(hisdata[x] / 5, convertedImage.Height - 1); y++)
                {
                    convertedImage.SetPixel(x, (convertedImage.Height - 1) - y, Color.Black);
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = convertedImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSepia_Click(object sender, EventArgs e)
        {
            convertedImage = new Bitmap(newImage.Width, newImage.Height);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    Color p = newImage.GetPixel(x, y);
                    int r = (int)(0.393 * p.R + 0.769 * p.G + 0.189 * p.B);
                    int g = (int)(0.349 * p.R + 0.686 * p.G + 0.168 * p.B);
                    int b = (int)(0.272 * p.R + 0.534 * p.G + 0.131 * p.B);

                    if (r > 255)
                    {
                        r = 255;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }

                    convertedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = convertedImage;
        }





    }
}
