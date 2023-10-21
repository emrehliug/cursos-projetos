using ProEventos.Domain;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [MinLength(3, ErrorMessage = "O campo {0} deve ter no minimo 4 caracters.")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no maximo 50 caracters.")]
        public string Tema { get; set; }
        [Display(Name = "Qtd de pessoas")]
        [Range(50,1000, ErrorMessage = "O numero minino de pessoas é de 50 e o maximo é 1000")]
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem valida...")]
        
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [Phone(ErrorMessage = "O {0} esta invalido e fora do padrão.")]
        public string Telefone { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [EmailAddress(ErrorMessage = "O campo {0} precisa de um e-mail valido!")]
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrante { get; set; }

    }
}
