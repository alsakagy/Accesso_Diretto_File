using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

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
        string Prezzo;
        int Lunghezza_Record = 64;
        int Numero_Prodotti = 0;
        int Max_Record = 100;
        int ricerca_Modifica;

        // Dichiaro il Writer, reader, FileStream
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
            File_W.BaseStream.Seek(0, 0);

            for (int i = 1; i <= Max_Record; i++)
            {
                File_W.Write(Riga_Binario);
            }
        }

        public int Ricerca_Binaria(Dati[] array, int lunghezza, string elemento)
        {
            int inizio = 0;
            int fine = lunghezza - 1;

            while (inizio <= fine)
            {
                int medio = (inizio + fine) / 2;
                string valoreMedio = array[medio].Nome;

                if (valoreMedio == elemento)
                {
                    return medio;  // Elemento trovato, restituisci l'indice.
                }
                else if (valoreMedio.CompareTo(elemento) < 0)
                {
                    inizio = medio + 1;  // L'elemento è nella metà superiore.
                }
                else
                {
                    fine = medio - 1;  // L'elemento è nella metà inferiore.
                }
            }

            return -1;  // Elemento non trovato.
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // se il file prodotti.dat non esiste ma il file record.txt esiste
            if (File.Exists("Prodotti.dat") == false && File.Exists("Record.txt") == true)
            {
                // apre il file 'prodotti.dat' creandolo e lo chiude
                FileStream File = new FileStream("Prodotti.dat", FileMode.Create, FileAccess.ReadWrite);
                File_W = new BinaryWriter(File);
                Reset_File(File, Riga_Vuoto, Dati_Vuoto, Riga_Binario);
                File_W = null;
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
                for (int i = 1; i <= Max_Record; i++)
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
                        Numero_Prodotti++;
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
                Numero_Prodotti++;
            }

            Percorso_File = new FileStream("Prodotti.dat", FileMode.Open, FileAccess.ReadWrite);
            File_W = new BinaryWriter(Percorso_File);
            File_R = new BinaryReader(Percorso_File);
        }
        public Form1()
        {
            InitializeComponent();
            // Dichiaro la struct
            Indici = new Dati[Max_Record];
        }
        private void Aggiungi_Click(object sender, EventArgs e)
        {
            if (Nome_Prodotto.Text != "" && Prezzo_Prodotto.Text != "")
            {
                if (Prezzo_Prodotto.Text.All(char.IsDigit) && Nome_Prodotto.Text.All(char.IsLetter))
                {
                    // Inserimento dati nelle variabili
                    Nome = Nome_Prodotto.Text;
                    Prezzo = Prezzo_Prodotto.Text;

                    if(Nome.Length < 31 && Prezzo_Prodotto.Text.Length < 32)
                    {
                        if (Numero_Prodotti <= Max_Record - 1)
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
                            for (int i = 1; i < Max_Record; i++)
                            {
                                // Leggi il record i - 1
                                File_R.BaseStream.Seek(((i) - 1) * Lunghezza_Record, 0);

                                // Leggi Nome prodotto fino a che è uguale a @ quindi vuoto
                                Leggi_Record = File_R.ReadBytes(64);

                                // Controllo se esistente o no nel file
                                if (Indici[i - 1].Nome == Nome)
                                {
                                    if (Leggi_Record[0] == '|')
                                    {
                                        C_Nome = true;
                                        j = i;
                                        Quantità = int.Parse(Encoding.Default.GetString(Leggi_Record, 62, 2));
                                        break;
                                    }
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

                                // Messaggio di risposta
                                MessageBox.Show("hai inserito un prodotto che già esiste, è incrementata la quantità");
                            }
                            else if (C_Vuoto == true)
                            {
                                int y = Numero_Prodotti;

                                // Se ci sono zero elementi l'ho inserisce nel primo 'slot' della struct
                                if(y == 0)
                                {
                                    Indici[y].Nome = Nome;
                                    Indici[y].Indice = k;
                                }
                                else
                                {
                                    // altrimenti confronta le stringhe fino a trovare la posizione corretta slittando tutti di uno
                                    bool Trovato = false;
                                    while (Trovato == false)
                                    {
                                        // se dopo aver confrontato tutte le strighe la posizione corretta è zero entra qui
                                        if(y == 0)
                                        {
                                            Indici[y].Nome = Nome;
                                            Indici[y].Indice = k;
                                            Trovato = true;
                                        }
                                        else
                                        {
                                            if (Nome.CompareTo(Indici[y - 1].Nome) < 0)
                                            {
                                                Indici[y].Nome = Indici[y - 1].Nome;
                                                Indici[y].Indice = Indici[y - 1].Indice;
                                            }
                                            else
                                            {
                                                Indici[y].Nome = Nome;
                                                Indici[y].Indice = k;
                                                Trovato = true;
                                            }
                                            y--;
                                        }
                                    }
                                }
                                Numero_Prodotti++;

                                // Conversione in binario dei dati
                                Riga = '|' + Nome.PadRight(31) + Prezzo.PadRight(30) + "1".PadRight(2);
                                Riga_Binario = Encoding.Default.GetBytes(Riga);

                                // Inserimento nel file
                                File_W.BaseStream.Seek(((k) - 1) * Lunghezza_Record, 0);
                                File_W.Write(Riga_Binario);
                            }
                            // Svuoto caselle di testo nel form
                            Nome_Prodotto.Text = "";
                            Prezzo_Prodotto.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("hai raggiunto il numero massimo di record, cancella dei prodotti e riprova");
                        }
                    }
                    else
                    {
                        MessageBox.Show("il nome o il prezzo sono troppo lunghi per essere memorizzati diminuisci la lunghezza e riprova");
                    }
                }
                else
                {
                    MessageBox.Show("il prezzo non è un numero o il nome non è una stringa, correggi gli errori e riprova");
                }
            }
            else
            {
                MessageBox.Show("non hai inserito il nome o il prezzo, assicurati di aver inserito tutti i dati e riprova");
            }
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
            int ricerca = Ricerca_Binaria(Indici, Numero_Prodotti, Ricerca_Prodotto.Text);

            if(ricerca == -1)
            {
                MessageBox.Show("il prodotto ricercato non esiste, inserisci un prodotto esistente e riprova");
                Ricerca_Prodotto.Text = "";
            }
            else
            {
                byte[] Leggi_Record;
                string Record;

                // Leggi il record del prodotto ricercato
                File_R.BaseStream.Seek(((Indici[ricerca].Indice) - 1) * Lunghezza_Record, 0);

                // leggo il record (lungo 64)
                Leggi_Record = File_R.ReadBytes(64);
                Record = Encoding.Default.GetString(Leggi_Record);

                if (Record[0] != '|')
                {
                    MessageBox.Show("il prodotto ricercato non esiste, inserisci un prodotto esistente e riprova");
                    Ricerca_Prodotto.Text = "";
                }
                else
                {
                    MessageBox.Show($"Nome Prodotto:    {Record.Substring(1, 31).TrimEnd(' ')}\nPrezzo Prodotto:    {Record.Substring(32, 30).TrimEnd(' ')}\nQuantità:   {Record.Substring(62, 2)}");
                    Ricerca_Prodotto.Text = "";
                }
            }
        }

        private void Modifica_File_Click(object sender, EventArgs e)
        {
            string Prodotto_Modifica = Interaction.InputBox("Inserisci il nome del prodotto che vuoi eliminare");

            int ricerca_Modifica = Ricerca_Binaria(Indici, Numero_Prodotti, Prodotto_Modifica);

            if (ricerca_Modifica == -1)
            {
                // messaggio errore
                MessageBox.Show("il prodotto ricercato da modificare non esiste, inserisci un prodotto esistente e riprova");
            }
            else
            {
                // instruzioni
                MessageBox.Show("ora inserisci il nuovo nome e/o prezzo negli spazi dedicati");

                // Disattivo i campi non utili
                Aggiungi.Enabled = false;
                Resetta_File.Enabled = false;
                Modifica_File.Enabled = false;
                Cancellazione_Fisica.Enabled = false;
                Cancellazione_Logica.Enabled = false;
                Prodotto_Cancellare.Enabled = false;
                Recupera_Prodotto.Enabled = false;
                Prodotto_Recuperare.Enabled = false;
                Ricerca_Prodotti.Enabled = false;
                Ricerca_Prodotto.Enabled = false;

                // rendo utilizzabile tasto di conferma
                Conferma.Visible = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter Sw = new StreamWriter("Record.txt");
            for (int i = 0; i < Numero_Prodotti; i++)
            {
                Sw.WriteLine($"{Indici[i].Nome};{Indici[i].Indice}");
            }
            Sw.Close();
            Percorso_File.Close();
            File_W.Close();
            File_R.Close();
        }

        private void Cancellazione_Logica_Click(object sender, EventArgs e)
        {
            int ricerca = Ricerca_Binaria(Indici, Numero_Prodotti, Prodotto_Cancellare.Text);

            if (ricerca == -1)
            {
                MessageBox.Show("il prodotto ricercato non esiste, inserisci un prodotto esistente e riprova");
                Prodotto_Cancellare.Text = "";
            }
            else
            {
                // mi posiziono per la scrittura
                File_W.BaseStream.Seek(((Indici[ricerca].Indice) - 1) * Lunghezza_Record, 0);

                // sovrascrivo il carattere identificatore
                File_W.Write(Encoding.Default.GetBytes("%"));

                // svuoto casella di testo
                Prodotto_Cancellare.Text = "";

                // messaggio di avviso
                MessageBox.Show("Prodotto cancellato logicamente");
            }
        }

        private void Recupera_Prodotto_Click(object sender, EventArgs e)
        {
            int ricerca = Ricerca_Binaria(Indici, Numero_Prodotti, Prodotto_Recuperare.Text);

            if (ricerca == -1)
            {
                MessageBox.Show("il prodotto in questione non è mai esistito o non può essere recuperato");
                Prodotto_Recuperare.Text = "";
            }
            else
            {
                // mi posiziono per la scrittura
                File_W.BaseStream.Seek(((Indici[ricerca].Indice) - 1) * Lunghezza_Record, 0);

                // sovrascrivo il carattere identificatore
                File_W.Write(Encoding.Default.GetBytes("|"));

                // svuoto casella di testo
                Prodotto_Recuperare.Text = "";

                // messaggio di avviso
                MessageBox.Show("Prodotto recuperato");
            }
        }

        private void Cancellazione_Fisica_Click(object sender, EventArgs e)
        {
            int ricerca = Ricerca_Binaria(Indici, Numero_Prodotti, Prodotto_Cancellare.Text);

            if (ricerca == -1)
            {
                MessageBox.Show("il prodotto ricercato non esiste, inserisci un prodotto esistente e riprova");
                Prodotto_Cancellare.Text = "";
            }
            else
            {
                // mi posiziono per la scrittura
                File_W.BaseStream.Seek(((Indici[ricerca].Indice) - 1) * Lunghezza_Record, 0);

                // Impostazione riga vuota
                Riga = Dati_Vuoto + Dati_Vuoto.PadRight(31) + Dati_Vuoto.PadRight(30) + Dati_Vuoto.PadRight(2);

                // Trasformazione in binario
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // Sovrascrittura
                File_W.Write(Riga_Binario);

                // for che sposta tutti gli elementi della struct di uno
                for(int i = ricerca; i < Numero_Prodotti - 1; i++)
                {
                    Indici[i].Nome = Indici[i + 1].Nome;
                    Indici[i].Indice = Indici[i + 1].Indice;
                }

                // Diminuisco il numero di prodotti inseriti
                Numero_Prodotti--;

                // svuoto casella di testo
                Prodotto_Cancellare.Text = "";

                // messaggio di avviso
                MessageBox.Show("Prodotto Cancellato fisicamente");
            }
        }

        private void Conferma_Click(object sender, EventArgs e)
        {

            // mi posiziono per la lettura
            File_R.BaseStream.Seek(((Indici[ricerca_Modifica].Indice) - 1) * Lunghezza_Record, 0);

            // leggo il record (lungo 64)
            byte[] Leggi_Record = File_R.ReadBytes(64);
            string Record = Encoding.Default.GetString(Leggi_Record);

            // mi posiziono per la scrittura
            File_W.BaseStream.Seek(((Indici[ricerca_Modifica].Indice) - 1) * Lunghezza_Record, 0);

            if (Nome_Prodotto.Text != "" && Prezzo_Prodotto.Text != "")
            {
                // Impostazione riga con dati modificati
                Riga = Record[0] + Nome_Prodotto.Text.PadRight(31) + Prezzo_Prodotto.Text.PadRight(30) + Record.Substring(62, 2);

                // Trasformazione in binario
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // sovrascrivo il record
                File_W.Write(Riga_Binario);

                // svuoto le caselle di testo
                Nome_Prodotto.Text = "";
                Prezzo_Prodotto.Text = "";
            }
            else if(Nome_Prodotto.Text != "" && Prezzo_Prodotto.Text == "")
            {
                // Impostazione riga con dati modificati
                Riga = Record[0] + Nome_Prodotto.Text.PadRight(31) + Record.Substring(32, 32);

                // Trasformazione in binario
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // sovrascrivo il record
                File_W.Write(Riga_Binario);

                // svuoto le caselle di testo
                Nome_Prodotto.Text = "";
            }
            else if (Prezzo_Prodotto.Text != "" && Nome_Prodotto.Text == "")
            {
                // Impostazione riga con dati modificati
                Riga = Record[0] + Record.Substring(1, 31) + Prezzo_Prodotto.Text.PadRight(30) + Record.Substring(62, 2);

                // Trasformazione in binario
                Riga_Binario = Encoding.Default.GetBytes(Riga);

                // sovrascrivo il record
                File_W.Write(Riga_Binario);

                // svuoto le caselle di testo
                Prezzo_Prodotto.Text = "";
            }
            // Messaggio errore per mancato inserimento dati
            else
            {
                MessageBox.Show("non hai inserito i campi necessari, riprova");
            }

            // riattivo i tasti disattivati
            Aggiungi.Enabled = true;
            Resetta_File.Enabled = true;
            Modifica_File.Enabled = true;
            Cancellazione_Fisica.Enabled = true;
            Cancellazione_Logica.Enabled = true;
            Prodotto_Cancellare.Enabled = true;
            Recupera_Prodotto.Enabled = true;
            Prodotto_Recuperare.Enabled = true;
            Ricerca_Prodotti.Enabled = true;
            Ricerca_Prodotto.Enabled = true;

            // rendo invisibile il tasto conferma
            Conferma.Visible = false;
        }
    }
}