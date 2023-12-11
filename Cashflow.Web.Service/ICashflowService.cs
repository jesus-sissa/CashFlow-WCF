using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Cashflow.Web.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICashflowService" in both code and config file together.
    [ServiceContract]
    public interface ICashflowService
    {

        [OperationContract]
        string TestConnection();

        // TODO: Add your service operations here
        [OperationContract]
        TransaccionResponse InsertTransaccionDeposito(string RFC, TransaccionEntityObject TransaccionInfo);

        [OperationContract]
        TransaccionResponse InsertTransaccionRetiro(string RFC, TransaccionEntityObject TransaccionInfo);

        [OperationContract]
        TransaccionResponse UpdateDepositoReferenciaStatus(string RFC, List<TransaccionEntityObject> Depositos);

        [OperationContract]
        TransaccionResponse AddTransactionLog(string RFC, TransaccionLogEntity LogIngo);

        [OperationContract]
        eArchivo GetFileTransferAcreditaciones(DateTime Fecha);

        [OperationContract]
        FileTransferResponse SendFileTransferResponse(FileTransferResponse Response);


        [OperationContract]
        FileTransferResponse SendFileTransferRecolecciones(eArchivo ArchivoInfo);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.    
    [DataContract]
    public class TransaccionEntityObject
    {
        #region Properties
        [DataMember]
        public Int32 IdTransaccion { get; set; }
        [DataMember]
        public string ClaveSucursal { get; set; }
        [DataMember]
        public string ClaveCajero { get; set; }
        [DataMember]
        public string ClaveCliente { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public TimeSpan HoraInicio { get; set; }
        [DataMember]
        public TimeSpan HoraFin { get; set; }
        [DataMember]
        public decimal ImporteTotal { get; set; }
        [DataMember]
        public Char Status { get; set; }
        [DataMember]
        public int IdRetiro { get; set; }
        [DataMember]
        public string UsuarioRegistro { get; set; }
        [DataMember]
        public decimal TotalMXN { get; set; }
        [DataMember]
        public decimal TotalUSD { get; set; }
        [DataMember]
        public decimal TotalUSDConvert { get; set; }
        [DataMember]
        public decimal TipoCambio { get; set; }
        [DataMember]
        public string Folio { get; set; }
        [DataMember]
        public int Tipo { get; set; }
        [DataMember]
        public Int32 IdCorte { get; set; }
        [DataMember]
        public bool EsEfectivo { get; set; }
        [DataMember]
        public Int32 IdCaja { get; set; }
        [DataMember]
        public string ClaveCaja { get; set; }
        [DataMember]
        public string NumeroCuenta { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Divisa { get; set; }
        [DataMember]
        public bool Acreditado { get; set; }

        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public double NumeroRemision { get; set; }
        [DataMember]
        public string NumeroEnvase { get; set; }
        [DataMember]
        public decimal ImporteOtros { get; set; }
        [DataMember]
        public decimal ImporteOtrosD { get; set; }
        
        [DataMember]
        public List<TransaccionDetailEntityObject> Movimientos { get; set; }

        [DataMember]
        public string rowID { get; set; }
        #endregion

        #region Contructor
        public TransaccionEntityObject()
        {
            IdTransaccion = 0;
            ClaveSucursal = string.Empty;
            ClaveCajero = string.Empty;
            ClaveCliente = string.Empty;

            Fecha = DateTime.Now;
            HoraInicio = TimeSpan.Zero;
            HoraFin = TimeSpan.Zero;
            ImporteTotal = 0;
            Status = 'F';
            IdRetiro = 0;
            UsuarioRegistro = string.Empty;

            TotalMXN = 0;
            TotalUSD = 0;
            TotalUSDConvert = 0;
            TipoCambio = 0;

            Folio = string.Empty;
            Tipo = 0;
            IdCorte = 0;
            EsEfectivo = false;
            IdCaja = 0;
            ClaveCaja = string.Empty;
            NumeroCuenta = string.Empty;
            Referencia = string.Empty;
            Divisa = string.Empty;
            Acreditado = false;

            Observaciones = string.Empty;
            NumeroRemision = 0;
            NumeroEnvase = string.Empty;
            ImporteOtros = 0;
            ImporteOtrosD = 0;
            
            Movimientos = new List<TransaccionDetailEntityObject>();
        }
        #endregion
    }

    


    [DataContract]
    public class TransaccionDetailEntityObject
    {
        #region Properties
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public Int32 IdTransaccion { get; set; }
        [DataMember]
        public string ClaveSucursal { get; set; }
        [DataMember]
        public string SerieCasete { get; set; }
        [DataMember]
        public string ClaveDenominacion { get; set; }
        [DataMember]
        public decimal Denominacion { get; set; }
        [DataMember]
        public int CantidadPiezas { get; set; }
        [DataMember]
        public decimal Importe { get; set; }
        [DataMember]
        public string ClaveMoneda { get; set; }
        [DataMember]
        public string SerieValidador { get; set; }
        [DataMember]
        public Int32 NumeroValidador { get; set; }
        #endregion

        #region Constructor
        public TransaccionDetailEntityObject()
        {
            ID = 0;
            IdTransaccion = 0;
            ClaveSucursal = string.Empty;
            SerieCasete = string.Empty;
            ClaveDenominacion = String.Empty;
            CantidadPiezas = 0;
            Importe = 0;
            ClaveMoneda = string.Empty;
            SerieValidador = string.Empty;
            NumeroValidador = 0;
        }
        #endregion
    }

    [DataContract]
    public class TransaccionResponse
    {
        #region Properties
        [DataMember]
        public string RowID { get; set; }
        [DataMember]
        public bool HasError { get; set; }
        [DataMember]
        public string ResultMessage { get; set; }
        #endregion

        #region Constructor
        public TransaccionResponse()
        {
            RowID = string.Empty;
            HasError = false;
            ResultMessage = string.Empty;
        }
        #endregion
    }

    [DataContract]
    public class eArchivo 
    {
        #region Properties
        [DataMember]
        public int IdArchivo { get; set; } 
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string NombreArchivoDeposito { get; set; }
        [DataMember]
        public string ContenidoDeposito { get; set; }
        [DataMember]
        public string NombreArchivoCheque { get; set; }
        [DataMember]
        public string ContenidoCheque { get; set; }
        #endregion

        #region Constructor
        public eArchivo() 
        {
            IdArchivo = 0;
            Fecha = DateTime.Now;
            NombreArchivoDeposito = string.Empty;
            ContenidoDeposito = string.Empty;
            NombreArchivoCheque = string.Empty;
            ContenidoCheque = string.Empty;
        }
        #endregion
    }

    public class FileTransferResponse
    {
        #region Properties
        [DataMember]
        public int IdArchivo { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string ResultMessage { get; set; }
        [DataMember]
        public string ResultCode { get; set; }
        #endregion

        #region Constructor
        public FileTransferResponse()
        {
            IdArchivo = 0;
            Fecha = DateTime.Now;
            FileName = string.Empty;
            ResultMessage = string.Empty;
            ResultCode = string.Empty;
        }
        #endregion
    }

    [DataContract]
    public class TransaccionLogEntity
    {
        #region "Properties"
        [DataMember]
        public string IdCajero { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public string ClaveSucursal { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string Hora { get; set; }
        [DataMember]
        public int IdLogDescripcion { get; set; }
        [DataMember]
        public int IdPantalla { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        #endregion

        #region "Constructor"
        public TransaccionLogEntity()
        {
            IdCajero = string.Empty;
            IdUsuario = 0;
            ClaveSucursal = string.Empty;
            Fecha = DateTime.Now;
            Hora = DateTime.Now.ToShortTimeString();
            IdLogDescripcion = 0;
            IdPantalla = 0;
            Descripcion = string.Empty;
        }
        #endregion
    }
}
