using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Windows.Forms;

namespace BNLife
{
    class PostMannager
    {
        public bool Post(string accessToken, string statues)
        {
            try
            {
                FacebookClient fb = new FacebookClient(accessToken);
                Dictionary<string, object> postArgs = new Dictionary<string, object>();
                postArgs["message"] = statues;
                fb.Post("/me/feed", postArgs);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool make_like(string accessToken, string id)
        {
            try
            {
                FacebookClient fb = new FacebookClient(accessToken);
                Dictionary<string, object> postArgs = new Dictionary<string, object>();
                postArgs["message"] = "true";
                fb.Post("/" + id + "/Likes", postArgs);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public List<Posts> retrive_posts(string accessToken)
        {
            FacebookClient fb = new FacebookClient(accessToken);
            Dictionary<string, object> RetArgs = new Dictionary<string, object>();
            RetArgs["limit"] = 200;
            dynamic result = fb.Get("/me/feed", RetArgs);
            List<Posts> PostsList = new List<Posts>();
            for (int i = 0; i < result.data.Count; i++)
            {
                Posts posts = new Posts();
                posts.PostId = result.data[i].id;
                if (object.ReferenceEquals(result.data[i].story, null))
                    posts.PostStory = "this story is null";
                else
                    posts.PostStory = result.data[i].story;
                if (object.ReferenceEquals(result.data[i].message, null))
                    posts.PostMessage = "this message is null";
                else
                    posts.PostMessage = result.data[i].message;

                posts.PostPicture = result.data[i].picture;

                PostsList.Add(posts);

            }
            return PostsList;
        }

        public bool make_comment(string accessToken, string comment, string id)
        {

            try
            {
                FacebookClient fb = new FacebookClient(accessToken);
                Dictionary<string, object> CommArgs = new Dictionary<string, object>();
                CommArgs["message"] = comment;
                fb.Post("/" + id + "/comments", CommArgs);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }



        }

    }
}
