
using Db2Seeder.API.Models;
using Newtonsoft.Json;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Helpers
{
    public class ApiServices
    {
        public static async Task<Response> GetListAsync<T>(string controller, string route)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
               

                HttpResponseMessage response = await client.GetAsync($"/{controller}/{route}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> GetListAsyncByTypeandState<T>(string controller, int type,int state)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                var xx = url + $"/{controller}/Type/{type}/State{state}";
                HttpResponseMessage response = await client.GetAsync($"/{controller}/Type/{type}/State/{state}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> FindAsyncByID<T>(string controller, int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                string address;
                address = $"/{controller}={id}";

                HttpResponseMessage response = await client.GetAsync(address);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Not Found",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var element = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = element,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> FindAsyncByGuid<T>(string controller, Guid id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                string address;
                address = $"/{controller}={id}";

                HttpResponseMessage response = await client.GetAsync(address);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Not Found",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var element = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = element,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

         public static async Task<Response> PostAsync<T>(string controller, List<Guid> guid )
        {
            try
            {
                string request = JsonConvert.SerializeObject(guid);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.PostAsync($"/{controller}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }

        public static async Task<Response> GetAttachmentAsync<T>(string controller, Guid id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                string address;
                address = $"/{controller}={id}";

                HttpResponseMessage response = await client.GetAsync(address);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Not Found",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var element = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = element,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }



        //public static async Task<Response> FindAsyncByID<T>(string controller, string id)
        //{
        //    try
        //    {
        //        HttpClientHandler handler = new HttpClientHandler()
        //        {
        //            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //        };
        //        string url = Settings.GetApiUrl();
        //        HttpClient client = new HttpClient(handler)
        //        {
        //            BaseAddress = new Uri(url)
        //        };               
        //        string address;
        //        address = $"/{controller}/ID/{id}";

        //        HttpResponseMessage response = await client.GetAsync(address);
        //        string result = await response.Content.ReadAsStringAsync();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        //            {
        //                return new Response
        //                {
        //                    IsSuccess = false,
        //                    Message = "Not Found",
        //                };
        //            }
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = result,
        //            };
        //        }
        //        var element = JsonConvert.DeserializeObject<T>(result);
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Result = element,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message
        //        };
        //    }
        //}

    }
}
