using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Stargate.Application.Commands.AstronautDuty.Create;
using Stargate.Application.Interfaces;
using Stargate.Domain.Entities;

namespace Stargate.Application.Tests.Handlers;

public class CreateAstronautDutyHandlerTests
{
    private readonly Mock<IAstronautDutyRepository> _dutyRepoMock = new();
    private readonly Mock<IPersonRepository> _personRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<CreateAstronautDutyHandler>> _loggerMock = new();

    [Fact]
    public async Task Handle_ShouldSetCareerEndDate_WhenRetiredDuty()
    {
        // Arrange
        var personId = (new Random()).Next(int.MaxValue);
        var person = new Person
        {
            Id = personId,
            Name = "Buzz"
        };
        person.AstronautDetail = new AstronautDetail { Person = person };
        person.AstronautDuties = new List<AstronautDuty>
        {
            new AstronautDuty
            {
                DutyTitle = "Pilot",
                DutyStartDate = new DateTime(2020, 1, 1),
                DutyEndDate = null,
                Person = person
            }
        };

        _personRepoMock.Setup(x => x.GetByIdAsync(personId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);

        var handler = new CreateAstronautDutyHandler(
            _dutyRepoMock.Object,
            _personRepoMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);

        var request = new CreateAstronautDutyCommand
        (
            personId, // Pass the required 'PersonId' parameter
            "Commander", // Pass the required 'Rank' parameter
            "RETIRED", // Pass the required 'DutyTitle' parameter
            new DateTime(2024, 1, 1), // Pass the required 'DutyStartDate' parameter
            null // Pass the required 'DutyEndDate' parameter
        );

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        person.AstronautDetail.CareerEndDate.Should().Be(new DateTime(2023, 12, 31));
        _dutyRepoMock.Verify(x => x.AddAsync(It.IsAny<AstronautDuty>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
