using Fortunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.DataAccess.Repository.IRepository
{
    public interface IProducts : IRepository<Product>
    {
        void update(Product product);
    }
}
