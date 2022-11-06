using Spring_Systems.Pages.Employee;
namespace Spring.UnitTests.EmployeeTests;
public class NewEmployeeTests : BaseTest<EmployeeDTO>
{
    private Mock<ILogger<NewEmployeeModel>> _loggerMock;
    private Mock<IEmployeeRepository> _employeeRepositoryMock;
    private Mock<ICompanyRepository> _companyRepositoryMock;
    private EmployeeDTO _employee;
    private NewEmployeeModel _sut;
    protected override void SetupServices()
    {
        base.SetupServices();

        _loggerMock = new();
        _employeeRepositoryMock = new();
        _companyRepositoryMock = new();

        _companyRepositoryMock.Setup(x => x.GetAllCompanies(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Fixture.Create<IEnumerable<Domain.Company>>());

        _employee = _GetEmployeeFixture;
        
        _sut = new NewEmployeeModel(_loggerMock.Object, _employeeRepositoryMock.Object, _companyRepositoryMock.Object, Validator);
        _sut.Employee = _employee;

        Services.TryAddScoped<ILogger<NewEmployeeModel>>(f => _loggerMock.Object);
        Services.TryAddScoped<IEmployeeRepository>(f => _employeeRepositoryMock.Object);
        Services.TryAddScoped<ICompanyRepository>(f => _companyRepositoryMock.Object);
    }

    [Test]
    public async Task NewEmployee_OnPost_Should_Succeed()
    {
        RedirectToPageResult result = (RedirectToPageResult)await _sut.OnPost(new CancellationToken());

        result.Should().NotBeNull();
        result.PageName.Should().Be("Index");
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_WhenEmployeeModelIs_NotValid()
    {
        _employee = Fixture.Create<EmployeeDTO>();
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());

        result.Should().BeOfType<PageResult>();
    }

    [TestCase("")]  // case: empty first name
    [TestCase("A")] // case: single char first name
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_FirstNameIsInvalid(string firstName)
    {
        _employee.FirstName = firstName;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee,new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.FirstName));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase("")]  // case: empty last name
    [TestCase("A")] // case: single char last name
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_LastNameIsInvalid(string lastName)
    {
        _employee.LastName = lastName;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee, new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.LastName));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase("")]  // case: empty address
    [TestCase("A")] // case: single char address
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_AddressIsInvalid(string address)
    {
        _employee.Address = address;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee, new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.Address));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase("")]  // case: empty email
    [TestCase("invalid.email.com")] // case: invalid email
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_EmailIsInvalid(string email)
    {
        _employee.Email = email;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee, new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.Email));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase("")]  // case: empty contact
    [TestCase("abc1234-")] // case: invalid contact
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_ContactIsInvalid(string contact)
    {
        _employee.ContactNo = contact;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee, new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.ContactNo));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase(0)]  // case: invalid company id
    [TestCase(-1)] // case: invalid company id
    public async Task NewEmployee_OnPost_Should_ReturnPageResult_And_ValidationException_When_ContactIsInvalid(long companyId)
    {
        _employee.CompanyId = companyId;
        _sut.Employee = _employee;

        var result = await _sut.OnPost(new CancellationToken());
        var validationResult = await Validator.ValidateAsync(_employee, new CancellationToken());

        result.Should().BeOfType<PageResult>();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(p => p.PropertyName == nameof(EmployeeDTO.CompanyId));
        _employeeRepositoryMock.Verify(x => x.Create(It.IsAny<Domain.Employee>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    #region Helpers
    private EmployeeDTO _GetEmployeeFixture => new EmployeeDTO
    {
        FirstName = Fixture.Create<string>(),
        MiddleName = Fixture.Create<string>(),
        LastName = Fixture.Create<string>(),
        Address = Fixture.Create<string>(),
        Email = $"{Fixture.Create<EmailAddressLocalPart>()}@{Fixture.Create<DomainName>()}",
        ContactNo = "123-456789",
        CompanyId = Random.Next(1,10)
    };
    #endregion
}
