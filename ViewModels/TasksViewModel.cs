using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;

namespace TaskManager.ViewModels;

public enum TaskCategoryFilter
{
    Toutes = 0,
    Travail = 1,
    Personnel = 2,
    Etudes = 3,
}

public partial class TasksViewModel : ObservableObject
{
    public IReadOnlyList<TaskCategory> Categories { get; } = Enum.GetValues<TaskCategory>();

    private readonly TasksRepository _repo;

    public ObservableCollection<Tache> ToutesLesTaches => _repo.Taches;

    [ObservableProperty]
    private ObservableCollection<Tache> _tachesVisibles = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AjouterCommand))]
    private string _nouveauTitre = string.Empty;

    [ObservableProperty]
    private string _nouvelleDescription = string.Empty;

    [ObservableProperty]
    private TaskCategory _nouvelleCategorie = TaskCategory.Travail;

    [ObservableProperty]
    private TaskCategoryFilter _filtreActif = TaskCategoryFilter.Toutes;

    public int TotalTachesVisibles => TachesVisibles.Count;

    public int NombreTachesRestantes => TachesVisibles.Count(t => !t.EstFaite);

    public int NombreTachesFaites => TachesVisibles.Count(t => t.EstFaite);

    public string TitrePage =>
        NombreTachesRestantes switch
        {
            0 => "TaskManager — 0 restante",
            1 => "TaskManager — 1 restante",
            _ => $"TaskManager — {NombreTachesRestantes} restantes"
        };

    public TasksViewModel(TasksRepository repo)
    {
        _repo = repo;
        ToutesLesTaches.CollectionChanged += ToutesLesTaches_CollectionChanged;

        foreach (var t in ToutesLesTaches)
            HookTache(t);

        AppliquerFiltre();
        RafraichirCompteurs();
    }

    private void ToutesLesTaches_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is not null)
        {
            foreach (var item in e.NewItems.OfType<Tache>())
                HookTache(item);
        }

        if (e.OldItems is not null)
        {
            foreach (var item in e.OldItems.OfType<Tache>())
                UnhookTache(item);
        }

        AppliquerFiltre();
        RafraichirCompteurs();
    }

    private void HookTache(Tache tache) => tache.PropertyChanged += Tache_PropertyChanged;

    private void UnhookTache(Tache tache) => tache.PropertyChanged -= Tache_PropertyChanged;

    private void Tache_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(Tache.EstFaite) or nameof(Tache.Categorie))
        {
            AppliquerFiltre();
            RafraichirCompteurs();
        }
    }

    [RelayCommand(CanExecute = nameof(PeutAjouter))]
    private void Ajouter()
    {
        var t = new Tache
        {
            Titre = NouveauTitre.Trim(),
            Categorie = NouvelleCategorie,
            Description = (NouvelleDescription ?? string.Empty).Trim(),
            EstFaite = false
        };

        ToutesLesTaches.Add(t);
        NouveauTitre = string.Empty;
        NouvelleDescription = string.Empty;
        NouvelleCategorie = TaskCategory.Travail;
    }

    private bool PeutAjouter() => !string.IsNullOrWhiteSpace(NouveauTitre);

    [RelayCommand]
    private void Supprimer(Tache tache)
    {
        ToutesLesTaches.Remove(tache);
    }

    [RelayCommand]
    private void BasculerTerminee(Tache tache)
    {
        tache.EstFaite = !tache.EstFaite;
    }

    [RelayCommand]
    private void Filtrer(TaskCategoryFilter filtre)
    {
        FiltreActif = filtre;
        AppliquerFiltre();
    }

    partial void OnFiltreActifChanged(TaskCategoryFilter value) => AppliquerFiltre();

    private void AppliquerFiltre()
    {
        IEnumerable<Tache> query = ToutesLesTaches;

        query = FiltreActif switch
        {
            TaskCategoryFilter.Travail => query.Where(t => t.Categorie == TaskCategory.Travail),
            TaskCategoryFilter.Personnel => query.Where(t => t.Categorie == TaskCategory.Personnel),
            TaskCategoryFilter.Etudes => query.Where(t => t.Categorie == TaskCategory.Etudes),
            _ => query
        };

        var nouvelle = new ObservableCollection<Tache>(query.OrderBy(t => t.EstFaite).ThenByDescending(t => t.DateCreation));
        TachesVisibles = nouvelle;

        RafraichirCompteurs();
    }

    private void RafraichirCompteurs()
    {
        OnPropertyChanged(nameof(TotalTachesVisibles));
        OnPropertyChanged(nameof(NombreTachesRestantes));
        OnPropertyChanged(nameof(NombreTachesFaites));
        OnPropertyChanged(nameof(TitrePage));
    }

    [RelayCommand]
    private async Task OuvrirDetail(Tache tache)
    {
        if (tache is null)
            return;

        await Shell.Current.GoToAsync(nameof(TaskDetailPage), new Dictionary<string, object>
        {
            ["Tache"] = tache
        });
    }
}

