using AutoMapper;
using CSM.MVC.Models.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CourseDto, Course>();
        CreateMap<Course, CourseDto>();

        CreateMap<Student, StudentDto>();
        CreateMap<StudentDto,Student>();

    }
}
