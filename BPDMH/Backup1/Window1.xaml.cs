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
using System.ComponentModel;
using Microsoft.Windows.Controls;
using System.Data.Linq;
using System.Collections;
using System.Collections.Specialized;

namespace BlogPost
{
  public partial class Order : INotifyPropertyChanging, INotifyPropertyChanged
  {
    public void ForcePropertyChanged(string property)
    {
      SendPropertyChanged(property);
    }
  }
  public partial class Window1 : Window, INotifyPropertyChanged
  {
    public Window1()
    {
      InitializeComponent();

      this.Loaded += OnLoaded;
    }
    public string Country 
    {
      get
      {
        return (country);
      }
      set
      {
        country = value;
        Requery();
        FirePropertyChanged("Country");
      }
    }
    void OnLoaded(object sender, RoutedEventArgs e)
    {
      // Note, letting the orders lazily load.
      dataContext = new NorthwindDataContext();
      txtCountry.DataContext = this;
    }
    void Requery()
    {
      customersGrid.DataContext = dataContext.Customers.Where(c => c.Country == txtCountry.Text);
      ICollectionView colView = CollectionViewSource.GetDefaultView(customersGrid.DataContext);
      colView.CollectionChanged += OnCustomerListModified;
    }
    void OnCustomerListModified(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (object o in e.OldItems)
        {
          Customer c = o as Customer;

          if ((c != null) && (c.Orders != null))
          {
            dataContext.Orders.DeleteAllOnSubmit(c.Orders);
          }
        }
      }
    }
    void OnCustomersGridRowInsert(object sender, InitializingNewItemEventArgs e)
    {
      Customer c = e.NewItem as Customer;

      if (c != null)
      {
        c.Country = txtCountry.Text;
      }
    }
    void OnOrdersGridRowInsert(object sender, InitializingNewItemEventArgs e)
    {
      Order o = e.NewItem as Order;

      if (o != null)
      {
        o.PropertyChanged += OnOrderPropertyChanged;
      }
    }
    void OnOrderPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Customer")
      {
        Order o = sender as Order;

        if (o != null)
        {
          o.ForcePropertyChanged("CustomerID");
          o.PropertyChanged -= OnOrderPropertyChanged;
        }
      }
    }
    void FirePropertyChanged(string property)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }
    void OnSubmitChanges(object sender, EventArgs args)
    {
      // This needs error handling in the real world.
      dataContext.SubmitChanges();      
    }
    NorthwindDataContext dataContext;
    string country;

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
