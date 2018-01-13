﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TPGP.Models.Jobs;

namespace TPGP.Utils
{
    public sealed class Util
    {
        public static IPagedList<T> Pagination<T, TKey>(IEnumerable<T> items, Expression<Func<T, TKey>> orderBy, int noPage, int itemsPerPage)
        {
            return items.AsQueryable().OrderBy(orderBy).ToPagedList(noPage, itemsPerPage);
        }
    }
}