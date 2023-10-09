using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace UebungSavePatient
{
    class Patient
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly Birthday { get; set; }
        public bool isMale { get; set; }
        public bool isBedwetter { get; set; }

        public List<string> Diseases { get; set; } = new List<string>();

        public override string? ToString()
        {
            string gender = "";
            if (isMale)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            string bedwettertext = "";
            if (isBedwetter)
            {
                bedwettertext = "bedwetter";
            }
            else
            {
                bedwettertext = "No bedwetter";
            }
            string diseases ="";
            int counter = 0;
            foreach (var d in Diseases)
            {
                if
                diseases += d + ", ";
            }

            return $"{Firstname} {Lastname} {Birthday} [{gender}] - {bedwettertext} {{{diseases}}}";
        }

        public static bool TryParse(string s, out Patient p)
        {
            string[] parts =s.Split(' ');
            p = new Patient();

            if (parts.Length >=5 )
            {
                p.Firstname = parts[0];
                p.Lastname = parts[1];
                string[]dateParts = parts[2].Split(".");
                if (dateParts.Length == 3)
                {
                    int day = int.Parse(dateParts[0]);
                    int month = int.Parse(dateParts[1]);
                    int year = int.Parse(dateParts[2]);
                    
                    p.Birthday = new DateOnly(year, month, day);

                    string gender = parts[3].Trim('[', ']');
                    if (gender == "Male")
                    {
                        p.isMale = true;
                    }
                    else
                    {
                        p.isMale= false;
                    }
                    
                    string bedwetterText = parts[5];
                    if(bedwetterText == "bedwetter")
                    {
                        p.isBedwetter = true;
                    }
                    else
                    {
                        p.isBedwetter = false;
                    }
                    return true;

                  }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
            
        }
    }
}
