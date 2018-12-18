// ISDS 309 - Fall 2018 - Sec 80
// Program: Gas Pump modify for final
// Date:    
// Author:  Barnes, Patrick
// NOTE: WITHOUT classes, this version will use multi-dim array
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace isds309_pracFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            const int GAS_TYPES = 3;
            const string gasPath = "gas.txt";
            double[,] gas = new double[GAS_TYPES, 2];
            string file_line = "";
            string[] file_split;

            const string logPath = "log.txt";

            string in_card_str = "";
            int in_card = -1;
            int in_gas = -1;
            double in_gallons = -1;

            int selectedGasIdx = -1;

            double total_price = -1;
            int check_digit = -1;
            bool exit = false;
            //----------------------------------------------
            //   Read Prices from file
            //----------------------------------------------
            FileStream gasFile = new FileStream(gasPath, FileMode.Open, FileAccess.Read);
            StreamReader gasReader = new StreamReader(gasFile);
            file_line = gasReader.ReadLine();

            int gas_itr = 0; //only used here
            while (file_line != null && gas_itr <= GAS_TYPES)
            {
                file_split = file_line.Split(',');
                gas[gas_itr, 0] = int.Parse(file_split[0]);
                gas[gas_itr, 1] = double.Parse(file_split[1]);

                //end
                ++gas_itr;
                file_line = gasReader.ReadLine();
            }
            gasReader.Close();
            gasFile.Close();
            //open log / append
            FileStream logFile = new FileStream(logPath, FileMode.Append, FileAccess.Write);
            StreamWriter logWriter = new StreamWriter(logFile);
            while (!exit)
            {
                //-----------------------
                //RESET INPUT
                in_card_str = "";
                in_card = -1;
                in_gas = -1;
                in_gallons = -1;
                selectedGasIdx = -1;
                total_price = -1;
                check_digit = -1;
                //----------------------------------------------
                //   MENU-ish
                //----------------------------------------------
                WriteLine("Welcome to ISDS Gas Station");
                WriteLine("---------------------------");
                WriteLine("");
                WriteLine("You have the following choices ...");
                WriteLine("{0,10} {1,10}", "Octane", "Price");
                for (int i = 0; i < GAS_TYPES; ++i)
                {
                    WriteLine("{0,10} {1,10}", gas[i, 0], gas[i, 1]);
                }
                WriteLine("");
                WriteLine("---------------------------");
                WriteLine("");
                //----------------------------------------------
                // GET INPUT
                //----------------------------------------------
                WriteLine("Enter 999 to exit anytime");
                //octane
                while (selectedGasIdx == -1 && !exit)
                {
                    WriteLine("Please enter your choice of gasoline (octane #): ");
                    in_gas = int.Parse(ReadLine());

                    if (in_gas == 999)
                    {
                        exit = true;
                        break;
                    }
                    //search
                    //selectedGasIdx = -1;
                    for (int i = 0; i < GAS_TYPES; ++i)
                    {
                        if (in_gas == gas[i, 0])
                        {
                            selectedGasIdx = i;
                        }
                    }
                    if (selectedGasIdx == -1)
                        WriteLine("Invalid Octane");
                }
                //card length
                while (in_card_str.Length != 6 && !exit)
                {
                    WriteLine("Please enter your Credit card Number (6 digits): ");
                    in_card_str = ReadLine();
                    if (in_card_str == "999")
                    {
                        exit = true;
                        break;
                    }
                    else
                    {
                        in_card = int.Parse(in_card_str);
                        //WriteLine("DBG:" + (in_card / 1000000)); //DEBUG
                        //WriteLine("DBG:" + ((in_card / 10) % 5)); //DEBUG
                        check_digit = in_card % 10;
                        if ((in_card / 10) % 5 == check_digit)
                        {
                            //WriteLine("DBG: CARD VALID"); //DEBUG
                            //CARD IS VALID
                        }
                        else
                        {
                            WriteLine("Your credit card number is incorrect");
                            in_card_str = ""; //reset it... cheap hacky way to force loop w/o decl a new var
                        }
                    }
                }
                //gallons
                while (in_gallons <= 0 && !exit)
                {
                    WriteLine("Please enter amount of gas you need (gallons): ");
                    in_gallons = double.Parse(ReadLine());
                    if (in_gallons == 999)
                    {
                        exit = true;
                        break;
                    }
                }
                WriteLine("---------------------------");
                if (!exit)
                {
                    //----------------------------------------------
                    // CALC / OUTPUT
                    //----------------------------------------------
                    //calc
                    DateTime timeStamp = DateTime.Now; //consistent timestamp
                    total_price = in_gallons * gas[selectedGasIdx, 1];
                    
                    //in reality... all of the datetime should be saved here, then reused to ensure same timestamp!!!!
                    //output
                    Write("\n\n");
                    WriteLine("\tThank you for using ISDS Gas Station");
                    WriteLine("");
                    WriteLine("\t       Date: " + timeStamp.ToString("M/d/yyyy hh:mm:ss"));
                    WriteLine("\tCard Number: {0,10}", "**" + (in_card % 10000));
                    WriteLine("\t     Octane: {0,10}", gas[selectedGasIdx,0]);
                    WriteLine("\t  Gas Price: {0,10}  {1} gallons", gas[selectedGasIdx, 1].ToString("$#.000"), in_gallons);
                    WriteLine("\tTotal Price: {0,10}", total_price.ToString("$#.000"));

                    //save to receipt
                    string receiptPath = timeStamp.ToString("Mdyyyyhhmmss") + "_" + (in_card % 10000) + ".txt";
                    FileStream receiptFile = new FileStream(receiptPath, FileMode.Create, FileAccess.Write);
                    StreamWriter receiptWriter = new StreamWriter(receiptFile);
                    //copy paste above
                    receiptWriter.WriteLine("\tThank you for using ISDS Gas Station");
                    receiptWriter.WriteLine("");
                    receiptWriter.WriteLine("\t       Date: " + timeStamp.ToString("M/d/yyyy hh:mm:ss"));
                    receiptWriter.WriteLine("\tCard Number: {0,10}", "**" + (in_card % 10000));
                    receiptWriter.WriteLine("\t     Octane: {0,10}", gas[selectedGasIdx, 0]);
                    receiptWriter.WriteLine("\t  Gas Price: {0,10}  {1} gallons", gas[selectedGasIdx, 1].ToString("$#.000"), in_gallons);
                    receiptWriter.WriteLine("\tTotal Price: {0,10}", total_price.ToString("$#.000"));
                    receiptWriter.Close();
                    receiptFile.Close();

                    //save to log
                    logWriter.WriteLine(    timeStamp.ToString("o") + ',' + 
                                            (in_card % 10000) + ',' + 
                                            gas[selectedGasIdx, 0] + ',' + 
                                            in_gallons + ',' +
                                            gas[selectedGasIdx, 1] + ',' +
                                            total_price);
                }
                Write("\n\n");//end
            }//while (!exit)
            //-----close log------
            logWriter.Close();
            logFile.Close();
        }//main
    }
}
