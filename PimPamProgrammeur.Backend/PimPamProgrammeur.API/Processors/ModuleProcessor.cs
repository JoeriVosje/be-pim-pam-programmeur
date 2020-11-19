using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Processors
{
    public class ModuleProcessor : IModuleProcessor
    {
        private readonly IMapper _mapper;
        private readonly IModuleRepository _moduleRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IValidator<Classroom> _classroomValidator;
        private readonly ISessionRepository _sessionRepository;

        public ModuleProcessor(IMapper mapper, IModuleRepository moduleRepository, IClassroomRepository classroomRepository, IValidator<Classroom> classroomValidator, ISessionRepository sessionRepository)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _classroomRepository = classroomRepository;
            _classroomValidator = classroomValidator;
            _sessionRepository = sessionRepository;
        }
        public async Task<ModuleResponseDto> SaveModule(ModuleRequestDto requestDto)
        {

            var module = _mapper.Map<Module>(requestDto);

            var resultModule = await _moduleRepository.SaveModule(module);

            var moduleWithStatus = GetModuleStatus(resultModule);

            return moduleWithStatus ?? null;

        }
        public ModuleResponseDto GetModule(Guid id)
        {
            var module = _moduleRepository.GetModule(id);
            var moduleResponse = GetModuleStatus(module);

            return moduleResponse ?? null;

        }

        public async Task<ModuleResponseDto> UpdateModule(ModuleUpdateRequestDto requestDto)
        {
            var module = _mapper.Map<Module>(requestDto);
            
            var resultModule = await _moduleRepository.UpdateModule(module);

            var moduleWithStatus = GetModuleStatus(resultModule);

            return moduleWithStatus ?? null;
        }

        public IEnumerable<ModuleResponseDto> GetModules()
        {
            var modules = _moduleRepository.GetModules();
            var modulesWithStatuses = GetAllModuleStatus(modules);
            return modulesWithStatuses ?? null;
        }

        public async Task<ValidationResult> DeleteModule(Guid id)
        {
            var validationResult = new ValidationResult();

            var module = _moduleRepository.GetModule(id);

            if (module == null)
            {
                return null;
            }

            var classroom = _classroomRepository.GetClassroomByModule(module.Id);
            if (classroom != null)
            {
                validationResult =_classroomValidator.Validate(classroom);
                return validationResult;
            }

            await _moduleRepository.DeleteModule(id);

            return validationResult;

        }

        public ModuleResponseDto GetModuleStatus(Module module)
        {
            var session = _sessionRepository.GetOpenSessions(module.Id);

            var moduleWithoutSession = _mapper.Map<ModuleResponseDto>(module);
            if (session.Any())
            {
                moduleWithoutSession.Status = "open";
                var moduleWithSession = moduleWithoutSession;
                return moduleWithSession ?? null;
            }

            var Sessions = _sessionRepository.GetSessions(module.Id);
            var closedSessions = Sessions.Where(e => e.ModuleId == module.Id && e.EndTime != null);

            if (closedSessions.Any())
            {
                moduleWithoutSession.Status = "closed";
                var moduleWithClosedSession = moduleWithoutSession;
                return moduleWithClosedSession ?? null;
            }

            moduleWithoutSession.Status = "session not found";
            return moduleWithoutSession ?? null;
        }

        public IEnumerable<ModuleResponseDto> GetAllModuleStatus(IEnumerable<Module> modules)
        {
            var modulesWithStatuses = new List<ModuleResponseDto>();

            foreach (var module in modules)
            {
                var moduleWithStatus = GetModuleStatus(module);
                modulesWithStatuses.Add(moduleWithStatus);
            }

            return modulesWithStatuses;

        }

            



    }
}
