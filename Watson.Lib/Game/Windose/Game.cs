using AssetsTools.NET.Extra;
using Watson.Lib.Game.Windose.Texts;
using Watson.Lib.IO;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.Windose;

public class Game : IGame
{
    private ActMaster actMaster;
    private CmdMaster cmdMaster;
    private EgosaMaster egosaMaster;
    private EndingMaster endingMaster;
    private EndingTextMaster endingTextMaster;
    private EventTextMaster eventTextMaster;
    private KituneMaster kituneMaster;
    private KituneSuretaiMaster kituneSuretaiMaster;
    private KusoCommentMaster kusoCommentMaster;
    private LineMaster lineMaster;
    private UnityAssetFile m_assetfile;
    private MobCommentMaster mobCommentMaster;
    private MusicTitleMaster musicTitleMaster;
    private KRepMaster repMaster;
    private StatusLabelMaster statusLabelMaster;
    private StatusTextMaster statusTextMaster;
    private SystemTextMaster systemTextMaster;
    private TenCommentMaster tenCommentMaster;
    private TooltipMaster tooltipMaster;
    private TweetMaster tweetMaster;
    private yakujoMaster yakujoMaster;

    public Game(string path)
    {
        gamepath = path;
        Load();
    }

    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private string m_assetfilepath { get; set; } = "resources.assets";

    public void Load()
    {
        gamedatapath = Path.Combine(gamepath, "Windose_Data");
        assemblyFolder = Path.Combine(gamedatapath, "Managed");
        m_assetfilepath = Path.Combine(gamedatapath, "resources.assets");
        m_assetfile = new UnityAssetFile(m_assetfilepath, gamedatapath);
    }

