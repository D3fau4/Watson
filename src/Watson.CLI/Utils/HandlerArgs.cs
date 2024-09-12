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
        Psync2,
        CocoDrilo,
        Unity3D
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
            new( new[] {  "--coco" }, () => OperationMode = Mode.CocoDrilo),
            new( new[] {  "--unity" }, () => OperationMode = Mode.Unity3D),

            // Args
            new(new[] { "--gamepath" }, x => GamePath = x),
            new(new[] { "--output", "-o" }, x => OutPut = x),
            new(new[] { "--extract", "-x" }, () => extract = true),
            new(new[] { "--import", "-i" }, x => PoPath = x),
            new(new[] { "--file", "-f" }, x => filePath = x)
        };

        for (var i = 0; i < raw_args.Length; ++i)
        {
            var handler = Handlers.FirstOrDefault(x => x.Aliases.Contains(raw_args[i]));

            if (handler is null)
                Console.WriteLine($"Warning: unknown arg {raw_args[i]}");
            else
                handler.Invoke(handler.RequiresArg ? raw_args[++i] : null!);
        }

        if (import && string.IsNullOrEmpty(PoPath))
            throw new Exception("Import requires a Po file path");
    }

    public Mode? OperationMode { get; private set; }
    public string? GamePath { get; private set; }
    public string? filePath { get; private set; }
    public string? OutPut { get; private set; } = "out";
    public string? PoPath { get; private set; }
    public bool extract { get; private set; }
    public bool import { get; private set; }

    public static void PrintInfo()
    {
        AnsiConsole.Markup("[purple]Watson.CLI.exe[/] [red](MODE)[/] [yellow](OPTIONS)[/]");
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
