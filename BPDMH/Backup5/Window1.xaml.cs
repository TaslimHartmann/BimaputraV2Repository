using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace GroupingDataGridWPF
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            ObservableCollection<Employee> empData = new ObservableCollection<Employee> 
            {
                new Employee{Name="Diptimaya Patra", Contact="0000", 
                    EmailID="diptimaya.patra@some.com", Country="India"},
                new Employee{Name="Dhananjay Kumar", Contact="00020", 
                    EmailID="dhananjay.kumar@some.com", Country="India"},
                new Employee{Name="David Paul", Contact="1230", 
                    EmailID="david.paul@some.com", Country="India"},
                new Employee{Name="Christina Joy", Contact="1980", 
                    EmailID="christina.joy@some.com", Country="UK"},
                new Employee{Name="Hiro Nakamura", Contact="0000", 
                    EmailID="hiro.nakamura@some.com", Country="Japan"},
                new Employee{Name="Angela Patrelli", Contact="0000", 
                    EmailID="angela.patrelli@some.com", Country="Japan"},
                new Employee{Name="Zoran White", Contact="0000", 
                    EmailID="diptimaya.patra@some.com", Country="Scotland"},
            };

            ListCollectionView collection = new ListCollectionView(empData);
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Country"));
            dgData.ItemsSource = collection;
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string EmailID { get; set; }
        public string Country { get; set; }
    }
}


