//Pablo Villapun Martin
//Daniel González Sellán

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dungeon
{
    //posibles direcciones
    public enum Direction { North, South, East, West};

    public class Map
    {
        //Info enemigos
        struct Enemy
        {
            public string name;
            public int attHP, attATK, dungeonPos;
        }

        //Lugares del mapa
        struct Dungeon
        {
            public string name, description;
            public bool exit;
            public int[] doors;

            public Lista enemiesInDungeon;
        }

        Dungeon[] dungeons;
        Enemy[] enemies;
        int nDungeons, nEnemies;

        public Map(int numDungeons, int numEnem)
        {
            /*
            nDungeons = numDungeons;
            nEnemies = numEnem;

            dungeons = new Dungeon[numDungeons];
            enemies = new Enemy[numEnem];
            
            for (int i = 0; i < dungeons.Length; i++)   //Bucle que recorre cada posicion del vector dungeons
            {
                dungeons[i].doors = new int[4];
                for (int e = 0; e < 4; e++)     //Bucle para poner el valor de los elementos de puertas a -1 porque no hay conexion (mapa vacio)
                {
                    dungeons[i].doors[e] = -1;
                }

                dungeons[i].enemiesInDungeon = new Lista(); //Se invoca a la constructora de Listas para inicializarla en cada elemento
            }

            ReadMap("hauntedHouse.map");
            */



            //////
            ///
            dungeons = new Dungeon[4];
            enemies = new Enemy[6];

            for (int i = 0; i < dungeons.Length; i++)   //Bucle que recorre cada posicion del vector dungeons
            {
                dungeons[i].doors = new int[4];
                for (int e = 0; e < 4; e++)     //Bucle para poner el valor de los elementos de puertas a -1 porque no hay conexion (mapa vacio)
                {
                    dungeons[i].doors[e] = -1;
                }

                dungeons[i].enemiesInDungeon = new Lista(); //Se invoca a la constructora de Listas para inicializarla en cada elemento
            }

            //ReadMap("hauntedHouse.map");

            for (int i = 0; i < nDungeons; i++)
            {
                bool isExit;
                if (i == 3) isExit = true;
                else isExit = false;

                CreateDungeon(i, "nombre", isExit, "descripcion");
            }

            CreateDungeon(0, "ABIZ", false, "ABIZ");
            CreateDungeon(1, "ARIZ", false, "ARIZ");
            CreateDungeon(2, "ARDC", false, "ARDC");
            CreateDungeon(3, "ABDC", false, "ABDC");


            CreateDoor(0, 0, 1);
            CreateDoor(1, 2, 2);
            CreateDoor(2, 1, 3);
            CreateDoor(3, 3, 0);


            int c = 0;
            for (int i = 0; i < 2; i++)
            {
                CreateEnemy(c, "enemigohab1" + i, 2, 2, 0);
                c++;
            }
            for (int i = 0; i < 0; i++)
            {
                CreateEnemy(c, "enemigohab2" + i, 2, 2, 1);
                c++;
            }
            for (int i = 0; i < 0; i++)
            {
                CreateEnemy(c, "enemigohab3" + i, 2, 2, 2);
                c++;
            }
            for (int i = 0; i < 4; i++)
            {
                CreateEnemy(c, "enemigohab4" + i, 2, 3, 3);
                c++;
            }
        }


        public void ReadMap(string file) //Lee el archivo y dependiendo de lo leido llama a metodos distintos a los que les da la informacion necesaria
        {
            string linea;
            string[] div;
            StreamReader entrada = new StreamReader(file);

            while (!entrada.EndOfStream)
            {
                linea = entrada.ReadLine();
                div = linea.Split(' ');

                switch (div[0])
                {
                    case "dungeon":

                        bool isExit;
                        if (div[3] == "exit") isExit = true;
                        else isExit = false;

                        CreateDungeon(int.Parse(div[1]), div[2], isExit, ReadDescription(entrada));
                        break;

                    case "door":
                        if(div[4] == "north" || div[4] == "east" || div[4] == "west" || div[4] == "south" )CreateDoor(int.Parse(div[3]), CoordToInt(div[4]), int.Parse(div[6]));
                        break;

                    case "enemy":

                        CreateEnemy(int.Parse(div[1]), div[2], int.Parse(div[3]), int.Parse(div[4]), int.Parse(div[6]));
                        break;
                }
            }

            entrada.Close();
        }

        private string ReadDescription(StreamReader f) 
        {
            string read, desc = "";
            bool flag = false;
            
            while (!flag)
            {
                read = f.ReadLine();
                if (read[read.Length - 1] == '\"')
                {
                    flag = true;
                    desc += (read + "\n");
                }
                else desc += (read + "\n");
            }

            return desc;
        }

        private int CoordToInt(string st)
        {
            int coord;

            switch (st)
            {
                case "north":
                    coord = 0;
                    break;

                case "south":
                    coord = 1;
                    break;

                case "east":
                    coord = 2;
                    break;

                case "west":
                    coord = 3;
                    break;

                default:
                    coord = 0; //Nunca pasaria
                    break;
            }

            return coord;
        }

        private void CreateDungeon(int pos, string newName, bool newExit, string newDesc)
        {
            dungeons[pos].name = newName;
            dungeons[pos].exit = newExit;
            dungeons[pos].description = newDesc;
        }

        private void CreateDoor(int numDungeon, int dir, int nextDungeon)
        {
            dungeons[numDungeon].doors[dir] = nextDungeon;
            
            switch (dir)
            {
                case 0:
                    dungeons[nextDungeon].doors[1] = numDungeon;
                    break;
                case 1:
                    dungeons[nextDungeon].doors[0] = numDungeon;
                    break;
                case 2:
                    dungeons[nextDungeon].doors[3] = numDungeon;
                    break;
                case 3:
                    dungeons[nextDungeon].doors[2] = numDungeon;
                    break;
            }
        }

        private void CreateEnemy(int n, string newName, int newHP, int newATK, int newPos) //crea un nuevo enemigo con los atributos recibidos y lo inserta al final de la lista de enemigos
        {
            enemies[n].name = newName;
            enemies[n].attHP = newHP;
            enemies[n].attATK = newATK;
            enemies[n].dungeonPos = newPos;

            dungeons[newPos].enemiesInDungeon.insertaFin(n);
        }


        public string GetDungeonInfo(int dung) //devuelve la descripcion de dung
        {
            return dungeons[dung].description;
        }


        public string GetMoves(int dung)
        {
            string moves = "";
            string[] coords = { "north", "south", "east", "west" };

            for (int i = 0; i < 4; i++)
            {
                if (dungeons[dung].doors[i] != -1)
                {
                    moves += coords[i] + ": " + dungeons[dungeons[dung].doors[i]].name + "\n"; // Si hay puertas en esa direccion lo muestra de la forma "north: 1" y hace un salto de linea
                } 
            }

            return moves;
        }


        public int GetNumEnemies(int dung)
        {
            return dungeons[dung].enemiesInDungeon.nElems;
        }


        public string GetEnemyInfo(int en) //devuelve la información sobre el enemigo en de la lista de enemigos. Para el enemigo X devuelve: X: EnemyX "Nombre" HP Y ATK Z
        {
            return en + ": enemy" + en + " \"" + enemies[en].name + "\" " + "HP " + enemies[en].attHP + " ATK " + enemies[en].attATK;
        }


        public string GetenemiesInfo(int dung) // devuelve la información sobre todos los enemigos que hay en el lugar dung llamando al metodo GetEnemyInfo por cada uno
        {
            string enemiesInfo = "";
            int numEn = dungeons[dung].enemiesInDungeon.cuentaEltos();
            int i = 0;

            while (i < numEn && enemies[dungeons[dung].enemiesInDungeon.nEsimo(i)].attHP > 0)
            {
                enemiesInfo += GetEnemyInfo(dungeons[dung].enemiesInDungeon.nEsimo(i)) + "\n";
                i++;
            }

            return enemiesInfo;
        }


        public int GetEnemyATK(int en)
        {
            return enemies[en].attATK;
        }


        public bool MakeDamageEnemy(int en, int damage) //resta "damage" al HP del enemigo,si existe. Si muere el enemigo devuelve true, false si sigue vivo. 
        {
            if (enemies[en].attHP > 0)
            {
                enemies[en].attHP -= damage;
                if (enemies[en].attHP > 0) return false;
                else return true;
            }
            else return false;
        }


        public int AttackEnemiesInDungeon(int dung, int damage) //los enemigos muertos son eliminados de la lista. Devuelve el número de enemigos que han sido eliminados.
        {
            int muertesEnem = 0;
            int numEnem = GetNumEnemies(dung) - 1; 

            for (;numEnem >= 0; numEnem--)
            {
                if (MakeDamageEnemy(dungeons[dung].enemiesInDungeon.nEsimo(numEnem), damage)) muertesEnem++; //utiliza el metodo MakeDamageEnemy en cada enemigo
            }

            return muertesEnem;
        }


        public int ComputeDungeonDamage(int dung) //devuelve la suma del ataque de todos los enemigos
        {
            int totalDamage = 0;
            int numEnem = dungeons[dung].enemiesInDungeon.cuentaEltos() - 1;

            for (; numEnem >= 0; numEnem--)
            {
                if (enemies[dungeons[dung].enemiesInDungeon.nEsimo(numEnem)].attHP > 0)
                {
                    totalDamage += enemies[dungeons[dung].enemiesInDungeon.nEsimo(numEnem)].attATK;
                }
            }

            return totalDamage;
        }


        public int Move(int pl, Direction dir) //devuelve el lugar al que se llega desde el lugar pl avanzando en la dirección dir 
        {
            switch (dir)
            {
                case Direction.North:
                    return dungeons[pl].doors[0];

                case Direction.South:
                    return dungeons[pl].doors[1];

                case Direction.East:
                    return dungeons[pl].doors[2];

                case Direction.West:
                    return dungeons[pl].doors[3];
                default:
                    return -1;
            }
        }


        public bool IsExit(int dung) //comprueba si el lugar dung es salida del mapa
        {
            return dungeons[dung].exit;
        }

        public void SetEnemyHP(int newHP, int enemy)
        {
            enemies[enemy].attHP = newHP;
        }

        public void WriteEnemies(StreamWriter s)
        {
            for (int i = 0; i < nEnemies; i++)
            {
                s.Write(i + " ");
                s.WriteLine(enemies[i].attHP);
            }
        }

        #region Tests Map

        public Map(int numEnem1, int numEnem2, int numEnem3, int numEnem4)
        {
           

            dungeons = new Dungeon[4];
            enemies = new Enemy[numEnem1 + numEnem2 + numEnem3 + numEnem4];

            for (int i = 0; i < dungeons.Length; i++)   //Bucle que recorre cada posicion del vector dungeons
            {
                dungeons[i].doors = new int[4];
                for (int e = 0; e < 4; e++)     //Bucle para poner el valor de los elementos de puertas a -1 porque no hay conexion (mapa vacio)
                {
                    dungeons[i].doors[e] = -1;
                }

                dungeons[i].enemiesInDungeon = new Lista(); //Se invoca a la constructora de Listas para inicializarla en cada elemento
            }

            //ReadMap("hauntedHouse.map");
           
            for (int i = 0; i < nDungeons; i++)
            {
                bool isExit;
                if (i == 3) isExit = true;
                else isExit = false;

                CreateDungeon(i, "nombre" , isExit, "descripcion");
            }


            CreateDoor(0, 0, 1);
            CreateDoor(1, 2, 2);
            CreateDoor(2, 1, 3);
            CreateDoor(3, 3, 0);


            int c = 0;
            for (int i = 0; i < numEnem1; i++)
            {
                CreateEnemy(c, "enemigohab1", 2, 2, 0);
                c++;
            }
            for (int i = 0; i < numEnem2; i++)
            {
                CreateEnemy(c, "enemigohab2", 2, 2, 1);
                c++;
            }
            for (int i = 0; i < numEnem3; i++)
            {
                CreateEnemy(c, "enemigohab3", 2, 2, 2);
                c++;
            }
            for (int i = 0; i < numEnem4; i++)
            {
                CreateEnemy(c, "enemigohab4", 2, 2,3);
                c++;
            }

        }
    }
        


        #endregion
    
}
