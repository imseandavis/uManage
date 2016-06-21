using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using S203.uManage.Data.Interfaces;

namespace S203.uManage.Data.Repositories
{
    public class ActiveDirectoryContext : IDirectoryContext
    {
        private readonly string _directory;
        private readonly string _container;
        private readonly string _username;
        private readonly string _password;

        public ActiveDirectoryContext()
        {
            _directory = ConfigurationManager.AppSettings["ldap.directory"];
            _container = ConfigurationManager.AppSettings["ldap.container"];
            _username = ConfigurationManager.AppSettings["ldap.username"];
            _password = ConfigurationManager.AppSettings["ldap.password"];
        }

        public PrincipalContext LoadAndConnect()
        {
            return new PrincipalContext(ContextType.Domain,
                _directory,
                _container,
                ContextOptions.Negotiate,
                _username,
                _password);
        }
    }
}
