using System;
using WinApi;
using System.Runtime.InteropServices;
using System.Runtime;

namespace cs
{
    class Args{
        public int from;
        public int to;

        public int result;

        public Args(int from, int to){
            this.from = from;
            this.to = to;
        }
    }
    class Program
    {
        private static void Count (int from, int to, out int result){
            result = 0;
            for (int i=from;i<to;i++)
             result += i;
            
        }
        private static void Wrap (IntPtr data){
            var args = (Args) GCHandle.FromIntPtr(data).Target;
            Count (args.from, args.to, out args.result);            
        }
        static void Main(string[] args)
        {
            Args arrgs1 = new Args (1,100);
            IntPtr ziped1 = (IntPtr) GCHandle.Alloc(arrgs1);
            IntPtr t1 =  WinApiFuncs.CreateThread(Wrap, ziped1);
            Args arrgs2 = new Args (100,200);
            IntPtr ziped2 = (IntPtr) GCHandle.Alloc(arrgs2);
            IntPtr t2 =  WinApiFuncs.CreateThread(Wrap, ziped2);

            WinApiFuncs.WaitForSingleObject(t1, 1000);
            Console.WriteLine (arrgs1.result);
            WinApiFuncs.WaitForSingleObject(t2, 1000);
            Console.WriteLine (arrgs2.result);
            
        }
    }
}
