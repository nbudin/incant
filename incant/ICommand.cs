using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace incant
{
    public abstract class Command<OutputType>
    {
        public abstract OutputType execute();

        public Parameter[] Parameters { get; private set; }
        private Dictionary<string, Parameter> _ByNameIndex = new Dictionary<string, Parameter>();

        public Command()
        {
            Parameters = this.GetType().GetProperties()
                .Where(pi => pi.GetCustomAttributes(true).Any(o => o is ParameterAttribute))
                .Select(pi => new Parameter(pi,this)).ToArray();
            _ByNameIndex = Parameters.ToDictionary(p => p.Name, p => p);
        }

        public IEnumerable<Parameter> RequiredParameters
        {
            get
            {
                return Parameters.Where(p => p.Required);
            }
        }

        protected bool IsSatisfied(string paramName)
        {
            return IsSatisfied(this[paramName]);
        }

        protected virtual bool IsSatisfied(Parameter p)
        {
            return p.CurrentValue != null;
        }

        public bool Satisfied
        {
            get
            {
                return RequiredParameters.All(p => IsSatisfied(p));
            }
        }

        public Parameter this[string name]
        {
            get
            {
                Parameter p;
                if (_ByNameIndex.TryGetValue(name, out p))
                    return p;
                else
                    throw new InvalidParameterException(name);
            }
        }

        public class InvalidParameterException : Exception
        {
            public InvalidParameterException(string parameterName) : base("Parameter does not exist: " + parameterName) { }
        }

        public class Parameter
        {
            private PropertyInfo propertyInfo;
            private ParameterAttribute parameterInfo;
            private Command<OutputType> Owner { get; set; }

            public Parameter(PropertyInfo property, Command<OutputType> owner) { Owner = owner; propertyInfo = property; parameterInfo = (propertyInfo.GetCustomAttributes(true).First(o => o is ParameterAttribute) as ParameterAttribute); }

            public string Name { get { return propertyInfo.Name; } }
            public bool Required { get { return parameterInfo.Required; } }
            public object /*boooooo*/ CurrentValue
            {
                get
                {
                    return propertyInfo.GetValue(Owner, null);
                }
            }
        }
    }

    public class ParameterAttribute : Attribute
    {
        public bool Required { get; set; }
        public ParameterAttribute(bool required) { Required = required; }
        public ParameterAttribute() : this(true) { }
    }
}