namespace Interface_de_connexion_lecutre
{
    partial class Boisson
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelBoisson = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelBoisson
            // 
            this.flowLayoutPanelBoisson.Location = new System.Drawing.Point(3, 43);
            this.flowLayoutPanelBoisson.Name = "flowLayoutPanelBoisson";
            this.flowLayoutPanelBoisson.Size = new System.Drawing.Size(863, 440);
            this.flowLayoutPanelBoisson.TabIndex = 0;
            this.flowLayoutPanelBoisson.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Boisson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelBoisson);
            this.Name = "Boisson";
            this.Size = new System.Drawing.Size(898, 534);
            this.Load += new System.EventHandler(this.Boisson_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelBoisson;
    }
}
