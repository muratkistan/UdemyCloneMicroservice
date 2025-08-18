using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace UdemyCloneMicroservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }

        public ProblemDetails? Fail { get; set; }

        public bool IsSuccess => Fail is null;
        public bool IsFail => !IsSuccess;

        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = "The requested resource was not found."
                },
            };
        }

        public new static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                }
            };
        }

        public new static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                }
            };
        }

        public new static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Status = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult
            {
                Status = exception.StatusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = problemDetails
            };
        }

        public new static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Status = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>
            {
                Status = exception.StatusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }
}