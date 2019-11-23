using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace integrador_nectar_crm
{
    public class DAL
    {
        static string serverName = "localhost";
        static string port = "5432";
        static string userName = "postgres";
        static string password = "postgres";
        static string databaseName = "nectar";
        NpgsqlConnection pgsqlConnection = null;
        string connString = null;

        public DAL()
        {
            connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                        serverName, port, userName, password, databaseName);
        }

        //Oportunidades
        public DataTable GetTodasOportunidades()
        {

            DataTable dt = new DataTable();

            try
            {
                using (pgsqlConnection = new NpgsqlConnection(connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    pgsqlConnection.Open();
                    string cmdSeleciona = "Select * from oportunidade order by id";

                    using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                    {
                        Adpt.Fill(dt);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }

            return dt;
        }

        public DataTable GetOportunidadePorId(int id)
        {

            DataTable dt = new DataTable();

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL
                    pgsqlConnection.Open();
                    string cmdSeleciona = "Select * from oportunidade Where id = " + id;

                    using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                    {
                        Adpt.Fill(dt);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
            return dt;
        }

        public void InserirOportunidades(int idOportunidade, string nome, string responsavel, string autor,
            string autorAtualizacao, int codFarmacia, string funilDeVendas, string origem, string agente,
            string software_concorrente, string campanha, string indicador_trier_mais_1, double valor_total)
        {

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdInserir = $"Insert Into oportunidade(id,nome,responsavel,autor," +
                        $" autor_atualizacao,  cod_farmacia,  funil_vendas, origem, agente, " +
                        $"software_concorrente, campanha, indicador_trier_mais_1, valor_total) " +
                        $"values({idOportunidade},'{nome}','{responsavel}','{autor}','{autorAtualizacao}'," +
                        $"{codFarmacia},'{funilDeVendas}','{origem}','{agente}','{software_concorrente}','{campanha}'," +
                        $"'{indicador_trier_mais_1}','{valor_total}')";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        public void AtualizarOportunidade(int codigo, string email, int idade)
        {
            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdAtualiza = String.Format("Update oportunidades Set email = '" + email + "' , idade = " + idade + " Where id = " + codigo);

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        public void DeletarTodasOportunidades()
        {
            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //abre a conexao                
                    pgsqlConnection.Open();

                    string cmdDeletar = String.Format("DELETE FROM oportunidade");

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletar, pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }
        
        public void ImportacaoGeral()
        {
            //apenas teste
            //DeletarTodasOportunidades();

            DAL conexao = new DAL();
            var todosRegistros = conexao.GetTodasOportunidades();

            OportunidadeRepositorio listaOportunidades = new OportunidadeRepositorio();
            List<Oportunidade> lista = listaOportunidades.GetOportunidadesAsync();

            conexao.DeletarTodasOportunidades();

            lista.ForEach(item =>
            {
                conexao.InserirOportunidades(item.id, item.nome, item.responsavel.nome, item.autor.nome,
                    item.autorAtualizacao.nome, Convert.ToInt32(item.cliente.codigo), item.funilVenda.nome, item.origem.nome, item.camposPersonalizados.agente,
                   item.camposPersonalizados.Software_Concorrente, item.camposPersonalizados.campanha,
                   item.camposPersonalizados.Indicador_Trier_Mais_1, item.valorTotal);
            });
        }
    }
}