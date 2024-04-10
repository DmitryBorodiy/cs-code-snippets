using MvvmSample.WPF.Model;
using MvvmSample.WPF.ViewModel;
using System.Windows;
using Wpf.Ui.Controls;

namespace MvvmSample.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public IViewModel ViewModelContext
    {
        get { return (IViewModel)this.DataContext; }
        set { this.DataContext = value; }
    }

    private void SearchBoxUI_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        PersonViewModel viewModel = (PersonViewModel)ViewModelContext;

        if(!String.IsNullOrEmpty(sender.Text) && 
           viewModel.Items != null &&
           viewModel.FindItems != null)
        {
            viewModel.Search(sender.Text);
            sender.OriginalItemsSource = viewModel.FindItems.SourceCollection
                  .Cast<Person>()
                  .ToArray();
        }
    }
}