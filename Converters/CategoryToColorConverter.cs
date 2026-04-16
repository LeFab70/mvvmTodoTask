using System.Globalization;
using TaskManager.Models;

namespace TaskManager.Converters;

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

