using Calculator_Proga.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator_Proga.ViewModels
{
    class MainWindowViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        int n = 16; //ограничение на размер числа калькулятора
        bool negative = false; //отрицательное или положительное число

        //Первое число
        private string number1;
        public string Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }
        //

        //Второе и последующее число
        private string number2;
        public string Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }
        //

        //Число ввода/вывода
        private string inOut = "0";
        public string InOut
        {
            get => inOut;
            set
            {
                inOut = value;
                OnPropertyChanged();
            }
        }
        //

        //Выбор команды на 2 числа
        private string action = "";
        public string Action
        {
            get => action;
            set
            {
                action = value;
                OnPropertyChanged();
            }
        }
        //

        //Выбор команды на 1 число
        private string equ = "";
        public string Equ
        {
            get => equ;
            set
            {
                equ = value;
                OnPropertyChanged();
            }
        }
        //

        //Результат
        private double result = 0;
        public double Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }
        //

        //Команда вычисления результата
        public ICommand CalcMathCommand { get; }

        private void OnCalcMathCommandExecute(object p)
        {
            Equ = p.ToString();
            if (Equ == "=")
            {
                if (Result == 0)
                {
                    Number2 = InOut;
                }
                else
                {
                    Number1 = Result.ToString();
                }
                switch (Action)
                {
                    case "+":
                        Result = CalcMath.Add(Convert.ToDouble(Number1), Convert.ToDouble(Number2));
                        break;
                    case "-":
                        Result = CalcMath.Sub(Convert.ToDouble(Number1), Convert.ToDouble(Number2));
                        break;
                    case "x":
                        Result = CalcMath.Mul(Convert.ToDouble(Number1), Convert.ToDouble(Number2));
                        break;
                    case "/":
                        Result = CalcMath.Div(Convert.ToDouble(Number1), Convert.ToDouble(Number2));
                        break;
                    case "%":
                        Result = CalcMath.Proc(Convert.ToDouble(Number1), Convert.ToDouble(Number2));
                        break;
                }
            }
            else
            {
                switch (Equ)
                {
                    case "squ":
                        Result = CalcMath.Squ(Convert.ToDouble(InOut));
                        Equ = "√" + InOut.ToString();
                        break;
                    case "pow":
                        Result = CalcMath.Pow(Convert.ToDouble(InOut));
                        Equ = InOut.ToString() + "²";
                        break;
                    case "dec":
                        Result = CalcMath.Dec(Convert.ToDouble(InOut));
                        Equ = "1/" + InOut.ToString();
                        break;
                }
            }
            InOut = Result.ToString();
            negative = false;
        }
        //

        //Команда ввода числа
        public ICommand ButtonNumberCommand { get; }

        private void OnButtonNumberCommandExecute(object p)
        {
            if (Result != 0)
            {
                Number1 = "";
                Number2 = "";
                InOut = "0";
                Action = "";
                Equ = "";
                Result = 0;
            }
            if (InOut == "0")
            {
                if ((string)p != ",")
                {
                    InOut = "";
                }
            }
            if (InOut.Length < n) //Ограничение на размер числа
            {
                if ((string)p == "-" && negative == false)
                {
                    InOut = "-" + InOut;
                    negative = true;
                }
                else
                {
                    if ((string)p == "-" && negative == true)
                    {
                        InOut = InOut.Substring(1);
                        negative = false;
                    }
                    else
                    {
                        InOut += p.ToString();
                    }
                }
            }
        }
        //

        //Команда кнопок выбора действия
        public ICommand ButtonActionCommand { get; }

        private void OnButtonActionCommandExecute(object p)
        {
            Action = p.ToString();
            Number1 = InOut;
            InOut = "0";
            negative = false;
        }
        //

        //Команда кнопок редактирования ввода
        public ICommand ButtonEditCommand { get; }

        private void OnButtonEditCommandExecute(object p)
        {
            switch ((string)p)
            {
                case "⌫":
                    InOut = InOut.Remove(InOut.Length - 1);
                    break;
                case "CE":
                    InOut = "0";
                    break;
                case "C":
                    Number1 = "";
                    Number2 = "";
                    InOut = "0";
                    Action = "";
                    Equ = "";
                    Result = 0;
                    break;
            }
        }
        //

        //Команды математических вычислений
        public MainWindowViewModels()
        {
            CalcMathCommand = new RelayCommand(OnCalcMathCommandExecute);
            ButtonNumberCommand = new RelayCommand(OnButtonNumberCommandExecute);
            ButtonActionCommand = new RelayCommand(OnButtonActionCommandExecute);
            ButtonEditCommand = new RelayCommand(OnButtonEditCommandExecute);
        }
        //
    }
}
