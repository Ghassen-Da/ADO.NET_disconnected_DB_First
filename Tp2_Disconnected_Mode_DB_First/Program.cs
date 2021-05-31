using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_Disconnected_Mode_DB_First
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            while (true)
            {
/**/                Console.WriteLine("Merci de choisir:");
                Console.WriteLine("1- Pour ajouter un utilisateur");
                Console.WriteLine("2- Pour afficher tous les utilisateurs");
                Console.WriteLine("3- Pour afficher un utilisateur by id");
                Console.WriteLine("4- Pour supprimer un utilisateur");
                Console.WriteLine("5- Pour mettre à jour un utilisateur");
                Console.WriteLine("Q- Pour Quitter");
                String choix = Console.ReadLine();

                if (choix == "1")
                {
                    userRepository.AddUser();
                }
                if (choix == "2")
                {
                    Console.WriteLine("The Users are");
                    foreach (User user in userRepository.FindAllUsers())
                    {
                        Console.WriteLine(user.Id + "   " + user.Nom + "   " + user.Prenom + "   " + user.Age);
                        
                    }
                }

                if (choix == "3")
                {
                    Console.WriteLine("Entrer l'id");
                    int Id = Int32.Parse(Console.ReadLine());
                    userRepository.FindUserById(Id);
                }

                if (choix == "4")
                {
                    Console.WriteLine("Entrer l'id");
                    int Id = Int32.Parse(Console.ReadLine());
                    userRepository.DeleteUser(Id);
                }

                else if (choix == "Q")
                {
                    break;
                }

                /*userRepository.CommitChanges();*/
            }
        }
    }
}
