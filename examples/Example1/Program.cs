

using Clash;
using Newtonsoft.Json;

[Command(About = "This is a simple JSON File Parser CLI Application for Artisans", Author = "MyNameIs0xFFFF", Version = "1.0.0")]
internal class JsonFileParser
{
    //you expect them to pass this arg as -f "C:\temp\myfile.json" or longForm --filePath "C:\temp\myfile.json"
    [Arg(Short = "f", Long = "filePath", Required = true, Description = "This is the path of the file we want to parse")]
    public string InputFilePath { get; set; } = string.Empty;
}


internal class Program
{
    private static void Main(string[] args)
    {
        var parser = new Parser();//using Clash;
        Console.WriteLine("Please pass a valid JSON file path in the args");

        if (parser.TryParse<JsonFileParser>(args, out var cliResult)) // You are just telling clash to parse what comes from args into the JsonFileParser object...
        {
            Console.WriteLine("File Path successfully received from the CMD >>> {0}", cliResult.InputFilePath);

            var fileContentString = string.Empty;
            try
            {
                fileContentString = File.ReadAllText(cliResult.InputFilePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Please pass a valid filePath argument >>> {0}", ex.Message);

                return;
            }

            var parsedObjects = JsonConvert.DeserializeObject(fileContentString);

            Console.WriteLine(parsedObjects?.ToString());

        }


    }
}