using PCPF.Domain.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PCPF.Infra.AntiCorrupion.SMSGateway
{
    public class SMSGatewayFacade : ISMSGatewayFacade
    {
        private readonly HttpClient _httpClient;

        public SMSGatewayFacade(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Enviar(string destino, string mensagem)
        {
            //var _httpClient = new HttpClient();
            var message = new Message
            {
                sender = Environment.GetEnvironmentVariable("smsSender", EnvironmentVariableTarget.User),
                recipients = destino,
                text = mensagem
            };

            var json = new StringContent(
                JsonSerializer.Serialize(message),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(Environment.GetEnvironmentVariable("smsToken", EnvironmentVariableTarget.User), json);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
