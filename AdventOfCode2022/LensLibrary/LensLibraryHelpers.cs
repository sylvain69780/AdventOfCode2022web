namespace Domain.LensLibrary
{
    internal static class LensLibraryHelpers
    {

        public static int Hash(string s)
        {
            var h = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                h += (int)c;
                h *= 17;
                h %= 256;
            }
            return h;
        }
    }
}