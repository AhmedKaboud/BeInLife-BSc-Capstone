using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Data.SqlTypes;
using System.Globalization;

namespace BNLife
{
    class DatabaseControl
    {
        string connectionString = "Data Source=AHMED\\SQLKASHEF;Initial Catalog=BNLifeDB;Integrated Security=True";

        //This function used to write the file which will be saved in Database in specific format
        //it will return the file path
        private string writeFile(User user)
        {
            string fname = "FileOfMatrices.txt";
            FileStream fs = new FileStream(fname, FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);

            //write the Correlation Matrix in the file
            sr.WriteLine(user.CorMatx.GetLength(0).ToString() + " " + user.CorMatx.GetLength(1).ToString() + " ");
            for (int i = 0; i < user.CorMatx.GetLength(0); i++)
            {
                for (int j = 0; j < user.CorMatx.GetLength(1); j++)
                {
                    sr.Write(user.CorMatx[i, j].ToString() + " ");
                }
                sr.WriteLine();
            }

            //Split between the 2 matrices
            sr.WriteLine("_");
            sr.WriteLine();

            //write the Eigen Faces Matrix for user Matrix in the file
            sr.WriteLine(user.EigFacesUser.GetLength(0).ToString() + " " + user.EigFacesUser.GetLength(1).ToString() + " ");
            for (int i = 0; i < user.EigFacesUser.GetLength(0); i++)
            {
                for (int j = 0; j < user.EigFacesUser.GetLength(1); j++)
                {
                    sr.Write(user.EigFacesUser[i, j].ToString() + " ");
                }
                sr.WriteLine();
            }

            //Split between the 2 matrices
            sr.WriteLine("_");
            sr.WriteLine();

            //write the MforUser Matrix in the file
            sr.WriteLine(user.MforUser.GetLength(0).ToString() + " " + user.MforUser.GetLength(1).ToString() + " ");
            for (int i = 0; i < user.MforUser.GetLength(0); i++)
            {
                for (int j = 0; j < user.MforUser.GetLength(1); j++)
                {
                    sr.Write(user.MforUser[i, j].ToString() + " ");
                }
                sr.WriteLine();
            }
            sr.Close();
            return fname;
        }

        //This function used to Read the file which will be saved in Database in specific format
        //it will return List of 3 matrices
        //[0]-> Correlation Matrix
        //[1]-> Eigen Faces Matrix
        //[2]-> MforUser Matrix
        private List<double[,]> readFile(Byte[] arr)
        {
            double[,] matrix;
            List<double[,]> list = new List<double[,]>();
            string str = "";
            int wid = 0;
            int height = 0;
            int counter = 0;
            bool NewMatrix = true;
            int row, col;
            row = col = 0;

            //to set the first matrix dimention
            int i;
            for (i = 0; i < arr.Length; i++)
            {
                char ch = Convert.ToChar(arr[i]);
                if ((arr[i] >= 48 && arr[i] <= 57) || arr[i] == 46)
                {
                    str += ch;
                }
                else if (ch == ' ' || ch == '\n')
                {
                    if (counter == 0 & NewMatrix)
                    {
                        height = int.Parse(str);
                        counter++;
                        NewMatrix = false;
                    }
                    else if (counter == 1)
                    {
                        wid = int.Parse(str);
                        counter++;
                        break;
                    }
                    str = "";
                }
                else if (counter == 2)
                {
                    break;
                }
            }

            matrix = new double[height, wid];
            str = "";
            counter = 0;
            for (int indx = i + 1; indx < arr.Length; indx++)
            {
                char ch = Convert.ToChar(arr[indx]);
                if ((arr[indx] >= 48 && arr[indx] <= 57) || arr[indx] == 46 || ch == '-')
                {
                    str += ch;
                }
                else if (ch == ' ')
                {
                    if (counter == 0 & NewMatrix)
                    {
                        height = int.Parse(str);
                        counter++;
                        NewMatrix = false;
                    }
                    else if (counter == 1)
                    {
                        wid = int.Parse(str);
                        counter--;
                        row = col = 0;
                        matrix = new double[height, wid];
                    }
                    else
                    {
                        matrix[row, col++] = double.Parse(str);
                        if (col == wid)
                        {
                            row++;
                            col = 0;
                        }
                    }
                    str = "";
                }
                else if (ch == '\n')
                {
                    continue;
                }
                else if (ch == '_')
                {
                    list.Add(matrix);
                    NewMatrix = true;
                }
            }
            list.Add(matrix);
            return list;

        }

