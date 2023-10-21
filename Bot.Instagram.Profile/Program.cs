using System;

namespace Bot.Instagram.Profile
{
    class Program
    {
        static void Main(string[] args)
        {
            Profile perfil;
            Console.WriteLine("Digite o perfil que deseja pesquisar: ");

            if ((perfil = Instragram.GetProfileByUser(Console.ReadLine())) != null)
            {
                Console.WriteLine($"Nome = {perfil.UserName}");
                Console.WriteLine($"Descricao = {perfil.Description}");
                Console.WriteLine($"Link para o perfil = {perfil.Url}");
            }
            else
            {
                Console.WriteLine("Perfil não encontrado!\nTente novamente com perfil valido.");
            }
            

            Console.ReadKey();
        }
    }
}
