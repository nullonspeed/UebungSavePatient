using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
        // public ObservableCollection<Patient> PatientList { get; set; } = new ObservableCollection<Patient>();
        StackPanel stackPanel;

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
            stackPanel = panelOne;
            //Patient p1 = new Patient() {Birthday = DateTime.Now, Firstname ="fn1", isBedwetter=true, Lastname ="nn" , isMale = false};
            //Patient p2 = new Patient() { Birthday = DateTime.Now, Firstname = "fn2", isBedwetter = true, Lastname = "nn1", isMale = false };
            //Patient p3 = new Patient() { Birthday = DateTime.Now, Firstname = "fn3", isBedwetter = true, Lastname = "nn2", isMale = false };


            //listbox.Items.Add(p1);
            //listbox.Items.Add(p2);
            //listbox.Items.Add(p3);



        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listbox.SelectedItem != null)
            {
                // Patients.Remove(Patients.Find(x=> x.ToString()==listbox.SelectedItem.ToString()));
                stackPanel.Children.RemoveAt(listbox.SelectedIndex);
                listbox.Items.Remove(listbox.SelectedItem);
                
            }


        }

      

        private void Button_Click_PushToList(object sender, RoutedEventArgs e)
        {
            DateTime? dateTime = birthdaydtp.SelectedDate;
            if (FirstName.Text == null || FirstName.Text == "" || LastName.Text == "" || LastName==null || Male.IsChecked ==false &&Female.IsChecked==false || dateTime ==null){
                MessageBox.Show("Please fill out the values");
            }
            else
            {
               
               // DateOnly date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
               Patient patient = new Patient();
                patient.Firstname = FirstName.Text;
                patient.Lastname = LastName.Text;
                patient.Birthday = (DateTime)birthdaydtp.SelectedDate;
                if(Male.IsChecked==true){
                patient.isMale = true;
                }
                else
                {
                    patient.isMale = false;
                }
                patient.isBedwetter=cb.IsChecked ==true;
                Label personLabel = new Label();
                
                personLabel.Background =Brushes.Red;
                personLabel.Content = $"{patient.Firstname} {patient.Lastname}";
                personLabel.Margin = new Thickness(0, 5, 0, 5);
                stackPanel.Children.Add(personLabel);
                listbox.Items.Add(patient);
               // Patients.Add(patient);
                
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string line = "";
            foreach(var item in listbox.Items)
            {
                Patient p = (Patient)item;
                line += p.ToCSV()+"\n";

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
            int logcounter = 0;
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    listbox.Items.Clear();
                    stackPanel.Children.Clear();
                    foreach (string line in lines)
                    {
                        if (Patient.TryParse(line, out Patient p))
                        {
                            // Patients.Add(p);
                            Label personLabel = new Label();

                            personLabel.Background = Brushes.Red;
                            personLabel.Content = $"{p.Firstname} {p.Lastname}";
                            personLabel.Margin = new Thickness(0, 5, 0, 5);
                            stackPanel.Children.Add(personLabel);
                            listbox.Items.Add(p);
                            
                        }
                        else
                        {
                            logcounter++;
                        }
                    }
                    
                    Console.WriteLine(lines);
                    if (logcounter!=0)
                    {
                        MessageBox.Show(logcounter+" lines are Depricated, please check the file the layout does not match");
                    }
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
                Patient p = (Patient)listbox.SelectedItem;
               
                    lastnametb.Text = p.Lastname;
                    firstnametb.Text = p.Firstname;
                    DateTime dt = p.Birthday;
                    birthdaydtp.SelectedDate = dt;
                    cb.IsChecked = p.isBedwetter;
                    if(p.isMale)
                    {
                        Male.IsChecked = true;
                    }
                    else
                    {
                        Female.IsChecked = true;
                    }
                   

                
            }
        }

        private void addDiseadse_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                Patient p = (Patient)listbox.SelectedItem;

                p.Diseases.Add(Diseases.Text);

                    listbox.SelectedItem = p;
                listbox.Items.Refresh();
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listbox.Items.Clear();
            stackPanel.Children.Clear();
        }
    }
}
