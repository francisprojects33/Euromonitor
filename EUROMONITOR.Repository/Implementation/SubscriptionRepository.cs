using EUROMONITOR.Model;
using EUROMONITOR.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository.Implementation
{
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {

        }

        public async Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.SubscriptionDate)
            .ToListAsync();

        public async Task<Subscription> GetUserSubscriptionAsync(Guid subscriptionId, bool trackChanges) =>
            await FindByCondition(c => c.BookId.Equals(subscriptionId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateSubscription(Subscription subscription) => Create(subscription);

        public void DeleteSubscription(Subscription subscription)
        {
            Delete(subscription);
        }
    }
}
