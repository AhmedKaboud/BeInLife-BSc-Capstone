using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.IO;

namespace BNLife
{
    public partial class SignUp : Form
    {
        SpeechControl speechControl;
        MatLapControl matlapControl;
        CamControl camcontrol;
        int TimerCounter;
        string root = "C:\\SNCVC\\SignUpImages\\";
        string previosString = "";
        int imageCounter;
        List<PictureBox> ListOfPicBox;
        List<string> listOfPathes;

        public SignUp()
        {
            InitializeComponent();
            speechControl = new SpeechControl();
            matlapControl = new MatLapControl();
            ListOfPicBox = new List<PictureBox>();
            FillListOfBox();
            camcontrol = new CamControl(PicBox_SignUp);
            camcontrol.CloseVideoSource();
            camcontrol.GetCamList();
            camcontrol.openCam();
            TimerCounter = 0;
            imageCounter = 0;
            listOfPathes = new List<string>();
            FillListOfPathes();
        }

        private void FillListOfPathes()
        {
            for (int i = 1; i <= 9; i++)
            {
                listOfPathes.Add("C:\\Users\\ahmedelkashef\\Documents\\Visual Studio 2012\\Projects\\BNLife\\BNLife\\bin\\Debug\\Data\\s1\\" + i.ToString() + ".jpg");
            }
        }

        //open the Async Task for Mice to Do Speech Reconition
        private void SignUp_Load(object sender, EventArgs e)
        {
            PicBox_State.ImageLocation = listOfPathes[imageCounter + 1];

            //List of Coice Commands which will be used in SignUp Form
            string[] Commands = { "next", "cancel", "start" };
            speechControl.IntializeSpeech(Commands);

            //It Create Event Handler to handle the Connection of Sign Up Commands
            speechControl.recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            //Start the Speech recognition asynchronously with Multi Lestin 
            speechControl.StartSingleAsync();
        }

        //Event Handler to Handle Login form speech Recognition -> [start, next, cancel] Commands
        //there are Restrictions on commands
        //1- must say "start" at first.
        //2- must say next after "next" or after "start"
        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string str = e.Result.Text;
            if (str == "start" && previosString == "")
            {
                TimerCounter = 3;
                previosString = str;
                timer_signup.Start();
            }
            else if (str == "next" && previosString == "next" && imageCounter == 9)
            {
                timer_signup.Start();
            }
            else if ((str == "next" && previosString == "start") || (str == "next" && previosString == "next"))
            {
                TimerCounter = 3;
                previosString = str;
                timer_signup.Start();
            }
            else if (str == "cancel")
            {
                this.Close();
            }
        }

        //Login Timer tick event Handler
        //it handle if it will take a sequnce picture or go to next form
        private void timer_signup_Tick(object sender, EventArgs e)
        {
            if (TimerCounter > 0)
            {
                lbl_timer.Text = lbl_timer.Text.Substring(0, lbl_timer.Text.Length - 2) + " " + TimerCounter.ToString();
                TimerCounter--;
            }
            else
            {
                timer_signup.Stop();

                if (imageCounter == 9)
                {
                    lbl_timer.Text = "Done";
                    camcontrol.CloseVideoSource();
                    speechControl.recognitionEngine.RecognizeAsyncStop();
                    timer_signup.Stop();
                    timer_signup.Dispose();
                    timer_signup.Enabled = false;
                    System.Threading.Thread.Sleep(1000);
                    UserData dataForm = new UserData();
                    this.Hide();
                    dataForm.Show();
                }
                else
                {
                    takeSequencePic();
                }
            }
        }

        //it called 9 times to take Pictures which needed to sign up
        private void takeSequencePic()
        {
            string dirctory = matlapControl.makeDirectory(root + "1\\");
            string Index = (++imageCounter).ToString();
            camcontrol.path = dirctory + Index + ".jpg";
            camcontrol.TakePicture();

            //it validates the picture is human
            //return 1 for true, 0 for false
            Object[] MatlabOut = matlapControl.MatlabHandler(camcontrol.path, "ValidateImage", 1);
            double x = (double)MatlabOut[0];
            int y = (int)x;

            if (y == 1)
            {
                ListOfPicBox[imageCounter - 1].ImageLocation = camcontrol.path;
                PicBox_State.ImageLocation = listOfPathes[imageCounter + 1];
            }
            else
            {
                imageCounter--;
            }
            //start new task to go to next command
            speechControl.StartSingleAsync();
        }

        //it cancel Sign up Process and go to Home Form
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Home home = new Home();
            this.Hide();
            home.Show();
        }

        //this will fill the list with all picture Box to be displayed at the Buttom
        private void FillListOfBox()
        {
            ListOfPicBox.Add(PicBox_1);
            ListOfPicBox.Add(PicBox_2);
            ListOfPicBox.Add(PicBox_3);
            ListOfPicBox.Add(PicBox_4);
            ListOfPicBox.Add(PicBox_5);
            ListOfPicBox.Add(PicBox_6);
            ListOfPicBox.Add(PicBox_7);
            ListOfPicBox.Add(PicBox_8);
            ListOfPicBox.Add(PicBox_9);
        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            camcontrol.CloseVideoSource();
        }
    }
}
