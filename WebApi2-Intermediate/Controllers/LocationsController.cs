using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Jo2let.Data;
using Jo2let.Infrastructure.Factory;
using Jo2let.Infrastructure.Repository;
using Jo2let.Infrastructure.Repository.Interface;
using Jo2let.Infrastructure.UnitOfWork;
using Jo2let.Model;
using Jo2let_Api.Models.Location;

namespace Jo2let_Api.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly ILocationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LocationsController()
        {
            _repository = new LocationRepository(new DatabaseFactory());
            _unitOfWork = new UnitOfWork(new DatabaseFactory(), new PropertyDbContext());
        }

        public IHttpActionResult Get()
        {
            var locationList = _repository.GetAll();
            var locationViewModels = new List<LocationViewModel>();
            foreach (var location in locationList)
            {
                locationViewModels.Add(new LocationViewModel
                {
                    Id = location.Id,
                    Name = location.Name
                });
            }
            return Ok(locationViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            var location = _repository.GetById(id);
            var locationViewModel = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name
            };
            return Ok(locationViewModel);
        }

        public IHttpActionResult Post(CreateLocationViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationData = new Location
            {
                Name = createModel.CreateName
            };

            _repository.Add(locationData);
            _unitOfWork.SaveChanges();

            var location = new LocationViewModel
            {
                Id = locationData.Id,
                Name = locationData.Name
            };
            return Created(new Uri(Request.RequestUri + "api/loctions" + location.Id), location);

        }

        public IHttpActionResult Put(EditLocationViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationData = _repository.GetById(editModel.Id);
            if (locationData == null)
                return NotFound();

            locationData.Id = editModel.Id;
            locationData.Name = editModel.EditName;

            _repository.Update(locationData);
            _unitOfWork.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }

        public IHttpActionResult Delete(int id)
        {
            var locationData = _repository.GetById(id);
            if (locationData == null)
                return NotFound();

            _repository.Delete(locationData);
            _unitOfWork.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}
