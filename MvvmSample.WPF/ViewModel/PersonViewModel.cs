using System.Linq;
using MvvmSample.WPF.Data;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using MvvmSample.WPF.Model;
using System.Windows.Controls;
using System.Reflection.Emit;
using System.Windows.Input;

namespace MvvmSample.WPF.ViewModel
{
    class PersonViewModel : DependencyObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Inits ViewModel with persons collection for search.
        /// </summary>
        public PersonViewModel() 
        {
            DBContext = new DB();
            Items = CollectionViewSource.GetDefaultView
            (
                DBContext.GetPersons()
            );
        }

        private DB DBContext { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand MyCommand { get; private set; }

        #region FilterTextProp
        /// <summary>
        /// Filter text for search box.
        /// </summary>
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); Search(value); NotifyPropertyChanged("FilterText"); }
        }

        private static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(PersonViewModel), new PropertyMetadata(String.Empty, FilterText_Changed));
        #endregion

        #region Items
        /// <summary>
        /// Search items data collection.
        /// </summary>
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        private static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(PersonViewModel), new PropertyMetadata(null));
        #endregion

        #region FindItems
        /// <summary>
        /// Search items data collection.
        /// </summary>
        public ICollectionView FindItems
        {
            get { return (ICollectionView)GetValue(FindItemsProperty); }
            set { SetValue(FindItemsProperty, value); NotifyPropertyChanged("FindItems"); }
        }

        private static readonly DependencyProperty FindItemsProperty =
            DependencyProperty.Register("FindItems", typeof(ICollectionView), typeof(PersonViewModel), new PropertyMetadata(null));
        #endregion

        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region SearchTextChangedCommand
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = d as PersonViewModel;
            string query = (string)e.NewValue;

            if(!String.IsNullOrEmpty(query) && viewModel != null)
            {
                var search = viewModel.Items.SourceCollection
                                  .Cast<Person>()
                                  .Where(x => x.FirstName.Contains(query) ||
                                  x.LastName.Contains(query));

                viewModel.FindItems = CollectionViewSource.GetDefaultView
                (
                    search
                );
            }
        }

        public void Search(string query)
        {
            if(!String.IsNullOrEmpty(query))
            {
                var search = this.Items.SourceCollection
                                  .Cast<Person>()
                                  .Where(x => x.FirstName.Contains(query) ||
                                  x.LastName.Contains(query));

                this.FindItems = CollectionViewSource.GetDefaultView
                (
                    search
                );
            }
        }
        #endregion
    }
}
