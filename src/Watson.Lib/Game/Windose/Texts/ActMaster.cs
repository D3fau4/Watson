namespace Watson.Lib.Game.Windose.Texts;

public class ActMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string Id;
        public string TitleCn;
        public string TitleEn;
        public string TitleJp;
        public string TitleKo;
        public string TitleTw;
    }
}