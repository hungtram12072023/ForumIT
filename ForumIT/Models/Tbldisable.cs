using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class Tbldisable
    {
        public int FkT2 { get; set; }
        public bool? Disabledd { get; set; }
        public DateTime? Logtime { get; set; }

        public virtual TblBaiViet FkT2Navigation { get; set; } = null!;
    }
}
