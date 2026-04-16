using System.Globalization;
using TaskManager.ViewModels;

namespace TaskManager.Converters;

public class FilterToButtonColorsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TaskCategoryFilter current)
            return Colors.Transparent;

        if (parameter is null)
            return Colors.Transparent;

        TaskCategoryFilter btn;
        if (parameter is TaskCategoryFilter p)
            btn = p;
        else if (!Enum.TryParse(parameter.ToString(), out btn))
            return Colors.Transparent;

        var selected = current == btn;
        return selected ? Color.FromArgb("#111827") : Color.FromArgb("#F3F4F6");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

