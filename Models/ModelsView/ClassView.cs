using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.ModelsView
{
    public class ClassView
    {

    }

    public class sNhanVienView : sNhanVien
    {
        public string DonViThuVien { get; set; }
    }

}
