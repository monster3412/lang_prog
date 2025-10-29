using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FractionCalculator
{
    public partial class Form1 : Form
    {

        private TextBox txtFrac1, txtFrac2, txtPower, txtResult;
        //txtFrac1 — куда вводят первую дробь(например, 2/3)
        //txtFrac2 — вторую дробь
        //txtPower — степень(например, 2)
        //txtResult — сюда покажется ответ

        private bool isDarkTheme = true;//флажок, какая тема. false = светлая тема

        private Color primaryColor, secondaryColor, textColor, bgColor;
        //primaryColor — основной цвет кнопок
        //для второстепенных элементов, например, цвета кнопок при наведении курсора.
        //textColor — цвет текста
        //bgColor — цвет фона окна
        //Пока пустые — позже мы их наполним.

        public Form1()
        {
            InitializeComponent();//базовая настройка окна

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);//чтобы окно не мигало, когда мы что-то меняем (например, переключаем тему).

            this.DoubleBuffered = true;//тоже самое

            this.StartPosition = FormStartPosition.CenterScreen;//Окно появится ровно по центру экрана.

            SetupTheme();//Выбери цвета (тёмные или светлые)

            SetupUI();//
        }
        private void SetupTheme()//Функция «настройки цвета». 
        {
            if (isDarkTheme)
                {
                    primaryColor = Color.FromArgb(90, 90, 138); // тёмно-фиолетовый
                    secondaryColor = Color.FromArgb(123, 123, 170);
                    textColor = Color.White;                   // белый текст
                    bgColor = Color.FromArgb(30, 30, 46);      // почти чёрный фон
                }
            else
                {
                    primaryColor = Color.FromArgb(233, 236, 239);
                    secondaryColor = Color.FromArgb(222, 226, 230);
                    textColor = Color.Black;
                    bgColor = Color.FromArgb(248, 249, 250);
                }
            this.BackColor = bgColor;
            this.ForeColor = textColor;//Применяем цвета к фону окна и тексту по умолчанию
        }
        private void SetupUI()//Функция «нарисуй интерфейс»
        {
            this.Text = "🧮 Калькулятор дробей";
            this.Size = new Size(700, 600);
            // Создаём элементы
            var lblTitle = new Label { Text = "Калькулятор дробей", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = textColor, Location = new(20, 15), AutoSize = true };

            //Создаём надпись(Label) с заголовком.  new Font("Segoe UI", 16, FontStyle.Bold)- крупный жирный шрифт
            //Location = new(20, 15)-  отступ 20 пикселей от левого края, 15 — от верха. 

              
            var lbl1 = new Label { Text = "Дробь 1 (P/Q):", Location = new(20, 55), ForeColor = textColor, AutoSize = true };
            //Поле для ввода
           txtFrac1 = new TextBox { Text = "2/3", Location = new(130, 55), Width = 120 };

            var lbl2 = new Label { Text = "Дробь 2 (P/Q):", Location = new(20, 90), ForeColor = textColor, AutoSize = true };
            txtFrac2 = new TextBox { Text = "3/4", Location = new(130, 90), Width = 120 };

            var lbl3 = new Label { Text = "Степень n:", Location = new(20, 125), ForeColor = textColor, AutoSize = true };
            txtPower = new TextBox { Text = "2", Location = new(130, 125), Width = 60 };

            var lblResult = new Label { Text = "Результат:", Location = new(20, 160), ForeColor = textColor, AutoSize = true };
            txtResult = new TextBox { Location = new(20, 185), Size = new Size(640, 300), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            //Это большое поле, куда выводится ответ.
            //Multiline = true → можно писать много строк
            //ReadOnly = true → пользователь не может туда писать
            //ScrollBars → появится полоса прокрутки, если текст не помещается

            // Кнопки
            //Создаём кнопку с текстом «➕ Сложить»
            var btnAdd = new Button { Text = "➕ Сложить", Location = new(270, 55), Size = new(100, 30) };
            
            btnAdd.Click += (s, e) => Compute((a, b) => a + b);
            //Когда на неё нажмут, выполнится:
            //→ Возьми первую дробь(a) и вторую(b)
            //→ Сложи их(a +b)
            //→ Покажи результат

            //Но как работает? Click -событие когда на кнопку кликнули мышкой

            //+= -  Добавь эту функцию в список тех, кто должен сработать при клике

            //(s, e) => лямбда-выражение — короткая запись функции без имени.(s, e) — это параметры, которые Windows автоматически передаёт при клике:

            // s(или sender) — это объект, который вызвал событие(в нашем случае — сама кнопка btnAdd).

            //e(или EventArgs) — дополнительная информация о событии(например, координаты клика). Для простого клика она почти не нужна.

            // Compute((a, b) => a + b) принимает один аргумент: операцию над двумя дробями.(a, b) => a + b — это ещё одна лямбда!
            //Но так как a и b — это объекты типа Fraction, то a + b вызывает собственный оператор +, который  написан в классе Fraction.
            var btnToggle = new Button { Text = "☀️ Светлая", Location = new(580, 510), Size = new(100, 30) };//Кнопка смены темы

            btnToggle.Click += (s, e) =>
            {
                isDarkTheme = !isDarkTheme; // переверни флажок
                SetupTheme();               // перенастрой цвета
                btnToggle.Text = isDarkTheme ? "☀️ Светлая" : "🌙 Тёмная";
                foreach (Control c in Controls)//Controls — это список всех элементов, которые  на него положены:
                {
                    c.ForeColor = textColor;//Устанавливает цвет текста у каждого элемента (кнопки, поля, надписи) на текущий — белый или чёрный, в зависимости от темы. 
                    if (c is TextBox tb) //Является ли текущий элемент(c) полем ввода(TextBox)
                        tb.BackColor = isDarkTheme ? Color.FromArgb(40, 40, 60) : Color.White;//В тёмной теме — тёмно-серый (40, 40, 60)В светлой — белый
                }
                this.BackColor = bgColor;
                // и обнови цвета у всех элементов
            }; //жмяк — тема меняется на противоположную. 

            Controls.AddRange(new Control[] {
                lblTitle, lbl1, txtFrac1, lbl2, txtFrac2, lbl3, txtPower, lblResult, txtResult, btnAdd, btnToggle
            });//здеся собираем все элементы
        }
        // =============== ЛОГИКА ДРОБЕЙ ===============
        private Fraction Parse(string input)//Что делает этот метод? Он принимает строку(например, "2/3" или "5") и возвращает объект типа Fraction — то есть настоящую дробь, с которой можно считать.
        {// Fraction - обственный тип данных,
            input = input.Trim();//Убирает лишние пробелы в начале и конце
            if (input.Contains("/"))//Проверяет: есть ли в строке символ /?
            {
                var parts = input.Split('/');//Разбивает строку на части по символу /. "2/3" → ["2", "3"]
                return new Fraction(int.Parse(parts[0]), int.Parse(parts[1]));//числитель = 2, знаменатель = 3
            }
            return new Fraction(int.Parse(input), 1);// "5" → Fraction(5, 1)
        }

        private void Compute(Func<Fraction, Fraction, Fraction> op)
        {
            try
            {
                var a = Parse(txtFrac1.Text);
                var b = Parse(txtFrac2.Text);
                var res = op(a, b);
                txtResult.Text = $"{a} и {b} → результат: {res}";
            }
            catch (Exception ex)
            {
                txtResult.Text = "Ошибка: " + ex.Message;
            }
        }
    }
    public class Fraction
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public Fraction(int num, int den)
        {
            if (den == 0) throw new ArgumentException("Знаменатель не может быть 0.");
            if (den < 0) { num = -num; den = -den; }

            int gcd = GCD(Math.Abs(num), den);
            Numerator = num / gcd;
            Denominator = den / gcd;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0) (a, b) = (b, a % b);
            return a;
        }

        public static Fraction operator +(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                         a.Denominator * b.Denominator);

        public override string ToString() =>
            Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";
    }


}



    
