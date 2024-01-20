using Application.Features.Hotels.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.ApplicationTests.HotelTests;

public class HotelBusinessRulesTests
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly HotelBusinessRules _hotelBusinessRules;

    public HotelBusinessRulesTests()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _hotelBusinessRules = new HotelBusinessRules(_mockHotelRepository.Object);
    }

    [Fact]
    public async Task CompanyNameCannotBeDuplicatedWhenInserted_WhenCompanyNameIsUnique_ShouldPass()
    {
        // Arrange

        const string companyName = "NeredeKal";

        _mockHotelRepository.Setup(m => m.GetAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync((Hotel?)null);

        // Act and Assert

        await _hotelBusinessRules.CompanyNameCannotBeDuplicatedWhenInserted(companyName);       
    }

    [Fact]
    public async Task CompanyNameCannotBeDuplicatedWhenInserted_WhenCompanyNameAlreadyExists_ShouldThrowException()
    {
        // Arrange

        const string companyName = "NeredeKal";

        _mockHotelRepository.Setup(m => m.GetAsync(
            It.IsAny<Expression<Func<Hotel, bool>>>(),
            It.IsAny<Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Hotel { CompanyName = companyName });

        // Act

        async Task Act() 
        {
            await _hotelBusinessRules.CompanyNameCannotBeDuplicatedWhenInserted(companyName);

        }

        // Assert

        await Assert.ThrowsAsync<BusinessException>(Act);
    }
}
