using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PartenaServiceFabricOnPrem.Pages
{
    public class IndexModel : PageModel
    {

        public string ApiResponse { get; set; }

        public async Task OnGet()
        {
            HttpClient client = new HttpClient();

            var payload = new ResignationRequest();
            payload.EmployeeQuit = true;
            payload.EmploymentStartDate = new DateTime(2018, 01, 01);
            payload.IsInEarlyRetirement = true;
            payload.JointCommittee = "string";
            payload.Language = "string";
            payload.ResignationDate = new DateTime(2018, 12, 01);
            payload.Salary = 3000;
            payload.Statute = "clerk";
            payload.LessThanSixMonthsContractArrangementsInDays = 0;
            
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
               
                response = await client.PostAsync(
                    "https://4a.resignation.partena.be/api/ResignationCalculation?code=9btRZ5wGtdEMq2r/pzF5OK/sb4k8mfWI6oQ2gBtbKKxRBnRndkEBkA==",
                    new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                ApiResponse = $"Version = {response.Version}, " +
                $"StatusCode = {response.StatusCode}," +
                $"RequestMessage = {response.RequestMessage}," +
                $"ReasonPhrase = {response.ReasonPhrase}," +
                $"IsSuccessStatusCode = {response.IsSuccessStatusCode}," +
                $"Headers = {response.Headers}," +
                $"Content = {response.Content}," +
                $"";

                ViewData["ApiResponse"] = ApiResponse;
            }
            catch (Exception e)
            {
                ViewData["ApiResponse"] = e.Message;
            }

        }
    }

    public class ResignationRequest
    {
        public bool? EmployeeQuit { get; set; }

        public DateTime EmploymentStartDate { get; set; }

        public bool? IsInEarlyRetirement { get; set; }

        public string JointCommittee { get; set; }

        public string Language { get; set; }

        public DateTime ResignationDate { get; set; }

        public decimal? Salary { get; set; }

        public string Statute { get; set; }

        public int? LessThanSixMonthsContractArrangementsInDays { get; set; }
    }
}
