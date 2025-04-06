using System.ComponentModel;

public class MyTask : INotifyPropertyChanged
{
    private string title;
    private string description;
    private string priority;
    private string status;

    public string Title
    {
        get => title;
        set
        {
            if (title != value)
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }

    public string Description
    {
        get => description;
        set
        {
            if (description != value)
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    public string Priority
    {
        get => priority;
        set
        {
            if (priority != value)
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
    }

    public string Status
    {
        get => status;
        set
        {
            if (status != value)
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Reset()
    {
        Status = "Новая";
        Priority = "Низкий";
        Description = string.Empty;
    }
}