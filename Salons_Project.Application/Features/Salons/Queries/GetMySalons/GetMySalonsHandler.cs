using MediatR;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Organization.Queries.GetOrganization;
using Salons_Project.Domain.Models.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Application.Features.Salons.Queries.GetMySalons
{
    public class GetMySalonsHandler : IRequestHandler<GetMySalonsQueryable, IQueryable<Salon>>
    {
        private readonly IDataContext _db;

        public GetMySalonsHandler(IDataContext db) => _db = db;

        public Task<IQueryable<Salon>> Handle(GetMySalonsQueryable req, CancellationToken cancellationToken)
        {
            IQueryable<Salon> query = _db.Salons.Where(e => e.UserId == req.userid);

            return Task.FromResult(query);  
        }
    }
}
