![invalid_args_clash](https://github.com/propenster/Clash/assets/51266654/7650caf7-935b-4dfd-aace-b60faaec90fe)# Clash
A simple to use, efficient, and full-featured Command Line Argument Parser library for C# .NET

### What is Clash?
Clash is a simple to use, efficient, and full-featured Command Line Argument Parser library for C# .NET. 
You can use Clash in your CLI applications to parse CommandLine Arguments to C# objects.

### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Clash](https://www.nuget.org/packages/Clash/) from the package manager console:

```
PM> Install-Package Clash
```
Or from the .NET CLI as:
```
dotnet add package Clash
```

### How do I get started?

First, 
*  create a class that you would like to use to hold the commandline args like below and annotate it with the CommandAttribute:
*  You can configure About, Author, Version and LongAbout for your Cli App using the CommandAttribute
*  Annotate each field you would like to receive from CMD with the ArgAttribute

### Please refer to the snippet below:

```csharp
[Command(About = "A simple to use, efficient, and full-featured Command Line Argument Parser", Author = "propenster", Version = "1.0.0.0")]
    public class Cli
    {
        [Arg(Short = "n", Long = "name", Required = true, Description = "Name of the person to greet", DefaultValue = "John Doe")]
        public string Name { get; set; }
        [Arg(Short = "c", Long = "count", Required = true, Description = "How many times are we trying to greet them for?", DefaultValue = 20)]
        public int Count { get; set; }
        [Arg(Short = "i", Long = "ipAddress", Description = "IP Address", DefaultValue = "127.0.0.1")]
        public string IpAddress { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Name, Count, IpAddress);
        }
    }

```

* Then parse CLI args anywhere in your application like below.

```csharp
  using Clash;

  var parser = new Parser();
  var cli = parser.Parse<Cli>(args);
```

* You can now do anything with the cli object. All the fields (args) will have been filled with values from the cmd args. If there is an error or invalid arguments, an error response will be sent the STDOUT buffer
*  See below
```csharp
  ProcessCliArgs(cli);
```

## See images below for some better context

![invali![test_clash](https://github.com/propenster/Clash/assets/51266654/acf24023-21f5-4011-be11-14d115097e66)


![invalid_args_clash](https://github.com/propenster/Clash/assets/51266654/fd37e9fe-e80a-4c2e-904e-2db5abc24388)

### Do you have an issue?

If you're facing some problems using the package, file an issue above.

### License, etc.
FakerLib.Net is Copyright &copy; 2023 [Faith Olusegun](https://github.com/propenster) and other contributors under the [MIT license](https://github.com/propenster/Clash/blob/main/LICENSE).

Contributions, Issues submissions, PRs etc are in order. Please communicate if you any issues or want to contribute.





