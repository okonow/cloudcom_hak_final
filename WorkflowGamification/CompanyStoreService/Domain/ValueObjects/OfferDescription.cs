using Domain.Common;

namespace Domain.ValueObjects
{
    public class OfferDescription : ValueObject
    {
        public OfferDescription()
        {        
        }

        public OfferDescription(string offerTitle, string mainInformation, IList<StoredFile> addedPhotos)
        {
            OfferDescriptionTitle = offerTitle;
            MainInformation = mainInformation;

            foreach (var addedPhoto in addedPhotos)
                AddedPhotos.Add(addedPhoto);
        }

        public string? OfferDescriptionTitle { get; private set; }

        public string? MainInformation { get; private set; }

        public IList<StoredFile>? AddedPhotos { get; private set; } = [];

        public void AddPhoto(StoredFile photo)
            => AddedPhotos!.Add(photo);
    }
}
