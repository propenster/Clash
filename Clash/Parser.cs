using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Clash
{
    public interface IParser
    {
        object Parse<T>(string[] args) where T : class, new();
    }
    public class Parser : IParser
    {
        public Parser()
        {

        }
        public object Parse<T>(string[] args) where T : class, new()
        {
            T result = new T();
            var sb = new StringBuilder();
            var arguments = args.Select(x => string.Format("{0}{1}", x, "#")).ToArray();
            sb.Append(string.Join("#", args));
            sb.Append("#");
            var argsArrayString = sb.ToString();



            foreach (var prop in result.GetType().GetProperties())
            {
                var attrib = prop.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault();
                if (attrib == null) continue;

                if (args.Length == 1)//probably default tags...
                {
                    //check for default tags... help, version, about etc...
                    Console.WriteLine(HandleDefaultArgs(result, args.FirstOrDefault()));

                    return result;

                }

                var matches = Regex.Matches(argsArrayString, @"-(?<variableType>\w)\#(?<variableValue>\S+)");
                if (matches.Count == 0) continue;

                var splitSlash = argsArrayString.Trim().Replace("\n", "").Split('-').Where(x => !string.IsNullOrWhiteSpace(x));

                var interestValue = splitSlash.FirstOrDefault(c => c.ToLowerInvariant()[0] == attrib.Short.ToLowerInvariant()[0]);
                if (string.IsNullOrWhiteSpace(interestValue)) continue;
                var matchActualValue = Regex.Matches(interestValue, @"(?<variableType>\w)#(?<variableValue>.*?)#");

                if (matchActualValue != null && matchActualValue.Count > 0)
                {
                    var targetString = matchActualValue.Cast<Match>().Select(x => x.Groups["variableValue"].Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(targetString))
                    {
                        var value = Convert.ChangeType(targetString, prop.PropertyType);
                        prop.SetValue(result, value, null);
                        continue;
                    }
                }           

            }

            //check for errors...
            //properties that are required but value couldn't be set for them
            var errorString = string.Empty;
            var props = result.GetType().GetProperties().Where(x => x.GetValue(result, null) == null && x.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Required == true);
            if (props != null && props.Any())
            {
                Console.Clear();
                var helpString = HelpString(result);
                sb.Clear();
                sb.AppendLine(string.Format("{0} for these arguments [{1}] \n{2}", "Invalid arguments and/or values", string.Join(", ", props.Select(x => $"*--{x.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Long.ToLowerInvariant()} or -{x.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Short.ToLowerInvariant()}*")), helpString));
                errorString = sb.ToString();

                Console.WriteLine(errorString);
            }
            //break cli_args into tokens... and attach tokens to object value...

            return result;

        }

        private string HandleDefaultArgs(object t, string v)
        {
            var outString = string.Empty;
            switch (v.ToLowerInvariant())
            {
                case "-h":
                case "--help":
                    return HelpString(t);

                case "-v":
                case "--version":
                    return VersionString(t);

                case "-a":
                case "--authors":
                    return AuthorString(t);

                default:
                    return string.Format("Invalid Arguments \n{0}", HelpString(t));

            }

        }

        public string HelpString(object obj)
        {
            var sb = new StringBuilder();

            var body = @"$ demo --help
A simple to use, efficient, and full-featured Command Line Argument Parser

Usage: demo[EXE] [OPTIONS] --name <NAME>

Options:
  -n, --name <NAME>    Name of the person to greet
  -c, --count <COUNT>  Number of times to greet [default: 1]
  -h, --help           Print help
  -V, --version        Print version";

            var isValidObj = obj.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault();
            if (isValidObj == null) return string.Empty; //Parseable object must impelement the CommandAttribute

            //get valid properties of this object that are decorated with ArgAttribute...
            var fields = obj.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ArgAttribute))).ToList();
            //get appName
            var appName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            //paint
            sb.Append(!string.IsNullOrWhiteSpace(isValidObj?.About) ? isValidObj?.About : Defaults.About);
            var argNameArryString = String.Join(" ",
                fields.Select(p => string.Format("--{0} <{1}>",
                p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Long.ToLowerInvariant()
                ?? p.Name.ToLowerInvariant(), p.Name.ToUpperInvariant()
                ?? p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Long.ToUpperInvariant())));

            sb.Append("\n\n");

            var fullDescriptionArray = String.Join("\n",
                fields.Select(p => string.Format("-{0}, --{1} <{2}> {3} {4}",
                p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Short.ToLowerInvariant()
                ?? p.Name[0].ToString().ToLowerInvariant(),
                p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Long.ToLowerInvariant()
                ?? p.Name.ToLowerInvariant(), p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Long.ToUpperInvariant()
                ?? p.Name.ToUpperInvariant(), p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.Description
                ?? $"Description for {p.Name.ToLowerInvariant()}", p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.DefaultValue == null ? string.Empty : $"[default: {p.GetCustomAttributes().OfType<ArgAttribute>().FirstOrDefault()?.DefaultValue}]"
                )));

            var helpAndVersion = string.Format("{0}\n{1}\n{2}", "-h, --help <HELP> Print help", $"-V, --version <VERSION> Print version", $"-A, --author <AUTHOR> Print author(s)");

            sb.Append(string.Format(@"{0}: {1}[EXE] OPTIONS {2}", "Usage", appName, argNameArryString));

            sb.Append("\n\n");


            sb.Append(
                string.Format(
                    "{0}:\n{1}\n{2}",
                    "Options",
                    fullDescriptionArray,
                    helpAndVersion


                    )
                );

            return sb.ToString();
        }

        public string VersionString(object obj)
        {
            var sb = new StringBuilder();

            var isValidObj = obj.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault();
            if (isValidObj == null) return string.Empty; //Parseable object must impelement the CommandAttribute
            //get appName
            var appName = Assembly.GetEntryAssembly().GetName().Name;

            sb.AppendLine(string.Format("{0} {1}", appName, isValidObj?.Version));

            return sb.ToString();
        }
        public string AuthorString(object obj)
        {
            var sb = new StringBuilder();

            var isValidObj = obj.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault();
            if (isValidObj == null) return string.Empty; //Parseable object must impelement the CommandAttribute
            //get appName

            sb.AppendLine(string.Format("{0} [{1}]", "Author(s)", isValidObj?.Author));

            return sb.ToString();
        }

    }
}
