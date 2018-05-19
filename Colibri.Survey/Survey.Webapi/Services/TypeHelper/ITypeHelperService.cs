using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Services.TypeHelper
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
