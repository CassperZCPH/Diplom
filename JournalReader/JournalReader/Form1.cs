using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using WIA;
using Emgu.CV;
using Emgu.CV.Structure;

namespace JournalReader
{
    public partial class Form1 : Form
    {
        private List<DeviceInfo> scanners = new List<DeviceInfo>();
        private DeviceInfo availableScanner;
        private Image<Bgr, byte> inputImage;
        private GridHandler gridHandler = new GridHandler();
        
        private Size pictureSize;
        private Rectangle selectRect;
        private Point startPoint;
        private Pen pen = new Pen(Brushes.Crimson, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

        private byte countRect = 0;

        public Form1()
        {
            InitializeComponent();
            pictureSize = pictureBox1.Size;
            SizeChanged += Form1_SizeChanged;
            comboBoxScan.SelectedIndexChanged += ComboBoxScan_SelectedIndexChanged;
            trackBar1.Scroll += TrackBar1_Scroll;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DeviceManager deviceManager = new DeviceManager();
                for (byte i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType) continue;
                    comboBoxScan.Items.Add(deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
                    scanners.Add(deviceManager.DeviceInfos[i]);
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (comboBoxScan.Items.Count == 0)
            {
                comboBoxScan.Enabled = false;
                comboBoxScan.Text = " Нет подключённых устройств";
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(panelPictureBox.Width / 2 - pictureBox1.Width / 2, panelPictureBox.Height / 2 - pictureBox1.Height / 2);
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            try
            {
                Device device = availableScanner.Connect();
                Item scanerItem = device.Items[1];
                IImageFile imageFile = (ImageFile)scanerItem.Transfer(FormatID.wiaFormatJPEG);

                string fileName = "JR_scan_image_" + DateTime.Now.ToString().Replace(".", "-").Replace(" ", "-").Replace(":", "-");
                imageFile.SaveFile(fileName);
                inputImage = new Image<Bgr, byte>(fileName);

                float scaleImage = (float)inputImage.Size.Height / inputImage.Size.Width;
                float width = pictureBox1.Height / scaleImage;
                pictureBox1.Width = (int)width;
                pictureSize = pictureBox1.Size;

                byte[] imageBites = (byte[])imageFile.FileData.get_BinaryData();
                MemoryStream ms = new MemoryStream(imageBites);
                pictureBox1.Image = Image.FromStream(ms);

                btnSelect.Enabled = true;
                btnProcess.Enabled = false;
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
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                    float scaleImage = (float)inputImage.Size.Height / inputImage.Size.Width;
                    float width = pictureBox1.Height / scaleImage ;
                    pictureBox1.Width = (int)width;
                    pictureSize = pictureBox1.Size;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                    btnSelect.Enabled = true;
                    btnProcess.Enabled = false;
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Неверный формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.BackColor = Color.Crimson;
            btnScan.Enabled = false;
            btnOpen.Enabled = false;
            btnSelect.Enabled = false;
            btnProcess.Enabled = false;
            trackBar1.Enabled = false;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            labelPictureBox.Text = "Выделите основную область";
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> viewImage = inputImage.Copy();
            gridHandler.DetectGrid(ref viewImage);
            gridHandler.DetectIntersect(ref viewImage);
            pictureBox1.Image = Image.FromStream(new MemoryStream(viewImage.ToJpegData()));
        }

        private void ComboBoxScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            availableScanner = scanners[comboBoxScan.SelectedIndex];
            btnScan.Enabled = true;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Paint -= SelectionPaint;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.Invalidate();

            if (selectRect.Size != new Size(0, 0)) countRect++;

            float scale = (float)inputImage.Height / pictureBox1.Height;
            switch (countRect)
            {
                case 1:
                    int firstLeftX = (int)(selectRect.X * scale);
                    int topY = (int)(selectRect.Y * scale);
                    int bottomY = (int)(selectRect.Bottom * scale);
                    gridHandler.SetEdgeDetect(firstLeftX, topY, bottomY);
                    labelPictureBox.Text = "Выделите область оценок";
                    break;
                case 2:
                    labelPictureBox.Text = "";
                    int secondLeftX = (int)(selectRect.X * scale);
                    gridHandler.SetEdgeDetect(secondLeftX);

                    btnSelect.BackColor = Color.Gainsboro;
                    if (availableScanner != null) btnScan.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSelect.Enabled = true;
                    btnProcess.Enabled = true;
                    trackBar1.Enabled = true;
                    pictureBox1.Paint -= PictureBox1_Paint;
                    pictureBox1.MouseMove -= PictureBox1_MouseMove;
                    pictureBox1.MouseUp -= PictureBox1_MouseUp;
                    pictureBox1.MouseDown -= PictureBox1_MouseDown;

                    countRect = 0;
                    break;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Paint -= PictureBox1_Paint;
            pictureBox1.Paint += SelectionPaint;
            startPoint = e.Location;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            selectRect = GetSelRectangle(startPoint, e.Location);
            if (e.Button == MouseButtons.Left) (sender as PictureBox).Refresh();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 3), selectRect);
        }

        private void SelectionPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, selectRect);
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(pictureSize.Width * trackBar1.Value / 100, pictureSize.Height * trackBar1.Value / 100);
            pictureBox1.Location = new Point(panelPictureBox.Width / 2 - pictureBox1.Width / 2, panelPictureBox.Height / 2 - pictureBox1.Height / 2);
            labelTrackBar.Text = trackBar1.Value.ToString() + "%";
        }

        private Rectangle GetSelRectangle(Point pt1, Point pt2)
        {
            int deltaX = pt2.X - pt1.X;
            int deltaY = pt2.Y - pt1.Y;
            Size sizeRect = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
            Rectangle rect = new Rectangle();
            if (deltaX >= 0 & deltaY >= 0)
            {
                rect = new Rectangle(pt1, sizeRect);
            }
            else if (deltaX < 0 & deltaY > 0)
            {
                rect = new Rectangle(pt2.X, pt1.Y, sizeRect.Width, sizeRect.Height);
            }
            else if (deltaX > 0 & deltaY < 0)
            {
                rect = new Rectangle(pt1.X, pt2.Y, sizeRect.Width, sizeRect.Height);
            }
            else if (deltaX < 0 & deltaY < 0)
            {
                rect = new Rectangle(pt2, sizeRect);
            }
            return rect;
        }
    }
}