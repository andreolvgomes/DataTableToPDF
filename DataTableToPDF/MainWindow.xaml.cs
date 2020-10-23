using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using DataTableToPDF.PDF;

namespace DataTableToPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void LoadDatas()
        {
            dgv.ItemsSource = GetDatatable(this.txtQuery.Text).DefaultView;
        }

        public DataTable GetDatatable(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection cnn = new SqlConnection(this.txtCnn.Text))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        adp.Fill(table);
                }
            }
            return table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (PDFDataTable topdf = new PDFDataTable())
                topdf.OpenPdf((dgv.ItemsSource as DataView).Table, dgv.Columns);
        }

        private void CarregaClick(object sender, RoutedEventArgs e)
        {
            this.LoadDatas();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFilter.Text))
                (dgv.ItemsSource as DataView).RowFilter = string.Empty;
            else
                (dgv.ItemsSource as DataView).RowFilter = $"Pro_codigo like '{this.txtFilter.Text}%'";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.txtCnn.Text.NullOrEmpty())
            {
                MessageBox.Show("Informe a ConnectionString", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.txtCnn.Text))
                    cnn.Open();

                grdMain.IsEnabled = true;
                MessageBox.Show("Conexão Ok!!!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}