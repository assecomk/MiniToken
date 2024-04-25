using System.ComponentModel.DataAnnotations;

namespace MiniToken.Models
{
    public class SubmitTokenModel
    {
        [Required]
        public decimal? ClockTime { get; set; }

        [Required]
        public string Token { get; set; }

        public bool ShowTooltip { get; set; }
    }
}