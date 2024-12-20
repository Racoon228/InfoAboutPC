namespace InfoAboutPC
{
    partial class Form1
    {
        /// <summary>
        /// Необходимая переменная дизайнера.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Чистит все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">true, если используется управляемый ресурс; иначе false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный Windows Form Designer

        /// <summary>
        /// Требуемый метод для поддержки конструктора - не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            treeViewInfo = new TreeView();
            chartCPU = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartRAM = new System.Windows.Forms.DataVisualization.Charting.Chart();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();

            ((System.ComponentModel.ISupportInitialize)(chartCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(chartRAM)).BeginInit();
            SuspendLayout();

            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 3; // Три столбца
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F)); // Равное деление
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.RowCount = 1; // Один ряд
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Занимает всю высоту
            tableLayoutPanel.Dock = DockStyle.Fill;

            // 
            // treeViewInfo
            // 
            treeViewInfo.Dock = DockStyle.Fill; // Заполняет ячейку
            treeViewInfo.Name = "treeViewInfo";
            treeViewInfo.Margin = new Padding(0); // Убираем отступы

            // 
            // chartCPU
            // 
            chartCPU.Dock = DockStyle.Fill; // Заполняет ячейку
            chartCPU.Name = "chartCPU";
            chartCPU.Margin = new Padding(0); // Убираем отступы

            // 
            // chartRAM
            // 
            chartRAM.Dock = DockStyle.Fill; // Заполняет ячейку
            chartRAM.Name = "chartRAM";
            chartRAM.Margin = new Padding(0); // Убираем отступы

            // Добавляем элементы в TableLayoutPanel
            tableLayoutPanel.Controls.Add(treeViewInfo, 0, 0); // Первый столбец
            tableLayoutPanel.Controls.Add(chartCPU, 1, 0); // Второй столбец
            tableLayoutPanel.Controls.Add(chartRAM, 2, 0); // Третий столбец

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600); // Устанавливаем фиксированный размер формы
            Controls.Add(tableLayoutPanel); // Добавляем TableLayoutPanel
            Name = "Form1";
            Text = "Утилита для сбора информации о системе";

            ((System.ComponentModel.ISupportInitialize)(chartCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(chartRAM)).EndInit();
            ResumeLayout(false);
        }


        #endregion
        private System.Windows.Forms.TreeView treeViewInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCPU;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRAM;
    }
}