    public void Proccess()
    {
        foreach (var m_monobehaviour in m_assetfile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized =
                m_assetfile.AM.GetBaseField(m_assetfile.Assets, m_monobehaviour);
            var m_Script = m_assetfile.AM.GetExtAsset(m_assetfile.Assets, deserialized["m_Script"]).baseField;
            if (m_Script != null)
            {
                var classname = m_Script.Get("m_ClassName").Value.AsString;
                switch (classname)
                {
                    case "ActMaster":
                        actMaster = new ActMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new ActMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.TitleJp = m_param.Get("TitleJp").Value.AsString;
                            param.TitleEn = m_param.Get("TitleEn").Value.AsString;
                            param.TitleCn = m_param.Get("TitleCn").Value.AsString;
                            param.TitleKo = m_param.Get("TitleKo").Value.AsString;
                            param.TitleTw = m_param.Get("TitleTw").Value.AsString;
                            actMaster.param.Add(param);
                        }

                        Console.WriteLine($"ActMaster: {actMaster.param.Count}");
                        break;
                    case "CmdMaster":
                        cmdMaster = new CmdMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new CmdMaster.Param();
                            param.ParentAct = m_param.Get("ParentAct").Value.AsString;
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.LabelJp = m_param.Get("LabelJp").Value.AsString;
                            param.LabelEn = m_param.Get("LabelEn").Value.AsString;
                            param.LabelCn = m_param.Get("LabelCn").Value.AsString;
                            param.LabelKo = m_param.Get("LabelKo").Value.AsString;
                            param.LabelTw = m_param.Get("LabelTw").Value.AsString;
                            param.DescJp = m_param.Get("DescJp").Value.AsString;
                            param.DescEn = m_param.Get("DescEn").Value.AsString;
                            param.DescCn = m_param.Get("DescCn").Value.AsString;
                            param.DescKo = m_param.Get("DescKo").Value.AsString;
                            param.DescTw = m_param.Get("DescTw").Value.AsString;
                            param.TweetID = m_param.Get("TweetID").Value.AsString;
                            param.PassingTime = m_param.Get("PassingTime").Value.AsInt;
                            param.FollowerDelta = m_param.Get("FollowerDelta").Value.AsInt;
                            param.AttentionDelta = m_param.Get("AttentionDelta").Value.AsInt;
                            param.StressDelta = m_param.Get("StressDelta").Value.AsInt;
                            param.YamiDelta = m_param.Get("YamiDelta").Value.AsInt;
                            param.FavorDelta = m_param.Get("FavorDelta").Value.AsInt;
                            param.OkusuriCount = m_param.Get("OkusuriCount").Value.AsInt;
                            param.OirokeCount = m_param.Get("OirokeCount").Value.AsInt;
                            param.SNS = m_param.Get("SNS").Value.AsInt;
                            param.GameCount = m_param.Get("GameCount").Value.AsInt;
                            param.CinePhillCount = m_param.Get("CinePhillCount").Value.AsInt;
                            param.ShabekuriCount = m_param.Get("ShabekuriCount").Value.AsInt;
                            param.DatespotCount = m_param.Get("DatespotCount").Value.AsInt;
                            param.Harumagedo = m_param.Get("Harumagedo").Value.AsInt;
                            cmdMaster.param.Add(param);
                        }

                        Console.WriteLine($"CmdMaster: {cmdMaster.param.Count}");
                        break;
                    case "EgosaMaster":
                        egosaMaster = new EgosaMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new EgosaMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Type = m_param.Get("Type").Value.AsString;
                            param.Jouken = m_param.Get("Jouken").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            egosaMaster.param.Add(param);
                        }

                        Console.WriteLine($"EgosaMaster: {egosaMaster.param.Count}");
                        break;
                    case "EndingMaster":
                        endingMaster = new EndingMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new EndingMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.EndingNameJp = m_param.Get("EndingNameJp").Value.AsString;
                            param.EndingNameEn = m_param.Get("EndingNameEn").Value.AsString;
                            param.EndingNameCn = m_param.Get("EndingNameCn").Value.AsString;
                            param.EndingNameKo = m_param.Get("EndingNameKo").Value.AsString;
                            param.EndingNameTw = m_param.Get("EndingNameTw").Value.AsString;
                            param.JissekiJp = m_param.Get("JissekiJp").Value.AsString;
                            param.JissekiEn = m_param.Get("JissekiEn").Value.AsString;
                            param.JissekiCn = m_param.Get("JissekiCn").Value.AsString;
                            param.JissekiKo = m_param.Get("JissekiKo").Value.AsString;
                            param.JissekiTw = m_param.Get("JissekiTw").Value.AsString;
                            param.ReasonJp = m_param.Get("ReasonJp").Value.AsString;
                            param.ReasonEn = m_param.Get("ReasonEn").Value.AsString;
                            param.ReasonCn = m_param.Get("ReasonCn").Value.AsString;
                            param.ReasonKo = m_param.Get("ReasonKo").Value.AsString;
                            param.ReasonTw = m_param.Get("ReasonTw").Value.AsString;
                            endingMaster.param.Add(param);
                        }

