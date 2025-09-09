using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Gestão_de_Estoque.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }

        public DateOnly DataCriacao { get; init; } = DateOnly.FromDateTime(DateTime.Now);

        public string Telefone { get; set; }

        public DateOnly? DataNascimento { get; set; }
    }
}
