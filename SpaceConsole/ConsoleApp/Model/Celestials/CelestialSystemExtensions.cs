using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public static class CelestialSystemExtensions
    {
        public static IReadOnlyCollection<ICelestialSystem> GetSiblings(this ICelestialSystem root)
        {
            if (root.Parent == null)
                return new List<ICelestialSystem>();

            return root.Parent.Children.Except(root).ToList();
        }

        public static ICelestialSystem GetDescendent(this ICelestialSystem root, string name)
        {
            ICelestialSystem current = root;

            foreach (var child in current.Children)
            {
                if (child.Name == name)
                    return child;

                var descendent = child.GetDescendent(name);
                if (descendent != null)
                    return descendent;
            }

            return null;
        }
    }
}