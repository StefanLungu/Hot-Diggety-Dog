using Domain.Resources;

namespace Domain.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int EntitiesPerPage { get; set; } = Constants.EntitiesPerPage;
    }
}
