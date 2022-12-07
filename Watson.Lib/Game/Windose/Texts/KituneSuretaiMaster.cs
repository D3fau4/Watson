namespace Watson.Lib.Game.Windose.Texts;

public class KituneSuretaiMaster
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
        public string Id;
    }
}