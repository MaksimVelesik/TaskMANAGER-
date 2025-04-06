using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

public class TaskChat
{
    private const string PipeName = "TaskChatPipe";

    public void StartServer()
    {
        var server = new NamedPipeServerStream(PipeName);
        server.WaitForConnection();

        using (var reader = new StreamReader(server))
        {
            while (true)
            {
                var message = reader.ReadLine();
                Console.WriteLine($"Received: {message}");
            }
        }
    }

    public void SendMessage(string message)
    {
        using (var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
        {
            client.Connect();
            using (var writer = new StreamWriter(client) { AutoFlush = true })
            {
                writer.WriteLine(message);
            }
        }
    }
}