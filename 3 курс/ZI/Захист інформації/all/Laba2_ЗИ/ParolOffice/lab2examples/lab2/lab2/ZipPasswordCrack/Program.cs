using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Ionic.Zip;

namespace ZipPasswordCrack
{
    class Program
    {
        static int Main(string[] args)
        {
            // Checking count of input params.
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: ZipPasswordCrack.exe alphabetfile mask zipfile");
                return -1;
            }

            //Console.Write("Input alphabet: ");
            //var alphabet = Console.ReadLine();

            // Reading alphabet.
            var alphabetFile = args[0];
            if (!File.Exists(alphabetFile))
            {
                Console.WriteLine("Alphabet file doesn't exist.");
                return -1;
            }

            var alphabet = File.ReadAllText(alphabetFile);
            if (string.IsNullOrEmpty(alphabet))
            {
                Console.WriteLine("Wrong alphabet.");
                return -1;
            }

            // alphabetLength is used during generation.
            var alphabetLength = alphabet.Length;

            //Console.Write("Input password mask: ");
            //var mask = Console.ReadLine();

            // Reading password mask.
            var mask = args[1];
            if (string.IsNullOrEmpty(alphabet))
            {
                Console.WriteLine("Wrong password mask.");
                return -1;
            }
            Debug.Assert(mask != null, "mask != null");

            //Console.Write("Input zip file path: ");
            //var zip = Console.ReadLine();
            
            // Checking input zip file.
            var zip = args[2];
            if (!File.Exists(zip) || !ZipFile.IsZipFile(zip))
            {
                Console.WriteLine("Wrong zip file.");
                return -1;
            }

            // Maybe there is no password?
            if (ZipFile.CheckZipPassword(zip, ""))
            {
                Console.WriteLine("No password.");
                return -2;
            }

            // Count number of variable chars in mask.
            var passwordLength = mask.Count(c => '?' == c);
            if (0 == passwordLength)
            {
                Console.WriteLine("Constant password.");
                return -1;
            }

            // Create variable part of password for iterating.
            var password = new byte[passwordLength];
            // Result storage.
            var passwordString = new StringBuilder(mask.Length);
            passwordString.Append(mask);

            // Map position of '?' in password array to their's position in result string.
            var passwordMaskMapping = new int[passwordLength];
            var pi = 0;
            for (var i = 0; i < mask.Length; i++)
            {
                if ('?' == mask[i])
                {
                    passwordMaskMapping[pi++] = i;
                }
            }

            // Main iteration.
            while (true)
            {
                // Fill result string with current value of password.
                for (var i = 0; i < passwordLength; i++)
                {
                    passwordString[passwordMaskMapping[i]] = alphabet[password[i]];
                }

                Console.WriteLine("Testing: " + passwordString);

                // Write status when keyboard is hit.
                // Used when stdout is redirected to NUL for gaining more speed.
                if (Console.KeyAvailable)
                {
                    Console.Error.WriteLine("Testing: " + passwordString);

                    do
                    {
                        Console.ReadKey(false);
                    } while (Console.KeyAvailable);
                }

                // Try current password.
                try
                {
                    if (ZipFile.CheckZipPassword(zip, passwordString.ToString()))
                    {
                        Console.WriteLine("Correct password: " + passwordString);
                        Console.Error.WriteLine("Correct password: " + passwordString);
                        break;
                    }
                }
                catch 
                {
                    // Some errors may occur because of wrong password (crc or smth like that).
                    Console.WriteLine("Testing error.");
                }

                // Increment current password.
                if (++password[passwordLength - 1] == alphabetLength)
                {
                    // We have carry - pass it to next digit, zero smallest digit.
                    password[passwordLength - 1] = 0;

                    // Calculate position of next digit to receive carry.
                    var prev = passwordLength - 2;
                    if (prev < 0)
                        break;

                    // Pass carry to next digits.
                    var i = prev;
                    while (i >= 0)
                    {
                        if (++password[i] != alphabetLength)
                        {
                            // If there is no carry - stop.
                            break;
                        }
                        
                        // Carry still exists. Pass it to next digits.
                        password[i] = 0;
                        i--;
                    }

                    if (i < 0)
                    {
                        // We have an overflow - all combinations are iterated.
                        break;
                    }
                }
            }

            return 0;
        }
    }
}
