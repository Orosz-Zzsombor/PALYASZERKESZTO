using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace k
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> lista = new List<string>();
        string fajlNev;



        public MainWindow()
        {
            InitializeComponent();
        }

        private void FajlBetoltes(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.ShowDialog();
            fajlNev = FileDialog.FileName;
            
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                lista.Add(sr.ReadLine());
            }

            lbMegjelenit.ItemsSource = lista;

        }

        private void btnOsvenyFelvetel_Click(object sender, RoutedEventArgs e)
        {
            lista.Add(txtSzerkeszto.Text.ToUpper());
            lbMegjelenit.Items.Refresh();
        }

        int szerkesztoIndex = 0;
        private void Szerkeszto(object sender, MouseButtonEventArgs e)
        {
            txtSzerkeszto.Text = lbMegjelenit.SelectedItem.ToString().ToUpper().Replace(" ", "");

            szerkesztoIndex = lbMegjelenit.SelectedIndex;
        }

        private void txtEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.M:
                case Key.V:
                case Key.E:
                case Key.CapsLock:
                case Key.LeftShift:
                  
                    break;
                case Key.Enter:
                    lista[szerkesztoIndex] = txtSzerkeszto.Text.ToUpper().Replace(" ", "");
                    lbMegjelenit.Items.Refresh();
                    txtSzerkeszto.Clear();
                    MessageBox.Show("Sikeres Módosítás");
                    break;
                default:

                    MessageBox.Show("Csak M, E vagy V karakterek engedélyezettek!");
                    e.Handled = true;
                    break;
            }
        }

        private void btnPalyaMentes_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(fajlNev);
            foreach (var item in lista)
            {
                sw.WriteLine(item);
            }
            MessageBox.Show("Sikeres Mentés");
            sw.Close();
        }

    }
}