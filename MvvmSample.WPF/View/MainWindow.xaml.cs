using MvvmSample.WPF.Data;
using MvvmSample.WPF.Model;
using MvvmSample.WPF.ViewModel;
using System.Diagnostics;
using System.Windows;
using Wpf.Ui.Controls;

namespace MvvmSample.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.Loaded += MainWindow_Loaded;

        DBContext = new DB();
        SearchBoxUI.OriginalItemsSource = DBContext.GetPersons();
    }

    private DB DBContext { get; set; }
    private void WindowInit()
    {
        try
        {
            //WindowBackgroundManager.UpdateBackground
            //(
            //    this,
            //    ApplicationTheme.Dark,
            //    WindowBackdropType.Mica,
            //    false
            //);

            this.Title = "MVVM Sample";
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message + ex.Source, this.GetType().Name);
        }
    }

    private void ViewInit() => this.DataContext = new PersonViewModel();

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        WindowInit();
        ViewInit();
    }

    private void SearchBoxUI_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        PersonViewModel vm = (PersonViewModel)this.DataContext;
        vm.Search(sender.Text);

        sender.OriginalItemsSource = vm.FindItems.SourceCollection.Cast<Person>().ToList();
    }
}