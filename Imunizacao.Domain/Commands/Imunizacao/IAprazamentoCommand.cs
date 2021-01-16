namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IAprazamentoCommand
    {
        string GetAprazamentoByCidadao { get; }
        string GetNewId { get; }
        string GetAprazamentoByCalendarioBasico { get; }
        string Insert { get; }
        string UpdateAprazamentoVacinados { get; }
        string PermiteDeletar { get; }
        string Delete { get; }

        //Gera Aprazamento por indivíduo
        string GeraAprazamentoPopGeralByIndividuo { get; }
        string GeraAprazamentoFemininoByIndividuo { get; }
        string GeraAprazamentoMasculinoByIndividuo { get; }
        string GeraAprazamentoDeficienciaByIndividuo { get; }
        string GeraAprazamentoGestacaoByIndividuo { get; }
        string GeraAprazamentoPuerperaByIndividuo { get; }

        string GeraAprazamentoCalendarioBasico { get; }

        //Gera aprazamento sem parametro
        string GeraAprazamentoPopGeral { get; }
        string GeraAprazamentoFeminino { get; }
        string GeraAprazamentoMasculino { get; }
        string GeraAprazamentoDeficiencia { get; }
        string GeraAprazamentoGestacao { get; }
        string GeraAprazamentoPuerpera { get; }
    }
}
