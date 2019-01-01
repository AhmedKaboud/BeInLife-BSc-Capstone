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
using mshtml;
using Facebook;
using System.IO;

namespace BNLife
{
    public partial class Facebook : Form
    {
        SpeechControl speechControl;
        string previousString;
        string WrittenString;
        string profile_home;
        List<Posts> posts;
        int indexOfPosts;
        PostMannager Pmanger;
        int counter1;

        //deafult constructor
        public Facebook()
        {
            InitializeComponent();
            speechControl = new SpeechControl();
            previousString = WrittenString = "";
            indexOfPosts = -1;
            counter1 = 0;
            profile_home = "";
            Pmanger = new PostMannager();
        }

        // copy constructor
        public Facebook(string email, string password)
        {
            InitializeComponent();
            speechControl = new SpeechControl();
            previousString = WrittenString = "";
            indexOfPosts = -1;
            counter1 = 0;
            profile_home = "";
            Pmanger = new PostMannager();
            AppSetting.Default.Email = email;
            AppSetting.Default.password = password;

        }

        //injecting javascript files
        void inject_Files()
        {
            HtmlElement head = FB_Webbrowser.Document.GetElementsByTagName("head")[0];
            //jquery file
            HtmlElement scriptEl = FB_Webbrowser.Document.CreateElement("script");
            IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
            string srJquery = File.ReadAllText("jquery-1.11.3.min.js");
            element.text = srJquery;
            head.AppendChild(scriptEl);

            //content scripts
            HtmlElement scriptElement = FB_Webbrowser.Document.CreateElement("script");
            IHTMLScriptElement domScriptElement = (IHTMLScriptElement)scriptElement.DomElement;
            string contentScript = File.ReadAllText("contentScript.js");
            domScriptElement.text = contentScript;
            head.AppendChild(scriptElement);

        }

        //login function
        void login(object[] info)
        {
            inject_Files();
            FB_Webbrowser.Document.InvokeScript("login", info);
        }

        //when browser document completed
        private void FB_Webbrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (FB_Webbrowser.Url.ToString() == "about:blank")
            {
                string redirectUrl = "https://www.facebook.com/connect/login_success.html&response_type=token";
                string auth1 = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&client_secret={2}&scope={3}", AppSetting.Default.AppID, redirectUrl, AppSetting.Default.AppSecret, AppSetting.Default.Scope);
                FB_Webbrowser.Navigate(auth1);
            }

            if (FB_Webbrowser.Url.AbsoluteUri.Contains("api_key"))
            {
                object[] obj = new object[2];
                obj[0] = AppSetting.Default.Email;
                obj[1] = AppSetting.Default.password;
                login(obj);
            }
            if (FB_Webbrowser.Url.AbsoluteUri.Contains("access_token"))
            {
                string url = FB_Webbrowser.Url.AbsoluteUri;
                AppSetting.Default.AccessToken = url.Substring(url.IndexOf("access_token") + 13);
                FB_Webbrowser.Navigate("http://www.facebook.com");
            }

        }

        //load the grammer and start the first task to start speech recognition
        private void Facebook_Load(object sender, EventArgs e)
        {
            //List of Coice Commands which will be used to Access Facebook Form
            string[] Commands = { "next", "back", "home", "profile", "publish", "post", "comment", "like", "down", "up", "close" };
            speechControl.IntializeSpeech(Commands);

            //It Create Event Handler to handle the Connection of Sign Up Commands
            speechControl.recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            //Start the Speech recognition asynchronously with Single Lestin 
            speechControl.StartSingleAsync();
        }

