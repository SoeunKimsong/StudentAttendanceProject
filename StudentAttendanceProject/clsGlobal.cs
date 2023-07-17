using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendanceProject
{
    class clsGlobal
    {
        public static SqlConnection cn = new SqlConnection();
    }
}
