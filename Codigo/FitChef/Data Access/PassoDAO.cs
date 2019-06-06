using FitChef.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class PassoDAO
    {
        private Connection _connection;

        public PassoDAO(Connection connection)
        {
            this._connection = connection;
        }

        public Passo FindById(int idP)
        {
            Passo obj = new Passo();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select id, descricao " + "From Passo " + "Where id = @idP";

                command.Parameters.Add("@idP", SqlDbType.Int).Value = idP;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        obj = new Passo
                        {
                            Id = (int)row["id"],
                            Descricao = row["descricao"].ToString()
                        };
                    }
                }
            }

            return obj;
        }

        

        public Collection<Passo> FindAll()
        {
            Collection<Passo> resultado = new Collection<Passo>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select id, descricao From Passo";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Passo novo = new Passo
                        {
                            Id = (int)row["id"],
                            Descricao = row["descricao"].ToString()
                        };
                        resultado.Add(novo);
                    }
                }
            }

            return resultado;
        }

        public Collection<Explicacao> FindExplicacoesFromPasso(int id)
        {
            Collection<Explicacao> resultado = new Collection<Explicacao>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select url, video, id, duvida, Passo_id From Explicacao where Passo_id = @id;";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Explicacao nova = new Explicacao
                        {
                            Url = row["url"].ToString(),
                            Video = (bool)row["video"],
                            Id = (int)row["id"],
                            Duvida = row["duvida"].ToString(),
                            IdP = (int)row["Passo_id"]
                        };
                        resultado.Add(nova);
                    }
                }
            }

            return resultado;
        }

        public int Insert(Passo obj)
        {
            int id_passo = 1;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO Passo (descricao) VALUES(@descricao)";

                command.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;

                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Passo where id =(Select Max(id) from Passo)";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string passo_id = reader["id"].ToString();
                    id_passo = Int32.Parse(passo_id);
                }

            }

            return id_passo;
        }
    }
}