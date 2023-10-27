﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.IdServer.Api;
using SimpleIdServer.IdServer.Domains;
using System.Linq;

namespace SimpleIdServer.IdServer.Extractors
{
    public class AttributeClaimExtractor : BaseClaimExtractor, IClaimExtractor
    {
        public MappingRuleTypes MappingRuleType => MappingRuleTypes.USERATTRIBUTE;

        public object Extract(HandlerContext context, IClaimMappingRule mappingRule)
        {
            if (!mappingRule.IsMultiValued)
            {
                var attr = context.UserClaims.FirstOrDefault(c => c.Type == mappingRule.SourceUserAttribute);
                if (attr == null) return null;
                return Convert(attr.Value, mappingRule);
            }

            var claims = context.UserClaims.Where(c => c.Type == mappingRule.SourceUserAttribute).Select(c => Convert(c.Value, mappingRule)).ToList();
            return claims;
        }
    }
}
