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
    public class UtensilioDAO : IDAO<Utensilio>
    {

        private Connection _connection;

        public UtensilioDAO(Connection connection)
        {
            _connection = connection;
        }


        public Collection<Utensilio> FindAll()
        {
            Collection<Utensilio> utensilios = new Collection<Utensilio>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, nome FROM utensilio";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Utensilio ut = new Utensilio
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                        };
                        utensilios.Add(ut);
                    }
                }
                return utensilios;
            }
        }

        public bool Update(Utensilio obj)
        {
            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utensilio Set nome=@nome WHERE id=@ID";

                //... SqlDbType.
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = obj.Id;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }



        public Utensilio FindById(int idI)
        {
            Utensilio obj = new Utensilio();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select id, nome FROM Utensilio WHERE id = @idI";

                command.Parameters.Add("@idI", SqlDbType.Int).Value = idI;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        obj = new Utensilio
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString()
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



        

        public int Insert(Utensilio obj)
        {
            int id_ut = 0;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO utensilio (nome) VALUES(@nome)";


                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Utensilio where id =(Select Max(id) from Utensilio)";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string utensilio_id = reader["id"].ToString();
                    id_ut = Int32.Parse(utensilio_id);
                }
            }
            return id_ut;
        }

        public bool AddReceitaUtensilio(int idR, int idU)
        {
            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Receita_Utensilio VALUES(@idReceita, @idUtensilio)";
                command.Parameters.Add("@idReceita", SqlDbType.Int).Value = idR;
                command.Parameters.Add("@idUtensilio", SqlDbType.Int).Value = idU;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
            }
            return updated;
        }

        public bool InsertRIP(int id_Rec, int passo, int ing, int quant, int ord)
        {
            bool updated = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Receita_Passo_Ingrediente VALUES(@idReceita, @idIngrediente, @idPasso, " +
                                      "@quantidade, @ordem)";
                command.Parameters.Add("@idReceita", SqlDbType.Int).Value = id_Rec;
                command.Parameters.Add("@idIngrediente", SqlDbType.Int).Value = ing;
                command.Parameters.Add("@idPasso", SqlDbType.Int).Value = passo;
                command.Parameters.Add("@quantidade", SqlDbType.Int).Value = quant;
                command.Parameters.Add("@ordem", SqlDbType.Int).Value = ord;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
            }
            return updated;
        }



    }
}