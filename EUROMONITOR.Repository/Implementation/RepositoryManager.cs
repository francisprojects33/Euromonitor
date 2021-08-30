using EUROMONITOR.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private AppDbContext _appDbContext;
        private IBookRepository _bookRepository;
        private ISubscriptionRepository _subscriptionRepository;

        public RepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IBookRepository Book 
        {
            get
            { 
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_appDbContext);
                return _bookRepository;
            }
        }

        public ISubscriptionRepository Subscription
        {
            get
            {
                if (_subscriptionRepository == null)
                    _subscriptionRepository = new SubscriptionRepository(_appDbContext);
                return _subscriptionRepository;
            }
        }

        public Task SaveAsync() => _appDbContext.SaveChangesAsync();
    }
}
