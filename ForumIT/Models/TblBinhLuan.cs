using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class TblBinhLuan
    {
        public TblBinhLuan()
        {
            TblTraLoiBls = new HashSet<TblTraLoiBl>();
        }

        public int IdBinhLuan { get; set; }
        public string? NoiDung { get; set; }
        public int? IdidBaiVietBl { get; set; }
        public string? IdUser { get; set; }
        public DateTime? NgayBl { get; set; }

        public virtual AspNetUser? IdUserNavigation { get; set; }
        public virtual TblBaiViet? IdidBaiVietBlNavigation { get; set; }
        public virtual ICollection<TblTraLoiBl> TblTraLoiBls { get; set; }
    }
}
