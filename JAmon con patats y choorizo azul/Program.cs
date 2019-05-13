//Pablo Villapun Martin
//Daniel González Sellán

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class Program
    {
        static void Main()
        {
            Map map = new Map(10, 4); //crea el mapa
            Player player = new Player(); //crea el player
            bool YN = false;
            string auto;

            Console.WriteLine("Cargar partida anterior? Y/N");
            auto = Console.ReadLine();
            if (auto == "y" || auto == "Y")
            {
                CargarPartida(map, player);
            }

            do
            {
                Console.WriteLine("¿Deseas utilizar un archivo automatico? Y/N");
                auto = Console.ReadLine();
                if (auto == "Y" || auto == "y")
                {
                    YN = true;
                    Automatizado(map, player);
                }
                else if (auto == "N" || auto == "n")
                {
                    YN = true;
                    string input = "";
                    while (player.IsAlive() && input != "quit")
                    {
                        Console.Write("(go, info, stats, enemies, attack)\n >"); //en cada iteracion se dan las opciones al jugador y se muestra el prompt ">"
                        input = Console.ReadLine(); //se lee el comando introducido
                        Console.Clear();
                        ProcesaInput(input, player, map); //se procesa el input
                        //DEBUG(map, player);
                    }

                    if (!player.IsAlive()) Console.WriteLine("You are dead");
                    else if (input == "quit") GuardarPartida(map, player);
                }
            } while (!YN);
        }

        static void ProcesaInput(string com, Player p, Map m)  //procesa el comando representado en la string com
        {
            string[] div;
            div = com.Split(' ');
            try
            {
                switch (div[0])
                {
                    case "go":
                        if (div.Length > 1 && (div[1] == "north" || div[1] == "south" || div[1] == "west" || div[1] == "east"))
                        {
                            p.Move(m, StringToDir(div[1])); //Solo si el jugador introduce una direccion
                            EnemiesAttackPlayer(m, p);
                        }
                        else Console.WriteLine("Direccion cardinal erronea");
                        break;

                    case "enemies":
                        Console.WriteLine(m.GetenemiesInfo(p.GetPosition()));
                        break;

                    case "attack":
                        Console.WriteLine("Enemies eliminated: " + PlayerAttackEnemies(m, p));
                        EnemiesAttackPlayer(m, p);
                        break;

                    case "info":
                        Console.WriteLine(m.GetDungeonInfo(p.GetPosition()));
                        break;

                    case "stats":
                        Console.WriteLine(p.PrintStats());
                        break;

                    
                }
            }
            catch (Exception)
            {

                Console.WriteLine("No has introducido un input correcto");
            }
           
        }

        static Direction StringToDir(string s) //pasa la string de direccion a un enum con su direccion adecuada
        {
            
            switch (s)
            {
                case "north":
                    return Direction.North;

                case "south":
                    return Direction.South;

                case "east":
                    return Direction.East;
                    
                case "west":
                    return Direction.West;
                default:
                    return Direction.North; //nunca va a darse este caso
            }
        }

        static bool EnemiesAttackPlayer(Map m, Player p) //devuelve true si hay enemigos donde se encuentra el jugador (lo atacan) y false si no 
        {
            if (m.GetNumEnemies(p.GetPosition()) > 0)
            {
                p.ReceiveDamage(m.ComputeDungeonDamage(p.GetPosition()));
                return true;
            }
            else return false;
        } 

        static int PlayerAttackEnemies(Map m, Player p) //el jugador ataca a los enemigos que se encuentran en el lugar, devuelve los enemigos que han sido eliminados
        {
            return m.AttackEnemiesInDungeon(p.GetPosition(), p.GetATK());
        }

        static void DEBUG(Map m, Player p)
        {
            Console.WriteLine("Dungeon: " + p.GetPosition());
            Console.WriteLine();
            Console.WriteLine(p.PrintStats());
            Console.WriteLine();
            Console.WriteLine("Enemies: \n" + m.GetenemiesInfo(p.GetPosition()));
            Console.WriteLine();
            Console.WriteLine("Dungeon info: " + m.GetDungeonInfo(p.GetPosition()));
        }

        static void Automatizado(Map map, Player player)
        {
            StreamReader auto = new StreamReader("auto.txt");
            string autoinput = auto.ReadLine();       
            while(autoinput != null) //mientras haya un input lo intentará procesar
            {
                ProcesaInput(autoinput, player, map);
                autoinput = auto.ReadLine();
            }
            Console.WriteLine("Final del proceso");
            Console.ReadLine();
        }

        static void GuardarPartida(Map m, Player p)
        {
            StreamWriter salida = new StreamWriter("PartidaGuardada");

            m.WriteEnemies(salida);
            salida.WriteLine();
            p.WritePlayerInfo(salida);

            salida.Close();
        }

        static void CargarPartida(Map m, Player p)
        {
            try
            {
                StreamReader entrada = new StreamReader("PartidaGuardada");
                string linea = ".";
                string[] div;
                bool flag = true;

                while (flag)
                {
                    linea = entrada.ReadLine();
                    if (linea != "")
                    {
                        div = linea.Split(' ');
                        m.SetEnemyHP(int.Parse(div[1]), int.Parse(div[0]));
                    }
                    else flag = false;
                }

                linea = entrada.ReadLine();
                div = linea.Split(' ');
                p.SetPlayerStats(int.Parse(div[0]), int.Parse(div[1]));

                entrada.Close();

                
            }
            catch
            {
               Console.WriteLine("No se ha encontrado ningun archivo guardado");
            }
            
        }
    }
}
