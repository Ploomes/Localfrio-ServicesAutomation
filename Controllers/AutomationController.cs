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
                urlRequest = $"Products?$filter=(Id+eq+{productId})&$expand=OtherProperties";
                JObject productGet = RequestHandler.MakeRequest(urlRequest, Method.GET)[0] as JObject;

                JArray prodOtherProp = JArray.Parse(productGet["OtherProperties"].ToString());
                string strValue = ConcatenaValores(prodOtherProp);
                JArray otherProp = new JArray();
                otherProp.Add(PlooLib.handleOtherProperties("quote_product_2D4A9A22-3E15-4280-8CE1-08DC63306D32", "StringValue", strValue));

                JObject product = new JObject();
                product["ProductId"] = productGet["Id"];
                product["Quantity"] = 1;
                product["UnitPrice"] = productGet["UnitPrice"];
                product["OtherProperties"] = otherProp;


                JObject productObj = new JObject();
                productObj["Id"] = productGet["Id"];
                productObj["Name"] = productGet["Name"];
                productObj["OtherProperties"] = otherProp;
                product["Product"] = productObj;

                blockProducts.Add(product);
            }

            block.Add("Products", blockProducts);
            return block;
        }

        private string ConcatenaValores(JArray otherProperties)
        {
            //FieldKey:DecimalValue--
            string concatenado = "";
            foreach (JObject item in otherProperties)
            {
                string fieldKey = "";
                float decimalValue = 0;

                if (!PlooLib.IsNullOrEmpty(item["DecimalValue"]))
                {
                    fieldKey = item["FieldKey"].ToString();
                    decimalValue = (float)item["DecimalValue"];
                    concatenado += $"{fieldKey}:{decimalValue}--";
                }

            }
            return concatenado;
        }
        [HttpPost]
        [Route("ServiceProductsTest")]
        public JObject GetServiceProductsTest(JObject body)
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
                product = productGet;
                /*product["Quantity"] = 1;
                product["UnitPrice"] = productGet["UnitPrice"];*/

                JObject productObj = new JObject();
                productObj = product;
                /*productObj["Name"] = productGet["Name"];
                product["Product"] = productObj;*/

                blockProducts.Add(product);
            }

            block.Add("Products", blockProducts);
            return block;
        }
    }
}