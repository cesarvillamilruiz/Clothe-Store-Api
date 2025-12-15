namespace ClotheStore.Application.ViewModels
{
    public class AddressVM
    {
        public Guid? AddressId { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public string RoadType { get; set; }
        public string LineOne { get; set; }
        public string? ParticleOne { get; set; }
        public string? LineTwo { get; set; }
        public string? ParticleTwo { get; set; }
        public string? LineThree { get; set; }
        public string? ParticleThree { get; set; }
        public string? Complement { get; set; }

        public string City { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }

        public string Phone { get; set; }
    }
}
