using System.ComponentModel.DataAnnotations;

namespace AdminDesk.Models.ServiceOrdrer
{

    public class ServiceOrderFullViewModel
    {
        public ServiceOrderFullViewModel()
        {
            UpsertModel = new ServiceOrderViewModel();
            ServiceOrdreList = new List<ServiceOrderViewModel>();
        }
        public ServiceOrderViewModel UpsertModel { get; set; }
        public List<ServiceOrderViewModel> ServiceOrdreList { get; set; }


    }


    public class ServiceOrderViewModel
    {
        [Required]
        public string Mechanic { get; set; }
        public bool IsAdministrator { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        [Range(0,100000,ErrorMessage ="Hei sveis, denne verdien er rar, bruk heltall mellom 0 og 100 000")]
        public decimal ConsumedHours { get; set; }
        //public string ImageUrl { get; set; }
        public string MechanicComment { get; set; }
        public string CustomerComment { get; set; }

        public string FutureMaintenance { get; set; }
        public int ServiceOrderId { get; set; }
        //public List<ServiceOrderJobGroupModel> JobGroups { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerTelephoneNumber { get; set; }

    }
    /*
      public class ServiceOrderJobGroupModel
      {
          public string Name { get; set; }

          public List<string> Jobs { get; set; }
      }*/

}
