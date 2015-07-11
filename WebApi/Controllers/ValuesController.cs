namespace Api.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Http;

    using NLog;

    /// <summary>
    /// The values controller is just a test controller.
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// GET api/values - just a test method.
        /// </summary>
        public IEnumerable<string> Get()
        {
            this.logger.Info("Get in ValuesController is called");

            return new[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/values/5 - just a test method.
        /// </summary>
        public string Get(int id)
        {
            this.logger.Info(string.Format("Get, with id {0}, in ValuesController is called.", id.ToString(CultureInfo.InvariantCulture)));

            return "value";
        }

        /// <summary>
        ///  POST api/values - just a test method.
        /// </summary>
        public void Post([FromBody]string value)
        {
            this.logger.Info(string.Format("Post with value {0}, in ValuesController is called.", value));
        }

        /// <summary>
        ///  PUT api/values/5 - just a test method.
        /// </summary>
        public void Put(int id, [FromBody]string value)
        {
            this.logger.Info("Put, with id {0}, and value {1}, in ValuesController is called.", id.ToString(CultureInfo.InvariantCulture), value);
        }

        /// <summary>
        /// DELETE api/values/5 - just a test method.
        /// </summary>
        public void Delete(int id)
        {
            this.logger.Info(string.Format("Delete, with id {0}, in ValuesController is called.", id.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
