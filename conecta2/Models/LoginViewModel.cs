using System.ComponentModel.DataAnnotations;

namespace conecta2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio.")]
        public string Password { get; set; }

    }
}
