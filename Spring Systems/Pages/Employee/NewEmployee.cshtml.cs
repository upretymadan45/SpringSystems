using FluentValidation;

namespace Spring_Systems.Pages.Employee
{
    public class NewEmployeeModel : PageModel
    {
        private readonly ILogger<NewEmployeeModel> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IValidator<EmployeeDTO> _validator;

        [BindProperty]
        public EmployeeDTO Employee { get; set; } = new();
        public SelectList CompaniesList { get; set; } = null!;

        public NewEmployeeModel(ILogger<NewEmployeeModel> logger,
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IValidator<EmployeeDTO> validator)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _validator = validator;
        }
        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            try
            {
                await _GetCompanySelectList(cancellationToken);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading employee form");
                throw;
            }
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(Employee, cancellationToken);
                if (!validationResult.IsValid)
                {
                    await _GetCompanySelectList(cancellationToken);
                    return Page();
                }

                var employee = new Spring.Domain.Employee(
                    firstName: Employee.FirstName,
                    middleName: Employee.MiddleName!,
                    lastName: Employee.LastName,
                    address: Employee.Address,
                    email: Employee.Email,
                    contactNo: Employee.ContactNo,
                    companyId: Employee.CompanyId,
                    addedBy: "admin");

                await _employeeRepository.Create(employee, cancellationToken);

                return RedirectToPage("Index");
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating employee {employee}",Employee);
                throw;
            }
        }

        private async Task _GetCompanySelectList(CancellationToken cancellationToken=default)
        {
            var companies = await _companyRepository.GetAllCompanies(cancellationToken);
            CompaniesList = new SelectList(companies, "Id", "Name");
        }
    }
}
