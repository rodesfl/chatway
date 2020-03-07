using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTest {
    class Conexao {
        HubConnection connection;


        public Conexao() {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chathub")
                .Build();

            connection.Closed += async (error) => {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                Console.WriteLine("Reconnecting...");
                await connection.StartAsync();
            };
        }
        public async void Conectar() {

            try {
                await connection.StartAsync();
                Console.WriteLine(connection.ConnectionId);
                Console.WriteLine("Connection started");
                await connection.InvokeAsync("Handshake", new { testando = "haha" });
                Console.WriteLine("Metodo invocado");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }


}
