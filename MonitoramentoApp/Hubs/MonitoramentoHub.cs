using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class MonitoramentoHub : Hub
{
    // Esse método será chamado pelo backend para enviar atualizações aos clientes
    public async Task EnviarAtualizacao(string mensagem)
    {
        await Clients.All.SendAsync("ReceberAtualizacao", mensagem);
    }
}
