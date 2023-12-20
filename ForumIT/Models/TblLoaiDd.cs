using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class TblLoaiDd
    {
        public TblLoaiDd()
        {
            TblBaiViets = new HashSet<TblBaiViet>();
        }

        public int IdLoaiDd { get; set; }
        public string? TenLoaiDd { get; set; }

        public virtual ICollection<TblBaiViet> TblBaiViets { get; set; }
    }
}
