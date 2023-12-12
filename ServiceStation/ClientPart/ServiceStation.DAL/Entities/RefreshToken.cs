using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Client Client { get; set; }
    }
}
