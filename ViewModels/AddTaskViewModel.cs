using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.ViewModels;

public partial class AddTaskViewModel : ObservableObject
{
    private readonly TasksRepository _repo;

    public IReadOnlyList<TaskCategory> Categories { get; } = Enum.GetValues<TaskCategory>();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AjouterCommand))]
    private string _titre = string.Empty;

    [ObservableProperty]
    private TaskCategory _categorie = TaskCategory.Travail;

    [ObservableProperty]
    private string _description = string.Empty;

    public AddTaskViewModel(TasksRepository repo)
    {
        _repo = repo;
    }

    [RelayCommand(CanExecute = nameof(PeutAjouter))]
    private async Task Ajouter()
    {
        var t = new Tache
        {
            Titre = Titre.Trim(),
            Categorie = Categorie,
            Description = (Description ?? string.Empty).Trim(),
            EstFaite = false
        };

        _repo.Taches.Add(t);

        Titre = string.Empty;
        Description = string.Empty;
        Categorie = TaskCategory.Travail;

        await Shell.Current.DisplayAlertAsync("Ajoutée", "Ta tâche a été ajoutée.", "OK");
    }

    private bool PeutAjouter() => !string.IsNullOrWhiteSpace(Titre);
}

