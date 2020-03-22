using System;
using System.Collections.Generic;
using System.Text;

namespace infra.api.pandemiclocator.io.ConfigurationSecions
{
    public class DynamoDbConnectionSection
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string System { get; set; }
    }
}
