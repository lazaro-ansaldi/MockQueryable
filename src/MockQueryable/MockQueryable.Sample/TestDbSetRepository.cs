﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockQueryable.Sample
{
    public class TestDbSetRepository : IUserRepository
    {
        private readonly DbSet<UserEntity> _dbSet;

        public TestDbSetRepository(DbSet<UserEntity> dbSet)
        {
            _dbSet = dbSet;
        }
        public IQueryable<UserEntity> GetQueryable()
        {
            return _dbSet;
        }

        public async Task CreateUser(UserEntity user)
        {
            await _dbSet.AddAsync(user);
        }

        public async Task<List<UserEntity>> GetAll() {
            return await _dbSet.ToListAsync();
        }

        public IAsyncEnumerable<UserEntity> GetAllAsync()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public async Task<int> DeleteUserAsync(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<int> UpdateFirstNameByIdAsync(Guid id, string firstName)
        {
            return await _dbSet.Where(x => x.Id == id)
                .ExecuteUpdateAsync(opt => opt.SetProperty(x => x.FirstName, firstName));
        }
    }
}