                        Console.WriteLine($"EndingMaster: {endingMaster.param.Count}");
                        break;
                    case "EndingTextMaster":
                        endingTextMaster = new EndingTextMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new EndingTextMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.ParentID = m_param.Get("ParentID").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            endingTextMaster.param.Add(param);
                        }

                        Console.WriteLine($"EndingTextMaster: {endingTextMaster.param.Count}");
                        break;
                    case "EventTextMaster":
                        eventTextMaster = new EventTextMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new EventTextMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.EventId = m_param.Get("EventId").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.ArgumentType1 = m_param.Get("ArgumentType1").Value.AsString;
                            param.ArgumentType2 = m_param.Get("ArgumentType2").Value.AsString;
                            param.ArgumentType3 = m_param.Get("ArgumentType3").Value.AsString;
                            param.ArgumentType4 = m_param.Get("ArgumentType4").Value.AsString;
                            eventTextMaster.param.Add(param);
                        }

                        Console.WriteLine($"EventTextMaster: {eventTextMaster.param.Count}");
                        break;
                    case "KituneMaster":
                        kituneMaster = new KituneMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new KituneMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.FollowerRank = m_param.Get("FollowerRank").Value.AsString;
                            param.ResNumber = m_param.Get("ResNumber").Value.AsInt;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            kituneMaster.param.Add(param);
                        }

                        Console.WriteLine($"KituneMaster: {kituneMaster.param.Count}");
                        break;
                    case "KituneSuretaiMaster":
                        kituneSuretaiMaster = new KituneSuretaiMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new KituneSuretaiMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            kituneSuretaiMaster.param.Add(param);
                        }

                        Console.WriteLine($"KituneSuretaiMaster: {kituneSuretaiMaster.param.Count}");
                        break;
                    case "KRepMaster":
                        repMaster = new KRepMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new KRepMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.ParentID = m_param.Get("ParentID").Value.AsString;
                            param.IconId = m_param.Get("IconId").Value.AsString;
                            param.UserId = m_param.Get("UserId").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.ImageId = m_param.Get("ImageId").Value.AsString;
                            repMaster.param.Add(param);
                        }

                        Console.WriteLine($"KRepMaster: {repMaster.param.Count}");
                        break;
                    case "KusoCommentMaster":
                        kusoCommentMaster = new KusoCommentMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new KusoCommentMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.BodyJP = m_param.Get("BodyJP").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.goodbad = m_param.Get("goodbad").Value.AsString;
                            kusoCommentMaster.param.Add(param);
                        }

                        Console.WriteLine($"KusoCommentMaster: {kusoCommentMaster.param.Count}");
                        break;
                    case "LineMaster":
                        lineMaster = new LineMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new LineMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.ParentId = m_param.Get("ParentId").Value.AsString;
                            param.Speaker = m_param.Get("Speaker").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.ImageId = m_param.Get("ImageId").Value.AsString;
                            param.ArgumentType = m_param.Get("ArgumentType").Value.AsString;
                            lineMaster.param.Add(param);
                        }

                        Console.WriteLine($"LineMaster: {lineMaster.param.Count}");
                        break;
                    case "MobCommentMaster":
                        mobCommentMaster = new MobCommentMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new MobCommentMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Rank = m_param.Get("Rank").Value.AsInt;
                            param.BodyJP = m_param.Get("BodyJP").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCh = m_param.Get("BodyCh").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.timing = m_param.Get("timing").Value.AsString;
                            param.goodbad = m_param.Get("goodbad").Value.AsString;
                            mobCommentMaster.param.Add(param);
                        }

                        Console.WriteLine($"MobCommentMaster: {mobCommentMaster.param.Count}");
                        break;
                    case "MusicTitleMaster":
                        musicTitleMaster = new MusicTitleMaster();
                        foreach (var m_param in deserialized["_MusicTitleList"])
                        {
                            var param = new MusicTitleMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Index = m_param.Get("Index").Value.AsInt;
                            param.IsPlayable = m_param.Get("IsPlayable").Value.AsBool;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            musicTitleMaster.param.Add(param);
                        }

                        Console.WriteLine($"MusicTitleMaster: {musicTitleMaster.param.Count}");
                        break;
                    case "StatusLabelMaster":
                        statusLabelMaster = new StatusLabelMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new StatusLabelMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Body = m_param.Get("Body").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            statusLabelMaster.param.Add(param);
                        }

                        Console.WriteLine($"StatusLabelMaster: {statusLabelMaster.param.Count}");
                        break;
                    case "StatusTextMaster":
                        statusTextMaster = new StatusTextMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new StatusTextMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Body = m_param.Get("Body").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            statusTextMaster.param.Add(param);
                        }

                        Console.WriteLine($"StatusTextMaster: {statusTextMaster.param.Count}");
                        break;
                    case "SystemTextMaster":
                        systemTextMaster = new SystemTextMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new SystemTextMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            param.ArgumentType = m_param.Get("ArgumentType").Value.AsString;
                            param.ArgumentType2 = m_param.Get("ArgumentType2").Value.AsString;
                            systemTextMaster.param.Add(param);
                        }

                        Console.WriteLine($"SystemTextMaster: {systemTextMaster.param.Count}");
                        break;
                    case "TenCommentMaster":
                        tenCommentMaster = new TenCommentMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new TenCommentMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.BodyJP = m_param.Get("BodyJP").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            tenCommentMaster.param.Add(param);
                        }

                        Console.WriteLine($"TenCommentMaster: {tenCommentMaster.param.Count}");
                        break;
                    case "TooltipMaster":
                        tooltipMaster = new TooltipMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new TooltipMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Speaker = m_param.Get("Speaker").Value.AsString;
                            param.Summary = m_param.Get("Summary").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            tooltipMaster.param.Add(param);
                        }

                        Console.WriteLine($"TooltipMaster: {tooltipMaster.param.Count}");
                        break;
                    case "TweetMaster":
                        tweetMaster = new TweetMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new TweetMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.CommandID = m_param.Get("CommandID").Value.AsString;
                            param.Result = m_param.Get("Result").Value.AsString;
                            param.OmoteBodyJp = m_param.Get("OmoteBodyJp").Value.AsString;
                            param.OmoteBodyEn = m_param.Get("OmoteBodyEn").Value.AsString;
                            param.OmoteBodyCn = m_param.Get("OmoteBodyCn").Value.AsString;
                            param.OtomeBodyKo = m_param.Get("OtomeBodyKo").Value.AsString;
                            param.OmoteBodyTw = m_param.Get("OmoteBodyTw").Value.AsString;
                            param.OmoteImageId = m_param.Get("OmoteImageId").Value.AsString;
                            param.BuzzPowerFav = m_param.Get("BuzzPowerFav").Value.AsInt;
                            param.BuzzPowerRT = m_param.Get("BuzzPowerRT").Value.AsInt;
                            param.UraBodyJp = m_param.Get("UraBodyJp").Value.AsString;
                            param.UraBodyEn = m_param.Get("UraBodyEn").Value.AsString;
                            param.UraBodyCn = m_param.Get("UraBodyCn").Value.AsString;
                            param.UraBodyKo = m_param.Get("UraBodyKo").Value.AsString;
                            param.UraBodyTw = m_param.Get("UraBodyTw").Value.AsString;
                            param.UraImageId = m_param.Get("UraImageId").Value.AsString;
                            param.isNight = m_param.Get("isNight").Value.AsBool;
                            param.isDay = m_param.Get("isDay").Value.AsBool;
                            tweetMaster.param.Add(param);
                        }

                        Console.WriteLine($"TweetMaster: {tweetMaster.param.Count}");
                        break;
                    case "yakujoMaster":
                        yakujoMaster = new yakujoMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new yakujoMaster.Param();
                            param.Id = m_param.Get("Id").Value.AsString;
                            param.Type = m_param.Get("Type").Value.AsString;
                            param.Content = m_param.Get("Content").Value.AsString;
                            param.BodyJp = m_param.Get("BodyJp").Value.AsString;
                            param.BodyEn = m_param.Get("BodyEn").Value.AsString;
                            param.BodyCn = m_param.Get("BodyCn").Value.AsString;
                            param.BodyKo = m_param.Get("BodyKo").Value.AsString;
                            param.BodyTw = m_param.Get("BodyTw").Value.AsString;
                            yakujoMaster.param.Add(param);
                        }

                        Console.WriteLine($"yakujoMaster: {yakujoMaster.param.Count}");
                        break;
                    default:
                        continue;
                }
            }
        }
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);

        new Node("ActMaster.EN", new Po2Binary().Convert(new ActMaster2Po().Convert(actMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "ActMaster.EN.po"));

        var cmd = new CmdMaster2Po().Convert(cmdMaster.param.ToArray());
        new Node("CmdMaster.desc.EN", new Po2Binary().Convert(cmd.Item1)).Stream?.WriteTo(Path.Combine(outpath,
            "CmdMaster.desc.EN.po"));
        new Node("CmdMaster.label.EN", new Po2Binary().Convert(cmd.Item2)).Stream?.WriteTo(Path.Combine(outpath,
            "CmdMaster.label.EN.po"));

        new Node("EgosaMaster.EN", new Po2Binary().Convert(new EgosaMaster2Po().Convert(egosaMaster.param.ToArray())))
            .Stream
            ?.WriteTo(Path.Combine(outpath, "EgosaMaster.EN.po"));

        var endingm = new EndingMaster2Po().Convert(endingMaster.param.ToArray());
        new Node("EndingMaster.name.EN", new Po2Binary().Convert(endingm.Item1)).Stream?.WriteTo(Path.Combine(outpath,
            "EndingMaster.name.EN.po"));
        new Node("EndingMaster.isseki.EN", new Po2Binary().Convert(endingm.Item2)).Stream?.WriteTo(Path.Combine(outpath,
            "EndingMaster.isseki.EN.po"));
        new Node("EndingMaster.reason.EN", new Po2Binary().Convert(endingm.Item3)).Stream?.WriteTo(Path.Combine(outpath,
            "EndingMaster.reason.EN.po"));

        new Node("EndingTextMaster.EN",
                new Po2Binary().Convert(new EndingTextMaster2Po().Convert(endingTextMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "EndingTextMaster.EN.po"));

        new Node("EventTextMaster.EN",
                new Po2Binary().Convert(new EventTextMaster2Po().Convert(eventTextMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "EventTextMaster.EN.po"));

        new Node("KituneMaster.EN",
                new Po2Binary().Convert(new KituneMaster2po().Convert(kituneMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "KituneMaster.EN.po"));

        new Node("KituneSuretaiMaster.EN",
                new Po2Binary().Convert(new KituneSuretaiMaster2Po().Convert(kituneSuretaiMaster.param.ToArray())))
            .Stream
            ?.WriteTo(Path.Combine(outpath, "KituneSuretaiMaster.EN.po"));

        new Node("KRepMaster.EN", new Po2Binary().Convert(new KRepMaster2Po().Convert(repMaster.param.ToArray())))
            .Stream
            ?.WriteTo(Path.Combine(outpath, "KRepMaster.EN.po"));

        new Node("KusoCommentMaster.EN",
                new Po2Binary().Convert(new KusoCommentMaster2Po().Convert(kusoCommentMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "KusoCommentMaster.EN.po"));

        new Node("LineMaster.EN", new Po2Binary().Convert(new LineMaster2Po().Convert(lineMaster.param.ToArray())))
            .Stream
            ?.WriteTo(Path.Combine(outpath, "LineMaster.EN.po"));

        new Node("MobCommentMaster.EN",
                new Po2Binary().Convert(new MobCommentMaster2Po().Convert(mobCommentMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "MobCommentMaster.EN.po"));

        new Node("MusicTitleMaster.EN",
                new Po2Binary().Convert(new MusicTitleMaster2Po().Convert(musicTitleMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "MusicTitleMaster.EN.po"));

        new Node("StatusLabelMaster.EN",
                new Po2Binary().Convert(new StatusLabelMaster2Po().Convert(statusLabelMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "StatusLabelMaster.EN.po"));

        new Node("StatusTextMaster.EN",
                new Po2Binary().Convert(new StatusTextMaster2Po().Convert(statusTextMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "StatusTextMaster.EN.po"));

        new Node("SystemTextMaster.EN",
                new Po2Binary().Convert(new SystemTextMaster2Po().Convert(systemTextMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "SystemTextMaster.EN.po"));

        new Node("TenCommentMaster.EN",
                new Po2Binary().Convert(new TenCommentMaster2Po().Convert(tenCommentMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "TenCommentMaster.EN.po"));

        new Node("TooltipMaster.EN",
                new Po2Binary().Convert(new TooltipMaster2Po().Convert(tooltipMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "TooltipMaster.EN.po"));

        var tweet = new TweetMaster2Po().Convert(tweetMaster.param.ToArray());
        new Node("TweetMaster.omote.EN", new Po2Binary().Convert(tweet.Item1)).Stream?.WriteTo(Path.Combine(outpath,
            "TweetMaster.omote.EN.po"));
        new Node("TweetMaster.ura.EN", new Po2Binary().Convert(tweet.Item2)).Stream?.WriteTo(Path.Combine(outpath,
            "TweetMaster.ura.EN.po"));

        new Node("yakujoMaster.EN",
                new Po2Binary().Convert(new yakujoMaster2Po().Convert(yakujoMaster.param.ToArray()))).Stream
            ?.WriteTo(Path.Combine(outpath, "yakujoMaster.EN.po"));
    }

    #region Converters

    public class ActMaster2Po : IConverter<ActMaster.Param[], Po>
    {
        public Po Convert(ActMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - ActMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.TitleEn,
                    Context = entry.Id
                });

            return po;
        }
    }

    public class CmdMaster2Po : IConverter<CmdMaster.Param[], (Po, Po)>
    {
        public (Po, Po) Convert(CmdMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var podesc = new Po
            {
                Header = new PoHeader("Windose - CmdMaster.Desc", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            var polabel = new Po
            {
                Header = new PoHeader("Windose - CmdMaster.Label", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
            {
                podesc.Add(new PoEntry
                {
                    Original = entry.DescEn != string.Empty ? entry.DescEn : "{empty}",
                    Context = entry.ParentAct,
                    ExtractedComments = entry.ParentAct
                });

                polabel.Add(new PoEntry
                {
                    Original = entry.LabelEn != string.Empty ? entry.LabelEn : "{empty}",
                    Context = entry.TweetID,
                    ExtractedComments = entry.ParentAct
                });
            }

            return (podesc, polabel);
        }
    }

    public class EgosaMaster2Po : IConverter<EgosaMaster.Param[], Po>
    {
        public Po Convert(EgosaMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - EgosaMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.Type
                });

            return po;
        }
    }

    public class EndingMaster2Po : IConverter<EndingMaster.Param[], (Po, Po, Po)>
    {
        public (Po, Po, Po) Convert(EndingMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var poname = new Po
            {
                Header = new PoHeader("Windose - EndingMaster.Name", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            var pojisseki = new Po
            {
                Header = new PoHeader("Windose - EndingMaster.Jisseki", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            var pojreason = new Po
            {
                Header = new PoHeader("Windose - EndingMaster.Reason", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
            {
                poname.Add(new PoEntry
                {
                    Original = entry.EndingNameEn != string.Empty ? entry.EndingNameEn : "{empty}",
                    Context = entry.Id
                });
                pojisseki.Add(new PoEntry
                {
                    Original = entry.JissekiEn != string.Empty ? entry.JissekiEn : "{empty}",
                    Context = entry.Id
                });
                pojreason.Add(new PoEntry
                {
                    Original = entry.ReasonEn != string.Empty ? entry.ReasonEn : "{empty}",
                    Context = entry.Id
                });
            }

            return (poname, pojisseki, pojreason);
        }
    }

    public class EndingTextMaster2Po : IConverter<EndingTextMaster.Param[], Po>
    {
        public Po Convert(EndingTextMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - EndingTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.ParentID
                });

            return po;
        }
    }

    public class EventTextMaster2Po : IConverter<EventTextMaster.Param[], Po>
    {
        public Po Convert(EventTextMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - EventTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.EventId,
                    ExtractedComments = entry.ArgumentType1
                });

            return po;
        }
    }

    public class KituneMaster2po : IConverter<KituneMaster.Param[], Po>
    {
        public Po Convert(KituneMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - KituneMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn != string.Empty ? entry.BodyEn : "{empty}",
                    Context = entry.Id,
                    Reference = entry.FollowerRank,
                    ExtractedComments = $"{entry.ResNumber}"
                });

            return po;
        }
    }

    public class KituneSuretaiMaster2Po : IConverter<KituneSuretaiMaster.Param[], Po>
    {
        public Po Convert(KituneSuretaiMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - KituneSuretaiMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id
                });

            return po;
        }
    }

    public class KRepMaster2Po : IConverter<KRepMaster.Param[], Po>
    {
        public Po Convert(KRepMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - KRepMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.ParentID
                });

            return po;
        }
    }

    public class KusoCommentMaster2Po : IConverter<KusoCommentMaster.Param[], Po>
    {
        public Po Convert(KusoCommentMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - KusoCommentMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.goodbad
                });

            return po;
        }
    }

    public class LineMaster2Po : IConverter<LineMaster.Param[], Po>
    {
        public Po Convert(LineMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - LineMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.ParentId,
                    TranslatorComment = $"Speaker: {entry.Speaker}"
                });

            return po;
        }
    }

    public class MobCommentMaster2Po : IConverter<MobCommentMaster.Param[], Po>
    {
        public Po Convert(MobCommentMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - MobCommentMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.goodbad,
                    TranslatorComment = $"Rank: {entry.Rank}"
                });

            return po;
        }
    }

    public class MusicTitleMaster2Po : IConverter<MusicTitleMaster.Param[], Po>
    {
        public Po Convert(MusicTitleMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - MusicTitleMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    Reference = entry.FileName
                });

            return po;
        }
    }

    public class StatusLabelMaster2Po : IConverter<StatusLabelMaster.Param[], Po>
    {
        public Po Convert(StatusLabelMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - StatusLabelMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id
                });

            return po;
        }
    }

    public class StatusTextMaster2Po : IConverter<StatusTextMaster.Param[], Po>
    {
        public Po Convert(StatusTextMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - StatusTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id
                });

            return po;
        }
    }

    public class SystemTextMaster2Po : IConverter<SystemTextMaster.Param[], Po>
    {
        public Po Convert(SystemTextMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - SystemTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn != string.Empty ? entry.BodyEn : "{empty}",
                    Context = entry.Id,
                    ExtractedComments = entry.ArgumentType
                });

            return po;
        }
    }

    public class TenCommentMaster2Po : IConverter<TenCommentMaster.Param[], Po>
    {
        public Po Convert(TenCommentMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - SystemTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn != string.Empty ? entry.BodyEn : "{empty}",
                    Context = entry.Id
                });

            return po;
        }
    }

    public class TooltipMaster2Po : IConverter<TooltipMaster.Param[], Po>
    {
        public Po Convert(TooltipMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - SystemTextMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    TranslatorComment = $"Speaker: {entry.Speaker}\nSummary: {entry.Summary}"
                });

            return po;
        }
    }

    public class TweetMaster2Po : IConverter<TweetMaster.Param[], (Po, Po)>
    {
        public (Po, Po) Convert(TweetMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var poomote = new Po
            {
                Header = new PoHeader("Windose - TweetMaster.Omote", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            var poUra = new Po
            {
                Header = new PoHeader("Windose - TweetMaster.Ura", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
            {
                poomote.Add(new PoEntry
                {
                    Original = entry.OmoteBodyEn != string.Empty ? entry.OmoteBodyEn : "{empty}",
                    Context = entry.Id,
                    Reference = entry.CommandID,
                    TranslatorComment = entry.Result
                });

                poUra.Add(new PoEntry
                {
                    Original = entry.UraBodyEn != string.Empty ? entry.UraBodyEn : "{empty}",
                    Context = entry.Id,
                    Reference = entry.CommandID,
                    TranslatorComment = entry.Result
                });
            }


            return (poomote, poUra);
        }
    }

    public class yakujoMaster2Po : IConverter<yakujoMaster.Param[], Po>
    {
        public Po Convert(yakujoMaster.Param[] source)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var po = new Po
            {
                Header = new PoHeader("Windose - yakujoMaster", "d3fau4@not-d3fau4.com", currentCulture.Name)
                {
                    LanguageTeam = "Any"
                }
            };

            foreach (var entry in source)
                po.Add(new PoEntry
                {
                    Original = entry.BodyEn,
                    Context = entry.Id,
                    TranslatorComment = $"Type: {entry.Type}\nContent: {entry.Content}"
                });

            return po;
        }
    }

    #endregion
}