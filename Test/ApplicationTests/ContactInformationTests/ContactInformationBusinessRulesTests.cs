using Application.Features.ContactInformations.Rules;
using Application.Features.Hotels.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.ApplicationTests.ContactInformationTests;

public class ContactInformationBusinessRulesTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly ContactInformationBusinessRules _contactInformationBusinessRules;

    public ContactInformationBusinessRulesTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _contactInformationBusinessRules = new ContactInformationBusinessRules(_mockHotelRepository.Object);
    }

    [Fact]
    public async Task HotelIdMustBeExistsWhenInserted_WhenHotelIdIsNotExists_ShouldPass()
    {
        Guid hotelId = Guid.NewGuid();

        _mockHotelRepository.Setup(m => m.AnyAsync(
            It.IsAny<Expression<Func<Hotel,bool>>>(),
            It.IsAny<bool>(), 
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(true);

        // Act and Assert

        await _contactInformationBusinessRules.HotelIdMustBeExistsWhenInserted(hotelId);
    }

    [Fact]
    public async Task HotelIdMustBeExistsWhenInserted_WhenHotelIdIsExists_ShouldNotPass()
    {
        Guid hotelId = Guid.NewGuid();

        _mockHotelRepository.Setup(m => m.AnyAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(false);

        // Act 

        async Task Act()
        {
            await _contactInformationBusinessRules.HotelIdMustBeExistsWhenInserted(hotelId);
        }

        // Assert

        await Assert.ThrowsAsync<BusinessException>(Act);
    }
}
