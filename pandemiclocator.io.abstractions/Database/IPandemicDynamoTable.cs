using System;

namespace pandemiclocator.io.abstractions.Database
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