using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.IO;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// OnbaseIntegrationBase class
    /// </summary>
    public class OnbaseServiceBase
    {
        /// <summary>
        /// OnbaseConector
        /// </summary>
        private readonly IOnbaseConector _onbaseConector;


        /// <summary>
        /// Onbase Unity Application Instance
        /// </summary>
        protected Hyland.Unity.Application unityApplication;

        /// <summary>
        /// OnbaseSettings Instance
        /// </summary>
        protected OnbaseSettings onbaseSettings;

        public OnbaseServiceBase(OnbaseSettings onbaseSettings, IOnbaseConector onbaseConector)
        {
            this.onbaseSettings = onbaseSettings;
            this._onbaseConector = onbaseConector;
        }

        /// <summary>
        /// Init new Onbase Form Integration and retun StoreNewUnityFormProperties to set fields and values
        /// </summary>
        /// <param name="onBaseFormID">Onbase Form Template ID</param>
        protected StoreNewUnityFormProperties InitNewForm(FormTemplate formTemplate)
        {
            var store = this.CreateNewStoreUnityForm(formTemplate);
            return store;
        }


        /// <summary>
        /// Connect to Onbase App Server 
        /// </summary>
        protected void GetConector()
        {
            try
            {
                unityApplication = this._onbaseConector.GetApplication();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Disconect()
        {
            this.unityApplication.Disconnect();
        }

        /// <summary>
        /// Update a image file image to form document
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keywordType"></param>
        /// <param name="keywordValue"></param>
        public void UploadUnityFormImage(string filePath, string keywordType, string keywordValue)
        {
            using (PageData pageData = this.unityApplication.Core.Storage.CreatePageData(new MemoryStream(Convert.FromBase64String(filePath)), ".jpg"))
            {
                DocumentType docType = this.unityApplication.Core.DocumentTypes.Find(onbaseSettings.CustomerDocumentType);
                FileType img = this.unityApplication.Core.FileTypes.Find(onbaseSettings.CustomerDocumentFileType);

                StoreNewDocumentProperties newDocProps = this.unityApplication.Core.Storage.CreateStoreNewDocumentProperties(docType, img);
                KeywordType idAnexoType = this.unityApplication.Core.KeywordTypes.Find(keywordType);
                Keyword idanexo = idAnexoType.CreateKeyword(keywordValue);
                newDocProps.AddKeyword(idanexo);

                Document newDoc = this.unityApplication.Core.Storage.StoreNewDocument(pageData, newDocProps);
            }
        }

        /// <summary>
        /// Find form tempalte by id
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        protected FormTemplate FindFormTemplate(long formID)
        {
            FormTemplate formTemplate = this.unityApplication.Core.UnityFormTemplates.Find(formID);

            if (formTemplate == null)
            {
                throw new ApplicationException($"From template with ID {formID} not found.");
            }

            return formTemplate;
        }

        /// <summary>
        /// Create a new Unity Form Store Properties
        /// </summary>
        /// <param name="formTemplate"></param>
        /// <returns></returns>
        private StoreNewUnityFormProperties CreateNewStoreUnityForm(FormTemplate formTemplate)
        {
            StoreNewUnityFormProperties properties = this.unityApplication.Core.Storage.CreateStoreNewUnityFormProperties(formTemplate);

            if (properties == null)
            {
                throw new ApplicationException("Could't create a new Onbase Store Form Properties.");
            }

            return properties;
        }

        /// <summary>
        /// Store a form to Unity application
        /// </summary>
        /// <param name="storeNew"></param>
        /// <returns></returns>
        protected Document StoreNewUnityForm(StoreNewUnityFormProperties storeNew)
        {
            return this.unityApplication.Core.Storage.StoreNewUnityForm(storeNew);
        }
    }
}
