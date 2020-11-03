using System.Linq;
using AutoMapper;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.API.Models
{
    /// <summary>
    /// Профиль для AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Student, Student>()
                .ForMember(m => m.Groups,
                    eo => eo.MapFrom(
                        e => e.StudentGroups.Select(x => x.Group).Distinct().ToArray()
                    ));

            CreateMap<Domain.Entities.Group, Group>()
                .ForMember(m => m.Students,
                    eo => eo.MapFrom(
                        e => e.StudentGroups.Select(x => x.Student).Distinct().ToArray()
                    ));
            ;

            CreateMap<Domain.Entities.Student, StudentDto>();
            CreateMap<Domain.Entities.Group, GroupDto>();

            CreateMap<PaginationResult<Domain.Entities.Student>, PaginationResult<Student>>();
        }
    }
}