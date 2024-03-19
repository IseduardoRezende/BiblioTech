using BiblioTechData.Enums;
using BiblioTechData.Interfaces;
using System.Linq.Expressions;

namespace BiblioTechData.Extensions
{
    public static class Query
    {
        public static IEnumerable<Model> OrderList<Model>(this IEnumerable<Model> filteredList, string orderField, OrderType orderType)
            where Model : IBaseModel
        {
            var modelProperties = filteredList.GetType().GetGenericArguments().First().GetProperties();
            var property = modelProperties.FirstOrDefault(c => string.Equals(c.Name, orderField, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                throw new ArgumentException("Non-Existent/Invalid property to be an orderField");

            var parameter = Expression.Parameter(typeof(Model));
            var memberAcess = Expression.Property(parameter, property);                       
            
            var convertedMemberAcess = Expression.Convert(memberAcess, typeof(object));            
            var orderPredicate = Expression.Lambda<Func<Model, object>>(convertedMemberAcess, parameter);
                        
            return orderType switch
            {
                OrderType.Asc => OrderListBy(filteredList.AsQueryable(), orderPredicate),
                OrderType.Desc => OrderListByDescending(filteredList.AsQueryable(), orderPredicate),
                _ => Enumerable.Empty<Model>()
            };
        }

        private static IEnumerable<Model> OrderListBy<Model>(IQueryable<Model> filteredList, Expression<Func<Model, object>> expression)
            where Model : IBaseModel
        {
            return filteredList.OrderBy(expression);
        }

        private static IEnumerable<Model> OrderListByDescending<Model>(IQueryable<Model> filteredList, Expression<Func<Model, object>> expression)
            where Model : IBaseModel
        {   
            return filteredList.OrderByDescending(expression);
        }
    }
}
