using EUROMONITOR.Model;
using EUROMONITOR.Model.DTO;
using EUROMONITOR.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROMONITOR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IAuthenticationManager _authManager;

        public SubscriptionController(IRepositoryManager repository, IAuthenticationManager authManager)
        {
            _repository = repository;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionCreationDto subscription)
        {
            try
            {
                if (subscription == null)
                {
                    return BadRequest("SubscriptionCreationDto object is null");
                }

                var userId = HttpContext.Session.Keys.FirstOrDefault();

                Guid bookIdGuid = new Guid(subscription.BookId);

                var subscriptionModel = new Subscription
                {
                    Id = userId.ToString(),
                    BookId = bookIdGuid,
                    SubscriptionDate = subscription.SubscriptionDate
                };

                _repository.Subscription.CreateSubscription(subscriptionModel);

                await _repository.SaveAsync();

                return Ok("Subscription Created");
            }
            catch (Exception)
            {
                return StatusCode(400, "Object is null");
            }
        }

        // All Subscriptions for a single user (currently authenticated user)
        [HttpGet(Name = "GetUserSubscriptions")]
        public async Task<IActionResult> GetUserSubscriptions()
        {
            try
            {
                var subscriptions = await _repository.Subscription.GetUserSubscriptionsAsync(trackChanges: false);
                var userId = HttpContext.Session.Keys.FirstOrDefault();
                var subscriptionsPerUser = subscriptions.Where(u => u.Id == userId).ToList();

                var subscriptionsDto = subscriptionsPerUser.Select(b => new SubscriptionDto
                {
                    UserId = b.Id,
                    BookId = b.BookId,
                    SubscriptionDate = b.SubscriptionDate
                }).ToList();

                return Ok(subscriptionsDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "SubscriptionByBookId")]
        public async Task<IActionResult> GetUserSubscription(Guid id)
        {
            try
            {
                var subscription = await _repository.Subscription.GetUserSubscriptionAsync(id, trackChanges: false);
                if (subscription == null)
                {
                    return NotFound();
                }
                else
                {
                    var subscriptionDto = new SubscriptionDto
                    {
                        UserId = subscription.Id,
                        BookId = subscription.BookId,
                        SubscriptionDate = subscription.SubscriptionDate
                    };

                    return Ok(subscriptionDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "Not Found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(Guid id)
        {
            try
            {
                var subscription = await _repository.Subscription.GetUserSubscriptionAsync(id, trackChanges: false);
                if (subscription == null)
                {
                    return NotFound();
                }

                _repository.Subscription.DeleteSubscription(subscription);
                await _repository.SaveAsync();
                return Ok("Subscription successfully deleted.");
            }
            catch (Exception)
            {
                return StatusCode(404, "Not Found");
            }
        }
    }
}
