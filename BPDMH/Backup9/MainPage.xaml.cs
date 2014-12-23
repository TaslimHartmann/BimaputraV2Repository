using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GridViewRowDetailsTemplateSample
{

    public partial class MainPage : UserControl
    {
        List<Employee> employeeList = new List<Employee>()
        {
            new Employee(){ EmpID = 1, Name="Bob", Address=" NY", Project =  new List<string>(){"Alpha", "Beta"}},
            new Employee(){ EmpID = 2, Name="Smith", Address="LA", Project = new List<string>(){"Ray", "Ultra"}},
            new Employee(){ EmpID = 3, Name="John", Address="CAN", Project = new List<string>(){"Silverlight", "VS 2010"}},
            new Employee(){ EmpID = 4, Name="Rob", Address="UK", Project = new List<string>(){"HTML5", "CSS 3"}},
            new Employee(){ EmpID = 5, Name="Mick", Address="NY", Project = new List<string>(){"WPF", "WCF"}},
        };
        public MainPage()
        {
            InitializeComponent();
            this.grdVwDetails.ItemsSource = employeeList;
        }

        private void HandleExpandCollapseForRow(object sender, RoutedEventArgs e)
        {
            Button expandCollapseButton = (Button)sender;
            DataGridRow selectedRow = DataGridRow.GetRowContainingElement(expandCollapseButton);

            if (null != expandCollapseButton && "+" == expandCollapseButton.Content.ToString())
            {
                selectedRow.DetailsVisibility = Visibility.Visible;
                expandCollapseButton.Content = "-";
            }
            else
            {
                selectedRow.DetailsVisibility = Visibility.Collapsed;
                expandCollapseButton.Content = "+";
            }

        }
    }

    public class Employee
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> Project { get; set; }
    }

    public class Projects
    {
        public List<string> ProjectName { get; set; }
    }
}
