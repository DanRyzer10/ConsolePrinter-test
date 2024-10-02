using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsolePrinter
{
    internal class ServicePrinter
    {
        private Timer _timer;
        private string selectedPrinter;
        private bool isRunning = false;
        public string filePath;

        public ServicePrinter()
        {
        }

        public string[] getAllPrinters()
        {
            return PrinterSettings.InstalledPrinters.Cast<string>().ToArray();

        }
        public void Start()
        {
            if (!isRunning)
            {
                Console.WriteLine("Iniciando servicio de impresion...");
                _timer = new Timer(AutoPrintJob, null, 0, 60000);
            }
        }

        public void Stop()
        {

            if (isRunning)
            {
                Console.WriteLine("Deteniendo servicio de impresion...");
                _timer.Dispose();
                isRunning = false;
            }
        }
        private void AutoPrintJob(object state)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                Console.WriteLine($"Imprimiendo archivo: {filePath}");
                PrintFile(filePath);
            }
        }

        public void ShowPrinters()
        {
            Console.WriteLine("Impresoras disponibles:");
            string[] printers = this.getAllPrinters();
            foreach (string printerName in printers)
            {
                Console.WriteLine(printerName);
            }
        }
        public void SelectPrinter(string printerName)
        {
            var printerSettigs = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
            if (!printerSettigs.Contains(printerName))
            {
                Console.WriteLine("Impresora no encontrada.");
            }
            else
            {
                selectedPrinter = printerName;
                Console.WriteLine($"Impresora seleccionada: {selectedPrinter}");
            }
        }

        public void SetFilePath(string path)
        {
            if (File.Exists(path))
            {
                filePath = path;
                Console.WriteLine($"Archivo seleccionado: {filePath}");
            }
            else
            {
                Console.WriteLine("Archivo no encontrado.");
            }
        }

        public void PrintFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Archivo no encontrado.");
                return;
            }

            try
            {
                using (PrintDocument pd = new PrintDocument())
                {
                    pd.PrinterSettings.PrinterName = selectedPrinter;
                    pd.PrintController = new StandardPrintController();
                    pd.Print();
                }
                Console.WriteLine("Documento enviado a imprimir.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al imprimir: {ex.Message}");
            }
        }

    }
}
