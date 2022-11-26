
#nullable disable

namespace MyWebApp.Models
{
    public class StateModel
    {
        public int StateId { get; set; }

        public int Page { get; set; }
        public string FilterParam { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc { get; set; }

        public int UserId { get; set; }
        public virtual UserModel User { get; set; }
    }
}
