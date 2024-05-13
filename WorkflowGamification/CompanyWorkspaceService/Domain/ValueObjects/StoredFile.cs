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

        public string Name { get; init; }

        public string Extension { get; init; }

        public byte[] Data { get; init; }
    }
}
