using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace integrador_nectar_crm
{
    public class OportunidadeRepositorio
    {
        HttpClient client = new HttpClient();
        public async Task<List<Oportunidade>> GetOportunidadesAsync()
        {
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync("https://app.nectarcrm.com.br/crm/api/1/oportunidades/?api_token=73d0f6ccb9104c35bf4602d0f4b8ac22&dataInicio=01/10/2019&dataFim=31/10/2019").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var CalcaQuadrada = response.Content.ReadAsStringAsync().Result;
                        var teta = "";
                        return null;
                    }
                    return null;
                }
                return null;
            }
        }

    }
}
