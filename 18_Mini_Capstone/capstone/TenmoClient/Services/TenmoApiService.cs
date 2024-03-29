﻿using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;


        public TenmoApiService(string apiUrl) : base(apiUrl)
        {
            ApiUrl = apiUrl;

        }

        public Account GetAccount()
        {
            RestRequest request = new RestRequest($"{ApiUrl}account");
            IRestResponse<Account> response = client.Get<Account>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }

        }

        public Account GetRecepientAccount(int userId)
        {
            RestRequest request = new RestRequest($"{ApiUrl}account/{userId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }

        }



        public List<ApiUser> GetUsers()
        {
            RestRequest request = new RestRequest($"{ApiUrl}user");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred-unable to reach server");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public Transfer MakeTransfer(int userId, decimal amount)
        {
            int fromId = GetAccount().AccountId;
            Account account = GetAccount();
                int toId = GetRecepientAccount(userId).AccountId;
                Transfer transferringTo = new Transfer { AccountFromId = fromId, AccountToId = toId, Amount = amount };
                RestRequest request = new RestRequest($"{ApiUrl}account");
                request.AddJsonBody(transferringTo);
                IRestResponse<Transfer> response = client.Post<Transfer>(request);
                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    throw new Exception("Error occurred-unable to reach server");
                }
                else if (!response.IsSuccessful)
                {
                    throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
                }
                else
                {
                    return response.Data;
                }
          
        }
        public List<ReturnTransfer> GetTransfers()
        {
            //int accountId = GetAccount().AccountId;
            RestRequest request = new RestRequest($"{ApiUrl}account/transfer");
            IRestResponse<List<ReturnTransfer>> response = client.Get<List<ReturnTransfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }

        }
    }
}
