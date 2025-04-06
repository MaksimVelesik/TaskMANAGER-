using System;
using System.ComponentModel;

public class TaskItem : INotifyPropertyChanged
{
    private string title;
    private DateTime deadline;
    private string status;

    public string Title
    {
        get => title;
        set
        {
            title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public DateTime Deadline
    {
        get => deadline;
        set
        {
            deadline = value;
            OnPropertyChanged(nameof(Deadline));
        }
    }

    public string Status
    {
        get => status;
        set
        {
            status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}