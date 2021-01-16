using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IFamiliaRepository
    {
        Familia GetFamiliaById(string ibge, int id);
        int GetNewId(string ibge);
        void Insert(string ibge, Familia model);
        void Update(string ibge, Familia model);
        FamiliaProntuarioQtdeViewModel FamiliaProntuarioQtde(string ibge, int profissional);
        void AtualizaCadPacFamilia(string ibge, int? id_familia, int id_responsavel);
        int GetIndividuoFamilia(string ibge, int id);
        void AtualizaResponsavelFamilia(string ibge, int id, int responsavel);
        void AtualizaFamiliaOutraArea(string ibge, int id);
        void AtualizaFamiliaDomicilio(string ibge, int familia, int domicilio);
        List<Familia> GetFamiliaByIndividuoResponsavel(string ibge, int responsavel);
    }
}
