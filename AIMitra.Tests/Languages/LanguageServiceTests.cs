using Application.Languages;
using AutoMapper;
using Contracts.Languages;
using Domain.Languages;
using Moq;

namespace AIMitra.Tests.Languages;

public class LanguageServiceTests
{
    private readonly Mock<ILanguageRepository> _languageRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly LanguageService _sut;

    public LanguageServiceTests()
    {
        _languageRepositoryMock = new Mock<ILanguageRepository>();
        _mapperMock = new Mock<IMapper>();
        _sut = new LanguageService(
            _languageRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnMappedLanguages()
    {
        var languages = new List<Language>
        {
            new() { Id = Guid.NewGuid(), Code = "en", Name = "English" },
            new() { Id = Guid.NewGuid(), Code = "np", Name = "Nepali" }
        };

        var expectedDtos = languages.Select(l => new LanguageDto
        {
            Id = l.Id,
            Code = l.Code,
            Name = l.Name
        }).ToList();

        _languageRepositoryMock
            .Setup(r => r.GetListAsync())
            .ReturnsAsync(languages);

        _mapperMock
            .Setup(m => m.Map<List<Language>, List<LanguageDto>>(languages))
            .Returns(expectedDtos);

        var result = await _sut.GetListAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("en", result[0].Code);
        Assert.Equal("Nepali", result[1].Name);
        _languageRepositoryMock.Verify(r => r.GetListAsync(), Times.Once);
    }

    [Fact]
    public async Task GetListAsync_WhenNoLanguages_ShouldReturnEmptyList()
    {
        _languageRepositoryMock
            .Setup(r => r.GetListAsync())
            .ReturnsAsync(new List<Language>());

        _mapperMock
            .Setup(m => m.Map<List<Language>, List<LanguageDto>>(It.IsAny<List<Language>>()))
            .Returns(new List<LanguageDto>());

        var result = await _sut.GetListAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetListAsync_ShouldCallRepositoryExactlyOnce()
    {
        _languageRepositoryMock
            .Setup(r => r.GetListAsync())
            .ReturnsAsync(new List<Language>());

        _mapperMock
            .Setup(m => m.Map<List<Language>, List<LanguageDto>>(It.IsAny<List<Language>>()))
            .Returns(new List<LanguageDto>());

        await _sut.GetListAsync();

        _languageRepositoryMock.Verify(r => r.GetListAsync(), Times.Once);
        _mapperMock.Verify(
            m => m.Map<List<Language>, List<LanguageDto>>(It.IsAny<List<Language>>()),
            Times.Once);
    }
}
