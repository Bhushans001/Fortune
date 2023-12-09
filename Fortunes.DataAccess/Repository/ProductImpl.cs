using Fortunes.DataAccess.Repository.IRepository;
using Fortunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.DataAccess.Repository
{
    public class ProductImpl : RepositoryImpl<Product>, IProducts
    {
        private readonly ApplicationDBContext _db;
        public ProductImpl(ApplicationDBContext db) : base(db) 
        {
                _db = db;   
        }

        public void update(Product product)
        {
            _db.Products.Update(product);   
        }
    }
}
