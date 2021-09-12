using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopifyChallenge.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected Guid CustomerId = Guid.Parse("ce3b9449-7970-4d8c-9e19-6715772601d6");

        protected BaseController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool IsOperationValid()
        {
            return !_notifications.HasNotification();
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }
    }
}
