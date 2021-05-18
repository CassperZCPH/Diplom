using System;
using System.Windows.Forms;
using System.Drawing;
using WIA;
using System.Runtime.InteropServices;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        JournalRecognizer journalRecognizer = new JournalRecognizer();

        private DeviceInfo AvailableScanner = null;

        private Image<Bgr, byte> inputImage = null;

        public Form1()
        {
            InitializeComponent();
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
                    ListOfScan.Items.Add(AvailableScanner.Properties["Name"].get_Value());
                    break;
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                IImageFile imgFile = (ImageFile)scanerItem.Transfer(FormatID.wiaFormatJPEG);
                
                byte[] imageBites = (byte[])imgFile.FileData.get_BinaryData();
                MemoryStream ms = new MemoryStream(imageBites);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                //inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
            }
/*
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            journalRecognizer.DetectGrid(inputImage);
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
