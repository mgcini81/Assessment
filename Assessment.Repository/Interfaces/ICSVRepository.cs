using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assessment.Repository.Interfaces
{
    public interface ICSVRepository<X,Y,Z> where X : class
    {
        IEnumerable<X> GetAll();

        X SingleOrDefault(Expression<Func<X, bool>> predicate);
       
        IEnumerable<Y> GetNameFrequency();

        IEnumerable<Z> GetSortedAddresses();

    }
}
