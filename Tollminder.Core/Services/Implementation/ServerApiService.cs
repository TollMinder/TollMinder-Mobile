﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Platform;
using Tollminder.Core.Helpers;
using Tollminder.Core.Models;

namespace Tollminder.Core.Services.Implementation
{
    public class ServerApiService : HttpClientService, IServerApiService 
	{   
        const string BaseApiUrl = "http://54.152.103.212/api/";
        private string authToken = Mvx.Resolve<IStoredSettingsService>().AuthToken;

        public async Task<IList<TollRoad>> RefreshTollRoads (long lastSyncDateTime, CancellationToken token)
		{
            IList<TollRoad> result = new List<TollRoad>();
            try
            {
                //Debug.WriteLine($"{BaseApiUrl}sync/{lastSyncDateTime}");
                //var tollRoads = GetAsync<object>($"{BaseApiUrl}sync/{0}", token, "2f45b14a832198b132863af5a82e7a382f9b534b5ded4451c3eac1844cf94cfd");
                //Debug.WriteLine(tollRoads.Result);
                //return null;
                result = await GetAsync<IList<TollRoad>>($"{BaseApiUrl}sync/{0}", token, "LM9NJSUN3GDQU8BFPPCUPpCRtLnd89NZXLSUUR9DBjjSR32EBQxCbHX963ycqcjv");
                Debug.WriteLine($"{BaseApiUrl}sync/{0}");
                //Debug.WriteLine(result);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
		}

        public Task<User> SignIn(string phone, string password)
        {
            var parameters = new Dictionary<string, object>
            {
                ["phone"] = phone,
                ["password"] = password
            };

            var user = SendAsync<Dictionary<string, object>, User>(parameters, $"{BaseApiUrl}user/signin");
            return user;
        }

        public Task<User> GetUser(string id)
        {
            var user = GetAsync<User>($"{BaseApiUrl}user/{id}");
            return user;
        }
	}
}

