using AutoMapper;
using MambaProject.DAL;
using MambaProject.Models;
using MambaProject.ViewModels.ServiceVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServiceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVM service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }

            var isExist = await _context.Services.AnyAsync(x => x.Name.ToLower().Contains(service.Name.ToLower()));
            if (isExist)
            {
                ModelState.AddModelError("Name", "Movcuddur");
                return View(service);
            }
            var mapperService = _mapper.Map<Service>(service);

            await _context.Services.AddAsync(mapperService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
            {
                throw new Exception("Servis movcud deyil");
            }
            var vm=_mapper.Map<ServiceUpdateVM>(service);   

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult>Update(ServiceUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existedService= await _context.Services.FirstOrDefaultAsync(x=>x.Id==vm.Id);
            if (existedService==null)
            {
                throw new Exception("Bu servis movcuddur");
            }
            var isExist = await _context.Services.AnyAsync(x => x.Name.ToLower().Contains(vm.Name.ToLower()) && x.Id!=vm.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Movcuddur");
                return View(vm);
            }

            var mapperService= _mapper.Map(vm,existedService);

            _context.Update(existedService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
