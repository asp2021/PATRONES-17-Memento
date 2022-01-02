using System;
using System.Collections.Generic;

namespace Memento
{
    class Program
    {
        public class Memento
        { 
            public int Balance { get; }

            public Memento(int balance)
            {
                Balance = balance;
            }
            
        }

        public class Account
        {
            public int Balance { get; set; }
            private List<Memento> _Operations = new List<Memento>();
            private int _current;

            public Account(int balance)
            {
                Balance = balance;
                _Operations.Add(new Memento(balance));
                _current++;
            }

            public Memento Deposit(int amount)
            {
                Balance += amount;
                var memento = new Memento(Balance);
                _Operations.Add(memento);
                _current++;
                return memento;
            }

            public Memento Undo()
            {
                if (_current > 0)
                {
                    var memento = _Operations[--_current];
                    Balance = memento.Balance;
                    return memento;
                }
                return null;
            }

            public Memento Redo()
            {
                if(_current + 1 < _Operations.Count)
                {
                    var memento = _Operations[++_current];
                    Balance = memento.Balance;
                    return memento;
                }
                return null;
            }

            public void Restore(Memento memento)
            {
                Balance = memento.Balance;
            }

            public override string ToString() => $"Balance: {Balance}";
           
        }



        static void Main(string[] args)
        {
            Console.WriteLine("MEMENTO" + "\n");
            Console.WriteLine("Nos permite  volver un objeto a un estado anterior." + "\n");


            var account = new Account(100);
            Console.WriteLine($"Saldo inicial: {account}");

            var m1 = account.Deposit(20);  // 120
            Console.WriteLine($"Deposito 20: {account}");

            var m2 = account.Deposit(30);  // 150
            Console.WriteLine($"Deposito 30: {account}");

            account.Undo();
            Console.WriteLine($"Undo1: {account}");
            
            account.Undo();
            Console.WriteLine($"Undo2: {account}");

            account.Undo();
            Console.WriteLine($"Undo3: {account}");

            account.Redo();
            Console.WriteLine($"Redo: {account}");

            Console.ReadLine();
        }
    }
}
