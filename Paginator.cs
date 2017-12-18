using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project5_6.Models;
using Microsoft.EntityFrameworkCore;

namespace restserver.Paginator{
    public class Page<T>{
        public int Index { get; set; }
        public T[] Items { get; set; }
        public int TotalPages { get; set; }
    }
}