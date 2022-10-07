// See https://aka.ms/new-console-template for more information

//If there is an issue with the command line when typing dotnet run -v then it needs to be input as:
//dotnet run -- -v -i etc
//You need the two dashes in order to use my arguments due to the command line interface

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using HtmlAgilityPack;

namespace Tutorial1
{

    public class Table {

        private int numRows;
        private int numCols;

        private string [,] myTable; //2d array for the table format

        public Table() {

        }

        public Table(int numRows, int numCols)
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

        public void setValue(int i, int j, String s) {

            myTable[i,j] = s;

        }

    }

    class Program
    {

        static void tablePrinter(Table input) {

            Console.WriteLine();

            for (int i = 0; i < input.getNumRows(); i++) {
                for (int j = 0; j < input.getNumCols(); j++) {
                    Console.Write(input.getValue(i, j));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

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

            string p = @"C:\temp\l.csv";
            Table t = csvToTable(p);

            for (int i = 0; i < t.getNumRows(); i++)
            {
                for (int j = 0; j < t.getNumCols(); j++)
                {
                    Console.Write(t.getValue(i,j) + " ");
                }
                Console.WriteLine();
            }*/

            string p = @"C:\temp\j.json";
            Table t = jsonToTable(p);
            string p2 = @"C:\temp\w.json";
            tableToJSON(p2, t);

            /*string p = @"C:\temp\oldTable.html";
            Table t = htmlToTable(p);
            string p2 = @"C:\temp\newTable.html";
            tableToHTML(p2, t);*/

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

            string [] originalFileTypeCheck = originalFilePath.Split('.'); //This code is just to get the filename extension
            string originalFileType = originalFileTypeCheck[originalFileTypeCheck.Length - 1];
            originalFileType = originalFileType.ToLower(); //ensures the file extension is lowercase

            switch (originalFileType) {
                case "csv" : 
                    myTable = csvToTable(originalFilePath); // working
                    break;
                case "json" : 
                    myTable = jsonToTable(originalFilePath);
                    break;
                case "html" : 
                    myTable = htmlToTable(originalFilePath);
                    break;
            }

            string[] newFileTypeCheck = newFilePath.Split('.');
            string newFileType = newFileTypeCheck[newFileTypeCheck.Length - 1];
            newFileType = newFileType.ToLower();

            switch (newFileType)
            {
                case "csv":
                    tableToCSV(newFilePath, myTable); //fixed
                    break;
                case "json":
                    tableToJSON(newFilePath, myTable);
                    break;
                case "html":
                    tableToHTML(newFilePath, myTable);
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

        static Table htmlToTable(string path)
        {

            string wholeFile = "";

            if (File.Exists(path))
            {
                wholeFile = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("Error: No such file");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(wholeFile);

            int numRows = 0;
            int numCols = 0;

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table")) //Taken from code provided by John Keating and modified to count the numRows and numCols
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    numCols = 0;
                    foreach (HtmlNode cell in row.SelectNodes("td|th"))
                    {
                        numCols++;
                    }
                    numRows++;
                }
            }


            Table myTable = new Table(numRows, numCols);
            int rowIndex = 0;
            int colIndex = 0;

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table")) //Taken from code provided by John Keating and modified to fill my table object
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    colIndex = 0;
                    foreach (HtmlNode cell in row.SelectNodes("td|th"))
                    {
                        myTable.setValue(rowIndex, colIndex, cell.InnerHtml);
                        colIndex++;
                    }
                    rowIndex++;
                }
            }

            return myTable;
        }

        static bool containsNoText(string input) {

            foreach (char c in input) {
                //checks if there is text or digits in the string using chars
                if ( c >= 'a' || c <= 'z' || c >= 'A' || c <= 'Z' || c >= '0' || c <= '9' ) {
                    return false;
                }
            }

            return true;
        }

        static Table mdToTable(string path)
        {

            string wholeFile = "";

            if (File.Exists(path))
            {
                wholeFile = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("Error: No such file");
            }

            string[] rows = wholeFile.Split('\n'); //Split the string by new line in order to split it into an array of rows
            int numRows = rows.Length - 2; //Which allows you to determine the number of rows
            string[] cols = rows[0].Split('|'); //split one of the rows in order to determine the number of columns
            int numCols = cols.Length - 2;

            Console.WriteLine("numrows : {0}, numcols : {1}",numRows, numCols);
            /*for (int i = 0; i < cols.Length; i++) {
                Console.WriteLine(cols[i]);
                if (containsNoText(cols[i])) {
                    Console.WriteLine("Found one!");
                }
            }*/

            Table myTable = new Table(0, 0);
            return myTable;
        }

        static Table jsonToTable(string path)
        {

            string data = "";

            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("Error: No such file");
            }

            using JsonDocument docJSON = JsonDocument.Parse(data); //Taken from the code provided by John Keating
            JsonElement rootJSON = docJSON.RootElement;

            int propertiesCounter = 0;
            var enumerator = rootJSON.EnumerateArray(); //Use this to find the number of properties. This is a modified version of a code snippet I found at https://stackoverflow.com/questions/60838935/get-property-name-from-json
            while (enumerator.MoveNext())
            {
                var propertyEnumerator = enumerator.Current.EnumerateObject();
                while (propertyEnumerator.MoveNext())
                {
                    propertiesCounter++;
                }

                break; //Only need to enumerate through the first object as the properties appear in all objects

            }

            string [] propertyNames = new string[propertiesCounter];
            int tempCounter = 0;

            while (enumerator.MoveNext()) //enumerate again, this time to fill the array with the names of the properties
            {
                var propertyEnumerator = enumerator.Current.EnumerateObject();
                while (propertyEnumerator.MoveNext())
                {
                    propertyNames[tempCounter] = propertyEnumerator.Current.Name;
                    tempCounter++;
                }

                break; //Only need to enumerate through one object as the same properties appear in all objects

            }

            int numCols = propertiesCounter;
            int numRows = rootJSON.GetArrayLength() + 1;

            Table myTable = new Table(numRows, numCols);

            for (int i = 0; i < numCols; i++) { //iterate through the first row in the table to set the headers
                myTable.setValue(0, i, propertyNames[i]);
            }


            for (int i = 1; i < numRows; i++) { //iterate through every other spot in the table to fill in the values

                var currentRow = rootJSON[i-1];

                for (int j = 0; j < numCols; j++) {

                    string currentProperty = propertyNames[j];
                    //Console.WriteLine(currentRow.GetProperty(currentProperty).ToString());
                    myTable.setValue(i, j, currentRow.GetProperty(currentProperty).ToString());

                }

            }

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

        //This is based off a a method I found here: https://stackoverflow.com/questions/6750116/how-to-eliminate-all-line-breaks-in-string
        //This fixes the issue I had where random line breaks were being inserted in my string builder for csv and I couldn't get rid of them
        static string removeLineEndings(string input) {

            string lineSeparatorChar = ((char)0x2028).ToString();
            string paragraphSeparatorChar = ((char)0x2029).ToString();

            return input.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparatorChar, string.Empty)
                        .Replace(paragraphSeparatorChar, string.Empty);

        }

