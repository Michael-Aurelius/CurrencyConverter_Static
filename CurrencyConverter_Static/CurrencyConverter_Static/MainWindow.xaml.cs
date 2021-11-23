using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter_Static
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // ClearControls is a method used to clear all values
            ClearControls();

            // BindCurrency is used to bind the currency names with the values in the comboboxes
            BindCurrency();
           
        }

        #region Bind Currency From and To Combobox
        private void BindCurrency()
        {
            // Creating a DataTable object and setting the columns
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            // Add rows in the DataTable with text and value
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);

            // Assigning the data from dtCurrency to the From: combobox
            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            // Assigning the data from dtCurrency to the To: combobox
            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }
        #endregion

        #region Button Click Event

        // The Covert button event on Click
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            // Creating ConvertedValue with a double data type to store the currency converted value
            double ConvertedValue;

            // Check to see if the "Enter Amount" textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                // If the textbox is Null or Blank display the below message box   
                MessageBox.Show("Please Enter Currency", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // After clicking on message box OK sets the Focus on the textbox
                txtCurrency.Focus();
                return;
            }
            // Else if the "From:" currency is not selected or if it is the default text --SELECT--
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                // It will show this message
                MessageBox.Show("Please Select Currency From", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Set the focus on the "From:" Combobox
                cmbFromCurrency.Focus();
                return;
            }
            // Else if the "To:" currency is not selected or if it is the default text --SELECT--
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                // It will show this message
                MessageBox.Show("Please Select Currency To", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Set focus on the "To:" Combobox
                cmbToCurrency.Focus();
                return;
            }
            // Check to see if the From: and the To: Combobox selected values are same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                // "Enter Amount:" textbox value is set to ConvertedValue after double.parse to convert it
                ConvertedValue = double.Parse(txtCurrency.Text);

                // Shows the converted current to the nearest thousandth (N3 = .000)
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                // Calculation for converting the currencies 
                // "From:" currency value multiplied (*) by "Enter Amount:" then divided (/) by "To:" currency value
                ConvertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                // Shows the converted current to the nearest thousandth (N3 = .000)
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
        }

        // The Clear Button event on Click 
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // ClearControls method is used to clear all values
            ClearControls();
        }
        #endregion

        #region Extra Events

        // Clear Controls method
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = "";
            txtCurrency.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Regular Expression library is used to add regex
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }

}
