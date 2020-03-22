using System;

namespace api.pandemiclocator.io.Infra.Data.Tables
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