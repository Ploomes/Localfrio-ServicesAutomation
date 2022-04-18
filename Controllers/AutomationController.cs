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
    }
}