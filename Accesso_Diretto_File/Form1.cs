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
        // Dati Comuni
        string Dati_Vuoto = "@";
        string Nome;
        string Riga;
        string Riga_Vuoto;
        string File_Record = "Record.txt";
        byte[] Riga_Binario;
        int Prezzo;
        int Lunghezza_Record = 64;

        // Dichiaro il Writer, reader e FileStream
        FileStream Percorso_File;
        BinaryWriter File_W;
        BinaryReader File_R;
        public void Reset_File(FileStream Percorso_File, string Riga_Vuoto, string Dati_Vuoto, byte[] Riga_Binario)
        {
            // Creo riga con dati vuoti
            Riga_Vuoto = Dati_Vuoto + Dati_Vuoto.PadRight(32) + Dati_Vuoto.PadRight(31);
            // Trasformo riga in binario
            Riga_Binario = Encoding.Default.GetBytes(Riga_Vuoto);
            // Stampo 100 righe nel file
            for (int i = 1; i <= 100; i++)
            {
                File_W.Write(Riga_Binario);
            }
        }
        public Form1()
        {
            InitializeComponent();
            // Metodo per la dimensione (in byte) del file
            FileInfo Info = new FileInfo("Prodotti.dat");
            /* Utilizzo la dimensione del file per capire se è vuoto
            - nel caso dimesione e zero riempio il file con un contenuto vuoto (le chiocciole)
            _ nel caso dimensione diversa da zero il file è già pieno */
            if (Info.Length == 0)
            {
                Reset_File(Percorso_File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
            }
        }

        private void Aggiungi_Click(object sender, EventArgs e)
        {
            // Array per il record letto 
            byte[] Leggi_Record;
            // Gira per il numero di record fino a che non trova un record vuoto
            for(int i = 1; i <= 100; i++)
            {
                // Leggi il record i + 1
                File_R.BaseStream.Seek(((i) - 1) * Lunghezza_Record, 0);

                // Leggi Nome prodotto fino a che è uguale a @ quindi vuoto
                Leggi_Record = File_R.ReadBytes(31);
                if (Leggi_Record[1] == '@')
                {
                    // Inserimento dati nelle variabili
                    Nome = Nome_Prodotto.Text;
                    Prezzo = int.Parse(Prezzo_Prodotto.Text);

                    // Inserimento nel file 
                    StreamWriter Stream = new StreamWriter(File_Record, true);
                    Stream.Write($"{Nome};{i}\n");
                    Stream.Close();

                    // Conversione in binario dei dati
                    Riga = '|' + Nome.PadRight(32) + Prezzo.ToString().PadRight(31);
                    Riga_Binario = Encoding.Default.GetBytes(Riga);

                    // Inserimento nel file
                    File_W.BaseStream.Seek(((i) - 1) * Lunghezza_Record, 0);
                    File_W.Write(Riga_Binario);
                    break;
                }
            }
            // Svuoto caselle di testo nel form
            Nome_Prodotto.Text = "";
            Prezzo_Prodotto.Text = "";
        }

        private void Resetta_File_Click(object sender, EventArgs e)
        {
            Reset_File(Percorso_File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Percorso_File = new FileStream("Prodotti.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            // apro il Writer e reader
            File_W = new BinaryWriter(Percorso_File);
            File_R = new BinaryReader(Percorso_File);
        }
    }
}
