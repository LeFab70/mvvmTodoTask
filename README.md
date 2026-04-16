# TaskManager (MVVM) — Fabrice

Application **.NET MAUI** de gestion de tâches réalisée en **MVVM** avec **CommunityToolkit.Mvvm**, sans logique dans le code-behind (hors affectation du `BindingContext`).

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

