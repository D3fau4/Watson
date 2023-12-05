using Spectre.Console;

namespace Watson.Program.Utils;

public class HandlerArgs
{
    // Modes of operation
    public enum Mode
    {
        Help,
        HoloError,
        Windose,
        SVS,
        Psync2
    }

    public HandlerArgs(string[] raw_args)
    {
        Handler[] Handlers =
        {
            // OP modes
            new( new[] { "--help", "-h" }, () => OperationMode = Mode.Help),
            new( new[] { "--holoerror" }, () => OperationMode = Mode.HoloError),
            new( new[] { "--windose" }, () => OperationMode = Mode.Windose),
            new( new[] {  "--NeptuniaSVS", "-svs" }, () => OperationMode = Mode.SVS),
            new( new[] {  "--AI2", "-ai2" }, () => OperationMode = Mode.Psync2),

            // Args
            new(new[] { "--gamepath" }, x => GamePath = x),
            new(new[] { "--output", "-o" }, x => OutPut = x),
            new(new[] { "--extract", "-x" }, () => extract = true)
        };

        for (var i = 0; i < raw_args.Length; ++i)
        {
            var handler = Handlers.FirstOrDefault(x => x.Aliases.Contains(raw_args[i]));

            if (handler is null)
                Console.WriteLine($"Warning: unknown arg {raw_args[i]}");
            else
                handler.Invoke(handler.RequiresArg ? raw_args[++i] : null!);
        }
    }

    public Mode? OperationMode { get; private set; }
    public string? GamePath { get; private set; }
    public string? OutPut { get; private set; } = "out";
    public bool extract { get; private set; }

    public static void PrintInfo()
    {
        AnsiConsole.Markup("[purple]Watson.CLI.exe[/] [red](MODE)[/] [yellow](OPTIONS)[/]");
        /*cmdutils.print("Operation Modes:");
        cmdutils.print("    --Hololy                        Download and decrypt Hololy models.");
        cmdutils.print("    --HoloEarth                     Download HoloEarths.");
        cmdutils.print("    --HololiveError                 Decrypt HololiveError bundles.");
        cmdutils.print("General options: ");
        cmdutils.print("    --output, -o                    Set the output folder.");
        cmdutils.print("Hololive Error options:");
        cmdutils.print("    --input, -i                     Set the input file to decrypt.");
        cmdutils.print("    --hash                          Set the hash of the bundle");
        cmdutils.print("Hololy options:");
        cmdutils.print("    --dev                           Set the dev server");
        cmdutils.print("    --list, -l                      List the models");
        cmdutils.print("    --download, -d                  Download the models");
        cmdutils.print("    --model                         Download the model by name");*/
    }

    private class Handler
    {
        public readonly string[] Aliases;
        private readonly Action? Fn;

        private readonly Action<string>? FnArg;

        public Handler(string[] Aliases, Action<string> Fn)
        {
            this.Aliases = Aliases;
            FnArg = Fn;
        }

        public Handler(string[] Aliases, Action Fn)
        {
            this.Aliases = Aliases;
            this.Fn = Fn;
        }

        public bool RequiresArg => FnArg != null;

        public void Invoke(string arg)
        {
            if (FnArg != null)
                FnArg(arg);
            else
                Fn!();
        }
    }
}