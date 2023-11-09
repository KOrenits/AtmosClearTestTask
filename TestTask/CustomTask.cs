using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask
{
    public class CustomTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Here i removed [Required] and added nullable string values so i can make my
        //own error messages in response when Title and Description properties are empty.
        //[Required]
        public string? Title { get; set; }
        //[Required]
        public string? Description { get; set; }
    }
}
