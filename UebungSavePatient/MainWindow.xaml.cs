using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UebungSavePatient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        TextBox FirstName;
        TextBox LastName;
        DatePicker Birthday;
        RadioButton Male;
        RadioButton Female;
        CheckBox cb;
        ComboBox comboBox;

        //List<Patient> Patients;

        ListBox listbox;
        public MainWindow()
        {
            InitializeComponent();
            FirstName = firstnametb;
            LastName = lastnametb;
            Birthday = birthdaydtp;
            Male = maleradiobox;
            Female = femaleradiobox;
            cb = bedwettercheckbox;
            comboBox = Diseases;
            //Patients = new List<Patient>();
            listbox = listboxpatient;
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listbox.SelectedItem != null)
            {
               // Patients.Remove(Patients.Find(x=> x.ToString()==listbox.SelectedItem.ToString()));
                listbox.Items.Remove(listbox.SelectedItem);
            }


        }

      

        private void Button_Click_PushToList(object sender, RoutedEventArgs e)
        {
            DateTime? dateTime = birthdaydtp.SelectedDate;
            if (FirstName == null || LastName==null || Male.IsChecked ==false &&Female.IsChecked==false || dateTime ==null){
                MessageBox.Show("Please fill out the values");
            }
            else
            {
               
                DateOnly date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
               Patient patient = new Patient();
                patient.Firstname = FirstName.Text;
                patient.Lastname = LastName.Text;
                patient.Birthday = date;
                if(Male.IsChecked==true){
                patient.isMale = true;
                }
                else
                {
                    patient.isMale = false;
                }
                patient.isBedwetter=cb.IsChecked ==true;
                listbox.Items.Add(patient);
               // Patients.Add(patient);
                
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string line = "";
            foreach(var item in listbox.Items)
            {
                line += item.ToString()+"\n";

            }
            SaveFileDialog sFD = new SaveFileDialog();
            sFD.Filter = "CSV-Datei (*.csv)|*.csv";
            if (sFD.ShowDialog() == true)
            {
                string filePath = sFD.FileName;

                File.WriteAllText(filePath, line);

            }

           // File.WriteAllText()
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (Patient.TryParse(line, out Patient p))
                        {
                           // Patients.Add(p);
                            listbox.Items.Add(p);
                        }
                    }
                    
                    Console.WriteLine(lines);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while reading the CSV file: {ex.Message}", "CSV File Reading Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            }

        private void listboxpatient_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"An error occurred while reading the CSV file: ");

        }

        private void listboxpatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listbox.SelectedItem != null)
            {
                string value = listbox.SelectedItem.ToString();
                if (Patient.TryParse(value, out Patient p))
                {
                    lastnametb.Text = p.Lastname;
                    firstnametb.Text = p.Firstname;
                    
                }
            }
        }

        private void addDiseadse_Click(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)listbox.SelectedItem;

            p.Diseases.Add((string)Diseases.SelectedItem);
        }
    }
}
