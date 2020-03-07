using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRTest {
    class Program {


        static void Main(string[] args) {
            Conexao conn = new Conexao();
            conn.Conectar();
            Console.ReadLine();

        }
    }
}
