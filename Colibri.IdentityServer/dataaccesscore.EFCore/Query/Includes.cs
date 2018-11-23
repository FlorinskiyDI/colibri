using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataaccesscore.EFCore.Query
{
    public class Includes<TEntity>
    {
        public Includes(Func<IQueryable<TEntity>, IQueryable<TEntity>> expression)
        {
            Expression = expression;
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Expression { get; private set; }
    }
}
