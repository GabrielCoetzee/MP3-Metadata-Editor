using System.Runtime.Serialization;
using MP3_MetadataEditor_RestServiceLibrary.Service_Layer.Base_Communication_Objects;
using Newtonsoft.Json;

namespace MP3_MetadataEditor_RestServiceLibrary.Service_Layer.LastFM_Service.Communication_Objects.RequestObjects
{
    public class LastFMServiceRequest : RequestBase<LastFmRequestIntermediaryObjectBody>
    {
        #region Constructor

        public LastFMServiceRequest()
        {
        }

        public LastFMServiceRequest(LastFmRequestIntermediaryObjectBody body)
        {
            Body = body;
        }

        #endregion Constructor
    }

    [DataContract]
    public class LastFmRequestIntermediaryObjectBody
    {
        [DataMember]
        public string Artist { get; set; }
        [DataMember]
        public string Song { get; set; }
    }


}
