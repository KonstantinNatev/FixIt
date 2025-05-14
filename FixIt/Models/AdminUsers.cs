using System.ComponentModel.DataAnnotations;

namespace FixIt.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // За по-сигурно — може да е хеш
    }
}
