namespace Accesso_Diretto_File
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.Nome_Prodotto = new System.Windows.Forms.TextBox();
            this.Prezzo_Prodotto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Aggiungi = new System.Windows.Forms.Button();
            this.Cancellazione_Logica = new System.Windows.Forms.Button();
            this.Cancellazione_Fisica = new System.Windows.Forms.Button();
            this.Prodotto_Cancellare = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Resetta_File = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Ricerca_NomeProdotto = new System.Windows.Forms.TextBox();
            this.Ricerca_Prodotti = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Ricerca_NumeroProdotto = new System.Windows.Forms.TextBox();
            this.Modifica_File = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Nome_Prodotto
            // 
            this.Nome_Prodotto.Location = new System.Drawing.Point(196, 63);
            this.Nome_Prodotto.Name = "Nome_Prodotto";
            this.Nome_Prodotto.Size = new System.Drawing.Size(209, 20);
            this.Nome_Prodotto.TabIndex = 0;
            // 
            // Prezzo_Prodotto
            // 
            this.Prezzo_Prodotto.Location = new System.Drawing.Point(197, 143);
            this.Prezzo_Prodotto.Name = "Prezzo_Prodotto";
            this.Prezzo_Prodotto.Size = new System.Drawing.Size(209, 20);
            this.Prezzo_Prodotto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome Prodotto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prezzo Prodotto:";
            // 
            // Aggiungi
            // 
            this.Aggiungi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aggiungi.Location = new System.Drawing.Point(472, 88);
            this.Aggiungi.Name = "Aggiungi";
            this.Aggiungi.Size = new System.Drawing.Size(135, 52);
            this.Aggiungi.TabIndex = 4;
            this.Aggiungi.Text = "Aggiungi\r\nProdotto";
            this.Aggiungi.UseVisualStyleBackColor = true;
            this.Aggiungi.Click += new System.EventHandler(this.Aggiungi_Click);
            // 
            // Cancellazione_Logica
            // 
            this.Cancellazione_Logica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancellazione_Logica.Location = new System.Drawing.Point(88, 222);
            this.Cancellazione_Logica.Name = "Cancellazione_Logica";
            this.Cancellazione_Logica.Size = new System.Drawing.Size(156, 76);
            this.Cancellazione_Logica.TabIndex = 7;
            this.Cancellazione_Logica.Text = "Cancellazione Logica";
            this.Cancellazione_Logica.UseVisualStyleBackColor = true;
            // 
            // Cancellazione_Fisica
            // 
            this.Cancellazione_Fisica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancellazione_Fisica.Location = new System.Drawing.Point(250, 222);
            this.Cancellazione_Fisica.Name = "Cancellazione_Fisica";
            this.Cancellazione_Fisica.Size = new System.Drawing.Size(156, 76);
            this.Cancellazione_Fisica.TabIndex = 8;
            this.Cancellazione_Fisica.Text = "Cancellazione Fisica";
            this.Cancellazione_Fisica.UseVisualStyleBackColor = true;
            // 
            // Prodotto_Cancellare
            // 
            this.Prodotto_Cancellare.Location = new System.Drawing.Point(120, 363);
            this.Prodotto_Cancellare.Name = "Prodotto_Cancellare";
            this.Prodotto_Cancellare.Size = new System.Drawing.Size(242, 20);
            this.Prodotto_Cancellare.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(297, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nome Prodotto Da Cancellare";
            // 
            // Resetta_File
            // 
            this.Resetta_File.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resetta_File.Location = new System.Drawing.Point(613, 88);
            this.Resetta_File.Name = "Resetta_File";
            this.Resetta_File.Size = new System.Drawing.Size(135, 52);
            this.Resetta_File.TabIndex = 12;
            this.Resetta_File.Text = "Resetta\r\nFile\r\n";
            this.Resetta_File.UseVisualStyleBackColor = true;
            this.Resetta_File.Click += new System.EventHandler(this.Resetta_File_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(528, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nome Prodotto Da Cercare\r\n";
            // 
            // Ricerca_NomeProdotto
            // 
            this.Ricerca_NomeProdotto.Location = new System.Drawing.Point(543, 379);
            this.Ricerca_NomeProdotto.Name = "Ricerca_NomeProdotto";
            this.Ricerca_NomeProdotto.Size = new System.Drawing.Size(242, 20);
            this.Ricerca_NomeProdotto.TabIndex = 15;
            // 
            // Ricerca_Prodotti
            // 
            this.Ricerca_Prodotti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ricerca_Prodotti.Location = new System.Drawing.Point(584, 222);
            this.Ricerca_Prodotti.Name = "Ricerca_Prodotti";
            this.Ricerca_Prodotti.Size = new System.Drawing.Size(156, 76);
            this.Ricerca_Prodotti.TabIndex = 14;
            this.Ricerca_Prodotti.Text = "Ricerca Prodotti\r\n";
            this.Ricerca_Prodotti.UseVisualStyleBackColor = true;
            this.Ricerca_Prodotti.Click += new System.EventHandler(this.Ricerca_Prodotti_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(518, 418);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(283, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Record Prodotto Da Cercare\r\n";
            // 
            // Ricerca_NumeroProdotto
            // 
            this.Ricerca_NumeroProdotto.Location = new System.Drawing.Point(543, 462);
            this.Ricerca_NumeroProdotto.Name = "Ricerca_NumeroProdotto";
            this.Ricerca_NumeroProdotto.Size = new System.Drawing.Size(242, 20);
            this.Ricerca_NumeroProdotto.TabIndex = 17;
            // 
            // Modifica_File
            // 
            this.Modifica_File.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Modifica_File.Location = new System.Drawing.Point(754, 88);
            this.Modifica_File.Name = "Modifica_File";
            this.Modifica_File.Size = new System.Drawing.Size(135, 52);
            this.Modifica_File.TabIndex = 19;
            this.Modifica_File.Text = "Modifica\r\nFile\r\n";
            this.Modifica_File.UseVisualStyleBackColor = true;
            this.Modifica_File.Click += new System.EventHandler(this.Modifica_File_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(161, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 76);
            this.button1.TabIndex = 20;
            this.button1.Text = "Recupera_Prodotto\r\n";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(96, 490);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(306, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Nome Prodotto Da Recuperare\r\n";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 533);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(242, 20);
            this.textBox1.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(469, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(312, 32);
            this.label7.TabIndex = 23;
            this.label7.Text = "Per aggiungere un prodotto scrivere nome e prezzo\r\nnelle categorie e poi cliccare" +
    " aggiugi";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(469, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(385, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Per modificare un prodotto scrivere il nome precedente\r\npoi cliccare il tasto mod" +
    "ifica e scrivere il nuovo nome e/o prezzo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 575);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Modifica_File);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Ricerca_NumeroProdotto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Ricerca_NomeProdotto);
            this.Controls.Add(this.Ricerca_Prodotti);
            this.Controls.Add(this.Resetta_File);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Prodotto_Cancellare);
            this.Controls.Add(this.Cancellazione_Fisica);
            this.Controls.Add(this.Cancellazione_Logica);
            this.Controls.Add(this.Aggiungi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Prezzo_Prodotto);
            this.Controls.Add(this.Nome_Prodotto);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Nome_Prodotto;
        private System.Windows.Forms.TextBox Prezzo_Prodotto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Aggiungi;
        private System.Windows.Forms.Button Cancellazione_Logica;
        private System.Windows.Forms.Button Cancellazione_Fisica;
        private System.Windows.Forms.TextBox Prodotto_Cancellare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Resetta_File;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Ricerca_NomeProdotto;
        private System.Windows.Forms.Button Ricerca_Prodotti;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Ricerca_NumeroProdotto;
        private System.Windows.Forms.Button Modifica_File;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

