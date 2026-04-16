using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskManager.Models;

public partial class Tache : ObservableObject
{
    public Guid Id { get; } = Guid.NewGuid();

    [ObservableProperty]
    private string _titre = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private TaskCategory _categorie = TaskCategory.Travail;

    [ObservableProperty]
    private bool _estFaite;

    public DateTime DateCreation { get; } = DateTime.Now;
}

