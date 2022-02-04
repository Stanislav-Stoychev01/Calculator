using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form1 : Form
    {
        char[] symbols = new char[11];
        char[] operations = new char[5];
        double number1 = 0;
        double number2 = 0;
        bool flag = false;
        public static List<string> history = new List<string>();
        string text;
        public Form1()
        {
            InitializeComponent();
        }
        public char[] FillArrayNumbers(char[] array)
        {
            for (int i = 0; i < 10; i++)
            {
                array[i] = (char)('0' + i);
            }
           
            array[10] = ',';
            return array;
        }
        public double BasicMaths(double number1, double number2, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "+":
                    {
                        result = number1 + number2;
                        return result;
                    }
                case "-":
                    {
                        result = number1 - number2;
                        return result;
                    }
                case "*":
                    {
                        result = number1 * number2;
                        return result;
                    }
                case "/":
                    {
                        result = number1 / number2;
                        return result;
                    }
                case "%":
                    {
                        result = (number1 / 100) * number2;
                        return result;
                    }
            }

            return result;
        }

        public char[] FillOperations(char[] array)
        {
            array[0] = '+';
            array[1] = '-';
            array[2] = '*';
            array[3] = '/';
            array[4] = '%';

            return array;
        }
        public void Base(double a, double b, string str)
        {
            a = 0;
            b = 0;
            str = "";
        }
        public bool ValidCharacter(char[] symbols, char[] operations, char input,  string text)
        {
            bool valid = false;

            for (int i = 0; i < symbols.Length; i++)
            {
                if (textBoxCalculation.Text.Contains("="))
                    break;

                if (input == symbols[i])
                {
                    if ((text == "" || text == null) && input == ',')
                    {

                    }
                    else
                    {
                        if (input == ',' && text.Contains(input))
                        {
                            break;
                        }
                        valid = true;
                        break;
                    }
                }
            }
            for (int i = 0; i < operations.Length; i++)
            {
                if (textBoxToatal.Text.Contains(operations[i]))
                    break;
                if (input == operations[i])
                {
                    if (text == "" || text == null)
                        break;
                    else
                    {
                        if (text.Contains(operations[i]))
                        {
                            break;
                        }
                        else
                        {
                            valid = true;
                            break;
                        }
                    }
                }
            }
            return valid;
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBoxCalculation_TextChanged(object sender, EventArgs e)
        {
            if (flag == true)
            {
                if (textBoxCalculation.Text.Contains("="))
                {
                    textBoxToatal.Text = text + textBoxCalculation.Text;
                }
                else
                {
                    text = textBoxToatal.Text;
                    textBoxToatal.Text = text + textBoxCalculation.Text;
                }
            }
            if (flag == false )
            {
                if (textBoxCalculation.Text.Length != textBoxToatal.Text.Length)
                {
                    textBoxToatal.Text = text + textBoxCalculation.Text;
                }
                else
                {
                    textBoxToatal.Text = textBoxCalculation.Text;
                }
            }      
        }

        private void textBoxCalculation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (flag == true)
                flag = false;

            if (symbols[0] == symbols[1])
            {
                FillOperations(operations);
                FillArrayNumbers(symbols);
            }
            if (e.KeyChar != '=')
            {
                e.Handled = !ValidCharacter(symbols, operations, e.KeyChar, textBoxCalculation.Text);
            }
            else
            {
                for (int i = 0; i < operations.Length; i++)
                {
                    if (textBoxToatal.Text.Contains(operations[i]) && textBoxCalculation.Text.Length != textBoxToatal.Text.Length)
                    {
                        var numbersArray = textBoxToatal.Text.Split(operations[i]).ToArray();
                        number1 = double.Parse(numbersArray[0]);
                        number2 = double.Parse(numbersArray[1]);
                        var operation = operations[i].ToString();
                        var result = BasicMaths(number1, number2, operation);
                        flag = true;
                        e.Handled = false;
                        textBoxCalculation.Text = result.ToString();
                        break;
                    }
                }
            }
            for (int i = 0; i < operations.Length; i++)
            {
                if (textBoxCalculation.Text.Contains(operations[i]))
                {
                    if (operations[i] != '=' && e.KeyChar != operations[i])
                    {
                        flag = true;
                        textBoxToatal.Text = textBoxCalculation.Text;
                        textBoxCalculation.Text = null;
                        break;
                    }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (textBoxToatal.Text.Contains("="))
            {
                var str = textBoxToatal.Text;
                history.Add(str);
            }
            textBoxCalculation.Text = null;
            textBoxToatal.Text = null;
            text = null;
            flag = false;
            Base(number1, number2, text);
        }

        private void buttonPowerOf2_Click(object sender, EventArgs e)
        {
            number1 = double.Parse(textBoxCalculation.Text);
            var result = Math.Pow(number1, 2);
            textBoxCalculation.Text = "=" + result.ToString();
        }

        private void buttonSqarreRoot_Click(object sender, EventArgs e)
        {
            number1 = double.Parse(textBoxCalculation.Text);
            var result = Math.Sqrt(number1);
            textBoxCalculation.Text = "=" + result.ToString();
        }

        private void button1DividebByX_Click(object sender, EventArgs e)
        {
            number1 = double.Parse(textBoxCalculation.Text);
            var result = 1 / number1;
            textBoxCalculation.Text = "=" + result.ToString();
        }

        private void buttonShowHistory_Click(object sender, EventArgs e)
        {
            var newForm = new FormHistory();
            newForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "3";
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "+";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "6";
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "-";
        }

        private void buttonPercent_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "%";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "9";
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "*";
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "=";
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            textBoxCalculation.Text = textBoxCalculation.Text + "/";
        }
    }
}
