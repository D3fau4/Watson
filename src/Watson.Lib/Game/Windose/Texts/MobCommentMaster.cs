namespace Watson.Lib.Game.Windose.Texts;

public class MobCommentMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public string BodyCh;
        public string BodyEn;
        public string BodyJP;
        public string BodyKo;
        public string BodyTw;
        public string goodbad;
        public string Id;
        public int Rank;
        public string timing;
    }
}