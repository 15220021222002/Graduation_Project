using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Configuration
{
    public class StripeSettings
    {
        public string Publishablekey { get; set; }
        public string SecretKey { get; set; }
        public string SuccessUrl { get; set; }    
        public string CancelUrl { get; set; }
    }
}
