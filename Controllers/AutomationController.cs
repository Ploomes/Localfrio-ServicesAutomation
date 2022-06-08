using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ServicesAutomation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutomationController
    {
        [HttpPost]
        [Route("ServiceProducts")]
        public JObject GetServiceProducts(JObject body)
        {
            PlooLib.InstantiateConnection();
            JObject sections = new JObject();

            JArray arraySections = new JArray();

            JObject block = new JObject();

            int serviceId = (int)body["ServiceId"];

            string urlRequest = $"Products?$filter=(Id+eq+{serviceId})&$expand=OtherProperties($filter=FieldId+eq+10119528)";
            //product_06495A67-033C-4DFB-9290-AAEC721D7C09

            JArray getProducts = RequestHandler.MakeRequest(urlRequest, Method.GET);

            //Produtos do bloco
            JArray blockProducts = new JArray();

            JArray otherProperties = JArray.Parse(getProducts[0]["OtherProperties"].ToString());

            foreach (JObject productInArray in otherProperties)
            {
                int productId = (int)productInArray["ProductValueId"];
                urlRequest = $"Products?$filter=(((Id+eq+{productId})))";
                JObject productGet = RequestHandler.MakeRequest(urlRequest, Method.GET)[0] as JObject;

                JObject product = new JObject();
                product["ProductId"] = productGet["Id"];
                product["Quantity"] = 1;
                product["UnitPrice"] = productGet["UnitPrice"];

                JObject productObj = new JObject();
                productObj["Id"] = productGet["Id"];
                productObj["Name"] = productGet["Name"];
                product["Product"] = productObj;

                blockProducts.Add(product);
            }

            block.Add("Products", blockProducts);
            return block;
        }
    }
}