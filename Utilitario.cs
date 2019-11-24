using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integrador_nectar_crm
{
    public class Utilitario
    {
        public DateTime buscaUltimoDiaDoMes(DateTime inicio)
        {
            DateTime ultimoDiaDoMes = new DateTime(inicio.Year, inicio.Month, DateTime.DaysInMonth(inicio.Year, inicio.Month));
            return ultimoDiaDoMes;
        }

        public int qtdDiasASeremBuscadosNaAPI (DateTime dataInicio)
        {
            TimeSpan date = Convert.ToDateTime(DateTime.Today) - Convert.ToDateTime(dataInicio);

            int totalDias = date.Days;

            return totalDias;
        }
    }
}
