using System.ComponentModel.DataAnnotations;

namespace Engine.Edit.Models
{
    public class ApartmentMailModel
    {

            [Required(ErrorMessage = "Imię jest wymagane")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Nieprawidłowy format email")]
            public string Email { get; set; }

            public string Phone { get; set; }

            [Required(ErrorMessage = "Wiadomość jest wymagana")]
            public string Message { get; set; }

            public int ApartmentId { get; set; } // id apartamentu

    }
}
