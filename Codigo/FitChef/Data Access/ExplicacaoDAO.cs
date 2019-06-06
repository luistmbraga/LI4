using FitChef.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class ExplicacaoDAO
    {
        private Connection _connection;

        public ExplicacaoDAO(Connection connection)
        {
            this._connection = connection;
        }

        public Explicacao FindById(int idE)
        {
            Explicacao obj = new Explicacao();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select url, video, id, duvida, Passo_id From Explicacao where id = @idE";

                command.Parameters.Add("@idE", SqlDbType.Int).Value = idE;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        obj = new Explicacao
                        {
                            Url = row["url"].ToString(),
                            Video = (bool)row["video"],
                            Id = (int)row["id"],
                            Duvida = row["duvida"].ToString(),
                            IdP = (int)row["IdP"]
                        };
                    }
                }
            }

            return obj;
        }

        public Collection<Explicacao> FindAllExplicacoesPasso(int idE)
        {
            Collection<Explicacao> resultado = new Collection<Explicacao>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select url, video, id, duvida, Passo_id From Explicacao where id = @idE";

                command.Parameters.Add("@idE", SqlDbType.Int).Value = idE;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Explicacao obj = new Explicacao
                        {
                            Url = row["url"].ToString(),
                            Video = (bool)row["video"],
                            Id = (int)row["id"],
                            Duvida = row["duvida"].ToString(),
                            IdP = (int)row["IdP"]
                        };

                        resultado.Add(obj);
                    }
                }
            }

            return resultado;
        }


        public bool Insert(Explicacao inserir)
        {
            bool resultado = true;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Insert Into Explicacao Values(@url, @video, @duvida, @Passo_id)";

                command.Parameters.Add("@url", SqlDbType.VarChar).Value = inserir.Url;
                command.Parameters.Add("@video", SqlDbType.Binary).Value = inserir.Video;
                command.Parameters.Add("@duvida", SqlDbType.VarChar).Value = inserir.Duvida;
                command.Parameters.Add("@Passo_id", SqlDbType.Int).Value = inserir.IdP;

                resultado = (command.ExecuteNonQuery() > 0);

            }

            return resultado;
        }

    }
}