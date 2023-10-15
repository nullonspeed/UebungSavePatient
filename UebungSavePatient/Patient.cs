using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace UebungSavePatient
{
    class Patient
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public bool isMale { get; set; }
        public bool isBedwetter { get; set; }

        public List<string> Diseases { get; set; } = new List<string>();


        public string ToCSV()
        {
            string diseases ="";
            int counter = 0;
            if (Diseases.Count != 0)
            {
                diseases = ";";
                foreach (var d in Diseases)
                {
                    counter++;
                    if (counter == Diseases.Count)
                    {
                        diseases += d;
                    }
                    else
                    {
                        diseases += d + ";";
                    }


                }
            }
           


            return $"{Firstname};{Lastname};{Birthday.ToShortDateString()};{isMale};{isBedwetter}{diseases}";
        }
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
                counter++;
                if (counter==Diseases.Count)
                {
                    diseases += d ;
                }
                else
                {
                    diseases += d + ", ";
                }
                
                
            }

            return $"{Firstname} {Lastname} {Birthday.ToShortDateString()} [{gender}] - {bedwettertext} "+"{"+  diseases +"}";
        }


        public static bool TryParse(string s, out Patient p)
        {
            try
            {

           
            string[] parts =s.Split(';');
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
                    
                    p.Birthday = new DateTime(year, month, day);

                    string gender = parts[3];
                    if (Boolean.Parse(gender.ToLower()) == true) 
                    {
                        p.isMale = true;
                    }
                    else
                    {
                        p.isMale= false;
                    }
                    
                    string bedwetterText = parts[4];
                    if(Boolean.Parse( bedwetterText.ToLower()) == true)
                    {
                        p.isBedwetter = true;
                    }
                    else
                    {
                        p.isBedwetter = false;
                    }

                    if(parts.Length >= 6){
                            int temporaryCounter = parts.Length - 5;
                            int temporaryCounterfromZerso = 0;
                            while (temporaryCounterfromZerso!=temporaryCounter)
                            {
                                
                                    p.Diseases.Add(parts[5 + temporaryCounterfromZerso]);
                                    temporaryCounterfromZerso++;
                                
                                
                                
                            }


                    }
                    else
                    {

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
            catch (Exception e)
            {
                p = null;
                return false;
            }

        }

    }
}
