﻿using BaseCrud.Abstractions.Entities;
using BaseCrud.Errors;
using BaseCrud.ServiceResults;
using General.Helper;
using General.Models;
using LearnSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Minerals.StringCases;
using System.Security.Claims;

namespace LearnSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// A helper method to Transform ServiceResult to ActionResult
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="serviceAction">A service action to invoke</param>
        /// <returns> An ActionResult with T value or null as a value </returns>
        protected async Task<ActionResult<T?>> FromServiceResult<T>(Task<ServiceResult<T>> serviceAction)
        {
            try
            {
                ServiceResult<T> actionResult = await serviceAction;

                if (actionResult.TryGetResult(out T? result))
                {

                    return result is not null ? result : NoContent();
                }

                return StatusCode(actionResult.StatusCode, actionResult.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500,
                    new ServiceError(
                        e.Message,
                        e.GetType().Name.ToSnakeCase().Replace("exception", "error")
                    )
                );
            }
        }

        /// <summary>
        /// A helper method to Transform ServiceResult to ActionResult
        /// </summary>
        /// <param name="serviceAction">A service action to invoke</param>
        /// <returns> An ActionResult with T value or null as a value </returns>
        protected async Task<ActionResult> FromServiceResult(Task<ServiceResult> serviceAction)
        {
            try
            {
                ServiceResult actionResult = await serviceAction;

                if (actionResult.IsSuccess)
                    return StatusCode(actionResult.StatusCode);

                return StatusCode(actionResult.StatusCode, actionResult.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    ErrorMessage = e.Message,
                    ErrorKey = e.GetType().Name.ToSnakeCase().Replace("exception", "error")
                });
            }
        }

        /// <summary>
        /// The User definition of current scope
        /// </summary>
        protected IUserProfile<Guid>? UserProfile => HelperUserProfile.GetUserProfile(User);
    }
}
