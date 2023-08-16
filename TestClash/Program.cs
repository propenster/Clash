using Clash;
using TestClash;

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        var parser = new Parser();
        while(true)
        {
            var cli = (Cli) parser.Parse<Cli>(args);

            if(cli != null)
            {
                ProcessCliArgs(cli);
            }

        }


        //var helpString = parser.HelpString(cli);

        //Console.WriteLine("CLI parsed Values >>> {0}", cli.ToString());

        //Console.WriteLine(helpString);

    }

    private static void ProcessCliArgs(Cli cli)
    {
        //let's use 
        Console.WriteLine("Name >>> {0} Count >>> {1} IPAddress >>> {2}", cli.Name, cli.Count, cli.IpAddress);
    }
}