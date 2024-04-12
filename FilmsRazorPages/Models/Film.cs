using System.ComponentModel.DataAnnotations;

namespace FilmsRazorPages.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le champ doit etre defini")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Le champ doit etre defini")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Le champ doit etre defini")]
        public string? Realisateur { get; set; }

        [Required(ErrorMessage = "Le champ doit etre defini")]
        [Range(1895, 2024, ErrorMessage = "Anne invalide")]
        public string? Anne { get; set; }
        [Required(ErrorMessage = "Le champ doit etre defini")]
        public string? Description { get; set; }
        public string? Poster { get; set; }
    }
}
