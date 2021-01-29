using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MbcaWebAPI.Models.Repository
{
    interface IDataStatusRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> Lock(TKey id);

        Task<TEntity> Unlock(TKey id);
    }
}
