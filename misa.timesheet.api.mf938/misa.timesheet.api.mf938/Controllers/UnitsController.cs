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
    public class UnitsController : BaseController<Unit>
    {
        #region Declare

        #endregion

        #region Constructor

        public UnitsController(IBaseService<Unit> baseService) : base(baseService)
        {
        }

        #endregion
    }
}
