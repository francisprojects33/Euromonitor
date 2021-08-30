using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        ISubscriptionRepository Subscription { get; }
        Task SaveAsync();
    }
}
