using System.CommandLine.Parsing;

namespace C4Sharp.Tools.Commands.Arguments;

internal static class  SolutionPathArgument
{
    public static Argument Get(string argumentName)
    {
        const string description =
            "The solution file path, if not informed it will search for the first sln file into current directory";

        var argument = new Argument<string>(argumentName, description);
        argument.SetDefaultValue(".");

        argument.AddValidator(opt =>
        {
            var path = opt.GetValueOrDefault<string>();

            return path switch
            {
                "." => null,
                _ when !File.Exists(path) => $"The specified solution file does not exists {path}",
                _ when Path.GetExtension(path) != ".sln" => "This file is not a solution file",
                _ => null
            };
        });

        return argument;
    }
}