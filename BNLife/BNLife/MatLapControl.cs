using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BNLife
{
    class MatLapControl
    {
        MLApp.MLApp Mat;

        //Default Constructor
        //it will set the MatLap Compiler and execute it.
        public MatLapControl()
        {
            Mat = new MLApp.MLApp();
            Mat.Execute(@"cd C:\\SNCVC\\MatLapFunc");
        }

        //It Handle the connection between matlap and C# in SIGNUP Phase
        public object[] MatlabHandler(string Path, string FunctionName, int numOfOutput)
        {
            object result = null;
            Mat.Feval(FunctionName, numOfOutput, out result, Path);

            Object[] res = result as object[];
            return res;
        }

        //It Handle the connection between matlap and C# in LOGIN Phase
        public object[] MatlabHandlerLogin(List<double[,]> Matrix, string Path, string FunctionName, int numOfOutput)
        {
            object result = null;

            Mat.Feval(FunctionName, numOfOutput, out result, Matrix[0], Matrix[1], Matrix[2], Matrix[3], Path);
            Object[] res = result as object[];
            return res;
        }

        //It check of the Path Exist Or Not.
        public string makeDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

    }
}
