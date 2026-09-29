namespace Interface_de_connexion_lecutre
{
    partial class Plat
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
            this.flowLayoutPanelPlats = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelPlats
            // 
            this.flowLayoutPanelPlats.Location = new System.Drawing.Point(3, 27);
            this.flowLayoutPanelPlats.Name = "flowLayoutPanelPlats";
            this.flowLayoutPanelPlats.Size = new System.Drawing.Size(878, 443);
            this.flowLayoutPanelPlats.TabIndex = 1;
            this.flowLayoutPanelPlats.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Plat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPlats);
            this.Name = "Plat";
            this.Size = new System.Drawing.Size(898, 534);
            this.Load += new System.EventHandler(this.Plat_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private FlowLayoutPanel flowLayoutPanelPlats;
    }
}
