using MediatR;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Application.Features.Users.Queries.GetUser;
public sealed class GetUserByIdQueryableHandler : IRequestHandler<GetUserByIdQueryable, IQueryable<User>>
{
    private readonly IDataContext _db;

    public GetUserByIdQueryableHandler(IDataContext db) => _db = db;

    public Task<IQueryable<User>> Handle(GetUserByIdQueryable req, CancellationToken ct)
    {
        IQueryable<User> query = _db.Users.Where(e => e.Id == req.Id);

        return Task.FromResult(query);
    }
}