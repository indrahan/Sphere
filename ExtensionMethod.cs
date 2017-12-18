using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project5_6.Models;
using Microsoft.EntityFrameworkCore;


namespace ExtensionMethods
{
    
    using restserver.Paginator;
    public static class MyExtensions
    {
        public static Page<T> GetPage<T>(this Microsoft.EntityFrameworkCore.DbSet<T> list, int page_index, int page_size, Func<T, object> order_by_selector) where T : class
        {
            //the given index and size are used to select the correct data
            //note the OrderBy which is necessary to ensure the selection in the 
            //correct order
            var res = list.OrderBy(order_by_selector)
                        .Skip(page_index * page_size)
                        .Take(page_size)
                        .ToArray();

            //in case the query fails or the page size is zero a NotFOund is returned
            if (res == null || res.Length == 0)
                return null;

            //we calculate once the total amonut of items of the unput list
            var tot_items = list.Count();

            //we calculate the total amount of pages. incase the page size is greater than
            //the total amount of items we set the total pages to 1
            var tot_pages = tot_items / page_size;
            if (tot_items < page_size) tot_pages = 1;

            //we group all the date and return then to the caller
            return new Page<T>() { Index = page_index, Items = res, TotalPages = tot_pages };


        }
    }
}