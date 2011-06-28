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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MileageStats.Data.SqlCe.Initializers.Sql;
using MileageStats.Model;

namespace MileageStats.Data.SqlCe
{
	/// <summary>
	/// Initializes the repository for SQL
	/// </summary>
	public class SqlRepositoryInitializer : IRepositoryInitializer
	{
		private readonly IUnitOfWork unitOfWork;

		public SqlRepositoryInitializer(IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null)
			{
				throw new ArgumentNullException("unitOfWork");
			}

			this.unitOfWork = unitOfWork;

			Database.DefaultConnectionFactory = new SqlConnectionFactory();

			Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<MileageStatsDbContext>());
		}

		protected MileageStatsDbContext Context
		{
			get { return (MileageStatsDbContext) unitOfWork; }
		}

		#region IRepositoryInitializer Members

		public void Initialize()
		{
			Context.Set<Country>().ToList().Count();

			IEnumerable<string> indexes =
				Context.Database.SqlQuery<string>("SELECT name FROM sys.indexes;");

			if (!indexes.Contains("IDX_FillupEntries_FillupEntryId"))
			{
				Context.Database.ExecuteSqlCommand(
					"CREATE UNIQUE INDEX IDX_FillupEntries_FillupEntryId ON FillupEntries (FillupEntryId);");
			}

			if (!indexes.Contains("IDX_Reminders_ReminderId"))
			{
				Context.Database.ExecuteSqlCommand(
					"CREATE UNIQUE INDEX IDX_Reminders_ReminderId ON Reminders (ReminderId);");
			}

			if (!indexes.Contains("IDX_VehiclePhotos_VehiclePhotoId"))
			{
				Context.Database.ExecuteSqlCommand(
					"CREATE UNIQUE INDEX IDX_VehiclePhotos_VehiclePhotoId ON VehiclePhotos (VehiclePhotoId);");
			}
		}

		#endregion
	}
}