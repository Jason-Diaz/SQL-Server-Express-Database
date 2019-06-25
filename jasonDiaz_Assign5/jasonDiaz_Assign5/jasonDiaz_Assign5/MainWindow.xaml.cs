//******************************************************
// File: MainWindow.xaml.cs
//
// Purpose: This application will use a Windows Presentation
//          Foundation (WPF) GUI to display country data.
//          The main window displays a master/detail window.
//          Data for the main window is retrieved from the
//          SQL Server Express database. The master is a
//          list of country names in a ListBox. The details
//          portion of the window shows data for the selected
//          country name (from the ListBox). When the user
//          selects a country name in the ListBox the data
//          for that country is loaded into all the detail
//          TextBoxes on the right side of the window.
//
// Written By: Jason Diaz 
//
// Compiler: Visual Studio 2017
//
//******************************************************

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using CountryDataLibrary;
using Microsoft.Win32;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;

namespace jasonDiaz_Assign5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connString = @"server = (LocalDB)\MSSQLLocalDB;" +
			"integrated security = SSPI;" +
			"database=Country;" +
			"MultipleActiveResultSets=True";

        List<Country> countries = new List<Country>();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Window_Loaded method
        //****************************************************
        // Method: Window_Loaded
        //
        // Purpose:	When the window loads call ShowData to
        //          display the data currently in the database.
        //****************************************************
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowData();
        }
        #endregion

        #region MenuItems methods
        #region MenuItem_Exit method
        //****************************************************
        // Method: MenuItem_Exit
        //
        // Purpose:	Closes the application.
        //****************************************************
        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #region MenuItem_Import method
        //****************************************************
        // Method: MenuItem_Import
        //
        // Purpose:	Displays a file open dialog to let the user
        //          select the JSON file to import from. Reads
        //          the country data from the selected countries
        //          JSON file. Deserialize data from the file
        //          into a List<Country> instance.
        //          Calls: ClearCountryData();
        //          StoreCountryData();
        //          ShowData();
        //****************************************************
        private void MenuItem_Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                FileStream reader = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(reader, Encoding.UTF8);
                string streamString = streamReader.ReadToEnd();

                byte[] byteArray = Encoding.UTF8.GetBytes(streamString);
                MemoryStream stream = new MemoryStream(byteArray);


                DataContractJsonSerializer inputSerializer;
                inputSerializer = new DataContractJsonSerializer(typeof(List<Country>));
                countries = (List<Country>)inputSerializer.ReadObject(stream);

                stream.Close();
            }
            ClearCountryData();
            StoreCountryData();
            ShowData();
        }
        #endregion

        #region MenuItem_About method
        //****************************************************
        // Method: MenuItem_About
        //
        // Purpose:	show a dialog box that displays the program
        //          name, program version, and developer name.
        //****************************************************
        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Country GUI \nVersion 1 \nCreator: Jason Diaz", "About Country GUI");
        }
        #endregion
        #endregion

        #region ShowData method
        //****************************************************
        // Method: ShowData
        //
        // Purpose:	Populates the country ListBox with data
        //          from the SQL Server database.
        //****************************************************
        private void ShowData()
        {
            SqlConnection sqlConn;
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            string sql = "SELECT * FROM Country";
            SqlCommand command = new SqlCommand(sql, sqlConn);

            // Retrieve the data from the database
            SqlDataReader reader = command.ExecuteReader();

            //*************************************************
            // Associate the DataGrid with the SqlDataReader.
            // This will automatically populate the DataGrid.
            //*************************************************
            listBox.ItemsSource = reader;
            listBox.DisplayMemberPath = "Name";
            listBox.SelectedValuePath = "Name";
        }
        #endregion

        #region listBox_SelectionChanged method
        //****************************************************
        // Method: listBox_SelectionChanged
        //
        // Purpose:	Occurs when there is a selection of a
        //          ListBox item.
        //****************************************************
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Name;

            if (listBox.SelectedItem != null)
            {
                Name = Convert.ToString(listBox.SelectedValue);
                SetTextBoxes(Name);
            }
        }
        #endregion

        #region SetTextBoxes method
        //****************************************************
        // Method: SetTextBoxes
        //
        // Purpose:	Populates the TextBoxes with the appropriate
        //          data for the selected ListBox item.
        //****************************************************
        private void SetTextBoxes(string Name)
        {
            string[] fields = new string[] { "Capital", "Region", "Subregion", "Population" };
            string[] values = new string[4];
            string sql;

            SqlConnection sqlConn;
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            // Loop through retrieving all diffrent fields that match the selected country
            for (int i = 0; i < 4; i++)
            {
                sql = "SELECT " + fields[i] + " FROM Country WHERE Name = '" + Name + "'";
                SqlCommand command = new SqlCommand(sql, sqlConn);

                // Retrieve the data from the database
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (i < 3)
                {
                    values[i] = reader.GetString(0);
                }
                else
                {
                    values[i] = Convert.ToString(reader.GetInt32(0));
                }
                
            }

            // Setting TextBoxes
            textboxName.Text = Name;
            textboxCapital.Text = values[0];
            textboxRegion.Text = values[1];
            textboxSubregion.Text = values[2];
            textboxPopulation.Text = values[3];
        }
        #endregion

        #region StoreCountryData method
        //****************************************************
        // Method: StoreCountryData
        //
        // Purpose:	Uses sql commands to insert each country
        //          into the country table.
        //****************************************************
        private void StoreCountryData()
        {
            SqlConnection sqlConn;
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            foreach (Country country in countries)
            {

                string sql = string.Format(
                           "INSERT INTO Country" +
                           "(Name, Capital, Region, Subregion, Population) Values" +
                           "(@Name, @Capital, @Region, @Subregion, @Population)");

                SqlCommand command = new SqlCommand(sql, sqlConn);
                command.Parameters.Add("@Name", SqlDbType.NVarChar);
                command.Parameters["@Name"].Value = country.Name;
                command.Parameters.Add("@Capital", SqlDbType.NVarChar);
                command.Parameters["@Capital"].Value = country.Capital;
                command.Parameters.Add("@Region", SqlDbType.NVarChar);
                command.Parameters["@Region"].Value = country.Region;
                command.Parameters.Add("@Subregion", SqlDbType.NVarChar);
                command.Parameters["@Subregion"].Value = country.Subregion;
                command.Parameters.Add("@Population", SqlDbType.Int);
                command.Parameters["@Population"].Value = country.Population;
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region ClearCountryText method
        //****************************************************
        // Method: ClearCountryText
        //
        // Purpose:	Clear the country data TextBoxes and ListBox
        //****************************************************
        private void ClearCountryData()
        {
            textboxName.Text = String.Empty;
            textboxCapital.Text = String.Empty;
            textboxRegion.Text = String.Empty;
            textboxSubregion.Text = String.Empty;
            textboxPopulation.Text = String.Empty;
            SqlConnection sqlConn;
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();

            string sql = "DELETE FROM Country";
            SqlCommand command = new SqlCommand(sql, sqlConn);
            command.ExecuteNonQuery();

            listBox.ItemsSource = null;
        }
        #endregion
    }
}
