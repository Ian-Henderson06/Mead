using System;
using System.Collections.Generic;

namespace Mead
{
    class Mead
    {
        static bool hasError = false;

        static void Main(string[] args)
        {
           // if (args.Length == 0)
            //    Console.WriteLine("[MEAD][ERROR] Arguments must be provided.");

            try
            {
                // byte[] fileBytes = FileHandler.GetFileBytes(args[0]);
                byte[] fileBytes = FileHandler.GetLocalFileBytes("programs/test.me");
                run(System.Text.Encoding.Default.GetString(fileBytes, 0, fileBytes.Length));
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("[TRACE] " + e.StackTrace);
            }
                Console.ReadKey();
        }
        
        static void run(string src)
        {
            Scanner scanner = new Scanner(src);
            List<Token> tokens = scanner.ScanTokens();

            foreach(Token t in tokens)
            {
                Console.WriteLine(t);
            }
        }

        public static void Error(int line, string message)
        {
            report(line, "", message);    
        }

        static void report(int line, string where, string message)
        {
            Console.WriteLine("[MEAD] " +  "[Line " + line + "] Error" + where + ": " + message);
            hasError = true;
        }
     
    }
}
