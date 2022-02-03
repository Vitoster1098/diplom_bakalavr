using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap input, output, first;
        string filename = "", imageformats = "All files(*.*)|*.*|PNG Files(*.PNG)|*.PNG|JPG Files(*.jpg)|*.JPG|JPEG Files(*.jpeg)|*.JPEG|BMP Files(*.bmp)|*.BMP|TIFF Files(*.tiff)|*.TIFF|GIF Files(*.GIF)|*.GIF";
        double[] gistoramma = new double[256];
        double q = double.MinValue;
        decimal[] z = new decimal[256], d = new decimal[256];
        CheckGistogramm check;

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = imageformats;
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                filename = openFileDialog.FileName;
                input = new Bitmap(filename);
                first = new Bitmap(filename);
                //pict1.Size = new Size(input.Width, input.Height);                
                //ReChange();
            }
            else
            {
                return;
            }
            output = new Bitmap(input.Width, input.Height);
            for (int j = 0; j < input.Height; j++)
            {
                for (int i = 0; i < input.Width; i++)
                {
                    float NewColor = 0f;
                    if (radioButton1.Checked)
                    {
                        NewColor = (input.GetPixel(i, j).R * 0.299f + input.GetPixel(i, j).G * 0.587f + input.GetPixel(i, j).B * 0.114f);
                    }
                    else
                    {
                        NewColor = (input.GetPixel(i, j).R * 0.2126f + input.GetPixel(i, j).G * 0.7152f + input.GetPixel(i, j).B * 0.0722f);
                    }
                    output.SetPixel(i, j, Color.FromArgb((int)NewColor, (int)NewColor, (int)NewColor));
                }
            }
            pict1.Image = output;

            if (pict2.Image != null)
            {
                pict2.Image.Dispose();
                pict2.Image = null;
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = pict1.Image.Width * pict1.Image.Height;
            FillGistogramma((Bitmap)pict1.Image);            
            input.Dispose();
            pict1.Invalidate();
        }

        ImageFormat getFormat(string inp)
        {
            string ext = System.IO.Path.GetExtension(inp);
            ImageFormat formats = ImageFormat.Png;
            switch (ext)
            {
                case ".jpeg":
                    formats = ImageFormat.Jpeg;
                    break;
                case ".bmp":
                    formats = ImageFormat.Bmp;
                    break;
                case ".tiff":
                    formats = ImageFormat.Tiff;
                    break;
                case ".gif":
                    formats = ImageFormat.Gif;
                    break;
            }
            return formats;
        }

        /*public void ReChange() //изменение размеров и локации элементов управления под размер картинки
        {            
            pict2.Location = new Point(pict1.Width + 24, pict2.Location.Y);
            pict2.Size = pict1.Size;
            this.Width = pict1.Width + pict2.Width + 50;
            this.Height = pict1.Height + label1.Height + 120;
            label1.Location = new Point(label1.Location.X, pict1.Height + 50);
            textBox1.Location = new Point(textBox1.Location.X, label1.Location.Y);
            button1.Location = new Point(button1.Location.X, textBox1.Location.Y);            
            button3.Location = new Point(button3.Location.X, button1.Location.Y);
            progressBar1.Location = new Point(progressBar1.Location.X, button3.Location.Y);
        }*/

        public void FillGistogramma(Bitmap bitmap)
        {
            for (int i = 0; i < gistoramma.Length; ++i)
                gistoramma.SetValue(0, i);
            for(int i = 0; i < bitmap.Width; ++i)
            {
                for(int j = 0; j < bitmap.Height; ++j)
                {
                    gistoramma[bitmap.GetPixel(i,j).R]++;
                    progressBar1.Value++;
                }
            }
                for (int i = 0; i < gistoramma.Length; ++i)
                    gistoramma[i] = gistoramma[i] / (bitmap.Width * bitmap.Height);
        }        

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = imageformats;
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.OverwritePrompt = true;

            GetPB get_number = new GetPB();
            get_number.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
                switch (get_number.number)
                {
                case 0:
                    {
                            if (pict1.Image == null)
                            {
                                MessageBox.Show("Сохранение невозможно, так как нет сохраняемого изображения", "Ошибка");
                                return;
                            }
                            pict1.Image.Save(filename, getFormat(filename));
                            break;
                    }
                case 1:
                    {
                            if (pict2.Image == null)
                            {
                                MessageBox.Show("Сохранение невозможно, так как нет сохраняемого изображения", "Ошибка");
                                return;
                            }
                            pict2.Image.Save(filename, getFormat(filename));
                            break;
                    }
                case 2:
                    {
                            if(pict3.Image == null)
                            {
                                MessageBox.Show("Сохранение невозможно, так как нет сохраняемого изображения", "Ошибка");
                                return;
                            }
                            pict3.Image.Save(filename, getFormat(filename));
                            break;
                    }
                }
            get_number.Close();            
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
            this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана Матвеевым В.А.", "О программе");
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = "";
            input = null;
            output = null;
            pict1.Image = null;
            pict2.Image = null;
            pict3.Image = null;
            textBox1.Text = "0";
            //ReChange();
        }

        private void pict2_MouseClick(object sender, MouseEventArgs e)
        {            
            if(output == null) return; 
            Color da = new Color();
            da = output.GetPixel(e.X, e.Y);
            MessageBox.Show("R:"+da.R+" G:"+da.G+" B:"+da.B);
        }

        public bool EnterQ()
        {
            try
            {
                q = Convert.ToDouble(textBox1.Text);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Коэффициент Q введён не корректно","Ошибка");
                return false;
            }
        }

        private void гистограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pict1.Image != null || pict2.Image != null || pict3.Image != null)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = (pict1.Image.Width * pict1.Image.Height) * 3;
                FillGistogramma(new Bitmap(pict2.Image));
                check = new CheckGistogramm();
                check.mymas1 = new double[gistoramma.Length];
                gistoramma.CopyTo(check.mymas1, 0);
                FillGistogramma(new Bitmap(pict3.Image));
                check.mymas2 = new double[gistoramma.Length];
                gistoramma.CopyTo(check.mymas2, 0);
                FillGistogramma(new Bitmap(pict1.Image));                
                check.mymas = gistoramma;
                check.MdiParent = MDIParent1.ActiveForm;
                check.Show();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //старый формат
        {            
            float NewColor = 0;
            Bitmap change = null;
            if(first != null)
            {
                change = new Bitmap(first.Width, first.Height);
            }
            else
            {
                return;
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = first.Width * first.Height;
            for (int i = 0; i < first.Width; ++i)
            {
                for(int j = 0; j < first.Height; ++j)
                {
                    NewColor = (first.GetPixel(i, j).R * 0.299f + first.GetPixel(i, j).G * 0.587f + first.GetPixel(i, j).B * 0.114f);
                    change.SetPixel(i, j, Color.FromArgb((int)NewColor, (int)NewColor, (int)NewColor));
                    progressBar1.Value++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            Bitmap test = new Bitmap(first);

            BitmapData bpdata = test.LockBits(new Rectangle(0, 0, first.Width, first.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr intPtr = bpdata.Scan0;

            byte[] da = new byte[(first.Width * first.Height)*3];
            Marshal.Copy(intPtr, da, 0, da.Length); //в массиве типа байт находится теперь цвета пикселей в формате rgbrgbrgb
            //дальше этот массив отправляется на редактирование

            Bitmap net = BytesToBitmap(da);
            FillGistogramma(net);
            decimal[] arr = CreateZ(false);

            input = new Bitmap(output.Width, output.Height);
            progressBar1.Maximum = output.Width * output.Height;
            progressBar1.Value = 0;
            for (int i = 0; i < output.Width; ++i)
            {
                for (int j = 0; j < output.Height; ++j)
                {
                    var pix = ((Bitmap)output).GetPixel(i, j);
                    input.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(z[pix.R]), Convert.ToInt32(z[pix.G]), Convert.ToInt32(z[pix.B])));
                    progressBar1.Value++;
                }
            }
            pict3.Image = input;
            progressBar1.Value = 0;
            pict3.Invalidate();
        }

        public static Bitmap BytesToBitmap(byte[] byteArray)
        {

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray))
            {
                Bitmap img = (Bitmap)Image.FromStream(ms);
                return img;
            }
        }

            private void radioButton2_CheckedChanged(object sender, EventArgs e) //новый гост
        {
            float NewColor = 0;
            Bitmap change = null;
            if (first != null)
            {
                change = new Bitmap(first.Width, first.Height);
            }
            else
            {
                return;
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = first.Width * first.Height;
            for (int i = 0; i < first.Width; ++i)
            {
                for (int j = 0; j < first.Height; ++j)
                {
                    NewColor = (first.GetPixel(i, j).R * 0.2126f + first.GetPixel(i, j).G * 0.7152f + first.GetPixel(i, j).B * 0.0722f);
                    change.SetPixel(i, j, Color.FromArgb((int)NewColor, (int)NewColor, (int)NewColor));
                    progressBar1.Value++;
                }
            }
        }

        decimal[] CreateZ(bool newalg)
        {
            //FillGistogramma((Bitmap)pict1.Image, true);
            for (int i = 0; i < d.Length; ++i)
                {
                    if (!newalg)
                    {
                        try
                        {
                            d[i] = Convert.ToDecimal(0.5 + q * 256 * gistoramma[i]);
                        }
                        catch (Exception)
                        {
                            d[i] = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            d[i] = Convert.ToDecimal(0.5 + q * 32 * gistoramma[i] * Math.Log(1 / gistoramma[i])); //делить на 0 нельзя
                        }
                        catch (Exception)
                        {
                            d[i] = 0;
                        }
                    }
                }
                decimal[] u = new decimal[256];
                u[0] = 0;

                for (int i = 1; i < d.Length; ++i)
                {
                    u[i] = u[i - 1] + d[i - 1] + d[i];
                }

                decimal V = 255 / u[255];                
                z[255] = 255;

                for (int i = 0; i < 255; ++i)
                {
                    z[i] = V * u[i];
                }
            return z;
        }

        private void button3_Click(object sender, EventArgs e) //информ преобразование
        {
            EnterQ();
            if (q == double.MinValue || q < 0)
            {
                MessageBox.Show("Измените значение коэффициента Q");
                return;
            }
            else
            {
                decimal[] z = CreateZ(true);
            }            
            input = new Bitmap(output.Width, output.Height);
            progressBar1.Maximum = output.Width * output.Height;
            progressBar1.Value = 0;
            for (int i = 0; i < output.Width; ++i)
            {
                for (int j = 0; j < output.Height; ++j)
                {
                    var pix = ((Bitmap)output).GetPixel(i, j);
                    input.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(z[pix.R]), Convert.ToInt32(z[pix.G]), Convert.ToInt32(z[pix.B])));
                    progressBar1.Value ++;
                }
            }
            pict3.Image = input;
            progressBar1.Value = 0;
            pict3.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e) //преобразование
        {
            EnterQ();
            if (q == double.MinValue || q < 0)
            {
                MessageBox.Show("Измените значение коэффициента Q");
                return;
            }
            else
            {
                decimal[] z = CreateZ(false);
            }
            input = new Bitmap(output.Width, output.Height);
            progressBar1.Maximum = output.Width * output.Height;
            progressBar1.Value = 0;
            for (int i = 0; i < output.Width; ++i)
            {
                for(int j = 0; j < output.Height; ++j)
                {  
                    var pix = ((Bitmap)output).GetPixel(i, j);
                    input.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(z[pix.B]), Convert.ToInt32(z[pix.B]), Convert.ToInt32(z[pix.B])));
                    progressBar1.Value++;
                }               
            }
            pict2.Image = input;
            progressBar1.Value = 0;
            pict2.Invalidate();
        }
    }
}