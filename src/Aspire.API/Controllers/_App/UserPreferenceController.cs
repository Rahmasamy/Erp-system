using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
/// <summary>AddOrUpdate
/// 

/// Version : 1.0.0
/// </summary>
namespace Aspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserPreferenceController : ControllerBase
{
    private readonly ILogger<UserPreferenceController> _logger;
    private readonly ITenantProviderService _TenantService;
    private readonly IGenericRepository<UserPreference, UserPreferenceDTO, ApplicationDbContext> _genericRepository;
    private readonly IMapper _Mapper;
    public UserPreferenceController(ILogger<UserPreferenceController> logger, 
        ITenantProviderService TenantService,
        IGenericRepository<UserPreference, UserPreferenceDTO, ApplicationDbContext> genericRepository,
        IMapper Mapper)
    {
        _logger = logger;
        _TenantService = TenantService;
        _genericRepository = genericRepository;
        _Mapper = Mapper;
    }

}
