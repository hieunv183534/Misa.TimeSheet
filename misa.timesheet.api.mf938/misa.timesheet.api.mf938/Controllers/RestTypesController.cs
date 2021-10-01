using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using misa.timesheet.core.Entities;
using misa.timesheet.core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace misa.timesheet.api.mf938.Controllers
{
    public class RestTypesController : BaseController<RestType>
    {
        #region Declare

        #endregion

        #region Constructor

        public RestTypesController(IBaseService<RestType> baseService) : base(baseService)
        {
        }

        #endregion
    }
}
