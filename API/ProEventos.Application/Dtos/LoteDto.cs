using System;
using System.Collections.Generic;
using System.Text;

namespace ProEventos.Application.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public EventosDto Evento { get; set; }
    }
}
