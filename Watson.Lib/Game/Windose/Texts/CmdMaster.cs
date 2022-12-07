namespace Watson.Lib.Game.Windose.Texts;

public class CmdMaster
{
    public List<Param> param = new();

    [Serializable]
    public class Param
    {
        public int AttentionDelta;
        public int CinePhillCount;
        public int DatespotCount;
        public string DescCn;
        public string DescEn;
        public string DescJp;
        public string DescKo;
        public string DescTw;
        public int FavorDelta;
        public int FollowerDelta;
        public int GameCount;
        public int Harumagedo;
        public string Id;
        public string LabelCn;
        public string LabelEn;
        public string LabelJp;
        public string LabelKo;
        public string LabelTw;
        public int OirokeCount;
        public int OkusuriCount;
        public string ParentAct;
        public int PassingTime;
        public int ShabekuriCount;
        public int SNS;
        public int StressDelta;
        public string TweetID;
        public int YamiDelta;
    }
}