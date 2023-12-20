using System;
using System.Collections.Generic;

namespace ForumIT.Models
{
    public partial class TblBaiViet
    {
        public TblBaiViet()
        {
            TblBinhLuans = new HashSet<TblBinhLuan>();
            TblDaLuus = new HashSet<TblDaLuu>();
        }

        public int IdBaiViet { get; set; }
        public string? Title { get; set; }
        public string? Img { get; set; }
        public string? NoiDung { get; set; }
        public string? TrangThai { get; set; }
        public int? IdLdd { get; set; }
        public string? IdUser { get; set; }
        public DateTime? Ngaydang { get; set; }

        public virtual TblLoaiDd? IdLddNavigation { get; set; }
        public virtual AspNetUser? IdUserNavigation { get; set; }
        public virtual Tbldisable? Tbldisable { get; set; }
        public virtual ICollection<TblBinhLuan> TblBinhLuans { get; set; }
        public virtual ICollection<TblDaLuu> TblDaLuus { get; set; }
    }
}
