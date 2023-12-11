using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Cashflow.Web.Service
{
    public class ServiceAuthenticator: UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");
            if (!(userName == ConfigurationManager.AppSettings["CashflowService_User"] && password == ConfigurationManager.AppSettings["CashflowService_Pass"]))
                throw new FaultException(string.Format("ERROR: Usuario ({0}) or contraseña incorrecto.", userName));
        }
    }
}