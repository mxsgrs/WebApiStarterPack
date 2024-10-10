using Microsoft.AspNetCore.Mvc;

namespace Starter.Store.WebApi.BuildingBlocks.Presentation;

[ApiController]
[Route("api/store/[controller]")]
public class StoreControllerBase(IMapper mapper) : ControllerBase
{
    protected readonly IMapper _mapper = mapper;
}
