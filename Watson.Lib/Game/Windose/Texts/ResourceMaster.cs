namespace Watson.Lib.Game.Windose.Texts;

public class ResourceMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string FileName;
        public string Id;
    }
}