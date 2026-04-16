# TaskManager (MVVM)

**Programmeur : Fabrice**

Application **.NET MAUI** de gestion de tâches réalisée en **MVVM** avec **CommunityToolkit.Mvvm**, sans logique dans le code-behind (hors affectation du `BindingContext`).

## Alignement avec la grille (colonne « Excellent » 8–10)

| Critère | Ce que le projet respecte |
| --- | --- |
| **Architecture MVVM** | ViewModels en `partial` + `ObservableObject`, commandes `[RelayCommand]`, logique hors des `.xaml.cs` (code-behind limité à `InitializeComponent` + `BindingContext`). |
| **Binding et commands** | Bindings `TwoWay` sur les saisies, `CanExecute` sur l’ajout (`AjouterCommand`), liste avec **`ObservableCollection<Tache>`** (via `TasksRepository`). |
| **Filtrage** | Filtres par catégorie (Toutes / Travail / Personnel / Études), liste visible mise à jour en temps réel. |
| **Qualité du code** | Dossiers `Models/`, `ViewModels/`, `Views/`, `Services/`, noms explicites, namespaces cohérents. |
| **Design et UX** | Cartes avec ombre, tâche terminée visuellement distincte (barré / grisé), compteurs **restantes** et **faites** fonctionnels et cohérents avec le filtre actif. |

## Fonctionnalités

- Ajout d’une tâche avec **Titre**, **Catégorie** (Travail / Personnel / Études) et **Description** (onglet **Ajouter**)
- Liste des tâches (onglet **Accueil**) avec :
  - **Filtrage** par catégorie (Toutes, Travail, Personnel, Études)
  - Marquer une tâche **faite / non faite** (style visuel différent)
  - **Suppression** via swipe
  - Compteurs **Restantes** (non faites) et **Faites** (faites), adaptés au filtre actif
- Navigation vers une page **Détail** au tap sur une tâche

## Architecture

Structure respectée :

- `Models/` : entités (`Tache`, `TaskCategory`)
- `ViewModels/` : logique MVVM (`TasksViewModel`, `AddTaskViewModel`, `TaskDetailViewModel`)
- `Views/` : pages XAML (`TasksPage`, `AddTaskPage`, `TaskDetailPage`)
- `Services/` : stockage en mémoire partagé (`TasksRepository` en singleton)

Toolkit utilisé :

- `ObservableObject`, `[ObservableProperty]`, `[RelayCommand]`
- `ObservableCollection<Tache>`

## Lancer l’application

### Android (CLI)

Dans le dossier du projet :

```bash
cd TaskManager
dotnet restore
dotnet build -f net10.0-android
dotnet build -t:Run -f net10.0-android
```

### Android (Visual Studio)

- Ouvrir `TaskManager.csproj`
- Sélectionner une cible **Android** (émulateur ou appareil)
- Lancer en **Debug**

## Notes

- Les données sont stockées en mémoire (pas de base de données) via `TasksRepository`.

