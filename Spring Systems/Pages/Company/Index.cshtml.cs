using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spring.Application.DTO;

namespace Spring_Systems.Pages.Company
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICompanyRepository _companyRepository;

        public IndexModel(ILogger<IndexModel> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }
        public List<CompanyEmployeeGroupDTO> Companies { get; set; } = new();
        public async Task<ActionResult> OnGet(CancellationToken cancellationToken)
        {
            try
            {
                Companies = (List<CompanyEmployeeGroupDTO>)await _companyRepository.GetCompanyAndEmployeeCount(cancellationToken);

                return Page();
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Could not load companies");
                throw;
            }
        }
    }
}
