namespace Watson.Lib.Game.Windose.Texts;

public class EndingMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string EndingNameCn;
        public string EndingNameEn;
        public string EndingNameJp;
        public string EndingNameKo;
        public string EndingNameTw;
        public string Id;
        public string JissekiCn;
        public string JissekiEn;
        public string JissekiJp;
        public string JissekiKo;
        public string JissekiTw;
        public string ReasonCn;
        public string ReasonEn;
        public string ReasonJp;
        public string ReasonKo;
        public string ReasonTw;
    }
}