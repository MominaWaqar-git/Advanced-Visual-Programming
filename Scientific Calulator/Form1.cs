using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Scientific_Calulator
{
    public partial class Form1 : Form
    {
        string expression = "";

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "";
        }

        private void AddToExpression(string value)
        {
            if (expression.Length > 0)
            {
                char last = expression.Last();
                if ("+-*/^".Contains(last) && "+-*/^".Contains(value))
                    return;
            }

            expression += value;
            txtDisplay.Text = expression;
        }

        // 🔹 Numbers
        private void Number_Click(object sender, EventArgs e)
        {
            AddToExpression(((Button)sender).Text);
        }

        // 🔹 Operators
        private void Operator_Click(object sender, EventArgs e)
        {
            AddToExpression(((Button)sender).Text);
        }

        // 🔹 Decimal
        private void btn_point_Click(object sender, EventArgs e)
        {
            if (expression.Length == 0 || "+-*/^(".Contains(expression.Last()))
                expression += "0.";
            else
                expression += ".";

            txtDisplay.Text = expression;
        }

        // 🔹 Brackets
        private void btn_round_left_bracket_Click(object sender, EventArgs e)
        {
            AddToExpression("(");
        }

        private void btn_round_right_bracket_Click(object sender, EventArgs e)
        {
            AddToExpression(")");
        }

        // 🔹 Scientific Buttons
        private void btnSin_Click(object sender, EventArgs e) => AddToExpression("sin(");
        private void btnCos_Click(object sender, EventArgs e) => AddToExpression("cos(");
        private void btnTan_Click(object sender, EventArgs e) => AddToExpression("tan(");
        private void btnlog_Click(object sender, EventArgs e) => AddToExpression("log(");
        private void btnSqrt_Click(object sender, EventArgs e) => AddToExpression("sqrt(");

        private void btnPi_Click(object sender, EventArgs e)
        {
            AddToExpression(Math.PI.ToString());
        }

        // 🔹 x^y
        private void btnPowerXY_Click(object sender, EventArgs e)
        {
            AddToExpression("^");
        }

        // 🔹 x²
        private void btnPower2_Click(object sender, EventArgs e)
        {
            try
            {
                string exp = EvaluateScientific(expression);
                double val = Convert.ToDouble(new DataTable().Compute(exp, null));

                double res = Math.Pow(val, 2);

                txtDisplay.Text = res.ToString();
                expression = res.ToString();
            }
            catch
            {
                txtDisplay.Text = "Error";
                expression = "";
            }
        }

        

        // 🔹 Equal
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string exp = EvaluateScientific(expression);
                var result = new DataTable().Compute(exp, null);

                txtDisplay.Text = result.ToString();
                expression = result.ToString();
            }
            catch
            {
                txtDisplay.Text = "Error";
                expression = "";
            }
        }

        private string EvaluatePower(string exp)
        {
            while (exp.Contains("^"))
            {
                int index = exp.IndexOf("^");

                // LEFT SIDE (handle numbers + brackets properly)
                int leftStart = index - 1;
                int bracket = 0;

                while (leftStart >= 0)
                {
                    if (exp[leftStart] == ')') bracket++;
                    else if (exp[leftStart] == '(') bracket--;

                    if (bracket == 0 &&
                        !char.IsDigit(exp[leftStart]) &&
                        exp[leftStart] != '.' &&
                        exp[leftStart] != ')')
                        break;

                    leftStart--;
                }

                string left = exp.Substring(leftStart + 1, index - leftStart - 1);

                // RIGHT SIDE (IMPORTANT FIX)
                int rightEnd = index + 1;
                bracket = 0;

                while (rightEnd < exp.Length)
                {
                    if (exp[rightEnd] == '(') bracket++;
                    else if (exp[rightEnd] == ')') bracket--;

                    if (bracket == 0 &&
                        !char.IsDigit(exp[rightEnd]) &&
                        exp[rightEnd] != '.' &&
                        exp[rightEnd] != '(')
                        break;

                    rightEnd++;
                }

                string right = exp.Substring(index + 1, rightEnd - index - 1);

                double leftVal = Convert.ToDouble(new DataTable().Compute(left, null));
                double rightVal = Convert.ToDouble(new DataTable().Compute(right, null));

                double result = Math.Pow(leftVal, rightVal);

                exp = exp.Replace(left + "^" + right, result.ToString());
            }

            return exp;
        }

        // 🔥 SCIENTIFIC ENGINE
        private string EvaluateScientific(string exp)
        {
            exp = ProcessTrig(exp, "sin", Math.Sin);
            exp = ProcessTrig(exp, "cos", Math.Cos);
            exp = ProcessTrig(exp, "tan", angle =>
            {
                if (Math.Abs(Math.Cos(angle)) < 1e-10)
                    throw new Exception("Tan Undefined");

                return Math.Tan(angle);
            });

            exp = ProcessFunction(exp, "log", Math.Log10);
            exp = ProcessFunction(exp, "sqrt", Math.Sqrt);

            // 🔥 FIXED POWER
            exp = EvaluatePower(exp);

            return exp;
        }

        // 🔹 Helper for trig
        private string ProcessTrig(string exp, string func, Func<double, double> method)
        {
            while (exp.Contains(func + "("))
            {
                int s = exp.LastIndexOf(func + "(");
                int e = exp.IndexOf(")", s);
                string val = exp.Substring(s + func.Length + 1, e - s - func.Length - 1);

                double angle = Convert.ToDouble(new DataTable().Compute(val, null));
                angle = angle * Math.PI / 180;

                double res = method(angle);

                if (Math.Abs(res) < 1e-10) res = 0;

                exp = exp.Replace($"{func}({val})", res.ToString());
            }
            return exp;
        }

        // 🔹 Helper for other functions
        private string ProcessFunction(string exp, string func, Func<double, double> method)
        {
            while (exp.Contains(func + "("))
            {
                int s = exp.LastIndexOf(func + "(");
                int e = exp.IndexOf(")", s);
                string val = exp.Substring(s + func.Length + 1, e - s - func.Length - 1);

                double num = Convert.ToDouble(new DataTable().Compute(val, null));

                double res = method(num);

                exp = exp.Replace($"{func}({val})", res.ToString());
            }
            return exp;
        }

        // 🔹 Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            expression = "";
            txtDisplay.Text = "";
        }

        // 🔹 Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (expression.Length > 0)
            {
                expression = expression.Remove(expression.Length - 1);
                txtDisplay.Text = expression;
            }
        }

        // 🔹 Buttons mapping
        private void btn0_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn1_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn2_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn3_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn4_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn5_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn6_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn7_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn8_Click(object sender, EventArgs e) => Number_Click(sender, e);
        private void btn9_Click(object sender, EventArgs e) => Number_Click(sender, e);

        private void btnPlus_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnSubtract_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnMultiply_Click(object sender, EventArgs e) => Operator_Click(sender, e);
        private void btnDivide_Click(object sender, EventArgs e) => Operator_Click(sender, e);

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}