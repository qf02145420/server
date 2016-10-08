using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace CreateUserData
{
    public class IentityCard
    {
        public class Zone
        {
            public string id;
            public string province;
            public string city;
            public string town;
            public Zone(string i, string p, string c, string t)
            {
                id = i;
                province = p;
                city = c;
                town = t;
            }
        }


        public List<Zone> zones = new List<Zone>();
        public Random random = new Random();
        public IentityCard(string filepath)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filepath))
                {
                    String line;
                    string[] linestrs;
                    string id = string.Empty;
                    string province = string.Empty;
                    string city = string.Empty;
                    string town = string.Empty;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        linestrs = line.Split(',');
                        Console.WriteLine(linestrs[1]);
                        if (linestrs[0].Substring(2) == "0000")
                        {
                            province = linestrs[1];
                            
                            continue;
                        }
                        else if (linestrs[0].Substring(4) == "00")
                        {
                            city = linestrs[1];
                            continue;
                        }
                        else
                        {
                            town = linestrs[1];
                            zones.Add(new Zone(linestrs[0], province, city, town));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read : '" + filepath + "'");
                Console.WriteLine(e.Message);
            }
        }


        public string GetCardID(int gender, int age)
        {
            string id = string.Empty;
            string area = string.Empty;
            string birthday = string.Empty;
            string station = string.Empty;
            int genderid = 0;
            string sign = string.Empty;
            int[] wi = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            string[] lastnum = { "1", "0","X", "9", "8", "7", "6", "5", "4", "3", "2"};

            int temp;
            //area
            temp = random.Next(0, zones.Count);
            area = zones[temp].id;

            //birthday
            TimeSpan ts = new TimeSpan(age * 365 + random.Next(0, 365), 0, 0, 0, 0);
            DateTime nw = DateTime.Now;
            birthday = (nw - ts).ToString("yyyyMMdd");

            //station
            int ct = int.Parse(zones[temp].id.PadRight(2));
            
            if (ct < 5)
                station = random.Next(0, 30).ToString("00");
            else
                station = random.Next(0, 50).ToString("00");

            

            //gender
            genderid = random.Next(0, 3);
            if (gender % 2 == 0)
            {
                genderid = genderid * 2;
            }
            else
            {
                genderid = genderid * 2 + 1;
            }

            //sign
            id = area + birthday + station + genderid.ToString();
            Console.WriteLine("id:" + id.ToString());
            char[] idws = id.ToCharArray();
            int sum = 0;
            for (int i = 0; i < wi.Length; i++)
            {
                sum += wi[i] * idws[i];
            }
            sign = lastnum[sum % 11];
            id += sign;

            Console.WriteLine(zones[temp].province + ":" + zones[temp].city + ":" + zones[temp].town);
            return id;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string areaFilePath = "D:/myproject/wcf/SimpleWCF/IdentityCardInfo.csv";
            IentityCard ic = new IentityCard(areaFilePath);
            string id = ic.GetCardID(0, 32);
            Console.WriteLine(id);
            Console.Read();

        }
    }
}
