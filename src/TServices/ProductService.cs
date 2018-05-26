using System.Collections.Generic;
using System.Linq;
using TModel;

namespace TServices
{
    public class ProductService
    {
        public static IList<TProduct> GetAllProducts()
        {
            using (TestDbEntities db = new TestDbEntities())
            {
                return db.TProducts.ToList();
            }
        }
    }
}
