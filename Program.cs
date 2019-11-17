using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;

namespace integrador_nectar_crm
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //WriteLine("Aguardando retorno da API");
            var repositorio = new OportunidadeRepositorio();
            var oportunidadesGeral = repositorio.GetOportunidadesAsync();

            oportunidadesGeral.ContinueWith(task =>
            {
                var oportunidades = task.Result;
                foreach (var o in oportunidades)
                    WriteLine(o.ToString());

                Environment.Exit(0);
            },
            TaskContinuationOptions.OnlyOnRanToCompletion
            );

            ReadLine();
        }
    }
}
