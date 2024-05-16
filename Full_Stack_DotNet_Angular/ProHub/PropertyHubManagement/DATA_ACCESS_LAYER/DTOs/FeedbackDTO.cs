using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.DTOs
{
   public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int PropertyId { get; set; }
        public int BuyerId { get; set; }
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
