using Microsoft.AspNetCore.Mvc;
using ProductTask.Base.ErrorHandling;
using ProductTask.Base.OperationResult;

namespace ButterflyApi.Base.ErrorHandling
{
    public static class ThrowException
    {
        public static JsonResult GetResult<TEntity>(this OperationResult<TEntity> result)
        {
            return result.Status switch
            {
                ResultStatus.Success => new JsonResult(result.Data),
                ResultStatus.NotFound => throw new NotFoundException(result.ErrorMessage.ToString()),
                ResultStatus.ValidationError => throw new BadRequestException(result.ErrorMessage.ToString()),
                ResultStatus.InternalServerError => throw new InternalServerException(result.ErrorMessage.ToString()),
                _ => throw new InternalServerException(result.ErrorMessage.ToString())
            };
        }
    }

}