using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDDL8_Keygen
{
    public partial class Form1 : Form
    {
        private string Path1 = @"C:\ProgramData\Detroit Diesel\Drumroll\Application Data\";
        private string Filena = "Settings.xml";
        private string FilenaBACKUP = "SettingsBACKUP.xml";
        private bool ParamExist = false;

        public Form1()
        {
            InitializeComponent();

            label3.Text = "";
            LoadParams();

            //Path1 = Application.StartupPath + "\\";
        }

        private void LoadParams()
        {
            ParamExist = false;

            if (File.Exists(Path1 + Filena))
            {
                string[] AllLines = File.ReadAllLines(Path1 + Filena);

                bool RLvl = false;
                bool WLvl = false;

                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Contains("ReadLevel</Property>") && !RLvl)
                    {
                        try
                        {
                            string ThisLine = AllLines[i + 3].Substring(AllLines[i + 3].IndexOf('>') + 1);
                            ThisLine = ThisLine.Substring(0, ThisLine.IndexOf('<'));
                            int ReadLevell = int.Parse(ThisLine);
                            numericUpDown1.Value = ReadLevell;
                            label3.Text = "ReadLevel Loaded!";
                            RLvl = true;
                        }
                        catch { }
                    }
                    if (AllLines[i].Contains("WriteLevel</Property>") && !WLvl)
                    {
                        try
                        {
                            string ThisLine = AllLines[i + 3].Substring(AllLines[i + 3].IndexOf('>') + 1);
                            ThisLine = ThisLine.Substring(0, ThisLine.IndexOf('<'));
                            int WriteLevell = int.Parse(ThisLine);
                            numericUpDown2.Value = WriteLevell;
                            label3.Text = "WriteLevel Loaded!";
                            WLvl = true;
                        }
                        catch { }
                    }
                }

                if (RLvl && WLvl)
                {
                    label3.Text = "Access Levels Loaded!";
                    ParamExist = true;
                }
            }
            else
            {
                MessageBox.Show("Cannot find parameters file: '" + Path1 + Filena + "'");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path1 + Filena))
            {
                string[] AllLines = File.ReadAllLines(Path1 + Filena);
                List<string> SaveLines = new List<string>();

                if (ParamExist)
                {
                    for (int i = 0; i < AllLines.Length; i++)
                    {
                        SaveLines.Add(AllLines[i]);
                        if (AllLines[i].Contains("ReadLevel</Property>"))
                        {
                            i++;
                            SaveLines.Add(AllLines[i]);
                            i++;
                            SaveLines.Add(AllLines[i]);
                            i++;
                            string ModdedLevel = "      <Object type=\"System.Int32, mscorlib\">" + numericUpDown1.Value.ToString() + "</Object>";
                            SaveLines.Add(ModdedLevel);
                        }
                        if (AllLines[i].Contains("WriteLevel</Property>"))
                        {
                            i++;
                            SaveLines.Add(AllLines[i]);
                            i++;
                            SaveLines.Add(AllLines[i]);
                            i++;
                            string ModdedLevel = "      <Object type=\"System.Int32, mscorlib\">" + numericUpDown2.Value.ToString() + "</Object>";
                            SaveLines.Add(ModdedLevel);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < AllLines.Length; i++)
                    {
                        if (AllLines[i].Contains("RegistrationKey</Property>"))
                        {
                            int Counting = 0;
                            while (Counting < 9)
                            {
                                SaveLines.Add(AllLines[i]);
                                Counting++;
                                i++;
                            }
                            SaveLines.Add("<Object type=\"DetroitDiesel.Settings.SettingInformation, CommonCS\">");
                            SaveLines.Add("    <Property name=\"Name\">ReadLevel</Property>");
                            SaveLines.Add("    <Property name=\"Group\">Client</Property>");
                            SaveLines.Add("    <Property name=\"Obj\">");
                            SaveLines.Add("      <Object type=\"System.Int32, mscorlib\">" + numericUpDown1.Value.ToString() + "</Object>");
                            SaveLines.Add("    </Property>");
                            SaveLines.Add("    <Property name=\"Encrypt\">False</Property>");
                            SaveLines.Add("</Object>");
                            SaveLines.Add("<Object type=\"DetroitDiesel.Settings.SettingInformation, CommonCS\">");
                            SaveLines.Add("    <Property name=\"Name\">WriteLevel</Property>");
                            SaveLines.Add("    <Property name=\"Group\">Client</Property>");
                            SaveLines.Add("    <Property name=\"Obj\">");
                            SaveLines.Add("      <Object type=\"System.Int32, mscorlib\">" + numericUpDown2.Value.ToString() + "</Object>");
                            SaveLines.Add("   </Property>");
                            SaveLines.Add("    <Property name=\"Encrypt\">False</Property>");
                            SaveLines.Add("</Object>");
                            SaveLines.Add("<Object type=\"DetroitDiesel.Settings.SettingInformation, CommonCS\">");
                            SaveLines.Add("   <Property name=\"Name\">ComputerDescription</Property>");
                            SaveLines.Add("   <Property name=\"Group\">Client</Property>");
                            SaveLines.Add("    <Property name=\"Obj\">");
                            SaveLines.Add("      <Object type=\"DetroitDiesel.Settings.StringSetting, CommonCS\">");
                            SaveLines.Add("        <Property name=\"Value\">BMDevs</Property>");
                            SaveLines.Add("      </Object>");
                            SaveLines.Add("   </Property>");
                            SaveLines.Add("    <Property name=\"Encrypt\">False</Property>");
                            SaveLines.Add("</Object>");

                            SaveLines.Add(AllLines[i]);
                        }
                        else
                        {
                            SaveLines.Add(AllLines[i]);
                        }
                    }
                }


                //Remake
                string[] AllLinesFinal = new string[SaveLines.Count];
                for (int i = 0; i < AllLinesFinal.Length; i++) AllLinesFinal[i] = SaveLines[i];

                File.Create(Path1 + FilenaBACKUP).Dispose();
                File.WriteAllBytes(Path1 + FilenaBACKUP, File.ReadAllBytes(Path1 + Filena));

                File.Create(Path1 + Filena).Dispose();
                File.WriteAllLines(Path1 + Filena, AllLinesFinal);

                LoadParams();

                MessageBox.Show("Done!");
            }
            else
            {
                MessageBox.Show("Cannot find file: '" + Path1 + Filena + "'");
            }
        }
    }
}
