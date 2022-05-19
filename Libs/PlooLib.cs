using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServicesAutomation
{
    public class PlooLib
    {
        public static void CreateTask(bool success, int actionUserId, int contactId)
        {
            string urlTask = "Tasks?";
            string description = "";
            if (success)
            {
                description = "INTEGRAÇÃO FEITA COM SUCESSO!!!";
            }
            else
            {
                description = "FALHA NA INTEGRAÇÃO!!!";
            }
            JArray users = new JArray();
            JObject userId = new JObject()
            {
                {"UserId", actionUserId}
            };
            users.Add(userId);

            JObject objTask = new JObject()
            {
                {"DealId", null},
                {"ContactId", contactId},
                {"Users", users },
                {"Description",description}
            };
            RequestHandler.MakeRequest(urlTask, Method.POST, objTask);
        }
        public static void InstantiateConnection()
        {
            RequestHandler.client = new HttpClient();
            RequestHandler.client.DefaultRequestHeaders.Clear();
            RequestHandler.client.DefaultRequestHeaders.Add("User-Key", "E6E1AD32C19B1D9FC8C01D14F968EAC9FEB88263E4501D5BD92F8D728E7079968400C0EC5CC8F749E8D0026B7BBB89937703D83DFCD1DA3F8BF83120C06B1EF8");
            RequestHandler.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static bool IsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
        public static string StripHTML(string input)
        {
            string str = Regex.Replace(input, "<.*?>", String.Empty);
            str = str.Replace("\n", "");
            return str;
        }
        public static string RemoveSpecialString(string str)
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            str = reg.Replace(str, string.Empty);
            return str;
        }
        public static string RemoveString(string str)
        {
            var numberString = Regex.Match(str, @"\d+").Value;
            return numberString;
        }
        public static string GetOtherPropValue(JArray otherProp, string fieldKey, string type)
        {
            string returnString = "";
            foreach (var option in otherProp)
            {
                if (option["FieldKey"].ToString() == fieldKey)
                {
                    returnString = option[type].ToString();
                    break;
                }
            }
            return returnString;
        }
        public static string GetLineOfBusName(int idLine)
        {
            string name = "";
            string urlRequest = "Contacts@LinesOfBusiness?$filter=Id+eq+" + idLine;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }
            return name;
        }
        public static string GetCityName(int cityId)
        {
            string name = "";
            string urlRequest = "Cities?$filter=Id+eq+" + cityId;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }

            return name;
        }
        public static string GetStateName(int stateId)
        {
            string name = "";
            string urlRequest = "Cities@Countries@States?$filter=Id+eq+" + stateId;
            urlRequest += "&$select=Short";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Short"].ToString();
            }

            return name;
        }
        public static string GetCountryName(int stateId)
        {
            string name = "";
            string urlRequest = "Cities@Countries?$filter=Id+eq+" + stateId;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }

            return name;
        }
        public static string GetOriginName(int originId)
        {
            string name = "";
            string urlRequest = "Contacts@Origins?$filter=Id+eq+" + originId;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }

            return name;
        }
        public static string GetOwnerName(int ownerId)
        {
            string name = "";
            string urlRequest = "Users/GetContactOwners?$filter=Id+eq+" + ownerId;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }

            return name;
        }
        public static string GetCurrencyName(int currencyId)
        {
            string name = "";
            string urlRequest = "Currencies?$filter=Id+eq+" + currencyId;
            urlRequest += "&$select=Name";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                name = response[0]["Name"].ToString();
            }

            return name;
        }

        public static string GetTableOptionName(int optionId, int tableId)
        {
            string name = "";
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+Id+eq+" + optionId;
            urlRequest += "&$select=Name";
            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                name = options[0]["Name"].ToString();
            }
            return name;
        }
        public static string GetProductCode(int id)
        {
            InstantiateConnection();
            string productCode = "";
            string urlRequest = "Products?$filter=Id+eq+" + id;
            urlRequest += "&$select=Code";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                productCode = response[0]["Code"].ToString();

            }
            return productCode;
        }
        public static string GetCityIbgeCode(int cityId)
        {
            string cityIBGECode = "";
            string strRequest = "Cities?$select=IBGECode&$filter=Id+eq+" + cityId;
            JArray arrayRequest = RequestHandler.MakeRequest(strRequest, Method.GET);
            if (!IsNullOrEmpty(arrayRequest))
            {
                cityIBGECode = arrayRequest[0]["IBGECode"].ToString();
            }
            return cityIBGECode;
        }
        public static int GetCountryId(JToken country)
        {
            int countryId = 0;
            string urlRequest = "Cities@Countries?";
            urlRequest += "$filter=Name+eq+'" + country.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                countryId = Convert.ToInt32(response[0]["Id"]);
            }

            return countryId;
        }
        public static int GetStateId(JToken state, int countryId)
        {
            int stateId = 0;
            string urlRequest = "Cities@Countries@States?$filter=CountryId+eq+" + countryId;
            urlRequest += "+and+Short+eq+'" + state.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                stateId = Convert.ToInt32(response[0]["Id"]);
            }

            return stateId;
        }
        public static int GetCityId(JToken city, int stateId)
        {
            int cityId = 0;
            string urlRequest = "Cities?$filter=StateId+eq+" + stateId;
            urlRequest += "+and+Name+eq+'" + city.ToString() + "'";
            urlRequest += "&$select=Id";
            urlRequest += "&$orderby=IBGECode desc";
            JArray response = new JArray();
            response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                cityId = Convert.ToInt32(response[0]["Id"]);
            }

            return cityId;
        }

        public static int GetTableOptionIdByExtKey(JToken externalKey, int tableId)
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+ExternalKey+eq+'" + externalKey.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int GetTableOptionIdConcat(string name, int tableId)
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+(((contains(Name,'" + name + " -" + "'))))";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int GetTableOptionId(JToken option, int tableId)
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+Name+eq+'" + option.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int InsertTableOptionId(string option, int tableId, string externalKey = "")
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?";
            var objOption = new JObject()
            {
                {"Name", option },
                {"TableId", tableId }
            };
            if (externalKey != "")

            {
                objOption["ExternalKey"] = externalKey;
            }

            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.POST, objOption);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int GetProductGroupId(JToken group)
        {
            int optionId = 0;
            string urlRequest = "Products@Groups?";
            urlRequest += "$filter=Name+eq+'" + group.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int InsertProductGroup(JToken group)
        {
            int optionId = 0;
            string urlRequest = "Products@Groups";
            var objOption = new JObject()
            {
                {"Name", group },
            };

            JArray options = new JArray();
            options = RequestHandler.MakeRequest(urlRequest, Method.POST, objOption);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static int GetProductId(string prodCode)
        {
            InstantiateConnection();
            int productId = 0;
            string urlRequest = "Products?$filter=Code+eq+'" + prodCode + "'";
            urlRequest += "&$select=Id";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                productId = Convert.ToInt32(response[0]["Id"]);

            }
            return productId;
        }
        public static int GetUserId(string userName)
        {
            InstantiateConnection();
            int userId = 0;
            string urlRequest = "Users?$filter=Name+eq+'" + userName + "'";
            urlRequest += "&$select=Id";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                userId = Convert.ToInt32(response[0]["Id"]);

            }
            return userId;
        }
        public static int PostOrPatchProduct(JObject objProduct)
        {
            int productId = GetProductId(objProduct["Code"].ToString());
            if (productId == 0)
            {
                var responseContact = InsertProduct(objProduct);
                productId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                UpgradeProduct(objProduct, productId);
            }
            return productId;
        }
        public static int GetContactId(string register)
        {
            InstantiateConnection();
            int contactId = 0;
            string urlRequest = "Contacts?$filter=Register+eq+'" + register + "'";
            urlRequest += "&$select=Id";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                contactId = Convert.ToInt32(response[0]["Id"]);

            }
            return contactId;
        }
        public static int GetContactProductId(string serieNumber)
        {
            InstantiateConnection();
            int contactProductId = 0;
            string urlRequest = "Contacts@Products?$filter=Name+eq+'" + serieNumber + "'";
            urlRequest += "&$select=Id";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                contactProductId = Convert.ToInt32(response[0]["Id"]);
            }
            return contactProductId;
        }

        public static int GetOrderId(string order)
        {
            InstantiateConnection();
            int orderId = 0;
            string urlRequest = "Orders?$filter=OrderNumber+eq+'" + order + "'";
            urlRequest += "&$select=Id";
            JArray response = RequestHandler.MakeRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                orderId = Convert.ToInt32(response[0]["Id"]);

            }
            return orderId;
        }
        public static int PostOrPatchContact(JObject objContact)
        {
            int contactId = GetContactId(objContact["Register"].ToString());
            if (contactId == 0)
            {
                var responseContact = InsertContact(objContact);
                contactId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                UpgradeContact(objContact, contactId);
            }
            return contactId;
        }
        public static int PostOrPatchContactProduct(JObject objProduct)
        {
            int contactProductId = GetContactProductId(objProduct["Name"].ToString());
            if (contactProductId == 0)
            {
                var responseContact = InsertContactProduct(objProduct);
                contactProductId = Convert.ToInt32(responseContact["Id"]);
            }

            return contactProductId;
        }
        public static int PostOrPatchOrder(JObject objOrder)
        {
            int orderId = GetOrderId(objOrder["OrderNumber"].ToString());
            if (orderId == 0)
            {
                var responseContact = InsertOrder(objOrder);
                orderId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                UpdateOrder(orderId, objOrder);
            }
            return orderId;
        }

        public static JObject handleOtherProperties(string fieldKey, string type, JToken value)
        {
            JObject obj = new JObject
            {
                { "FieldKey", fieldKey },
                { type,value }
            };

            return obj;
        }
        public static JObject GetContact(int contactId)
        {
            InstantiateConnection();
            string urlRequest = "Contacts?$filter=Id+eq+" + contactId;
            urlRequest += "&$expand=Phones,OtherProperties";
            JArray arrayContact = new JArray();
            arrayContact = RequestHandler.MakeRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayContact))
            {
                JObject objContact = new JObject();
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }

            return null;
        }
        public static JObject InsertContact(JObject objContact)
        {
            InstantiateConnection();
            string urlRequest = "Contacts?";
            JArray arrayContact = RequestHandler.MakeRequest(urlRequest, Method.POST, objContact);
            if (!IsNullOrEmpty(arrayContact))
            {
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }
            return null;
        }
        public static JObject UpgradeContact(JObject objContact, int contactId)
        {
            InstantiateConnection();
            string urlRequest = "Contacts(" + contactId + ")";
            JArray arrayContact = RequestHandler.MakeRequest(urlRequest, Method.PATCH, objContact);
            if (!IsNullOrEmpty(arrayContact))
            {
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }
            return null;
        }
        public static JObject InsertProduct(JObject objProduct)
        {
            InstantiateConnection();
            string urlRequest = "Products?";
            JArray arrayContact = RequestHandler.MakeRequest(urlRequest, Method.POST, objProduct);
            if (!IsNullOrEmpty(arrayContact))
            {
                objProduct = JObject.Parse(arrayContact[0].ToString());
                return objProduct;
            }
            return null;
        }
        public static JObject UpgradeProduct(JObject objProduct, int productId)
        {
            InstantiateConnection();
            string urlRequest = "Products(" + productId + ")";
            JArray arrayContact = RequestHandler.MakeRequest(urlRequest, Method.PATCH, objProduct);
            if (!IsNullOrEmpty(arrayContact))
            {
                objProduct = JObject.Parse(arrayContact[0].ToString());
                return objProduct;
            }
            return null;
        }
        public static JObject InsertContactProduct(JObject objContactProduct)
        {
            InstantiateConnection();
            string urlRequest = "Contacts@Products?";
            JArray arrayContact = RequestHandler.MakeRequest(urlRequest, Method.POST, objContactProduct);
            if (!IsNullOrEmpty(arrayContact))
            {
                objContactProduct = JObject.Parse(arrayContact[0].ToString());
                return objContactProduct;
            }
            return null;
        }

        public static JArray HandlePhoneNumber(string phoneNumber)
        {
            var phones = new JArray();
            var phone = new JObject()
            {
                {"PhoneNumber", phoneNumber },
                {"CountryId", 76 }
            };
            phones.Add(phone);
            return phones;
        }
        public static JObject GetOrderAndProducts(int orderId)
        {
            InstantiateConnection();
            string urlRequest = "Orders?$filter=Id+eq+" + orderId;
            urlRequest += "&$expand=OtherProperties, Products($expand=OtherProperties)";
            JArray arrayOrder = RequestHandler.MakeRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayOrder))
            {
                JObject objOrder = new JObject();
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static JObject InsertOrder(JObject objOrder)
        {
            InstantiateConnection();
            string urlRequest = "Orders?";
            JArray arrayOrder = RequestHandler.MakeRequest(urlRequest, Method.POST, objOrder);
            if (!IsNullOrEmpty(arrayOrder))
            {
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static JObject UpdateOrder(int orderId, JObject objOrder)
        {
            string urlRequest = "Orders(" + orderId + ")";
            JArray arrayOrder = new JArray();
            arrayOrder = RequestHandler.MakeRequest(urlRequest, Method.PATCH, objOrder);
            if (!IsNullOrEmpty(arrayOrder))
            {
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }

        //======================================================================================\\
        //======================TASK METHODS====================================================\\
        public static async Task<JObject> GetAsyncContact(int contactId)
        {
            InstantiateConnection();
            string urlRequest = "Contacts?$filter=Id+eq+" + contactId;
            urlRequest += "&$expand=Phones,OtherProperties";
            JArray arrayContact = new JArray();
            arrayContact = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayContact))
            {
                JObject objContact = new JObject();
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }

            return null;
        }
        public static async Task<JObject> InsertAsyncContact(JObject objContact)
        {
            InstantiateConnection();
            string urlRequest = "Contacts?";
            JArray arrayContact = await RequestHandler.MakeAsyncRequest(urlRequest, Method.POST, objContact);
            if (!IsNullOrEmpty(arrayContact))
            {
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }
            return null;
        }
        public static async Task<JObject> UpgradeAsyncContact(JObject objContact, int contactId)
        {
            InstantiateConnection();
            string urlRequest = "Contacts(" + contactId + ")";
            JArray arrayContact = await RequestHandler.MakeAsyncRequest(urlRequest, Method.PATCH, objContact);
            if (!IsNullOrEmpty(arrayContact))
            {
                objContact = JObject.Parse(arrayContact[0].ToString());
                return objContact;
            }
            return null;
        }
        public static async Task<JObject> InsertAsyncProduct(JObject objProduct)
        {
            InstantiateConnection();
            string urlRequest = "Products?";
            JArray arrayContact = await RequestHandler.MakeAsyncRequest(urlRequest, Method.POST, objProduct);
            if (!IsNullOrEmpty(arrayContact))
            {
                objProduct = JObject.Parse(arrayContact[0].ToString());
                return objProduct;
            }
            return null;
        }
        public static async Task<JObject> UpgradeAsyncProduct(JObject objProduct, int productId)
        {
            InstantiateConnection();
            string urlRequest = "Products(" + productId + ")";
            JArray arrayContact = await RequestHandler.MakeAsyncRequest(urlRequest, Method.PATCH, objProduct);
            if (!IsNullOrEmpty(arrayContact))
            {
                objProduct = JObject.Parse(arrayContact[0].ToString());
                return objProduct;
            }
            return null;
        }
        public static async Task<JObject> InsertAsyncOrder(JObject objOrder)
        {
            InstantiateConnection();
            string urlRequest = "Orders?";
            JArray arrayOrder = await RequestHandler.MakeAsyncRequest(urlRequest, Method.POST, objOrder);
            if (!IsNullOrEmpty(arrayOrder))
            {
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static async Task<JObject> GetOrder(int orderId)
        {
            InstantiateConnection();
            string urlRequest = "Orders?$filter=Id+eq+" + orderId;
            urlRequest += "&$expand=OtherProperties";
            JArray arrayOrder = new JArray();
            arrayOrder = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayOrder))
            {
                JObject objOrder = new JObject();
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static async Task<JObject> GetOrderByOrderNumber(int orderNumber)
        {
            InstantiateConnection();
            string urlRequest = "Orders?$filter=OrderNumber+eq+" + orderNumber;
            urlRequest += "&$expand=OtherProperties";
            JArray arrayOrder = new JArray();
            arrayOrder = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayOrder))
            {
                JObject objOrder = new JObject();
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static async Task<JObject> GetAsyncOrderAndProducts(int orderId)
        {
            InstantiateConnection();
            string urlRequest = "Orders?$filter=Id+eq+" + orderId;
            urlRequest += "&$expand=OtherProperties, Products";
            JArray arrayOrder = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);

            if (!IsNullOrEmpty(arrayOrder))
            {
                JObject objOrder = new JObject();
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static async Task<JObject> UpdateAsyncOrder(int orderId, JObject objOrder)
        {
            string urlRequest = "Orders(" + orderId + ")";
            JArray arrayOrder = new JArray();
            arrayOrder = await RequestHandler.MakeAsyncRequest(urlRequest, Method.PATCH, objOrder);
            if (!IsNullOrEmpty(arrayOrder))
            {
                objOrder = JObject.Parse(arrayOrder[0].ToString());
                return objOrder;
            }
            return null;
        }
        public static async Task<int> GetAsyncTableOptionId(JToken option, int tableId)
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+Name+eq+'" + option.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static async Task<int> GetAsyncTableOptionIdByExtKey(JToken externalKey, int tableId)
        {
            int optionId = 0;
            string urlRequest = "Fields@OptionsTables@Options?$filter=TableId+eq+" + tableId;
            urlRequest += "+and+ExternalKey+eq+'" + externalKey.ToString() + "'";
            urlRequest += "&$select=Id";
            JArray options = new JArray();
            options = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(options))
            {
                optionId = Convert.ToInt32(options[0]["Id"]);
            }
            return optionId;
        }
        public static async Task<int> GetAsyncContactId(string register)
        {
            InstantiateConnection();
            int contactId = 0;
            string urlRequest = "Contacts?$filter=Register+eq+'" + register + "'";
            urlRequest += "&$select=Id";
            JArray response = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                contactId = Convert.ToInt32(response[0]["Id"]);

            }
            return contactId;
        }
        public static async Task<int> PostOrPatchAsyncContact(JObject objContact)
        {
            int contactId = await GetAsyncContactId(objContact["Register"].ToString());
            if (contactId == 0)
            {
                var responseContact = await InsertAsyncContact(objContact);
                contactId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                await UpgradeAsyncContact(objContact, contactId);
            }
            return contactId;
        }
        public static async Task<int> GetAsyncProductId(string prodCode)
        {
            InstantiateConnection();
            int productId = 0;
            string urlRequest = "Products?$filter=Code+eq+'" + prodCode + "'";
            urlRequest += "&$select=Id";
            JArray response = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                productId = Convert.ToInt32(response[0]["Id"]);

            }
            return productId;
        }
        public static async Task<int> PostOrPatchAsyncProduct(JObject objProduct)
        {
            int productId = await GetAsyncProductId(objProduct["Code"].ToString());
            if (productId == 0)
            {
                var responseContact = await InsertAsyncProduct(objProduct);
                productId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                await UpgradeAsyncProduct(objProduct, productId);
            }
            return productId;
        }
        public static async Task<int> GetAsyncOrderId(string order)
        {
            InstantiateConnection();
            int orderId = 0;
            string urlRequest = "Orders?$filter=OrderNumber+eq+'" + order + "'";
            urlRequest += "&$select=Id";
            JArray response = await RequestHandler.MakeAsyncRequest(urlRequest, Method.GET);
            if (!IsNullOrEmpty(response))
            {
                orderId = Convert.ToInt32(response[0]["Id"]);

            }
            return orderId;
        }
        public static async Task<int> PostOrPatchAsyncOrder(JObject objOrder)
        {
            int orderId = await GetAsyncOrderId(objOrder["OrderNumber"].ToString());
            if (orderId == 0)
            {
                var responseContact = await InsertAsyncOrder(objOrder);
                orderId = Convert.ToInt32(responseContact["Id"]);
            }
            else
            {
                await UpdateAsyncOrder(orderId, objOrder);
            }
            return orderId;
        }

    }
}
