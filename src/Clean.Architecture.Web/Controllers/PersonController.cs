using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.Core.PersonAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.Web.ApiModels;
using Clean.Architecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace Clean.Architecture.Web.Controllers;

[Route("[controller]")]
public class PersonController : Controller
{
  private readonly IRepository<Person> _repository;
  protected readonly IOptions<AppSettingsModel> options;
  public static string? ServiceUrl { get; set; }

  public PersonController(IRepository<Person> repository, IOptions<AppSettingsModel> appSettings)
  {
    _repository = repository;
    options = appSettings;
    ServiceUrl = options.Value.MainServiceUrl;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var personDTOs = (await _repository.ListAsync())
        .Select(person => new PersonViewModel
        {
          Id = person.Id,
          FirstName = person.FirstName,
          LastName = person.LastName,
          Email = person.Email,
          PhoneNumber = person.PhoneNumber,
          Age = person.Age
        })
        .ToList();
    ViewData["MainServiceUrl"] = ServiceUrl;
    ViewBag.MainServiceUrl = ServiceUrl;
    ViewBag.DataSource = personDTOs;
    return View(personDTOs);
  }

  [HttpPost("{id:int}")]
  [ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(int id)
  {
    var spec = new PersonByIdSpec(id);
    var person = await _repository.FirstOrDefaultAsync(spec);
    if (person == null)
    {
      return NotFound();
    }
    await _repository.DeleteAsync(person);
    return RedirectToAction("Index");
  }

  [HttpGet("Create")]
  public IActionResult Create()
  {
    return View();
  }

  [HttpPost("Create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(PersonViewModel personViewModel)
  {
    if (personViewModel.FirstName == null)
    {
      ModelState.AddModelError("FirstName", "First Name is required");
      return View(personViewModel);
    }
    if (personViewModel.LastName == null)
    {
      ModelState.AddModelError("LastName", "Last Name is required");
      return View(personViewModel);
    }
    if (personViewModel.Email == null)
    {
      ModelState.AddModelError("Email", "Email is required");
      return View(personViewModel);
    }
    if (personViewModel.PhoneNumber == null)
    {
      ModelState.AddModelError("PhoneNumber", "Phone Number is required");
      return View(personViewModel);
    }
    if (personViewModel.Age < 0)
    {
      ModelState.AddModelError("Age", "Age must be greater than 0");
      return View(personViewModel);
    }
    var person = new Person(personViewModel.FirstName, personViewModel.LastName, personViewModel.Email, personViewModel.PhoneNumber, personViewModel.Age);
    await _repository.AddAsync(person);
    return RedirectToAction("Index");
  }

  [HttpGet("Edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id)
  {
    var spec = new PersonByIdSpec(id);
    var person = await _repository.FirstOrDefaultAsync(spec);
    if (person == null)
    {
      return NotFound();
    }
    var personViewModel = new PersonViewModel
    {
      Id = person.Id,
      FirstName = person.FirstName,
      LastName = person.LastName,
      Email = person.Email,
      PhoneNumber = person.PhoneNumber,
      Age = person.Age
    };
    return View(personViewModel);
  }

  [HttpPost("Edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(PersonViewModel personViewModel)
  {
    if (personViewModel.FirstName == null)
    {
      ModelState.AddModelError("FirstName", "First Name is required");
      return View(personViewModel);
    }
    if (personViewModel.LastName == null)
    {
      ModelState.AddModelError("LastName", "Last Name is required");
      return View(personViewModel);
    }
    if (personViewModel.Email == null)
    {
      ModelState.AddModelError("Email", "Email is required");
      return View(personViewModel);
    }
    if (personViewModel.PhoneNumber == null)
    {
      ModelState.AddModelError("PhoneNumber", "Phone Number is required");
      return View(personViewModel);
    }
    if (personViewModel.Age < 0)
    {
      ModelState.AddModelError("Age", "Age must be greater than 0");
      return View(personViewModel);
    }
    var spec = new PersonByIdSpec(personViewModel.Id);
    var personToUpdate = await _repository.FirstOrDefaultAsync(spec);
    if (personToUpdate == null)
    {
      return NotFound();
    }
    personToUpdate.UpdateName(personViewModel.FirstName);
    personToUpdate.UpdateLastName(personViewModel.LastName);
    personToUpdate.UpdateEmail(personViewModel.Email);
    personToUpdate.UpdatePhoneNumber(personViewModel.PhoneNumber);
    personToUpdate.UpdateAge(personViewModel.Age);
    await _repository.UpdateAsync(personToUpdate);
    return RedirectToAction("Index");
  }

}
