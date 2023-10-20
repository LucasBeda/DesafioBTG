using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Consumer
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
            var config = new ConsumerConfig()
            {
                BootstrapServers = configuration["KafkaConfiguration:Server"],
                GroupId = configuration["KafkaConfiguration:Group"],
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false,
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                try
                {
                    consumer.Subscribe(configuration["KafkaConfiguration:Topic"]);
                    while (true)
                    {
                        var consumerReport = consumer.Consume();

                        if (consumerReport.Message != null && consumerReport.Message.Value.Contains("bitcoin"))
                            await client.PostAsync(client.BaseAddress + "/SalvarCotacao", new StringContent(consumerReport.Message.Value, Encoding.UTF8, "application/json"));
                    }
                }
                catch (Exception)
                {
                    consumer.Close();
                    throw;
                }
            }
        }
    }
}