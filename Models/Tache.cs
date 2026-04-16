using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskManager.Models;

//on crée une classe Tache qui hérite de ObservableObject
//ObservableObject est une classe qui permet de créer des propriétés observables
public partial class Tache : ObservableObject
{
    //on crée une propriété Id qui est un Guid
    //Guid est un type qui permet de créer des identifiants uniques
    //NewGuid() crée un nouvel identifiant unique
    public Guid Id { get; } = Guid.NewGuid();

    [ObservableProperty]
    //on crée une propriété Titre qui est un string
    //string est un type qui permet de créer des chaînes de caractères
    //Empty est une chaîne de caractères vide
    private string _titre = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    //on crée une propriété Categorie qui est un TaskCategory
    [ObservableProperty]
    private TaskCategory _categorie = TaskCategory.Travail;

    [ObservableProperty]
    private bool _estFaite;

    public DateTime DateCreation { get; } = DateTime.Now;
}

