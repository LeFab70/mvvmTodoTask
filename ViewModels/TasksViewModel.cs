using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Models;
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

    public ObservableCollection<Tache> ToutesLesTaches { get; } = new();

    [ObservableProperty]
    private ObservableCollection<Tache> _tachesVisibles = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AjouterCommand))]
    private string _nouveauTitre = string.Empty;

    [ObservableProperty]
    private TaskCategory _nouvelleCategorie = TaskCategory.Travail;

    [ObservableProperty]
    private TaskCategoryFilter _filtreActif = TaskCategoryFilter.Toutes;

    public int NombreTachesRestantes => ToutesLesTaches.Count(t => !t.EstFaite);

    public string TitrePage =>
        NombreTachesRestantes switch
        {
            0 => "TaskManager — 0 restante",
            1 => "TaskManager — 1 restante",
            _ => $"TaskManager — {NombreTachesRestantes} restantes"
        };

    public TasksViewModel()
    {
        ToutesLesTaches.CollectionChanged += ToutesLesTaches_CollectionChanged;

        Seed();
        AppliquerFiltre();
        OnPropertyChanged(nameof(NombreTachesRestantes));
        OnPropertyChanged(nameof(TitrePage));
    }

    private void Seed()
    {
        AjouterTacheSeed("Lire le module 3", TaskCategory.Etudes);
        AjouterTacheSeed("Faire le devoir TaskManager", TaskCategory.Etudes);
        AjouterTacheSeed("Préparer la réunion", TaskCategory.Travail);
        AjouterTacheSeed("Acheter du lait", TaskCategory.Personnel);
    }

    private void AjouterTacheSeed(string titre, TaskCategory categorie)
    {
        var t = new Tache { Titre = titre, Categorie = categorie, EstFaite = false };
        HookTache(t);
        ToutesLesTaches.Add(t);
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
        OnPropertyChanged(nameof(NombreTachesRestantes));
        OnPropertyChanged(nameof(TitrePage));
    }

    private void HookTache(Tache tache) => tache.PropertyChanged += Tache_PropertyChanged;

    private void UnhookTache(Tache tache) => tache.PropertyChanged -= Tache_PropertyChanged;

    private void Tache_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(Tache.EstFaite) or nameof(Tache.Categorie))
        {
            AppliquerFiltre();
            OnPropertyChanged(nameof(NombreTachesRestantes));
            OnPropertyChanged(nameof(TitrePage));
        }
    }

    [RelayCommand(CanExecute = nameof(PeutAjouter))]
    private void Ajouter()
    {
        var t = new Tache
        {
            Titre = NouveauTitre.Trim(),
            Categorie = NouvelleCategorie,
            EstFaite = false
        };

        ToutesLesTaches.Add(t);
        NouveauTitre = string.Empty;
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

