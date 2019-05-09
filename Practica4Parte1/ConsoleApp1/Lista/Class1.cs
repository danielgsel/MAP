using System;

namespace Lista
{

    class ListaEnlazada
    {
        private class Nodo
        {
            public int dato;
            public Nodo sig;
        }

        Nodo pri;

        public ListaEnlazada()
        {
            pri = null;
        }

        public void insertaFinal(int e)
        {
            if (pri == null)
            { // lista vacia
                pri = new Nodo();
                pri.dato = e;
                pri.sig = null;
            }
            else
            { // lista no vacia
                Nodo aux = pri;
                while (aux.sig != null)
                {
                    aux = aux.sig;
                }
                aux.sig = new Nodo();
                aux = aux.sig;
                aux.dato = e;
                aux.sig = null;

            }
        }

        private Nodo buscaNodo(int e)
        {
            Nodo aux = pri;
            while (aux != null && aux.dato != e)
            {
                aux = aux.sig;
            }
            return aux;
        }

        public bool buscaDato(int e)
        {
            Nodo aux = buscaNodo(e);
            return (aux != null);
        }

        public void ver()
        {
            Console.Write("\nLista: ");
            Nodo aux = pri;
            while (aux != null)
            {
                Console.Write(aux.dato + " ");
                aux = aux.sig;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public void InsertaIni(int e)
        {
            Nodo aux = new Nodo();

            aux.sig = pri;
            aux.dato = e;
            pri = aux;
        }

        public int Suma()
        {
            Nodo aux = pri;
            int suma = 0;

            while (aux != null)
            {
                suma += aux.dato;
                aux = aux.sig;
            }

            return suma;
        }

        public int CuentaElementos()
        {
            Nodo aux = pri;
            int suma = 0;

            while (aux != null)
            {
                suma ++;
                aux = aux.sig;
            }

            return suma;
        }

        public int CuentaOcurrencias(int e)
        {
            Nodo aux = pri;
            int suma = 0;

            while (aux != null)
            {
                if (aux.dato == e) suma++;
                aux = aux.sig;
            }

            return suma;
        }

        private Nodo NesimoNodo(int n)
        {
            Nodo aux = pri;

            while (aux != null && n > 0)
            {
                aux = aux.sig;
                n--;
            }

            return aux;
        }

        public int Nesimo(int e)
        {
            Nodo obj = NesimoNodo(e);

            if (obj != null)
            {
                return obj.dato;
            }
            else throw new Exception("No se ha encontrado el dato en la lista");
        }

        public void InsertaNesimo(int n, int e)
        {
            if (n <= 1) InsertaIni(e);
            else
            {
                Nodo aux = pri;
                int i = 1;

                while (aux != null && i < n - 1)
                {
                    aux = aux.sig;
                    i++;
                }

                if (aux != null)
                {
                    Nodo nuevoNodo = new Nodo();
                    nuevoNodo.dato = e;
                    nuevoNodo.sig = aux.sig;
                    aux.sig = nuevoNodo;
                }
                else Console.WriteLine("No hay suficientes elementos en la lista");
            }
        }

        public bool BorraElto(int x)
        {
            if (pri == null) return false;
            else
            {
                bool result = false;
                if (x == pri.dato)
                {
                    result = true;
                    if (pri.sig == null)
                        pri = null;
                    else
                        pri = pri.sig;
                }
                else
                {
                    Nodo aux = pri;
                    while (aux.sig != null && x != aux.sig.dato)
                        aux = aux.sig;
                    if (aux.sig != null)
                    {
                        result = true;
                        aux.sig = aux.sig.sig;
                    }
                }
                return result;

            }
        }

        public void BorraTodos(int e)
        {
            while (BorraElto(e)) ;
        }

        public bool Iguales2(ListaEnlazada l)
        {
            bool iguales = true;

            if (this.CuentaElementos() == l.CuentaElementos())
            {
                Nodo aux = this.pri;
                int[] numerosComprobados = new int[this.CuentaElementos()];
                int contador = 0;

                while (iguales && aux != null)
                {
                    int i = 0;
                    while (i < contador && numerosComprobados[i] != aux.dato) i++;

                    if (i == contador)
                    {
                        numerosComprobados[contador] = aux.dato;
                        contador++;
                        iguales = ComparaNumeroElementos(aux.dato, l.CuentaOcurrencias(aux.dato));
                    }

                    aux = aux.sig;
                }
            }
            else iguales = false;

            return iguales;
        }

        private bool ComparaNumeroElementos(int numero, int repeticiones)
        {
            Nodo aux = pri;
            int contador = 0;

            while (aux != null && contador <= repeticiones)
            {
                if (aux.dato == numero) contador++;
                aux = aux.sig;
            }

            return contador == repeticiones;
        }
    }
}

