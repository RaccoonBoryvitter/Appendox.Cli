using Appendox.Cli.Commands;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(cfg =>
{
    cfg.AddCommand<MainCommand>("run")
        .WithDescription(
            "Executes a main flow of the tool to generate media file from source code."
        );
});

return await app.RunAsync(args);
