using AutoMapper.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AgentOrange.Models
{
    public class HelperUtils
    {
        public class CustomContractResolver : DefaultContractResolver
        {

            private Dictionary<string, string> PropertyMappings { get; set; }

            public CustomContractResolver()
            {
                this.PropertyMappings = new Dictionary<string, string>{
                                        {"Id", "_id"},
                                        {"AgentId", "agent_id"},
                                        {"CustomerGuid", "guid"},
                                        {"IsActive", "isActive" },
                                        {"EyeColor","eyeColor" },
                                        {"ZipCode","zipCode" },
                                        {"PhoneNumbers", "phone" }, };
            }

            protected override string ResolvePropertyName(string propertyName)
            {
                string resolvedName = null;

                var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);

                return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName.ToLower());
            }

        }

        public static List<string> GetChangedProperties(Object objA, Object objB)
        {
            if (objA.GetType() != objB.GetType())
            {
                throw new System.InvalidOperationException("Objects of different Type");
            }
            List<string> changedProperties = ElaborateChangedProperties(objA.GetType().GetProperties(), objB.GetType().GetProperties(), objA, objB);
            return changedProperties;
        }


        public static List<string> ElaborateChangedProperties(PropertyInfo[] pA, PropertyInfo[] pB, Object A, Object B)
        {
            List<string> changedProperties = new List<string>();
            foreach (PropertyInfo info in pA)
            {
                object propValueA = info.GetValue(A, null);
                object propValueB = info.GetValue(B, null);
                if (propValueA != propValueB)
                {
                    changedProperties.Add(info.Name);
                }
            }
            return changedProperties;
        }
    }

}
