using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Spring_Systems.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public IndexModel(ILogger<IndexModel> logger,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public List<Spring.Domain.Employee> Employees { get; set; } = new();
        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            Employees = (List<Spring.Domain.Employee>)await _employeeRepository.GetEmployees(cancellationToken);

            return Page();
        }
    }
}
