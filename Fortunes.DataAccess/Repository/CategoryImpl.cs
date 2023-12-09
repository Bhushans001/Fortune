using Fortunes.DataAccess.Repository.IRepository;
using Fortunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.DataAccess.Repository
{
    internal class CategoryImpl : RepositoryImpl<Category>,ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryImpl(ApplicationDBContext db) : base(db) 
        {
            _db = db;   
        }
        public void update(Category obj)
        {
            _db.Category.Update(obj);   
        }
    }
}
