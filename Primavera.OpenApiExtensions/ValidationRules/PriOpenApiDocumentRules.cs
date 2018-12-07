using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Validations;
using Microsoft.OpenApi.Validations.Rules;

namespace Primavera.OpenApiExtensions.ValidationRules
{
    [OpenApiRule]
    public static class PriOpenApiDocumentRules
    {
        private static IOpenApiExtension value;

        /// <summary>
        /// The Info field is required.
        /// </summary>
        public static ValidationRule<OpenApiDocument> FirstPrimavera =>
            new ValidationRule<OpenApiDocument>(
                (context, item) =>
                {
                    // paths
                    //context.Enter("x-Primavera-Extensions");
                    if (!item.Extensions.ContainsKey("x-Primavera-Extensions"))
                    {

                        context.CreateError(nameof(FirstPrimavera),
                            String.Format("Falta o campo {0} no nó {1}", "x-Primavera-Extensions", "document"));
                    }
                    //context.Exit();

                    context.Enter("x-Primavera-Extensions");

                    item.Extensions.TryGetValue("x-Primavera-Extensions",  out var value);
                    
                    if (value is OpenApiObject pairs)
                    {
                        foreach (var t in pairs)
                        {
                            
                        }
                    }
                    

                    if (!item.Info.Extensions.ContainsKey("x-Primavera-Resources"))
                    {
                        context.CreateError(nameof(FirstPrimavera),
                            String.Format("Falta o campo {0} no nó {1}", "x-Primavera-Resources", "document"));
                    }
                    context.Exit();

                    //context.Enter("info");
                    //if (!item.Info.Extensions.ContainsKey("x-Primavera-Resources"))
                    //{
                    //    context.CreateError(nameof(FirstPrimavera),
                    //        String.Format("Falta o campo {0} no nó {1}", "x-Primavera-Extensions", "document"));
                    //}
                    //context.Exit();





                    //context.Enter("x-Primavera-Extensions");
                    //var primaveraRoot = item.Extensions.TryGetValue("x-Primavera-Extensions", out value);
                    //var primaveraRoot2 = item.Extensions.Values;
                    //context.Exit();
                    //if (true)
                    //{
                    //    context.CreateError(nameof(FirstPrimavera),
                    //        String.Format("Falta o campo {0} no nó {1}", "x-Primavera-Extensions", "document"));
                    //}




                });
    }
}
