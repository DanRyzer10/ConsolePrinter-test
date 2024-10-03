using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrinter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ServicePrinter();
            string printerName = GetArgumentValue(args, "-p");
            string getPrinters = GetArgumentValue(args, "-l");
            string filePath = GetArgumentValue(args, "-f");

            if (getPrinters != null)
            {
                service.ShowPrinters();
                return;
            }

            if (printerName != null && filePath != null)
            {
                service.SelectPrinter(printerName);
                service.SetFilePath(filePath);
                if (!string.IsNullOrEmpty(service.filePath))
                {
                    service.PrintFile(service.filePath);
                }
                else
                {
                    Console.WriteLine("La ruta del archivo no es válida.");
                }
                return;
            }

            Console.WriteLine("Uso: ConsolePrinter -p <nombre_impresora> -f <ruta_archivo> | -l");
            Console.WriteLine("-p: Nombre de la impresora");
            Console.WriteLine("-f: Ruta del archivo a imprimir");
            Console.WriteLine("-l: Listar impresoras disponibles");
        }

        static string GetArgumentValue(string[] args, string paramName)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == paramName)
                    return args[i + 1];
            }
            return null;
        }
    }
}