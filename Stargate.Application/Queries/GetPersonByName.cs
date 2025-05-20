﻿//using Dapper;
//using MediatR;
//using Stargate.API.Business.Data;
//using Stargate.API.Business.Dtos;
//using Stargate.API.Controllers;

//namespace Stargate.Application.Queries
//{
//    public class GetPersonByName : IRequest<GetPersonByNameResult>
//    {
//        public required string Name { get; set; } = string.Empty;
//    }

//    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
//    {
//        private readonly StargateContext _context;
//        public GetPersonByNameHandler(StargateContext context)
//        {
//            _context = context;
//        }

//        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
//        {
//            var result = new GetPersonByNameResult();

//            var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE '{request.Name}' = a.Name";

//            var person = await _context.Connection.QueryAsync<PersonAstronaut>(query);

//            result.Person = person.FirstOrDefault();

//            return result;
//        }
//    }

//    public class GetPersonByNameResult : BaseResponse
//    {
//        public PersonAstronaut? Person { get; set; }
//    }
//}
