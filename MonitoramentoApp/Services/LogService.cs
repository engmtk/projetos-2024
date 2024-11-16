using System;
using System.IO;

namespace MonitoramentoApp.Services // Ou MonitoramentoApp.Utilities
{
    public class LogService
    {
        private readonly string _logFilePath;

        // Construtor para definir o caminho do arquivo de log
        public LogService(string logFilePath = @"C:\Logs\Log_exec_proc.log")
        {
            _logFilePath = logFilePath;
        }

        // Método para escrever no log
        public void EscreverLog(string mensagem)
        {
            try
            {
                // Verifica se o diretório existe, se não existir cria
                string directoryPath = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Escreve a mensagem no arquivo de log
                using (StreamWriter sw = new StreamWriter(_logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now}: {mensagem}");
                }
            }
            catch (Exception ex)
            {
                // Se houver algum erro ao gravar no log, capture a exceção
                Console.WriteLine($"Erro ao escrever no log: {ex.Message}");
            }
        }
    }
}
