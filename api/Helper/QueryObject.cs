using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helper
{
    public class QueryObject
    {
        public string? VibeFilter { get; set; } = null;

        public bool OldestFirst { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string? Institution { get; set; } = null;


    }
}