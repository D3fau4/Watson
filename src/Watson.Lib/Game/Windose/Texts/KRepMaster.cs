namespace Watson.Lib.Game.Windose.Texts;

public class KRepMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string BodyCn;
        public string BodyEn;
        public string BodyJp;
        public string BodyKo;
        public string BodyTw;
        public string IconId;
        public string Id;
        public string ImageId;
        public string ParentID;
        public string UserId;
    }
}