        //this is event handler to check all words in the command list
        //ther are restriction in some command like "publish" which will be executed only if previous is "post" or "comment"
        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string str = e.Result.Text;
            if (str == "home")
            {
                GoHome();
            }
            else if (str == "profile")
            {
                GoProfile();
            }
            else if (str == "next")
            {
                GoToNextPost();
            }
            else if (str == "back")
            {
                GoToBackPost();
            }
            else if (str == "publish" && previousString == "post")
            {
                previousString = "";
                PublishPost();
            }
            else if (str == "publish" && previousString == "comment")
            {
                previousString = "";
                PublishComment();
            }
            else if (str == "like")
            {
                LikePost();
            }
            else if (str == "post")
            {
                previousString = str;
                WritePost();
            }
            else if (str == "comment")
            {
                previousString = str;
                WriteComment();
            }
            else if (str == "down")
            {
                ScrollDown();
            }
            else if (str == "up")
            {
                ScrollUp();
            }
            else if (str == "close")
            {
                Application.Exit();
            }
        }

        private void PublishComment()
        {
            if (profile_home == "profile")
            {
                while (indexOfPosts < posts.Count && indexOfPosts >= 0)
                {
                    if (posts[indexOfPosts].PostId != null)
                    {

                        string id = posts[indexOfPosts].PostId;
                        Pmanger.make_comment(AppSetting.Default.AccessToken, WrittenString, id);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {

            }
            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //This to write a comment through Diticataion Grammer
        private void WriteComment()
        {
            WrittenString = "";
            RecognitionResult result = null;
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                recognizer.SetInputToDefaultAudioDevice();
                result = recognizer.Recognize(new TimeSpan(0, 0, 2));
            }
            catch
            {
            }
            finally
            {
                WrittenString = result.Text;
            }
            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //This to write a comment through Diticataion Grammer
        private void WritePost()
        {
            WrittenString = "";
            RecognitionResult result = null;
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                recognizer.SetInputToDefaultAudioDevice();
                result = recognizer.Recognize(new TimeSpan(0, 0, 2));
            }
            catch
            {
            }
            finally
            {
                WrittenString = result.Text;
            }
            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to scroll up in the facebook page
        private void ScrollUp()
        {
            counter1 -= 400;
            FB_Webbrowser.Document.Window.ScrollTo(counter1, counter1);

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to scroll down in the facebook page
        private void ScrollDown()
        {
            counter1 += 400;
            FB_Webbrowser.Document.Window.ScrollTo(counter1, counter1);
            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncCancel();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to like highlighted post in the Facebook
        private void LikePost()
        {
            if (profile_home == "profile")
            {
                while (indexOfPosts < posts.Count && indexOfPosts >= 0)
                {
                    if (posts[indexOfPosts].PostId != null)
                    {

                        string id = posts[indexOfPosts].PostId;
                        Pmanger.make_like(AppSetting.Default.AccessToken, id);
                        break;
                    }
                    else
                    {

                        break;
                    }
                }
            }
            else
            {

            }


            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to publish post to the Facebook
        private void PublishPost()
        {
            //Type code Here
            Pmanger.Post(AppSetting.Default.AccessToken, WrittenString);

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to Go back to previous post and highlight it
        private void GoToBackPost()
        {

            //Type code Here
            if (indexOfPosts <= -1)
            {

                return;
            }
            FB_Webbrowser.Document.InvokeScript("find_divs");
            FB_Webbrowser.Document.InvokeScript("pre");
            indexOfPosts--;

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to go next to the next post and highlight it
        private void GoToNextPost()
        {
            if (indexOfPosts == -1)
            {
                inject_Files();
            }
            else if (indexOfPosts < -1)
            {

                indexOfPosts = -1;
                return;
            }
            FB_Webbrowser.Document.InvokeScript("find_divs");
            FB_Webbrowser.Document.InvokeScript("next");
            indexOfPosts++;

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to go to profile page
        private void GoProfile()
        {
            profile_home = "profile";
            indexOfPosts = -1;
            counter1 = 0;
            inject_Files();
            posts = Pmanger.retrive_posts(AppSetting.Default.AccessToken);
            FB_Webbrowser.Document.InvokeScript("goToProfile");
            FB_Webbrowser.Document.InvokeScript("find_divs");

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

        //this to go to home page
        private void GoHome()
        {
            profile_home = "home";
            counter1 = 0;
            indexOfPosts = -1;
            FB_Webbrowser.Navigate("http://www.facebook.com");
            inject_Files();
            FB_Webbrowser.Document.InvokeScript("find_divs");

            //Start new Async Task to speech
            speechControl.recognitionEngine.RecognizeAsyncStop();
            System.Threading.Thread.Sleep(100);
            speechControl.StartSingleAsync();
        }

    }
}
