using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IASEProject
{
    public partial class Form1 : Form
    {

        public int[,] stareInitiala, stareFinala, stareActuala;
        public byte[] initState, finalState;
        List<int[,]> noduri = new List<int[,]>();
        List<int[,]> exploredState = new List<int[,]>();
        List<Button> buttons = new List<Button>();
        private CautareInAdancime mInstance=null;
        public int n = 0;

        public Form1()
        {
            InitializeComponent();

            btnAdancime.Enabled = false;
            btnInformata.Enabled = false;
            btnLimitare.Enabled = false;
            btnInformata.Enabled = false;
            btnNivel.Enabled = false;
            btnUniform.Enabled = false;

          //  mInstance = mInstance.CreateCautareInAdancime();
        }

        //Generarea butoanelor
        Button btn(int i, int j)
        {
            n = int.Parse(txtN.Text);
            Button b = new Button();
            b.Name = i.ToString() + "," + j.ToString();
            // b.Width = (n*100)/( (int)(Math.Pow(n, 1.5)));
            // b.Height = (n * 100) / ((int)(Math.Pow(n, 1.5)));
            b.Width = (flowLayoutPanel1.Width / n) - 7;
            b.Height = (flowLayoutPanel1.Height / n) - 7;
            b.Click += B_Click;

            if (stareFinala[i, j] == 0)
                b.BackColor = Color.Red;
            else
                b.BackColor = Color.Green;


            //Se creeaza o lista de butoane pentru a avea acces mai usor la ele
            var x = buttons.FindIndex(_=>_.Name.Equals(b.Name));
            if (x<0)
            {
                buttons.Add(b);
            }
            else
            {
                buttons[x] = b;
            }

                return b;
        }

        //Eveniment valabil pentru toate butoanele
        private void B_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.BackColor = (b.BackColor == Color.Green) ? Color.Red : Color.Green; // la apasare culoarea se schimba
            //MessageBox.Show(b.Name.ToString());
            int i = int.Parse(b.Name.Split(',')[0]);
            int j = int.Parse(b.Name.Split(',')[1]);

            stareFinala[i, j] = (stareFinala[i, j] == 0) ? 1 : 0;
        }

        //Initializarea starii initiale si starii finale a matricei de leduri
        private void btnGenerareJoc_Click(object sender, EventArgs e)
        {
            int n = int.Parse(txtN.Text);
            if (n > 1 && n < 15)
            {
                stareFinala = new int[n, n];
                stareInitiala = new int[n, n];
                initState = new byte[n * n];
                finalState = new byte[n * n];
                InitializateInitialState(n * n);
                InitializateFinalState(n * n);

                InitializateInitialMatrix(n);
                InitializateFinalMatrix(n);
               
                flowLayoutPanel1.Controls.Clear();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        flowLayoutPanel1.Controls.Add(btn(i, j));
                    }
                }
            }
            else
            {
                MessageBox.Show("Alegeti un n intre 2 si 10");
            }
        }

        private void InitializateFinalState(int v)
        {
            //Se initializeaza matricea finala cu 0, dimensiunea fiind n * n pentru vector
            for (int i = 0; i < n*n; i++)
            {
                    initState[i] = 0;
            }
        }

        private void InitializateInitialState(int v)
        {
            //Se initializeaza matricea initiala cu 0, dimensiunea fiind n * n pentru vector
            for(int i=0;i<n*n;i++)
            {
                finalState[i] = 0;
            }
        }

        public void InitializateInitialMatrix(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    stareInitiala[i, j] = 0;
                }
            }

            stareActuala = stareInitiala;
        }

        public void InitializateFinalMatrix(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    stareFinala[i, j] = 0;
                }
            }
        }

        //Aisare Matrice .. prin functia asta putem afisa solutia. Dupa fiecare apasare led apelam functia ca sa afisam stare(Solutia o sa fie multimea starilor)
        public void PrintMatrix(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    textBox1.Text = textBox1.Text + stareInitiala[i, j].ToString() + " ";
                }
                textBox1.Text = textBox1.Text + Environment.NewLine;
            }
            textBox1.Text = textBox1.Text + Environment.NewLine;
        }

        //Functie echivalenta cu apasarea unui led din matrice(ledurile adiacente trec pe starea complementara)
        public void SetPosition(int i, int j, int n)
        {
            stareActuala[i, j] = (stareActuala[i, j] == 0) ? 1 : 0; // ledul apasat

            if (i + 1 < n)
            {
                stareActuala[i + 1, j] = (stareActuala[i + 1, j] == 0) ? 1 : 0; // Led jos
            }

            if (j + 1 < n)
            {
                stareActuala[i, j + 1] = (stareActuala[i, j + 1] == 0) ? 1 : 0; // Led dreapta
            }

            if (i - 1 >= 0)
            {
                stareActuala[i - 1, j] = (stareActuala[i - 1, j] == 0) ? 1 : 0; // led sus
            }

            if (j - 1 >= 0)
            {
                stareActuala[i, j - 1] = (stareActuala[i, j - 1] == 0) ? 1 : 0; // led stanga
            }
        }

        private void btnConfirmStFinala_Click(object sender, EventArgs e)
        {
            if (btnConfirmStFinala.Text == "Confirm Stare Finala")
            {
                btnAdancime.Enabled = true;
                btnInformata.Enabled = true;
                btnLimitare.Enabled = true;
                btnInformata.Enabled = true;
                btnNivel.Enabled = true;
                btnUniform.Enabled = true;

                btnConfirmStFinala.Text = "Editare Stare Finala";
                flowLayoutPanel1.Enabled = false;
            }
            else
            {
                btnAdancime.Enabled = false;
                btnInformata.Enabled = false;
                btnLimitare.Enabled = false;
                btnInformata.Enabled = false;
                btnNivel.Enabled = false;
                btnUniform.Enabled = false;
                btnConfirmStFinala.Text = "Confirm Stare Finala";
                flowLayoutPanel1.Enabled = true;
            }

            //Se modifica valoarea matricei finale in functie de apasarea butoanelor
           int i = 0;
           foreach(var x in buttons)
            {
                
                if (x.BackColor == Color.Red)
                    finalState[i] = 0;
                else
                    finalState[i] = 1;
                i++;
            }
        }

        /* Aici puteti scrie algoritmii. Aveti ca si intrari pentru program matricele stareInitiala si stareFinala.
         * Pasi:
         *  -pornesti programul, setezi n-ul si dai Generare Joc. Apoi selectezi Starea Finala apasand pe butoanele aparute.
         *  -apasa pe Confirm Stare Finala( acum nu mai poti apasa pe butoanele din matrice - doar daca apesi pe Editare Stare Finala)
         *  -apoi se activeaza butoanele pentru algoritmi
         */




        //Inca nu functioneaza
        private void btnAdancime_Click(object sender, EventArgs e)
        {
            noduri.Clear();
            n = int.Parse(txtN.Text);
            Stack<int[,]> nodesToExplore = new Stack<int[,]>();

            // verificareMatrice(stareInitiala, stareFinala);
            var ceva = new byte[n * n];
            ceva = finalState;
            //SearchTreeNode currentNode = new SearchTreeNode(null, stareInitiala, 0);
            nodesToExplore.Push(stareActuala);
            do
            {
                if(nodesToExplore.Count == 0)
                {
                    MessageBox.Show("Nu exista o solutie");
                    break;
                }
                stareActuala = nodesToExplore.Pop();
                if(verificareStareFinala(stareActuala,stareFinala))
                {
                    MessageBox.Show("S-a gasit solutia");
                    break;
                }
                if(!exploredState.Contains(stareActuala))
                {
                    AddSuccesorAdancime(stareActuala);
                    exploredState.Add(stareActuala);
                }
            } while (true);

        }
        private void btnUniform_Click(object sender, EventArgs e)
        {
            noduri.Clear();
        }

        private void btnNivel_Click(object sender, EventArgs e)
        {
            noduri.Clear();
        }

        private void btnLimitare_Click(object sender, EventArgs e)
        {
            noduri.Clear();
        }

        private void btnInformata_Click(object sender, EventArgs e)
        {
            noduri.Clear();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {

            }
            foreach (var button in flowLayoutPanel1.Controls)
            {
                B_Click((Button)button, null);
            }
        }

        //trebuie modificat ca sa mearga pe vectori si nu pe matrice
        private bool verificareStareFinala(int[,] stareInitiala, int[,] stareFinala)
        {
            int i, j, flag = 1;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (stareInitiala[i, j] != stareFinala[i, j])
                    {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 1)
                return true;
            else
                return false;
        }

        private void AddSuccesorAdancime(int[,] stareActuala)
        {

        }
    }
}

/*
             * if (n > 1)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        List<Button> buttons = new List<Button>();
                        panelLeduri.Controls.Clear();
                        Button newButton = new Button();
                        newButton.Text = "HELLO";
                        newButton.Height = 50;
                        newButton.Width = 140;
                        buttons.Add(newButton);
                        
                        panelLeduri.Controls.Add(newButton);

                    }
                }
            }
            else
            {
                MessageBox.Show("Intorduceti un numar mai mare ca 2");
            }
            */