        static void tableToCSV(string path, Table input) {

            String output = "";
            String row = "";

            int rows = input.getNumRows();
            int cols = input.getNumCols();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rows; i++) {

                for (int j = 0; j < cols; j++) {

                    if ( (j == cols-1) ) {

                        if (onlyDigits(input.getValue(i, j))) {
                            //Console.WriteLine(input.getValue(i,j));
                            row = row + input.getValue(i, j);
                            //sb.Append(input.getValue(i, j));
                        }
                        else {
                            //Console.WriteLine(input.getValue(i, j));
                            string concat = String.Concat("\"", input.getValue(i, j).TrimEnd('\n','\r'), "\"");
                            concat = concat.Replace(Environment.NewLine, "");
                            row = row + concat;
                            //sb.Append("\"");
                            ////sb.Append(input.getValue(i, j));
                            //sb.Append("\"");
                        }

                    }
                    else {

                        if (onlyDigits(input.getValue(i, j))) {
                            //Console.WriteLine(input.getValue(i, j));
                            string concat = String.Concat("\"",input.getValue(i,j),"\" ",",");
                            row = row + input.getValue(i, j) + ",";
                            //sb.Append(input.getValue(i, j));
                            //sb.Append(",");
                        }
                        else {
                            //Console.WriteLine(input.getValue(i, j));
                            string concat = String.Concat("\"", input.getValue(i, j), "\"", ",").TrimEnd('\r', '\n');
                            //Console.WriteLine(concat);
                            row = row + concat;
                            /*sb.Append("\"");
                            sb.Append(input.getValue(i, j));
                            sb.Append("\"");
                            sb.Append(",");*/
                        }

                    }

                    row = removeLineEndings(row);

                }

                output = output + row;
                row = "";

                if (i != rows-1) {
                    output = output + "\n";
                }

            }


            //output = sb.ToString();
            //output = removeLineEndings(output);
            //Console.WriteLine(output);

            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(output);
            }

        }

        static string [] getHeadings(Table input) { //returns an array of the headings from a given table

            string [] output = new string[input.getNumCols()];

            for (int i = 0; i < input.getNumCols(); i++) {
                output[i] = input.getValue(0, i);
            }

            return output;

        }

        static void tableToJSON(string path, Table input) {

            string [] headings = getHeadings(input);

            string output = "[ \n";

            for (int i = 1; i < input.getNumRows(); i++) { //iterate through the table and build the string to be written to the json file

                output = output + @"	{ " + "\n";

                for (int j = 0; j < input.getNumCols(); j++) {
                    if (j != input.getNumCols()-1) { //if its not the last row, make sure it ends in a comma
                        if (onlyDigits(input.getValue(i, j))) { //if its only digits don't put it in inverted commas
                            output = output + "		\"" + headings[j] + "\" : " + input.getValue(i, j) + ", \n";
                        }
                        else {
                            output = output + "		\"" + headings[j] + "\" : \"" + input.getValue(i, j) + "\", \n";
                        }
                    }
                    else {
                        if (onlyDigits(input.getValue(i, j)))
                        {
                            output = output + "		\"" + headings[j] + "\" : " + input.getValue(i, j) + " \n";
                        }
                        else
                        {
                            output = output + "		\"" + headings[j] + "\" : \"" + input.getValue(i, j) + "\" \n";
                        }
                    }
                }

                if (i != input.getNumRows()-1) {
                    output = output + @"	}," + "\n";
                }
                else {
                    output = output + @"	}" + "\n";
                }

            }

            output = output + "]";

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(output);
            }

        }

        static void tableToHTML(string path, Table input) {

            string output = "";

            int numRows = input.getNumRows();
            int NumCols = input.getNumCols();

            output = output + "<table> \n";

            for (int i = 0; i < numRows; i++) {

                output = output + "    <tr> \n";

                for (int j = 0; j < NumCols; j++) {

                    if (i == 0) {

                        output = output + "        <th>" + input.getValue(i, j) + "</th> \n";

                    }
                    else {

                        output = output + "        <td>" + input.getValue(i, j) + "</td> \n";

                    }

                }

                output = output + "    </tr> \n";

            }

            output = output + "</table>";

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(output);
            }

        }

    }
}