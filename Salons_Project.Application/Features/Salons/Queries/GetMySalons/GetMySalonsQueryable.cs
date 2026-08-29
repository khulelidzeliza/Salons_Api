using MediatR;
using Salons_Project.Domain.Models.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Application.Features.Salons.Queries.GetMySalons;

public sealed record GetMySalonsQueryable(int userid) : IRequest<IQueryable<Salon>>;
