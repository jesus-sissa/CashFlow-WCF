﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cashflow.Web.Service.WsCajerosEmpresariales {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsCajerosEmpresariales.ICajerosEmpresariales")]
    public interface ICajerosEmpresariales {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICajerosEmpresariales/GetClaveCliente", ReplyAction="http://tempuri.org/ICajerosEmpresariales/GetClaveClienteResponse")]
        string GetClaveCliente(string parametro);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICajerosEmpresariales/GetClaveCliente", ReplyAction="http://tempuri.org/ICajerosEmpresariales/GetClaveClienteResponse")]
        System.Threading.Tasks.Task<string> GetClaveClienteAsync(string parametro);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICajerosEmpresarialesChannel : Cashflow.Web.Service.WsCajerosEmpresariales.ICajerosEmpresariales, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CajerosEmpresarialesClient : System.ServiceModel.ClientBase<Cashflow.Web.Service.WsCajerosEmpresariales.ICajerosEmpresariales>, Cashflow.Web.Service.WsCajerosEmpresariales.ICajerosEmpresariales {
        
        public CajerosEmpresarialesClient() {
        }
        
        public CajerosEmpresarialesClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CajerosEmpresarialesClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CajerosEmpresarialesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CajerosEmpresarialesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetClaveCliente(string parametro) {
            return base.Channel.GetClaveCliente(parametro);
        }
        
        public System.Threading.Tasks.Task<string> GetClaveClienteAsync(string parametro) {
            return base.Channel.GetClaveClienteAsync(parametro);
        }
    }
}