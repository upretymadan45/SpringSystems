using FluentValidation;

namespace Spring_Systems.Pages.Company;
public class NewCompanyModel : PageModel
{
    private readonly ILogger<NewCompanyModel> _logger;
    private readonly IValidator<CompanyDTO> _validator;
    private readonly ICompanyRepository _companyRepository;

    [BindProperty]
    public CompanyDTO CompanyDTO { get; set; } = null!;
    public NewCompanyModel(ILogger<NewCompanyModel> logger,
        IValidator<CompanyDTO> validator,
        ICompanyRepository companyRepository)
    {
        _logger = logger;
        _validator = validator;
        _companyRepository = companyRepository;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(CompanyDTO);
            if(!validationResult.IsValid)
                return Page();

            var company = new Spring.Domain.Company(
                name: CompanyDTO.Name,
                address: CompanyDTO.Address,
                addedBy: "admin"
                );

            await _companyRepository.Create(company,cancellationToken);

            return RedirectToPage("Index");
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not create company");
            throw;
        }
    }
}
