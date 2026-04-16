using System.Globalization;
using TaskManager.ViewModels;

namespace TaskManager.Converters;

public class FilterToButtonTextColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TaskCategoryFilter current)
            return Colors.Black;

        if (parameter is null)
            return Colors.Black;

        TaskCategoryFilter btn;
        if (parameter is TaskCategoryFilter p)
            btn = p;
        else if (!Enum.TryParse(parameter.ToString(), out btn))
            return Colors.Black;

        var selected = current == btn;
        return selected ? Colors.White : Color.FromArgb("#111827");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

