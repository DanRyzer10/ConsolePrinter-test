using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Diagnostics;

namespace PrinterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        private string execPath = "C:\\Users\\vscod\\OneDrive\\Documentos\\exec\\ConsolePrinter.exe";

        [HttpPost("print")]

        public async Task<IActionResult> Print(IFormFile file, [FromForm] string printerName)
        {
            if(file==null || file.Length == 0)
            
                return BadRequest("archivo vacio");

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var startInfo = new ProcessStartInfo
            {
                FileName = this.execPath,
                Arguments = $"/t -f \"{filePath}\" -p \"{printerName}\" ",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo))
            {
                if(process==null) return BadRequest("No se pudo iniciar el proceso");
                var output = await process.StandardOutput.ReadToEndAsync();
                return Ok(output);
            }
        }
        [HttpGet("printers")]
        public async Task<IEnumerable<string>> GetPrinters()
        {
            if (!System.IO.File.Exists(this.execPath))
            {
                return [
                              "Archivo no encontrado"
                    ];
            }
            var startInfo = new ProcessStartInfo
            {
                FileName = this.execPath,
                RedirectStandardOutput = true,
                Arguments = "-l hola",
                UseShellExecute = false,
                RedirectStandardError = true,  // Opcional para capturar errores
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo))
            {
                if(process==null) return new List<string>();
                process.WaitForExit();
                try
                {
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var printers = output.Split(Environment.NewLine);
                    return printers;

                }catch(Exception e)
                {
                    return new List<string>() { 
                        e.Message
                    
                    };
                }
           
            }

        }

    }
}
