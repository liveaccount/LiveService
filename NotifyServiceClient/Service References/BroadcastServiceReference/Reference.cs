﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotifyServiceClient.BroadcastServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BroadcastServiceReference.IBroadcastService", CallbackContract=typeof(NotifyServiceClient.BroadcastServiceReference.IBroadcastServiceCallback))]
    public interface IBroadcastService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastService/OpenSession", ReplyAction="http://tempuri.org/IBroadcastService/OpenSessionResponse")]
        void OpenSession();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastService/OpenSession", ReplyAction="http://tempuri.org/IBroadcastService/OpenSessionResponse")]
        System.Threading.Tasks.Task OpenSessionAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBroadcastServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastService/OnCallback", ReplyAction="http://tempuri.org/IBroadcastService/OnCallbackResponse")]
        void OnCallback();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBroadcastServiceChannel : NotifyServiceClient.BroadcastServiceReference.IBroadcastService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BroadcastServiceClient : System.ServiceModel.DuplexClientBase<NotifyServiceClient.BroadcastServiceReference.IBroadcastService>, NotifyServiceClient.BroadcastServiceReference.IBroadcastService {
        
        public BroadcastServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public BroadcastServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public BroadcastServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void OpenSession() {
            base.Channel.OpenSession();
        }
        
        public System.Threading.Tasks.Task OpenSessionAsync() {
            return base.Channel.OpenSessionAsync();
        }
    }
}