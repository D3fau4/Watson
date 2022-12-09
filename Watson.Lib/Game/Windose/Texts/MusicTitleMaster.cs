namespace Watson.Lib.Game.Windose.Texts;

public class MusicTitleMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string BodyCn;
        public string BodyEn;
        public string BodyJp;
        public string FileName;
        public string goodbad;
        public string Id;
        public int Index;
        public bool IsPlayable;
    }
}