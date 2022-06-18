using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vega.API.Core;
using Vega.API.Core.Models;
using Vega.API.Resources;

namespace Vega.API.Controllers
{
    [ApiController]
    [Route("/api/vehicles/{vehicleId}")]

    public class PhotosController : Controller
    {
        private readonly ILogger<PhotosController> _logger;
        private readonly IHostEnvironment hostEnvironment;
        private readonly IVehicleRepository repository;
        private readonly IVehiclePhotoRepository vehiclePhotoRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        public PhotosController(ILogger<PhotosController> logger, IHostEnvironment hostEnvironment, IVehicleRepository repository, IVehiclePhotoRepository vehiclePhotoRepository, IUnitOfWork uow, IMapper mapper, IOptionsSnapshot<PhotoSettings> option)
        {
            _logger = logger;
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
            this.vehiclePhotoRepository = vehiclePhotoRepository;
            this.uow = uow;
            this.mapper = mapper;
            photoSettings = option.Value;
        }
        [HttpGet()]
        [Route("photos")]
        public async Task<IActionResult> Photos(int vehicleId)
        {
            var photos = await vehiclePhotoRepository.GetPhotos(vehicleId);
            return Ok(mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos));
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadPhoto(int vehicleId, IFormFile file)
        {
            var vehicle = await repository.GetVehicle(vehicleId);
            if (vehicle == null)
                return NotFound("There is no vehicle with the given id.");

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("File is empty");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Exceed the limited file size");
            if (!photoSettings.IsSupportedType(file.FileName)) return BadRequest("Unsupported file type");

            var rootPath = hostEnvironment.ContentRootPath;
            var uploadFolderPath = Path.Combine(rootPath, "uploads");

            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await uow.CompeleteAsync();

            return Ok(mapper.Map<Photo, Resources.PhotoResource>(photo));
        }


    }
}