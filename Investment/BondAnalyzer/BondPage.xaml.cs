using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;

namespace Investment.BondAnalyzer {
    /// <summary>
    /// Interaction logic for BondPage.xaml
    /// </summary>
    public partial class BondPage : Page {
        public BondPage() {
            InitializeComponent();
        }
        /// <summary>
        ///     Checks if the value recieved is not null nor a white space nor empty
        /// </summary>
        /// <param name="value">A string</param>
        /// <returns>Either true if there is content or false if there is no content</returns>
        private static bool CheckTextBoxContent(string value) {
            if(String.IsNullOrWhiteSpace(value) && String.IsNullOrEmpty(value)) {
                return false;
            }
            return true;
        }
        /// <summary>
        ///     Calculate the return over a certain amount of years where you
        ///     reinvest the earning of the past year.
        /// </summary>
        /// <param name="bondDuration">Durantion of a bond in years</param>
        /// <param name="amountInvested">Amount invested</param>
        /// <param name="yieldReturn">Yearly return in a range from 0 to 1</param>
        /// <returns>
        ///     Tuple of doubles where the first is the total return and the 
        ///     second is the average return per year
        /// </returns>
        private static (double amountInvested, double yearReturnAverage) CalculateProfitReinvest(int bondDuration, double amountInvested, double yieldReturn) {
            double loopReturn;
            double yearReturnAverage = 0;
            double totalReturn = amountInvested;

            for(int i = 0; i < bondDuration; i++) {
                loopReturn = totalReturn * yieldReturn;
                yearReturnAverage += loopReturn;
                totalReturn += loopReturn;
            }

            yearReturnAverage = Math.Round(yearReturnAverage / bondDuration, 2);

            totalReturn = Math.Round(totalReturn, 2);

            return (totalReturn, yearReturnAverage);
        }
        private double GetUnitPrice() {
            double unitPrice = 0;

            if(CheckTextBoxContent(unitPriceBox.Text)) {
                try { unitPrice = Convert.ToDouble(unitPriceBox.Text); }
                // Will make unitPrice = 0 if the text cant be converted to a double
                catch(FormatException) { unitPrice = 0; }
            }

            return unitPrice;
        }
        private double GetUnitAmount() {
            double unitAmount = 0;

            if(CheckTextBoxContent(unitAmountBox.Text)) {
                try { unitAmount = Convert.ToDouble(unitAmountBox.Text); }
                // Will make unitAmount = 0 if the text cant be converted to a double
                catch(FormatException) { unitAmount = 0; }
            }

            return unitAmount;
        }
        private double GetTotalAmount() {
            double totalAmount = 0;

            if(CheckTextBoxContent(amountPaiedBox.Text)) {
                try { totalAmount = Convert.ToDouble(amountPaiedBox.Text); }
                // Will make totalAmount = 0 if the text cant be converted to a double
                catch(FormatException) { totalAmount = 0; }
            }

            return totalAmount;
        }
        private int GetBondDuration() {
            int bondDuration = 0;

            if(CheckTextBoxContent(durationBox.Text)) {
                try { bondDuration = Convert.ToInt16(durationBox.Text); }
                // Will make bondDuration = 0 if the text cant be converted to a double
                catch(FormatException) { bondDuration = 0; }
            }

            return bondDuration;
        }
        private double GetTax() {
            double tax = 0;

            if(CheckTextBoxContent(taxBox.Text)) {
                try { tax = Convert.ToDouble(taxBox.Text); }
                // Will make tax = 0 if the text cant be converted to a double
                catch(FormatException) { tax = 0; }
            }

            // The tax value comes in percentage but we need a 0 to 1 range
            tax = tax / 100;

            return tax;
        }
        private double GetAnualYield() {
            double anualYield = 0;

            if(CheckTextBoxContent(anualYieldBox.Text)) {
                try { anualYield = Convert.ToDouble(anualYieldBox.Text); }
                // Will make yieldAnual = 0 if the text cant be converted to a double
                catch(FormatException) { anualYield = 0; }
            }

            // The tax value comes in percentage but we need a 0 to 1 range
            anualYield = anualYield / 100;

            return anualYield;
        }
        private double GetTotalAmount(double unitPrice, double unitAmount, double totalAmount) {
            double amountInvested;

            if(totalAmount == 0) {
                amountInvested = unitPrice * unitAmount;
            } else {
                amountInvested = totalAmount;
            }

            return amountInvested;
        }
        private void CalculateBond(object sender, RoutedEventArgs e) {
            bool reinvestProfits = reinvestProfitsSwitch.switchOn;
            double unitPrice = GetUnitPrice();
            double unitAmount = GetUnitAmount();
            double totalAmount = GetTotalAmount();
            int bondDuration = GetBondDuration();
            double tax = GetTax();
            double yieldAnual = GetAnualYield();
            double amountInvested = GetTotalAmount(unitPrice, unitAmount, totalAmount);

            // Calculate/Get full ROI and anual ROI
            double anualReturn;
            double totalReturn;
            if(reinvestProfits) {
                (double amountInvested, double yearReturnAverage) profitReinvest = CalculateProfitReinvest(bondDuration, amountInvested, yieldAnual);
                // Get full ROI
                totalReturn = profitReinvest.amountInvested;
                totalReturnBox.Text = Convert.ToString(totalReturn);
                // Get the anual ROI
                anualReturn = profitReinvest.yearReturnAverage;
                anualReturnBox.Text = Convert.ToString(anualReturn);
            } else {
                // Calculate the anual ROI
                anualReturn = amountInvested * yieldAnual;
                anualReturnBox.Text = Convert.ToString(anualReturn);
                // Calculate full ROI
                totalReturn = anualReturn * bondDuration;
                totalReturnBox.Text = Convert.ToString(totalReturn);
            }
            // Calculate ROI after tax
            if(tax != 0) {
                double owedTax = totalReturn * tax;
                double totalReturnAfterTax = totalReturn - owedTax;
                owedTaxBox.Text = Convert.ToString(owedTax);
                totalReturnAfterTaxBox.Text = Convert.ToString(totalReturnAfterTax);
            }
        }
        private void PreviewTextOnlyNumeric(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex(@"[^0-9|\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
