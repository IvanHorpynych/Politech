﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace KA4_client.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebService1Soap", Namespace="http://tempuri.org/")]
    public partial class WebService1 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AutorizationOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRegUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback ChangeStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddMsgOperationCompleted;
        
        private System.Threading.SendOrPostCallback RefreshOperationCompleted;
        
        private System.Threading.SendOrPostCallback refreshUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetHistoryOperationCompleted;
        
        private System.Threading.SendOrPostCallback UserLogOffOperationCompleted;
        
        private System.Threading.SendOrPostCallback WriteDownToJournOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService1() {
            this.Url = global::KA4_client.Properties.Settings.Default.KA4_client_localhost_WebService1;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event AutorizationCompletedEventHandler AutorizationCompleted;
        
        /// <remarks/>
        public event RegisterCompletedEventHandler RegisterCompleted;
        
        /// <remarks/>
        public event GetStatusCompletedEventHandler GetStatusCompleted;
        
        /// <remarks/>
        public event GetRegUsersCompletedEventHandler GetRegUsersCompleted;
        
        /// <remarks/>
        public event ChangeStatusCompletedEventHandler ChangeStatusCompleted;
        
        /// <remarks/>
        public event AddMsgCompletedEventHandler AddMsgCompleted;
        
        /// <remarks/>
        public event RefreshCompletedEventHandler RefreshCompleted;
        
        /// <remarks/>
        public event refreshUsersCompletedEventHandler refreshUsersCompleted;
        
        /// <remarks/>
        public event GetHistoryCompletedEventHandler GetHistoryCompleted;
        
        /// <remarks/>
        public event UserLogOffCompletedEventHandler UserLogOffCompleted;
        
        /// <remarks/>
        public event WriteDownToJournCompletedEventHandler WriteDownToJournCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Autorization", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Autorization(string name, string pass) {
            object[] results = this.Invoke("Autorization", new object[] {
                        name,
                        pass});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void AutorizationAsync(string name, string pass) {
            this.AutorizationAsync(name, pass, null);
        }
        
        /// <remarks/>
        public void AutorizationAsync(string name, string pass, object userState) {
            if ((this.AutorizationOperationCompleted == null)) {
                this.AutorizationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAutorizationOperationCompleted);
            }
            this.InvokeAsync("Autorization", new object[] {
                        name,
                        pass}, this.AutorizationOperationCompleted, userState);
        }
        
        private void OnAutorizationOperationCompleted(object arg) {
            if ((this.AutorizationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AutorizationCompleted(this, new AutorizationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Register", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Register(string smth) {
            object[] results = this.Invoke("Register", new object[] {
                        smth});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RegisterAsync(string smth) {
            this.RegisterAsync(smth, null);
        }
        
        /// <remarks/>
        public void RegisterAsync(string smth, object userState) {
            if ((this.RegisterOperationCompleted == null)) {
                this.RegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterOperationCompleted);
            }
            this.InvokeAsync("Register", new object[] {
                        smth}, this.RegisterOperationCompleted, userState);
        }
        
        private void OnRegisterOperationCompleted(object arg) {
            if ((this.RegisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterCompleted(this, new RegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetStatus(string Authname) {
            object[] results = this.Invoke("GetStatus", new object[] {
                        Authname});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetStatusAsync(string Authname) {
            this.GetStatusAsync(Authname, null);
        }
        
        /// <remarks/>
        public void GetStatusAsync(string Authname, object userState) {
            if ((this.GetStatusOperationCompleted == null)) {
                this.GetStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetStatusOperationCompleted);
            }
            this.InvokeAsync("GetStatus", new object[] {
                        Authname}, this.GetStatusOperationCompleted, userState);
        }
        
        private void OnGetStatusOperationCompleted(object arg) {
            if ((this.GetStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetStatusCompleted(this, new GetStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetRegUsers", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetRegUsers() {
            object[] results = this.Invoke("GetRegUsers", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetRegUsersAsync() {
            this.GetRegUsersAsync(null);
        }
        
        /// <remarks/>
        public void GetRegUsersAsync(object userState) {
            if ((this.GetRegUsersOperationCompleted == null)) {
                this.GetRegUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRegUsersOperationCompleted);
            }
            this.InvokeAsync("GetRegUsers", new object[0], this.GetRegUsersOperationCompleted, userState);
        }
        
        private void OnGetRegUsersOperationCompleted(object arg) {
            if ((this.GetRegUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRegUsersCompleted(this, new GetRegUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangeStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ChangeStatus(string name, string status) {
            this.Invoke("ChangeStatus", new object[] {
                        name,
                        status});
        }
        
        /// <remarks/>
        public void ChangeStatusAsync(string name, string status) {
            this.ChangeStatusAsync(name, status, null);
        }
        
        /// <remarks/>
        public void ChangeStatusAsync(string name, string status, object userState) {
            if ((this.ChangeStatusOperationCompleted == null)) {
                this.ChangeStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnChangeStatusOperationCompleted);
            }
            this.InvokeAsync("ChangeStatus", new object[] {
                        name,
                        status}, this.ChangeStatusOperationCompleted, userState);
        }
        
        private void OnChangeStatusOperationCompleted(object arg) {
            if ((this.ChangeStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ChangeStatusCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddMsg", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddMsg(string msg) {
            this.Invoke("AddMsg", new object[] {
                        msg});
        }
        
        /// <remarks/>
        public void AddMsgAsync(string msg) {
            this.AddMsgAsync(msg, null);
        }
        
        /// <remarks/>
        public void AddMsgAsync(string msg, object userState) {
            if ((this.AddMsgOperationCompleted == null)) {
                this.AddMsgOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddMsgOperationCompleted);
            }
            this.InvokeAsync("AddMsg", new object[] {
                        msg}, this.AddMsgOperationCompleted, userState);
        }
        
        private void OnAddMsgOperationCompleted(object arg) {
            if ((this.AddMsgCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddMsgCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Refresh", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Refresh(string fname) {
            object[] results = this.Invoke("Refresh", new object[] {
                        fname});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RefreshAsync(string fname) {
            this.RefreshAsync(fname, null);
        }
        
        /// <remarks/>
        public void RefreshAsync(string fname, object userState) {
            if ((this.RefreshOperationCompleted == null)) {
                this.RefreshOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRefreshOperationCompleted);
            }
            this.InvokeAsync("Refresh", new object[] {
                        fname}, this.RefreshOperationCompleted, userState);
        }
        
        private void OnRefreshOperationCompleted(object arg) {
            if ((this.RefreshCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RefreshCompleted(this, new RefreshCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/refreshUsers", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string refreshUsers() {
            object[] results = this.Invoke("refreshUsers", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void refreshUsersAsync() {
            this.refreshUsersAsync(null);
        }
        
        /// <remarks/>
        public void refreshUsersAsync(object userState) {
            if ((this.refreshUsersOperationCompleted == null)) {
                this.refreshUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrefreshUsersOperationCompleted);
            }
            this.InvokeAsync("refreshUsers", new object[0], this.refreshUsersOperationCompleted, userState);
        }
        
        private void OnrefreshUsersOperationCompleted(object arg) {
            if ((this.refreshUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.refreshUsersCompleted(this, new refreshUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetHistory", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetHistory() {
            object[] results = this.Invoke("GetHistory", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetHistoryAsync() {
            this.GetHistoryAsync(null);
        }
        
        /// <remarks/>
        public void GetHistoryAsync(object userState) {
            if ((this.GetHistoryOperationCompleted == null)) {
                this.GetHistoryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetHistoryOperationCompleted);
            }
            this.InvokeAsync("GetHistory", new object[0], this.GetHistoryOperationCompleted, userState);
        }
        
        private void OnGetHistoryOperationCompleted(object arg) {
            if ((this.GetHistoryCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetHistoryCompleted(this, new GetHistoryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UserLogOff", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UserLogOff(string name) {
            object[] results = this.Invoke("UserLogOff", new object[] {
                        name});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UserLogOffAsync(string name) {
            this.UserLogOffAsync(name, null);
        }
        
        /// <remarks/>
        public void UserLogOffAsync(string name, object userState) {
            if ((this.UserLogOffOperationCompleted == null)) {
                this.UserLogOffOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserLogOffOperationCompleted);
            }
            this.InvokeAsync("UserLogOff", new object[] {
                        name}, this.UserLogOffOperationCompleted, userState);
        }
        
        private void OnUserLogOffOperationCompleted(object arg) {
            if ((this.UserLogOffCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserLogOffCompleted(this, new UserLogOffCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WriteDownToJourn", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void WriteDownToJourn(string sign) {
            this.Invoke("WriteDownToJourn", new object[] {
                        sign});
        }
        
        /// <remarks/>
        public void WriteDownToJournAsync(string sign) {
            this.WriteDownToJournAsync(sign, null);
        }
        
        /// <remarks/>
        public void WriteDownToJournAsync(string sign, object userState) {
            if ((this.WriteDownToJournOperationCompleted == null)) {
                this.WriteDownToJournOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWriteDownToJournOperationCompleted);
            }
            this.InvokeAsync("WriteDownToJourn", new object[] {
                        sign}, this.WriteDownToJournOperationCompleted, userState);
        }
        
        private void OnWriteDownToJournOperationCompleted(object arg) {
            if ((this.WriteDownToJournCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WriteDownToJournCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AutorizationCompletedEventHandler(object sender, AutorizationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AutorizationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AutorizationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void RegisterCompletedEventHandler(object sender, RegisterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetStatusCompletedEventHandler(object sender, GetStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetRegUsersCompletedEventHandler(object sender, GetRegUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRegUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRegUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ChangeStatusCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AddMsgCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void RefreshCompletedEventHandler(object sender, RefreshCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RefreshCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RefreshCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void refreshUsersCompletedEventHandler(object sender, refreshUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class refreshUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal refreshUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetHistoryCompletedEventHandler(object sender, GetHistoryCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetHistoryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetHistoryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void UserLogOffCompletedEventHandler(object sender, UserLogOffCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserLogOffCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserLogOffCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void WriteDownToJournCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591