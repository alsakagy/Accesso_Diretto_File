using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accesso_Diretto_File
{
    public partial class Form1 : Form
    {
        // Struct 
        public struct Dati
        {
            public string Nome;
            public int Indice;
        }

        // Dati Comuni
        public Dati[] Indici; 
        string Dati_Vuoto = "@";
        string Nome;
        string Riga;
        string Riga_Vuoto;
        string File_Record = "Record.txt";
        byte[] Riga_Binario;
        double Prezzo;
        int Lunghezza_Record = 64;

        // Dichiaro il Writer, reader, FileStream e il File Indici
        FileStream Percorso_File;
        BinaryWriter File_W;
        BinaryReader File_R;

        public void Reset_File(FileStream Percorso_File, string Riga_Vuoto, string Dati_Vuoto, byte[] Riga_Binario)
        {
            // Creo riga con dati vuoti
            Riga_Vuoto = Dati_Vuoto + Dati_Vuoto.PadRight(31) + Dati_Vuoto.PadRight(30) + Dati_Vuoto.PadRight(2);
            // Trasformo riga in binario
            Riga_Binario = Encoding.Default.GetBytes(Riga_Vuoto);
            // Stampo 100 righe nel file
            for (int i = 1; i <= 64; i++)
            {
                File_W.Write(Riga_Binario);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Percorso_File = new FileStream("Prodotti.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            // Apro il Writer e reader
            File_W = new BinaryWriter(Percorso_File);
            File_R = new BinaryReader(Percorso_File);

            // Metodo per la dimensione (in byte) del file
            FileInfo Info = new FileInfo("Prodotti.dat");
            /* Utilizzo la dimensione del file per capire se è vuoto
            - nel caso dimesione e zero riempio il file con un contenuto vuoto (le chiocciole)
            _ nel caso dimensione diversa da zero il file è già pieno */
            if (File.Exists("Prodotti.dat") == false || Info.Length == 0)
            {
                Reset_File(Percorso_File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
                File.Delete(File_Record);
                File.Create(File_Record);
                MessageBox.Show("Attenzione: il file dei prodotti è stato cancellato o non esiste, per evitare errori è stato resettato anche il file contenente gli indici");
            }

            // Lettura dati
            string[] strings = File.ReadAllLines(File_Record);

            // Ciclo for per inserimento dati nella struct
            for(int i = 0; i < strings.Length; i++)
            {
                Indici[i].Nome = strings[i].Split(';')[0];
                Indici[i].Indice = int.Parse(strings[i].Split(';')[1]);
            }

        }
        public Form1()
        {
            InitializeComponent();
            // Dichiaro la struct
            Indici = new Dati[100];
        }

        private void Aggiungi_Click(object sender, EventArgs e)
        {
            // Array per il record letto 
            byte[] Leggi_Record;

            // Variabili bool e int
            bool C_Nome = false;
            bool C_Vuoto = false;
            int j = 0;
            int k = 0;
            int Quantità = 0;
            bool temp1 = true;
            bool temp2 = true;

            // Gira per il numero di record fino a che non trova un record vuoto
            for (int i = 1; i < 100; i++)
            {
                // Leggi il record i + 1
                File_R.BaseStream.Seek(((i) - 1) * Lunghezza_Record, 0);

                // Inserimento dati nelle variabili
                Nome = Nome_Prodotto.Text;
                Prezzo = Convert.ToDouble(Prezzo_Prodotto.Text);

                // Leggi Nome prodotto fino a che è uguale a @ quindi vuoto
                Leggi_Record = File_R.ReadBytes(64);

                // Controllo se esistente o no nel file
                if (Indici[i - 1].Nome == Nome)
                {
                    C_Nome = true;
                    if (temp1)
                    {
                        j = i;
                        temp1 = false;
                        Quantità = int.Parse(Encoding.Default.GetString(Leggi_Record, 62, 2));
                    }
                    break;
                }
                else if (Leggi_Record[1] == '@')
                {
                    C_Vuoto = true;
                    if(temp2)
                    {
                        k = i;
                        temp2 = false;
                    }
                }
            }

            // If di svolgimento aggiunta
            if(C_Nome == true)
            {
                // Conversione in binario dei dati
                Riga = $"{Quantità + 1}".PadRight(2);
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // Inserimento nel file
                File_W.BaseStream.Seek(((j - 1) * Lunghezza_Record) + 62, 0);
                File_W.Write(Riga_Binario);
            }
            else if(C_Vuoto == true && C_Nome == false)
            {
                // Inserimento nel file Indici
                StreamWriter Stream = new StreamWriter(File_Record, true);
                Stream.Write($"{Nome};{k}\n");
                Stream.Close();

                // Conversione in binario dei dati
                Riga = '|' + Nome.PadRight(31) + Prezzo.ToString().PadRight(30) + "1".PadRight(2);
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // Inserimento nel file
                File_W.BaseStream.Seek(((k) - 1) * Lunghezza_Record, 0);
                File_W.Write(Riga_Binario);
            }
            // Svuoto caselle di testo nel form
            Nome_Prodotto.Text = "";
            Prezzo_Prodotto.Text = "";
        }

        private void Resetta_File_Click(object sender, EventArgs e)
        {
            Reset_File(Percorso_File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
        }

        private void Ricerca_Prodotti_Click(object sender, EventArgs e)
        {
            if(Ricerca_NomeProdotto.Text == "" && Ricerca_NumeroProdotto.Text != "")
            {
                // Caso ricerca dal numero prodotto (non controllo errori di inserimento)
            }
            else if(Ricerca_NumeroProdotto.Text == "" && Ricerca_NomeProdotto.Text != "")
            {
                // Caso ricerca dal nome prodotto (non controllo errori di inserimento)
            }
            else
            {
                // Caso inserisce qualcosa in entrambi oppure non inserisce nulla
            }
        }
        /* array di struct che contiene nome prodotto e numero prodotto 
         * -ordinato per ricerca binaria
         * -di 100 elementi perchè abbiamo 100 record
         * -bisogna aver un numero per indicare quali sono gli elementi validi nei 100 dell'array
         * -e poi si salva su file alla chiusura del file 
         * -alla apertura si riempe di nuovo l'array di struct
         */
    }
}
