using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interface_de_connexion_lecutre
{
    public partial class Ajoute_plat : Form
    {
        public Ajoute_plat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            emptyRowCell1.Value2 = textBoxCatégorie.Text;

            // Écrire textBoxNom.Text dans la deuxième colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell2 = workSheet.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell2.Value2 = textBoxNom.Text;

            // Écrire textBoxPrix.Text dans la troisième colonne.
            Microsoft.Office.Interop.Excel.Range emptyRowCell3 = workSheet.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range;
            emptyRowCell3.Value2 = textBoxPrix.Text;
            // Avertir l'utilisateur du bonne ajout 
            Label3.Text = "Plat ajouté";
            // Fermez le classeur et l'application Excel.
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

        private void Ajoute_plat_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

