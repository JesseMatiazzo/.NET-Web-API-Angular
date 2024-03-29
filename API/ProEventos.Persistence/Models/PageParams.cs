﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProEventos.Persistence.Models
{
    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public string Terms { get; set; } = string.Empty;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
