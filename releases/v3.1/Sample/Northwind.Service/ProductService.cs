using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Repository;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Service
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetShippingRate(string AccessKey, string Username, string Password, string FromZip, string ToZip, string weight)
        {
          string rate = "";

          string requestString = CreateRequest(AccessKey, Username, Password, FromZip, ToZip, weight);
            string responseXML = DoRequest("https://www.ups.com/ups.app/xml/Rate", requestString);
            List<ShippingOption> shippingOptions = ParseResponse(responseXML);
            foreach (var shippingOption in shippingOptions)
            {
                rate = shippingOption.Rate.ToString();
                shippingOption.Name = string.Format("UPS {0}", shippingOption.Name);
            }

            return rate;
        }

        private List<ShippingOption> ParseResponse(string responseXml)
        {
            //psuedo code, example for implementing a service class that has business logic
            return new List<ShippingOption>();
        }

        private string DoRequest(string URL, string RequestString)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(RequestString);
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            var response = request.GetResponse();
            string responseXML = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
                responseXML = reader.ReadToEnd();

            return responseXML;
        }

        private string CreateRequest(string AccessKey, string Username, string Password, string FromZip, string ToZip, string weight)
        {
            return string.Empty;
        }

        [Serializable]
        public class ShippingOption
        {
            public decimal Rate { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

    }
}
