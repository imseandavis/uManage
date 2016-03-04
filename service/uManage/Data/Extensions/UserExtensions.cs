using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using S203.uManage.Models;

namespace S203.uManage.Data.Extensions
{
    public static class UserExtensions
    {
        public static User AsUser(this UserPrincipal user)
        {
            return HydrateFromPrincipal(user);
        }

        public static string GetDirectoryProperty(this DirectoryEntry directoryEntry, string property)
        {
            var v = directoryEntry.Properties[property].Value;
            return v?.ToString();
        }

        private static User HydrateFromPrincipal(UserPrincipal principal)
        {
            var e = principal.GetUnderlyingObject() as DirectoryEntry;
            
            return new User
            {
                Id = principal.Guid.GetValueOrDefault(),
                UserName = principal.UserPrincipalName.ToLower(),
                DistinguishedName = principal.DistinguishedName,
                IsLocked = principal.IsAccountLockedOut(),
                IsDisabled = !principal.Enabled.GetValueOrDefault(),
                IsExpired = principal.AccountExpirationDate.HasValue && principal.AccountExpirationDate.GetValueOrDefault() <= DateTime.UtcNow,
                ExpiresOn = principal.AccountExpirationDate.GetValueOrDefault(),
                
                Name = principal.Name,
                DisplayName = principal.DisplayName,
                FirstName = principal.GivenName,
                MiddleName = principal.MiddleName,
                LastName = principal.Surname,
                Email = principal.EmailAddress,
                Website = e.GetDirectoryProperty("wWWHomePage"),

                Organization = e.GetDirectoryProperty("company"),
                Department = e.GetDirectoryProperty("department"),
                Title = e.GetDirectoryProperty("title"),
                Office = e.GetDirectoryProperty("physicalDeliveryOfficeName"),
                EmployeeId = e.GetDirectoryProperty("employeeNumber"),
                BadgeId = e.GetDirectoryProperty("employeeID"),

                Address1 = e.GetDirectoryProperty("streetAddress"),
                Address2 = e.GetDirectoryProperty("postOfficeBox"),
                City = e.GetDirectoryProperty("l"),
                Province = e.GetDirectoryProperty("st"),
                PostCode = e.GetDirectoryProperty("postalCode"),
                Country = e.GetDirectoryProperty("c"),

                HomePhone = e.GetDirectoryProperty("homephone"),
                OfficePhone = principal.VoiceTelephoneNumber,
                MobilePhone = e.GetDirectoryProperty("mobile"),
                SipPhone = e.GetDirectoryProperty("ipPhone"),
                Pager = e.GetDirectoryProperty("pager"),
                Fax = e.GetDirectoryProperty("facsimileTelephoneNumber"),
            };
        }
    }
}
