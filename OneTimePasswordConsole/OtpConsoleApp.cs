using System;

namespace VE.OneTimePassword.UI
{
    class OtpConsoleApp
    {
        static void Main(string[] args)
        {
            String login;
            String password;
            OtpControler otpController = new OtpControler();

            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter an argument.");
            }

            else if (args.Length == 2)
            {
                if (args[0] == "-c")
                {
                    login = args[1];
                    try
                    {
                        password = otpController.CreatePassword(login);
                        Console.WriteLine("Created Password: " + password);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            else if (args.Length == 3)
            {
                if (args[0] == "-v")
                {
                    login = args[1];
                    try
                    {
                        if (otpController.ValidatePassword(args[1], args[2]))
                        {
                            Console.WriteLine("Password " + args[2] + " is valid for user " + args[1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Password");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Parameters");
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
