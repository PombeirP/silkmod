//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

using System.Data.Entity;
using System.Linq;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe.Initializers.Sql
{
    /// <summary>
    /// A base class for initializing SQL databases in AppHarbor.
    /// </summary>
    /// <typeparam name="T">The concrete DbContext to use.</typeparam>
    internal class DontDropDbJustCreateTablesIfModelChanged<T> : IDatabaseInitializer<T> where T : DbContext
    {
        private readonly Devtalk.EF.CodeFirst.DontDropDbJustCreateTablesIfModelChanged<T> baseInitializer = new Devtalk.EF.CodeFirst.DontDropDbJustCreateTablesIfModelChanged<T>();

        #region Seeding methods

        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding. 
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected virtual void Seed(T context)
        {
            var seeder = context as ISeedDatabase;
            if (seeder != null)
            {
                seeder.Seed();
            }
        }

        #endregion

        public virtual void InitializeDatabase(T context)
        {
            baseInitializer.InitializeDatabase(context);

            if (!context.Set<Country>().Any())
            {
                this.Seed(context);
                context.SaveChanges();
            }
        }
    }
}
