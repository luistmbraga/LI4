using FitChef.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class UtilizadorDAO : IDAO<Utilizador>
    {

        private Connection _connection;

        public UtilizadorDAO(Connection connection)
        {
            _connection = connection;
        }

        public Utilizador FindById(int idU)
        {
            // Retornar o utilizador
            Utilizador obj = new Utilizador();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, username, pass, nome, email, tipo FROM Utilizador WHERE id = @idU";

                command.Parameters.Add("@idU", SqlDbType.Int).Value = idU;
             

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        obj = new Utilizador
                        {
                            Id = (int) row["id"],
                            Username = row["username"].ToString(),
                            Password = row["pass"].ToString(),
                            Nome = row["nome"].ToString(),
                            Email = row["email"].ToString(),
                            Tipo = Convert.ToBoolean(row["tipo"])
                        };
                    }
                }
            }

            if(obj.Id == 0)
            {
                throw new Exception();
            }

            return obj;
        }

        // retorna boolean se o utilizador existe na BD
        public Boolean FindByUsername(string user)
        {

            Boolean existe = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, username, pass, nome, email, tipo FROM Utilizador WHERE username = @user";

                command.Parameters.Add("@user", SqlDbType.VarChar).Value = user;

                if (command.ExecuteReader().HasRows)
                {
                    existe = true;
                }
            }
            return existe;
        }
        

        public int FindIDByUsername(string user)
        {
            using(SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, username FROM Utilizador WHERE username = @user";

                command.Parameters.Add("@user", SqlDbType.VarChar).Value = user;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        return Convert.ToInt32(row["id"]);
                    }
                    throw new System.InvalidOperationException("O dado utilizador é inválido.");
               }
            }
        }

        // retorna o utilizador dando uma password
        public Utilizador FindByPassword(string passw)
        {
            // Retornar o utilizador
            Utilizador obj = new Utilizador();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, username, pass, nome, email, tipo FROM Utilizador WHERE pass = passw";

                command.Parameters.Add("@id", SqlDbType.Int).Value = obj.Id;
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = obj.Username;
                command.Parameters.Add("@pass", SqlDbType.VarChar).Value = obj.Password;
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                command.Parameters.Add("@tipo", SqlDbType.Bit).Value = obj.Tipo;

                command.ExecuteNonQuery();
            }
            return obj;
        }

        public bool Insert(Utilizador obj)
        {

            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO utilizador (username,pass,nome,email,tipo) " +
                                      " VALUES(@username, @pass, @nome, @email, @tipo)";

                command.Parameters.Add("@username", SqlDbType.VarChar).Value = obj.Username;
                command.Parameters.Add("@pass", SqlDbType.VarChar).Value = obj.Password;
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                command.Parameters.Add("@tipo", SqlDbType.Bit).Value = obj.Tipo;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }

        public bool Update(Utilizador obj)
        {
            bool updated = false;
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utilizador Set username=@username, pass=@pass, nome=@nome, email=@email, tipo=@tipo WHERE id=@ID";

                //... SqlDbType.
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = obj.Username;
                command.Parameters.Add("@pass", SqlDbType.VarChar).Value = obj.Password;
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                command.Parameters.Add("@tipo", SqlDbType.Bit).Value = obj.Tipo;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = obj.Id;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }

        public bool Remove(Utilizador obj)
        {
            bool updated = false;

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM utilizador WHERE id = @idu";

                command.Parameters.Add("@idu", SqlDbType.Int).Value = obj.Id;

                if (command.ExecuteNonQuery() > 0)
                {
                    updated = true;
                }
                return updated;
            }
        }


        public bool VerificaUser(string user)
        {      
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT username, pass FROM utilizador WHERE username = @user;";

                command.Parameters.Add("@user", SqlDbType.VarChar).Value = user;

                /*
                int result = (int)command.ExecuteScalar();
                return result > 0;
                */ 
                if (command.ExecuteReader().HasRows)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public string Login(string user, string password)
        {
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT username, pass, tipo FROM utilizador WHERE username = @user AND pass = @password;";

                command.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);
                    foreach (DataRow row in tab.Rows)
                    {
                        if (Convert.ToBoolean(row["tipo"])) return "Cliente";
                        else return "Nutricionista";
                    }
                    throw new System.InvalidOperationException("The password is incorrect! Please, try again.");
                }

            }
        }



        // lista todos os utilizadores do sistema;
        public Collection<Utilizador> FindAll()
        {
            Collection<Utilizador> utilizadores = new Collection<Utilizador>();
            
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT id, username, pass, nome, email, tipo FROM utilizador";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Utilizador ut = new Utilizador
                        {
                            Id = (int)row["id"],
                            Username = row["username"].ToString(),
                            Password = row["pass"].ToString(),
                            Nome = row["nome"].ToString(),
                            Email = row["email"].ToString(),
                            Tipo = (bool)row["tipo"]
                        };
                        utilizadores.Add(ut);
                    }
                }
                return utilizadores;
            }
        }


        public void AtualizaPreferencias(int idUtilizador, Collection<Ingrediente> gostos, Collection<Ingrediente> evitados)
        {

            using (SqlCommand commandRetirar = _connection.Fetch().CreateCommand())
            {
                commandRetirar.CommandType = System.Data.CommandType.Text;
                commandRetirar.CommandText = "Delete From Cliente_preferencia where Utilizador_id = @id";

                commandRetirar.Parameters.Add("@id", SqlDbType.Int).Value = idUtilizador;

                commandRetirar.ExecuteNonQuery();

            }


            foreach (Ingrediente i in gostos)
            {
                using (SqlCommand commandGostos = _connection.Fetch().CreateCommand())
                {
                    commandGostos.CommandType = System.Data.CommandType.Text;
                    commandGostos.CommandText = "INSERT INTO Cliente_preferencia (Utilizador_id, Ingrediente_id, gosto) " +
                        "                        VALUES(@Utilizador_id , @Ingrediente_id, @gosto)";

                    commandGostos.Parameters.Add("@Utilizador_id", SqlDbType.Int).Value = idUtilizador;
                    commandGostos.Parameters.Add("@Ingrediente_id", SqlDbType.Int).Value = i.Id;
                    commandGostos.Parameters.Add("@gosto", SqlDbType.Bit).Value = 1;

                    commandGostos.ExecuteNonQuery();
                }
            }

            foreach (Ingrediente i in evitados)
            {
                using (SqlCommand commandEvitados = _connection.Fetch().CreateCommand())
                {
                    commandEvitados.CommandType = System.Data.CommandType.Text;
                    commandEvitados.CommandText = "INSERT INTO Cliente_preferencia (Utilizador_id, Ingrediente_id, gosto) " +
                        "                         VALUES(@Utilizador_id , @Ingrediente_id, @gosto)";

                    commandEvitados.Parameters.Add("@Utilizador_id", SqlDbType.Int).Value = idUtilizador;
                    commandEvitados.Parameters.Add("@Ingrediente_id", SqlDbType.Int).Value = i.Id;
                    commandEvitados.Parameters.Add("@gosto", SqlDbType.Bit).Value = 0;

                    commandEvitados.ExecuteNonQuery();
                }
            }
        }

        /*
        public bool SetPreferencias(int idUtilizador, Collection<Ingrediente> gostos, Collection<Ingrediente> evitados)
        {
            bool updated = false;
            foreach (Ingrediente i in gostos)
            {
                using (MySqlCommand commandGostos = _connection.Fetch().CreateCommand())
                {
                    commandGostos.CommandType = System.Data.CommandType.Text;
                    commandGostos.CommandText = "INSERT INTO Cliente_preferencia VALUES(@Utilizador_id , @Ingrediente_id, @gosto)";

                    commandGostos.Parameters.Add("@Utilizador_id", MySqlDbType.Int32).Value = idUtilizador;
                    commandGostos.Parameters.Add("@Ingrediente_id", MySqlDbType.Int32).Value = i.Id;
                    commandGostos.Parameters.Add("@pass", MySqlDbType.Bit).Value = 1;
                    command.Parameters.Add("@nome", MySqlDbType.Text).Value = obj.Nome;
                    command.Parameters.Add("@email", MySqlDbType.Text).Value = obj.Email;
                    command.Parameters.Add("@tipo", MySqlDbType.Bit).Value = obj.Tipo;

                    command.ExecuteNonQuery();
                }
            }
        }

        /*
        public bool UserExists(string user, string passw)
        {
            Collection<Utilizador> uts = new Collection<Utilizador>();
            bool res = false;

            uts = findAll(); 

            foreach (Utilizador ut in uts)
            {
                if (ut.Username.Equals(user))
                {
                    if (ut.Password.Equals(passw))
                    {
                        res = true;
                    } else
                    {
                        res = false; 
                    }
                }
            }

            return res;

        }

    }
    */

        public Collection<Historico> GetHistorico(int idCliente)
        {
            Collection<Historico> historico = new Collection<Historico>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "Select cr.Receita_id, cr.tempo_medio, cr.quantas, r.nome From Cliente_Receita cr, Receita r " +
                    "where r.id = cr.Receita_id and cr.Utilizador_id = @id;";

                command.Parameters.Add("@id", SqlDbType.Int).Value = idCliente;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        String[] time = row["tempo_medio"].ToString().Split(':');
                        Historico h = new Historico
                        {
                            IdReceita = (int)row["Receita_id"],
                            NomeReceita = row["nome"].ToString(),
                            Quantas = (int)row["quantas"],
                            TempoMedio = new TimeSpan(Int32.Parse(time[0]), Int32.Parse(time[1]), Int32.Parse(time[2]))
                        };
                        historico.Add(h);
                    }
                }
                return historico;
            }
        }

        public Collection<string> GetDificuldadesFromReceita(int idUtilizador, int idReceita)
        {
            int realizar_id = 0;
            Collection<string> res = new Collection<string>();

            using (SqlCommand commandAux = _connection.Fetch().CreateCommand())
            {
                commandAux.CommandType = CommandType.Text;
                commandAux.CommandText = "Select id From Cliente_Receita where Utilizador_id = @idU and Receita_id = @idR;";

                commandAux.Parameters.Add("@idU", SqlDbType.Int).Value = idUtilizador;
                commandAux.Parameters.Add("@idR", SqlDbType.Int).Value = idReceita;

                using (SqlDataAdapter adapter = new SqlDataAdapter(commandAux))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        realizar_id = (int)row["id"];
                    }
                }
            }
           
            using(SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select comentario From Dificuldade where Realizar_id = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = realizar_id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        res.Add(row["comentario"].ToString());
                    }
                }
            }
            return res;
        }

    }
 }