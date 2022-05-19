using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ServicesAutomation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutomationController
    {
        [HttpGet]
        [Route("Products")]
        public JObject Products()
        {
            JObject sections = new JObject();

            JArray arraySections = new JArray();

            JObject blockZero = new JObject();
            //blockZero["Code"] = 0;

            //Produtos do bloco
            JArray blockProducts = new JArray();
            JObject product = new JObject();
            product["ProductId"] = 4402979;
            product["Quantity"] = 99;
            product["UnitPrice"] = 55;

            JObject productObj = new JObject();
            productObj["Id"] = 4402979;
            productObj["Name"] = "1º Periodo";
            product["Product"] = productObj;

            blockProducts.Add(product);
            //blockProducts.Add(serviceField);

            blockZero.Add("Products", blockProducts);
            return blockZero;
        }

        [HttpGet]
        [Route("Service")]
        public JObject Service()
        {
            JObject sections = new JObject();

            JArray arraySections = new JArray();

            JObject blockZero = new JObject();
            //blockZero["Code"] = 0;

            //Campo de opções de serviço
            JArray serviceField = new JArray();
            JObject service = new JObject();
            service["FieldKey"] = "quote_section_2DBC39C3-B23A-4589-8247-CE815AE5B85B";
            service["Id"] = 4402979;
            service["Name"] = "ARMAZENAGEM DE CONT\u00caINER FCL";
            serviceField.Add(service);
            blockZero.Add("OtherProperties", serviceField);

            arraySections.Add(blockZero);
            sections["Sections"] = arraySections;
            return sections;
        }

        [HttpPost]
        [Route("ServiceProducts")]
        public JObject GetServiceProducts(JObject body)
        {
            JObject sections = new JObject();

            JArray arraySections = new JArray();

            JObject block = new JObject();

            string urlRequest = "";

            JArray getProducts = RequestHandler.MakeRequest(urlRequest, Method.GET);


            //Produtos do bloco
            JArray blockProducts = new JArray();

            foreach (JObject productInArray in getProducts)
            {
                JObject product = new JObject();
                product["ProductId"] = productInArray["Id"];
                product["Quantity"] = 1;
                product["UnitPrice"] = productInArray["UnitPrice"];

                JObject productObj = new JObject();
                productObj["Id"] = productInArray["Id"];
                productObj["Name"] = productInArray["Name"];
                product["Product"] = productObj;

                blockProducts.Add(product);
            }

            block.Add("Products", blockProducts);
            return block;
        }
    }
}