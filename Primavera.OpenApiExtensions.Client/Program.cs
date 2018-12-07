using System;
using System.IO;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Validations;

namespace Primavera.OpenApiExtensions.Client
{
    class Program
    {
        public static string _inputFile = "pri.yml";

        static void Main(string[] args)
        {
            ParseDocument();
        }

        internal static void ParseDocument()
        {
            Stream stream;
            stream = new FileStream(_inputFile, FileMode.Open);

            //var document = new OpenApiStreamReader
            //    (new OpenApiReaderSettings
            //        {
            //            ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences,
            //            RuleSet = ValidationRuleSet.GetDefaultRuleSet()
            //        }
            //    ).Read(stream, out var context);

            var settings = new OpenApiReaderSettings
            {
                ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences,
                RuleSet = ValidationRuleSet.GetDefaultRuleSet()
            };

            settings.RuleSet.Add(Primavera.OpenApiExtensions.ValidationRules.PriOpenApiDocumentRules.FirstPrimavera);

            var document = new PrimaveraOpenApiStreamReader(settings).Read(stream, out var context);
            //document.Paths.Validate();

            var rootExtensions = document.Extensions.TryGetValue("x-next", out var value);
            var pathResponseExtensions = document.Paths.TryGetValue("/clients", out var a);
            Console.WriteLine("");

        }
    }
}
