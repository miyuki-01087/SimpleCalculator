using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_01
{
    public partial class SimpkeCalculator : Form
    {

        private StringBuilder input_value = new StringBuilder("");
        private string left_value = "";
        private bool can_reset_by_equal_key = true;
        private bool can_input_point = true;
        private bool is_inputted_by_left_value = false;

        private enum CALCULATE_TYPE{
            NONE, EQUAL, PLUS, MINUS, MULTIPLE, DIVIDE
        }

        private CALCULATE_TYPE calculate_type = CALCULATE_TYPE.NONE;



        public SimpkeCalculator()
        {
            InitializeComponent();
        }
        private void inputNumber(string str)
        {   if(input_value.ToString() == "0")
            {
                input_value.Length -= 1;
            }
            input_value.Append(str);
            box_view.Text = input_value.ToString();
            is_inputted_by_left_value = false;
        }
        private void inputPeriod(string str)
        {
            input_value.Append(str);
            box_view.Text = input_value.ToString();
            is_inputted_by_left_value = false;
        }
        private double executeCalculation(double a, double b)
        {
            double ans = 0.0;
            switch (calculate_type)
            {
                case CALCULATE_TYPE.PLUS:
                    ans = a + b;
                        break;
                case CALCULATE_TYPE.MINUS:
                    ans = a - b;
                    break;
                case CALCULATE_TYPE.MULTIPLE:
                    ans = a * b;
                    break;
                case CALCULATE_TYPE.DIVIDE:
                    if(b != 0)
                    {
                        ans = a / b;
                    }
                    else {
                        ans = 0;
                    }
                    break;
            }
            ans = Math.Round(ans, 7);
            return ans;
        }
        private double parseValue(string str)
        {
            if(str != "")
            {
                return Double.Parse(str);
            }
            return 0;
        }
        private void beforeActionNumberInput()
        {
            if (calculate_type == CALCULATE_TYPE.EQUAL && can_reset_by_equal_key)
            {
                left_value = "";
                can_reset_by_equal_key = false;
                if (can_input_point)
                {
                    input_value.Clear();
                }
            }
        }
        private void ParseAndCallCalculation()
        {
            double right_double = parseValue(input_value.ToString());
            double left_double = parseValue(left_value.ToString());
            double answer = executeCalculation(left_double, right_double);
            box_view.Text = answer.ToString();
            left_value = answer.ToString();
            is_inputted_by_left_value = true;
        }
        private bool JudgeDoCalculation()
        {
            return (left_value != "" && calculate_type != CALCULATE_TYPE.EQUAL && input_value.ToString() != "");
        }
        private bool JudgeInsertToleft_value()
        {
            return (left_value == "" || (calculate_type == CALCULATE_TYPE.EQUAL && input_value.ToString() != ""));
        }
        private void Execute(CALCULATE_TYPE cal)
        {
            if(cal == CALCULATE_TYPE.EQUAL)
            {
                if (calculate_type != CALCULATE_TYPE.EQUAL && calculate_type != CALCULATE_TYPE.NONE)
                {
                    double right_double = parseValue(input_value.ToString());
                    double left_double = parseValue(left_value.ToString());
                    double answer = executeCalculation(left_double, right_double);
                    box_view.Text = answer.ToString();
                    left_value = answer.ToString();
                    is_inputted_by_left_value = true;
                    input_value.Clear();
                    can_input_point = true;
                }
            } else {
                if (JudgeDoCalculation())
                {
                    ParseAndCallCalculation();
                }
                else if (JudgeInsertToleft_value())
                {
                    left_value = input_value.ToString();
                }
                input_value.Clear();
                can_reset_by_equal_key = true;
                can_input_point = true;
            }
            calculate_type = cal;
        }
        private void button_equal_Click(object sender, EventArgs e)
        {
            Execute(CALCULATE_TYPE.EQUAL);
        }
        private void button_plus_Click(object sender, EventArgs e)
        {
            Execute(CALCULATE_TYPE.PLUS);
        }
        private void button_minus_Click(object sender, EventArgs e)
        {
            Execute(CALCULATE_TYPE.MINUS);
        }
        private void button_multi_Click(object sender, EventArgs e)
        {
            Execute(CALCULATE_TYPE.MULTIPLE);
        }
        private void button_divide_Click(object sender, EventArgs e)
        {
            Execute(CALCULATE_TYPE.DIVIDE);
        }
        private void button_period_Click(object sender, EventArgs e)
        {
            if (can_input_point)
            {
                if(input_value.ToString() == "")
                {
                    inputPeriod("0.");
                }
                else {
                    inputPeriod(".");
                }
                can_input_point = false;
            }
        }
        private void button_0_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("0");
        }
        private void button_1_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("1");
        }
        private void button_2_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("2");
        }
        private void button_3_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("3");
        }
        private void button_4_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("4");
        }
        private void button_5_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("5");
        }
        private void button_6_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("6");
        }
        private void button_7_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("7");
        }
        private void button_8_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("8");
        }
        private void button_9_Click(object sender, EventArgs e)
        {
            beforeActionNumberInput();
            inputNumber("9");
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Decimal:
                case Keys.OemPeriod:
                    if (can_input_point)
                    {
                        if (input_value.ToString() == "")
                        {
                            inputPeriod("0.");
                        }
                        else
                        {
                            inputPeriod(".");
                        }
                        can_input_point = false;
                    }
                    break;

                case Keys.Back:
                case Keys.Delete:
                    if (is_inputted_by_left_value)
                    {
                        BackSpaceToleft_value();
                    }
                    else
                    {
                        BackSpaceToinput_value();
                    }
                    break;

                case Keys.Enter:
                    Execute(CALCULATE_TYPE.EQUAL);
                    break;

                case Keys.Add:
                case Keys.Oemplus:
                    Execute(CALCULATE_TYPE.PLUS);
                    break;

                case Keys.Subtract:
                case Keys.OemMinus:
                    Execute(CALCULATE_TYPE.MINUS);
                    break;

                case Keys.Multiply:
                case Keys.Oem1:
                    Execute(CALCULATE_TYPE.MULTIPLE);
                    break;

                case Keys.Divide:
                case Keys.Oem2:
                    Execute(CALCULATE_TYPE.DIVIDE);
                    break;

                case Keys.D0:
                case Keys.NumPad0:
                    beforeActionNumberInput();
                    inputNumber("0");
                    break;

                case Keys.D1:
                case Keys.NumPad1:
                    beforeActionNumberInput();
                    inputNumber("1");
                    break;

                case Keys.D2:
                case Keys.NumPad2:
                    beforeActionNumberInput();
                    inputNumber("2");
                    break;

                case Keys.D3:
                case Keys.NumPad3:
                    beforeActionNumberInput();
                    inputNumber("3");
                    break;

                case Keys.D4:
                case Keys.NumPad4:
                    beforeActionNumberInput();
                    inputNumber("4");
                    break;

                case Keys.D5:
                case Keys.NumPad5:
                    beforeActionNumberInput();
                    inputNumber("5");
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    beforeActionNumberInput();
                    inputNumber("6");
                    break;

                case Keys.D7:
                case Keys.NumPad7:
                    beforeActionNumberInput();
                    inputNumber("7");
                    break;

                case Keys.D8:
                case Keys.NumPad8:
                    beforeActionNumberInput();
                    inputNumber("8");
                    break;

                case Keys.D9:
                case Keys.NumPad9:
                    beforeActionNumberInput();
                    inputNumber("9");
                    break;
            }
        }
        private void button_backspece_Click(object sender, EventArgs e)
        {
            if (is_inputted_by_left_value)
            {
                BackSpaceToleft_value();
            }
            else {
                BackSpaceToinput_value();
            }
        }
        private void BackSpaceToleft_value()
        {
            if (left_value != "")
            {
                StringBuilder tmp = new StringBuilder(left_value);
                if (tmp.Length == 0)
                {
                    left_value = "0";
                    box_view.Text = "0";
                }
                else
                {
                    if (tmp.ToString().EndsWith("."))
                    {
                        can_input_point = true;
                        tmp.Length -= 1;
                    }
                    else
                    {
                        tmp.Length -= 1;
                        if (tmp.ToString().EndsWith("."))
                        {
                            can_input_point = true;
                            tmp.Length -= 1;
                        }
                        left_value = tmp.ToString();
                        box_view.Text = tmp.ToString();
                    }
                }
            }
        }
        private void BackSpaceToinput_value()
        {
            if (input_value.ToString() != "")
            {
                if (input_value.Length == 0)
                {
                    box_view.Text = "0";
                }
                else
                {
                    if (input_value.ToString().EndsWith("."))
                    {
                        input_value.Length -= 1;
                        can_input_point = true;
                    }
                    else
                    {
                        input_value.Length -= 1;
                        if (input_value.ToString().EndsWith("."))
                        {
                            can_input_point = true;
                            input_value.Length -= 1;
                        }
                        left_value = input_value.ToString();
                        box_view.Text = input_value.ToString();
                    }
                    box_view.Text = input_value.ToString();
                }
            }
        }
    }
}
