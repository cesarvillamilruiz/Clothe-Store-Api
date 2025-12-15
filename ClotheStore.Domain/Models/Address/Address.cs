namespace ClotheStore.Domain.Models.Address
{
    public class Address
    {
        public Guid? AddressId { get; set; }
        public Guid? UserId { get; set; }

        public string Name { get; set; } = null!;
        public string Type { get; set; }
        public string RoadType { get; set; } = null!;
        public string LineOne { get; set; } = null!;
        public string? ParticleOne { get; set; }
        public string? LineTwo { get; set; }
        public string? ParticleTwo { get; set; }
        public string? LineThree { get; set; }
        public string? ParticleThree { get; set; }
        public string? Complement { get; set; }

        public string City { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Country { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
