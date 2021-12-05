using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models
{
    public class SimpleInterestModel
    {
        [Required(ErrorMessage = "Price is Required")]
        public string Price {get; set;}

        [Required(ErrorMessage = "Time is Required")]
        public string Time {get; set;}

        public string Interest {get; set;}
    }
}
