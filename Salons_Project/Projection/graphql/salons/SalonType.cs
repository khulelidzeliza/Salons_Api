using Salons_Project.Api.Projection.graphql.users;
using Salons_Project.Domain.Models.Organization;
using System.Security.Claims;

namespace Salons_Project.Api.Projection.graphql.salons;

public class SalonType : ObjectType<Salon>
{
    protected override void Configure(IObjectTypeDescriptor<Salon> descriptor)
    {
        descriptor.Field(s => s.Id);
        descriptor.Field(s => s.Name);
        descriptor.Field(s => s.Location);
        descriptor.Field(s => s.PhoneNumber);
        descriptor.Field(s => s.Email);
        descriptor.Field(s => s.Price);
        descriptor.Field(s => s.WorkingHours);
        descriptor.Field(s => s.FacebookLink);
        descriptor.Field(s => s.InstagramLink);
        descriptor.Field(s => s.Pictures);


        // relationsss
        descriptor.Field(s => s.Reviews);
        descriptor.Field(s => s.Procedures);
        descriptor.Field(s => s.UserId);


        descriptor.Field("AverageRating")
            .Type<FloatType>()
             .Resolve(ctx =>
             {
                 var salon = ctx.Parent<Salon>();

                 if (salon.Reviews is null || salon.Reviews.Count == 0)
                     return 0d;

                 return Math.Round(salon.Reviews.Average(r => (int)r.star + 1), 1);
             });

        descriptor
            .Field("owner")
            .Type<UserType>()
            .Resolve(ctx => ctx.Parent<Salon>().User);


        descriptor.Field("isFavoritedByCurrentUser")
          .Type<BooleanType>()
          .Resolve(ctx =>
          {
              var salon = ctx.Parent<Salon>();

              var httpContextAccessor = ctx.Service<IHttpContextAccessor>();
              var httpContext = httpContextAccessor.HttpContext;

              var userIdClaim = httpContext?.User?.FindFirst("userId")?.Value
                  ?? httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

              if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var currentUserId))
              {
                  return false;
              }

              return salon.Favorites?.Any(f => f.UserId == currentUserId) ?? false;
          });
    }
}
