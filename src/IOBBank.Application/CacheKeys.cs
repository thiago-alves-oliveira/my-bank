namespace IOBBank.Application;

public static class CacheKeys
{
    public static string PrecoCombustivelPorPosto(Guid postoId)
    {
        return $"{postoId}-combustiveis";
    }

    public static string AssociadosIntegradorPorGrupo(Guid grupoId)
    {
        return $"{grupoId}-associados";
    }
}
