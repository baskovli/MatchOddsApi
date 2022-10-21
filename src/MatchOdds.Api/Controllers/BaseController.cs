using MatchOdds.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MatchOdds.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
