using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interface_de_connexion_lecutre.Plat;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using Application = System.Windows.Forms.Application;

namespace Interface_de_connexion_lecutre
{
    public partial class Client : Form
    {
        private void AddTab(string tabName, UserControl userControl)
        {
            TabPage tabPage = new TabPage(tabName);
            tabPage.Controls.Add(userControl);

            // Ajoutez l'onglet à votre TabControl
            tabControl1.TabPages.Add(tabPage);
        }
        
        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
           
            // Ajouter l'onglet Menu
            Menu menuTab = new Menu();
            menuTab.BackColor = Color.DarkOrange;
            AddTab("Menu", menuTab);

            // Ajouter l'onglet Plat
            Plat platTab = new Plat();
            platTab.BackColor = Color.DarkOrange;
            AddTab("Plat", platTab);

            // Ajouter l'onglet Boisson
            Boisson boissonTab = new Boisson();
            boissonTab.BackColor = Color.DarkOrange;
           AddTab("Boisson", boissonTab);

            // Ajouter l'onglet Dessert
            Dessert dessertTab = new Dessert();
            AddTab("Dessert", dessertTab);
            dessertTab.BackColor = Color.DarkOrange;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void AjouterBoutons()
        {
            
        }
        private void AjouterBoutonsPourIndex(int index)
        {
           
        }
        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }
        private string GetSelectedProductName()
        {
            // Assurez-vous qu'un élément est sélectionné
            if (listBox1.SelectedItem != null)
            {
                // Récupérez le texte de l'élément sélectionné (par exemple, "Nom (quantité) - Prix")
                string selectedItemText = listBox1.SelectedItem.ToString();

                // Vous devez extraire le nom du produit du texte. Vous pouvez utiliser une logique de split ou d'autres méthodes
                // Supposons que le nom du produit est avant la première parenthèse
                int index = selectedItemText.IndexOf("(");
                if (index != -1)
                {
                    // Extrayez le nom du produit
                    string nomProduit = selectedItemText.Substring(0, index).Trim();

                    // Retournez le nom du produit
                    return nomProduit;
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner l'article que vous voulez modifier", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Si aucun élément n'est sélectionné ou si le format est incorrect, retournez une chaîne vide ou null, selon votre logique
            return string.Empty;
        }
        private void PlusButton_Click(object sender, EventArgs e)
        {
            // Votre logique ici
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            
        }

        private void ClearButtons()
        {
           
        }
        public void UpdateListBox(string message)
        {
            // Vérifier si le produit existe déjà dans la ListBox
            bool produitExiste = false;
            foreach (var item in listBox1.Items)
            {
                if (item.ToString().StartsWith(message.Split(' ')[0]))
                {
                    produitExiste = true;
                    break;
                }
            }

            // Ajouter ou mettre à jour le texte dans la listBox1
            if (!produitExiste)
            {
                listBox1.Items.Add(message);
            }
            else
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString().StartsWith(message.Split(' ')[0]))
                    {
                        listBox1.Items[i] = message;
                        break;
                    }
                }
            }
           
        }
        
        private double CalculerPrixTotal()
        {
            
            double prixTotal = 0.0;

            foreach (var item in listBox1.Items)
            {
                // Supposons que le format est "Nom (Quantité) - Prix"
                string[] elements = item.ToString().Split(" - ");
                if (elements.Length == 2)
                {
                    // Extraire la partie du prix et supprimer tout caractère non numérique
                    string prixPart = Regex.Replace(elements[1], @"[^\d.,]+", "");

                    // Utiliser la culture actuelle pour la conversion
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;

                    if (double.TryParse(prixPart, NumberStyles.Any, cultureInfo, out double prixArticle))
                    {
                        prixTotal += prixArticle;
                    }
                }
            }

            return prixTotal;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double prixtotal = CalculerPrixTotal();
            labelprixTotal.Text = prixtotal.ToString("C", CultureInfo.CurrentCulture);




        }
        public static int rest2 = 0;
        public static int drapeau2 = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            string cheminFichierExcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "invoice.xlsx");
            double prixtotal = CalculerPrixTotal();
            Random random = new Random();
            int randomNumber = random.Next(0, 501);
            // Initialiser Excel
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(cheminFichierExcel);
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];
            Excel.Worksheet sheet2 = (Excel.Worksheet)workbook.Sheets[2];
            // Lire le nombre de feuilles existantes dans le classeur
            int sheetCount = workbook.Sheets.Count;

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1]; // Utilisez l'index de la feuille


            // Trouvez la première ligne vide dans la feuille de calcul.
           int row = 1;
           /* while (workSheet.Cells[row, 1].Value != null)
            {
                row++;
            }*/

            // Ajoutez les éléments de la ListBox à la feuille de calcul.
            foreach (var item in listBox1.Items)
            {
                string[] elements = item.ToString().Split(" - ");

                // Remplissez les cellules de la ligne actuelle.
                for (int i = 0; i < elements.Length; i++)
                {
                    workSheet.Cells[row, i + 1].Value = elements[i];
                }

                row++;
            }

            // Dupliquer la feuille 2
            sheet2.Copy(Type.Missing, sheet2);
            Excel.Worksheet newSheet = (Excel.Worksheet)workbook.Sheets[3];//+ 1]; // Utiliser le nombre de feuilles + 1 pour accéder à la nouvelle feuille

            // Renommer la nouvelle feuille avec une date/heure
            newSheet.Name = "NouvelleFacture_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            // Ajouter la date dans la cellule D17 de la nouvelle feuille
            newSheet.Cells[17, 4].Value = DateTime.Now.ToString("dd/MM/yyyy"); //pour un format spécifique

            // Ajouter l'heure dans la cellule F17 de la nouvelle feuille
            newSheet.Cells[17, 6].Value = DateTime.Now.ToString("HH:mm:ss"); //pour un format spécifique

            //Ajoute le numéro de commande           
            newSheet.Cells[16, 5].Value = randomNumber;
           // Compter le nombre de lignes à ajouter après la ligne 22
           int numberOfLinesToAdd = sheet1.UsedRange.Rows.Count ;

            // Ajouter le nombre de lignes nécessaires dans la nouvelle feuille
            for (int i = 0; i < numberOfLinesToAdd; i++)
            {
                newSheet.Rows[23 + i].Insert();
            }

            // Copier les colonnes depuis la feuille 1 vers la nouvelle feuille
            for (int i = 1; i <= sheet1.UsedRange.Rows.Count; i++)
            {
                // Copier la première colonne depuis la feuille 1
                newSheet.Cells[i + 20, 2].Value = sheet1.Cells[i, 1].Value;

                // Copier la deuxième colonne depuis la feuille 1
                newSheet.Cells[i + 20, 8].Value = sheet1.Cells[i, 2].Value;
            }
            // Recherche de la cellule contenant "fct_prix_total"
            Excel.Range prixTotalCell = newSheet.Cells.Find("TOTAL");

            if (prixTotalCell != null)
            {
                // Récupérer les coordonnées de la cellule
                int rowIndex = prixTotalCell.Row+1;
                int columnIndex = prixTotalCell.Column+1;

                // Insérer la valeur de prix_total dans la cellule trouvée
                newSheet.Cells[rowIndex, columnIndex].Value = prixtotal;
            }

            // Supprimer le contenu de la feuille 1
           sheet1.UsedRange.ClearContents();
      


            // Enregistrer et fermer le fichier Excel
            workbook.Save();
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
            
            // Afficher une boîte de dialogue pour confirmer la fermeture de la page
            // Afficher un message de remerciement avec le numéro de commande
            DialogResult result = MessageBox.Show($"Merci de votre commande. Votre numéro de commande est le {randomNumber}. 🐻 Bonne appétit 🐻", "Commande confirmée");
            // Créer une instance de ProcessStartInfo
            ProcessStartInfo startInfo = new ProcessStartInfo(cheminFichierExcel);
            startInfo.UseShellExecute = true;

            // Démarrer le processus
            Process.Start(startInfo);
             if (result == DialogResult.OK)
            {  listBox1.Items.Clear();
                rest2 = 1;
            }
                if (drapeau2 == 4)
            {
                rest2 = 0;
                drapeau2 = 0;
            }

}
private void DuplicateSection(Word.Document doc, string nomProduit, string prix)
        {
            // Nom des signets dans le modèle Word.
            const string produitBookmark = "PRODUIT";
            const string prixBookmark = "PRIX";

            // Vérifiez si les signets existent.
            if (BookmarkExists(doc, produitBookmark) && BookmarkExists(doc, prixBookmark))
            {
                // Récupérez les signets de modèle.
                Word.Bookmark produitBookmarkObj = doc.Bookmarks[produitBookmark];
                Word.Bookmark prixBookmarkObj = doc.Bookmarks[prixBookmark];

                // Dupliquez la ligne de modèle pour chaque produit.
                Word.Range produitRange = produitBookmarkObj.Range;
                Word.Range prixRange = prixBookmarkObj.Range;

                // Insérez les données du produit dans la ligne dupliquée.
                produitRange.Text = nomProduit;
                prixRange.Text = prix;

                // Insérez une nouvelle ligne après la ligne dupliquée.
                produitRange.InsertParagraphAfter();
                prixRange.InsertParagraphAfter();

                // Ajouter les nouvelles lignes au document.
                doc.Bookmarks.Add($"{produitBookmark}_Duplicate", produitRange);
                doc.Bookmarks.Add($"{prixBookmark}_Duplicate", prixRange);
            }
        }

        private bool BookmarkExists(Word.Document doc, string bookmarkName)
        {
            try
            {
                // Essayez d'accéder au signet.
                Word.Bookmark bookmark = doc.Bookmarks[bookmarkName];
                return bookmark != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void FindAndReplace(Word.Document doc, string findText, string replaceText)
        {
            foreach (Word.Range storyRange in doc.StoryRanges)
            {
                Word.Find find = storyRange.Find;
                find.Text = findText;
                find.Replacement.Text = replaceText;

                object replaceAll = Word.WdReplace.wdReplaceAll;
                find.Execute(Replace: replaceAll);
            }
        }


        private void ReplaceTextInWord(Word.Document doc, string placeholder, string replacement)
        {
            foreach (Word.Range range in doc.StoryRanges)
            {
                Word.Find find = range.Find;
                find.Text = placeholder;
                find.Replacement.Text = replacement;

                object replaceAll = Word.WdReplace.wdReplaceAll;
                find.Execute(Replace: replaceAll);
            }
        }
        // Méthode pour ajouter un élément à la ListBox
        public void AjouterElementListBox(string element)
        {
            listBox1.Items.Add(element);
        }
        public void RetirerElementListBox(string element)
        {
            

            // Trouver l'index de l'élément dans la ListBox
            int index = listBox1.Items.IndexOf(element);

            // Si l'élément est trouvé, le supprimer
            if (index != -1)
            {
                listBox1.Items.RemoveAt(index);
            }
            UpdateListBox(element);
        }
        public void MettreAJourListBox(string message)
        {
            
        }
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
       

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
    
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void moinsButton_Click(object sender, EventArgs e)
        {
          
            switch (tabControl1.SelectedTab?.Text)
            {
                case "Dessert":

                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Dessert dessertUserControl = tabControl1.SelectedTab.Controls[0] as Dessert;

                    // Récupérez le nom du produit sélectionné
                    string nomProduitSelectionne = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionne = dessertUserControl.GetProduitByName(nomProduitSelectionne);

                    // Appeler la méthode pour décrémenter la quantité
                    dessertUserControl.DecrementerQuantite(nomProduitSelectionne, produitSelectionne);
                    break;
                case "Plat":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Plat platUserControl = tabControl1.SelectedTab.Controls[0] as Plat;

                    // Récupérez le nom du produit sélectionné
                    string nomProduitSelectionnee = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionnee = platUserControl.GetProduitByName(nomProduitSelectionnee);

                    // Appeler la méthode pour décrémenter la quantité
                    platUserControl.DecrementerQuantite(nomProduitSelectionnee, produitSelectionnee);
                    break;
                case "Boisson":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Boisson boissonUserControl = tabControl1.SelectedTab.Controls[0] as Boisson;

                    // Récupérez le nom du produit sélectionné
                    string nomProduitSelectionn = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionn = boissonUserControl.GetProduitByName(nomProduitSelectionn);

                    // Appeler la méthode pour décrémenter la quantité
                    boissonUserControl.DecrementerQuantite(nomProduitSelectionn, produitSelectionn);
                    break;
                case "Menu":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Menu MenuUserControl = tabControl1.SelectedTab.Controls[0] as Menu;

                    // Récupérez le nom du produit sélectionné
                    string nomProduitSelectione = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectione = MenuUserControl.GetProduitByName(nomProduitSelectione);

                    // Appeler la méthode pour décrémenter la quantité
                    MenuUserControl.DecrementerQuantite(nomProduitSelectione, produitSelectione);
                    break;
            }
        }

        private void plusButton_Click_1(object sender, EventArgs e)
        {
            string nomProduitSelectionne = GetSelectedProductName();
            switch (tabControl1.SelectedTab?.Text)
            {
                case "Dessert":

                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Dessert dessertUserControl = tabControl1.SelectedTab.Controls[0] as Dessert;

                    // Récupérez le nom du produit sélectionné
                    //string nomProduitSelectionne = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionne = dessertUserControl.GetProduitByName(nomProduitSelectionne);

                    // Appeler la méthode pour décrémenter la quantité
                    dessertUserControl.IncrementerQuantite(nomProduitSelectionne, produitSelectionne);
                    break;
                case "Plat":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Plat platUserControl = tabControl1.SelectedTab.Controls[0] as Plat;

                    // Récupérez le nom du produit sélectionné
                    //string nomProduitSelectionnee = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionnee = platUserControl.GetProduitByName(nomProduitSelectionne);

                    // Appeler la méthode pour décrémenter la quantité
                    platUserControl.IncrementerQuantite(nomProduitSelectionne, produitSelectionnee);
                    break;
                case "Boisson":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Boisson boissonUserControl = tabControl1.SelectedTab.Controls[0] as Boisson;

                    // Récupérez le nom du produit sélectionné
                    //string nomProduitSelectionn = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectionn = boissonUserControl.GetProduitByName(nomProduitSelectionne);

                    // Appeler la méthode pour décrémenter la quantité
                    boissonUserControl.IncrementerQuantite(nomProduitSelectionne, produitSelectionn);
                    break;
                case "Menu":
                    // Obtenez l'instance du UserControl Dessert à partir de l'onglet actif
                    Menu MenuUserControl = tabControl1.SelectedTab.Controls[0] as Menu;

                    // Récupérez le nom du produit sélectionné
                    //string nomProduitSelectione = GetSelectedProductName();

                    // Récupérez l'objet Produit correspondant au nom du produit en utilisant la méthode GetProduitByName
                    Produit produitSelectione = MenuUserControl.GetProduitByName(nomProduitSelectionne);

                    // Appeler la méthode pour décrémenter la quantité
                    MenuUserControl.IncrementerQuantite(nomProduitSelectionne, produitSelectione);
                    break;
            }
        }

      

        private void button1_Click_2(object sender, EventArgs e)
        {
            string cheminFichierExcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "invoice.xlsx");

            // Créez une instance d'Excel Application.
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Ouvrez le classeur Excel.
            Workbook workbook = excelApp.Workbooks.Open(cheminFichierExcel);

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1]; // Utilisez l'index de la feuille


            // Trouvez la première ligne vide dans la feuille de calcul.
            int row = 1;
            while (workSheet.Cells[row, 1].Value != null)
            {
                row++;
            }

            // Ajoutez les éléments de la ListBox à la feuille de calcul.
            foreach (var item in listBox1.Items)
            {
                string[] elements = item.ToString().Split(" - ");

                // Remplissez les cellules de la ligne actuelle.
                for (int i = 0; i < elements.Length; i++)
                {
                    workSheet.Cells[row, i + 1].Value = elements[i];
                }

                row++;
            }

            // Enregistrez le classeur Excel.
            workbook.Save();

            // Fermez le classeur et l'application Excel.
            workbook.Close();
            excelApp.Quit();

            // Libérez les objets COM.
            Marshal.ReleaseComObject(workSheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);


        }

        private void buttonPrixtotal_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
        public int reset = 0;
        public int flag = 0;
        // Déclaration d'un événement qui se déclenchera lorsque reset est modifié
        public event EventHandler ResetChanged;

        // Champ privé pour stocker la valeur de reset
        public static int rest = 0;
        public static int drapeau = 0;
        



        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            // Afficher une boîte de dialogue pour confirmer la fermeture de la page
            DialogResult result = MessageBox.Show("Voulez-vous vraiment annuler votre panier 🐻 ?", "Confirmation de fermeture", MessageBoxButtons.OKCancel);

            // Vérifier la réponse de l'utilisateur
            if (result == DialogResult.OK)
            {
                listBox1.Items.Clear();
                rest = 1;
                
            }

            if (drapeau == 4)
            {
                rest = 0;
                drapeau = 0;
            }


        }

        private void labelprixTotal_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBoxmdp.Visible = true;

            if (textBoxmdp.Text == Environment.GetEnvironmentVariable("RESTAURANT_ADMIN_PASSWORD"))
            {
                // Afficher un message de validation
                MessageBox.Show("Mot de passe correct. Accès autorisé ! 🐻", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                {
                    // Afficher un message d'erreur
                    MessageBox.Show("Mot de passe incorrect, veuillez réessayer", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
        }
    }
}
