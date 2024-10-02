using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrinter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simulador Mejorado de Servicio de Impresión");
            var service = new ServicePrinter();

            while (true)
            {
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("1. Mostrar impresoras disponibles");
                Console.WriteLine("2. Seleccionar impresora");
                Console.WriteLine("3. Establecer ruta de archivo");
                Console.WriteLine("4. Imprimir archivo");
                Console.WriteLine("5. Iniciar servicio de impresión automática");
                Console.WriteLine("6. Detener servicio de impresión automática");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        service.ShowPrinters();
                        break;
                    case "2":
                        Console.Write("Ingrese el nombre de la impresora: ");
                        service.SelectPrinter(Console.ReadLine());
                        break;
                    case "3":
                        Console.Write("Ingrese la ruta del archivo: ");
                        service.SetFilePath(Console.ReadLine());
                        break;
                    case "4":
                        if (!string.IsNullOrEmpty(service.filePath))
                        {
                            service.PrintFile(service.filePath);
                        }
                        else
                        {
                            Console.WriteLine("Primero debe establecer la ruta del archivo.");
                        }
                        break;
                    case "5":
                        service.Start();
                        break;
                    case "6":
                        service.Stop();
                        break;
                    case "7":
                        service.Stop();
                        return;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }
    }
}
