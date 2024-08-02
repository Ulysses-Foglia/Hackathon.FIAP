using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Gateway
{
    public class MedicoGateway : IMedicoGateway
    {
        private readonly IDatabaseClient _database;

        public MedicoGateway(IDatabaseClient database)
        {
            _database = database;
        }

        public string GerarToken(Medico medico) => _database.GerarToken(medico);
        public IEnumerable<Medico> BuscarTodos() => _database.BuscarTodosMedicos();
        public IEnumerable<Medico> BuscarMedicosDisponibilidade() => _database.BuscarMedicosDisponibilidade();
        public Medico BuscarPorId(int id) => _database.BuscarMedicoPorId(id);
        public void Criar(Medico medico) => _database.CriarMedico(medico);
        public Medico Alterar(Medico medico) => _database.AlterarMedico(medico);
        public void Excluir(int id) => _database.ExcluirMedico(id);
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDosMedicos(int Limite) => _database.BusqueTodasAgendasDoMedico(Limite);
    }
}
