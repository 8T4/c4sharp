using System.CommandLine.Parsing;

namespace C4Sharp.Tools.Commands.Options;

public static class OutputPathOption
{
    public static Option Get()
    {
        const string description =
            "The output path, if not informed it will search for the first sln file into current directory";

        var option = new Option<string?>(new[] { "-o", "--output" }, description);
        option.SetDefaultValue(null);

        option.AddValidator(ValidateSymbol);

        return option;
    }

    private static string? ValidateSymbol(OptionResult opt)
    {
        try
        {
            var path = opt.GetValueOrDefault<string?>();

            if (path is null)
            {
                return null;
            }

            return path switch
            {
                "." => null,
                _ when !Directory.Exists(path) => $"The specified output path does not exists {path}",
                _ => null
            };
        }
        catch(Exception e)
        {
            return $"The specification of output path has fail:\r\n {e.Message}";
        }
    }
}