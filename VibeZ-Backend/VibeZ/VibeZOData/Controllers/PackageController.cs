using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Thêm thư viện này
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using VibeZDTO;
using Repositories.UnitOfWork;

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PackageController> _logger; // Khai báo logger

        public PackageController(IUnitOfWork unitOfWork, ILogger<PackageController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger; // Khởi tạo logger
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAllPackages()
        {
            _logger.LogInformation("Getting all packages."); // Ghi log thông tin
            var packages = await _unitOfWork.Packages.GetAll();
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackageById(Guid id)
        {
            _logger.LogInformation($"Getting package with ID {id}."); // Ghi log thông tin
            var package = await _unitOfWork.Packages.GetById(id);
            if (package == null)
            {
                _logger.LogWarning($"No package found with ID {id}."); // Ghi log cảnh báo
                return NotFound($"No package found with ID {id}");
            }
            return Ok(package);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePackage(string name, double price, int numberOfAcc, string description)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid while creating package."); // Ghi log cảnh báo
                return BadRequest(ModelState);
            }

            Package pc = new Package
            {
                Id = new Guid(),
                Name = name,
                price = price,
                Number_of_acc = numberOfAcc,
                Description = description
            };
            await _unitOfWork.Packages.Add(pc);
            await _unitOfWork.Complete();

            _logger.LogInformation("Package created successfully."); // Ghi log thông tin
            return Ok(new { message = "Package created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdatePackage(Guid id, [FromBody] Package updatedPackage)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid while updating package."); // Ghi log cảnh báo
                return BadRequest(ModelState);
            }

            var package = await _unitOfWork.Packages.GetById(id);
            if (package == null)
            {
                _logger.LogWarning($"No package found with ID {id} for update."); // Ghi log cảnh báo
                return NotFound($"No package found with ID {id}");
            }

            updatedPackage.Id = id; // Đảm bảo ID không thay đổi
            await _unitOfWork.Packages.Update(updatedPackage);
            await _unitOfWork.Complete();
            _logger.LogInformation($"Package with ID {id} updated successfully."); // Ghi log thông tin
            return Ok(new { message = "Package updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeletePackage(Guid id)
        {
            var package = await _unitOfWork.Packages.GetById(id);
            if (package == null)
            {
                _logger.LogWarning($"No package found with ID {id} for deletion."); // Ghi log cảnh báo
                return NotFound($"No package found with ID {id}");
            }

            await _unitOfWork.Packages.Delete(package);
            await _unitOfWork.Complete();
            _logger.LogInformation($"Package with ID {id} deleted successfully."); // Ghi log thông tin
            return Ok(new { message = "Package deleted successfully" });
        }
    }
}
