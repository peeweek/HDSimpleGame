using System.Collections.Generic;

namespace ThirdPersonController
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}
