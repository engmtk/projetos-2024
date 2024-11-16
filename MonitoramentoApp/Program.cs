using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o SignalR
builder.Services.AddSignalR();

// Adicionar serviços MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar rotas e hubs do SignalR
app.MapHub<MonitoramentoHub>("/monitoramentoHub"); // Definindo o endpoint do hub

// Configuração de rotas para os controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Monitoramento}/{action=Index}/{id?}");

app.Run();
