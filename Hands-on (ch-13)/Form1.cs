using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hands_on__ch_13_
{
    delegate double myDelegate(double value1, double value2);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtValue1.Clear();
            txtValue2.Clear();
            cboOperation.SelectedIndex = -1;
            lblResult.ResetText();

            txtValue1.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //performOperation();
            //performOperationWithLambda();
            performOperationWithBuiltIns();
        }
        
        
        private void performOperation()
        {
            if(!double.TryParse(txtValue1.Text, out double value1))
            {
                txtValue1.SelectAll();
                txtValue1.Focus();
                lblResult.Text = "Enter a value";
            }
            else if(cboOperation.SelectedIndex == -1)
            {
                lblResult.Text = "Select an operation";
            }
            else if(!double.TryParse(txtValue2.Text, out double value2))
            {
                txtValue2.SelectAll();
                txtValue2.Focus();
                lblResult.Text = "Enter a value";
            }
            else
            {
                myDelegate add = delegate (double number1, double number2)
                { return number1 + number2; };

                myDelegate subtract = delegate (double number1, double number2)
                { return number1 - number2; };

                myDelegate multiply = delegate (double number1, double number2)
                { return number1 * number2; };

                myDelegate divide = delegate (double number1, double number2)
                { return number1 / number2; };

                myDelegate myOperation;

                if(cboOperation.SelectedItem.ToString() == "+")
                    myOperation = add;
                else if(cboOperation.SelectedItem.ToString() == "-")
                    myOperation = subtract;
                else if(cboOperation.SelectedItem.ToString() == "*")
                    myOperation = multiply;
                else
                    myOperation = divide;

                double result = myOperation(value1, value2);

                lblResult.Text = result.ToString();
            }
        }

        private void performOperationWithLambda()
        {
            if (!double.TryParse(txtValue1.Text, out double value1))
            {
                txtValue1.SelectAll();
                txtValue1.Focus();
                lblResult.Text = "Enter a value";
            }
            else if (cboOperation.SelectedIndex == -1)
            {
                lblResult.Text = "Select an operation";
            }
            else if (!double.TryParse(txtValue2.Text, out double value2))
            {
                txtValue2.SelectAll();
                txtValue2.Focus();
                lblResult.Text = "Enter a value";
            }
            else
            {
                myDelegate add = (number1, number2) => number1 + number2;
                myDelegate subtract = (number1, number2) => number1 - number2;
                myDelegate multiply = (number1, number2) => number1 * number2;
                myDelegate divide = (number1, number2) => number1 / number2;
                myDelegate unknown = (number1, number2) => 0;

                myDelegate myOperation;

                switch (cboOperation.SelectedItem.ToString())
                {
                    case "+":
                        myOperation = add;
                        break;
                    case "-":
                        myOperation = subtract;
                        break;
                    case "*":
                        myOperation = multiply;
                        break;
                    case "/":
                        myOperation = divide;
                        break;
                    default:
                        myOperation = unknown;
                        break;
                }

                double result = myOperation(value1, value2);
                lblResult.Text = result.ToString();
            }
        }
        //using .NET built delegate Func. Also look at the other two built-in .net delegates Action and Predicate
        private void performOperationWithBuiltIns()
        {
            Action<string> displayResult = (str) => lblResult.Text = str;
            Predicate<int> notSelected = (i) => i == -1;

            if (!double.TryParse(txtValue1.Text, out double value1))
            {
                txtValue1.SelectAll();
                txtValue1.Focus();
                lblResult.Text = "Enter a value";
            }
            else if (notSelected(cboOperation.SelectedIndex))//using the .Net built-in Predicate delegate
            {
                lblResult.Text = "Select an operation";
            }
            else if (!double.TryParse(txtValue2.Text, out double value2))
            {
                txtValue2.SelectAll();
                txtValue2.Focus();
                lblResult.Text = "Enter a value";
            }
            else
            {
                //the last data type represents the return type
                Func<double, double, double> add = (number1, number2) => number1 + number2;
                Func<double, double, double> subtract = (number1, number2) => number1 - number2;
                Func<double, double, double> multiply = (number1, number2) => number1 * number2;
                Func<double, double, double> divide = (number1, number2) => number1 / number2;
                Func<double, double, double> unknown = (number1, number2) => 0;


                switch (cboOperation.SelectedItem.ToString())
                {
                    case "+":
                        lblResult.Text = add(value1,value2).ToString();
                        break;
                    case "-":
                        lblResult.Text = subtract(value1, value2).ToString();
                        break;
                    case "*":
                        lblResult.Text = multiply(value1, value2).ToString();
                        break;
                    case "/":
                        lblResult.Text = divide(value1, value2).ToString();
                        break;
                    default:
                        lblResult.Text = unknown(value1, value2).ToString();
                        break;
                }

            }

        }
    }
}
