using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackerconfig.Utilities
{
    class TrackerBlobProperties : ConfigurationSection
    {
        [ConfigurationProperty("StorageConnectionString", IsRequired = true)]
        public string StorageConnectionString {
            get
            {
                return (string)this["StorageConnectionString"];
            }
        }

        [ConfigurationProperty("BlobContainerName", IsRequired = true)]
        public string BlobContainerName
        {
            get
            {
                return (string)this["BlobContainerName"];
            }
        }
    }
}
