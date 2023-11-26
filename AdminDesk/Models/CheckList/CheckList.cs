using System;
using System.ComponentModel.DataAnnotations;
using AdminDesk.Entities;
using AdminDesk.Models.CheckList;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDesk.Models.CheckList
{
    public class CheckListFullViewModel
    {
        public CheckListFullViewModel()
        {
            UpsertModel = new CheckListViewModel();
            CheckListList = new List<CheckListViewModel>();
        }

        public CheckListViewModel UpsertModel { get; set; }
        public List<CheckListViewModel> CheckListList { get; set; }
    }

    public class CheckListViewModel
    {
        [Required]
        public int CheckListId { get; set; }
        public int ServiceOrderId { get; set; }

        public string Avd1Sp1 { get; set; }
        public string Avd1Sp2 { get; set; }
        public string Avd1Sp3 { get; set; }
        public string Avd1Sp4 { get; set; }
        public string Avd1Sp5 { get; set; }
        public string Avd1Sp6 { get; set; }
        public string Avd1Sp7 { get; set; }
        public string Avd1Sp8 { get; set; }
        public string Avd2Sp1 { get; set; }
        public string Avd2Sp2 { get; set; }
        public string Avd2Sp3 { get; set; }
        public string Avd2Sp4 { get; set; }
        public string Avd2Sp5 { get; set; }
        public string Avd2Sp6 { get; set; }
        public string Avd2Sp7 { get; set; }

        public string Avd3Sp1 { get; set; }
        public string Avd3Sp2 { get; set; }
        public string Avd3Sp3 { get; set; }

        public string Avd4Sp1 { get; set; }
        public string Avd5Sp1 { get; set; }
        public string Avd5Sp2 { get; set; }
        public string Avd5Sp3 { get; set; }
   }
}