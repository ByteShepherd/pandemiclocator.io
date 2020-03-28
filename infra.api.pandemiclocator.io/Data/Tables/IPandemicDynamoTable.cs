using System;

namespace infra.api.pandemiclocator.io.Data.Tables
{
    public interface IPandemicDynamoTable
    {
        string Id { get; set; }

        public void GenerateNewId()
        {
            Id = $"{Guid.NewGuid():D}";
        }
    }
}