namespace SecretSantaEmailSender.Core.Lists.Extensions;

public static class ListsExtensions
{
    public static void Shuffle<T>(this IList<T> list, Random? random = null)
    {
        random ??= new Random();

        for (int i = 0; i < list.Count; i++)
        {
            var newPosition = random.Next(0, list.Count);

            (list[i], list[newPosition]) = (list[newPosition], list[i]);
        }
    }
}
