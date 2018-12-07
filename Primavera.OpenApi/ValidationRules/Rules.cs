using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Validations;
using Microsoft.OpenApi.Validations.Rules;

namespace Primavera.OpenApi.ValidationRules
{
    [OpenApiRule]
    public static class PrimaveraOpenApiRules
    {
        /// <summary>
        /// The Info field is required.
        /// </summary>
        public static ValidationRule<PrimaveraOpenApiDocument> FirstPrimavera =>
            new ValidationRule<PrimaveraOpenApiDocument>(
                (context, item) =>
                {
                    // paths
                    context.Enter("x-Primavera-Extensions");

                    if (!item.Extensions.ContainsKey("x-Primavera-Extensions"))
                    {
                        context.CreateError(nameof(FirstPrimavera),
                            String.Format("Falta o campo {0} no nó {1}", "x-Primavera-Extensions", "document"));
                    }
                    context.Exit();
                });
    }
}
