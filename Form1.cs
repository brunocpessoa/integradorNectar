using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace integrador_nectar_crm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DAL conexao = new DAL();
            var todosRegistros = conexao.GetTodosRegistros();

            OportunidadeRepositorio listaOportunidades = new OportunidadeRepositorio();
            List<Oportunidade> lista = listaOportunidades.GetOportunidadesAsync();

            //conexao.DeletarTodosRegistros();

            lista.ForEach(item => {
                //var objetoSalvar = item.cliente.nome;
                conexao.InserirRegistros(item.id, item.nome);
            });

            dataGridView1.DataSource = todosRegistros;
            //var oportunidadeGeral = listaOportunidades.GetOportunidadesAsync();
        }
    }
}