        //function to add new user to the database. This used in SignUp Phase
        //it Will Retrun the ID of the inserted User from the database
        public void InsertNewUser(User user)
        {
            //Create the connection.
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a SqlCommand, and identify it as a stored procedure.
            SqlCommand cmdNewCustomer = new SqlCommand("InsertNewUser", conn);
            cmdNewCustomer.CommandType = CommandType.StoredProcedure;

            //Add input parameter from the stored procedure and specify what to use as its value.
            cmdNewCustomer.Parameters.Add(new SqlParameter("@Email", SqlDbType.Text));
            cmdNewCustomer.Parameters["@Email"].Value = user.email;

            cmdNewCustomer.Parameters.Add(new SqlParameter("@Password", SqlDbType.Text));
            cmdNewCustomer.Parameters["@Password"].Value = user.password;

            //it will write the file usign WriteFile function, then convert it to binary to add it Database as a parameter
            byte[] file;
            string fname = writeFile(user);
            FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            file = br.ReadBytes((int)fs.Length);
            cmdNewCustomer.Parameters.Add(new SqlParameter("@FileMatrices", SqlDbType.VarBinary));
            cmdNewCustomer.Parameters["@FileMatrices"].Value = file;
            /////

            cmdNewCustomer.Parameters.Add(new SqlParameter("@EigFacesSize", SqlDbType.Int));
            cmdNewCustomer.Parameters["@EigFacesSize"].Value = user.eigFaceSize;

            //open the connection
            conn.Open();

            //execute the query to insert the new customer data
            cmdNewCustomer.ExecuteNonQuery();
        }

        //retrieve list of all users data in the database
        //used in login phase
        public List<User> GetAllUser()
        {
            List<User> Users = new List<User>();
            User user = new User();

            //Create the connection.
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a SqlCommand, and identify it as a stored procedure.
            SqlCommand cmd = new SqlCommand("GetAllUsersData", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //open the connection with the database
            conn.Open();

            //make reader to read the data sequentially from database
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            //add user bby user from DB to the returned list
            while (rdr.Read())
            {
                user.Id = rdr.GetInt32(rdr.GetOrdinal("ID"));
                user.email = rdr.GetString(rdr.GetOrdinal("Email"));
                user.password = rdr.GetString(rdr.GetOrdinal("Password"));
                user.eigFaceSize = rdr.GetInt32(rdr.GetOrdinal("EigFacesSize"));

                Byte[] blob = new Byte[(rdr.GetBytes(rdr.GetOrdinal("FileMatrices"), 0, null, 0, int.MaxValue))];
                rdr.GetBytes(rdr.GetOrdinal("FileMatrices"), 0, blob, 0, blob.Length);
                List<double[,]> list = readFile(blob);
                user.CorMatx = list[0];
                user.EigFacesUser = list[1];
                user.MforUser = list[2];

                Users.Add(user);
            }
            rdr.Close();

            return Users;
        }

        //this to update the file of matrices in the Database for specific User
        //this used in adaptive learning which be achived after login with the new picture [login pic]
        public void UpdateFileMatrices(User user)
        {
            //Create the connection.
            SqlConnection conn = new SqlConnection(connectionString);

            //Create a SqlCommand, and identify it as a stored procedure.
            SqlCommand cmdNewCustomer = new SqlCommand("UpdateFileMatrices", conn);
            cmdNewCustomer.CommandType = CommandType.StoredProcedure;

            //Add input parameter from the stored procedure and specify what to use as its value.
            cmdNewCustomer.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmdNewCustomer.Parameters["@ID"].Value = user.Id;

            //add the updated file to the sqlCommand
            byte[] file;
            string fname = writeFile(user);
            FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            file = br.ReadBytes((int)fs.Length);
            cmdNewCustomer.Parameters.Add(new SqlParameter("@FileMatrices", SqlDbType.VarBinary));
            cmdNewCustomer.Parameters["@FileMatrices"].Value = file;

            //open the connection
            conn.Open();

            //execute the query to insert the new customer data
            cmdNewCustomer.ExecuteNonQuery();
        }
    }
}
