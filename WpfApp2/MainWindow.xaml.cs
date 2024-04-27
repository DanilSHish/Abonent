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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model mod = new Model();
        private string connectionString = @"Data Source=D:\\C# Projects\\WpfApp2\\WpfApp2\\DataBase\\identifier.sqlite;";
        private SqliteConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connection = new SqliteConnection(connectionString);
            dataGrid.ItemsSource = mod.LoadData();
        }

        private void searchByNumber_Click(object sender, RoutedEventArgs e)
        {
            SearchByNumber search = new SearchByNumber();
            search.ShowDialog();
        }

        private void uploadCSV_Click(object sender, RoutedEventArgs e)
        {
            ItemCollection item = dataGrid.Items;
            bool flag = mod.uploadCSVFile(item);
            if(flag)
                MessageBox.Show($"Данные успешно сохранены в файл.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show($"Произошла ошибка при сохранении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void StreetsButton_Click(object sender, RoutedEventArgs e)
        {
            Streets streets = new Streets();
            streets.ShowDialog();
            //
        }

        private void description_Click(object sender, RoutedEventArgs e)
        {
                MessageBox.Show("Тестовое задание\n" +
                                "Приложение для работы с абонентами телефонной компании\n" +
                                "\n-Основное окно выводит информацию об абонентах в формате таблицы dataGrid." +
                                "Приложение считывает базу данных из папки ..\\WpfApp1\\WpfApp1\\DataBase.\n" +
                                "\n-Кнопка \"Поиск\" - вызов модального окна, в котором при вводе в текстовое" +
                                " поле номера телефона, выводит абонента с совпавшим номером телефона.\n" +
                                "\n-Кнопка  \"Выгрузить CSV\" - формирует файл с названием report_{дата и время}.csv" +
                                " в котором содержится информация из таблицы основного окна. Файл сохраняется в ..\\WpfApp1\\WpfApp1\\report\n" +
                                "\n-Кнопка \"Улицы\" - вызов модального окна, в котором отображается информация" +
                                " об обслуживаемых улицах и количестве абонентов на каждой из них.\n" +
                                "\nДля СУБД используется SQLite.\n" +
                                "\nРазработчик - Шишкин Данил Сергеевич\n" +
                                "\nГород Киров" +
                                "\n2024 год"
                                , "Описание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
