using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace RecordedCourseSellingApp.DataAccess;

public static class NHibernateExtensions
{
    public static IServiceCollection AddNHibernate(this IServiceCollection services, 
        string connectionString)
    {
        var mapper = new ModelMapper();
        mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
        HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
 
        var configuration = new Configuration();
        configuration.DataBaseIntegration(c =>
        {
            c.Dialect<MsSql2012Dialect>();
            c.ConnectionString = connectionString;
            c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            c.SchemaAction = SchemaAutoAction.Validate;
            c.LogFormattedSql = true;
            c.LogSqlInConsole = true;
        });
        
        configuration.AddMapping(domainMapping);
        
        var schemaUpdate = new SchemaUpdate(configuration);
        schemaUpdate.Execute(false, true);
        
        var sessionFactory = configuration.BuildSessionFactory();
 
        services.AddSingleton(sessionFactory);
        services.AddScoped(factory => sessionFactory.OpenSession());
        
        return services;
    }
}
