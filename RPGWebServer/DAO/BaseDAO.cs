using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RPGWebServer.DAO
{
    public class BaseDAO
    {
        protected SqlConnection Connection;

        public BaseDAO()
        {
            Connection = new SqlConnection("Server=274fd5ab-26b8-4b83-ac98-a41400b95ef1.sqlserver.sequelizer.com;Database=db274fd5ab26b84b83ac98a41400b95ef1;User ID=ejcrcjcozrpznerc;Password=mhiynNqxH5qr5KhtHxbmXXcGpgvdXbxfb3kEXb3DfPfjgVLuHqvhhBqveJigsmHG;");
        }

    }
}