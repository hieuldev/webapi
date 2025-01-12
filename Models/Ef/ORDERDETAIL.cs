namespace Models.Ef
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDERDETAIL")]
    public partial class ORDERDETAIL
    {
        [Key]
        public int DetailID { get; set; }

        public int? Quantity { get; set; }

        public int? OrderID { get; set; }

        public int? SizeID { get; set; }

        [StringLength(20)]
        public string ProductID { get; set; }

        public virtual ORDER ORDER { get; set; }

        public virtual SIZE SIZE { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
