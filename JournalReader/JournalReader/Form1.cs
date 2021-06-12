using System;
using System.Windows.Forms;
using System.Drawing;
using WIA;
using System.Runtime.InteropServices;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace JournalReader
{
    public partial class Form1 : Form
    {
        private GridHandler gridHandler = new GridHandler();
        private DeviceInfo AvailableScanner;
        private Image<Bgr, byte> inputImage;

        private Rectangle selRect;
        private Point orig;
        private Pen pen = new Pen(Brushes.OrangeRed, 2.0f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DeviceManager deviceManager = new DeviceManager();
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }
                    AvailableScanner = deviceManager.DeviceInfos[i];
                    comboBox1.Items.Add(AvailableScanner.Properties["Name"].get_Value());
                    break;
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Enabled = false;
                comboBox1.Text = " Нет подключённых устройств";
            }
            if (AvailableScanner != null) btnScan.Enabled = true;
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            try
            {
                /*DeviceManager deviceManager = new DeviceManager();

                DeviceInfo AvailableScanner = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }

                    AvailableScanner = deviceManager.DeviceInfos[i];

                    break;
                }*/
                Device device = AvailableScanner.Connect();
                Item scanerItem = device.Items[1];
                IImageFile imageFile = (ImageFile)scanerItem.Transfer(FormatID.wiaFormatJPEG);
                //inputImage = (Image<Bgr, byte>)scanerItem.Transfer(FormatID.wiaFormatJPEG);
                inputImage = (Image<Bgr, byte>)imageFile;

                byte[] imageBites = (byte[])imageFile.FileData.get_BinaryData();
                MemoryStream ms = new MemoryStream(imageBites);
                pictureBox1.Image = Image.FromStream(ms);

                btnProcess.Enabled = true;
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    /*Size vol = Image.FromFile(openFileDialog1.FileName).Size;
                    double scaleVol = (double)vol.Height / vol.Width;
                    double width = pictureBox1.Height / scaleVol;
                    pictureBox1.Width = (int)width;*/
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);

                    btnProcess.Enabled = true;
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Неверный формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> viewImage = inputImage.Copy();
            gridHandler.DetectGrid(ref viewImage);
            gridHandler.DetectIntersect(ref viewImage);
            pictureBox1.Image = Image.FromStream(new MemoryStream(viewImage.ToJpegData()));
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Возвращаем основную процедуру рисования
            pictureBox1.Paint -= SelectionPaint;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.Invalidate();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Назначаем процедуру рисования при выделении
            pictureBox1.Paint -= PictureBox1_Paint;
            pictureBox1.Paint += SelectionPaint;
            orig = e.Location;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //при движении мышкой считаем прямоугольник и обновляем picturebox
            selRect = GetSelRectangle(orig, e.Location);
            if (e.Button == MouseButtons.Left) (sender as PictureBox).Refresh();
        }

        //основное событие рисования
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 2.0f), selRect);
        }

        //Рисование мышкой с нажатой кнопкой
        private void SelectionPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, selRect);
        }

        private Rectangle GetSelRectangle(Point orig, Point location)
        {
            int deltaX = location.X - orig.X, deltaY = location.Y - orig.Y;
            Size s = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
            Rectangle rect = new Rectangle();
            if (deltaX >= 0 & deltaY >= 0)
            {
                rect = new Rectangle(orig, s);
            }
            else if (deltaX < 0 & deltaY > 0)
            {
                rect = new Rectangle(location.X, orig.Y, s.Width, s.Height);
            }
            else if (deltaX < 0 & deltaY < 0)
            {
                rect = new Rectangle(location, s);
            }
            else if (deltaX > 0 & deltaY < 0)
            {
                rect = new Rectangle(orig.X, location.Y, s.Width, s.Height);
            }
            return rect;
        }
    }
}

/* для сохранения
string path = @"C:\Users\Каспер\Desktop\Дэплом\Сканы";
string fileName = @"\ScanImg1.jpg";

if (!Directory.Exists(path))
{
    Directory.CreateDirectory(path);
}

path += fileName;

if (File.Exists(path))
{
    int index = path.LastIndexOf("Img1") + 3;
    int numberOfFile = Convert.ToInt32(path[index]);
    numberOfFile += 1;
    path.Replace("Img1", "Img" + Convert.ToChar(numberOfFile));
}
*/