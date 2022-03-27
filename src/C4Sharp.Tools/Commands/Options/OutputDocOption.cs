using System.CommandLine.Parsing;

namespace C4Sharp.Tools.Commands.Options;

public static class OutputDocOption
{
    public static Option Get()
    {
        const string description = "set 'html' to output a document as html page";

        var option = new Option<string?>(new[] { "--doc", "-d" }, () => "html", description);
        option.SetDefaultValue("html");
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
                "html" => null,
                "md" => null,
                _ => $"inválid format {path}",
            };
        }
        catch
        {
            return $"An error was raised when try get option argument (--doc). Verify if the option value was typed correctly!";
        }
    }
}