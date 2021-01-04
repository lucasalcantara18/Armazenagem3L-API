using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories {
    public interface IMotoristaRepository {
        Motorista FindById(int Id);
        
        Motorista FindByLogin(string login);

        Motorista FindByEmail(string email);

        void Add(Motorista motorista);

        void Update(Motorista motorista);

        bool SaveChanges();

    }
}
