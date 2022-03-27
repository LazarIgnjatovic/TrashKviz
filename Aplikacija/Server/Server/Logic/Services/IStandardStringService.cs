using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IStandardStringService
    {
        string Standardize(string latinString);
    }
}
