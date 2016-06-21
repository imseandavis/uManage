using System.DirectoryServices.AccountManagement;

namespace S203.uManage.Data.Interfaces
{
    public interface IDirectoryContext
    {
        PrincipalContext LoadAndConnect();
    }
}
