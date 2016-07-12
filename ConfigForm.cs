using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneiFurius
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ApocaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyApocalipsis = e.KeyCode;
            ApocaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyApocalipsis);  
        }

        private void DescargaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyDescarga = e.KeyCode;
            DescargaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyDescarga);
        }

        private void InmoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyInmovilizar = e.KeyCode;
            InmoTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyInmovilizar);
        }

        private void RemoverTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyRemover = e.KeyCode;
            RemoverTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyRemover);
        }

        private void TormentaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyTormenta = e.KeyCode;
            TormentaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyTormenta);
        }

        private void MaldicionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Configuration.keyMaldicion = e.KeyCode;
            MaldicionTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyMaldicion);
        }

        private void SaveConfigButton_Click(object sender, EventArgs e)
        {
            Character.cheaterName = NameTextBox.Text;
            using (StreamWriter sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "//config.txt"))
            {

                sw.WriteLine("-----------------------------------------------");
                sw.WriteLine("Genei AO Configuration File * Argentum Online *");
                sw.WriteLine("-----------------------------------------------");
                sw.WriteLine(Character.cheaterName + ";" + Configuration.keyApocalipsis.ToString() + ";" + Configuration.keyDescarga.ToString() + ";" + Configuration.keyInmovilizar.ToString() + ";" + Configuration.keyRemover.ToString() + ";" + Configuration.keyTormenta.ToString() );
            }   
            MessageBox.Show("Saved");
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
            ApocaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyApocalipsis);
            DescargaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyDescarga);
            InmoTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyInmovilizar);
            RemoverTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyRemover);
            TormentaTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyTormenta);
            MaldicionTextBox.Text = Enum.GetName(typeof(Keys), Configuration.keyMaldicion);
            NameTextBox.Text = Character.cheaterName;            
        }

        private void LoadConfig()
        {
            string s;
            string path = AppDomain.CurrentDomain.BaseDirectory + "//config.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(File.OpenRead(path)))
                {
                    int Rows = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (Rows < 3)
                        {
                            Rows++;
                            continue;
                        }
                        else
                        {
                            string[] split = s.Split(new Char[] { ';' });
                            Character.cheaterName = split[0];
                            Configuration.keyApocalipsis = (Keys)Enum.Parse(typeof(Keys), split[1]);
                            Configuration.keyDescarga = (Keys)Enum.Parse(typeof(Keys), split[2]);
                            Configuration.keyInmovilizar = (Keys)Enum.Parse(typeof(Keys), split[3]);
                            Configuration.keyRemover = (Keys)Enum.Parse(typeof(Keys), split[4]);
                            Configuration.keyTormenta = (Keys)Enum.Parse(typeof(Keys), split[5]);
                        }
                    }
                }
            }
        }
    }
}
