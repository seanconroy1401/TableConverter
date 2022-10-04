// See https://aka.ms/new-console-template for more information

//If there is an issue with the command line when typing dotnet run -v then it needs to be input as:
//dotnet run -- -v -i etc
//You need the two dashes in order to use my arguments due to the command line interface

using System;
using System.IO;

namespace Tutorial1
{

    public class Table {

        private int numRows;
        private int numCols;

        private string [,] myTable; //2d array for the table format

        public Table() {

        }

        public Table(int numCols, int numRows)
        {
            this.numRows = numRows;
            this.numCols = numCols;

            myTable = new string[numRows, numCols];
        }

        public int getNumRows() {
            return this.numRows;
        }
        public void setNumRows(int input)
        {
            this.numRows = input;
        }
        public int getNumCols()
        {
            return this.numCols;
        }
        public void setNumCols(int input)
        {
            this.numCols = input;
        }

        public void fillTable(string[] inputs)
        { //Take in an array of data and fill the table piece by piece

            int counter = 0; //keep track of what point in the input array we're at

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    myTable[i, j] = inputs[counter];
                    counter++;
                }
            }
        }

        public string getValue(int i, int j) { //returns an individual value from the table

            return this.myTable[i,j];

        }


    }

    class Program
    {

        static void Main(string[] args)
        {


            /*string path = @"C:\temp\MyText.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("World!");
                }
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

            string path = @"C:\temp\l.csv";

            string wholeFile = "";

            if (File.Exists(path))
            {
                wholeFile = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("Error: No such file");
            }

            string[] individualParts = wholeFile.Split(',', '\n');

            for (int i = 0; i < individualParts.Length; i++) {
                individualParts[i] = individualParts[i].Replace("\"", "");
            }

            //Console.WriteLine(wholeFile);
            for (int i = 0; i < individualParts.Length; i++) {
                Console.WriteLine(individualParts[i]);
            }

            string path = @"C:\temp\l.csv";
            Table myTable = csvToTable(path);

            for (int i = 0; i < myTable.getNumRows(); i++)
            {
                for (int j = 0; j < myTable.getNumCols(); j++)
                {
                    Console.WriteLine(myTable.getValue(i,j));
                }
            }*/

            string originalFilePath = "";
            string newFilePath = "";

            for (int i = 0; i < args.Length; i++) { //loop through the dotnet command line args

                switch (args[i]) {

                    case "tabconv" : originalFilePath = args[i+1];
                    break;

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

                    case "-o" : newFilePath = args[i+1];
                                Console.WriteLine("File name chosen: " + newFilePath);
                    break;

                }

            }

            Table myTable = new Table();

            string [] originalFileTypeCheck = originalFilePath.Split('.');
            string originalFileType = originalFileTypeCheck[originalFileTypeCheck.Length - 1];
            originalFileType = originalFileType.ToLower();

            switch (originalFileType) {
                case "csv" : myTable = csvToTable(originalFilePath); // working
                break;
            }

            string[] newFileTypeCheck = newFilePath.Split('.');
            string newFileType = newFileTypeCheck[newFileTypeCheck.Length - 1];
            newFileType = newFileType.ToLower();

            switch (newFileType)
            {
                case "csv":
                    tableToCSV(newFilePath, myTable); //bugged
                    break;
            }

        }

        static Table csvToTable(string path)
        { //these are placeholders

            string wholeFile = "";

            if (File.Exists(path)) {
                wholeFile = File.ReadAllText(path);
            }
            else {
                Console.WriteLine("Error: No such file");
            }

            string [] rows = wholeFile.Split('\n'); //Split the string by new line in order to split it into an array of rows
            int numRows = rows.Length; //Which allows you to determine the number of rows
            string [] cols = rows[1].Split(','); //split one of the rows in order to determine the number of columns
            int numCols = cols.Length;

            string [] individualParts = wholeFile.Split(',','\n');

            for (int i = 0; i < individualParts.Length; i++)
            {
                individualParts[i] = individualParts[i].Replace("\"", "");
                individualParts[i] = individualParts[i].Replace(Environment.NewLine, "");
            }

            Table myTable = new Table(numRows, numCols);
            myTable.fillTable(individualParts);
            return myTable;
        }

        static Table htmlToTable()
        {
            Table myTable = new Table(0, 0);
            return myTable;
        }

        static Table mdToTable()
        {
            Table myTable = new Table(0, 0);
            return myTable;
        }

        static Table jsonToTable()
        {
            Table myTable = new Table(0, 0);
            return myTable;
        }

        static bool onlyDigits(string input) { //I use this to check if I'm putting a string or number (including decimals) into my CSV file
            foreach (char c in input) {
                if (c < '0' || c > '9') {
                    return false;
                }
            }
            return true;
        }

        static void tableToCSV(string path, Table input) {

            String output = "";

            int rows = input.getNumRows();
            int cols = input.getNumCols();

            for (int i = 0; i < rows; i++) {

                for (int j = 0; j < cols; j++) {

                    if ( (j == cols-1) ) {

                        if (onlyDigits(input.getValue(i, j))) {
                            Console.WriteLine(input.getValue(i,j));
                            output = output + input.getValue(i, j);
                        }
                        else {
                            Console.WriteLine(input.getValue(i, j));
                            string concat = String.Concat("\"", input.getValue(i, j).TrimEnd('\n','\r'), "\"");
                            concat = concat.Replace(Environment.NewLine, "");
                            output = output + concat;
                        }

                    }
                    else {

                        if (onlyDigits(input.getValue(i, j))) {
                            Console.WriteLine(input.getValue(i, j));
                            //string concat = String.Concat("\"",input.getValue(i,j),"\" ",",");
                            output = output + input.getValue(i, j) + ",";
                        }
                        else {
                            Console.WriteLine(input.getValue(i, j));
                            string concat = String.Concat("\"", input.getValue(i, j), "\"", ",").TrimEnd('\r', '\n');
                            //Console.WriteLine(concat);
                            output = output + concat;
                        }

                    }

                }

                //output = output + "\n";

            }

            Console.WriteLine(output);

            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(output);
            }

        }
    }
}