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

namespace MileageStats.Model
{
	public enum FillupUnits
	{
		ImperialGallons = 2,
		UsGallons = 0,
		Litres = 1
	}

	public class FillupEntry : IEquatable<FillupEntry>
	{
		private static int tempKey;

		public FillupEntry()
		{
			UnitOfMeasure = FillupUnits.Litres;
			Date = DateTime.UtcNow;
			FillupEntryId = --tempKey;
		}

		/// <summary>
		/// Identifier for FillupEntry.  Should be unique once persisted.
		/// </summary>
		public int FillupEntryId { get; set; }

		/// <summary>
		/// Identifier for the Vehicle this fillup is related to.  
		/// </summary>
		public int VehicleId { get; set; }

		/// <summary>
		/// Date of the fillup.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Odometer reading for the fillup (in kms).
		/// </summary>
		public int Odometer { get; set; }

		/// <summary>
		/// Price per unit.
		/// </summary>
		public double PricePerUnit { get; set; }

		/// <summary>
		/// Total number of units.
		/// </summary>
		public double TotalUnits { get; set; }

		public FillupUnits UnitOfMeasure { get; set; }

		public int UnitOfMeasureInt
		{
			get { return (int) UnitOfMeasure; }
			set { UnitOfMeasure = (FillupUnits) value; }
		}

		public string Vendor { get; set; }

		public double TransactionFee { get; set; }

		public string Remarks { get; set; }

		/// <summary>
		/// Total cost of fillup.
		/// </summary>
		public double TotalCost
		{
			get { return (PricePerUnit*TotalUnits) + TransactionFee; }
		}

		#region Cached Calculations

		/// <summary>
		/// Gets or sets the distance from last fillup.  
		/// </summary>
		public int? Distance { get; set; }

		#endregion

		#region Object overrides

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (FillupEntry)) return false;
			return Equals((FillupEntry) obj);
		}

		#endregion

		#region IEquatable<FillupEntry> Members

		public bool Equals(FillupEntry other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.VehicleId == VehicleId && other.Date.Equals(Date) && other.Odometer == Odometer &&
			       other.PricePerUnit.Equals(PricePerUnit) && other.TotalUnits.Equals(TotalUnits) &&
			       Equals(other.UnitOfMeasure, UnitOfMeasure) && Equals(other.Vendor, Vendor) &&
			       other.TransactionFee.Equals(TransactionFee) && Equals(other.Remarks, Remarks);
		}

		#endregion

		public override int GetHashCode()
		{
			unchecked
			{
				int result = VehicleId;
				result = (result*397) ^ Date.GetHashCode();
				result = (result*397) ^ Odometer;
				result = (result*397) ^ PricePerUnit.GetHashCode();
				result = (result*397) ^ TotalUnits.GetHashCode();
				result = (result*397) ^ UnitOfMeasure.GetHashCode();
				result = (result*397) ^ (Vendor != null ? Vendor.GetHashCode() : 0);
				result = (result*397) ^ TransactionFee.GetHashCode();
				result = (result*397) ^ (Remarks != null ? Remarks.GetHashCode() : 0);
				return result;
			}
		}

		public static bool operator ==(FillupEntry left, FillupEntry right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(FillupEntry left, FillupEntry right)
		{
			return !Equals(left, right);
		}
	}
}