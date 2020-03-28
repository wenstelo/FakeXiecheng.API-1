﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeXiecheng.API.Dtos;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeXiecheng.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TouristRoutesController : Controller //ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;

        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository ??
                throw new ArgumentNullException(nameof(touristRouteRepository));
        }

        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var touristRoutesFromRepo = _touristRouteRepository.GetAllTouristRoutes();

            // use for loop 
            //var touristRoutes = new List<TouristRouteDto>();
            //foreach(var touristRoute in touristRoutesFromRepo)
            //{
            //    var touristRoutePictures = new List<TouristRoutePictureDto>();
            //    foreach (var picture in touristRoute.TouristRoutePictures)
            //    {
            //        touristRoutePictures.Add(new TouristRoutePictureDto()
            //        {
            //            Url = picture.Url
            //        });
            //    }

            //    touristRoutes.Add(new TouristRouteDto()
            //    {
            //        Id = touristRoute.Id,
            //        Title = touristRoute.Title,
            //        Description = touristRoute.Description,
            //        OriginalPrice = touristRoute.OriginalPrice,
            //        DiscountPercent = touristRoute.DiscountPercent,
            //        Price = touristRoute.OriginalPrice * (decimal)(touristRoute.DiscountPercent ??= 1),
            //        Coupons = touristRoute.Coupons,
            //        Points = touristRoute.Points,
            //        Rating = touristRoute.Rating,
            //        Features = touristRoute.Features,
            //        Fees = touristRoute.Fees,
            //        Notes = touristRoute.Notes,
            //        Pictures = touristRoutePictures

            //    });
            //}

            // using Linq
            var touristRoutes = touristRoutesFromRepo.Select(t => new TouristRouteDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                OriginalPrice = t.OriginalPrice,
                DiscountPercent = t.DiscountPercent,
                Price = t.OriginalPrice * (decimal)(t.DiscountPercent ??= 1),
                Coupons = t.Coupons,
                Points = t.Points,
                Rating = t.Rating,
                Features = t.Features,
                Fees = t.Fees,
                Notes = t.Notes,
                Pictures = t.TouristRoutePictures.Select(p => new TouristRoutePictureDto()
                {
                    Url = p.Url
                }).ToList()
            }).ToList();

            return Ok(touristRoutes);
        }

        [HttpGet("{routeId}")]
        public IActionResult GetTouristRoute(Guid routeId)
        {
            var authorFromRepo = _touristRouteRepository.GetTouristRoute(routeId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(authorFromRepo);
        }
    }
}