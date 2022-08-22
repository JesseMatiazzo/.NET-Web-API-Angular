using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    public class EventosDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        //[MinLength(3, ErrorMessage ="{0} deve ter no minimo {1} caracteres")]
        //[MaxLength(50, ErrorMessage ="{0} deve ter no máximo {1} caracteres")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Tema { get; set; }
        [Display(Name = "Quantidade Pessoas")]
        [Range(1, 120000, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public int QtdPessoas { get; set; }
        [RegularExpression(@".*.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem inválida")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O campo {0} deve ser válido")]
        public string Telefone { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }
    }
}
