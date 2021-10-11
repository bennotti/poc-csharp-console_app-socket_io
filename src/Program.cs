using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poc_csharp_console_app_socket_io
{
    class Program
    {
        public static async Task exec()
        {
            var client = new SocketIO("http://localhost:8080/", new SocketIOOptions
            {
                Query = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("token", "abc123")
                }
            });
            client.On("chat_message", response =>
            {
                // You can print the returned data first to decide what to do next.
                // output: ["hi client"]
                Console.WriteLine(response);

                string text = response.GetValue<string>();

                // The socket.io server code looks like this:
                // socket.emit('hi', 'hi client');
            });

            //client.On("test", response =>
            //{
            //    // You can print the returned data first to decide what to do next.
            //    // output: ["ok",{"id":1,"name":"tom"}]
            //    Console.WriteLine(response);

            //    // Get the first data in the response
            //    string text = response.GetValue<string>();
            //    // Get the second data in the response
            //    var dto = response.GetValue<TestDTO>(1);

            //    // The socket.io server code looks like this:
            //    // socket.emit('hi', 'ok', { id: 1, name: 'tom'});
            //});

            client.OnConnected += async (sender, e) =>
            {
                // Emit a string
                await client.EmitAsync("chat_message", "C# socket.io");

                // Emit a string and an object
                //var dto = new TestDTO { Id = 123, Name = "bob" };
                //await client.EmitAsync("register", "source", dto);
            };
            await client.ConnectAsync();
        }

        static void Main(string[] args)
        {
			exec().Wait();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
