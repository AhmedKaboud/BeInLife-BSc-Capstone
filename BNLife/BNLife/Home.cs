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
    public partial class Home : Form
    {
        SpeechControl speechControl;
        MatLapControl matlapControl;
        DatabaseControl databaseControl;
        CamControl camcontrol;
        int TimerCounter = 0;
        string root = "C:\\SNCVC\\SignUpImages\\";

        //Constructor
        //it will open the camera & set the Timer Counter to 0 & make root path to save the images
        public Home()
        {
            InitializeComponent();
            speechControl = new SpeechControl();
            matlapControl = new MatLapControl();
            databaseControl = new DatabaseControl();
            camcontrol = new CamControl(PicBox_Login);
            camcontrol.CloseVideoSource();
            camcontrol.GetCamList();
            camcontrol.openCam();
            TimerCounter = 0;
        }

        //we will Start the Speech Recognition and Create the event to handle its Command
        private void Home_Load(object sender, EventArgs e)
        {
            //List of Coice Commands which will be used in Home Form
            string[] Commands = { "login", "signup", "close" };
            speechControl.IntializeSpeech(Commands);

            //It Create Event Handler to handle the Connection of Login
            speechControl.recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            //Start the Speech recognition asynchronously with Single Lestin 
            speechControl.StartSingleAsync();
        }

        //Event Handler to Handle Login form speech Recognition -> [login, signup] Commands
        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string str = e.Result.Text;
            if (str == "login")
            {
                TimerCounter = 3;
                timer_login.Start();
            }
            else if (str == "signup")
            {
                //this.Close() ;
                camcontrol.CloseVideoSource();
                System.Threading.Thread.Sleep(1000);
                SignUp form = new SignUp();
                this.Hide();
                form.Show();
                camcontrol.openCam();
            }
            else if (str == "close")
            {
                Application.Exit();
            }
        }

        //Login Timer tick event Handler
        private void timer_login_Tick(object sender, EventArgs e)
        {
            if (TimerCounter > 0)
            {
                lbl_timer.Text = lbl_timer.Text.Substring(0, lbl_timer.Text.Length - 2) + " " + TimerCounter.ToString();
                TimerCounter--;
            }
            else
            {
                timer_login.Stop();
                lbl_timer.Text = "Done";
                Login();
            }
        }

        //Login into Facebook
        //1- it capture the picture for the user
        //2- Retrieve All Data from DB.
        //3- Send the taken Picture and the data from DB to make Face Recognition
        private void Login()
        {
            //take Picture using CamCotrol Class
            string dirctory = matlapControl.makeDirectory(root + "1\\");
            string Index = Get_Index();
            camcontrol.path = dirctory + Index + ".jpg";
            camcontrol.TakePicture();

            //Retrive all Users Data from DB using DatabaseControl Class
            DatabaseControl db = new DatabaseControl();
            List<User> list = db.GetAllUser();

            //Convert it 
            List<double[,]> Matrices = Get_ListOFMatrix(list);

            //it will Call matlap and return the index of matched User
            Object[] MatlabOut = matlapControl.MatlabHandlerLogin(Matrices, camcontrol.path, "SignIn", 1);
            int MatchedUserIndex = (int)((double)MatlabOut[0]);

            //it will get the Email and Password
            string Email = list[MatchedUserIndex].email;
            string Password = list[MatchedUserIndex].password;

            Facebook facebook = new Facebook(Email, Password);
            this.Hide();
            facebook.Show();

            //Adaptive learning for Face Recognition
            User user = new User();

            Object[] MatlabOut2 = matlapControl.MatlabHandler(root, "SignUp", 4);
            user.CorMatx = (double[,])MatlabOut2[0];
            user.EigFacesUser = (double[,])MatlabOut2[1];
            user.MforUser = (double[,])MatlabOut2[2];
            user.eigFaceSize = (int)((double)MatlabOut2[3]);

            DatabaseControl db2 = new DatabaseControl();
            db2.UpdateFileMatrices(user);
        }

        //Convert all matrices to the Matlap Form to send to it.
        //Matlap form -> double[,]
        private List<double[,]> Get_ListOFMatrix(List<User> list)
        {
            //get Dimension of the width and height of each mtrix
            ////[1,0] ---> num of rows [height]
            ////[1,1] ---> num of cols [width]
            int[,] Dim = new int[3, 2];
            for (int i = 0; i < list.Count; i++)
            {
                Dim[0, 0] = Math.Max(Dim[0, 0], list[i].CorMatx.GetLength(0));
                Dim[0, 1] += list[i].CorMatx.GetLength(1);

                Dim[1, 0] = Math.Max(Dim[1, 0], list[i].EigFacesUser.GetLength(0));
                Dim[1, 1] += list[i].EigFacesUser.GetLength(1);

                Dim[2, 0] = Math.Max(Dim[2, 0], list[i].MforUser.GetLength(0));
                Dim[2, 1] += list[i].MforUser.GetLength(1);
            }

            //make matrix 2d for every matrix in the User DB which will be sent to matlap
            //will convert the Eig sie to 1d Matrix
            double[,] corMatx = new double[Dim[0, 0], Dim[0, 1]];
            double[,] EigFacesMatx = new double[Dim[1, 0], Dim[1, 1]];
            double[,] MUserMatx = new double[Dim[2, 0], Dim[2, 1]];
            double[] Eigensize = new double[list.Count];
            int StartIndex1 = 0, StartIndex2 = 0, StartIndex3 = 0;
            for (int i = 0; i < list.Count; ++i)
            {
                Array.Copy(list[i].CorMatx, 0, corMatx, StartIndex1, list[i].CorMatx.GetLength(0) * list[i].CorMatx.GetLength(1));
                Array.Copy(list[i].EigFacesUser, 0, EigFacesMatx, StartIndex2, list[i].EigFacesUser.GetLength(0) * list[i].EigFacesUser.GetLength(1));
                Array.Copy(list[i].MforUser, 0, MUserMatx, StartIndex3, list[i].MforUser.GetLength(0) * list[i].MforUser.GetLength(1));
                Eigensize[i] = list[i].eigFaceSize;
                StartIndex1 += Dim[0, 0] * list[i].CorMatx.GetLength(1);
                StartIndex2 += list[i].EigFacesUser.GetLength(0) * list[i].EigFacesUser.GetLength(1);
                StartIndex3 += list[i].MforUser.GetLength(0) * list[i].MforUser.GetLength(1);
            }

            //make matrix 2d for Eigen Size matrix which will be sent to matlap
            double[,] EigensizeMatx = new double[1, list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                EigensizeMatx[0, i] = Eigensize[i];
            }

            //Add all 2d Matrices to the returned List
            List<double[,]> listOfMatx = new List<double[,]>();
            listOfMatx.Add(corMatx);
            listOfMatx.Add(EigFacesMatx);
            listOfMatx.Add(MUserMatx);
            listOfMatx.Add(EigensizeMatx);

            return listOfMatx;
        }

        //it get the index of the next picture to be saved in the Folder.
        //it count num of files in the Folder then increment it by one.
        private string Get_Index()
        {
            DirectoryInfo Folder = new DirectoryInfo(root + "1\\");
            FileInfo[] Images = Folder.GetFiles();
            int Index = Images.Length + 1;
            return Index.ToString();
        }

        //Hanle Sign Up Button Click
        private void btn_signup_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            this.Hide();
            signUp.Show();

        }

        //To close the camera after closing the home form
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            camcontrol.CloseVideoSource();
        }

        private void lbl_timer_Click(object sender, EventArgs e)
        {

        }

        private void Home_Activated(object sender, EventArgs e)
        {
            //openAgain Camer
            camcontrol.openCam();
        }

    }
}
