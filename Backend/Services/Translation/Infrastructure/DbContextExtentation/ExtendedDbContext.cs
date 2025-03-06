
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace WebAPI.DatabaseExtensions;

//public abstract class ExtendedDbContext : DbContext
//{
//    private readonly IServiceProvider _provider;

//    protected ExtendedDbContext(DbContextOptions options, IServiceProvider provider) : base(options)
//    {
//        _provider = provider;
//    }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder
//            .EnableSensitiveDataLogging()
//            .EnableDetailedErrors()
//            .AddInterceptors(
//                new LongExecutionWarningInterceptor(_provider.GetService<IConfiguration>()!)
//            );
//    }

//    public override int SaveChanges()
//    {
//        HandleEntities();

//        return base.SaveChanges();
//    }

//    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
//        CancellationToken cancellationToken = default)
//    {
//        HandleEntities();

//        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
//    }

//    private void HandleEntities()
//    {
//        using (var scope = _provider.CreateScope())
//        {
//            var accessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

//            if (accessor is not null)
//                HandleChangeTracking(HelperUserProfile.GetUserProfile(accessor.HttpContext!.User));
//        }

//        HandleVersioning();
//    }

//    private void HandleChangeTracking(UserProfile userProfile)
//    {
//        foreach (var entity in ChangeTracker.Entries<IEntityBase>().Where(x => x.State == EntityState.Added))
//        {
//            ModifyPropertyValue(entity.Property(o => o.Active), true);

//            ModifyPropertyValue(entity.Property(o => o.CreatedBy), userProfile.Id);

//            ModifyPropertyValue(entity.Property(o => o.CreatedDate), DateTime.Now);
//        }

//        foreach (var entity in
//                 ChangeTracker.Entries<IEntityBase>().Where(x => x.State == EntityState.Modified))
//        {
//            entity.Property(o => o.CreatedBy).IsModified = false;

//            entity.Property(o => o.CreatedDate).IsModified = false;

//            ModifyPropertyValue(entity.Property(o => o.LastModifiedBy), userProfile.Id);

//            ModifyPropertyValue(entity.Property(o => o.LastModifiedDate), DateTime.Now);
//        }
//    }

//    private static void ModifyPropertyValue<T>(PropertyEntry<IEntityBase, T> property, T value)
//    {
//        property.CurrentValue = value;
//        property.IsModified = true;
//    }

//    public void HandleVersioning()
//    {
//        foreach (var versionedEntity in ChangeTracker.Entries<IVersionedEntity>())
//        {
//            var versionProperty = versionedEntity.Property(o => o.Version);

//            switch (versionedEntity.State)
//            {
//                case EntityState.Added:
//                    versionProperty.CurrentValue = 1;
//                    break;
//                case EntityState.Modified:
//                    versionProperty.CurrentValue = versionProperty.OriginalValue + 1;
//                    versionProperty.IsModified = true;
//                    break;
//                case EntityState.Detached:
//                    break;
//                case EntityState.Unchanged:
//                    break;
//                case EntityState.Deleted:
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//        }
//    }
//}