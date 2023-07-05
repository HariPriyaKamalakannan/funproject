using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecifications : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecifications(ProductParams productParams)
        :base (x=>
        (string.IsNullOrEmpty(productParams.Search)||x.Name.ToLower().Contains(productParams.Search))&&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)&&
        (!productParams.TypeId.HasValue|| x.ProductTypeId==productParams.TypeId)
        )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productParams.PageSize*(productParams.PageIndex-1),productParams.PageSize);
            if(!string.IsNullOrEmpty(productParams.sort))
            {
                switch(productParams.sort)
                {
                    case "priceAsc":
                    AddOrderBy(p=>p.Price);
                    break;
                      case "priceDesc":
                    AddOrderByDesc(p=>p.Price);
                    break;
                    default:
                     AddOrderBy(x=>x.Name);
                    break;
                }
            }

        }

        public ProductsWithBrandsAndTypesSpecifications(int id)
         : base(x=>x.Id==id)
        {AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}