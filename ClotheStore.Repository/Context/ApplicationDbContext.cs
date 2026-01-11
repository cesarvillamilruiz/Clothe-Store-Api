using ClotheStore.Domain.Models.Address;
using ClotheStore.Domain.Models.Color;
using ClotheStore.Domain.Models.ContactPreference;
using ClotheStore.Domain.Models.Item;
using ClotheStore.Domain.Models.Option;
using ClotheStore.Domain.Models.User;
using ClotheStore.Repository.EntityDataHandlers;
using ClotheStore.Repository.Repositories.Interceptor;
using Microsoft.EntityFrameworkCore;

namespace ClotheStore.Repository.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        #region DbSet Properties
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<ContactPreference> ContactPreference { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Customization> Customization { get; set; }
        public virtual DbSet<OptionColor> OptionColor { get; set; }
        public virtual DbSet<OptionSize> OptionSize { get; set; }
        public virtual DbSet<OptionFont> OptionFont { get; set; }
        #endregion DbSet Properties

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Ignore(e => e.CreatedAt);
                entity.Ignore(e => e.UpdatedAt);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Ignore(e => e.CreatedAt);
                entity.Ignore(e => e.UpdatedAt);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.AddInterceptors(new QueryCommandInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = 0;
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            if (!entries.Any()) return result;

            using (var transaction = await Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entry in entries)
                    {
                        IEntityDataHandler entityDataHandler;

                        switch (entry.Entity)
                        {
                            case Address Address:
                                entityDataHandler = new AddressEntityDataHandler(this);
                                break;
                            case ContactPreference ContactPreference:
                                entityDataHandler = new ContactPreferenceEntityDataHandler(this);
                                break;
                            case CartItem CartItem:
                                entityDataHandler = new CartItemEntityDataHandler(this);
                                break;
                            case OptionColor Color:
                                entityDataHandler = new ColorEntityDataHandler(this);
                                break;
                            case Customization Customization:
                                entityDataHandler = new CustomizationEntityDataHandler(this);
                                break;
                            case OptionFont OptionFont:
                                entityDataHandler = new FontEntityDataHandler(this);
                                break;
                            case OptionSize OptionSize:
                                entityDataHandler = new SizeEntityDataHandler(this);
                                break;
                            default:
                                continue;
                        }

                        if (entry.State == EntityState.Added)
                        {
                            var entity = await entityDataHandler.Insert(entry);
                            if (entity != null) result++;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            var entity = await entityDataHandler.Update(entry);
                            if (entity != null) result++;
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            var entity = await entityDataHandler.Delete(entry);
                            if (entity) result++;
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    result = 0;
                    transaction.Rollback();
                    throw;
                }
            }

            //TODO: Test if we need to EF to do something else, in theory all custom entities are up to date
            //return base.SaveChangesAsync();

            return result;
        }
    }
}
