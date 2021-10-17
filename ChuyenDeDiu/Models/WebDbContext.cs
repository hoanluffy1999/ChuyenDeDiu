using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuyenDeDiu.Models
{
    public  class WebDbContext
    {
        private QLCayCanhEntities Instanse { get; set; }
        public WebDbContext()
        {
            if(Instanse == null)
            {
               Instanse = new QLCayCanhEntities();
            } 
        }
        public QLCayCanhEntities GetDBContext()
        {
            return Instanse;
        }
    }
}