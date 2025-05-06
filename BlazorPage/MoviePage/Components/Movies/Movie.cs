using System.ComponentModel.DataAnnotations;

namespace MyWebPage.Components
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        public float? Rate { get; set; }
        public string? ImageUrl { get; set; }

    }
}
