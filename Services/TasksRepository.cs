using System.Collections.ObjectModel;
using TaskManager.Models;

namespace TaskManager.Services;

public class TasksRepository
{
    public ObservableCollection<Tache> Taches { get; } = new();

    public TasksRepository()
    {
        Seed();
    }

    private void Seed()
    {
        Taches.Add(new Tache { Titre = "Lire le module 3", Categorie = TaskCategory.Etudes, Description = "MVVM + Toolkit : bindings, commands, navigation." });
        Taches.Add(new Tache { Titre = "Faire le devoir TaskManager", Categorie = TaskCategory.Etudes, Description = "Filtres, compteur, détail, zéro code-behind." });
        Taches.Add(new Tache { Titre = "Préparer la réunion", Categorie = TaskCategory.Travail, Description = "Agenda, points clés, actions." });
        Taches.Add(new Tache { Titre = "Acheter du lait", Categorie = TaskCategory.Personnel, Description = "2L, demi-écrémé." });
    }
}

