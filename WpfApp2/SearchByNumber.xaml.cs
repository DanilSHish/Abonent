using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для SearchByNumber.xaml
    /// </summary>
    public partial class SearchByNumber : Window
    {
        Model mod = new Model();
        public SearchByNumber()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = textSearch.Text;
            IEnumerable<Model.Abonent> phone = mod.Search(phoneNumber);

            if (!phone.Any())
            {
                MessageBox.Show("Нет абонентов удовлетворяющих критерию поиска");

            }
            else
            {
                dataGrid.ItemsSource = phone;
            }
        }
    }
}
