using Interface_de_connexion_lecutre;
using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interface_de_connexion_lecutre.Plat;
using static System.Windows.Forms.DataFormats;
using Button = System.Windows.Forms.Button;
using Font = System.Drawing.Font;
using Label = System.Windows.Forms.Label;

namespace Interface_de_connexion_lecutre
{

    public partial class Dessert : UserControl
    {
        // Liste pour stocker tous les produits
        private List<Produit> tousLesProduits;
        public Dessert()
        {
            InitializeComponent();
            // Initialisez la liste de tous les produits ici
            tousLesProduits = GetProduitsFromExcel(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));
           
        }
        // Méthode pour obtenir un objet Produit à partir du nom
        public Produit GetProduitByName(string nomProduit)
        {
            // Recherchez le produit dans la liste de tous les produits
            Produit produit = tousLesProduits.FirstOrDefault(p => p.Nom == nomProduit);

            return produit;
        }

        private Dictionary<string, int> nombreAppuisParProduit = new Dictionary<string, int>();

        private void Dessert_Load(object sender, EventArgs e)
        {
            
              string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx");

            // Lisez les données depuis Excel
            List<Produit> produits = GetProduitsFromExcel(cheminFichier);

            // Filtrer les produits par catégorie "Dessert"
            List<Produit> desserts = produits
                .Where(p => p.Categorie.Trim() == "Dessert")
                .GroupBy(p => p.Nom) // Grouper par nom pour obtenir des éléments uniques
                .Select(g => g.First()) // Prendre le premier élément de chaque groupe
                .ToList();

            // Afficher les boutons pour chaque plat
            foreach (Produit dessert in desserts)
            {
                Button button = new Button();
                
                // Charger l'image depuis l'URL stockée dans la quatrième colonne
                if (!string.IsNullOrEmpty(dessert.Image))
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            byte[] data = webClient.DownloadData(dessert.Image);
                            using (MemoryStream ms = new MemoryStream(data))
                            {
                                button.BackgroundImage = Image.FromStream(ms);
                                button.BackgroundImageLayout = ImageLayout.Stretch;  // Ajuste l'image à la taille du bouton
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors du chargement de l'image : {ex.Message}");
                    }
                }

                // Ajouter le nom et le prix dans la partie basse du bouton
               // button.Text = $"{dessert.Nom}{Environment.NewLine}{dessert.Prix:C}";
                button.TextAlign = ContentAlignment.BottomCenter;  // Positionner le texte en bas et au centre
                button.Font = new Font (button.Font.FontFamily, 6);  // Ajuster la taille de la police
                button.Click += (s, ev) => Button_Click(dessert);
                button.Width = 100;
                button.Height = 75;

               
                flowLayoutPanelDessert.Controls.Add(button);
                //button.FlatStyle = FlatStyle.Popup;
                // Créer un label pour afficher le nom et le prix
                Label label = new Label();
                label.Text = $"{dessert.Nom}\n{dessert.Prix:C}";
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Height = 60;
                label.ForeColor = Color.White;
                
                // Ajuster la taille de la police pour s'adapter au texte
                FitText(label);

                // Ajouter le label au flowLayoutPanelDessert
                flowLayoutPanelDessert.Controls.Add(label);

                // Ajouter de l'espace entre le bouton et le label
               // flowLayoutPanelDessert.SetFlowBreak(label, true);
            }
            // Définir la direction du flux du layout panel en bas
            flowLayoutPanelDessert.FlowDirection = FlowDirection.TopDown;

        }
        private void FitText(Label label)
        {
            int desiredHeight = label.Height;  // Hauteur souhaitée du label
            int currentHeight = int.MaxValue;  // Hauteur actuelle du label (initialisée à une valeur élevée pour la première itération)

            while (currentHeight > desiredHeight)
            {
                // Réduire progressivement la taille de la police jusqu'à ce que la hauteur souhaitée soit atteinte
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - 1, FontStyle.Bold);
                SizeF size = TextRenderer.MeasureText(label.Text, label.Font);
                currentHeight = (int)size.Height;
            }
        }

        private void Button_Click(Produit dessert)
        {

            if (Client.rest == 1)
            {
                nombreAppuisParProduit[dessert.Nom] = 0;
                Client.drapeau++;

            }
            if (Client.rest2 == 1)
            {
                nombreAppuisParProduit[dessert.Nom] = 0;

                Client.drapeau2++;
            }
            // Vérifier si le produit existe dans le dictionnaire
            if (nombreAppuisParProduit.ContainsKey(dessert.Nom))
            {
                // Incrémenter le nombre d'appuis pour ce produit
                nombreAppuisParProduit[dessert.Nom]++;
            }
            else
            {
                // Ajouter le produit au dictionnaire avec 1 appui
                nombreAppuisParProduit.Add(dessert.Nom, 1);
            }

            // Rechercher le contrôle parent de type Client
            Client client = FindForm() as Client;

            // Calculer le prix total pour ce produit
            double prixTotal = dessert.Prix * nombreAppuisParProduit[dessert.Nom];

            // Mise à jour du texte de la ListBox dans la classe du client
            if (client != null)
            {
                // Construire le texte avec le nombre d'appuis et le prix total
                string texte = $"{dessert.Nom} ({nombreAppuisParProduit[dessert.Nom]}) - {prixTotal:C}";
                client.UpdateListBox(texte);
            }



        }
        private List<Produit> GetProduitsFromExcel(string cheminFichier)
        {

            List<Produit> produits = new List<Produit>();

            // Créez une instance d'Excel Application.
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Ouvrez le classeur Excel.
            Workbook workbook = excelApp.Workbooks.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "products.xlsx"));

            // Accédez à la feuille de travail active (ActiveSheet).
            Microsoft.Office.Interop.Excel._Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

            int rowCount = workSheet.UsedRange.Rows.Count;

            for (int i = 2; i <= rowCount; i++)
            {
                Produit produit = new Produit
                {
                    Categorie = (workSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range)?.Value?.ToString(),
                    Nom = (workSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range)?.Value?.ToString(),
                    Prix = Convert.ToDouble((workSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range)?.Value),
                    Image = (workSheet.Cells[i, 4] as Microsoft.Office.Interop.Excel.Range)?.Value?.ToString(),
                };

                produits.Add(produit);
            }

            // Fermez le classeur et l'application Excel.
            workbook.Close();
            excelApp.Quit();

            // Libérez les ressources.
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            return produits;
        }
        public void DecrementerQuantite(string produit , Produit dessert)
        {
            // Rechercher le contrôle parent de type Client
            Client client = FindForm() as Client;
            if (nombreAppuisParProduit.ContainsKey(produit) && nombreAppuisParProduit[produit] >= 1)
            {
                nombreAppuisParProduit[produit]--;
                // Calculer le prix total pour ce produit
                double prixTotal = dessert.Prix * nombreAppuisParProduit[dessert.Nom];
                // Construire le texte avec le nombre d'appuis et le prix total
                string texte = $"{produit} ({nombreAppuisParProduit[produit]}) - {prixTotal:C}";

                // Appeler la méthode pour mettre à jour la ListBox avec le message
                client.UpdateListBox(texte);

            }
 


        }
        public void IncrementerQuantite(string produit, Produit dessert)
        {
            // Rechercher le contrôle parent de type Client
            Client client = FindForm() as Client;
            if (nombreAppuisParProduit.ContainsKey(produit) && nombreAppuisParProduit[produit] >= 0)
            {
                nombreAppuisParProduit[produit]++;
                // Calculer le prix total pour ce produit
                double prixTotal = dessert.Prix * nombreAppuisParProduit[dessert.Nom];
                // Construire le texte avec le nombre d'appuis et le prix total
                string texte = $"{produit} ({nombreAppuisParProduit[produit]}) - {prixTotal:C}";

                // Appeler la méthode pour mettre à jour la ListBox avec le message
                client.UpdateListBox(texte);
            }
        }

        private void flowLayoutPanelDessert_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
