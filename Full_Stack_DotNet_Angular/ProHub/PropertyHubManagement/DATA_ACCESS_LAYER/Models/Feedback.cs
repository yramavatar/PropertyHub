using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DATA_ACCESS_LAYER.Models
{
     
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

       
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        
        public int BuyerId { get; set; }

        public User Buyer { get; set; }

        public int? Rating { get; set; }

       
        public string Comment { get; set; }

    
        public DateTime FeedbackDate { get; set; }
    }
}
