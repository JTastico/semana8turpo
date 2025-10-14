// Archivo: Repositories/ClientRepository.cs
using Lab8_JamilTurpo.Data;
using Lab8_JamilTurpo.Models;
using Lab8_JamilTurpo.Repositories.Interfaces;

namespace Lab8_JamilTurpo.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(LINQExampleContext context) : base(context)
    {
    }
}