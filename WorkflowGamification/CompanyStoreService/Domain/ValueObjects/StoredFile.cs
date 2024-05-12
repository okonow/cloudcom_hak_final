using Domain.Common;

namespace Domain.ValueObjects
{
    public class StoredFile : ValueObject
    {
        public StoredFile()
        {
            
        }

        public StoredFile(
        string name,
        string extension,
        byte[] data)
        {
            Name = name;
            Extension = extension;
            Data = data;
        }

        public required string Name { get; init; }

        public required string Extension { get; init; }

        public required byte[] Data { get; init; }
    }
}
