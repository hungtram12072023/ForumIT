using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class TblTraLoiBl
    {
        public int IdTraloi { get; set; }
        public string? Noidung { get; set; }
        public DateTime? NgayTl { get; set; }
        public string? IdUser { get; set; }
        public int? IdBluanTloi { get; set; }

        public virtual TblBinhLuan? IdBluanTloiNavigation { get; set; }
        public virtual AspNetUser? IdUserNavigation { get; set; }
    }
}
