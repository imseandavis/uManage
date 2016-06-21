using System.Collections.Generic;

namespace S203.uManage.Models
{
    public class Information
    {
        public string Application { get; set; }
        public string Version { get; set; }
        public string ApiVersion { get; set; }
        public string CurrentUser { get; set; }
        public Dictionary<string, bool> Vitals { get; set; }
        // ReSharper disable once InconsistentNaming
        public Dictionary<string, Link> _links { get; set; }

        public Information()
        {
            // Base Vital
            if (Vitals == null)
                Vitals = new Dictionary<string, bool>();

            // Base Links
            if (_links == null)
                _links = new Dictionary<string, Link>();
        }
    }
}
