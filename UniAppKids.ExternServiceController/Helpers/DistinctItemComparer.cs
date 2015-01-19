using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniAppSpel.Helpers
{
    using Uni_AppKids.Application.Dto;

    class DistinctItemComparer : IEqualityComparer<WordDto>
    {

        public bool Equals(WordDto x, WordDto y)
        {
            return x.WordName == y.WordName;
            //&& x.Image == y.Image;
        }

        public int GetHashCode(WordDto obj)
        {
            return obj.WordName.GetHashCode();
            //^obj.Image.GetHashCode();
        }
    }
}