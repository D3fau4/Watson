namespace Watson.Lib.Game.Windose.Texts;

public class StatusTextMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string Body;
        public string BodyCn;
        public string BodyEn;
        public string BodyKo;
        public string BodyTw;
        public string Id;
    }
}