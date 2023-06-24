using MessagePack;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Assets;

public class FolderManager
{
    public static string saveDataFolder = "Data/SaveData";
    public static string scriptsFolder = "Assets/.Scripts";
    public static string scenesFolder = "Assets/Asset/scenes";
    public static string cutsceneFolder = "Assets/Asset/scenes/cutscene";
    public static string audioFolder = "Assets/Audio";
    public static string motionFolder = "Assets/Asset/graphics/3d/chara/motion";
    public static string nameFolder = "Assets/Asset/graphics/2d/Image/name";
    public static string imageFolder = "Assets/Asset/graphics/2d/Image";
    public static string clueIconFolder = "Assets/Asset/graphics/2d/Image/clue_icon";
    public static string infomationIconFolder = "Assets/Asset/graphics/2d/Image/infomation_icon";
    public static string locationFolder = "Assets/Asset/graphics/2d/Image/location";
    public static string guideFolder = "Assets/Asset/graphics/2d/Image/operation_guide";
    public static string bustshotFolder = "Assets/Asset/graphics/3d/chara/faceWindow/Prefabs";
    public static string charaFolder = "Assets/Asset/graphics/3d/chara/_variant";
    public static string itemFolder = "Assets/Asset/graphics/3d/item/psync2/model/Variant";
    public static string costumeFolder = "Assets/Asset/graphics/3d/item/psync2/md_cos/Variant";
    public static string prefabFolder = "Assets/Novel/Prefabs";
    public static string mapImageFolder = "Assets/Asset/graphics/2d/Image/map_image";
    public static string summaryFolder = "Assets/Asset/graphics/2d/Image/synopsis_image";
    public static string glyphFolder = "Assets/Fonts/text";
    public static string fontFolder = "Assets/Fonts/Build";
    public static string videoFolder = "Assets/Video";
    public static string inputCaptureFolder = "Data/InputCapture";
    public static string shaderVariantsFolder = "Assets/Data/ShaderVariants";
    public static string buildNo;
    public static string folderData;
    public static string textData;

    public FolderManager(string datafolder)
    {
        buildNo = datafolder + "/AutoGen/buildNo.txt";
        folderData = datafolder + "/AutoGen/folder.bytes";
        textData = datafolder + "/Text";
    }

    public static void MakeData()
    {
        var value = new Data();
        var text = scriptsFolder;
        var text2 = audioFolder;
        var text3 = scenesFolder;
        var text4 = cutsceneFolder;
        var directoryName = Path.GetDirectoryName(folderData);
        if (!Directory.Exists(directoryName))
        {
            var directoryInfo = Directory.CreateDirectory(directoryName);
        }

        /*byte[] bytes = SaveDataHelper.Serialize(value);
        File.WriteAllBytes(folderData, bytes);*/
    }

    [MessagePackObject]
    public class Data
    {
        public Dictionary<string, Folder> folder = new();
    }

    [MessagePackObject]
    public class Folder
    {
        public List<string> directorie = new();
        public List<string> files = new();
    }
}