namespace Interface_de_connexion_lecutre
{
    partial class Dessert
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
            this.flowLayoutPanelDessert = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelDessert
            // 
            this.flowLayoutPanelDessert.BackColor = System.Drawing.Color.DarkOrange;
            this.flowLayoutPanelDessert.Location = new System.Drawing.Point(3, 43);
            this.flowLayoutPanelDessert.Name = "flowLayoutPanelDessert";
            this.flowLayoutPanelDessert.Size = new System.Drawing.Size(863, 440);
            this.flowLayoutPanelDessert.TabIndex = 0;
            this.flowLayoutPanelDessert.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelDessert_Paint);
            // 
            // Dessert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flowLayoutPanelDessert);
            this.Name = "Dessert";
            this.Size = new System.Drawing.Size(898, 534);
            this.Load += new System.EventHandler(this.Dessert_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelDessert;
    }
}
