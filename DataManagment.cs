using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace GeneiFurius
{
    public class DataManagment
    {

        public static List<Character> charactersListInMap = new List<Character>();
        public static List<Character> charactersListInAutoAim = new List<Character>();
        public static List<Monster> monstersListInMap = new List<Monster>();
        private static int _selectedCharacter;
        private static string _selectedName;      
        public static bool prepareForRemo = false;
        public static bool _hideCheat = false;

        public static bool hideCheat
        {
            get { return _hideCheat; }
            set 
            {
                _hideCheat = value;
                if (_hideCheat)
                {
                    foreach (Form item in Application.OpenForms)
                    {
                        item.Opacity = 0;                      
                    }
                    HideUnhideCheating(true);
                }
                else if (!_hideCheat)
                {
                    foreach (Form item in Application.OpenForms)
                    {
                        item.Opacity = 100;                      
                    }
                    HideUnhideCheating(false);
                }
            }
        }


        public static string selectedName  //Here we show on console everytime the selectedName Changes
        {
            get { return _selectedName; }
            set
            {
                if (_selectedName != value)
                {
                    _selectedName = value;
                    if (!string.IsNullOrEmpty(_selectedName))
                    {
                        DataManagment.ConsoleInterface("GeneiAO> Selected Player : " + _selectedName);
                    }
                }
            }
        }

        public static int selectedCharacter //Here we set TARGET color and name everytime selectedPlayer changes
        {
            get { return _selectedCharacter; }
            set
            {
                //FIRST WE CHANGE THE CURRENT SELECTED CHARACTER NAME BACK TO NORMAL
                Character foundPlayer = charactersListInMap.Find(x => x.id == _selectedCharacter);
                if (foundPlayer != null)
                {
                    if (!hideCheat)
                    {
                        string packetFakeQDL = "QDL" + foundPlayer.id;
                        string packetFakeSX = string.Empty;
                        if (foundPlayer.isInvi)
                        {
                            if(Configuration.verInvisOn)
                                packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [INVI]" + "," + foundPlayer.faction + ",0,0";
                            else
                                packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + "," + foundPlayer.faction + ",1,0";
                        }
                        else
                        {
                            packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + "," + foundPlayer.faction + ",0,0";
                        }                       
                        PortListener.SendToClient(packetFakeQDL);
                        PortListener.SendToClient(packetFakeSX);
                    }
                }
                // NOW WE SET UP THE NEW SELECTED CHARACTER AND THEN CHANGE HIS NAME AND COLOR TO SELECTED!.
                _selectedCharacter = value;
                foundPlayer = null;
                foundPlayer = charactersListInMap.Find(x => x.id == _selectedCharacter);
                if (foundPlayer != null)
                {
                    if (!hideCheat)
                    {
                        string packetFakeQDL = "QDL" + foundPlayer.id;
                        string packetFakeSX = "";
                        if (foundPlayer.isInvi)
                        {
                            packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + "[INVI] [TARGET]" + "," + "1" + ",0,0";
                        }
                        else
                        {
                            packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [TARGET]" + "," + "1" + ",0,0";
                        }
                        PortListener.SendToClient(packetFakeQDL);
                        PortListener.SendToClient(packetFakeSX);
                    }
                }
            }
        }
        

        private bool VerifyAdmins(string packetString)
        {
            bool found = false;
            //Con el tiempo agregare todos los consejeros q no son gms pero te pueden banear
            List<string> listConsejeros = new List<string>();
            listConsejeros.Add("muddy");
            listConsejeros.Add("furiusao staff");
            listConsejeros.Add("judith");

            string lowerString = packetString.ToLower();

            foreach (string item in listConsejeros)
            {
                if (lowerString.Contains(item))
                {
                    found = true;
                    break;
                }                
            }

            if (found)
                return true;
            else
                return false;
        }


        private string GetPlayerName(int p)
        {
            foreach (var item in charactersListInMap)
            {
               if( item.id == p)
               {
                   return item.name;
               }
            }

            return "";
        }

        private void GetNextCharacter()
        {   
            Character foundPlayer = charactersListInAutoAim.Find(x => x.id == selectedCharacter);
            if (foundPlayer != null && foundPlayer != charactersListInAutoAim.Last())
            {
                var index = charactersListInAutoAim.IndexOf(foundPlayer);
                Character nextPlayer = charactersListInAutoAim[index + 1];
                selectedCharacter = nextPlayer.id;
                selectedName = nextPlayer.name;
            }
            else if (foundPlayer != null && foundPlayer == charactersListInAutoAim.Last())
            {
                selectedCharacter = charactersListInAutoAim.First().id;
                selectedName = charactersListInAutoAim.First().name;                
            }
            else if( foundPlayer == null )
            {
                if( charactersListInAutoAim.Count() != 0 )
                {
                    selectedCharacter = charactersListInAutoAim.First().id;
                    selectedName = charactersListInAutoAim.First().name;
                }
                else if ( charactersListInAutoAim.Count() == 0 )
                {
                    DataManagment.ConsoleInterface("GeneiAO> No characters in range!");
                    //PortListener.SendToClient("||GeneiAO> No characters in range!~10~236~18~0~0");
                }
            }
        }

        private void AddPlayer(string[] split)
        {
            bool alreadyAdded = false;
            Character newPlayer = new Character();
            newPlayer.id = int.Parse(split[3]);
            newPlayer.posX = int.Parse(split[4]);
            newPlayer.posY = int.Parse(split[5]);
            newPlayer.name = split[11];
            newPlayer.faction = int.Parse(split[12]);
            foreach (var item in charactersListInMap)
            {
                if (item.id == newPlayer.id && item.name == newPlayer.name)
                {
                    alreadyAdded = true;
                }
            }
            if (!alreadyAdded)
            {
                charactersListInMap.Add(newPlayer);
            }
        }

        public static void ConsoleInterface(string message)
        {
            ConsoleForm console = (ConsoleForm)Application.OpenForms["ConsoleForm"];
            console.PrintMessageConsole(message);
        }

        private static void HideUnhideCheating(bool option)
        {
                if (option)
                {
                        foreach (Character player in DataManagment.charactersListInMap)
                        {
                            if (player.isInvi)
                            {
                                //string packetFakeQDL = "QDL" + player.id;
                                //string packetFakeSX = "SX,,," + player.id + "," + player.posX + "," + player.posY + ",10,1,0,999,3," + player.name + "," + player.faction + ",1,0";
                                //PortListener.SendToClient(packetFakeQDL);
                                //PortListener.SendToClient(packetFakeSX); 
                                PortListener.SendToClient("V3" + player.id + ",1");
                            }
                        }
                        Character foundPlayer = charactersListInMap.Find(x => x.id == _selectedCharacter);
                        if (foundPlayer != null)
                        {
                                string packetFakeQDL = "QDL" + foundPlayer.id;
                                string packetFakeSX = string.Empty;
                                if (foundPlayer.isInvi)
                                {
                                    packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + "," + foundPlayer.faction + ",1,0";
                                }
                                else
                                {
                                    packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + "," + foundPlayer.faction + ",0,0";
                                }
                                PortListener.SendToClient(packetFakeQDL);
                                PortListener.SendToClient(packetFakeSX);
                        }
                }
                else if (!option)
                {
                    Character selectedCharacter = DataManagment.charactersListInAutoAim.Find(x => x.id == DataManagment.selectedCharacter);
                    if (selectedCharacter != null)
                    {
                        if (selectedCharacter.isInvi)
                        {
                            string packetFakeQDL = "QDL" + selectedCharacter.id;
                            string packetFakeSX = "RM,,," + selectedCharacter.id + "," + selectedCharacter.posX + "," + selectedCharacter.posY + ",10,1,0,999,3," + selectedCharacter.name + " [INVI] [TARGET]" + "," + "1" + ",0,0";
                            PortListener.SendToClient(packetFakeQDL);
                            PortListener.SendToClient(packetFakeSX);
                        }
                        else
                        {
                            string packetFakeQDL = "QDL" + selectedCharacter.id;
                            string packetFakeSX = "RM,,," + selectedCharacter.id + "," + selectedCharacter.posX + "," + selectedCharacter.posY + ",10,1,0,999,3," + selectedCharacter.name + " [TARGET]" + "," + "1" + ",0,0";
                            PortListener.SendToClient(packetFakeQDL);
                            PortListener.SendToClient(packetFakeSX);
                        }
                    
                    }
                    if(Configuration.verInvisOn)
                    {
                        foreach (Character player in DataManagment.charactersListInMap)
                        {
                            if ((player.isInvi && player.id != Character.cheaterId))
                            {
                                string packetFakeQDL = "QDL" + player.id;
                                string packetFakeSX = "RM,,," + player.id + "," + player.posX + "," + player.posY + ",10,1,0,999,3," + player.name + " [INVI]" + "," + player.faction + ",0,0";
                                PortListener.SendToClient(packetFakeQDL);
                                PortListener.SendToClient(packetFakeSX);
                            }
                        }
                    }
                }
  
        }


        internal string AnalizeRecvPackets(string packetString)
        {
            bool returnPacket = true;  // Return always starts as true;

            if (packetString.StartsWith("PAIN")) //If the GameMaster is taking a screenshot of you
                hideCheat = true;
            if (packetString.StartsWith("V3")) //If the players throws Invisibilidad
            {
                    string[] split = packetString.Split(new Char[] { ',' });
                    string playerId = split[0].Substring(2);
                    int Invisible = int.Parse(split[1]);
                    Character foundPlayer = charactersListInMap.Find(x => x.id == int.Parse(playerId));
                    if (foundPlayer != null)
                    {
                        Character foundPlayerAutoAim = charactersListInAutoAim.Find(x => x.id == foundPlayer.id);
                        if (Invisible == 1)
                        {
                            foundPlayer.isInvi = true;
                            if (foundPlayerAutoAim != null)
                                foundPlayerAutoAim.isInvi = true;
                            if (!hideCheat)
                            {
                                if (Configuration.verInvisOn)
                                {
                                    string packetFakeQDL = "QDL" + foundPlayer.id;
                                    string packetFakeSX = string.Empty;
                                    if (foundPlayer.id != Character.cheaterId)
                                        packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [INVI]" + "," + foundPlayer.faction + ",0,0";
                                    if (!string.IsNullOrEmpty(packetFakeSX))
                                    {
                                        PortListener.SendToClient(packetFakeQDL);
                                        PortListener.SendToClient(packetFakeSX);
                                        returnPacket = false;
                                    }                                   
                                }
                            }
                        }
                        else if (Invisible == 0)
                        {
                            foundPlayer.isInvi = false;
                            if (foundPlayerAutoAim != null)
                                foundPlayerAutoAim.isInvi = false;

                            if (foundPlayer.id == selectedCharacter)
                            {
                                string packetFakeQDL = "QDL" + foundPlayer.id;
                                string packetFakeSX = "";  
                                packetFakeSX = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [TARGET]" + "," + "1" + ",0,0";
                                PortListener.SendToClient(packetFakeQDL);
                                PortListener.SendToClient(packetFakeSX);
                            }                           
                        }
                    }
            }
            if (packetString.StartsWith("HO")) //Checks if there is any Chest in the map
            {
                string[] split = packetString.Split(new Char[] { ',' });
                string itemID = split[0];
                itemID = itemID.Substring(2);

                if (itemID.Contains("24078"))
                {
                    DataManagment.ConsoleInterface("Cofre encontrado en x:" + split[1] + " y: " + split[2]);
                }
            }
            if (packetString.StartsWith("SHS")) //Gets the spells positions
            {
                string[] split = packetString.Split(new Char[] { ',' });
                string spellPosition = split[0];
                string spellName = split[2];
                spellPosition = spellPosition.Substring(3);
                if (spellName.Contains("Apocalipsis"))
                {
                    Configuration.positionApocalipsis = "LH" + spellPosition;
                }
                if (spellName.Contains("Inmovilizar"))
                {
                    Configuration.positionInmovilizar = "LH" + spellPosition;
                }
                if (spellName.Contains("Remover paralisis"))
                {
                    Configuration.positionRemover = "LH" + spellPosition;
                }
                if (spellName.Contains("Descarga"))
                {
                    Configuration.positionDescarga = "LH" + spellPosition;
                }
                if (spellName.Contains("Tormenta"))
                {
                    Configuration.positionTormenta = "LH" + spellPosition;
                }
                if (spellName.Contains("Maldicion Fúriosa"))
                {
                    Configuration.positionMaldicion = "LH" + spellPosition;
                }
            }
            if (packetString.Contains("P9")) //If you Get Inmovilizado
            {
                Character.cheaterParalizado = true;
            }
            if (packetString.Contains("P8")) //If you Get Removed from inmo spell
            {
                Character.cheaterParalizado = false;
            }

            if (packetString.StartsWith("PU"))  // If the player Gets paralized, Cast Remover Paralisis.
            {
                if (Character.cheaterParalizado)
                {
                    string[] split = packetString.Split(new Char[] { ',' });
                    int posX = int.Parse(split[0].Substring(2));
                    int posY = int.Parse(split[1]);
                    Character.cheaterPosX = int.Parse(split[0].Substring(2));
                    Character.cheaterPosY = int.Parse(split[1]);
                    if (Configuration.autoRemoOn)
                    {
                        BasicForm.CastRemo(posX, posY);
                    }
                    Character.cheaterParalizado = false;
                }
            }
            if (packetString.StartsWith("RM"))  //Players enter the map
            {
                string[] split = packetString.Split(new Char[] { ',' });
                if (split.Count() > 11)
                {
                    int idSX = int.Parse(split[3]);

                    if (VerifyAdmins(packetString)) //Game master is in the Map
                    {
                        DataManagment.ConsoleInterface("GeneiAO>GM -> " + split[11] + " en X:" + split[4] + " Y:" + split[5]);                    
                    }
                    if (!VerifyAdmins(packetString) && idSX != Character.cheaterId)
                        AddPlayer(split);
                    
                    if (split[13] == "1")  //If the player is invisible
                    {
                        int playerId = int.Parse(split[3]);
                        Character foundPlayer = charactersListInMap.Find(x => x.id == playerId);                     
                        if (foundPlayer != null)
                        {
                            foundPlayer.isInvi = true;
                            if (Configuration.verInvisOn)
                            {
                                if (!hideCheat)
                                {
                                    //string packetFakeQDL = "QDL" + foundPlayer.id;
                                    //string packetFakeSX = "SX,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [INVI]" + "," + foundPlayer.faction + ",0,0";
                                    //PortListener.SendToClient(packetFakeQDL);
                                    //PortListener.SendToClient(packetFakeSX);
                                    //returnPacket = false;
                                    packetString = string.Empty;
                                    //packetString = "RM,,," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + ",10,1,0,999,3," + foundPlayer.name + " [INVI]" + "," + foundPlayer.faction + ",0,0";
                                    packetString = split[0] + "," + split[1] + "," + split[2] + "," + foundPlayer.id + "," + foundPlayer.posX + "," + foundPlayer.posY + "," + split[6] + "," + split[7] + "," + split[8] + "," + split[9] + "," + split[10] + "," + foundPlayer.name + " [INVI]" + "," + foundPlayer.faction + "," + "0" + "," + split[14];
                                }
                            }
                        }
                    }
                    else if (split[13] == "0")
                    {
                        int playerId = int.Parse(split[3]);
                        Character foundPlayer = charactersListInMap.Find(x => x.id == playerId);
                        if (foundPlayer != null)
                            foundPlayer.isInvi = false;
                    }

                    if (split[11].Contains(Character.cheaterName)) //Cheaters DATA based on his name
                    {
                        Character.cheaterId = int.Parse(split[3]);
                        Character.cheaterPosX = int.Parse(split[4]);
                        Character.cheaterPosY = int.Parse(split[5]);
                        Character.cheaterFaction = int.Parse(split[12]);

                        if (Character.cheaterFaction == 3)  // If he is Criminal then asociatedFaccion is Elite Criminal
                            Character.cheaterAsociatedFaction = 18;
                        else if (Character.cheaterFaction == 18)
                            Character.cheaterAsociatedFaction = 3;

                        if (Character.cheaterFaction == 2) // If he is Ciudadano then asociatedFaccion is Elite Real
                            Character.cheaterAsociatedFaction = 15;
                        else if (Character.cheaterFaction == 15)
                            Character.cheaterAsociatedFaction = 2;
                    }
                }
                //else if (split.Count() < 11)
                //{
                //    Monster newMonster = new Monster();
                //    newMonster.ID = int.Parse(split[3]);
                //    newMonster.type = "Monster";
                //    newMonster.posX = int.Parse(split[4]);
                //    newMonster.posY = int.Parse(split[5]);
                //    monstersListInMap.Add(newMonster);
                //}
            }
            if (packetString.StartsWith("QDL")) //Players exit the map
            {
                string[] split = packetString.Split(new Char[] { 'L' });
                if (split[0] == "QD")
                {
                    Character foundPlayerInAutoAim = charactersListInAutoAim.Find(x => x.id == int.Parse(split[1]));
                    Character foundPlayerInMap = charactersListInMap.Find(x => x.id == int.Parse(split[1]));
                    //Monster foundMonster = monstersListInMap.Find(x => x.ID == int.Parse(split[1]));
                    charactersListInAutoAim.Remove(foundPlayerInAutoAim);
                    charactersListInMap.Remove(foundPlayerInMap);
                    //monstersListInMap.Remove(foundMonster);
                }
            }
            if (packetString.StartsWith("CM")) //Cheater changes map
            {
                charactersListInAutoAim.Clear();
                charactersListInMap.Clear();
                //monstersListInMap.Clear();
                selectedCharacter = 0;
                selectedName = string.Empty;
            }
            if (packetString.StartsWith("MP")) //Players Movement
            {
                string[] split = packetString.Split(new Char[] { ',' });
                string idPlayerString = split[0];
                idPlayerString = idPlayerString.Substring(2);
                int idPlayer = int.Parse(idPlayerString);
                Character foundPlayerMap = charactersListInMap.Find(x => x.id == idPlayer);
                if (foundPlayerMap != null)
                {
                    if (!VerifyAdmins(foundPlayerMap.name))
                    {
                        if (charactersListInAutoAim.Count != 0)
                        {
                            Character foundPlayerAutoAim = charactersListInAutoAim.Find(x => x.id == foundPlayerMap.id);
                            if (foundPlayerAutoAim != null)
                            {
                                foundPlayerAutoAim.posX = int.Parse(split[1]);
                                foundPlayerAutoAim.posY = int.Parse(split[2]);
                            }
                            else if (foundPlayerAutoAim == null)
                            {
                                if ((foundPlayerMap.faction != Character.cheaterFaction && foundPlayerMap.faction != Character.cheaterAsociatedFaction) || Character.cheaterFaction == 5) //If the faction is diferent from the cheater Add it to AutoAim
                                {
                                    Character newPlayer = new Character();
                                    newPlayer.id = foundPlayerMap.id;
                                    newPlayer.name = GetPlayerName(newPlayer.id);
                                    newPlayer.posX = int.Parse(split[1]);
                                    newPlayer.posY = int.Parse(split[2]);
                                    newPlayer.isInvi = foundPlayerMap.isInvi;
                                    charactersListInAutoAim.Add(newPlayer);
                                }
                            }
                        }
                        else if (charactersListInAutoAim.Count == 0)
                        {
                            if ((foundPlayerMap.faction != Character.cheaterFaction && foundPlayerMap.faction != Character.cheaterAsociatedFaction) || Character.cheaterFaction == 5) //If the faction is diferent from the cheater Add it to AutoAim
                            {
                                Character newPlayer = new Character();
                                newPlayer.id = foundPlayerMap.id;
                                newPlayer.name = GetPlayerName(newPlayer.id);
                                newPlayer.posX = int.Parse(split[1]);
                                newPlayer.posY = int.Parse(split[2]);
                                newPlayer.isInvi = foundPlayerMap.isInvi;
                                charactersListInAutoAim.Add(newPlayer);
                                selectedCharacter = newPlayer.id;
                                selectedName = newPlayer.name;
                            }
                        }
                    }
                }
                //else if (foundPlayerMap == null)
                //{
                //    Monster foundMonster = monstersListInMap.Find(x => x.ID == idPlayer);
                //    foundMonster.posX = int.Parse(split[1]);
                //    foundMonster.posY = int.Parse(split[2]);
                //}
            }

            if(returnPacket)
            {
                PortListener.SendToClient(packetString);
                return packetString;
            }

            return "";  
        }

        internal string AnalizeSendPackets(string packetString)
        {
            if (Configuration.autoEntrenar)
            {
                if (packetString.Contains("CHEA1"))
                {
                    Character.cheaterPosY++;
                    PortListener.SendToServer("M3");
                    PortListener.SendToServer("RPU");
                }
                else if (packetString.Contains("CHEA3"))
                {
                    Character.cheaterPosY--;
                    PortListener.SendToServer("M1");
                    PortListener.SendToServer("RPU");
                }
                else if (packetString.Contains("CHEA4"))
                {
                    Character.cheaterPosX++;
                    PortListener.SendToServer("M2");
                    PortListener.SendToServer("RPU");
                }
                else if (packetString.Contains("CHEA2"))
                {
                    Character.cheaterPosX--;
                    PortListener.SendToServer("M4");
                    PortListener.SendToServer("RPU");
                }
            }
            if (packetString.Contains("WLC")) //If you Send Spell - RemoveCartel
            {
                if (Configuration.remoCartel)
                    PortListener.SendToServer(";1  ");

                BasicForm.savedWLC = packetString;
            }
            if (packetString.Contains("LH")) //This saves the last Spell casted!
                BasicForm.savedLH = packetString;
            if (packetString.Contains("RC")) //Right Click Changes selected character in autoaim
                if (Configuration.autoAimStatus)
                    GetNextCharacter();

            if (packetString == "/salir") //Command /Salir resets lists
            {
                charactersListInAutoAim.Clear();
                charactersListInMap.Clear();
            }

            if (packetString.Contains("PRC"))
	        {
                packetString = string.Empty;
                packetString = "PRC @ Inicio:65672 @ Furius AO V 5.5.:1704776 @ FúriusAO:2032840 @ Skype™ - amolinari:1573518 @ Games:591016 @ Program Manager:131206";		        
	        }

	        if (packetString.Contains("PRR"))
	        {
                packetString = string.Empty;
                packetString = @"PRRC:\\FuriusAO\\FuriusAO.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Skype\\Phone\\Skype.exe%C:\\Program Files(x86)\\Skype\\Phone\\Skype.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Prog찀ᣉ   es(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Program Files(x86)\\Common Files\\Java\\Java Update\\jusched.exe%C:\\Program Files(x86)\\Common Files\\Java\\Java Update\\jusched.exe%C:\\Program Files(x86)\\Common Files\\Java\\Java Update\\jusched.exe%C:\\Program Files(x86)\\360\\Total Security\\safemon\\QHSafeTray.exe%C:\\Program Files(x86)\\AVG Web TuneUp\\vprot.exe%C:\\Program Files(x86)\\Intel\\Intel(R) USB 3.0 eXtensible Host Controller Driver\\Application\\iusb3mon.exe%C:\\Program Files(x86)\\Hotkey\\HkeyTray.exe%C:\\Program Files(x86)\\Hotkey\\HkeyTray.exe%C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe%C:\\Users\\Pferraggi\\AppData\\Local\\Microsoft\\BingSvc\\BingSvc.exe%C:\\Program Files(x86)\\ChiconyCam\\CECAPLF.exe%";
	        }

            PortListener.SendToServer(packetString);
            return packetString;
        }


    }
}
