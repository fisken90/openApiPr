﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using SharpYaml.Serialization;

namespace Microsoft.OpenApi.Readers.YamlReaders.ParseNodes
{
    internal class PropertyNode : ParseNode
    {
        public PropertyNode(ParsingContext context, OpenApiDiagnostic log, string name, YamlNode node) : base(
            context,
            log)
        {
            Name = name;
            Value = Create(context, log, node);
        }

        public string Name { get; set; }

        public ParseNode Value { get; set; }

        public void ParseField<T>(
            T parentInstance,
            IDictionary<string, Action<T, ParseNode>> fixedFields,
            IDictionary<Func<string, bool>, Action<T, string, ParseNode>> patternFields)
        {
            Action<T, ParseNode> fixedFieldMap;
            var found = fixedFields.TryGetValue(Name, out fixedFieldMap);

            if (fixedFieldMap != null)
            {
                try
                {
                    Context.StartObject(Name);
                    fixedFieldMap(parentInstance, Value);
                }
                catch (OpenApiException ex)
                {
                    ex.Pointer = Context.GetLocation();
                    Diagnostic.Errors.Add(new OpenApiError(ex));
                }
                finally
                {
                    Context.EndObject();
                }
            }
            else
            {
                var map = patternFields.Where(p => p.Key(Name)).Select(p => p.Value).FirstOrDefault();
                if (map != null)
                {
                    try
                    {
                        Context.StartObject(Name);
                        map(parentInstance, Name, Value);
                    }
                    catch (OpenApiException ex)
                    {
                        ex.Pointer = Context.GetLocation();
                        Diagnostic.Errors.Add(new OpenApiError(ex));
                    }
                    finally
                    {
                        Context.EndObject();
                    }
                }
                else
                {
                    Diagnostic.Errors.Add(
                        new OpenApiError("", $"{Name} is not a valid property at {Context.GetLocation()}"));
                }
            }
        }
    }
}