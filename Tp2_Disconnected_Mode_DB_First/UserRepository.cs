using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_Disconnected_Mode_DB_First
{
    class UserRepository
    {
        private string ConnectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable userTable;

        public UserRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Tp2User"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            //Create a SqlDataAdapter for the Suppliers table.
            adapter = new SqlDataAdapter("SELECT * FROM Users;", connection);


                // Create a SqlCommand to retrieve Suppliers data.
                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Users;",
                    connection);


                // Insert command config
                adapter.InsertCommand = new SqlCommand("INSERT INTO Users (id, nom, prenom, age) VALUES (@id, @nom, @prenom, @age);", connection);
                adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 250, "id");
                adapter.InsertCommand.Parameters.Add("@nom", SqlDbType.VarChar, 50, "nom");
                adapter.InsertCommand.Parameters.Add("@prenom", SqlDbType.VarChar, 10, "Prenom");
                adapter.InsertCommand.Parameters.Add("@age", SqlDbType.Int, 250, "age");

                // Update command config
                adapter.UpdateCommand = new SqlCommand("UPDATE Users SET nom = @nom, prenom = @prenom, age = @age WHERE ID = @id;", connection);
                adapter.UpdateCommand.Parameters.Add("@nom", SqlDbType.VarChar, 50, "nom");
                adapter.UpdateCommand.Parameters.Add("@prenom", SqlDbType.VarChar, 50, "prenom");
                adapter.UpdateCommand.Parameters.Add("@age", SqlDbType.Int, 250, "age");
                var param = adapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int);
                param.SourceColumn = "ID";
                param.SourceVersion = DataRowVersion.Original;

                // Delete command config
                adapter.DeleteCommand = new SqlCommand("DELETE FROM PRODUCTS WHERE ID = @id;", connection);
                adapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 11, "ID");

                // datatable configuration
                userTable = new DataTable();
                DataColumn idCol = userTable.Columns["ID"];
                userTable.PrimaryKey = new[] { idCol };
                adapter.Fill(userTable);
            }

        public List<User> FindAllUsers()
        {
            List<User> users = new List<User>();

            //reading all users
            foreach (DataRow userRow in userTable.Rows)
            {
                User user = new User();
                user.Id = Int32.Parse(userRow["ID"].ToString());
                user.Nom = userRow["NOM"].ToString();
                user.Prenom = userRow["PRENOM"].ToString();
                user.Age = Int32.Parse(userRow["AGE"].ToString());
                users.Add(user);
            }
            return users;
        }

        public User FindUserById(int id)
        {
            User user = null;
            if (userTable.Select("ID = " + id).Length != 0)
            {
                //finding the row
                DataRow userRow = userTable.Select("ID = " + id)[0];

                //configuring user
                user = new User();
                user.Id = Int32.Parse(userRow["ID"].ToString());
                user.Nom = userRow["NOM"].ToString();
                user.Prenom = userRow["PRENOM"].ToString();
                user.Age = Int32.Parse(userRow["AGE"].ToString());
            }
            return user;
        }

        public void AddUser()
        {
            DataRow userRow = userTable.NewRow();
            Console.WriteLine("Entrer l'id");
            int Id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter le nom de l'utilisateur");
            string Nom = Console.ReadLine();
            Console.WriteLine("Enter le prénom de l'utilisateur");
            string Prenom = Console.ReadLine();
            Console.WriteLine("Enter l'age de l'utilisateur");
            int Age = Int32.Parse(Console.ReadLine());
            userRow["ID"] = Id;
            userRow["NOM"] = Nom;
            userRow["PRENOM"] = Prenom;
            userRow["AGE"] = Age;
            userTable.Rows.Add(userRow);
        }

        public void UpdateUser(User user)
        {
            if (userTable.Select("ID = " + user.Id).Length != 0)
            {
                DataRow userRow = userTable.Select("ID = " + user.Id)[0];
                userRow["NOM"] = user.Nom;
                userRow["PRENOM"] = user.Prenom;
                userRow["AGE"] = user.Age;
            }
        }

        public void DeleteUser(int id)
        {
            if (userTable.Select("ID = " + id).Length != 0)
            {
                DataRow userRow = userTable.Select("ID = " + id)[0];
                userRow.Delete();
            }
        }

        public void CommitChanges()
        {
            adapter.Update(userTable);
        }
    }
    }