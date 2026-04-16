using System.Globalization;
using TaskManager.Models;

namespace TaskManager.Converters;

//permet de convertir une catégorie en couleur
//IValueConverter est une interface qui permet de convertir un type en un autre
//value est la valeur à convertir
//targetType est le type de la valeur cible
//parameter est un paramètre facultatif
//culture est la culture à utiliser pour la conversion
//idée : on peut utiliser cette classe pour convertir une catégorie en couleur dans un binding
//par exemple :
//<Border BackgroundColor="{Binding Categorie, Converter={StaticResource CategoryToColorConverter}}" />
//<Label Text="{Binding Categorie, Converter={StaticResource CategoryToColorConverter}}" />
//<Picker ItemsSource="{Binding Categories, Converter={StaticResource CategoryToColorConverter}}" />
//<Picker ItemsSource="{Binding Categories, Converter={StaticResource CategoryToColorConverter}}" />
public class CategoryToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TaskCategory category)
            return Colors.Gray;

        return category switch
        {
            TaskCategory.Travail => Color.FromArgb("#2563EB"),   // blue
            TaskCategory.Personnel => Color.FromArgb("#9333EA"), // purple
            TaskCategory.Etudes => Color.FromArgb("#0D9488"),    // teal
            _ => Colors.Gray
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

