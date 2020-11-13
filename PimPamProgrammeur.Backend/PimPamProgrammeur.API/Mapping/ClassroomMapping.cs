using AutoMapper;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimPamProgrammeur.API.Mapping
{
    public class ClassroomMapping : Profile
    {
        public ClassroomMapping()
        {
            CreateMap<Classroom, ClassroomResponseDto>().ConvertUsing((e, _, context) => ClassroomToClassroomResponseDto(e, context));
            CreateMap<ClassroomRequestDto, Classroom>().ConvertUsing((e, _, context) => ClassroomRequestDtoToClassroom(e));
        }

        private ClassroomResponseDto ClassroomToClassroomResponseDto(Classroom classroom, ResolutionContext resolution)
        {
            return new ClassroomResponseDto
            {
                Id = classroom.Id,
                Major = classroom.Major,
                ModuleId = classroom.ModuleId,
                Name = classroom.Name
            };
        }

        private Classroom ClassroomRequestDtoToClassroom(ClassroomRequestDto requestDto)
        {
            return new Classroom
            {
                Major = requestDto.Major,
                ModuleId = requestDto.ModuleId,
                Name = requestDto.Name,
                CreationDate = DateTime.Now
            };
        }
    }
}
