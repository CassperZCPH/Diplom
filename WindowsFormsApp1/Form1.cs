using System;
using System.Windows.Forms;
using System.Drawing;
using WIA;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private DeviceInfo AvailableScanner = null;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnScan_Click(object sender, EventArgs e)
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
                return;
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
