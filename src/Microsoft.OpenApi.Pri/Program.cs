using System;
using System.IO;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Validations;

namespace Microsoft.OpenApi.Pri
{
    class Program 
    {

        public static string _inputFile = "api-documentation-test.yml";
        static void Main(string[] args)
        {
            Program.ParseDocument();
        }

        internal static void ParseDocument()
        {
            Stream stream;
            stream = new FileStream(_inputFile, FileMode.Open);

            var document = new OpenApiStreamReader
                (new OpenApiReaderSettings
                    {
                        ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences,
                        RuleSet = ValidationRuleSet.GetDefaultRuleSet()
                    }
                ).Read(stream, out var context);
            var rootExtensions = document.Extensions.TryGetValue("x-next", out var value);
            var pathResponseExtensions = document.Paths.TryGetValue("/clients", out var a);
            Console.WriteLine("");

        }
    }//x-Primavera-Extensions
}
