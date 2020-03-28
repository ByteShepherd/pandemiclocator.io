using System;
using System.Net;
using System.Net.Http;
using pandemiclocator.io.abstractions.Environment;

namespace infra.api.pandemiclocator.io.Environment
{
    public class HostInstanceProvider : IHostInstanceProvider
    {
        //Fonte: https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/instancedata-data-retrieval.html
        private const string AwsMetadataUri = "http://169.254.169.254/latest/meta-data/";

        private static (HttpStatusCode status, string metadata) RetrieveMetadata(string route = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);

                    var response = string.IsNullOrEmpty(route)
                        ? client.GetAsync(AwsMetadataUri).Result
                        : client.GetAsync(string.Concat(AwsMetadataUri, route)).Result;

                    var metadata = response.StatusCode == HttpStatusCode.OK ? response.Content.ReadAsStringAsync().Result : string.Empty;
                    return (response.StatusCode, metadata);
                }
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static readonly object IsRunningOnAwsLocker = new object();
        private static bool? _isRunningOnAws;
        public bool IsRunningOnCloud
        {
            get
            {
                if (!_isRunningOnAws.HasValue)
                {
                    lock (IsRunningOnAwsLocker)
                    {
                        if (!_isRunningOnAws.HasValue)
                        {
                            var metadataResponse = RetrieveMetadata();
                            _isRunningOnAws = metadataResponse.status == HttpStatusCode.OK;
                        }
                    }
                }

                return _isRunningOnAws.Value;
            }
        }

        private const int DefaultShrinkMertadataInstanceSize = 64;
        private static readonly object InstanceIdLocker = new object();
        private static string _instanceId;
        public string HostInstanceId
        {
            get
            {
                if (_instanceId == null)
                {
                    lock (InstanceIdLocker)
                    {
                        if (_instanceId == null)
                        {
                            string metadataResponse = null;
                            var (status, metadata) = RetrieveMetadata("instance-id");
                            if (status == HttpStatusCode.OK && !string.IsNullOrEmpty(metadata))
                            {
                                metadataResponse = metadata.Length > DefaultShrinkMertadataInstanceSize 
                                    ? metadata.Substring(0, DefaultShrinkMertadataInstanceSize) 
                                    : metadata;
                            }

                            if (string.IsNullOrEmpty(metadataResponse))
                            {
                                metadataResponse = System.Environment.MachineName;
                            }

                            _instanceId = metadataResponse;
                        }
                    }
                }

                return _instanceId;
            }
        }
    }
}
