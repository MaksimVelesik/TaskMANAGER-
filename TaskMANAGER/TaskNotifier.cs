using System;
using System.IO.MemoryMappedFiles;
using System.Text;

public class TaskNotifier
{
    private const string MapName = "TaskNotification";

    public void NotifyNewTask(string taskId)
    {
        using (var mmf = MemoryMappedFile.CreateOrOpen(MapName, 10000))
        {
            using (var stream = mmf.CreateViewStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(taskId);
                stream.Write(data, 0, data.Length);
            }
        }
    }

    public string WaitForNotification()
    {
        using (var mmf = MemoryMappedFile.OpenExisting(MapName))
        {
            using (var stream = mmf.CreateViewStream())
            {
                byte[] data = new byte[100];
                stream.Read(data, 0, data.Length);
                return Encoding.UTF8.GetString(data).TrimEnd('\0');
            }
        }
    }
}