using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Api.Projection.graphql.users;

public sealed class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(e => e.Id);
        descriptor.Field(e => e.Role);
        descriptor.Field(e => e.UserName);
        descriptor.Field(e => e.Gender);

        descriptor
            .Field(e => e.Email)
            .Type<NonNullType<StringType>>()
            .Resolve(e => e.Parent<User>().Email.Value);

        descriptor.Field(e => e.Salons);
        descriptor.Field(e => e.Favorites);

        descriptor.Ignore(e => e.Password);
        descriptor.Ignore(e => e.EmailVerification);
        descriptor.Ignore(e => e.PasswordEmailVerification);
    }
}


