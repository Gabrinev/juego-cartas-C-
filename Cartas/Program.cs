using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartas
{
    class Program
    {
        static Player p1 = new Player(1);
        static Player p2 = new Player(2);
        static Player p3 = new Player(3);
        static Player p4 = new Player(4);
        static Player p5 = new Player(5);


        static void Main(string[] args)
        {
            List<Player> twoPlayers = new List<Player>() { p1, p2 };
            List<Player> threePlayers = new List<Player>() { p1, p2, p3 };
            List<Player> fourPlayers = new List<Player>() { p1, p2, p3, p4 };
            List<Player> fivePlayers = new List<Player>() { p1, p2, p3, p4, p5 };
            int n = selectPlayers();
            if (n == 2) {
                game(twoPlayers);
            } 
            else if (n == 3)
            {
                game(threePlayers);
            }
            else if (n == 4)
            {
                game(fourPlayers);
            }
            else if (n == 5)
            {
                game(fivePlayers);
            }



            Console.WriteLine("");
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadKey();


        }
        
        static void game(List<Player> players)
        {
            int round = 0;
            Boolean ganador = false;
            Carta pCard;
            List<Carta> roundCards = new List<Carta>();


            dealCards(players);


            while (!ganador)
            {
                Console.WriteLine("");
                Console.WriteLine("RONDA " + round);
                foreach (Player p in players)
                {
                    if (!p.eliminado)
                    {
                        Console.WriteLine("");
                        pCard = p.playCard();
                        roundCards.Add(pCard);
                        Console.WriteLine("Jugador " + p.numPlayer + " saca " + pCard.escribeCarta());   
                    }
                }

                checkRoundWinner(roundCards);
                checkLosers();
                round++;
                Console.WriteLine("Espacio para pasar a la siguiente ronda");
                Console.ReadKey();
            }                
        }

        static void checkRoundWinner(List<Carta> cards)
        {
            Carta temp = cards[0];
            Player winner;
            
            foreach (Carta c in cards)
            {
               if (c.numero > temp.numero)
                {
                    temp = c;
                }
            }
            winner = temp.owner;

            foreach (Carta c in cards)
            {
                winner.addCard(c);
            }


        }

        static void dealCards(List<Player> players)
        {

            Baraja baraja = new Baraja();

            foreach (Player p in players)
            {
                if (baraja.numeroCartas() > 0)
                {

                    baraja.giveCardToPlayer(p);
                }
                else break;
                
            }
        }


        static void checkLosers()
        {
            
            if (p1.mano.Count() == 0)
            {
                p1.eliminado = true;
                Console.WriteLine("PLAYER 1 ELIMINADO");
            } else if (p2.mano.Count() == 0)
            {
                p2.eliminado = true;
                Console.WriteLine("PLAYER 2 ELIMINADO");
            }
            else if (p3.mano.Count() == 0)
            {
                p3.eliminado = true;
                Console.WriteLine("PLAYER 3 ELIMINADO");
            }
            else if (p4.mano.Count() == 0)
            {
                p4.eliminado = true;
                Console.WriteLine("PLAYER 4 ELIMINADO");
            }
            else if (p5.mano.Count() == 0)
            {
                p5.eliminado = true;
                Console.WriteLine("PLAYER 5 ELIMINADO");
            }
        }


        static int selectPlayers()
        {
            menu();
            int bowl; // Variable to hold number

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                bowl = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
                return bowl;
            }
            else
            {
                bowl = 0;  // Else we assign a default value
                return bowl;
            }
        }
        static void menu()
        {
            Console.WriteLine("Bienvenido al juego de cartas");
            Console.WriteLine("Pulse 1 para jugar 2 Jugadores");
            Console.WriteLine("Pulse 2 para jugar 3 Jugadores");
            Console.WriteLine("Pulse 3 para jugar 4 Jugadores");
            Console.WriteLine("Pulse 4 para jugar 5 Jugadores");
            Console.WriteLine("Pulse 0 para salir");
        }
    }
    
    class Player
    {
        private int _numPlayer;
        private List<Carta> _mano = new List<Carta>();
        private Boolean _eliminado = false;

        public int numPlayer
        {
            get
            {
                return _numPlayer;
            }
        }
        public List<Carta> mano
        {
            get
            {
                return _mano;
            }
            
        }
        public Boolean eliminado
        {
            get
            {
                return _eliminado;
            }
            set
            {
                _eliminado = value;
            }
        }

        public Player(int n)
        {
            _numPlayer = n;
            
        }

        public void addCard(Carta c)
        {
            _mano.Add(c);
        }

        public Carta playCard()
        {
            Carta c = mano[0];
            mano.Remove(mano[0]);
            return c;

        }





    }
    class Carta
    {
        private int _numero;
        private int _palo;
        String[] palos = { "Oros", "Copas", "Espadas", "Bastos" };
        Player _owner;

        public Carta(int n, int p)
        {
            _numero = n;
            _palo = p;
        }
        public Player owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }
        public int numero
        {
            get
            {
                return _numero;
            }
           
        }

        public String escribeCarta()
        {
            String c = numero + " de " + palos[_palo];
            return c;
        }
    }
    class Baraja
    {
        List<Carta> baraja = new List<Carta>();
        Carta card;

        public Baraja()
        {
            List<Carta> orderedDeck = new List<Carta>();
            int i, j;
            var rnd = new Random();
            // mediante el bucle creamos una baraja ordenada
            for (i = 0; i < 4; i++)
            {                
                for (j = 0; j < 12; j++)
                {
                    card = new Carta(j + 1, i);
                    orderedDeck.Add(card);
                } 
            }
            // y aqui la desordenamos y guardamos
            var randomized = orderedDeck.OrderBy(item => rnd.Next());
            foreach (var value in randomized)
            {
                baraja.Add(value);
            }
        }
        public int numeroCartas()
        {
            return baraja.Count();
        }

        public void giveCardToPlayer(Player p)
        {
            Carta c = baraja[0];
            c.owner = p;
            baraja.Remove(baraja[0]);
            p.addCard(c);
        }


    }
}
