using AutoMapper;
using MambaProject.DAL;
using MambaProject.Migrations;
using MambaProject.Models;
using MambaProject.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace MambaProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.ToListAsync();
            return View(teams);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }
            var isExisted = await _context.Teams.AnyAsync(x => x.FullName.ToLower().Contains(team.FullName.ToLower()));
            if (isExisted)
            {
                ModelState.AddModelError("FullName", "Bu name movcuddur");
                return View(team);
            }

            if (!team.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Seklin tipi yanlisdir");
                return View(team);
            }

            if (team.Image.Length > 1024 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Seklin maximal olcusu 1 mb olmalidir");
                return View(team);
            }

            string filename = Guid.NewGuid() + team.Image.FileName;
            string path = Path.Combine(_environment.ContentRootPath, "wwwroot", "assets", "img", filename);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await team.Image.CopyToAsync(stream);
            }

            var newTeam = _mapper.Map<Team>(team);
            newTeam.IMageUrl = filename;

            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
            {
                throw new Exception("Team movcud deyil");
            }
            var vm = _mapper.Map<TeamUpdateVM>(team);
            vm.IMageUrl = team.IMageUrl;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TeamUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existed = await _context.Teams.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (existed == null)
            {
                throw new Exception("Movcuddur");
            }
            string filename = existed.IMageUrl;
            var isExisted = await _context.Teams.AnyAsync(x=>x.FullName.ToLower().Contains(vm.FullName.ToLower()) && x.Id!=vm.Id);
            if (isExisted)
            {
                ModelState.AddModelError("FullName", "Bu name movcuddur");
                return View(vm);
            }

            if (vm.Image is not null)
            {
                filename = Guid.NewGuid() + vm.Image.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "wwwroot", "assets", "img");
                if (System.IO.File.Exists(path + "/" + existed.IMageUrl))
                {
                    System.IO.File.Delete(path + "/" + existed.IMageUrl);
                }


                using (FileStream stream = new(path + "/" + filename, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(stream);
                }


            }


            existed.IMageUrl = filename;

            existed = _mapper.Map(vm, existed);
            existed.IMageUrl = filename;

            _context.Teams.Update(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
