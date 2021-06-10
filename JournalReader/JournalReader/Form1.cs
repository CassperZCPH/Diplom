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
        GridHandler gridHandler = new GridHandler();
        private DeviceInfo AvailableScanner;
        private Image<Bgr, byte> inputImage;

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