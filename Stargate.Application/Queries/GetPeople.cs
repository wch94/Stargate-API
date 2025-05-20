﻿//using Dapper;
//using MediatR;
//using Stargate.API.Business.Data;
//using Stargate.API.Business.Dtos;
//using Stargate.API.Controllers;

//namespace Stargate.Application.Queries
//{
//    public class GetPeople : IRequest<GetPeopleResult>
//    {

//    }

//    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
//    {
//        public readonly StargateContext _context;
//        public GetPeopleHandler(StargateContext context)
//        {
//            _context = context;
//        }
//        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
//        {
//            var result = new GetPeopleResult();

//            var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id";

//            var people = await _context.Connection.QueryAsync<PersonAstronaut>(query);

//            result.People = people.ToList();

//            return result;
//        }
//    }

//    public class GetPeopleResult : BaseResponse
//    {
//        public List<PersonAstronaut> People { get; set; } = new List<PersonAstronaut> { };

//    }
//}
