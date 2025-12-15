namespace ClotheStore.Repository.Helper
{
    public class EntitiesHelper
    {
        public List<EntitySettings> Entities { get; set; } = new List<EntitySettings>();

        public EntitiesHelper()
        {
            Entities.Add(new EntitySettings
            {
                EntityName = "VendorTest",
                SP_Delete = "dbo.VendorTestDelete @VendorId",
                SP_Insert = "dbo.VendorTestInsert @VendorId, @VendorName, @JdeVendorNumber, @DefaultRebatePercent, @ActiveVendor, @IsManufacturer, @IsSupplier, @IsVendor, @CreatedBy, @CreatedDate, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @Country, @FriendlyVendorName",
                SP_Update = "dbo.VendorTestUpdate @VendorId, @VendorName, @JdeVendorNumber, @DefaultRebatePercent, @ActiveVendor, @IsManufacturer, @IsSupplier, @IsVendor, @ModifiedBy, @ModifiedDate, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @Country, @FriendlyVendorName "
            });
        }
    }
}
