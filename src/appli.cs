using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataTable = System.Data.DataTable;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace Interface_de_connexion_lecutre
{
    public partial class appli : Form
    {
     
        public appli()
        {
            InitializeComponent();
        }

        private void appli_Load(object sender, EventArgs e)
        {
            DataTable exceldata = LoadExcelData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));
            // Associez le DataTable au DataGridView.
            dataGridView1.DataSource = exceldata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Créez une instance d'Excel Application.
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Ouvrez le classeur Excel.
            Workbook workbook = excelApp.Workbooks.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;
            int row = 1;

            // Trouver la première ligne vide.
            while (!string.IsNullOrEmpty((workSheet.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range)?.Value2?.ToString()))
            {
                row++;
            }

            // Écrire TextBoxCatégorie.Text dans la première colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell1 = workSheet.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell1.Value2 = comboBoxCatégorie.Text;

            // Écrire textBoxNom.Text dans la deuxième colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell2 = workSheet.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell2.Value2 = textBoxNom.Text;

            // Écrire textBoxPrix.Text dans la troisième colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell3 = workSheet.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell3.Value2 = textBoxPrix.Text;
            // Écrire textBoxPrix.Text dans la quatrième colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell4 = workSheet.Cells[row, 4] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell3.Value2 = textBoxURL.Text;

            // Avertir l'utilisateur du bonne ajout 

            MessageBox.Show("Le plat a été ajouté", "confirmation de l'ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);

           
            // Effacez les données existantes dans le DataGridView.
            dataGridView1.DataSource = null;



            // Accédez à la feuille de travail active (ActiveSheet).
            workSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelApp.ActiveSheet;
            // Accédez à la feuille de travail active (ActiveSheet).

            // Créez un DataTable pour stocker les données.
            System.Data.DataTable dataTable = new System.Data.DataTable();

            // Ajoutez des colonnes au DataTable pour correspondre aux données lues.
            for (int column = 1; column <= 2; column++)
            {
                dataTable.Columns.Add("Colonne " + column);
            }

            int rows = 1;
            bool hasData = true;

            // Parcourez les lignes jusqu'à ce qu'une ligne vide soit atteinte.
            while (hasData)
            {
                DataRow dataRow = dataTable.NewRow();
                hasData = false; // Supposons que la ligne est vide au départ.

                for (int column = 1; column <= 2; column++)
                {
                    Microsoft.Office.Interop.Excel.Range cell = workSheet.Cells[rows, column] as Microsoft.Office.Interop.Excel.Range;
                    if (cell != null)
                    {
                        string cellValue = cell.Value2?.ToString();
                        dataRow[column - 1] = cellValue;

                        // Vérifiez si la cellule contient des données.
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            hasData = true;
                        }
                    }
                }

                if (hasData)
                {
                    dataTable.Rows.Add(dataRow);
                }

                rows++;
            }

            // Associez le DataTable au DataGridView.
            dataGridView1.DataSource = dataTable;

            workbook.Close();
            excelApp.Quit();

            // Libérez les ressources.
            System.Runtime.InteropServices.Marshal.ReleaseComObject(emptyRowCell1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(emptyRowCell2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(emptyRowCell3);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void SupprimerPlatDansExcel(string nomProduitASupprimer)
        {
            // Chemin du fichier Excel
            string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx");

            // Créez une instance d'Excel Application.
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Ouvrez le classeur Excel.
            Workbook workbook = excelApp.Workbooks.Open(cheminFichier);

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

            // Trouver la ligne correspondant au nom du produit à supprimer.
            int rowCount = workSheet.UsedRange.Rows.Count;
            int rowToDelete = -1;


            for (int i = 2; i <= rowCount; i++)
            {
                string produitNom = (workSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range)?.Value?.ToString();
                if (produitNom == nomProduitASupprimer)
                {
                    rowToDelete = i;
                    break; // Arrêter la recherche une fois la ligne trouvée.
                }
            }

            if (rowToDelete != -1)
            {
                // Supprimer la ligne trouvée.
                Microsoft.Office.Interop.Excel.Range rowRange = workSheet.Rows[rowToDelete] as Microsoft.Office.Interop.Excel.Range;
                rowRange.Delete();

                // Fermer le classeur Excel avec sauvegarde des modifications.
                workbook.Close(true);
                MessageBox.Show("Produit supprimé ");
            }
            else
            {
                // Avertissement : Le produit n'a pas été trouvé.
                MessageBox.Show("Produit non trouvé. Aucune modification effectuée.");
            }

            // Quitter l'application Excel.
            excelApp.Quit();

            // Libérer les ressources.
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(rowRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            string nomProduitASupprimer = comboBoxProduit.Text;
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce plat ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Vérifier la réponse de l'utilisateur
            if (result == DialogResult.Yes)
            {
                // L'utilisateur a cliqué sur "Oui", supprimer le plat
                SupprimerPlatDansExcel(nomProduitASupprimer);
            }
            else
            {
                // L'utilisateur a cliqué sur "Non", aucune action requise
            }
            DataTable exceldata = LoadExcelData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));
            // Associez le DataTable au DataGridView.
            dataGridView1.DataSource = exceldata;

            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxCaté_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lorsque la catégorie est sélectionnée, filtrez les produits en conséquence
            string selectedCategory = comboBoxCaté.SelectedItem?.ToString();

            // Créez une nouvelle DataTable pour stocker les données filtrées
            DataTable filteredTable = new DataTable();

            // Ajoutez des colonnes à la DataTable filtrée
            filteredTable.Columns.Add("Nom");
            filteredTable.Columns.Add("Categorie");

            // Vérifiez si la catégorie sélectionnée n'est pas vide
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                // Créez une instance d'Excel Application.
                var excelApp = new Microsoft.Office.Interop.Excel.Application();

                // Ouvrez le classeur Excel.
                Workbook workbook = excelApp.Workbooks.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));

                // Accédez à la feuille de travail active (ActiveSheet).
                Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

                // Créez une DataTable pour stocker les données.
                DataTable dataTable = new DataTable();

                // Ajoutez des colonnes au DataTable pour correspondre aux données lues.
                for (int column = 1; column <= 2; column++)
                {
                    dataTable.Columns.Add("Colonne " + column);
                }

                int row = 1;
                bool hasData = true;

                // Parcourez les lignes jusqu'à ce qu'une ligne vide soit atteinte.
                while (hasData)
                {
                    DataRow dataRow = dataTable.NewRow();
                    hasData = false; // Supposons que la ligne est vide au départ.

                    for (int column = 1; column <= 2; column++)
                    {
                        Microsoft.Office.Interop.Excel.Range cell = workSheet.Cells[row, column] as Microsoft.Office.Interop.Excel.Range;
                        if (cell != null)
                        {
                            string cellValue = cell.Value2?.ToString();
                            dataRow[column - 1] = cellValue;

                            // Vérifiez si la cellule contient des données.
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                hasData = true;
                            }
                        }
                    }

                    if (hasData)
                    {
                        dataTable.Rows.Add(dataRow);
                    }

                    row++;
                }

                // Filtrer les produits en fonction de la catégorie sélectionnée
                var filteredProducts = dataTable.AsEnumerable()
                    .Where(row => row.Field<string>("Colonne 1") == selectedCategory)
                    .Select(row => row.Field<string>("Colonne 2"))
                    .ToList();

                // Ajouter les produits filtrés à la DataTable filtrée
                foreach (var productName in filteredProducts)
                {
                    filteredTable.Rows.Add(productName, selectedCategory);
                }

                // Remplir la ComboBox des produits avec les produits filtrés
                comboBoxProduit.DataSource = filteredTable;
                comboBoxProduit.DisplayMember = "Nom"; // Définir la colonne d'affichage

                // Fermer le classeur et l'application Excel.
                workbook.Close();
                excelApp.Quit();

                // Libérer les ressources.
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }
        private DataTable LoadExcelData(string filePath)
        {
            // Créez une instance d'Excel Application.
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Ouvrez le classeur Excel.
            Workbook workbook = excelApp.Workbooks.Open(filePath);

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

            // Créez un DataTable pour stocker les données.
            DataTable dataTable = new DataTable();

            // Ajoutez des colonnes au DataTable pour correspondre aux données lues.
            for (int column = 1; column <= 2; column++)
            {
                dataTable.Columns.Add("Colonne " + column);
            }

            int row = 1;
            bool hasData = true;

            // Parcourez les lignes jusqu'à ce qu'une ligne vide soit atteinte.
            while (hasData)
            {
                DataRow dataRow = dataTable.NewRow();
                hasData = false; // Supposons que la ligne est vide au départ.

                for (int column = 1; column <= 2; column++)
                {
                    Microsoft.Office.Interop.Excel.Range cell = workSheet.Cells[row, column] as Microsoft.Office.Interop.Excel.Range;
                    if (cell != null)
                    {
                        string cellValue = cell.Value2?.ToString();
                        dataRow[column - 1] = cellValue;

                        // Vérifiez si la cellule contient des données.
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            hasData = true;
                        }
                    }
                }

                if (hasData)
                {
                    dataTable.Rows.Add(dataRow);
                }

                row++;
            }

            // Fermez le classeur et l'application Excel.
            workbook.Close();
            excelApp.Quit();

            // Libérez les ressources.
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            return dataTable;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
