﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Writers;

namespace Microsoft.OpenApi.Models
{
    /// <summary>
    /// Example Object.
    /// </summary>
    public class OpenApiExample : OpenApiElement, IOpenApiReference, IOpenApiExtension
    {
        /// <summary>
        /// Short description for the example.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Long description for the example.
        /// CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Embedded literal example. The value field and externalValue field are mutually 
        /// exclusive. To represent examples of media types that cannot naturally represented 
        /// in JSON or YAML, use a string value to contain the example, escaping where necessary.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// A URL that points to the literal example. 
        /// This provides the capability to reference examples that cannot easily be 
        /// included in JSON or YAML documents. 
        /// The value field and externalValue field are mutually exclusive.
        /// </summary>
        public string ExternalValue { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IDictionary<string, IOpenApiAny> Extensions { get; set; }

        /// <summary>
        /// Reference object.
        /// </summary>
        public OpenApiReference Pointer
        {
            get; set;
        }

        /// <summary>
        /// Serialize <see cref="OpenApiExample"/> to Open Api v3.0
        /// </summary>
        internal override void WriteAsV3(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull(nameof(writer));
            }

            if (Pointer != null)
            {
                Pointer.WriteAsV3(writer);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteProperty("summary", Summary);
                writer.WriteProperty("description", Description);

                if (Value != null)
                {
                    writer.WritePropertyName("value");
                    writer.WriteRaw(Value);
                } else if (ExternalValue != null)
                {
                    writer.WriteProperty("externalValue", ExternalValue);
                }

                writer.WriteExtensions(Extensions);
                writer.WriteEndObject();
            }
        }

        /// <summary>
        /// Serialize <see cref="OpenApiExample"/> to Open Api v2.0
        /// </summary>
        internal override void WriteAsV2(IOpenApiWriter writer)
        {
            // Example object of this form does not exist in V2.
            // V2 Example object requires knowledge of media type and exists only
            // in Response object, so it will be serialized as a part of the Response object.
        }
    }
}
