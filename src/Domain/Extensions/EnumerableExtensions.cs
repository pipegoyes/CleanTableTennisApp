namespace CleanTableTennisApp.Domain.Extensions;

public static class EnumerableExtensions
{
    public static (IReadOnlyCollection<T> TrueItems, IReadOnlyCollection<T> FalseItems) Split<T>(this IEnumerable<T> items, Func<T, bool> splitFunction)
    {
        var trueItems = new List<T>();
        var falseItems = new List<T>();

        foreach (var item in items)
        {
            if (splitFunction(item))
            {
                trueItems.Add(item);
            }
            else
            {
                falseItems.Add(item);
            }
        }

        return (trueItems, falseItems);
    }
}