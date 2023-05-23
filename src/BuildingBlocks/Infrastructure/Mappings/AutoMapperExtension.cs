
using System.Reflection;
using AutoMapper;

namespace Infrastructure.Mappings
{
    public static class AutoMapperExtension
    {
        
        // public static IMappingExpression<Tsource, Tdescription> IgnoneAllNonExisting<Tsource, Tdescription>(this IMappingExpression<Tsource, Tdescription> expression)
        // {
        //     var flags = BindingFlags.public | BindingFlags.Instance;

        //     return expression;
        // }


        public static IMappingExpression<Tsource, Tdescription> IgnoreAllNonExisting<Tsource, Tdescription>(this IMappingExpression<Tsource, Tdescription> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var source = typeof(Tsource);
            var descriptionProperties = typeof(Tdescription).GetProperties(flags);

            foreach(var property in descriptionProperties)
            {
                if(source.GetProperty(property.Name,flags) == null)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }
    }
}