using System;
using System.Collections.Generic;
using System.Text;

namespace dataaccesscore.Abstractions.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(IDisposable context);
    }
}
