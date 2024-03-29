﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using Spectre.Console;
using Watson.Lib.Assets;
using Watson.Lib.Game.neptunia_sisters_vs_sisters.Assets;
using Watson.Lib.Game.neptunia_sisters_vs_sisters.Texts;
using Watson.Lib.IO;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.neptunia_sisters_vs_sisters;

public class Game : IGame
{
    public static readonly string gamename = "neptunia-sisters-vs-sisters";
    public static readonly string CSV_REGEX = "en_assets_all.*$";

    public Game(string gamepath, ProgressContext ctx = null)
    {
        this.gamepath = gamepath;
        this.ctx = ctx;
        Load();
    }

    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private List<string> csvassets { get; } = new();
    public Dictionary<string, CSV[]> csvfiles { get; } = new();
    public Dictionary<string, DbStringMake[]> dbstrings { get; } = new();
    private ProgressContext ctx { get; set; }

    public void Load()
    {
        AnsiConsole.Markup("[yellow]Neptunia: Sisters VS Sisters mode![/]\n");
        var task = ctx?.AddTask("[green]Searching assets[/]");
        gamedatapath = Path.Combine(gamepath, $"{gamename}_Data");
        
        foreach (var files in Directory.GetFiles(gamedatapath, "*.*", SearchOption.AllDirectories)
                     .Where(file => Regex.IsMatch(file, CSV_REGEX)))
            csvassets.Add(files);
        task?.Increment(100);
    }

    public void Proccess()
    {
        
        var task = ctx?.AddTask("[green]Processing assets[/]");
        float proInc = 100.0f / (csvassets.Count -1);
        foreach (var file in csvassets)
        {
            var am = new UnityAssetFile(file, gamedatapath);
            
            // CSV
            var text = new TextAsset(am);
            text.Load();
            foreach (var csv in text.m_TextsAssets)
            {
                var csvtxt = csv.Value.Item2["m_Script"].AsString;

                //File.WriteAllText($"csv/{csv.Value.Item2["m_Name"].AsString}.csv", csvtxt);

                List<CSV> csvs = new();
                List<string> baddata = new();
                var isRecordBad = false;

                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Encoding = Encoding.UTF8,
                    Delimiter = ";",
                    MissingFieldFound = null,
                    BadDataFound = context =>
                    {
                        isRecordBad = true;
                        baddata.Add(context.RawRecord);
                    }
                };

                using (var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(csvtxt))))
                using (var csvr = new CsvReader(reader, configuration))
                {
                    while (csvr.Read())
                    {
                        var records = csvr.GetRecords<CSV>();

                        foreach (var entry in records)
                            if (entry.Header.Equals("eTALK_SET_ALL"))
                            {
                                csvs.Add(entry);
                            }
                            else if (entry.Header.Equals("WIPE_TALK"))
                            {
                                // TODO: hacer mejor esto que es un truño
                                entry.talkername_en = entry.talkername_ko;
                                entry.message_en = entry.message_ko;
                                csvs.Add(entry);
                            }
                    }
                }

                if (baddata.Count > 0)
                    foreach (var bad in baddata)
                    {
                        var entrys = bad.Split(";");
                        var csventry = new CSV();
                        csventry.Header = entrys[0];
                        csventry.unk_1 = entrys[1];
                        csventry.unk_2 = entrys[2];
                        csventry.unk_3 = entrys[3];
                        csventry.talkername_jp = entrys[4];
                        csventry.talkername_en = entrys[5];
                        csventry.talkername_cn = entrys[6];
                        csventry.talkername_cn2 = entrys[7];
                        csventry.talkername_ko = entrys[8];
                        csventry.message_jp = entrys[9];
                        csventry.message_en = entrys[10];
                        csventry.message_cn = entrys[11];
                        csventry.message_cn2 = entrys[12];
                        csventry.message_ko = entrys[13];
                        csventry.unk_4 = entrys[14];
                        csvs.Add(csventry);
                    }
                
