using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Auth;

public class SignInDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"Login\" é obrigatório e não pode ser uma string vazia.")]
    public string Login { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"Password\" é obrigatório e não pode ser uma string vazia.")]
    public string Password { get; set; }
}