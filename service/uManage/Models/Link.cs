using System.Text.RegularExpressions;

namespace S203.uManage.Models
{
    public class Link
    {
        public string Href { get; set; }
        public bool? IsTemplated
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Href) && IsTemplatedRegex.IsMatch(Href))
                    return true;
                return null;
            }
        }

        private static readonly Regex IsTemplatedRegex = new Regex(@"{.+}", RegexOptions.Compiled);

        public Link(string href)
        {
            Href = href;
        }
    }
}
