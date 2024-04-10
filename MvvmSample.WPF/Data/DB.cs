using MvvmSample.WPF.Model;
using System.Collections.ObjectModel;

namespace MvvmSample.WPF.Data
{
    public class DB
    {
        public DB() 
        {
            ItemsSource = new ObservableCollection<Person>()
            {
                new Person("Dmytro", "Borodiy", 0),
                new Person("Ivan", "Fedorov", 1),
                new Person("James", "Montemagno", 2),
                new Person("David", "Ortinao", 3)
            };
        }

        private ObservableCollection<Person> ItemsSource { get; set; }

        public ObservableCollection<Person> GetPersons()
        {
            return ItemsSource;
        }
    }
}
