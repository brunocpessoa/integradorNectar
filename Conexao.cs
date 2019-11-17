using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using Aspose.Pdf.Operators;

namespace integrador_nectar_crm
{
    public class Conexao
    {
        private static string serverdb = "localhost";

        string connectionString = "Server=" + serverdb + ";Port=5432;UserID=" + "postgres" + ";password=" + "postgres" + ";Database=" + "nectar" + ";";
        private NpgsqlConnection coneccao;

        public Boolean conecta()
        {
            //Estabelece Ligações a Bases de Dados
            try
            {
                this.coneccao = new NpgsqlConnection(connectionString);
                coneccao.Open();
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(EX.Message, "Erro de ligação");
                return false;
            }

        }
    }
}