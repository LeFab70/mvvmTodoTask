using CommunityToolkit.Mvvm.ComponentModel;
using TaskManager.Models;

namespace TaskManager.ViewModels;

public partial class TaskDetailViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private Tache? _tache;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Tache", out var value) && value is Tache t)
            Tache = t;
    }
}

