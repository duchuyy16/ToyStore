using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatus _iStatus;
        public StatusesController(IStatus iStatus)
        {
            _iStatus = iStatus;
        }

        [HttpPost("GetAllStatuses")]
        public List<StatusModel> GetAllStatuses()
        {
            var statuses = _iStatus.GetAll();

            List<StatusModel> lstStatus = new List<StatusModel>();

            foreach (var item in statuses)
            {
                StatusModel statusModel = new StatusModel
                {
                    StatusId = item.StatusId,
                    StatusName = item.StatusName,
                };
                lstStatus.Add(statusModel);
            }

            return lstStatus;
        }
    }
}
