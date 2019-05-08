using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lista;
using NUnit.Framework;

namespace TestLista
{
    [TestFixture]
    public class TestLista
    {
        [Test]

        public void CuentaEltos()
        {
            Lista.Lista l = new Lista.Lista(3, 2);

            Assert.AreEqual(6, l.cuentaEltos());
        }

        /*[Test]

        public void InsertaFin()
        {

        }

        [Test]

        public void BorraElto()
        {

        }

        [Test]

        public void NEsimo()
        {

        }*/
    }
}
