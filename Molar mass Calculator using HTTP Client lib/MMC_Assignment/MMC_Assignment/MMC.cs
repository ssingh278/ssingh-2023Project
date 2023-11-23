//***********************************************************************************
//Program: MMC_Assignment
//Description:  Molecular Weight Calculator (LINQ Assignment)
//Date: 19-Oct-2022
//Group: Sahil Sharma,Sharry Singh
//Course: CMPE2800
//Class: CNT.A01(fall 2023)
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.IO;
using MMC_Assignment.Properties;

namespace MMC_Assignment
{
    public partial class MMC : Form
    {
        /// <summary>
        /// Class containing the definition of the Atom
        /// </summary>
        public class Atom
        {
            public int AtomicNumber { get; set; } //Atomic number 
            public string Name { get; set; }      // name of elements
            public string Symbol { get; set; }    // Symbol of elements
            public float MolarMass { get; set; }  // Molars of Elements
        }
        List<Atom> _atomList = new List<Atom>();// list to store info of all the atoms
        BindingSource _bindingSource = new BindingSource();//binding source will be use to bind the data to Grid view
        public MMC()
        {
            InitializeComponent();
        }

        private void UI_ChemicalFormula_tbx_TextChanged(object sender, EventArgs e)
        {

            Dictionary<string, int> elementDictionary = new Dictionary<string, int>();

            string userInput = UI_ChemicalFormula_tbx.Text;

            string pattern = @"([A-Z][a-z])(-?\d*)|([A-Z])(-?\d*)";

            MatchCollection matches = Regex.Matches(userInput, pattern);
            foreach (Match match in matches)
            {
                string count = null;
                string element = null;
                if (match.Groups[1].Success)
                {
                    element = match.Groups[1].Value;
                    count = match.Groups[2].Value;
                }
                else if (match.Groups[3].Success)
                {
                    element = match.Groups[3].Value;
                    count = match.Groups[4].Value;
                }

                if (count != null)
                {
                    int int_count = int.TryParse(count, out int_count) ? int_count : 1;
                    if(int_count>= 999999999)
                    {
                        MessageBox.Show("999999999 is the max number allowed, it will not measure after that");
                        UI_ChemicalFormula_tbx.Clear();
                        _bindingSource.DataSource = null;
                        UI_DataGridView.DataSource = _bindingSource;
                        UI_MolarMass_Tbx.BackColor = Color.White;
                        UI_MolarMass_Tbx.Text = "";
                        break;
                    }
                    if (int_count > 0)
                    {
                        if (!elementDictionary.ContainsKey(element))
                        {
                            if (element.Length > 1)
                                elementDictionary[$"{element[0]}"] = 1;
                            //this happens always
                            elementDictionary[element] = int_count;
                        }
                        else
                        {
                            elementDictionary[element] += int_count;
                        }
                    }
                }

            }

            var elements = from kvp in elementDictionary
                           join s in _atomList on kvp.Key equals s.Symbol
                           select new
                           {
                               AtomicNumber = s.AtomicNumber,
                               Count = kvp.Value,
                               Name = s.Name,
                               Symbol = s.Symbol,
                               MolarMass = s.MolarMass,
                               MassInFormula = s.MolarMass * kvp.Value
                           };

            if (elements.Count() > 0)
            {
                _bindingSource.DataSource = from item in elements
                                            select new
                                            {
                                                Element = item.Name,
                                                Count = item.Count,
                                                Mass = item.MolarMass,
                                                TotalMass = item.MassInFormula
                                            };
                UI_DataGridView.DataSource = _bindingSource;
                UI_DataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                UI_DataGridView.Columns[3].HeaderText = "Total Mass";
            }
            else
            {
                _bindingSource.DataSource = null;
                UI_DataGridView.DataSource = _bindingSource;
            }

            string pattern2 = @"[A-Z][a-z]|[A-Z]|[a-z]";
            MatchCollection matchesForColor = Regex.Matches(userInput, pattern2);

            int color_value = 0;
            int discrepancy = 0;
            foreach (Match item in matchesForColor)
            {
                foreach (var element in elements)
                {
                    if (element.Symbol == item.Value)
                    {
                        color_value++;
                    }
                    if (element.Symbol == $"{item.Value[0]}")
                    {
                        color_value++;
                        discrepancy++; //increasing it so that the yellow color is displayed, the regex will take Hh as one item and make the backcolor red because H won't be equal to Hh,but if I just check for item[0] and increase the color value, it will make it green, so had to include discrepancy
                    }
                }

            }

            if (color_value == 0)
                UI_MolarMass_Tbx.BackColor = Color.Red;
            else if (color_value > 0 && color_value < matchesForColor.Count + discrepancy)
                UI_MolarMass_Tbx.BackColor = Color.Yellow;
            else
                UI_MolarMass_Tbx.BackColor = Color.Green;

            UI_MolarMass_Tbx.Text = $"{elements.Sum(x => x.MassInFormula)} g/mol";
            //If empty then back to default
            if (userInput.Length == 0)
            {
                UI_MolarMass_Tbx.BackColor = Color.White;
                UI_MolarMass_Tbx.Text = "";
            }
        }

