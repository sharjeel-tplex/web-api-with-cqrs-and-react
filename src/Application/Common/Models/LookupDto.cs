using DevTest.Domain.Entities;

namespace DevTest.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, LookupDto>();
        }
    }
}
