using MasterClass.WebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.Implementation
{
    public class ApplicationRequestContext : IApplicationRequestContext
    {
        public Guid Id { get; }

        public ApplicationRequestContext()
        {
            Id = Guid.NewGuid();
        }
    }
}
