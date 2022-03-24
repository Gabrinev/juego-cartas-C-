﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartas
{
    class Program
    {
        static public Player p1 = new Player(1);
        static public Player p2 = new Player(2);
        static public Player p3 = new Player(3);
        static public Player p4 = new Player(4);
        static public Player p5 = new Player(5);
        static public String[] palos = { "Oros", "Copas", "Espadas", "Bastos" };
        static public Baraja baraja = new Baraja();


        static void Main(string[] args)
        {
            List<Player> twoPlayers = new List<Player>() { p1, p2 };
            List<Player> threePlayers = new List<Player>() { p1, p2, p3 };
            List<Player> fourPlayers = new List<Player>() { p1, p2, p3, p4 };
            List<Player> fivePlayers = new List<Player>() { p1, p2, p3, p4, p5 };
            int n = selectPlayers();
            if (n == 1) {
                game(twoPlayers);
            } 
            else if (n == 2)
            {
                game(threePlayers);
            }
            else if (n == 3)
            {
                game(fourPlayers);
            }
            else if (n == 4)
            {
                game(fivePlayers);
            }



            Console.WriteLine("");
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadKey();


        }
        
        static void game(List<Player> players)
        {
            Random r = new Random();
            int round = 0;            
            Carta pCard;
            List<Carta> roundCards = new List<Carta>();
            List<Player> playersMod = new List<Player>();
            int mainSuit = r.Next(0, 4);



            dealCards(players, baraja);


            while (players.Count()>1)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("RONDA " + round);
                Console.WriteLine("PALO GANADOR DE ESTA PARTIDA: " + palos[mainSuit]);
                foreach (Player p in players)
                {
                    Console.WriteLine("");
                    pCard = p.playCard();
                    roundCards.Add(pCard);
                    Console.WriteLine("Jugador " + p.numPlayer + " saca " + pCard.escribeCarta());
                }

                checkRoundWinner(roundCards, mainSuit);
                playersMod = checkLosers(players);
                players = playersMod;
                round++;
                roundCards.Clear();
                Console.WriteLine("Pulse espacio para pasar a la siguiente ronda");
                Console.ReadKey();
            }
            Console.WriteLine("*********************************************");
            Console.WriteLine("HA GANADO EL JUGADOR "+players[0].numPlayer);
            Console.WriteLine("*********************************************");
        }

        static void checkRoundWinner(List<Carta> cards, int mainSuit)
        {
            Carta temp = cards[0];
            Player winner;
            Boolean tie = false;
            
            foreach (Carta c in cards)
            {

                if (temp.palo == mainSuit && c.palo == mainSuit)
                {
                    if (c.numero > temp.numero)
                    {
                        temp = c;
                        tie = false;
                    }
                        
                } else if (c.palo == mainSuit && temp.palo != mainSuit)
                {
                    temp = c;
                    tie = false;
                } else if (c.palo != mainSuit && temp.palo != mainSuit)
                {
                    if (c.numero > temp.numero)
                    {
                        temp = c;
                        tie = false;
                    }
                    else if (c.numero == temp.numero)
                        tie = true;
                    else tie = false;
                }               
                              
            }
            if (!tie)
            {
                winner = temp.owner;
                foreach (Carta c in cards)
                {
                    winner.addCard(c);
                }
                Console.WriteLine(" ");
                Console.WriteLine("EL GANADOR DE ESTA RONDA ES EL JUGADOR: " + winner.numPlayer);
                Console.WriteLine(" ");
            }
            else
            {
                foreach (Carta c in cards)
                {
                    c.owner.addCard(c);
                }
                Console.WriteLine(" ");
                Console.WriteLine("EMPATE, SE DEVOLVERAN LAS CARTAS");
                Console.WriteLine(" ");
            }

            


        }

        static void dealCards(List<Player> players, Baraja baraja)
        {

            

            while (baraja.numeroCartas() > 0)
            {
                foreach (Player p in players)
                {
                    baraja.giveCardToPlayer(p);
                }
            }           
        }


        static List<Player> checkLosers(List<Player> players)
        {
            Player temp= new Player();
            Boolean elimination = false;
            foreach (Player p in players)
            {
                if (p.mano.Count() == 0)
                {
                    Console.WriteLine("PLAYER "+p.numPlayer+" ELIMINADO");
                    temp = p;
                    elimination = true;
                }else Console.WriteLine("A JUGADOR "+p.numPlayer+" LE QUEDAN "+ p.mano.Count());
            }
            if (elimination)
                players.Remove(temp);
            return players;
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
            Console.Clear();
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
       

        public Player(int n)
        {
            _numPlayer = n;
            
        }
        public Player()
        {          

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
        Player _owner;
        String[] palos = { "Oros", "Copas", "Espadas", "Bastos" };

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
        public int palo
        {
            get
            {
                return _palo;
            }

        }

        public String escribeCarta()
        {
            return numero + " de " + palos[_palo];
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
                    Console.WriteLine("Añadido a baraja" + card.escribeCarta());
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
            Console.WriteLine("Dada la carta "+c.escribeCarta()+" a "+c.owner);
        }


    }
}
