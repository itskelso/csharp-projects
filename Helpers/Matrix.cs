using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySpotterLibrary.Helpers
{
    public static class Matrix
    {
        // Edge Detection
        public static double[,] Laplacian3x3
        {
            get
            {
                return new double[,]
                { { -1, -1, -1, },
                { -1,  8, -1, },
                { -1, -1, -1, }, };
            }
        }



        //Blurs Below
        public static double[,] Mean3x3
        {
            get
            {
                return new double[,]
               { {  1, 1, 1, },
                  {  1, 1, 1, },
                  {  1, 1, 1, }, };
            }
        }


        public static double[,] Mean5x5
        {
            get
            {
                return new double[,]
               { {  1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1 }, };
            }
        }


        public static double[,] Mean7x7
        {
            get
            {
                return new double[,]
                { {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 }, };
            }
        }


        public static double[,] Mean9x9
        {
            get
            {
                return new double[,]
                { {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1, 1, 1 }, };
            }
        }

        public static double[] Gaussian1D3X3
        {
            get
            {
                return new double[] {0.27901, 0.44198, 0.27901 };
            }
        }

        public static double[] Sobel1DX
        {
            get
            {
                return new double[] { 1.0, 0.0, -1.0 };
            }
        }

        public static double[] Sobel1DY
        {
            get
            {
                return new double[] { 1.0, 2.0, 1.0 };
            }
        }

        public static double[,] SobelX3x3
        {
            get
            {
                return new double[,]
                { {  -1.0,   0.0,    1.0 },
                  {  -2.0,   0.0,    2.0 },
                  {  -1.0,   0.0,    1.0 }, };
            }
        }

        public static double[,] SobelXRotated1803x3
        {
            get
            {
                return new double[,]
                { {  1.0,   0.0,    -1.0 },
                  {  2.0,   0.0,    -2.0 },
                  {  1.0,   0.0,    -1.0 }, };
            }
        }




        public static double[,] SobelY3x3
        {
            get
            {
                return new double[,]
                { { -1.0,  -2.0,   -1.0 },
                  {  0.0,   0.0,    0.0 },
                  {  1.0,   2.0,    1.0 }, };
            }
        }




        public static double[,] GaussianBlur3x3
        {
            get
            {
                return new double[,]
                { {  0.024879,   0.107973,    0.024879 },
                  {  0.107973,   0.468592,    0.107973 },
                  {  0.024879,   0.107973,    0.024879 }, };
            }
        }


        public static double[,] GaussianBlur5x5
        {
            get
            {
                return new double[,]
                { {  2, 04, 05, 04, 2 },
                  {  4, 09, 12, 09, 4 },
                  {  5, 12, 15, 12, 5 },
                  {  4, 09, 12, 09, 4 },
                  {  2, 04, 05, 04, 2 }, };
            }
        }


        public static double[,] MotionBlur5x5
        {
            get
            {
                return new double[,]
                { {  1, 0, 0, 0, 1 },
                  {  0, 1, 0, 1, 0 },
                  {  0, 0, 1, 0, 0 },
                  {  0, 1, 0, 1, 0 },
                  {  1, 0, 0, 0, 1 }, };
            }
        }


        public static double[,] MotionBlur5x5At45Degrees
        {
            get
            {
                return new double[,]
                { {  0, 0, 0, 0, 1 },
                  {  0, 0, 0, 1, 0 },
                  {  0, 0, 1, 0, 0 },
                  {  0, 1, 0, 0, 0 },
                  {  1, 0, 0, 0, 0 }, };
            }
        }


        public static double[,] MotionBlur5x5At135Degrees
        {
            get
            {
                return new double[,]
                { {  1, 0, 0, 0, 0 },
                  {  0, 1, 0, 0, 0 },
                  {  0, 0, 1, 0, 0 },
                  {  0, 0, 0, 1, 0 },
                  {  0, 0, 0, 0, 1 }, };
            }
        }


        public static double[,] MotionBlur7x7
        {
            get
            {
                return new double[,]
                { {  1, 0, 0, 0, 0, 0, 1 },
                  {  0, 1, 0, 0, 0, 1, 0 },
                  {  0, 0, 1, 0, 1, 0, 0 },
                  {  0, 0, 0, 1, 0, 0, 0 },
                  {  0, 0, 1, 0, 1, 0, 0 },
                  {  0, 1, 0, 0, 0, 1, 0 },
                  {  1, 0, 0, 0, 0, 0, 1 }, };
            }
        }


        public static double[,] MotionBlur7x7At45Degrees
        {
            get
            {
                return new double[,]
                { {  0, 0, 0, 0, 0, 0, 1 },
                  {  0, 0, 0, 0, 0, 1, 0 },
                  {  0, 0, 0, 0, 1, 0, 0 },
                  {  0, 0, 0, 1, 0, 0, 0 },
                  {  0, 0, 1, 0, 0, 0, 0 },
                  {  0, 1, 0, 0, 0, 0, 0 },
                  {  1, 0, 0, 0, 0, 0, 0 }, };
            }
        }


        public static double[,] MotionBlur7x7At135Degrees
        {
            get
            {
                return new double[,]
                { {  1, 0, 0, 0, 0, 0, 0 },
                  {  0, 1, 0, 0, 0, 0, 0 },
                  {  0, 0, 1, 0, 0, 0, 0 },
                  {  0, 0, 0, 1, 0, 0, 0 },
                  {  0, 0, 0, 0, 1, 0, 0 },
                  {  0, 0, 0, 0, 0, 1, 0 },
                  {  0, 0, 0, 0, 0, 0, 1 }, };
            }
        }


        public static double[,] MotionBlur9x9
        {
            get
            {
                return new double[,]
                { { 1, 0, 0, 0, 0, 0, 0, 0, 1, },
                  { 0, 1, 0, 0, 0, 0, 0, 1, 0, },
                  { 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                  { 0, 0, 0, 1, 0, 1, 0, 0, 0, },
                  { 0, 0, 0, 0, 1, 0, 0, 0, 0, },
                  { 0, 0, 0, 1, 0, 1, 0, 0, 0, },
                  { 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                  { 0, 1, 0, 0, 0, 0, 0, 1, 0, },
                  { 1, 0, 0, 0, 0, 0, 0, 0, 1, }, };
            }
        }


        public static double[,] MotionBlur9x9At45Degrees
        {
            get
            {
                return new double[,]
                { { 0, 0, 0, 0, 0, 0, 0, 0, 1, },
                  { 0, 0, 0, 0, 0, 0, 0, 1, 0, },
                  { 0, 0, 0, 0, 0, 0, 1, 0, 0, },
                  { 0, 0, 0, 0, 0, 1, 0, 0, 0, },
                  { 0, 0, 0, 0, 1, 0, 0, 0, 0, },
                  { 0, 0, 0, 1, 0, 0, 0, 0, 0, },
                  { 0, 0, 1, 0, 0, 0, 0, 0, 0, },
                  { 0, 1, 0, 0, 0, 0, 0, 0, 0, },
                  { 1, 0, 0, 0, 0, 0, 0, 0, 0, }, };
            }
        }


        public static double[,] MotionBlur9x9At135Degrees
        {
            get
            {
                return new double[,]
                { { 1, 0, 0, 0, 0, 0, 0, 0, 0, },
                  { 0, 1, 0, 0, 0, 0, 0, 0, 0, },
                  { 0, 0, 1, 0, 0, 0, 0, 0, 0, },
                  { 0, 0, 0, 1, 0, 0, 0, 0, 0, },
                  { 0, 0, 0, 0, 1, 0, 0, 0, 0, },
                  { 0, 0, 0, 0, 0, 1, 0, 0, 0, },
                  { 0, 0, 0, 0, 0, 0, 1, 0, 0, },
                  { 0, 0, 0, 0, 0, 0, 0, 1, 0, },
                  { 0, 0, 0, 0, 0, 0, 0, 0, 1, }, };
            }
        }
    }
}
