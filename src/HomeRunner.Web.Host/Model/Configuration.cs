

namespace SlidingApps.Collaboration.Web.Host.Model
{
    public sealed class ExternalConfiguration
    {
        #region - Constructors -

        /// <summary>
        /// Standaard constructor.
        /// </summary>
        public ExternalConfiguration()
            : base()
        {
            this.InitializeObject();
        }

        #endregion

        public Application Application { get; set; }

        #region - Private & protected methods -

        /// <summary>
        /// Initialiseer het object.
        /// </summary>
        private void InitializeObject()
        {
            this.Application = new Application();
        }

        #endregion
    }

    public class Application
    {
        #region - Constructors -

        /// <summary>
        /// Standaard constructor.
        /// </summary>
        public Application()
            : base()
        {
            this.InitializeObject();
        }

        #endregion

        public string BaseHREF { get; set; }

        #region - Private & protected methods -

        /// <summary>
        /// Initialiseer het object.
        /// </summary>
        private void InitializeObject()
        {
            this.BaseHREF = string.Empty;
        }

        #endregion
    }
}