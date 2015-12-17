
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Dapper
{
    public interface IMappingProvider
    {
        Dictionary<Type, string> Mappings { get; }
    }
}
