using MediatR;
using ModularRestaurant.Menus.Domain.Repositories;
using ModularRestaurant.Shared.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace ModularRestaurant.Ratings.Infrastructure.EF.EventHandlers
{
    public class MenuChangedEventHandler : INotificationHandler<MenuChangedEvent>
    {
        private readonly IMenuRepository _repository;

        public MenuChangedEventHandler(IMenuRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MenuChangedEvent notification, CancellationToken cancellationToken)
        {
            var activeMenu = await _repository.GetActiveMenu();
            var menu = await _repository.GetAsync(notification.Id, cancellationToken);

            if (activeMenu is not null)
            {
                // TODO implement activeMenu.Deactivate();
                // TODO implement update in repository or consider moving from repository pattern
            }
            menu.Activate();
            // TODO implement update
        }
    }
}
