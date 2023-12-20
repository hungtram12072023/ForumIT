using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class TblDaLuu
    {
        public int IdDaluu { get; set; }
        public int? IdBaiVietDl { get; set; }
        public string? IdUser { get; set; }

        public virtual TblBaiViet? IdBaiVietDlNavigation { get; set; }
        public virtual AspNetUser? IdUserNavigation { get; set; }
    }
}
