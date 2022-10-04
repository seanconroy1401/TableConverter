// See https://aka.ms/new-console-template for more information

//If there is an issue with the command line when typing dotnet run -v then it needs to be input as:
//dotnet run -- -v -i etc
//You need the two dashes in order to use my arguments due to the command line interface

using System;

namespace Tutorial1
{

    public class Table {



    }

    class Program
    {

        static void Main(string[] args)
        {
            string originalFile = "";
            string newFileName = "";

            for (int i = 0; i < args.Length; i++) { //loop through the dotnet command line args

                switch (args[i]) {

                    case "-v" : Console.WriteLine("Running in verbose mode");
                                //additional verbose mode code
                    break;

                    case "-i" : Console.WriteLine("Code Version Number: 1.0.0");
                    break;

                    case "-h" : Console.WriteLine("The commands are:");
                                Console.WriteLine("'-h' for help");
                                Console.WriteLine("'-v' for verbose mode");
                                Console.WriteLine("'-i' for version info");
                                Console.WriteLine("'-o' to specify input");
                    break;

                    case "-o" : newFileName = args[i+1];
                                Console.Write("File name chosen: " + newFileName);
                    break;

                }

                /*if (s == "-v") {
                    Console.WriteLine("Running in verbose mode");
                    //additional verbose mode code
                }

                /*if (args[s] == "-i") {
                    //will return the table object's version number attribute
                }

                if (args[s] == "-o") {
                    //newFileName = args[s+1];
                }*/

            }

        }
    }
}