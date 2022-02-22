using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddItemViewList.Models
{
    public class AddList
    {

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemType { get; set; }

        public string ItemPrice { get; set; }

        [NotMapped]
        public int dataCount { get; set; }


    }

    public class DataCountM
    {
        public List<AddList> AddLists { get; set; }
        public int dataCount { get; set; }
    }

    public class Filter
    {

        public int pageNo { get; set; }
        public int pageSize { get; set; }


        
    }
}
