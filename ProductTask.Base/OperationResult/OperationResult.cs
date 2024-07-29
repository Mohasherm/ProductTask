using ProductTask.Base.ErrorHandling;
using ProductTask.Shared.Enums;
using System;

namespace ProductTask.Base.OperationResult
{
    public class OperationResult<TEntity>
    {
        public TEntity Data { get; set; }
        public Exception Exception { get; set; }
        public ErrorKey ErrorMessage { get; set; }
        public ResultStatus Status { get; set; }
        public OperationResult(ResultStatus status = ResultStatus.Success)
        {
            Status = status;
            ErrorMessage = ErrorKey.None;
        }
        public void AddError(ErrorKey errorMessage, ResultStatus status)
        {
            ErrorMessage = errorMessage;
            Status = status;

        }
        public void ThrowException(ErrorKey errorMessage, ResultStatus status)
        {
            throw status switch
            {
                ResultStatus.ValidationError => new BadRequestException(errorMessage.ToString()),
                ResultStatus.NotFound => new NotFoundException(errorMessage.ToString()),
                ResultStatus.InternalServerError => new InternalServerException(errorMessage.ToString()),
                _ => new InternalServerException(ErrorKey.InternalServerError.ToString()),
            };
        }
    }

}