namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class Rating
    {
        public Guid UserId { get; set; }
        public Guid DishId { get; set; }
        public int RatingScore { get; set; }
    }
}
