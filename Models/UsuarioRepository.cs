using System;
using MySqlConnector;
using Destino_Certo.Models;

namespace Destino_Certo.Models
{
    public class UsuarioRepository
    {
        private const string baseDados = "Database=Destino_Certo;Data Source=localhost;User Id=root;";

        public void Insert(Usuario novoUsuario)
        {
            //Abrindo Conexão com o banco de dados
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

            //Criando Scrip Comando Sql
            string sql = "INSERT INTO Usuario(Nome, Login, Senha, Data_de_Nascimento) VALUES (@Nome, @Login, @Senha, @DataNascimento)";

            //tratando comando sql
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            //tratando injection 
            comando.Parameters.AddWithValue("@Nome", novoUsuario.Nome);
            comando.Parameters.AddWithValue("@Login", novoUsuario.Login);
            comando.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
            comando.Parameters.AddWithValue("@DataNascimento", novoUsuario.DataNascimento);

            //execultando coimando sql
            comando.ExecuteNonQuery();

            //fechando conexão
            conexao.Close();

        }

        public Usuario QueryLogin(Usuario u)
        {
            //abrindo conexão
            MySqlConnection conexao = new MySqlConnection(baseDados);
            conexao.Open();

             //Criando Scrip Comando Sql
            string sql = "SELECT * FROM Usuario WHERE login = @Login AND senha = @Senha";

            //tratando comando sql
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);

            //tratando injection 
            comandoQuery.Parameters.AddWithValue("@Login", u.Login);
            comandoQuery.Parameters.AddWithValue("@Senha", u.Senha);

            MySqlDataReader reader = comandoQuery.ExecuteReader();
            Usuario urs = null;

            if(reader.Read())
            {
                urs = new Usuario();
                urs.Id = reader.GetInt32("IdUsuario");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                urs.Nome = reader.GetString("Nome");

                if(!reader.IsDBNull(reader.GetOrdinal("Login")))
                urs.Login = reader.GetString("Login");

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                urs.Senha = reader.GetString("Senha");
            }

            //fexando conexão
            conexao.Close();

            //retornando para objeto Usuario
            return urs;

        }
    }
}