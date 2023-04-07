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
using WPFCRUD.VM;

namespace WPFCRUD
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            bibliothequeEntities db = new bibliothequeEntities();

            var result = from d in db.Domaines
                         select new
                         {
                             id = d.id,
                             nom = d.nom,
                             description = d.description

                         };

            this.griddomains.ItemsSource = result.ToList();

        }

        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            bibliothequeEntities db = new bibliothequeEntities();

            Domaine domaineObject = new Domaine()
            {
                nom = nom_tbx.Text,
                description = description_tbx.Text,
            };

            db.Domaines.Add(domaineObject); 
            db.SaveChanges();
        }

        private void load_btn_Click(object sender, RoutedEventArgs e)
        {
            reload_grid();
        }

        private int updatingDomainID = 0;

        private void griddomains_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.griddomains.SelectedIndex >= 0)
            {
                if (this.griddomains.SelectedItems.Count >= 0)
                {

                    object selectedItem = ((DataGrid)sender).SelectedItem;
                    Type type = selectedItem.GetType();


                    string nom = (string)type.GetProperty("nom").GetValue(selectedItem, null);
                    int id = (int)type.GetProperty("id").GetValue(selectedItem, null);
                    string description = (string)type.GetProperty("description").GetValue(selectedItem, null);

                    this.nom_update_tbx.Text = nom;
                    this.description_update_tbx.Text = description;

                    this.updatingDomainID = id;
                }
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to Delete?",
                "Delete Doctor",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

            if (msgBoxResult == MessageBoxResult.Yes)
            {

                bibliothequeEntities db = new bibliothequeEntities();

                var r = from d in db.Domaines
                        where d.id == this.updatingDomainID
                        select d;

                Domaine obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.Domaines.Remove(obj);
                    db.SaveChanges();
                }
            }

            reload_grid()

        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            bibliothequeEntities db = new bibliothequeEntities();

            var r = from d in db.Domaines
                    where d.id == this.updatingDomainID
                    select d;

            Domaine obj = r.SingleOrDefault();

            if (obj != null)
            {
                obj.nom = this.nom_update_tbx.Text;
                obj.description = this.description_update_tbx.Text;

                db.SaveChanges();
            }
            reload_grid()
        }

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            clear_form();
        }

        private void clear_form()
        {
            nom_tbx.Clear();
            description_tbx.Clear();
        }

        private void reload_grid()
        {

            bibliothequeEntities db = new bibliothequeEntities();

            this.griddomains.ItemsSource = db.Domaines.ToList();
        }
    }
}
