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
            btnGetInfo = new Button();
            treeViewInfo = new TreeView();
            chartCPU = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartRAM = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(chartCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(chartRAM)).BeginInit();
            SuspendLayout();
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Dock = System.Windows.Forms.DockStyle.Top;
            btnGetInfo.Location = new Point(14, 14);
            btnGetInfo.Margin = new Padding(4, 3, 4, 3);
            btnGetInfo.Name = "btnGetInfo";
            btnGetInfo.Size = new Size(303, 27);
            btnGetInfo.TabIndex = 0;
            btnGetInfo.Text = "Получить информацию о системе";
            btnGetInfo.UseVisualStyleBackColor = true;
            btnGetInfo.Click += btnGetInfo_Click;
            // 
            // treeViewInfo
            // 
            this.treeViewInfo.Dock = System.Windows.Forms.DockStyle.Left;
            treeViewInfo.Location = new Point(14, 58);
            treeViewInfo.Margin = new Padding(4, 3, 4, 3);
            treeViewInfo.Name = "treeViewInfo";
            treeViewInfo.Size = new Size(400, 500);
            treeViewInfo.TabIndex = 1;
            // 
            // chartCPU
            // 
            chartCPU.Dock = DockStyle.Right;
            chartCPU.Location = new Point(317, 58);
            chartCPU.Name = "chartCPU";
            chartCPU.Size = new Size(470, 200);
            chartCPU.TabIndex = 2;
            chartCPU.Text = "chartCPU";
            // 
            // chartRAM
            // 
            chartRAM.Dock = DockStyle.Bottom;
            chartRAM.Location = new Point(14, 288);
            chartRAM.Name = "chartRAM";
            chartRAM.Size = new Size(773, 182);
            chartRAM.TabIndex = 3;
            chartRAM.Text = "chartRAM";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 482);
            Controls.Add(chartRAM);
            Controls.Add(treeViewInfo);
            Controls.Add(chartCPU);
            Controls.Add(btnGetInfo);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Утилита для сбора информации о системе";
            ((System.ComponentModel.ISupportInitialize)(chartCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(chartRAM)).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.TreeView treeViewInfo; // Объявляем TreeView
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCPU; // Объявляем график CPU
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRAM; // Объявляем график RAM
    }
}