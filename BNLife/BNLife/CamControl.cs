using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using System.IO;

namespace BNLife
{
    class CamControl
    {
        private List<string> CamList;
        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        private PictureBox PicBox;
        private int CamIndex;
        public string path = "";

        //Default constructor
        public CamControl()
        {
        }

        //Constructor to update the picture Box which will contain the photo
        public CamControl(PictureBox _PicBox)
        {
            PicBox = _PicBox;
            CamIndex = 0;
            CamList = new List<string>();
        }

        //it get the List of Video Devices connected to Computer
        public void GetCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                CamList.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();
                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    CamList.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                MessageBox.Show("No capture device on your system");
            }
        }

        //Open the Camera. It returns True if the camera opened sucessfully otherwise False
        public Boolean openCam()
        {
            if (DeviceExist)
            {
                videoSource = new VideoCaptureDevice(videoDevices[CamIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                //CloseVideoSource();
                //videoSource.DesiredFrameSize = new Size(640, 460);
                videoSource.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Used to Capture a photo from the Camera by calling Capture Method
        public void TakePicture()
        {
            if (DeviceExist)
            {
                Capture();
            }
            else
            {
                MessageBox.Show("Error: No Device selected.");
            }
        }

        //to take current view of picBox (image)
        private void Capture()
        {
            PicBox.Image.Save(path);
            CloseVideoSource();
            //used to Stop the Camera to Show the taken pictures
            System.Threading.Thread.Sleep(1000);
            openCam();
        }

        //It's Event Handler to Update the View of PicBox to be able to show the Video.
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            PicBox.Image = img;
        }

        //close the device safely
        public void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

    }
}
