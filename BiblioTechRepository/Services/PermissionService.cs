using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Permissions;

namespace BiblioTechDomain.Services
{
    public class PermissionService : BaseReadOnlyService<ReadPermissionViewModel, Permission>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
            : base(permissionRepository, mapper) 
        {        
            _permissionRepository = permissionRepository;
        }
        
        public async Task<IEnumerable<ReadPermissionViewModel>> GetPermissionsAsync(long typeId)
        {
            var permissions = await _permissionRepository.GetPermissionsAsync(typeId);        
            return _mapper.Map<IEnumerable<ReadPermissionViewModel>>(permissions);
        }

        protected override Func<Permission, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            return a => true;
        }
    }
}
