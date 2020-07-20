using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Models;

namespace TenmoClient
{
    public class AuthService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public decimal GetBalance()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            return response.Data;
        }
        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/list");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
            return response.Data;
        }
        public List<Transfer> GetTransfers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/transfers");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            return response.Data;
        }
        public Transfer AddTransfer(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/transfers");
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            return response.Data;
        }
        public bool Register(LoginUser registerUser)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "login/register");
            request.AddJsonBody(registerUser);
            IRestResponse<API_User> response = client.Post<API_User>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return false;
            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    Console.WriteLine("An error message was received: " + response.Data.Message);
                }
                else
                {
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public API_User Login(LoginUser loginUser)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "login");
            request.AddJsonBody(loginUser);
            IRestResponse<API_User> response = client.Post<API_User>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return null;
            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    Console.WriteLine("An error message was received: " + response.Data.Message);
                }
                else
                {
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
                return null;
            }
            else
            {
                client.Authenticator = new JwtAuthenticator(response.Data.Token);
                return response.Data;
            }
        }

        
    }
}
