
namespace AnalizadorLexico
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.txtTextoAnalizar = new System.Windows.Forms.TextBox();
            this.lblprueba = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Location = new System.Drawing.Point(325, 195);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(75, 23);
            this.btnAnalizar.TabIndex = 0;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // txtTextoAnalizar
            // 
            this.txtTextoAnalizar.Location = new System.Drawing.Point(200, 83);
            this.txtTextoAnalizar.Name = "txtTextoAnalizar";
            this.txtTextoAnalizar.Size = new System.Drawing.Size(350, 23);
            this.txtTextoAnalizar.TabIndex = 1;
            this.txtTextoAnalizar.TextChanged += new System.EventHandler(this.txtTextoAnalizar_TextChanged_1);
            // 
            // lblprueba
            // 
            this.lblprueba.AutoSize = true;
            this.lblprueba.Location = new System.Drawing.Point(300, 248);
            this.lblprueba.Name = "lblprueba";
            this.lblprueba.Size = new System.Drawing.Size(38, 15);
            this.lblprueba.TabIndex = 3;
            this.lblprueba.Text = "label1";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(476, 195);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.lblprueba);
            this.Controls.Add(this.txtTextoAnalizar);
            this.Controls.Add(this.btnAnalizar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.TextBox txtTextoAnalizar;
        private System.Windows.Forms.Label lblprueba;
        private System.Windows.Forms.Button btnLimpiar;
    }
}

