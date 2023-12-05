namespace Watson.Lib.Game.Windose.Texts;

public class LineMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string ArgumentType;
        public string BodyCn;
        public string BodyEn;
        public string BodyJp;
        public string BodyKo;
        public string BodyTw;
        public string Id;
        public string ImageId;
        public string ParentId;
        public string Speaker;
    }
}