namespace Interface_de_connexion_lecutre
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string? expectedPassword = Environment.GetEnvironmentVariable("RESTAURANT_ADMIN_PASSWORD");
            if (textBoxUsername.Text == "admin" &&
                !string.IsNullOrEmpty(expectedPassword) &&
                textBoxPassword.Text == expectedPassword)
            {
                
                appli application = new appli();
                application.ShowDialog();
                

            }
            else
            {
                MessageBox.Show("Mot de passe ou identifiant incorrect.", "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Masquer la fen�tre principale au lieu de la fermer
            this.Hide();

            // Ouvrir la fen�tre du client en tant que fen�tre modale
            Client client = new Client();
            client.ShowDialog();

            // Afficher � nouveau la fen�tre principale lorsque la fen�tre du client est ferm�e
            this.Show();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = false;
        }
    }
}
