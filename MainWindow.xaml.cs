using MOS_Management.API.Controllers;
using MOS_Management.API.Models;
using MOS_Management.API.RepositoryInterface;
using MOS_Management.Models.TypeDonnées.Complexes;
using MOS_Management.Models.TypeDonnées.Complexes.Complexes_;
using MOS_Management.Wpf.Pages;
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
using System.Xaml;

namespace MOS_Management.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAgenceRepository agenceRepository;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly ICodeRepository codeRepository;


        private const string TITRE_CODE = "M.O.S: GESTION DES CODES";
        private const string TITRE_IDENTIFIANT = "M.O.S: GESTION DES IDENTIFIANTS";

        private const string BTN_LATERAL_GAUCHE_CODE = "Codes";
        private const string BTN_LATERAL_GAUCHE_IDENTIFIANT = "Identifiants";

        private const string TABITEM_AGENCE = "Agence";

        private const string TABITEM_NOMENCLATURE = "Nomenclature";
        private const string TABITEM_CODE = "Code";
        private const string TABITEM_CODES = "Codes";

        private const string TABITEM_SYSTEM = "Système";
        private const string TABITEM_IDENTIFIANT = "Identifiant";
        private const string TABITEM_IDENTIFIANTS = "Identifiants";

        private List<Agence> agences = new List<Agence>();
        private List<Nomenclature> nomenclatures = new List<Nomenclature>();
        private List<MosSystem> mosSystems = new List<MosSystem>();


        public MainWindow(IAgenceRepository _agenceRepository, INomenclatureRepository _nomenclatureRepository, ICodeRepository _codeRepository)
        {
            InitializeComponent();
            agenceRepository = _agenceRepository;
            nomenclatureRepository = _nomenclatureRepository;
            codeRepository = _codeRepository;


            btnCode.Content = BTN_LATERAL_GAUCHE_CODE;
            btnIdentifiant.Content = BTN_LATERAL_GAUCHE_IDENTIFIANT;

            tabItemAgenceCode.Header = TABITEM_AGENCE;
            tabItemNomenclature.Header = TABITEM_NOMENCLATURE;
            tabItemCode.Header = TABITEM_CODE;
            tabItemSystem.Header = TABITEM_SYSTEM;
            tabItemIdentiifant.Header = TABITEM_IDENTIFIANT;
            tabItemIdentiifants.Header = TABITEM_IDENTIFIANTS;
            tabItemCodes.Header = TABITEM_CODES;

            labelTitre.Content = TITRE_CODE;

            foreach (Control u in stackpanelBas.Children)
            {
                SetControlButtonBas(u);
            }


            foreach (Control u in stackpanelGauche.Children)
            {
                SetControlButtonLateralGauche(u);
            }

            SetControlButtonBas(buttonGestion);
            buttonGestion.Width = 120;
            buttonGestion.Height = 60;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            agences = agenceRepository.GetAgences_();
            nomenclatures = nomenclatureRepository.GetNomenclatures_();
            comboBoxAgenceCode_0.ItemsSource = agences;
            comboBoxAgenceCode_1.ItemsSource = agences;
            comboBoxNomenclatureCode.ItemsSource = nomenclatures;
            comboBoxNomenclatureGestion.ItemsSource = nomenclatures;
        }

        internal void BtnLateralGauche_Click(object sender, RoutedEventArgs e)
        {
            
            Button b = (Button)sender;
            string s = b.Content.ToString();
            switch (s)
            {
                case BTN_LATERAL_GAUCHE_CODE:
                    comboBoxAgenceCode_0.ItemsSource = null;
                    agences = agenceRepository.GetAgences_();
                    comboBoxAgenceCode_0.ItemsSource = agences;
                    labelTitre.Content = TITRE_CODE;
                    break;
                case BTN_LATERAL_GAUCHE_IDENTIFIANT:
                    labelTitre.Content = TITRE_IDENTIFIANT;
                    break;
            }

            new GestionGrille().GridVisibilty(mainGrid, stackpanelGauche.Children.IndexOf((UIElement)sender));

        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            string s = labelTitre.Content.ToString();
            switch (s)
            {
                case TITRE_CODE:
                    switch (GetTabHeaderSelected(tabControlCode))
                    {
                        case TABITEM_AGENCE:
                            Agence a = new Agence();
                            a.Acronyme = textboxAgenceCodeAcronyme.Text.Trim();
                            a.Label = textboxAgenceCodeLable.Text.Trim();
                            agenceRepository.AddAgence_(a);
                            break;
                        case TABITEM_NOMENCLATURE:
                            Nomenclature nomenclature = new Nomenclature();
                            nomenclature.Nom = textboxNomenclatureNom.Text.Trim();
                            nomenclature.Uri.Label = textboxNomenclatureUri.Text.Trim();
                            nomenclature.Version = textboxNomenclatureVersion.Text.Trim();
                            nomenclature.AgenceId = GetAgenceId(comboBoxAgenceCode_1);
                            nomenclatureRepository.AddNomenclature_(nomenclature);
                            break;
                        case TABITEM_CODE:
                            Code code = new Code();
                            code.Valeur = textboxCode.Text.Trim();
                            code.Affichage = textboxAffichage.Text.Trim();
                            //code.Langue.Code.Valeur = GetAgenceId(comboBoxLangue);
                            code.NomenclatureId = GetNomenclatureId(comboBoxNomenclatureCode);
                            codeRepository.AddCode_(code);
                            break;
                    }
                    break;
                case TITRE_IDENTIFIANT:
                    switch (GetTabHeaderSelected(tabControlIdentifiant))
                    {
                        case TABITEM_AGENCE:
                            break;
                        case TABITEM_SYSTEM:
                            break;
                        case TABITEM_IDENTIFIANT:
                            break;
                    }
                    break;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = labelTitre.Content.ToString();
            switch (s)
            {
                case TITRE_CODE:
                    switch (GetTabHeaderSelected(tabControlCode))
                    {
                        case TABITEM_AGENCE:

                            break;
                        case TABITEM_NOMENCLATURE:
                          

                            break;
                        case TABITEM_CODE:
                            //dataGridCode.ItemsSource = codeRepository.GetCodes_(GetNomenclatureId(comboBoxNomenclatureCodes));
                            break;
                    }
                    break;
                case TITRE_IDENTIFIANT:
                    switch (GetTabHeaderSelected(tabControlIdentifiant))
                    {
                        case TABITEM_AGENCE:
                            break;
                        case TABITEM_SYSTEM:
                            break;
                        case TABITEM_IDENTIFIANT:
                            break;
                    }
                    break;
            }
        }



        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            string s = labelTitre.Content.ToString();
            switch (s)
            {
                case TITRE_CODE:
                    switch (GetTabHeaderSelected(tabControlCode))
                    {
                        case TABITEM_AGENCE:
                            btnValider.IsEnabled = true;
                            break;

                        case TABITEM_NOMENCLATURE:
                            btnValider.IsEnabled = true;
                            break;
                        case TABITEM_CODE:
                            btnValider.IsEnabled = true;
                            break;
                        case TABITEM_CODES:
                            btnValider.IsEnabled = false;
                            break;
                    }
                    break;
                case TITRE_IDENTIFIANT:
                    switch (GetTabHeaderSelected(tabControlIdentifiant))
                    {
                        case TABITEM_AGENCE:

                            break;
                        case TABITEM_SYSTEM:
                            break;
                        case TABITEM_IDENTIFIANT:
                            break;
                        case TABITEM_IDENTIFIANTS:
                            btnValider.IsEnabled = false;
                            break;
                    }
                    break;
            }
        }
        /**************************/
        //METHODES PERSONNELS
        /**************************/
        private string GetTabHeaderSelected(TabControl tabControl)
        {
            string header = "";
            if(tabControl.Items.Count >0)
            {
                foreach (TabItem ti in tabControl.Items)
                {
                    if (ti.IsSelected == true)
                    {
                        header = ti.Header.ToString();
                    }
                }


            }
            return header;
        }

        private void SetControlButtonLateralGauche(Control u)
        {
            u.Width = 80;
            u.Height = 40;
            u.Margin = new Thickness(3, 10, 3, 10);
            u.BorderThickness = new Thickness(2);
            u.VerticalContentAlignment = VerticalAlignment.Center;
            u.HorizontalContentAlignment = HorizontalAlignment.Center;
        }

        private void SetControlButtonBas(Control u)
        {
            SetControlButtonLateralGauche(u);
            u.Margin = new Thickness(10);
          
        }
        private string GetAgenceId(ComboBox c)
        {
            Agence a = (Agence)c.SelectedItem;
            return a.AgenceId;
        }
        private string GetNomenclatureId(ComboBox c)
        {
            Nomenclature a = (Nomenclature)c.SelectedItem;
            return a.NomenclatureId;
        }

        private string GetLangueId(ComboBox c)
        {
            Code a = (Code)c.SelectedItem;
            return a.CodeId;
        }

        List<Code> codes = new List<Code>();
        private void ButtonGestion_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxNomenclatureGestion.SelectedIndex < 0)
            {
                MessageBox.Show("Vous devez sémectionner une nomenclature");
            }
            else
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                //liste

                List<string> listes = new List<String>();
                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    string[] array = System.IO.File.ReadAllLines(filename);
                    // string[] array = new string[] { "frFrance", "fr-CHFrance(Suisse)" };
                    for (int i = 0; i < array.Length; ++i)
                    {
                        String s = array[i];
                        String[] mots = s.Split(";");
                        Code code = new Code();
                        code.Valeur = mots[0].Trim();
                        code.Affichage = mots[1].Trim();
                        code.AffichageCourt = mots[2].Trim();
                        code.AffichageLong = mots[3].Trim();
                        code.NomenclatureId = GetNomenclatureId(comboBoxNomenclatureGestion);
                        //code.Langue.Code.Valeur = "fr";
                        codes.Add(code);

                    }

                    dataGridGestion.ItemsSource = codes;


                }

            }
        }

        private void ButtonInsertion_Click(object sender, RoutedEventArgs e)
        {
            foreach(Code c in codes)
            {
                codeRepository.AddCode_(c);
            }
        }

        
    }
}
