namespace Spring.UnitTests.CompanyTests;
public class NewCompanyTests : BaseTest<CompanyDTO>
{
    private Mock<ILogger<NewCompanyModel>> _loggerMock;
    private Mock<ICompanyRepository> _companyRepositoryMock;
    private NewCompanyModel _sut;
    private CompanyDTO _companyDTO;

    protected override void SetupServices()
    {
        base.SetupServices();

        _loggerMock = new();
        _companyRepositoryMock = new();
        _companyDTO = Fixture.Create<CompanyDTO>();

        _sut = new NewCompanyModel(_loggerMock.Object, Validator, _companyRepositoryMock.Object);
        _sut.CompanyDTO = _companyDTO;

        Services.TryAddScoped<ICompanyRepository>(f => _companyRepositoryMock.Object);
        Services.TryAddScoped<ILogger<NewCompanyModel>>(f => _loggerMock.Object);
    }

    [Test]
    public async Task NewCompany_OnPost_Should_Succeed()
    {
        RedirectToPageResult result = (RedirectToPageResult)await _sut.OnPost(new CancellationToken());

        result.Should().NotBeNull();
        result.PageName.Should().Be("Index");
        _companyRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Company>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task NewCompany_OnPost_ShouldThrowValidationException_When_CompanyNameIsEmpty()
    {
        _companyDTO = new CompanyDTO { Address = Fixture.Create<string>(), Name = "" };
        _sut.CompanyDTO = _companyDTO;

        var validationResult = await Validator.ValidateAsync(_companyDTO,new CancellationToken());
        var response = await _sut.OnPost(new CancellationToken());

        response.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(CompanyDTO.Name));
    }

    [Test]
    public async Task NewCompany_OnPost_ShouldThrowValidationException_When_CompanyNameIsLessThan2Characters()
    {
        _companyDTO = new CompanyDTO { Address = Fixture.Create<string>(), Name = "A" };
        _sut.CompanyDTO = _companyDTO;

        var validationResult = await Validator.ValidateAsync(_companyDTO, new CancellationToken());
        var response = await _sut.OnPost(new CancellationToken());

        response.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(CompanyDTO.Name));
    }

    [Test]
    public async Task NewCompany_OnPost_ShouldThrowValidationException_When_CompanyAddressIsEmpty()
    {
        _companyDTO = new CompanyDTO { Address = string.Empty, Name = Fixture.Create<string>() };
        _sut.CompanyDTO = _companyDTO;

        var validationResult = await Validator.ValidateAsync(_companyDTO, new CancellationToken());
        var response = await _sut.OnPost(new CancellationToken());

        response.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(CompanyDTO.Address));
    }

    [Test]
    public async Task NewCompany_OnPost_ShouldThrowValidationException_When_CompanyAddressIsLessThan2Characters()
    {
        _companyDTO = new CompanyDTO { Address = "A", Name = Fixture.Create<string>() };
        _sut.CompanyDTO = _companyDTO;

        var validationResult = await Validator.ValidateAsync(_companyDTO, new CancellationToken());
        var response = await _sut.OnPost(new CancellationToken());

        response.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(CompanyDTO.Address));
    }
}
