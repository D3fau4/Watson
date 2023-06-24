using Spectre.Console;
using Watson.Program.Utils;

AnsiConsole.Markup("[purple]Welcome to Watson![/] - [yellow]v1.0.0[/]\n");
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
    case HandlerArgs.Mode.Help:
    default:
        HandlerArgs.PrintInfo();
        break;
}