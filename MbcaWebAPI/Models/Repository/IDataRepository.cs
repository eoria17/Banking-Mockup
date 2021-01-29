using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MbcaWebAPI.Models.Repository
{
    public interface IDataRepository<TEntity , TKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(TKey id);


    }
}
