# AngularApplication

public async Task<UserAccessModel> IsAuthorize(string userId,string appname)
        {
            HttpClientHandler objHandler = new HttpClientHandler();

            objHandler.Credentials = new System.Net.NetworkCredential(
             configuration.GetSection("Settings").GetSection(CommonConstants.PassPortServiceAcccount).Value,
             configuration.GetSection("Settings").GetSection(CommonConstants.PassPortServiceAccountPass).Value);

            string passportApiUrl = configuration.GetSection("Settings").GetSection(CommonConstants.passportApiUrl).Value;
            string urlWithParams = string.Concat(passportApiUrl + "UserPermission/Get",
               (string.Format("?lanId={0}&applicationName={1}", userId,
               configuration.GetSection("Settings").GetSection(appname).Value)));

            var result = string.Empty;

            UserAccessModel userData = new UserAccessModel();

            using (HttpClient client = new HttpClient(objHandler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = TripleTapTask(client.GetAsync(urlWithParams));

                log.Info($"method:IsAuthorize-StatusCode {response.StatusCode} ");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    userData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccessModel>(result);
                    userData.StatusCode = 200;
                    userData.LanID = userId;

                    foreach (var AppSettings in userData.Application.ApplicationSettings)
                    {
                        if (AppSettings.Name == "Role")
                        {
                            foreach (var Role in AppSettings.Options)
                            {
                                userData.UserRoleList.Add(Role);
                            }
                        }
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    userData.StatusCode = 400;
                    userData.LanID = userId;
                    userData.IsAuthorized = false;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    userData.StatusCode = 500;
                    userData.LanID = userId;
                    userData.IsAuthorized = false;
                }
            }
            return userData;
        }

        private static HttpResponseMessage TripleTapTask(Task<HttpResponseMessage> t)
        {
            try
            {
                Task.Run(async () => { await t; }).Wait();
                if (!((t.Result?.IsSuccessStatusCode ?? false) || t.Result?.StatusCode == System.Net.HttpStatusCode.NotFound))
                {
                    Task.Run(async () => { await t; }).Wait();
                    if (!((t.Result?.IsSuccessStatusCode ?? false) || t.Result?.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        Task.Run(async () => { await t; }).Wait();
                    }
                }
                return t.Result;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable) { ReasonPhrase = "Server error: " + ex.Message };
            }
        }
    }

