using System;
using System.ComponentModel.DataAnnotations;
using FakeXiecheng.API.Dtos;

namespace FakeXiecheng.API.ValidationAttributes
{
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var touristRoute = (TouristRouteForManipulationDto)validationContext.ObjectInstance;

            if (touristRoute.Title == touristRoute.Description)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(TouristRouteForManipulationDto) }
                );
            }

            return ValidationResult.Success;
        }
    }
}