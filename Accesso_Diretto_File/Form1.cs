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
        byte[] Riga_Binario;
        double Prezzo;
        int Lunghezza_Record = 64;

        // Dichiaro il Writer, reader, FileStream
        FileStream Percorso_File;
        BinaryWriter File_W;
        BinaryReader File_R;

        public void Reset_File(FileStream Percorso_File, string Riga_Vuoto, string Dati_Vuoto, byte[] Riga_Binario)
        {
            BinaryWriter Bw = new BinaryWriter(Percorso_File);
            // Creo riga con dati vuoti
            Riga_Vuoto = Dati_Vuoto + Dati_Vuoto.PadRight(31) + Dati_Vuoto.PadRight(30) + Dati_Vuoto.PadRight(2);
            // Trasformo riga in binario
            Riga_Binario = Encoding.Default.GetBytes(Riga_Vuoto);
            // Stampo 100 righe nel file
            for (int i = 1; i <= 100; i++)
            {
                Bw.Write(Riga_Binario);
            }
            Bw.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // se il file prodotti.dat non esiste ma il file record.txt esiste
            if (File.Exists("Prodotti.dat") == false && File.Exists("Record.txt") == true)
            {
                // apre il file 'prodotti.dat' creandolo e lo chiude
                FileStream File = new FileStream("Prodotti.dat", FileMode.Create, FileAccess.ReadWrite);
                Reset_File(File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
                File.Close();

                // apre il file 'record.txt' e lo svuota aprendolo il truncate
                FileStream Files = new FileStream("Record.txt", FileMode.Truncate, FileAccess.ReadWrite);
                Files.Close();
            }
            // se il file prodotti.dat non esiste e il file record.txt non esiste
            else if (File.Exists("Prodotti.dat") == false && File.Exists("Record.txt") == false)
            {
                // apre il file 'prodotti.dat' creandolo e lo chiude
                FileStream File = new FileStream("Prodotti.dat", FileMode.Create, FileAccess.ReadWrite);
                Reset_File(File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
                File.Close();

                // apre il file 'record.txt' creandolo e lo chiude
                FileStream Files = new FileStream("Record.txt", FileMode.Create, FileAccess.ReadWrite);
                Files.Close();
            }
            // se il file prodotti.dat esiste ma il file record.txt non esiste
            else if (File.Exists("Prodotti.dat") == true && File.Exists("Record.txt") == false)
            {
                // apre il file 'prodotti.dat' e il reader
                FileStream File = new FileStream("Prodotti.dat", FileMode.Open, FileAccess.ReadWrite);
                BinaryReader Br = new BinaryReader(File);

                // apre il file 'record.txt' creandolo
                FileStream Files = new FileStream("Record.txt", FileMode.Create, FileAccess.ReadWrite);
                Files.Close();

                // for per il numero di record che legge il nome e il numero prodotto e li salva nel file struct
                byte[] Leggi_Record;
                for (int i = 1; i <= 100; i++)
                {
                    // Leggi il record i - 1
                    Br.BaseStream.Seek(((i) - 1) * Lunghezza_Record, 0);

                    // Leggi del record i primi 32 byte che comprendono il controllo a cancellazione logica e il nome
                    Leggi_Record = Br.ReadBytes(32);

                    // Scrivo nella struct solo i dati dei record non vuoti
                    if (Leggi_Record[1] != '@')
                    {
                        // Prende il nome e rimuove gli spazi alla fine
                        string temp = Encoding.Default.GetString(Leggi_Record, 1, 31).TrimEnd(' ');
              
                        // scrive nel file struct
                        StreamWriter Stream = new StreamWriter("Record.txt", true);
                        Stream.Write($"{temp};{i - 1}\n");
                        Stream.Close();
                    }
                }
                Br.Close();
                File.Close();
            }

            // Lettura dati
            string[] strings = File.ReadAllLines("Record.txt");

            // Ciclo for per inserimento dati nella struct
            for (int i = 0; i < strings.Length; i++)
            {
                Indici[i].Nome = strings[i].Split(';')[0];
                Indici[i].Indice = int.Parse(strings[i].Split(';')[1]);
            }

            Percorso_File = new FileStream("Prodotti.dat", FileMode.Open, FileAccess.ReadWrite);
            File_W = new BinaryWriter(Percorso_File);
            File_R = new BinaryReader(Percorso_File);
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
            bool temp = true;

            // Gira per il numero di record fino a che non trova un record vuoto
            for (int i = 1; i < 100; i++)
            {
                // Leggi il record i - 1
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
                    j = i;
                    Quantità = int.Parse(Encoding.Default.GetString(Leggi_Record, 62, 2));
                    break;
                }
                else if (Leggi_Record[1] == '@')
                {
                    C_Vuoto = true;
                    if (temp)
                    {
                        k = i;
                        temp = false;
                    }
                }
            }

            // If di svolgimento aggiunta
            if (C_Nome == true)
            {
                // Conversione in binario dei dati
                Riga = $"{Quantità + 1}".PadRight(2);
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // Inserimento nel file
                File_W.BaseStream.Seek(((j - 1) * Lunghezza_Record) + 62, 0);
                File_W.Write(Riga_Binario);
            }
            else if (C_Vuoto == true)
            {
                // Inserimento nel file Indici
                StreamWriter Stream = new StreamWriter("Record.txt", true);
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
            // apre il file 'record.txt' e lo svuota aprendolo il truncate
            FileStream Files = new FileStream("Record.txt", FileMode.Truncate, FileAccess.ReadWrite);
            Files.Close();
            MessageBox.Show("hai resettato il file prodotti, perciò e stato resettato anche il file degli indici");
        }

        private void Ricerca_Prodotti_Click(object sender, EventArgs e)
        {
            if (Ricerca_NomeProdotto.Text == "" && Ricerca_NumeroProdotto.Text != "")
            {
                // Caso ricerca dal numero prodotto (non controllo errori di inserimento)
            }
            else if (Ricerca_NumeroProdotto.Text == "" && Ricerca_NomeProdotto.Text != "")
            {
                // Caso ricerca dal nome prodotto (non controllo errori di inserimento)
            }
            else
            {
                // Caso inserisce qualcosa in entrambi oppure non inserisce nulla
            }
        }
    }
}