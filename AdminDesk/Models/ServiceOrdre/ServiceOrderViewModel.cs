using System;
using System.ComponentModel.DataAnnotations;
using AdminDesk.Entities;
using AdminDesk.Models.Report;
using AdminDesk.Models.ServiceOrder;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDesk.Models.ServiceOrder
{


    public class CompositeViewModel
    {
        public ServiceOrderFullViewModel ServiceOrderModel { get; set; }
        public ReportFullViewModel ReportModel { get; set; }
    }

    public class ServiceOrderFullViewModel
    {
        public ServiceOrderFullViewModel()
        {


            UpsertModel = new ServiceOrderViewModel();

            ServiceOrderList = new List<ServiceOrderViewModel>();
        }

        public ServiceOrderViewModel UpsertModel { get; set; }
        public List<ServiceOrderViewModel> ServiceOrderList { get; set; }
    }


    public class ServiceOrderViewModel
    {
        [Required]
        public int ServiceOrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } // Change the type to Customer

        public string Mechanic { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string FutureMaintenance { get; set; }
        public string CreatedById { get; set; }
        public string OrderStatus { get; set; }
        public string TotalWorkHours { get; set; }

    }

    public class CustomerViewModel
    {
        [Required]
        [Key]
        public int CustomerId { get; set; }
        
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerComment { get; set; }
    }
}

