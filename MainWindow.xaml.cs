using MOS_Management.API.Models;
using MOS_Management.Wpf.Utilitaire;
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

namespace MOS_Management.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MOS_Communes_DbContext dataContext;

        private const string  TITRE_CODE = "M.O.S: GESTION DES CODES";
        private const string TITRE_IDENTIFIANT = "M.O.S: GESTION DES IDENTIFIANTS";



        public MainWindow(MOS_Communes_DbContext dataContext)
        {
            InitializeComponent();
            this.dataContext = dataContext;
        }

        internal void BtnLateralGauche_Click(object sender, RoutedEventArgs e)
        {
            
            Button b = (Button)sender;
            string s = b.Content.ToString();
            switch (s)
            {
                case "Codes":
                    labelTitre.Content = TITRE_CODE;
                    break;
                case "Identifiants":
                    labelTitre.Content = TITRE_IDENTIFIANT;
                    break;
            }

            new GestionGrille().GridVisibilty(mainGrid, stackpanelGauche.Children.IndexOf((UIElement)sender));

        }



        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
