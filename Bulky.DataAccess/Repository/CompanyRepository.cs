﻿using BookBazaar.DataAccess.Data;
using BookBazaar.DataAccess.Repository.IRepository;
using BookBazaar.Models;

namespace BookBazaar.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
