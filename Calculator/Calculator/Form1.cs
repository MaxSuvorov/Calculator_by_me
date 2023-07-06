using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calc C;
        public double CorrDouble(double d)
        {
            if ((float)d == Math.Round(d))
                return Math.Round(d);
            else return d;
        }
        private void CorrectNumber()
        {   
            if (label.Text.Contains('н'))
                label.Text = label.Text.Remove(0, label.Text.Length -1);

            if (label.Text.Contains('∞'))
                label.Text = label.Text.Remove(0, label.Text.Length - 1);

            if (label.Text[0] == '0' && label.Text.Length != 1 && (label.Text.IndexOf(",") != 1))
                label.Text = label.Text.Remove(0, 1);

            if (label.Text[0] == '-')
                if (label.Text.Length > 1 && label.Text[1] == '0' && (label.Text.IndexOf(",") != 2))
                    label.Text = label.Text.Remove(1, 1);
            if (label.Text[0] == ',')
                label.Text = '0' + label.Text;
        }

        private bool CanPress() 
        {
            if (!buttonMultipl.Enabled) return false;
            if (!buttonDivision.Enabled) return false;
            if (!buttonPlus.Enabled) return false;
            if (!buttonMinus.Enabled) return false;
            if (!buttonEquals.Enabled) return false;
            return true;
        }

        private void FreeButtons() {
            buttonMultipl.Enabled = true;
            buttonDivision.Enabled = true;
            buttonPlus.Enabled = true;
            buttonMinus.Enabled = true;
            buttonEquals.Enabled = true;
        }
        public Form1()
        {
            InitializeComponent();
            C = new Calc();
            label.Text = "0";
            labelHistory.Text = "";
            label.Focus();
        }

        private void button_Click(object sender, EventArgs e)
        {
            label.Text += (sender as Button).Text;
            CorrectNumber();
            label.Focus();
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            label.Text = "0";
            label.Focus();
        }
        private void buttonC_Click(object sender, EventArgs e)
        {
            label.Text = "0";
            labelHistory.Text = "";
            C.Clear_A();
            FreeButtons();
            label.Focus();
        }

        private void buttonPlusMinus_Click(object sender, EventArgs e) 
        { 
            if (label.Text[0] == '-') 
                label.Text = label.Text.Remove(0, 1);
            else
                label.Text = "-" + label.Text;
            label.Focus();
        }
        private void buttonDot_Click(object sender, EventArgs e)
        {
            if ((label.Text.IndexOf(",") == -1) && (label.Text.IndexOf("∞") == -1)) 
                label.Text += ",";
            CorrectNumber();
            label.Focus();
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (!buttonMultipl.Enabled)
                label.Text = CorrDouble(C.Multiplication(Convert.ToDouble(label.Text))).ToString();
            if (!buttonDivision.Enabled)
                label.Text = CorrDouble(C.Division(Convert.ToDouble(label.Text))).ToString();
            if (!buttonPlus.Enabled)
                label.Text = CorrDouble(C.Sum(Convert.ToDouble(label.Text))).ToString();
            if (!buttonMinus.Enabled)
                label.Text = CorrDouble(C.Subtraction(Convert.ToDouble(label.Text))).ToString();
            C.Clear_A();
            FreeButtons();
            labelHistory.Text = "";
            label.Focus();
        }
        private void operator_Click(object sender, EventArgs e)
        {
            if (CanPress()) {
                C.Put_A(Convert.ToDouble(label.Text));
                (sender as Button).Enabled = false;
                labelHistory.Text = label.Text + " " + (sender as Button).Text;
                label.Text = "0";
                label.Focus();
            } 
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            C.Put_A(Convert.ToDouble(label.Text));
            label.Text = C.Sqrt().ToString();
            label.Focus();
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            C.Put_A(Convert.ToDouble(label.Text));
            label.Text = C.Square().ToString();
            label.Focus();
        }

        private void buttonDen_Click(object sender, EventArgs e)
        {
            C.Put_A(Convert.ToDouble(label.Text));
            label.Text = C.Den().ToString();
            label.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (label.Text.Length >= 2 && !(label.Text.Length == 2 && label.Text[0] == '-'))
                label.Text = label.Text.Remove(label.Text.Length - 1);
            else label.Text = "0";
            label.Focus();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    label.Text += e.KeyChar;
                    CorrectNumber();
                }
                else if (e.KeyChar == '+') operator_Click(buttonPlus, null);
                else if (e.KeyChar == '*') operator_Click(buttonMultipl, null);
                else if (e.KeyChar == '-') operator_Click(buttonMinus, null);
                else if (e.KeyChar == '/') operator_Click(buttonDivision, null);
                else if (e.KeyChar == ',') buttonDot_Click(buttonDot, null);
                else e.Handled = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
                buttonEquals_Click(buttonEquals, null);
            if (e.KeyValue == (char)Keys.Back)
                buttonDelete_Click(buttonDelete, null);
            if (e.KeyValue == (char)Keys.Delete)
                buttonC_Click(buttonC, null);
        }

        private void labelHistory_Click(object sender, EventArgs e)
        {

        }
    }
}
