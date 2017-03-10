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
using Jo2let_Api.Models.Property;

namespace Jo2let_Api.Controllers
{
    public class PropertiesController : ApiController
    {
        private readonly IPropertyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public PropertiesController()
        {
            _repository = new PropertyRepository(new DatabaseFactory());
            _unitOfWork = new UnitOfWork(new DatabaseFactory(), new PropertyDbContext());
        }

        public IHttpActionResult Get()
        {
            var propertyList = _repository.GetAll();
            var propertyViewModels = new List<PropertyViewModel>();
            foreach (var property in propertyList)
            {
                propertyViewModels.Add(new PropertyViewModel
                {
                    Id = property.Id,
                    Title = property.Title,
                    Description = property.Description,
                    Location = new LocationViewModel
                    {
                        Id = property.Location.Id,
                        Name = property.Location.Name
                    }
                });
            }
            return Ok(propertyViewModels);

        }


        public IHttpActionResult Get(int id)
        {
            var property = _repository.GetById(id);
            var propertyViewModel = new PropertyViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Location = new LocationViewModel
                {
                    Id = property.Location.Id,
                    Name = property.Location.Name
                }
            };

            return Ok(propertyViewModel);
        }

        public IHttpActionResult Post(CreatePropertyViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = new Property
            {
                Title = createModel.Title,
                Description = createModel.Description,
                LocationId = createModel.LocationId
            };

            _repository.Add(propertyData);
            _unitOfWork.SaveChanges();

            var property = new PropertyViewModel
            {
                Id = propertyData.Id,
                Title = propertyData.Title,
                Description = propertyData.Description,
                Location = new LocationViewModel
                {
                    Id = propertyData.Location.Id,
                    Name = propertyData.Location.Name
                }

            };
            return Created(
                new Uri(Request.RequestUri + "api/properties" + property.Id),
                property);
        }



        public IHttpActionResult Put(EditPropertyViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var propertyData = _repository.GetById(editModel.Id);
            if (propertyData == null)
                return NotFound();

            propertyData.Id = editModel.Id;
            propertyData.Title = editModel.Title;
            propertyData.Description = editModel.Description;
            propertyData.LocationId = editModel.LocationId;

            _repository.Update(propertyData);
            _unitOfWork.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }

        public IHttpActionResult Delete(int id)
        {

            var propertyData = _repository.GetById(id);
            if (propertyData == null)
                return NotFound();

            _repository.Delete(propertyData);
            _unitOfWork.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }

    }
}
