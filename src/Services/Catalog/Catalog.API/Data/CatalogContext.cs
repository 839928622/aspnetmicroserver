using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.ConfigBinding;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext :ICatalogContext
    {
       
        private readonly IOptions<DatabaseSettings> _databaseSettings;
        public CatalogContext( IOptions<DatabaseSettings> databaseSettings)
        {
         
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.Value.DatabaseName);
            this.Products = database.GetCollection<Product>(_databaseSettings.Value.CollectionName);

        }
        /// <inheritdoc />
        public IMongoCollection<Product> Products { get; }
    }
}
