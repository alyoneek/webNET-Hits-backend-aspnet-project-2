namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class DishPagedListDto
    {
        public List<DishDto> Dishes { get; set; }
        public PageInfoModel Pagination { get; set; }

        public DishPagedListDto(List<DishDto> dishes, PageInfoModel pagination)
        {
            Dishes = dishes;
            Pagination = pagination;
        }
    }
}
