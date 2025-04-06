using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System;

public class ChatClient
{
    public async Task StartChatClient()
    {
        using (var client = new NamedPipeClientStream(".", "TaskChatPipe", PipeDirection.InOut))
        {
            await client.ConnectAsync();
            Console.WriteLine("Connected to chat server.");

            using (var reader = new StreamReader(client))
            using (var writer = new StreamWriter(client) { AutoFlush = true })
            {
                while (client.IsConnected)
                {
                    Console.Write("Enter message: ");
                    var message = Console.ReadLine();
                    await writer.WriteLineAsync(message);

                    var response = await reader.ReadLineAsync();
                    Console.WriteLine($"Server response: {response}");
                }
            }
        }
    }
}