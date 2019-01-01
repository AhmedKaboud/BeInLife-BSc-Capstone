using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNLife
{
    class User
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double[,] CorMatx { get; set; }
        public double[,] EigFacesUser { get; set; }
        public double[,] MforUser { get; set; }
        public int eigFaceSize { get; set; }
        public string SignUpPicPath { get; set; }
    }
}
