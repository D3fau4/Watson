using AssetsTools.NET.Extra;
using Watson.Lib.Game.Windose.Texts;
using Watson.Lib.IO;

namespace Watson.Lib.Game.Windose;

public class Game : IGame
{
    private UnityAssetFile m_assetfile;

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
                        var actMaster = new ActMaster();
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
                        var cmdMaster = new CmdMaster();
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
                            param.YamiDelta = m_param.Get("YamiDelta").Value.AsInt;
                            param.FavorDelta = m_param.Get("FavorDelta").Value.AsInt;
                            param.OkusuriCount = m_param.Get("OkusuriCount").Value.AsInt;
                            param.OirokeCount = m_param.Get("OirokeCount").Value.AsInt;
                            param.SNS = m_param.Get("GameCount").Value.AsInt;
                            param.SNS = m_param.Get("CinePhillCount").Value.AsInt;
                            param.SNS = m_param.Get("ShabekuriCount").Value.AsInt;
                            param.SNS = m_param.Get("DatespotCount").Value.AsInt;
                            param.SNS = m_param.Get("Harumagedo").Value.AsInt;
                            cmdMaster.param.Add(param);
                        }

                        Console.WriteLine($"CmdMaster: {cmdMaster.param.Count}");
                        break;
                    case "EgosaMaster":
                        var egosaMaster = new EgosaMaster();
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
                        var endingMaster = new EndingMaster();
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
                        var endingTextMaster = new EndingTextMaster();
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
                        var eventTextMaster = new EventTextMaster();
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
                        var kituneMaster = new KituneMaster();
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
                        var kituneSuretaiMaster = new KituneSuretaiMaster();
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
                    case "KRep":
                        var repMaster = new KRepMaster();
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
                        var kusoCommentMaster = new KusoCommentMaster();
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
                        var lineMaster = new LineMaster();
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
                        MobCommentMaster mobCommentMaster = new MobCommentMaster();
                        foreach (var m_param in deserialized["param"])
                        {
                            var param = new MobCommentMaster.Param();
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
                        break;
                    default:
                        continue;
                }
            }
        }

        throw new NotImplementedException();
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export()
    {
        throw new NotImplementedException();
    }
}