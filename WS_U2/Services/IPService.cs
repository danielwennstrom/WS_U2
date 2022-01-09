using WS_U2.Interfaces;

namespace WS_U2.Services
{
    public class IPService : IIPService
    {
        public async Task<string> GetIPAsync()
        {
            string ip;

            using (var client = new HttpClient())
            {
                ip = await client.GetStringAsync("https://api.ipify.org");
            }

            return ip;
        }
    }
}