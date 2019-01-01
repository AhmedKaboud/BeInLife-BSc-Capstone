using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNLife
{
    public partial class UserData : Form
    {
        SpeechControl speechControl;
        DatabaseControl databaseControl;
        MatLapControl matlapControl;
        string root = "C:\\SNCVC\\SignUpImages\\";

        //default constructor to intatiate the class objects
        public UserData()
        {
            InitializeComponent();
            speechControl = new SpeechControl();
            matlapControl = new MatLapControl();
            databaseControl = new DatabaseControl();
        }

        //open the Async Task for Single once to be able to Submit Data
        private void UserData_Load(object sender, EventArgs e)
        {
            //List of Coice Commands which will be used in SignUp Form
            string[] Commands = { "submit", "cancel", "help" };
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
            if (str == "submit")
            {
                Submit();
                speechControl.recognitionEngine.RecognizeAsyncStop();
            }
            else if (str == "help")
            {

            }
            else if (str == "cancel")
            {
                this.Close();
            }
        }

        //it get the user data from Matlap after doing trainning  to the submitted pictures
        //and also get the Email and password from the userData form
        private void Submit()
        {
            User user = new User();
            user.email = txt_email.Text;
            user.password = txt_password.Text;

            Object[] MatlabOut = matlapControl.MatlabHandler(root, "SignUp", 4);
            user.CorMatx = (double[,])MatlabOut[0];
            user.EigFacesUser = (double[,])MatlabOut[1];
            user.MforUser = (double[,])MatlabOut[2];
            user.eigFaceSize = (int)((double)MatlabOut[3]);

            DatabaseControl db = new DatabaseControl();
            db.InsertNewUser(user);


            speechControl.recognitionEngine.RecognizeAsyncStop();
            Facebook facebook = new Facebook(user.email, user.password);
            this.Hide();
            facebook.Show();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            Submit();
        }
    }
}
