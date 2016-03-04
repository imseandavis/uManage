using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using S203.uManage.Data.Extensions;
using S203.uManage.Data.Interfaces;
using S203.uManage.Models;

namespace S203.uManage.Data.Repositories
{
    public class ActiveDirectory : IDirectory<User>
    {
        private readonly IDirectoryContext _context;
        public ActiveDirectory(IDirectoryContext context)
        {
            if (_context == null)
                _context = context;
        }

        public IEnumerable<User> All { get; }
        public User Find(string identity)
        {
            using (var ctx = _context.LoadAndConnect())
            {
                return UserPrincipal.FindByIdentity(ctx, identity).AsUser();
            }
        }

        public User Upsert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
