using Microsoft.Extensions.Configuration;
using System.Text;
using Websocket.Client;

namespace WebSocketCotacao
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var client = new HttpClient() { BaseAddress = new Uri(configuration["EndPointAPI"]) };
            var exitEvent = new ManualResetEvent(false);
            var websocket = new WebsocketClient(new Uri(configuration["EndPointWebsocket"]));
            websocket.ReconnectTimeout = TimeSpan.FromSeconds(10);

            websocket.MessageReceived.Subscribe(async msg =>
            {
                await client.PostAsync(client.BaseAddress + "/EnviarCotacaoMensageriaKafka", new StringContent(msg.Text, Encoding.UTF8, "application/json"));
            });

            websocket.Start();
            exitEvent.WaitOne();
        }
    }
}