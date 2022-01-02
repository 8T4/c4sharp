using System.CommandLine.Parsing;

namespace C4Sharp.Tools.Commands.Options;

public class OutputPathOption
{
    public static Option Get()
    {
        const string description =
            "The output path, if not informed it will search for the first sln file into current directory";

        var option = new Option<string?>(new[] { "-o", "--output" }, description);
        option.SetDefaultValue(null);

        option.AddValidator(opt =>
        {
            var path = opt.GetValueOrDefault<string?>();

            if (path is null)
                return null;

            return path switch
            {
                "." => null,
                _ when !Directory.Exists(path) => $"The specified output path does not exists {path}",
                _ => null
            };
        });

        return option;
    }    
}