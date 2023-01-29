namespace Watson.Lib.Game.neptunia_sisters_vs_sisters.Texts;

public class DbStringMake
{
    public string nameOld_ { get; set; }
    public uint id_ { get; set; }
    public string jpText_ { get; set; }
    public string enText_ { get; set; }
    public string chText_ { get; set; }
    public string chs_Text_ { get; set; }
    public string krText_ { get; set; }
    public string tag_ { get; set; }
    public DbExtendString extend_ { get; set; }
}

public class DbExtendString
{
    public DbExtendString(string comment)
    {
        Comment_ = comment;
    }

    public string Comment_ { get; set; }
}