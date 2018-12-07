using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Primavera.OpenApiExtensions
{
    public class PrimaveraOpenApiStreamReader : OpenApiStreamReader
    {
        private readonly OpenApiReaderSettings openApiReaderSettings;

        public PrimaveraOpenApiStreamReader(OpenApiReaderSettings openApiReaderSettings)
            :base(openApiReaderSettings)
        {          

            this.openApiReaderSettings = openApiReaderSettings;
        }

        public new OpenApiDocument Read(Stream input, out OpenApiDiagnostic diagnostic)
        {
            diagnostic = new OpenApiDiagnostic();  
                       

            var document = base.Read(input, out diagnostic);


            return (OpenApiDocument)document;
        }
    }
}
