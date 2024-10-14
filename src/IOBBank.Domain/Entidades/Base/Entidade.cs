using System.ComponentModel.DataAnnotations.Schema;

namespace IOBBank.Domain.Entidades.Base;

public abstract class Entidade : IEntidade
{
    protected Entidade()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow; // Melhor usar UTC para consistência em múltiplos fusos horários
        Status = true;
    }

    public bool Status { get; set; }
    public Guid Id { get; set; }

    [Column(TypeName = "timestamp")] // Usar timestamp ou datetime compatível com MySQL
    public DateTime DataCriacao { get; private set; }

    [Column(TypeName = "timestamp")] // Usar timestamp ou datetime compatível com MySQL
    public DateTime? DataAlteracao { get; set; }
}
