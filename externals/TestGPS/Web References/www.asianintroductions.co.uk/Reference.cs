// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace TestGPS.www.asianintroductions.co.uk {
    
    
    /// <remarks/>
    [System.Web.Services.WebServiceBinding(Name="GpsWebServiceTest1WebServiceSoap", Namespace="http://tempuri.org/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GpsWebServiceTest1WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddGpsInformationOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGpsInformationPreviouslySent_Last1000RecordsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGpsInformationPreviouslySent_SpecificIDOperationCompleted;
        
        public GpsWebServiceTest1WebService() {
            this.Url = "http://www.asianintroductions.co.uk/gpswebservicetest1webservice.asmx";
        }
        
        public GpsWebServiceTest1WebService(string url) {
            this.Url = url;
        }
        
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        public event AddGpsInformationCompletedEventHandler AddGpsInformationCompleted;
        
        public event GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventHandler GetGpsInformationPreviouslySent_Last1000RecordsCompleted;
        
        public event GetGpsInformationPreviouslySent_SpecificIDCompletedEventHandler GetGpsInformationPreviouslySent_SpecificIDCompleted;
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        public System.IAsyncResult BeginHelloWorld(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }
        
        public string EndHelloWorld(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks>
///Adds gps information.  pstrXML should be specified as proper XML formed document containing all GPS related information obtainable.
///</remarks>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddGpsInformation", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public bool AddGpsInformation(string pstrXML) {
            object[] results = this.Invoke("AddGpsInformation", new object[] {
                        pstrXML});
            return ((bool)(results[0]));
        }
        
        public System.IAsyncResult BeginAddGpsInformation(string pstrXML, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("AddGpsInformation", new object[] {
                        pstrXML}, callback, asyncState);
        }
        
        public bool EndAddGpsInformation(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }
        
        public void AddGpsInformationAsync(string pstrXML) {
            this.AddGpsInformationAsync(pstrXML, null);
        }
        
        public void AddGpsInformationAsync(string pstrXML, object userState) {
            if ((this.AddGpsInformationOperationCompleted == null)) {
                this.AddGpsInformationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddGpsInformationCompleted);
            }
            this.InvokeAsync("AddGpsInformation", new object[] {
                        pstrXML}, this.AddGpsInformationOperationCompleted, userState);
        }
        
        private void OnAddGpsInformationCompleted(object arg) {
            if ((this.AddGpsInformationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddGpsInformationCompleted(this, new AddGpsInformationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks>
///Returns the last 1000 entries that have been sent to the server previously
///</remarks>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetGpsInformationPreviouslySent_Last1000Records", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public GPSListItem[] GetGpsInformationPreviouslySent_Last1000Records() {
            object[] results = this.Invoke("GetGpsInformationPreviouslySent_Last1000Records", new object[0]);
            return ((GPSListItem[])(results[0]));
        }
        
        public System.IAsyncResult BeginGetGpsInformationPreviouslySent_Last1000Records(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetGpsInformationPreviouslySent_Last1000Records", new object[0], callback, asyncState);
        }
        
        public GPSListItem[] EndGetGpsInformationPreviouslySent_Last1000Records(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((GPSListItem[])(results[0]));
        }
        
        public void GetGpsInformationPreviouslySent_Last1000RecordsAsync() {
            this.GetGpsInformationPreviouslySent_Last1000RecordsAsync(null);
        }
        
        public void GetGpsInformationPreviouslySent_Last1000RecordsAsync(object userState) {
            if ((this.GetGpsInformationPreviouslySent_Last1000RecordsOperationCompleted == null)) {
                this.GetGpsInformationPreviouslySent_Last1000RecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGpsInformationPreviouslySent_Last1000RecordsCompleted);
            }
            this.InvokeAsync("GetGpsInformationPreviouslySent_Last1000Records", new object[0], this.GetGpsInformationPreviouslySent_Last1000RecordsOperationCompleted, userState);
        }
        
        private void OnGetGpsInformationPreviouslySent_Last1000RecordsCompleted(object arg) {
            if ((this.GetGpsInformationPreviouslySent_Last1000RecordsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGpsInformationPreviouslySent_Last1000RecordsCompleted(this, new GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks>
///Returns xml information on a specific record previously stored.
///</remarks>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetGpsInformationPreviouslySent_SpecificID", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public GPSListItem GetGpsInformationPreviouslySent_SpecificID(int pintID) {
            object[] results = this.Invoke("GetGpsInformationPreviouslySent_SpecificID", new object[] {
                        pintID});
            return ((GPSListItem)(results[0]));
        }
        
        public System.IAsyncResult BeginGetGpsInformationPreviouslySent_SpecificID(int pintID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetGpsInformationPreviouslySent_SpecificID", new object[] {
                        pintID}, callback, asyncState);
        }
        
        public GPSListItem EndGetGpsInformationPreviouslySent_SpecificID(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((GPSListItem)(results[0]));
        }
        
        public void GetGpsInformationPreviouslySent_SpecificIDAsync(int pintID) {
            this.GetGpsInformationPreviouslySent_SpecificIDAsync(pintID, null);
        }
        
        public void GetGpsInformationPreviouslySent_SpecificIDAsync(int pintID, object userState) {
            if ((this.GetGpsInformationPreviouslySent_SpecificIDOperationCompleted == null)) {
                this.GetGpsInformationPreviouslySent_SpecificIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGpsInformationPreviouslySent_SpecificIDCompleted);
            }
            this.InvokeAsync("GetGpsInformationPreviouslySent_SpecificID", new object[] {
                        pintID}, this.GetGpsInformationPreviouslySent_SpecificIDOperationCompleted, userState);
        }
        
        private void OnGetGpsInformationPreviouslySent_SpecificIDCompleted(object arg) {
            if ((this.GetGpsInformationPreviouslySent_SpecificIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGpsInformationPreviouslySent_SpecificIDCompleted(this, new GetGpsInformationPreviouslySent_SpecificIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class GPSListItem {
        
        /// <remarks/>
        public int RecordID;
        
        /// <remarks/>
        public string XMLContent;
        
        /// <remarks/>
        public System.DateTime RecordCreated;
    }
    
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs args);
    
    public partial class AddGpsInformationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddGpsInformationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    public delegate void AddGpsInformationCompletedEventHandler(object sender, AddGpsInformationCompletedEventArgs args);
    
    public partial class GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public GPSListItem[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((GPSListItem[])(this.results[0]));
            }
        }
    }
    
    public delegate void GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventHandler(object sender, GetGpsInformationPreviouslySent_Last1000RecordsCompletedEventArgs args);
    
    public partial class GetGpsInformationPreviouslySent_SpecificIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGpsInformationPreviouslySent_SpecificIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public GPSListItem Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((GPSListItem)(this.results[0]));
            }
        }
    }
    
    public delegate void GetGpsInformationPreviouslySent_SpecificIDCompletedEventHandler(object sender, GetGpsInformationPreviouslySent_SpecificIDCompletedEventArgs args);
}
