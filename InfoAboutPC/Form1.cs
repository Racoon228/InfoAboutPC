
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;

namespace InfoAboutPC
{
    public partial class Form1 : Form
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public Form1()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            SetupCharts();
        }

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        private void SetupCharts()
        {
            chartCPU.Series.Clear();
            var seriesCPU = new Series("CPU Usage")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };
            chartCPU.Series.Add(seriesCPU);

            chartRAM.Series.Clear();
            var seriesRAM = new Series("Available RAM")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };
            chartRAM.Series.Add(seriesRAM);
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            treeViewInfo.Nodes.Clear();
            GetSystemInfo();
            UpdateCharts();
        }

        private void GetSystemInfo()
        {
            var systemInfoNode = new TreeNode("Информация о системе");

            var osNode = new TreeNode("Операционная система");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    osNode.Nodes.Add($"Операционная система: {obj["Caption"]}\n");
                }
            }
            systemInfoNode.Nodes.Add(osNode);

            var userInfoNode = new TreeNode("Информация о пользователе");
            userInfoNode.Nodes.Add($"Имя компьютера: {Environment.MachineName}\n");
            userInfoNode.Nodes.Add($"Пользователь: {Environment.UserName}\n");
            systemInfoNode.Nodes.Add(userInfoNode);

            var processorNode = new TreeNode("Процессор");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    processorNode.Nodes.Add($"Процессор: {obj["Name"]}\n");
                }
            }
            systemInfoNode.Nodes.Add(processorNode);

            var memoryNode = new TreeNode("Оперативная память");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
            {
                long totalMemory = 0;
                foreach (ManagementObject obj in searcher.Get())
                {
                    totalMemory += Convert.ToInt64(obj["Capacity"]);
                }
                memoryNode.Nodes.Add($"Оперативная память: {totalMemory / (1024 * 1024)} MB\n");
            }
            systemInfoNode.Nodes.Add(memoryNode);

            string ipAddress = GetLocalIPAddress(); systemInfoNode.Nodes.Add($"IP-адрес: {ipAddress}\n");

            var disksNode = new TreeNode("Диски");
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    var driveNode = new TreeNode($"Диск: {drive.Name}");
                    driveNode.Nodes.Add($"   Общая емкость: {drive.TotalSize / (1024 * 1024 * 1024)} GB\n");
                    driveNode.Nodes.Add($"   Используемая емкость: {(drive.TotalSize - drive.AvailableFreeSpace) / (1024 * 1024 * 1024)} GB\n");
                    driveNode.Nodes.Add($"   Свободная емкость: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB\n");

                    disksNode.Nodes.Add(driveNode);
                }
            }
            systemInfoNode.Nodes.Add(disksNode);
            treeViewInfo.Nodes.Add(systemInfoNode);
        }

        private void UpdateCharts()
        {
            float cpuUsage = cpuCounter.NextValue();
            float availableRam = ramCounter.NextValue();

            chartCPU.Series["CPU Usage"].Points.AddY(cpuUsage);

            chartRAM.Series["Available RAM"].Points.AddY(availableRam);

            treeViewInfo.Nodes[0].Nodes.Add($"Текущая загрузка CPU: {cpuUsage}%");
            treeViewInfo.Nodes[0].Nodes.Add($"Доступная RAM: {availableRam} MB");
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Не удалось получить IP-адрес";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                localIP = ex.Message;
            }
            return localIP;
        }
    }
}