﻿namespace Watson.Lib.IO;

using Yarhl.FileFormat;
using Yarhl.Media.Text;

public class ArrayString2Po : IConverter<string[], Po>
{
    private string preContext { get; set; }
    private string Name { get; set; }

        public ArrayString2Po(string Name, string preContext = "")
        {
                this.Name = Name;
                this.preContext = preContext;

                if (preContext == string.Empty)
                    preContext = "NoPreContext";
        }

    public Po Convert(string[] source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader("Watson", "d3fau4@not-d3fau4.com", currentCulture.Name)
            {
                LanguageTeam = "Any"
            }
        };

        for (int i = 0; i < source.Length; i++) {
            var txt = (source[i] ?? string.Empty).Replace(string.Empty, "{EMPTY}");
            po.Add(new PoEntry
            {
                Original = txt,
                Context = $"{Name}.{preContext}.{i}"
            });
        }

        return po;
    }
}
