using ClotheStore.Application.Queries;
using ClotheStore.Domain.Repositories;
using ClotheStore.Domain.Services;

namespace ClotheStore.Application.Commands
{
    public class CartCommandService(ICartItemCommandService cartItemCommandService,
        ICartItemQueryService cartItemQueryService,
        IIdentityService identityService,
        IUnitOfWork unitOfWork) : ICartCommandService
    {        
        // TODO: Review if class is needed
    }
}
