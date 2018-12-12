// ISDS 309 - Fall 2018 - Sec 80 - Midterm 1 - Gas Pump
// Program: Gas Pump
// Date:    Oct 16, 2018
// Author:  Barnes, Patrick
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace isds309_pracFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            const double PRICE_REG = 3.359;
            const double PRICE_PLU = 3.459;
            const double PRICE_PRE = 3.559;

            string in_card_str;
            int in_card = 0;
            int in_gas = 0;
            double in_gallons = 0;

            double total_price = 0;
            int check_digit = 0;
            bool error = false;
            //----------------------------------------------
            //   MENU-ish
            //----------------------------------------------
            WriteLine("Welcome to ISDS Gas Station");
            WriteLine("---------------------------");
            WriteLine("");
            WriteLine("You have the following choices ...");
            WriteLine("   Regular (87)    ${0}", PRICE_REG);
            WriteLine("      Plus (89)    ${0}", PRICE_PLU);
            WriteLine("   Premium (91)    ${0}", PRICE_PRE);
            WriteLine("");
            WriteLine("---------------------------");
            WriteLine("");
            //----------------------------------------------
            // GET INPUT
            //----------------------------------------------
            WriteLine("Please enter your choice of gasoline (87,89,91): ");
            in_gas = int.Parse(ReadLine());
            WriteLine("Please enter your Credit card Number (6 digits): ");
            in_card_str = ReadLine();
            WriteLine("Please enter amount of gas you need (gallons): ");
            in_gallons = double.Parse(ReadLine());
            WriteLine("---------------------------");
            //----------------------------------------------
            // VALIDATE / CALC
            //----------------------------------------------
            if (in_card_str.Length == 6) //6 digits
            {
                in_card = int.Parse(in_card_str);
                //WriteLine("DBG:" + (in_card / 1000000)); //DEBUG
                //WriteLine("DBG:" + ((in_card / 10) % 5)); //DEBUG
                check_digit = in_card % 10;
                if ((in_card / 10) % 5 == check_digit)
                {
                    switch (in_gas)
                    {
                        case 87://reg
                            total_price = in_gallons * PRICE_REG;
                            break;
                        case 89://plus
                            total_price = in_gallons * PRICE_PLU;
                            break;
                        case 91://premium
                            total_price = in_gallons * PRICE_PRE;
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
            }
            else
            {
                WriteLine("Your credit card number should be 6 digits");
                error = true;
            }//6digits
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
                        WriteLine("\tRegular (87) {0,10}     {1} gallons", PRICE_REG.ToString("$#.000"), in_gallons);
                        break;
                    case 89:
                        WriteLine("\tPlus (89)    {0,10}     {1} gallons", PRICE_PLU.ToString("$#.000"), in_gallons);
                        break;
                    case 91:
                        WriteLine("\tPremium (91) {0,10}     {1} gallons", PRICE_PRE.ToString("$#.000"), in_gallons);
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
