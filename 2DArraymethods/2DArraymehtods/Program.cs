using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DArraymehtods
{
    
    class Program
    {
        //L12A1
        // Returns the 2nd smallest element in the 2d aray
        static int Finder(int[,]TDarr)
        {
            int k = TDarr[0, 0];     
            for (int i = 0; i < TDarr.GetLength(0); i++)
            {
                for (int j = 0; j <TDarr.GetLength(1) ; j++)
                {                                
                   if(TDarr[i,j]<k)
                    {
                        k = TDarr[i, j];            
                    }
                }              
            }
            int length=TDarr.Length-1;
            int[] arr = new int[length];
            int ScndSmall = TDarr[0, 0]; ;
            int copy = 0;
            for (int i = 0; i < TDarr.GetLength(0); i++)
            {
                for (int j = 0; j < TDarr.GetLength(1); j++)
                {
                    if (TDarr[i, j] != k)
                    {
                        arr[copy++] = TDarr[i, j];
                    }                 
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i]<ScndSmall)
                {
                    ScndSmall = arr[i];
                }
            }
            return ScndSmall;
        }
     
        //L12A2
        //Given a 2D array, a position and a new value, write a method that returns the array with the new value
        //at the given position
        static void SubstituteAElementAtGivenIndex(int[,]TDarr,int newVal,int position1,int position2)
        {
            for (int i = 0; i < TDarr.GetLength(0); i++)
            {
                for (int j = 0; j < TDarr.GetLength(1); j++)
                {
                   if(TDarr[i,j]==TDarr[position1,position2])
                    {
                        TDarr[i, j]=newVal;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            int[,] arr =
            {
                {11,2,3,4,5,6 },
                {12,23,13,14,16,18 }
            };
           // Console.WriteLine(Finder(arr));
            SubstituteAElementAtGivenIndex(arr, 38, 0, 3);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]+" ");
                }
            }
            Console.ReadKey();
        }
    }
}
