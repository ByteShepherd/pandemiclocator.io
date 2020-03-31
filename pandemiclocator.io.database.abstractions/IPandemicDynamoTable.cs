using System;

namespace pandemiclocator.io.database.abstractions
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