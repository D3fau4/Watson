using AssetsTools.NET.Extra;
using Spectre.Console;
using Watson.Lib.Game.AI_TheSomniumFiles2.Enums;
using Watson.Lib.IO;
using Watson.Program.Utils;

AnsiConsole.Markup("[purple]Welcome to Watson![/] - [yellow]v1.1.0[/]\n");
var arg = new HandlerArgs(args);

switch (arg.OperationMode)
{
    case HandlerArgs.Mode.SVS:
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                var svs = new Watson.Lib.Game.neptunia_sisters_vs_sisters.Game(arg.GamePath, ctx);
                svs.Proccess();

                // Extract to Po
                if (arg.extract)
                {
                    svs.Export(arg.OutPut);
                }
            });
        break;
    case HandlerArgs.Mode.Psync2:
        AnsiConsole.MarkupLine("[green]Juego - AI: The Somnium Files - Nirvana Initiative[/]");
        AnsiConsole.Status()
            .AutoRefresh(true)
            .Start("Iniciando...", ctx =>
            {
                // Simulate some work
                ctx.Status("Leyendo carpeta del juego...");
                ctx.Spinner(Spinner.Known.Circle);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                var psync2 = new Watson.Lib.Game.AI_TheSomniumFiles2.Game(arg.GamePath, LanguageType.en, ctx);


                // Update the status and spinner
                ctx.Status("Procesando Archivos...");
                psync2.Load();

                // Simulate some work
                psync2.Proccess();

                if (arg.extract)
                {
                    psync2.Export(arg.OutPut);
                }
            });
        break;
    case HandlerArgs.Mode.Unity3D:
        AnsiConsole.MarkupLine("[green]Modo - Unity3D[/]");
        AnsiConsole.Status()
            .AutoRefresh(true)
            .Start("Iniciando...", ctx =>
            {
                ctx.Status("Leyendo archivo...");
                ctx.Spinner(Spinner.Known.Circle);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                UnityAssetFile.LoadAndExtractUnity3D(arg.filePath);
            });
        break;
    case HandlerArgs.Mode.CocoDrilo:
        AnsiConsole.MarkupLine("[green]Juego - Later Alligator[/]");
        AnsiConsole.Status()
            .AutoRefresh(true)
            .Start("Iniciando...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Circle);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                var cocodrilo = new Watson.Lib.Game.LaterAlligator.Game(arg.GamePath, ctx);

                if (arg.extract)
                {
                    cocodrilo.Proccess();
                    cocodrilo.Export(arg.OutPut);
                }

                cocodrilo.Import(arg.PoPath);
            });
        break;
    case HandlerArgs.Mode.Help:
    default:
        HandlerArgs.PrintInfo();
        break;
}
