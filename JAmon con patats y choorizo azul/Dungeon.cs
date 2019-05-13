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
    public class Player
    {
        const int START_HEALTH = 10;
        const int START_DAMAGE = 2;

        int pos;
        int health, damage;

        public Player() //constructora de la clase, que sitúa al jugador en la posición 0 y con valores determinados de vida y daño (START_HEALTH y START_DAMAGE)
        {
            pos = 0;

            health = START_HEALTH;
            damage = START_DAMAGE;
        }

        public int GetPosition() //devuelve posicion actual del jugador
        {
            return pos;
        }

        public bool IsAlive() //true si el jugador sigue teniendo vida, false si no
        {
            return health > 0;
        }

        public string PrintStats() //devuelve un string con la vida y el daño del jugador
        {
            return "Player: HP " + health + " ATK " + damage;
        }

        public int GetATK() //devuelve el valor de ataque
        {
            return damage;
        }

        public bool ReceiveDamage(int damage) //resta el daño recibido al jugador y devuelve si este sigue vivo
        {
            health -= damage;

            return health > 0;  //NO SE PODRIA UTILIZAR ISALIVE()???????????????????????????????????????????????????
        } 

        public void Move(Map m, Direction dir) //mueve el jugador en una dir de acuerdo con el MAP m
        {
            if (m.Move(pos, dir) != -1) pos = m.Move(pos, dir);
        }

        public bool atExit(Map map) //indica si jugador está en una salida del mapa.
        {
            return map.IsExit(pos);
        }

        public void WritePlayerInfo(StreamWriter s)
        {
            s.Write(pos + " ");
            s.WriteLine(health);
        }

        public void SetPlayerStats(int newPos, int newHealth)
        {
            pos = newPos;
            health = newHealth;
        }
    }
}
