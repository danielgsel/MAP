using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dungeon;


namespace Tests
{
    [TestFixture]
    public class TestsMap
    {
        Map m;
        [SetUp]
        public void HacerMapa()
        {
            m = new Map(2, 0, 0, 4);
        }

        [Test]
        
        public void GetNumEnemies_hay()
        {
            Assert.AreEqual(2, m.GetNumEnemies(0));        

        }

        [Test]
        public void GetNumEnemies_NoHay()
        {
            Assert.AreEqual(0, m.GetNumEnemies(1));
        }

        [Test]
        public void MakeDamageEnemy_NoMuere()
        {
            
            bool muerte = m.MakeDamageEnemy(0, 1);
            Assert.IsFalse(muerte);

        }
        [Test]
        public void MakeDamageEnemy_Muere()
        {

            bool muerte = m.MakeDamageEnemy(1, 2);
            Assert.IsTrue(muerte);

        }
        [Test]
        public void Attackenemies()
        {

            int elim = m.AttackEnemiesInDungeon(3, 8);

            Assert.AreEqual(4, elim, "Error de enemigos eliminados");

            Assert.AreEqual(0, m.GetNumEnemies(3), "No se han eliminado todos");
        }


    }
}
