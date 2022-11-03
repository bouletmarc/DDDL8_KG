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

        public Form1()
        {
            InitializeComponent();

            //Path1 = Application.StartupPath + "\\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path1 + Filena))
            {
                string[] AllLines = File.ReadAllLines(Path1 + Filena);
                List<string> SaveLines = new List<string>();

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
                        SaveLines.Add("      <Object type=\"System.Int32, mscorlib\">10</Object>");
                        SaveLines.Add("    </Property>");
                        SaveLines.Add("    <Property name=\"Encrypt\">False</Property>");
                        SaveLines.Add("</Object>");
                        SaveLines.Add("<Object type=\"DetroitDiesel.Settings.SettingInformation, CommonCS\">");
                        SaveLines.Add("    <Property name=\"Name\">WriteLevel</Property>");
                        SaveLines.Add("    <Property name=\"Group\">Client</Property>");
                        SaveLines.Add("    <Property name=\"Obj\">");
                        SaveLines.Add("      <Object type=\"System.Int32, mscorlib\">10</Object>");
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


                //Remake
                string[] AllLinesFinal = new string[SaveLines.Count];
                for (int i = 0; i < AllLinesFinal.Length; i++) AllLinesFinal[i] = SaveLines[i];

                File.Create(Path1 + FilenaBACKUP).Dispose();
                File.WriteAllBytes(Path1 + FilenaBACKUP, File.ReadAllBytes(Path1 + Filena));

                File.Create(Path1 + Filena).Dispose();
                File.WriteAllLines(Path1 + Filena, AllLinesFinal);

                MessageBox.Show("Done!");
            }
            else
            {
                MessageBox.Show("Cannot find file: '" + Path1 + Filena + "'");
            }
        }
    }
}
