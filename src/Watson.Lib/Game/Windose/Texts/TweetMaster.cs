namespace Watson.Lib.Game.Windose.Texts;

public class TweetMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public int BuzzPowerFav;
        public int BuzzPowerRT;
        public string CommandID;
        public string Id;
        public bool isDay;
        public bool isNight;
        public string OmoteBodyCn;
        public string OmoteBodyEn;
        public string OmoteBodyJp;
        public string OmoteBodyTw;
        public string OmoteImageId;
        public string OtomeBodyKo;
        public string Result;
        public string UraBodyCn;
        public string UraBodyEn;
        public string UraBodyJp;
        public string UraBodyKo;
        public string UraBodyTw;
        public string UraImageId;
    }
}