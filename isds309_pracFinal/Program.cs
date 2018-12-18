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
            int in_card = 0;
            int in_gas = 0;
            double in_gallons = 0;

            int selectedGasIdx = -1;

            double total_price = 0;
            int check_digit = 0;
            bool error = false;
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
            //----------------------------------------------
            //   MENU-ish
            //----------------------------------------------
            WriteLine("Welcome to ISDS Gas Station");
            WriteLine("---------------------------");
            WriteLine("");
            WriteLine("You have the following choices ...");
            WriteLine("{0,10} {1,10}", "Octane", "Price");
            for ( int i = 0; i < GAS_TYPES; ++i)
            {
                WriteLine("{0,10} {1,10}", gas[i,0], gas [i,1]);
            }
            WriteLine("");
            WriteLine("---------------------------");
            WriteLine("");
            //----------------------------------------------
            // GET INPUT
            //----------------------------------------------
            //octane
            while (selectedGasIdx == -1)
            {
                WriteLine("Please enter your choice of gasoline (octane #): ");
                in_gas = int.Parse(ReadLine());
                //search
                //selectedGasIdx = -1;
                for (int i = 0; i < GAS_TYPES; ++i)
                {
                    if (in_gas == gas[i,0])
                    {
                        selectedGasIdx = i;
                    }
                }
                if (selectedGasIdx == -1)
                    WriteLine("Invalid Octane");
            }
            //card length
            while (in_card_str.Length != 6)
            {
                WriteLine("Please enter your Credit card Number (6 digits): ");
                in_card_str = ReadLine();
            }
            //gallons
            while (in_gallons > 0)
            {
                WriteLine("Please enter amount of gas you need (gallons): ");
                in_gallons = double.Parse(ReadLine());
            }
            WriteLine("---------------------------");
            //----------------------------------------------
            // VALIDATE / CALC
            // TODO INCOMPLETE, 
            //----------------------------------------------
            in_card = int.Parse(in_card_str);
            //WriteLine("DBG:" + (in_card / 1000000)); //DEBUG
            //WriteLine("DBG:" + ((in_card / 10) % 5)); //DEBUG
            check_digit = in_card % 10;
            if ((in_card / 10) % 5 == check_digit)
            {
                switch (in_gas)
                {
                    case 87://reg
//                            total_price = in_gallons * PRICE_REG;
                        break;
                    case 89://plus
                        //total_price = in_gallons * PRICE_PLU;
                        break;
                    case 91://premium
                        //total_price = in_gallons * PRICE_PRE;
                        break;
                    default:
                        WriteLine("Valid gas choices are 87,89,91");
                        error = true;
                        break;
                }
            }
            else
            {
                WriteLine("Your credit card number is incorrect");
                error = true;
            }
            if (!error)
            {
                //----------------------------------------------
                // OUTPUT
                //----------------------------------------------
                Write("\n\n");
                WriteLine("\tThank you for using ISDS Gas Station");
                WriteLine("");
                WriteLine("\tDate: " + DateTime.Now.ToString("M/d/yyyy hh:mm:ss"));
                WriteLine("\tCard Number: {0,10}", "**" + (in_card % 10000));
                switch (in_gas)
                {
                    case 87:
                        //WriteLine("\tRegular (87) {0,10}     {1} gallons", PRICE_REG.ToString("$#.000"), in_gallons);
                        break;
                    case 89:
                        //WriteLine("\tPlus (89)    {0,10}     {1} gallons", PRICE_PLU.ToString("$#.000"), in_gallons);
                        break;
                    case 91:
                        //WriteLine("\tPremium (91) {0,10}     {1} gallons", PRICE_PRE.ToString("$#.000"), in_gallons);
                        break;
                    default:
                        WriteLine("!!how did you get here? error should be caught above!!");
                        break;
                }
                WriteLine("\tTotal Price: {0,10}", total_price.ToString("$#.000"));
            }//error
            Write("\n\n");//end
        }//main
    }
}