                var arr = csvs.ToArray();
                if (arr.Length <= 0)
                    continue;
                    csvfiles.Add(csv.Value.Item2["m_Name"].AsString, arr);
            }
            
            // String DB
                
            var dbobject = new DbStringObject(am);
            dbobject.Load();

            foreach (var dbobjets in dbobject.m_TextsAssets)
            {
                List<DbStringMake> list = new List<DbStringMake>();
                for (int i = 0; i < dbobjets.Value.Item2.Children.Count; i++)
                {
                    if (dbobjets.Value.Item2.Children[i].TypeName.Equals("DbStringMake"))
                    {
                        foreach (var test in dbobjets.Value.Item2.Children[i].Children[0].Children)
                        {
                            var s = new DbStringMake();
                            s.nameOld_ = test["nameOld_"].AsString;
                            s.id_ = test["id_"].AsUInt;
                            s.jpText_ = test["jpText_"].AsString;
                            s.enText_ = test["enText_"].AsString;
                            s.chText_ = test["chText_"].AsString;
                            s.chs_Text_ = test["chs_text_"].AsString;
                            s.krText_ = test["krText_"].AsString;
                            s.tag_ = test["tag_"].AsString;
                            s.extend_ = new DbExtendString(test["extend_"]["Comment_"].AsString);
                            
                            list.Add(s);
                        }
                    }
                }
                
                var arr = list.ToArray();
                if (arr.Length <= 0)
                    continue;
                dbstrings.Add(dbobjets.Value.Item2["m_Name"].AsString, arr);
            }
            
            task?.Increment(proInc);
        }
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        var task = ctx?.AddTask("[green]Converting to po[/]");
        
        float proInc = 100.0f / (csvfiles.Count + dbstrings.Count);
        
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);

        var currentCulture = Thread.CurrentThread.CurrentCulture;
        foreach (var entrys in csvfiles)
        {
            new Node($"{entrys.Key}.{currentCulture}",
                    new Po2Binary().Convert(new CSV2Po().Convert((entrys.Key, entrys.Value)))).Stream
                ?.WriteTo(Path.Combine(outpath, $"{entrys.Key}_{currentCulture.Name}.po"));
            task?.Increment(proInc);
        }

        foreach (var entrys in dbstrings)
        {
            new Node($"{entrys.Key}.{currentCulture}",
                    new Po2Binary().Convert(new DbStringMake2Po().Convert((entrys.Key, entrys.Value)))).Stream
                ?.WriteTo(Path.Combine(outpath, $"{entrys.Key}_{currentCulture.Name}.po"));
            task?.Increment(proInc);
        }
    }
}

public class CSV2Po : IConverter<(string, CSV[]), Po>
{
    public Po Convert((string, CSV[]) source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader($"Neptunia: Sisters VS Sisters - {source.Item1}", "d3fau4@not-d3fau4.com",
                currentCulture.Name)
            {
                LanguageTeam = "Any"
            }
        };

        foreach (var csv in source.Item2)
        {
            if (csv.message_en.Equals(string.Empty))
                continue;

            po.Add(new PoEntry
            {
                Original = csv.message_en,
                Context = $"{csv.talkername_en}.{csv.unk_3}",
                TranslatorComment = $"Speaker: {csv.talkername_en}\n"
            });
        }

        return po;
    }
}

public class DbStringMake2Po : IConverter<(string, DbStringMake[]), Po>
{
    public Po Convert((string, DbStringMake[]) source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader($"Neptunia: Sisters VS Sisters - {source.Item1}", "d3fau4@not-d3fau4.com",
                currentCulture.Name)
            {
                LanguageTeam = "Any"
            }
        };

        foreach (var db in source.Item2)
        {
            if (db.enText_.Equals(string.Empty))
                continue;

            po.Add(new PoEntry
            {
                Original = db.enText_,
                Context = $"{db.id_}",
                TranslatorComment = $"{db.extend_.Comment_}\n"
            });
        }

        return po;
    }
}