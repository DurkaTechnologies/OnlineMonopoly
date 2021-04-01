﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BranchType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_UniqueKeyString", IsUnique = true, Order = 1)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}