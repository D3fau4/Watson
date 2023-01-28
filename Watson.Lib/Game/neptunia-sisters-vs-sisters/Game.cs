using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Watson.Lib.Assets;
using Watson.Lib.Game.neptunia_sisters_vs_sisters.Texts;
using Watson.Lib.IO;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.neptunia_sisters_vs_sisters;

public class Game : IGame
{
    public static readonly string gamename = "neptunia-sisters-vs-sisters";
    public static readonly string CSV_REGEX = "event_.*en_assets_all.*$";

    public Game(string gamepath)
    {
        this.gamepath = gamepath;
        Load();
    }

    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private List<string> csvassets { get; } = new();
    public Dictionary<string, CSV[]> csvfiles { get; } = new();

    public void Load()
    {
        gamedatapath = Path.Combine(gamepath, $"{gamename}_Data");

        foreach (var file in Directory.GetFiles(gamedatapath, "*.*", SearchOption.AllDirectories)
                     .Where(file => Regex.IsMatch(file, CSV_REGEX)))
            csvassets.Add(file);
    }

    public void Proccess()
    {
        foreach (var file in csvassets)
        {
            var text = new TextAsset(new UnityAssetFile(file, gamedatapath));
            text.Load();
            foreach (var csv in text.m_TextsAssets)
            {
                var csvtxt = csv.Value.Item2["m_Script"].AsString;

                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Encoding = Encoding.UTF8,
                    Delimiter = ";",
                    MissingFieldFound = null,
                    // Todo: Comprobar que es este bad data...
                    BadDataFound = null
                };
                List<CSV> csvs = new();
                using (var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(csvtxt))))
                using (var csvr = new CsvReader(reader, configuration))
                {
                    var records = csvr.GetRecords<CSV>();

                    foreach (var entry in records)
                        if (entry.Header.Equals("eTALK_SET_ALL"))
                        {
                            csvs.Add(entry);
                            break;
                        }
                }
                csvfiles.Add(csv.Value.Item2["m_Name"].AsString, csvs.ToArray());
            }
        }
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        throw new NotImplementedException();
    }
}

public class CSV2Po : IConverter<(string, CSV[]), Po>
{
    public Po Convert((string, CSV[]) source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader($"Neptunia: Sisters VS Sisters - {source.Item1}", "d3fau4@not-d3fau4.com", currentCulture.Name)
            {
                LanguageTeam = "Any"
            }
        };

        foreach (var csv in source.Item2)
        {
            po.Add(new PoEntry
            {
                Original = csv.message_en,
                Context = csv.unk_3,
                TranslatorComment = $"Speaker: {csv.talkername_en}\n"
            });
        }
        
        return po;
    }
}