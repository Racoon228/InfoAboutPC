using System.Diagnostics;
using System.Management;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;

namespace InfoAboutPC
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public Form1()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            SetupCharts();
            GetSystemInfo();
        }

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            float currentCpuUsage = cpuCounter.NextValue();
            float currentRamUsage = ramCounter.NextValue();
            chartCPU.Series["CPU Usage"].Points.AddY(currentCpuUsage);
            chartRAM.Series["RAM Usage"].Points.AddY(currentRamUsage);
            if (chartCPU.Series["CPU Usage"].Points.Count > 60)
                chartCPU.Series["CPU Usage"].Points.RemoveAt(0);
            if (chartRAM.Series["RAM Usage"].Points.Count > 60)
                chartRAM.Series["RAM Usage"].Points.RemoveAt(0);
        }

        private void SetupCharts()
        {
            ChartArea chartAreaCPU = new ChartArea("CPUUsageArea");
            chartCPU.ChartAreas.Add(chartAreaCPU);
            var seriesCPU = new Series("CPU Usage")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2,
                IsValueShownAsLabel = false
            };
            chartCPU.Series.Add(seriesCPU);

            ChartArea chartAreaRAM = new ChartArea("RAMUsageArea");
            chartRAM.ChartAreas.Add(chartAreaRAM);
            var seriesRAM = new Series("RAM Usage")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                IsValueShownAsLabel = false
            };
            chartRAM.Series.Add(seriesRAM);
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

            var processorNode = new TreeNode("Процессор");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    foreach (PropertyData property in obj.Properties)
                    {
                        string propertyName = property.Name;
                        string propertyValue = property.Value?.ToString() ?? "Не доступно";
                        processorNode.Nodes.Add($"{propertyName}: {propertyValue}");
                    }
                }
            }
            systemInfoNode.Nodes.Add(processorNode);

            var gpuNode = new TreeNode("Видеокарта");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString() ?? "Не доступно";
                    string memory = (obj["AdapterRAM"] != null ? Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024) + " MB" : "Не доступно");
                    string driverVersion = obj["DriverVersion"]?.ToString() ?? "Не доступно";

                    gpuNode.Nodes.Add($"Название: {name}");
                    gpuNode.Nodes.Add($"Память: {memory}");
                    gpuNode.Nodes.Add($"Версия драйвера: {driverVersion}");
                }
            }
            systemInfoNode.Nodes.Add(gpuNode);

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

            var virtualMemoryNode = new TreeNode("Виртуальная память");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string totalVirtualMemory = Convert.ToInt64(obj["TotalVirtualMemorySize"]) / 1024 + " MB";
                    string freeVirtualMemory = Convert.ToInt64(obj["FreeVirtualMemory"]) / 1024 + " MB";
                    long usedVirtualMemoryBytes = Convert.ToInt64(obj["TotalVirtualMemorySize"]) - Convert.ToInt64(obj["FreeVirtualMemory"]);
                    string usedVirtualMemory = usedVirtualMemoryBytes / 1024 + " MB";

                    virtualMemoryNode.Nodes.Add($"Общий объем: {totalVirtualMemory}");
                    virtualMemoryNode.Nodes.Add($"Свободно: {freeVirtualMemory}");
                    virtualMemoryNode.Nodes.Add($"Используется: {usedVirtualMemory}");
                }
            }
            systemInfoNode.Nodes.Add(virtualMemoryNode);
            treeViewInfo.Nodes.Add(systemInfoNode);

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

            var fileTypesNode = new TreeNode("Файловые типы");

            using (var classesRoot = Microsoft.Win32.Registry.ClassesRoot)
            {
                foreach (var subKeyName in classesRoot.GetSubKeyNames())
                {
                    if (subKeyName.StartsWith("."))
                    {
                        var subKey = classesRoot.OpenSubKey(subKeyName);

                        string fileType = subKey?.GetValue("")?.ToString() ?? "Не указан";

                        var extensionNode = new TreeNode($"Расширение: {subKeyName}");
                        extensionNode.Nodes.Add($"Тип файла: {fileType}");

                        if (!string.IsNullOrEmpty(fileType))
                        {
                            var typeKey = classesRoot.OpenSubKey(fileType);
                            if (typeKey != null)
                            {
                                foreach (var valueName in typeKey.GetValueNames())
                                {
                                    string value = typeKey.GetValue(valueName)?.ToString() ?? "Не доступно";
                                    extensionNode.Nodes.Add($"{valueName}: {value}");
                                }
                            }
                        }
                        fileTypesNode.Nodes.Add(extensionNode);
                    }
                }
            }
            treeViewInfo.Nodes.Add(fileTypesNode);
        }
    }
}