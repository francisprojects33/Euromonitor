using EUROMONITOR.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(bool trackChanges);
        Task<Subscription> GetUserSubscriptionAsync(Guid subscriptionId, bool trackChanges);
        void CreateSubscription(Subscription subscription);
        void DeleteSubscription(Subscription subscription);
    }
}
