namespace FractionCalculator
{
    partial class Form1//partial означает: «Этот класс определён в нескольких файлах». 
    {
        private System.ComponentModel.IContainer components = null;// управления ресурсами, созданными визуальным дизайнером(например, таймеры, иконки)

        protected override void Dispose(bool disposing)//Метод, вызываемый при закрытии формы, чтобы освободить ресурсы (память, файлы, соединения ). 
        {
            if (disposing && (components != null))
            {
                components.Dispose();//Освобождает управляемые ресурсы
            }
            base.Dispose(disposing);//вызывает метод Dispose у родительского класса (Form),
        }

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;// форма масштабируется пропорционально размеру системного шрифта
            this.ClientSize = new System.Drawing.Size(800, 450); //Задаёт начальный размер клиентской области формы
            this.Text = "Form1"; //Устанавливает заголовок окна(текст в верхней панели).
        }
    }
}