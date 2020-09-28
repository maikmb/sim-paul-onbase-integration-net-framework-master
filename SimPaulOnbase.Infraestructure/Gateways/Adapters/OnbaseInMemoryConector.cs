namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class OnbaseInMemoryConector : IOnbaseConector
    {
        private readonly Hyland.Unity.Application _application;

        public OnbaseInMemoryConector(Hyland.Unity.Application application)
        {
            _application = application;
        }

        public Hyland.Unity.Application GetApplication()
        {
            return this._application;
        }
    }
}
