using System.ComponentModel.DataAnnotations;

namespace MyWebApp.DTO.State.Request
{
    public class StateRequestDto
    {
        public bool? FirstLogin { get; set; }

        public bool? DESC { get; set; }

        [Range(1, 100)]
        public int? Page { get; set; }

        public string? Filter { get; set; }

        public string? SortBy { get; set; }
    }
}
