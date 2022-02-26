using System.CommandLine.Parsing;

namespace C4Sharp.Tools.Commands.Options;

public static class OutputDocOption
{
    public static Option Get()
    {
        const string description = "set 'html' or 'md' to output a document as html page or markdown file";

        var option =  new Option<string?>(new[] { "--doc", "-d" }, description);
        option.SetDefaultValue("html");
        
        option.AddValidator(opt =>
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
        });
        
        return option;        
    }    
}