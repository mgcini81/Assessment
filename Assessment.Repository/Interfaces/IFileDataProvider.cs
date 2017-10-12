using Assessment.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assessment.Repository.Interfaces
{
    public interface IFileDataProvider<TEntity> where TEntity : IPerson
    {
        IQueryable<TEntity> Data { get; set; }
        void Load();
    }
}
