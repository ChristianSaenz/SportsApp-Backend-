namespace SportsApp.DTO_s
{
    public class AddFavoriteDTO
    {
        public long PlayerId { get; set; }
    }

    public class FavoriteDTO
    {
        public long FavoriteId { get; set; }
        public long PlayerId { get; set; }

       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string Position { get; set; }
        public string TeamName { get; set; }  
    }


}
