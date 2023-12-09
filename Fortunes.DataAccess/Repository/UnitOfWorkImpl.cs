using Fortunes.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.DataAccess.Repository
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        public ICategoryRepository Category {  get; private set; }
        public IProducts products { get; private set; } 
        private readonly ApplicationDBContext _db;
        public UnitOfWorkImpl(ApplicationDBContext db)
        {
                _db = db;
            Category = new CategoryImpl(_db);  
            products = new ProductImpl(_db);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
