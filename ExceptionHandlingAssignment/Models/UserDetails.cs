using System.ComponentModel.DataAnnotations;

namespace ExceptionHandlingAssignment.Models
{
    public class UserDetails
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Name should only contain alphabets and spaces.")]
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your City.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "City Name should only contain alphabets.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile number should only in numbers.")]
        public int Mobile { get; set; }
    }
}
