using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitChef.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class IngredienteDAO : IDAO<Ingrediente>
    {

        private Connection _connection;

        public IngredienteDAO(Connection connection)
        {
            _connection = connection;
        }

        public Ingrediente FindById(int idI)
        {
            Ingrediente obj = new Ingrediente();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select id, nome, unidade FROM Ingrediente WHERE id = @idI";

                command.Parameters.Add("@idI", SqlDbType.Int).Value = idI;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        obj = new Ingrediente
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            Unidade = row["unidade"].ToString()
                        };
                    }
                }

            }

            if (obj.Id == 0)
            {
                throw new Exception();
            }

            return obj;
        }

       

        public Collection<Ingrediente> FindAll()
        {
            Collection<Ingrediente> ingredientes = new Collection<Ingrediente>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, nome, unidade FROM ingrediente";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Ingrediente ut = new Ingrediente
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            Unidade = row["unidade"].ToString()
                        };
                        ingredientes.Add(ut);
                    }
                }
                return ingredientes;
            }
        }

        public bool Update(Ingrediente obj)
        {
            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Ingrediente Set nome=@nome, unidade=@unidade WHERE id=@ID";

                //... SqlDbType.
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@unidade", SqlDbType.VarChar).Value = obj.Unidade;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = obj.Id;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }

        public bool Remove(Ingrediente obj)
        {
            bool updated = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM ingrediente WHERE id = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = obj.Id;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }

        public Collection<Ingrediente> getGostosFromUtilizador(int idUt)
        {

            Collection<Ingrediente> ingredientes = new Collection<Ingrediente>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT I.id, I.nome, unidade FROM " +
                                       " ( " +
                                            "SELECT * FROM utilizador AS U INNER JOIN Cliente_preferencia AS CP" +
                                                                        "ON U.id = CP.Utilizador_id" +
                                                                        "WHERE U.id = @id AND CP.gosto = 1" +
                                       " ) AS UP " +
                                       "INNER JOIN ingrediente AS I" +
                                       "ON UP.Ingrediente_id = I.id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = idUt;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Ingrediente ut = new Ingrediente
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            Unidade = row["unidade"].ToString()
                        };
                        ingredientes.Add(ut);
                    }
                }
                return ingredientes;
            }
        }

        public Collection<Ingrediente> getEvitadosFromUtilizador(int idUt)
        {

            Collection<Ingrediente> ingredientes = new Collection<Ingrediente>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select I.id, I.nome, I.unidade From Ingrediente I, Cliente_Preferencia C where C.Utilizador_id = @id and C.gosto = 0 and C.Ingrediente_id = I.id;";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = idUt;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Ingrediente ut = new Ingrediente
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            Unidade = row["unidade"].ToString(),
                            Quantidade = 0
                        };
                        ingredientes.Add(ut);
                    }
                }
                return ingredientes;
            }
        }

        public int Insert(Ingrediente obj)
        {

            int id_ing = 0;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO Ingrediente (nome, unidade) VALUES(@nome, @unidade)";


                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@unidade", SqlDbType.VarChar).Value = obj.Unidade;

                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Ingrediente where id =(Select Max(id) from Ingrediente)";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string ing_id = reader["id"].ToString();
                    id_ing = Int32.Parse(ing_id);
                }

            }
            return id_ing;
        }


        public bool FindByName(string name)
        {
            Ingrediente obj = new Ingrediente();
            Boolean existe = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select nome FROM Ingrediente WHERE nome = @name";

                command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;

                if (command.ExecuteReader().HasRows)
                {
                    existe = true;
                }
            }
            return existe;
        }

    }
}