        /// <summary>
        /// On Load event for the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MMC_Load(object sender, EventArgs e)
        {
            GetData();//calling the API function to get the data
            this.ActiveControl = UI_ChemicalFormula_tbx;
            UI_DataGridView.RowHeadersVisible = false;
        }

        /// <summary>
        /// This function will make a web call to get all the data as String
        /// and then parse into the JSON object and making list out of it
        /// </summary>
        private async void GetData()
        {
            try
            {
                HttpClient httpClient = new HttpClient();//new HTTP clietn object
                string apiUrl = "https://apiweatherforcast.azurewebsites.net/api/AtomControllers";//url for API
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);//awiting till We got response back 
                if (response.IsSuccessStatusCode)//if status code is 200 , then will populate the master Atom list
                {
                    string data = await response.Content.ReadAsStringAsync();//reading all the data
                    _atomList = JsonConvert.DeserializeObject<List<Atom>>(data);//desearilizing the data using JSON Newston lib
                }
                else//if APPI call is unsuccessfull then will call a ReadFile function to read all the data from the .txt file 
                {
                    MessageBox.Show("API request failed: " + response.ReasonPhrase, "Error");
                    ReadFile();
                }
            }
            catch (HttpRequestException ex)//if APPI call is unsuccessfull then will call a ReadFile function to read all the data from the.txt file
            {
                MessageBox.Show("API request failed: " + ex.Message, "Error");
                ReadFile();
            }
        }
        /// <summary>
        /// This is the backup method if , API fails to run it will call this method 
        /// to fill the list 
        /// </summary>
        private void ReadFile()
        {

            MessageBox.Show("Now reading the file. ");//displaying the message to the user 
            string data = Resources.AtomsData;//getting the content of file as string
            _atomList = JsonConvert.DeserializeObject<List<Atom>>(data);//making a new list of  Atoms

        }

        /// <summary>
        /// This function will sort all the data by Atom Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_SortByName_Btn_Click(object sender, EventArgs e)
        {
            //sorting all the elements in ascending order of the name and assigning it to the binding source
            _bindingSource.DataSource = (from item in _atomList
                                         orderby item.Name
                                         select new
                                         {
                                             Atomic = item.AtomicNumber,
                                             Name = item.Name,
                                             Symbol = item.Symbol,
                                             Mass = item.MolarMass
                                         });
            UI_DataGridView.DataSource = _bindingSource;

            // changing column name and Making proper grid size
            UI_DataGridView.Columns[0].HeaderText = "Atomic #";
            UI_DataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// This function will make a list of all the data where symbol contain only one letter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_SingleCharacterSymbols_Btn_Click(object sender, EventArgs e)
        {
            // selecting all the elements having Single char symbol and assigning it to BS 
            _bindingSource.DataSource = (from item in _atomList 
                                         where item.Symbol.Length == 1 
                                         select new 
                                         {Atomic = item.AtomicNumber, 
                                          Name = item.Name,
                                          Symbol = item.Symbol, 
                                          Mass = item.MolarMass});
            UI_DataGridView.DataSource = _bindingSource;

            // changing column name and Making proper grid size
            UI_DataGridView.Columns[0].HeaderText = "Atomic #";
            UI_DataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        /// <summary>
        /// This function sort all the data on the basis of Atomic Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_SortByAtomic_btn_Click(object sender, EventArgs e)
        {

            // sorting by Atomic number and assigning it to the Data source
            _bindingSource.DataSource = (from item in _atomList
                                         orderby item.AtomicNumber
                                         select new
                                         {
                                             Atomic = item.AtomicNumber,
                                             Name = item.Name,
                                             Symbol = item.Symbol,
                                             Mass = item.MolarMass
                                         });
            UI_DataGridView.DataSource = _bindingSource;
            
            // changing column name and Making proper grid size
            UI_DataGridView.Columns[0].HeaderText = "Atomic #";
            UI_DataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
    }
}
