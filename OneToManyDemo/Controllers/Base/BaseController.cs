using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneToManyDemo.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        [NonAction]
        protected Result<T> Do<T>(Func<T> func)
        {
            try
            {
                if (ModelState.IsValid)
                    return Result<T>.Succeed(func());
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return Result<T>.Fail(default).AddErrors(ModelState) as Result<T>;
        }

        [NonAction]
        protected async Task<Result<T>> Do<T>(Func<Task<T>> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Result<T>.Succeed(await func());
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return Result<T>.Fail(default).AddErrors(ModelState) as Result<T>;
        }

        [NonAction]
        protected async Task<Result> Do(Func<Task> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await func();
                    return Result.Succeed();
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return Result.Fail().AddErrors(ModelState);
        }
    }


    public class Result
    {
        public bool Faild { get; set; }
        public string Message { get; set; }
        protected Result(bool faild) => Faild = faild;

        public static Result Fail()
        {
            return new Result(true);
        }

        public static Result Succeed()
        {
            return new Result(false);
        }

        public Result AddErrors(ModelStateDictionary modelState)
        {
            Message = modelState.First().Value.Errors.FirstOrDefault()?.ErrorMessage;
            return this;
        }

        public JsonResult ToJsonResult()
        {
            return Faild
                ? new JsonResult(new { success = false, message = Message })
                : new JsonResult(new { success = true });
        }

    }

    public class Result<T> : Result
    {
        public T Output { get; set; }
        public Result(T output, bool failed) : base(failed)
        {
            Output = output;
        }

        public static Result<T> Fail(T output)
        {
            return new Result<T>(output, true);
        }

        public static Result<T> Succeed(T output)
        {
            return new Result<T>(output, false);
        }

    }
}
