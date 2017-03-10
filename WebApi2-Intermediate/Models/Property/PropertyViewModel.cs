using Jo2let_Api.Models.Location;

namespace Jo2let_Api.Models.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LocationViewModel Location { get; set; }

    }
}