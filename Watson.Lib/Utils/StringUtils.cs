namespace Watson.Lib.Utils;

internal class StringUtils
{
    public static bool IsUnityVersionGreaterThan(string minVersion, string currVersion)
    {
        string[] minimumVersionParts = minVersion.Split('.');
        string[] currentVersionParts = currVersion.Split('.');

        for (int i = 0; i < currentVersionParts.Length; i++)
        {
            int minimumPart = int.Parse(minimumVersionParts[i]);
            int currentPart = int.Parse(currentVersionParts[i]);

            if (currentPart > minimumPart)
            {
                return true;
            }
            else if (currentPart < minimumPart)
            {
                return false;
            }
        }

        return false;
    }
}