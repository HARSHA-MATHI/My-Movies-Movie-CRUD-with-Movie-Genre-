using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace My_Movies.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieDescription {  get; set; }
        [Required] 
        public int YearOfRelease {  get; set; }
        [Required]
        public int GenreId { get; set; }
        public MovieGenre Genre { get; set; }
    }
}