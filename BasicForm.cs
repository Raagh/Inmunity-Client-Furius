using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace GeneiFurius
{
    public partial class BasicForm : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Keys vKeys);

        public static string savedWLC;
        public static string savedLH;
        Thread listeningThread;
        public static DateTime prevTime;
        public static bool isConnected;
        public static bool monsterIsInmo = false;

        public BasicForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            listeningThread = new Thread(new ThreadStart(PortListener.StartListening));
            if (StartButton.Text == "Start")
            {
                if(!string.IsNullOrEmpty(Character.cheaterName))
                {
                    Configuration.autoAimStatus = true;
                    ThrowSpell.Enabled = true;
                    UpdatePlayers.Enabled = true;
                    Configuration.autoAimStatus = true;
                    listeningThread.Start();
                    StartButton.Text = "Stop";
                    StatusLabel.Text = "ON";
                }
                else
                {
                    MessageBox.Show("Olvidaste hacer o confirmar la configuracion!");
                }
            }
            else if (StartButton.Text == "Stop")
            {
                ThrowSpell.Enabled = false;
                UpdatePlayers.Enabled = false;
                AutoATTimer.Enabled = false;
                AutoPotas.Enabled = false;
                TSpeed.Enabled = false;
                AutoPotasCheckBox.Checked = false;
                AutoAttackCheckBox.Checked = false;
                SpeedHackCheckBox.Checked = false;
                VerInvisCheckBox.Checked = false;
                AutoRemoCheckBox.Checked = false;
                RemoverCartelCheckBox.Checked = false;
                Configuration.autoAimStatus = false;
                SeguroInviCheckBox.Checked = false;
                listeningThread.Abort();
                StartButton.Text = "Start";
                StatusLabel.Text = "OFF";
            }
        }

        public static void ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            MessageBox.Show("Inyeccion de Dll!" + Environment.NewLine +
                "Error:" + (String.IsNullOrEmpty(error) ? "(none)" : error) + Environment.NewLine +
                "ExitCode: " + exitCode.ToString(), "Genei AO"
                );
            process.Close();
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
            ClosePreviousSessions();
            ExecuteCommand("X-Runner.cmd");
            StatusLabel.Text = "OFF";
            StatusLabel.ForeColor = Color.Green;
        }

        private void ClosePreviousSessions()
        {
            Process[] processes = Process.GetProcesses();
            if (processes.Length > 0)
            { 
                foreach(var item in processes)
                { 
                    if (item.ProcessName.Contains("ts3client"))
                        item.CloseMainWindow();
                }
            }             
        }

        private void BasicForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ThrowSpell_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Configuration.keyApocalipsis) == -32767) //Cast Apocalipsis
                CastSpell(Configuration.positionApocalipsis);

            if (GetAsyncKeyState(Configuration.keyDescarga) == -32767)
                CastSpell(Configuration.positionDescarga);

            if (GetAsyncKeyState(Configuration.keyInmovilizar) == -32767) //Cast Inmovilizar
                CastSpell(Configuration.positionInmovilizar);

            if (GetAsyncKeyState(Configuration.keyRemover) == -32767) //Cast Remover Paralisis on yourself
                CastRemo(Character.cheaterPosX, Character.cheaterPosY);

            if (GetAsyncKeyState(Configuration.keyTormenta) == -32767)
                CastSpell(Configuration.positionTormenta);

            if (GetAsyncKeyState(Configuration.keyMaldicion) == -32767)
                CastSpell(Configuration.positionMaldicion);

            if (GetAsyncKeyState(Keys.NumPad0) == -32767) 
                CastSpellMonsters();

            if (GetAsyncKeyState(Keys.NumPad1) == -32767) //Testing
            {
                //PortListener.SendToClient("||PRUEBA CONSOLA!!~215~44~44~0~0");
                //PortListener.SendToClient("ERRHola!");
                //PortListener.GetMessage(true, "ERRHola", PortListener.staticStream);
                //PortListener.SendToServer(";1Hola");
                //PortListener.SendToServer("USEYj?A");
                if (DataManagment.hideCheat)
                    DataManagment.hideCheat = false;
                else
                    DataManagment.hideCheat = true;
            }
        }  //Auto Aim Timer

        private static void CastSpellMonsters()
        {
            PortListener.SendToServer(savedLH);
            PortListener.SendToServer("UK1");
            PortListener.SendToServer(savedWLC);
            if (Configuration.remoCartel)
            {
                PortListener.SendToServer(";1  ");
            }
        }

        private static void CastSpell(string spellLocation) //Cast Any Spell
        {
            if (Configuration.autoAimStatus)
            {
                DateTime currentTime = DateTime.Now;
                if ((currentTime - prevTime).Seconds >= 0.5)
                {
                    PortListener.SendToServer(spellLocation);
                    PortListener.SendToServer("UK1");
                    Character foundPlayer = DataManagment.charactersListInAutoAim.Find(x => x.id == DataManagment.selectedCharacter);

                    if (foundPlayer != null)
                    {
                        if (!Configuration.seguroInvi || (Configuration.seguroInvi && !foundPlayer.isInvi))
                        {
                            PortListener.SendToServer("WLC" + foundPlayer.posX + "," + foundPlayer.posY + "," + 1);
                        }
                        else if (Configuration.seguroInvi && foundPlayer.isInvi)
                        {
                            DataManagment.ConsoleInterface("SeguroInvi -> You cant cast spells on an Invisible Player");
                            //PortListener.SendToClient("||GeneiAO> SeguroInvi -> You cant cast spells on an Invisible Player~10~236~18~0~0");
                        }
                    }
                    if (Configuration.remoCartel)
                    {
                        PortListener.SendToServer(";1  ");
                    }   
                }
               
            }
        }

        public async static void CastRemo(int posX,int posY) //Casts Remover Paralisis
        {
                await Task.Delay(300);
                PortListener.SendToServer(Configuration.positionRemover);
                PortListener.SendToServer("UK1");
                posY = posY - 1;
                PortListener.SendToServer("WLC" + posX + "," + posY + "," + 1);
                if (Configuration.remoCartel)
                {
                    PortListener.SendToServer(";1  ");
                }
        }

        private void UpdatePlayers_Tick(object sender, EventArgs e) //Updates players in list and labels
        {
            try
            {
                PlayersListBox.Items.Clear();
                PlayersListBox.Items.AddRange(DataManagment.charactersListInAutoAim.ToArray());
                PlayersListBox.ValueMember = "id";
                PlayersListBox.DisplayMember = "name";
                Character selectedCharacter = DataManagment.charactersListInAutoAim.Find(x => x.id == DataManagment.selectedCharacter);
                if (selectedCharacter != null)
                {
                    PlayersListBox.SelectedValue = selectedCharacter.id;
                    PlayersListBox.SelectedItem = selectedCharacter;
                }
                    
                CheaterNick_Label.Text = Character.cheaterName;
            }
            catch (Exception)
            {
                
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            ConfigForm config = new ConfigForm();
            config.Show();
            config.SetDesktopLocation(this.Location.X + this.Size.Width, this.Location.Y);
        }

        private void AutoATTimer_Tick(object sender, EventArgs e) //Auto Attack! yeah im lazy
        {
                PortListener.SendToServer("AT");
        }

        private void AutoAttackCheckBox_CheckedChanged(object sender) //CheckBox AutoAttack
        {
            if (AutoAttackCheckBox.Checked)
            {
                AutoATTimer.Enabled = true;
                AutoAttackCheckBox.Checked = true;
            }
            else
            {
                AutoAttackCheckBox.Checked = false;
                AutoATTimer.Enabled = false;
            }
        }

        private void RemoverCartelCheckBox_CheckedChanged(object sender) //CheckBox RemoverCartel
        {
            if (Configuration.remoCartel)
            {
                Configuration.remoCartel = false;
                RemoverCartelCheckBox.Checked = false;
            }
            else
            {
                Configuration.remoCartel = true;
                RemoverCartelCheckBox.Checked = true;
            }
        }

        private void AutoPotas_Tick(object sender, EventArgs e) //AutoPotas Timer
        {
            try
            {              
                bool isProcessOpen = MemoryManagement.IsProcessOpen("FuriusAO");
                if (isProcessOpen)
                {
                    IntPtr processHandle = new IntPtr();
                    Process process = Process.GetProcessesByName("FuriusAo")[0];
                    processHandle = MemoryManagement.OpenProcess(0x001F0FFF, false, process.Id); // Opens FuriusAO Process
                    int structAddress = 0x007C04C4;
                    int maxLife = MemoryManagement.Read(processHandle, structAddress);
                    int actualLife = MemoryManagement.Read(processHandle, structAddress + 4);
                    int maxMana = MemoryManagement.Read(processHandle, structAddress + 8);
                    int actualMana = MemoryManagement.Read(processHandle, structAddress + 12);

                    if (actualLife != 0)
                    {
                        if (actualLife != maxLife)
                        {
                            PortListener.SendToServer("USA>O=:");
                            PortListener.SendToServer("USAm~AA");
                            //PortListener.SendToServer("USA*;;:");
                            //PortListener.SendToServer("USABS=>");
                            PortListener.SendToServer("USEct@A");
                            PortListener.SendToServer("USEgxA;");
                        }
                        else if (actualMana != maxMana)
                        {
                            PortListener.SendToServer("USAf|A:");
                            PortListener.SendToServer("USA*@;:");
                            //PortListener.SendToServer("USA2H;B");
                            //PortListener.SendToServer("USAJ`><");
                            PortListener.SendToServer("USEI_>;");
                            PortListener.SendToServer("USEe{@C");
                        }
                    }
                }            
            }
            catch (Exception)
            {
                
            }
        } 

        private void AutoPotasCheckBox_CheckedChanged(object sender) //AutoPotas CheckBox
        {
            if (!AutoPotas.Enabled)
                AutoPotas.Enabled = true;
            else
                AutoPotas.Enabled = false;
        }

        private void VerInvisCheckBox_CheckedChanged(object sender)
        {
            if (!Configuration.verInvisOn)
                Configuration.verInvisOn = true;
            else
                Configuration.verInvisOn = false;
        } 

        private void AutoRemoCheckBox_CheckedChanged(object sender)
        {
            if (!Configuration.autoRemoOn)
                Configuration.autoRemoOn = true;
            else
                Configuration.autoRemoOn = false;
        }

        private void SpeedHackCheckBox_CheckedChanged(object sender) //SpeedHack CheckBox
        {
            if (!TSpeed.Enabled)
            {
                TSpeed.Enabled = true;
                SpeedHackCheckBox.Checked = true;
            }
            else
            {
                TSpeed.Enabled = false;
                SpeedHackCheckBox.Checked = false;
            }
        }

        private void TSpeed_Tick(object sender, EventArgs e) //SpeedHack Timer
        {
            if (GetAsyncKeyState(Keys.Up) == -32767) 
            {
                PortListener.SendToServer("M1");
                PortListener.SendToServer("RPU");
            }
            if (GetAsyncKeyState(Keys.Down) == -32767) 
            {
                PortListener.SendToServer("M3");
                PortListener.SendToServer("RPU");
            }
            if (GetAsyncKeyState(Keys.Left) == -32767) 
            {
                PortListener.SendToServer("M4");
                PortListener.SendToServer("RPU");
            }
            if (GetAsyncKeyState(Keys.Right) == -32767) 
            {
                PortListener.SendToServer("M2");
                PortListener.SendToServer("RPU");
            }
        }

        private void SeguroInviCheckBox_CheckedChanged(object sender)
        {
            if (!Configuration.seguroInvi)
                Configuration.seguroInvi = true;
            else
                Configuration.seguroInvi = false;
        }

        private void BasicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePreviousSessions();
            if (listeningThread != null)
                listeningThread.Abort();
        }

        private void ConsoleButton_Click(object sender, EventArgs e)
        {
            ConsoleForm console = new ConsoleForm();
            console.Show();
            console.SetDesktopLocation(this.Location.X + this.Size.Width, this.Location.Y);
            console.Owner = this.FindForm();
        }

        private void AutoEntrenar_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Monster selectedMonster = DataManagment.monstersListInMap.First();
            MovePlayer(selectedMonster);

            if(selectedMonster.posX <= (Character.cheaterPosX - 5))
            {
                if(!monsterIsInmo)
                {
                    if (!string.IsNullOrEmpty(Configuration.positionInmovilizar))
                    {
                        PortListener.SendToServer(Configuration.positionInmovilizar);
                        PortListener.SendToServer("UK1");
                        PortListener.SendToServer("WLC" + selectedMonster.posX + "," + selectedMonster.posY + "," + 1);
                        monsterIsInmo = true;
                    }
                }
                if (!string.IsNullOrEmpty(savedLH))
                {
                    PortListener.SendToServer(savedLH);
                    PortListener.SendToServer("UK1");
                    PortListener.SendToServer("WLC" + selectedMonster.posX + "," + selectedMonster.posY + "," + 1);
                }   
            }     
        }

        private void MovePlayer(Monster selectedMonster)
        {
            if (Character.cheaterPosX > 5 && Character.cheaterPosX < 90 && Character.cheaterPosY > 5 && Character.cheaterPosY < 90)
            {
                if (Character.cheaterPosX < selectedMonster.posX)
                {
                    PortListener.SendToServer("M2");
                    PortListener.SendToServer("RPU");
                    Character.cheaterPosX++;
                }
                else if (Character.cheaterPosX > selectedMonster.posX)
                {
                    PortListener.SendToServer("M4");
                    PortListener.SendToServer("RPU");
                    Character.cheaterPosX--;
                }

                if (Character.cheaterPosY < selectedMonster.posY)
                {
                    PortListener.SendToServer("M3");
                    PortListener.SendToServer("RPU");
                    Character.cheaterPosY++;
                }
                else if (Character.cheaterPosY > selectedMonster.posY)
                {
                    PortListener.SendToServer("M1");
                    PortListener.SendToServer("RPU");
                    Character.cheaterPosY--;
                }                      
            }     
        }

        private void AutoEntrenarCheckBox_CheckedChanged(object sender)
        {
            if (AutoEntrenar.Enabled)
            {
                //AutoEntrenar.Enabled = false;
                //Configuration.autoEntrenar = false;
            }               
            else
            {
                //AutoEntrenar.Enabled = true;
                //Configuration.autoEntrenar = true;
            }                
        }
    }
}
