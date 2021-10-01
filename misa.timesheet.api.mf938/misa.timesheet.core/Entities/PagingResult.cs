using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.Entities
{
    public class PagingResult<MISAEntity>
    {
        public PagingResult(int TotalRecord, List<MISAEntity> Data)
        {
            this.TotalRecord = TotalRecord;
            this.Data = Data;
        }

        //tổng số bản ghi
        public int TotalRecord { get; set; }

        //dữ liệu trang các bản ghi lấy được
        public List<MISAEntity> Data { get; set; }
    }
}
