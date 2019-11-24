using integrador_nectar_crm;
using System;
using System.Collections.Generic;
using System.Net.Http;

using System.Web.Script.Serialization;

namespace integrador_nectar_crm
{
    public class OportunidadeRepositorio
    {
        HttpClient client = new HttpClient();
        public List<Oportunidade> GetOportunidadesAsync(DateTime dataBuscada)
        {
            string dataDeBusca = Convert.ToString(dataBuscada);
            List<Oportunidade> oportunidades = new List<Oportunidade>();
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync("https://app.nectarcrm.com.br/crm/api/1/oportunidades/?api_token=73d0f6ccb9104c35bf4602d0f4b8ac22&dataInicio=" + dataDeBusca + "&dataFim=" + dataDeBusca + ")").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = response.Content.ReadAsStringAsync().Result;
                        oportunidades = new JavaScriptSerializer().Deserialize<List<Oportunidade>>(resultado);
                        return oportunidades;
                    }
                    return null;
                }
            }
        }
    }
}
