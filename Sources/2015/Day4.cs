using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Hashing)] //MD5
    public class Day4 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int somma = 0;
            string secretkey = "";
            string numberToAddSecretKey = "";
            /*if (!test)
            {
                secretkey = "ckczppom"; numberToAddSecretKey = "";
            }
            else
            {
                secretkey = "They are deterministic"; numberToAddSecretKey = "609043";
                //secretkey = "abcdef"; numberToAddSecretKey = "609043";
                ///secretkey = "pqrstuv"; numberToAddSecretKey = "1048970";
            }*/
            bool founded = false;
            secretkey = (string)input;
            secretkey = secretkey.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            for (int i = 0; i < 1000000; i++)
            {
                numberToAddSecretKey=i.ToString().PadLeft(6, '0');
                string md5 = CreateMD5(secretkey + numberToAddSecretKey);
                if (md5.StartsWith("00000"))
                {
                    founded= true;
                    solution = i;
                    return;
                }
            }


            #region PREPARE INPUT
            //Converting the data to binary
            var binaryString = ToBinary(ConvertToByteArray(secretkey, Encoding.ASCII));

            //Padding to 448 bits
            int block = binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None).Count() * 8;
            int bit_to_padd = (512 - block) % 512;
            if (448 - 1 - block > 0 && 448 - 1 - block > 7)
            {
                binaryString += " 10000000";

                while (binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None).Count() * 8 < 448)
                {
                    binaryString += " 00000000";
                }
            }
            else
            {

            }

            //Fill with input length in binary
            if (512 - binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None).Count() * 8 == 64)
            {
                string block_binary = Convert.ToInt64(Convert.ToString(block, 2)).ToString("00000000");
                while (binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None).Count() * 8 < 512 - 8)
                {
                    binaryString += " 00000000";
                }
                binaryString += " " + block_binary;
            }

            #endregion

            #region CORE ALGORITHM
            //Split into 32-bit words
            List<string> Words = new List<string>();
            Dictionary<string, string> ExadecimalWords = new Dictionary<string, string>();
            int indice = 0;
            for (int i = 0; i < binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None).Count(); i += 4)
            {

                Words.Add(binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[0 + i] + " "
                    + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1 + i] + " "
                    + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[2 + i] + " "
                    + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[3 + i]);

                string word = (binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[0 + i]
                        + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1 + i]
                        + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[2 + i]
                        + binaryString.Split(Delimiter.delimiter_space, StringSplitOptions.None)[3 + i]);

                ExadecimalWords.Add("M" + indice, BinaryStringToHexString(word));
                indice++;
            }

            //https://www.rapidtables.com/convert/number/binary-to-hex.html
            //https://www.comparitech.com/blog/information-security/md5-algorithm-with-examples/
            //https://adventofcode.com/2015/day/4
            //https://onlinehextools.com/and-hex-numbers
            //https://stackoverflow.com/questions/10972850/how-is-a-hex-value-manipulated-bitwise
            //Initialization vectors
            string A = "01234567";
            string B = "89abcdef"; //2309737967
            string C = "fedcba98"; //4275878552
            string D = "76543210";
            string Z = Convert.ToInt64(Math.Pow(2, 32)).ToString("X");

            //Four Rounds of sixteen operations
            //Round 1 F(B, C, D) = (B∧C)∨(¬B∧D)
            for (int r = 0; r < 16; r++)
            {
                string F = BitWiseHexadecimalOperation(BitWiseHexadecimalOperation(B, "∧", C), "∨", BitWiseHexadecimalOperation(BitWiseHexadecimalOperation(B, "¬"), "∧", D));
                //Modular Addition
                string ma = ModularAddition(ExadecimalWords["M" + r], ModularAddition(A, F, Z), Z);
                //Constant Addition

                string ka = ModularAddition(KValues.K[r], ma, Z);
                //Left-Bit shift
                int p = 0;
                string kab = ConvertFromHexToBinary(ka);
                while ((kab.Length + p) / 4 % 4 != 0) { p++; }
                string ls = Convert.ToInt64(ShiftBit(kab.PadLeft(kab.Length + p, '0'), ShiftValues.RoundOne[r % 4]), 2).ToString("X");
                string fo = ModularAddition(B, ls, Z);
            }

            //Round 2: G(B, C, D) = (B∧D)∨(C∧¬D)
            //Round 3: H(B, C, D) = B⊕C⊕D
            //Round 4: I(B, C, D) = C⊕(B∨¬D)
            #endregion
            string hash = A + B + C + D;

        }
        //101011110100110000100111110000
        //101001100001001111100001010111
        //101001100001001111100001010111
        public string ShiftBit(string string_to_shift, int value_to_shift)//-left + right
        {
            string temp_string = "";
            string result_string = "";

            if (value_to_shift < 0)
            {
                for (int i = -value_to_shift - 1; i >= 0; i--)
                {
                    temp_string = string_to_shift[i] + temp_string;
                }
                for (int i = -value_to_shift; i < string_to_shift.Length; i++)
                {
                    result_string += string_to_shift[i];
                }

                return result_string + temp_string;
            }
            else
            {
                for (int i = string_to_shift.Length - value_to_shift; i < string_to_shift.Length; i++)
                {
                    temp_string += string_to_shift[i];
                }
                result_string = temp_string;
                for (int i = 0; i < string_to_shift.Length - value_to_shift; i++)
                {
                    result_string += string_to_shift[i];
                }

                return result_string;
            }
        }


        public string ConvertFromHexToBinary(string hex)
        {
            long dec = Convert.ToInt64(hex, 16);
            string bin = "";
            while (dec != 0)
            {
                bin = dec % 2 + bin;
                dec = dec / 2;
            }
            return bin;
        }
        public string ModularAddition(string X, string Y, string Z)
        {
            return (((Convert.ToInt64(X, 16) + Convert.ToInt64(Y, 16)) % (Convert.ToInt64(Z, 16))).ToString("X"));
        }
        public string BitWiseHexadecimalOperation(string first, string operation, string second = null)
        {
            switch (operation)
            {
                //AND
                case "∧":
                    return (Convert.ToInt64(first, 16) & Convert.ToInt64(second, 16)).ToString("X");
                //OR
                case "∨":
                    return (Convert.ToInt64(first, 16) | Convert.ToInt64(second, 16)).ToString("X");
                //XOR
                case "⊕":
                    return (Convert.ToInt64(first, 16) ^ Convert.ToInt64(second, 16)).ToString("X");
                //NOT
                case "¬":
                    long dec = Convert.ToInt64(first, 16);
                    string bin = "";
                    while (dec != 0)
                    {
                        bin = dec % 2 + bin;
                        dec = dec / 2;
                    }
                    bin = bin.Replace("0", "2").Replace("1", "3").Replace("2", "1").Replace("3", "0");
                    return BinaryStringToHexString(bin);
            }
            return "";

        }
        //        hex 76543210
        //        dec 1985229328
        //        bin 1110110010101000011001000010000
        //    NOT bin 0001001101010111100110111101111
        //        hex 9ABCDEF

        public static class ShiftValues
        {
            public static int[] RoundOne = new int[4] { 7, 12, 17, 22 };
            public static int[] RoundTwo = new int[4] { 5, 9, 14, 20 };
            public static int[] RoundThree = new int[4] { 4, 11, 16, 13 };
            public static int[] RoundFour = new int[4] { 6, 10, 15, 21 };
        }
        public static class KValues
        {
            public static string[] K = new string[64]{"D76AA478","8C7B756",
            "242070DB"
            ,"C1BDCEEE"
            ,"F57COFA"
            ,"4787C62A"
            ,"A8304613"
            ,"FD469501"
            ,"698098D8"
            , "8B44F7AF"
            , "FFFF5BB1"
            , "895CD7BE"
            , "6B901122"
            , "FD987193"
            , "A679438E"
            , "49B40821"
            , "F61E2562"
            , "C040B340"
            , "265E5A51"
            , "E9B6C7AA"
            , "D62F105D"
            , "02441453"
            , "D8A1E681"
            , "E7D3FBC8"
            , "21E1CDE6"
            , "C33707D6"
            , "F4D50D87"
            , "455A14ED"
            , "A9E3E905"
            , "FCEFA3F8"
            , "676F02D9"
            , "8D2A4C8A"
            , "FFFA3942"
            , "8771F681"
            , "699D6122"
            , "FDE5380C"
            , "A4BEEA44"
            , "4BDECFA9"
            , "F6BB4B60"
            , "BEBFBC70"
            , "289B7EC6"
            , "EAA127FA"
            , "D4EF3085"
            , "04881D05"
            , "D9D4D039"
            , "E6DB99E5"
            , "1FA27CF8"
            , "C4AC5665"
            , "F4292244"
            , "432AFF97"
            , "AB9423A7"
            , "FC93A039"
            , "655B59C3"
            , "8F0CCC92"
            , "FFEFF47D"
            , "85845DD1"
            , "6FA87E4F"
            , "FE2CE6E0"
            , "A3014314"
            , "4E0811A1"
            , "F7537E82"
            , "BD3AF235"
            , "2AD7D2BB"
            , "EB86D391"};
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return  HexadecimalEncoding.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }
        // Use any sort of encoding you like. 
        public static String ToExadecimal(Byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 16)));
        }
        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }


        public void Part2(object input, bool test, ref object solution)
        {
            int somma = 0;
            string secretkey = "";
            string numberToAddSecretKey = "";
            /*if (!test)
            {
                secretkey = "ckczppom"; numberToAddSecretKey = "";
            }
            else
            {
                secretkey = "They are deterministic"; numberToAddSecretKey = "609043";
                //secretkey = "abcdef"; numberToAddSecretKey = "609043";
                ///secretkey = "pqrstuv"; numberToAddSecretKey = "1048970";
            }*/
            bool founded = false;
            secretkey = (string)input;
            secretkey = secretkey.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            try
            {

            for (int i = 0; i < 10000000; i++)
            {
                numberToAddSecretKey = i.ToString().PadLeft(6, '0');
                string md5 = CreateMD5(secretkey + numberToAddSecretKey);
                   if(i%100000==0) Console.WriteLine(i);
                if (md5.StartsWith("000000"))
                {
                    founded = true;
                    solution = i;
                    return;
                }
                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
