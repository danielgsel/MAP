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
    public class Class1
    {

        [Test]
        public void IsAlive_true()
        {
            //Arrange
            Player p = new Player();

            //Act
            bool alive = p.IsAlive();

            //Assert
            Assert.That(alive, Is.True);
        
     

        }
    }
}
