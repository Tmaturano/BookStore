using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace BookStore.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Service _service;
        private BookBinding _book = new BookBinding();        

        public MainWindow()
        {
            InitializeComponent();
            _service = new Service();

            _book.Author = new AuthorBinding();
            spBooks.DataContext = _book;               
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtBookCodeSearch.Text))
                MessageBox.Show("The book code can not be empty!", "Warning", MessageBoxButton.OK);
            else
            {
                _book = _service.GetBookByCode(Convert.ToInt32(txtBookCodeSearch.Text));
                this.spBooks.DataContext = _book;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var response = _service.AddBook(spBooks.DataContext as BookBinding);

            MessageBox.Show(response.StatusCode.ToString());

            if (response.StatusCode == HttpStatusCode.OK)
            {                   
                List<BookBinding> books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookBinding>>(response.Content);


                foreach (var item in books)
                {                   

                    MessageBox.Show(string.Format(" Book Code: {0} \r\n Book Description: {1} \r\n Book Price: {2} \r\n" +
                                    "Author Code: {3} \r\n Author Name: {4}", item.Code, item.Description, item.Price.ToString(),
                                    item.Author.Code, item.Author.Name));
                }
                
            }
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var response = _service.UpdateBook(spBooks.DataContext as BookBinding);

            MessageBox.Show(response.StatusCode.ToString());

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<BookBinding> books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookBinding>>(response.Content);


                foreach (var item in books)
                {

                    MessageBox.Show(string.Format(" Book Code: {0} \r\n Book Description: {1} \r\n Book Price: {2} \r\n " +
                                    "Author Code: {3} \r\n Author Name: {4}", item.Code, item.Description, item.Price.ToString(),
                                    item.Author.Code, item.Author.Name));
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var response = _service.DeleteBook(((BookBinding)spBooks.DataContext).Code);

            MessageBox.Show(response.StatusCode.ToString());

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<BookBinding> books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookBinding>>(response.Content);


                foreach (var item in books)
                {

                    MessageBox.Show(string.Format(" Book Code: {0} \r\n Book Description: {1} \r\n Book Price: {2} \r\n " +
                                    "Author Code: {3} \r\n Author Name: {4}", item.Code, item.Description, item.Price.ToString(),
                                    item.Author.Code, item.Author.Name));
                }

            }
        }
    }
}
