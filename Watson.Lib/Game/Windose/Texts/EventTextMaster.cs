namespace Watson.Lib.Game.Windose.Texts;

public class EventTextMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string ArgumentType1;
        public string ArgumentType2;
        public string ArgumentType3;
        public string ArgumentType4;
        public string BodyCn;
        public string BodyEn;
        public string BodyJp;
        public string BodyKo;
        public string BodyTw;
        public string EventId;
        public string Id;
    }
}