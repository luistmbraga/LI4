using FitChef.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class ReceitaDAO
    {
        private Connection _connection;

        public ReceitaDAO(Connection connection)
        {
            this._connection = connection;
        }

        public Receita FindById(int idR)
        {
            // Retornar a receita
            Receita obj = new Receita();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, nome, infNutricional FROM Receita WHERE id = @idR";

                command.Parameters.Add("@idR", SqlDbType.Int).Value = idR;

                command.Parameters.Add("@id", SqlDbType.Int).Value = obj.Id;
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@pass", SqlDbType.Int).Value = obj.InfNutricional;

                command.ExecuteNonQuery();
            }
            return obj;
        }

        public int Insert(Receita obj)
        {
            int id_receita = 0;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO Receita VALUES(@nome, @infNutricional)";

                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@infNutricional", SqlDbType.Int).Value = obj.InfNutricional;

                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Receita where id =(Select Max(id) from Receita)";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string receita_id = reader["id"].ToString();
                    id_receita = Int32.Parse(receita_id);
                }
            }
            return id_receita;
        }

        public Collection<Receita> FindAll()
        {
            Collection<Receita> receitas = new Collection<Receita>();


            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, nome, infNutricional FROM Receita";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Receita rec = new Receita
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            InfNutricional = (int)row["infNutricional"]
                        };
                        receitas.Add(rec);
                    }
                }
                return receitas;
            }
        }

        public Collection<Passo> FindPassosFromReceita(int id)
        {
            Collection<Passo> resultado = new Collection<Passo>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT DISTINCT p.id, p.descricao " + "FROM Receita_Passo_Ingrediente r, Passo p "
                                        + "WHERE r.Receita_id = @id and p.id = r.Passo_id ";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        Passo p = new Passo
                        {
                            Id = (int)row["id"],
                            Descricao = row["descricao"].ToString()
                        };
                        resultado.Add(p);
                    }
                }
            }

            return resultado;
        }

        public Collection<Ingrediente> FindIngredientesFromReceita(int id)
        {
            Dictionary<int, Ingrediente> resultado = new Dictionary<int, Ingrediente>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT i.id, i.nome, i.unidade, r.quantidade " + "FROM Receita_Passo_Ingrediente r, Ingrediente i " +
                                           "WHERE @id = r.Receita_id and r.Ingrediente_id = i.id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        Ingrediente ing = new Ingrediente
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString(),
                            Unidade = row["unidade"].ToString(),
                            Quantidade = (int)row["quantidade"]
                        };

                        if(!resultado.ContainsKey(ing.Id)) resultado.Add(ing.Id, ing);
                        else
                        {
                            Ingrediente aux = resultado[ing.Id];
                            ing.Quantidade += aux.Quantidade;
                            resultado.Remove(ing.Id);
                            resultado.Add(ing.Id, ing);
                        }
                    }
                }

            }
            Collection<Ingrediente> r = new Collection<Ingrediente>();

            foreach(Ingrediente i in resultado.Values)
            {
                r.Add(i);
            }

            return r;
        }

        public Collection<Utensilio> FindUtensiliosFromReceita(int id)
        {
            Collection<Utensilio> resultado = new Collection<Utensilio>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select u.id, u.nome " + "From receita_utensilio r, Utensilio u "
                                            + "Where u.id = r.Utensilio_id and r.Receita_id = @id;";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        Utensilio u = new Utensilio
                        {
                            Id = (int)row["id"],
                            Nome = row["nome"].ToString()
                        };

                        resultado.Add(u);
                    }
                }
            }

            return resultado;
        }

        /**
         * VerificaReceita(String Receita)
         * 
         * Verifica de uma dada receita existe
         */
        public bool VerificaReceita(String receita)
        {
            Boolean existe = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id FROM receita WHERE nome = @receita";

                command.Parameters.Add("@receita", SqlDbType.VarChar).Value = receita;

                if (command.ExecuteReader().HasRows)
                {
                    existe = true;
                }
            }
            return existe;
        }


        /**
         * Update
         * 
         * Faz update de uma receita.
         */
        public bool Update(Receita obj)
        {
            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Receita Set nome=@nome, infNutricional=@infNutricional WHERE id=@ID";

                //... SqlDbType.
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = obj.Id;
                command.Parameters.Add("@infNutricional", SqlDbType.Int).Value = obj.InfNutricional;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }
    
        public void InsertReceitaRealizada(int idUtilizador, int idReceita, long time)
        {
            using(SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select tempo_medio, quantas, id From Cliente_Receita where Utilizador_id = @idU and Receita_id = @idR";

                command.Parameters.Add("@idU", SqlDbType.Int).Value = idUtilizador;
                command.Parameters.Add("@idR", SqlDbType.Int).Value = idReceita;

                int idRR = -1;
                string tempo_medio = "";
                int quantas = 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        tempo_medio = row["tempo_medio"].ToString();
                        quantas = (int)row["quantas"];
                        idRR = (int)row["id"];
                    }
                }

                quantas++;
                if(idRR == -1)
                {
                    long horas = time / (60 * 60 * 1000);
                    long min = (time - horas * 60 * 60 * 1000) / (60 * 1000);
                    long sec = (time - horas * 60 * 60 * 1000 - min * 60 * 1000) / 1000;
                    string h;
                    if (horas < 10)
                    {
                        h = "0" + horas;
                    }
                    else { h = horas.ToString(); }
                    if (min < 10)
                    {
                        h += ":" + "0" + min;
                    }
                    else { h += ":" + min; }
                    if (sec < 10)
                    {
                        h += ":" + "0" + sec;
                    }
                    else
                    {
                        h += ":" + sec;
                    }
                    using(SqlCommand command2 = _connection.Fetch().CreateCommand())
                    {
                        command2.CommandType = CommandType.Text;
                        command2.CommandText = "Insert Into Cliente_Receita (Utilizador_id, Receita_id, tempo_medio, quantas) " +
                                                "VALUES(@idU, @idR, @temp, @quantas)";

                        command2.Parameters.Add("@idU", SqlDbType.Int).Value = idUtilizador;
                        command2.Parameters.Add("@idR", SqlDbType.Int).Value = idReceita;
                        command2.Parameters.Add("@temp", SqlDbType.VarChar).Value = h;
                        command2.Parameters.Add("@quantas", SqlDbType.Int).Value = quantas;

                        command2.ExecuteNonQuery();

                    }
                }
                else
                {
                    string[] t = tempo_medio.Split(':');
                    time += (int.Parse(t[0]) * 60 * 60 * 1000 + int.Parse(t[1]) * 60 * 1000 + int.Parse(t[2]) * 1000)*(quantas-1);
                    time /= quantas;
                    long horas = time / (60 * 60 * 1000);
                    long min = (time - horas * 60 * 60 * 1000) / (60 * 1000);
                    long sec = (time - horas * 60 * 60 * 1000 - min * 60 * 1000) / 1000;
                    string h;
                    if(horas < 10)
                    {
                        h = "0" + horas;
                    }
                    else { h = horas.ToString(); }
                    if(min < 10)
                    {
                        h += ":" + "0" + min;
                    }
                    else { h += ":" + min; }
                    if(sec < 10)
                    {
                        h += ":" + "0" + sec;
                    }
                    else
                    {
                        h += ":" + sec;
                    }

                    using (SqlCommand command2 = _connection.Fetch().CreateCommand())
                    {
                        command2.CommandType = CommandType.Text;
                        command2.CommandText = "UPDATE Cliente_Receita Set tempo_medio=@temp, quantas=@quant WHERE id=@ID;";

                        command2.Parameters.Add("@temp", SqlDbType.VarChar).Value = h;
                        command2.Parameters.Add("@quant", SqlDbType.Int).Value = quantas;
                        command2.Parameters.Add("@ID", SqlDbType.Int).Value = idRR;

                        command2.ExecuteNonQuery();

                    }
                }

            }
        }

        public void InsertDificuldades(int idUtilizador, int idReceita, Collection<string> dificuldades)
        {
            int realizar_id = 0;
            using (SqlCommand commandAux = _connection.Fetch().CreateCommand())
            {
                commandAux.CommandType = CommandType.Text;
                commandAux.CommandText = "Select id From Cliente_Receita where Utilizador_id = @idU and Receita_id = @idR;";

                commandAux.Parameters.Add("@idU", SqlDbType.Int).Value = idUtilizador;
                commandAux.Parameters.Add("@idR", SqlDbType.Int).Value = idReceita;

                using(SqlDataAdapter adapter = new SqlDataAdapter(commandAux))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach(DataRow row in table.Rows)
                    {
                        realizar_id = (int)row["id"];
                    }
                }
            }
                foreach (string d in dificuldades)
                {
                    using (SqlCommand command = _connection.Fetch().CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "Insert Into Dificuldade (comentario, Realizar_id) Values(@comentario, @realizar);";

                        command.Parameters.Add("@comentario", SqlDbType.VarChar).Value = d;
                        command.Parameters.Add("@realizar", SqlDbType.Int).Value = realizar_id;

                        command.ExecuteNonQuery();
                    }
                }
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