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
using MileageStats.Data;
using MileageStats.Domain.Contracts;
using MileageStats.Domain.Models;
using MileageStats.Domain.Properties;

namespace MileageStats.Domain
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }
            this.userRepository = userRepository;
        }

        #region IUserServices Members

        public User GetOrCreateUser(string claimedId)
        {
            User user = null;
            try
            {
                user = GetUserByClaimedIdentifier(claimedId);
            }
            catch (BusinessServicesException)
            {
                user = null;
            }

            if (user == null)
            {
                user = new User
                           {
                               AuthorizationId = claimedId,
                               DisplayName = Resources.NewUserDefaultDisplayName,
                           };

                user = CreateUser(user);
            }

            return user;
        }

        public User GetUserByClaimedIdentifier(string claimedIdentifier)
        {
            if (claimedIdentifier == null)
            {
                throw new BusinessServicesException(Resources.UnableToFindUserExceptionMessage,
                                                    new ArgumentNullException("claimedIdentifier"));
            }
            if (String.IsNullOrEmpty(claimedIdentifier))
            {
                throw new BusinessServicesException(Resources.UnableToFindUserExceptionMessage,
                                                    new ArgumentException("claimedIdentifier"));
            }

            try
            {
                Model.User user = this.userRepository.GetByAuthenticatedId(claimedIdentifier);
                if (user != null)
                {
                    return ToServiceUser(user);
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToFindUserExceptionMessage, ex);
            }
        }

        public void UpdateUser(User updatedUser)
        {
            if (updatedUser == null)
            {
                throw new ArgumentNullException("updatedUser");
            }
            try
            {
                Model.User userToUpdate = ToDataModelUser(updatedUser);
                this.userRepository.Update(userToUpdate);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToUpdateUserExceptionMessage, ex);
            }
        }

        #endregion

        public User CreateUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }
            try
            {
                Model.User userToAdd = ToDataModelUser(newUser);
                this.userRepository.Create(userToAdd);
                return ToServiceUser(userToAdd);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessServicesException(Resources.UnableToCreateUserExceptionMessage, ex);
            }
        }

        internal static Model.User ToDataModelUser(User userToConvert)
        {
            if (userToConvert == null)
            {
                return null;
            }

            var modelUser = new Model.User
                                {
                                    UserId = userToConvert.UserId,
                                    AuthorizationId = userToConvert.AuthorizationId,
                                    DisplayName = userToConvert.DisplayName,
                                    CountryTwoLetterCode = userToConvert.TwoLetterCountryCode,
                                    PostalCode = userToConvert.PostalCode,
                                    HasRegistered = userToConvert.HasRegistered,
                                };
            return modelUser;
        }

        internal static User ToServiceUser(Model.User dataUser)
        {
            if (dataUser == null)
            {
                return null;
            }

            var user = new User
                           {
                               UserId = dataUser.UserId,
                               AuthorizationId = dataUser.AuthorizationId,
                               DisplayName = dataUser.DisplayName,
                               TwoLetterCountryCode = dataUser.CountryTwoLetterCode,
                               PostalCode = dataUser.PostalCode,
                               HasRegistered = dataUser.HasRegistered,
                           };
            return user;
        }
    }
}
