using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Configuration;
using System.Text;
using Cashflow.Web.Service.Clases;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Cashflow.Web.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CashflowService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CashflowService.svc or CashflowService.svc.cs at the Solution Explorer and start debugging.
    public class CashflowService : ICashflowService
    {

        #region Constructor
        
        #endregion

        #region Methods
        public string TestConnection()
        {
            string version = "1.0.0.13";
            return string.Format("Connection Success{0} [v{1}]", "", version);
        }

        #region "Cashflow Web"
        public TransaccionResponse InsertTransaccionDeposito(string RFC, TransaccionEntityObject TransaccionInfo)
        {
            TransaccionResponse _transaccionResponse = new TransaccionResponse();

            SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject _ConnectionBAL;
            SISSA.Cashflow.Web.Core.Entities.ConnectionClientEntityObject _ConnectionInfo;
            SISSA.Cashflow.Web.Core.Business.DepositoBusinessObject _DepositoBAL;
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _TransaccionInfo;
            string _connection = string.Empty; 

            try
            {
                //Se obtiene el string de conexion correspondiente al cliente
                _ConnectionBAL = new SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject();
                _ConnectionInfo = _ConnectionBAL.GetConnectionStringByRFC(RFC);
                _connection = _ConnectionInfo.ConnectionString;

                //Se convierte la transaccion del tipo Cashflow.Web.Service.TransaccionEntityObject a SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject
                _TransaccionInfo = ConvertRemoteTransaccionToLocalTransaccionObject(TransaccionInfo);

                //DEBUG: Get sample data to test
                //_TransaccionInfo = GetSampleDataDeposito();

                //Se genera el objeto Deposito de capa de negocio
                _DepositoBAL = new SISSA.Cashflow.Web.Core.Business.DepositoBusinessObject(_ConnectionInfo.ConnectionString);
                _transaccionResponse.RowID = _DepositoBAL.InsertTransaccion(RFC, _TransaccionInfo);
            }
            catch (Exception ex)
            {
                _transaccionResponse.HasError = true;
                _transaccionResponse.ResultMessage = ex.Message;
                _transaccionResponse.ResultMessage += string.Format(" // Connection: {0}", _connection);
            }

            return _transaccionResponse;
        }

        public TransaccionResponse InsertTransaccionRetiro(string RFC, TransaccionEntityObject TransaccionInfo)
        {
            TransaccionResponse _transaccionResponse = new TransaccionResponse();

            SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject _ConnectionBAL;
            SISSA.Cashflow.Web.Core.Entities.ConnectionClientEntityObject _ConnectionInfo;
            SISSA.Cashflow.Web.Core.Business.RetiroBusinessObject _RetiroBAL;
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _TransaccionInfo;

            try
            {
                //Se obtiene el string de conexion correspondiente al cliente
                _ConnectionBAL = new SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject();
                _ConnectionInfo = _ConnectionBAL.GetConnectionStringByRFC(RFC);

                //Se valida que exista la conexion para el RFC indicado
                if (_ConnectionInfo == null)
                    throw new Exception(string.Format("No existe configuracion de conexion para el RFC [{0}]", RFC));

                //Se convierte la transaccion del tipo Cashflow.Web.Service.TransaccionEntityObject a SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject
                _TransaccionInfo = ConvertRemoteTransaccionToLocalTransaccionObject(TransaccionInfo);

                //DEBUG: Get sample data to test
                //_TransaccionInfo = GetSampleDataRetiro();

                //Se genera el objeto Deposito de capa de negocio
                _RetiroBAL = new SISSA.Cashflow.Web.Core.Business.RetiroBusinessObject(_ConnectionInfo.ConnectionString);
                _transaccionResponse.RowID = _RetiroBAL.InsertTransaccion(RFC, _TransaccionInfo);
            }
            catch (Exception ex)
            {
                _transaccionResponse.HasError = true;
                _transaccionResponse.ResultMessage = ex.Message;
            }

            return _transaccionResponse;
        }

        public TransaccionResponse UpdateDepositoReferenciaStatus(string RFC, List<TransaccionEntityObject> Depositos)
        {
            string _tracking = string.Empty;
            TransaccionResponse _transaccionResponse = new TransaccionResponse();

            SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject _ConnectionBAL;
            SISSA.Cashflow.Web.Core.Entities.ConnectionClientEntityObject _ConnectionInfo;
            SISSA.Cashflow.Web.Core.Business.DepositoBusinessObject _DepositoBAL; 
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _TransaccionInfo;

            try
            {
                //debug
                _tracking = "(t.1)";

                //Se obtiene el string de conexion correspondiente al cliente
                _ConnectionBAL = new SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject();
                _ConnectionInfo = _ConnectionBAL.GetConnectionStringByRFC(RFC);

                //Se valida que exista la conexion para el RFC indicado
                if (_ConnectionInfo == null)
                    throw new Exception(string.Format("No existe configuracion de conexion para el RFC [{0}]", RFC));

                _DepositoBAL = new SISSA.Cashflow.Web.Core.Business.DepositoBusinessObject(_ConnectionInfo.ConnectionString);

                //debug
                _tracking = "(t.2)";

                foreach (TransaccionEntityObject deposito in Depositos)
                {
                    //debug
                    _tracking = "(t.3.1)";
                    _TransaccionInfo = ConvertRemoteTransaccionToLocalTransaccionObject(deposito);

                    //debug
                    _tracking = "(t.3.2)";

                    //Se genera el objeto Deposito de capa de negocio
                    _DepositoBAL.UpdateReferenciaAndEstatus(_TransaccionInfo);
                }

                //debug
                _tracking = "(t.4)";

                _transaccionResponse.HasError = false;
                _transaccionResponse.ResultMessage = "OK";
            }
            catch (Exception ex)
            {
                _transaccionResponse.HasError = true;
                _transaccionResponse.ResultMessage = string.Format("{0} : {1}", ex.Message,_tracking);
            }

            return _transaccionResponse;
        }

        public TransaccionResponse AddTransactionLog(string RFC, TransaccionLogEntity LogInfo)
        {
            TransaccionResponse _transaccionResponse = new TransaccionResponse();

            SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject _ConnectionBAL;
            SISSA.Cashflow.Web.Core.Entities.ConnectionClientEntityObject _ConnectionInfo;
            SISSA.Cashflow.Web.Core.Business.TransaccionLogBusinessObject _TransaccionLogBAL;
            SISSA.Cashflow.Web.Core.Entities.TransaccionLogEntityObject _TransaccionLogInfo;
            string _connection = string.Empty;

            try
            {
                if (LogInfo != null)
                {
                    //Se obtiene el string de conexion correspondiente al cliente
                    _ConnectionBAL = new SISSA.Cashflow.Web.Core.Business.ConnectionClientBusinessObject();
                    _ConnectionInfo = _ConnectionBAL.GetConnectionStringByRFC(RFC);
                    _connection = _ConnectionInfo.ConnectionString;

                    _TransaccionLogInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionLogEntityObject
                    {
                        IdCajero = LogInfo.IdCajero,
                        IdUsuario = LogInfo.IdUsuario,
                        ClaveSucursal = LogInfo.ClaveSucursal,
                        Fecha = LogInfo.Fecha,
                        Hora = LogInfo.Hora,
                        IdDescripcion = LogInfo.IdLogDescripcion,
                        IdPantalla = LogInfo.IdPantalla,
                        Descripcion = LogInfo.Descripcion
                    };

                    //Se genera el objeto Deposito de capa de negocio
                    _TransaccionLogBAL = new SISSA.Cashflow.Web.Core.Business.TransaccionLogBusinessObject(_ConnectionInfo.ConnectionString);
                    _transaccionResponse.RowID = _TransaccionLogBAL.InsertTransaccionLog(_TransaccionLogInfo);
                }

            }
            catch (Exception ex)
            {
                _transaccionResponse.HasError = true;
                _transaccionResponse.ResultMessage = ex.Message;
                _transaccionResponse.ResultMessage += string.Format(" // Connection: {0}", _connection);
            }
               
            return _transaccionResponse;
        }
        #endregion

        #region "Cajeros Empresariales"
        public eArchivo GetFileTransferAcreditaciones(DateTime Fecha)
        {
            eArchivo _eArchivo = new eArchivo();
            SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject _CajerosEmpresarialesBAL = new SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject();
            SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject _ValidateAcreditacionBAL = new SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject();
            //cadena de conexion
            string conn = ConfigurationManager.ConnectionStrings["ConexionCajerosEmpresariales"].ConnectionString;
            //creacion de la conexion desde el constructor de la capa de Business de la clase RecoleccionBusinessObject
            _ValidateAcreditacionBAL = new SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject(conn);

            string fileNameDepositos = string.Empty;
            string fileNameCheques = string.Empty;

            ResponseLocal ValidaAcreditaciones = new ResponseLocal();

            string line;
            object[] @params;
            DateTime fecha;
            int consecutivo;
            decimal total = 0;
            Int16 count = 0;

            DataTable dt;

            try
            {
                dt = _CajerosEmpresarialesBAL.ProcesaTransaccionesAcreditables(Fecha);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int32 idarchivo = 0;
                    StringBuilder sb = new StringBuilder();

                    //Se obtiene el ID del archivo a procesar
                    _eArchivo.IdArchivo = Convert.ToInt32(dt.Rows[0]["Id_Acreditacion"]);

                    // //Se obtiene primer renglon 
                    @params = new object[] { "8072", "90011", Fecha.ToString("yyMMdd") };
                    _eArchivo.NombreArchivoDeposito = string.Format("D{0}", dt.Rows[0]["Archivo"].ToString().Substring(1));
                    _eArchivo.NombreArchivoCheque = string.Format("C{0}", dt.Rows[0]["Archivo"].ToString().Substring(1));

                    //Se obtienen datos para encabezado
                    fecha = (DateTime)dt.Rows[0]["Fecha_Transmision"];
                    consecutivo = (int)dt.Rows[0]["Consecutivo"];

                    foreach (DataRow r in dt.Rows)
                    {
                        total += System.Convert.ToDecimal(r["Monto"]);                        
                    }

                    //Se genera primer registro del archivo
                    @params = new Object[] { "D", "8072", "90011", fecha.ToString("yyyyMMdd"), consecutivo, dt.Rows.Count.ToString(), total.ToString() };
                    line = String.Format("{0},{1},{2},{3},{4},{5},{6}", @params);
                    sb.Append(line);
                    sb.AppendLine();

                    foreach (DataRow row in dt.Rows)
                    {
                        count += 1;

                        @params = new object[] { count.ToString(), row["Cuenta"].ToString().Trim(), "0", "00000000000000000000", "01", row["Monto"], "0.00", "0.00", row["Monto"], "N", row["DETALLE"] };
                        line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},0.00,{9}{10}", @params);

                        sb.Append(line);

                        if (dt.Rows.IndexOf(row) < dt.Rows.Count - 1)
                            sb.AppendLine();
                    }

                    //Se inicializa archivo objeto de retorno
                    _eArchivo.ContenidoDeposito = sb.ToString();
                    _eArchivo.ContenidoCheque = "";

                    //copia de respaldo del archivo generado
                    //ValidaAcreditaciones = ValidateAcreditaciones(_eArchivo);

                    //if (!ValidaAcreditaciones.EXITOSO)
                    //{
                    //    _eArchivo.ContenidoDeposito = "Error Local Inesperado: " + ValidaAcreditaciones.MESSAGERETURN;
                    //    UInt16 exito = 0;
                    //    exito = _ValidateAcreditacionBAL.RollbackUpdateAcreditacion(_eArchivo.IdArchivo, _eArchivo.NombreArchivoDeposito, _eArchivo.ContenidoDeposito);
                    //    if (exito == 1) { _eArchivo.ContenidoDeposito = "Rollback: Exitoso. " + "Error local Inesperado: " + ValidaAcreditaciones.MESSAGERETURN; }
                    //    else { _eArchivo.ContenidoDeposito = "Rollback: Sin Exito. Urgentemente notifique al administrador. " + "Error Local Inesperado: " + ValidaAcreditaciones.MESSAGERETURN; }
                    //}
                }

                return _eArchivo;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileTransferResponse SendFileTransferResponse(FileTransferResponse Response)
        {
            //Variable local de respuesta
            FileTransferResponse Result = new FileTransferResponse();
            SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject _ValidateAcreditacionBAL = new SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject();
            //cadena de conexion
            string conn = ConfigurationManager.ConnectionStrings["ConexionCajerosEmpresariales"].ConnectionString;
            //creacion de la conexion desde el constructor de la capa de Business de la clase RecoleccionBusinessObject
            _ValidateAcreditacionBAL = new SISSA.Cashflow.Web.Core.Business.CajerosEmpresarialesBusinessObject(conn);

            try
            {
                if (Response.ResultCode == "ERROR")
                {
                    UInt16 exito = 0;
                    exito = _ValidateAcreditacionBAL.RollbackUpdateAcreditacion(Response.IdArchivo, Response.FileName, Response.ResultMessage);
                    if (exito == 1) { Result.ResultCode = "OK"; Result.ResultMessage = "Rollback: Exitoso."; }
                    else { Result.ResultCode = "ERROR"; Result.ResultMessage = "Rollback: Sin Exito. Urgentemente notifique al administrador."; }
                }
                else if (Response.ResultCode == "OK")
                {
                    UInt16 exito = 0;
                    exito = _ValidateAcreditacionBAL.SucessAcreditacion(Response.IdArchivo, Response.FileName, Response.ResultMessage);
                    if (exito == 1) { Result.ResultCode = "OK"; Result.ResultMessage = "Acreditacion procesada Correctamente"; }
                    else { Result.ResultCode = "ERROR"; Result.ResultMessage = "Error Inesperado: Acreditacion no procesada Correctamente"; }
                }

                return Result;
            }
            catch (Exception ex)
            {
                Result.ResultCode = "ERROR"; Result.ResultMessage = "Ocurrio una excepsion: " + ex.Message;
                return Result;
            }
        }

        public FileTransferResponse SendFileTransferRecolecciones(eArchivo ArchivoInfo)
        {
            //Variable de respuesta del metodo SendFileTransferRecolecciones
            FileTransferResponse _response = new FileTransferResponse();
            //Declaracion de objeto tipo SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject
            SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject _RecoleccionInfo;
            //Declaramos objetos que interactuan con la BD (Capa logica -> Datos)
            SISSA.Cashflow.Web.Core.Business.RecoleccionesBusinessObject _RecoleccionesBAL;

            try
            {
                //cadena de conexion
                string conn = ConfigurationManager.ConnectionStrings["ConexionCajerosEmpresariales"].ConnectionString;
                //creacion de la conexion desde el constructor de la capa de Business de la clase RecoleccionBusinessObject
                _RecoleccionesBAL = new SISSA.Cashflow.Web.Core.Business.RecoleccionesBusinessObject(conn);

                //Se convierte la recoleccion del tipo eArchivo a SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject
                _RecoleccionInfo = ConvertRemoteRecoleccionToLocalRecoleccionObject(ArchivoInfo);

                //Guardado de la recoleccion y respueta del servicio
                if (_RecoleccionInfo.Folio == 1)
                {
                    _RecoleccionesBAL.InsertRecoleciones(_RecoleccionInfo);

                    _response.Fecha = ArchivoInfo.Fecha;
                    _response.FileName = ArchivoInfo.NombreArchivoDeposito;
                    _response.ResultMessage = "OK";
                    _response.ResultCode = "OK";
                    return _response;
                }
                else
                {
                    string varmsg = string.Empty;
                    foreach(SISSA.Cashflow.Web.Core.Entities.RecoleccionResponseEntityObject msg in _RecoleccionInfo.Mensages)
                    {
                        varmsg += msg.MessageReturn;
                    }

                    _response.Fecha = ArchivoInfo.Fecha;
                    _response.FileName = ArchivoInfo.NombreArchivoDeposito;
                    _response.ResultMessage = "ERROR: "+ varmsg;
                    _response.ResultCode = "ERROR";
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.Fecha = ArchivoInfo.Fecha;
                _response.FileName = ArchivoInfo.NombreArchivoDeposito;
                _response.ResultMessage = "ERROR:" + ex.Message;
                _response.ResultCode = "ERROR";
                return _response;
            }
            
        }

        #endregion

        #endregion

        #region Functions
        private SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject ConvertRemoteTransaccionToLocalTransaccionObject(TransaccionEntityObject RemoteTransaccionInfo)
        {
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _LocalTransaccionInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject();
            SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject _LocalDetail;

            if (RemoteTransaccionInfo != null)
            {
                _LocalTransaccionInfo.IdTransaccion = RemoteTransaccionInfo.IdTransaccion;
                _LocalTransaccionInfo.ClaveSucursal = RemoteTransaccionInfo.ClaveSucursal;
                _LocalTransaccionInfo.ClaveCajero = RemoteTransaccionInfo.ClaveCajero;
                _LocalTransaccionInfo.Fecha = RemoteTransaccionInfo.Fecha;
                _LocalTransaccionInfo.HoraInicio = RemoteTransaccionInfo.HoraInicio;
                _LocalTransaccionInfo.HoraFin = RemoteTransaccionInfo.HoraFin;
                _LocalTransaccionInfo.ImporteTotal = RemoteTransaccionInfo.ImporteTotal;
                _LocalTransaccionInfo.Status = RemoteTransaccionInfo.Status;
                _LocalTransaccionInfo.IdRetiro = RemoteTransaccionInfo.IdRetiro;
                _LocalTransaccionInfo.UsuarioRegistro = RemoteTransaccionInfo.UsuarioRegistro;
                _LocalTransaccionInfo.TotalMXN = RemoteTransaccionInfo.TotalMXN;
                _LocalTransaccionInfo.TotalUSD = RemoteTransaccionInfo.TotalUSD;
                _LocalTransaccionInfo.TotalUSDConvert = RemoteTransaccionInfo.TotalUSDConvert;
                _LocalTransaccionInfo.TipoCambio = RemoteTransaccionInfo.TipoCambio;
                _LocalTransaccionInfo.Folio = RemoteTransaccionInfo.Folio;
                _LocalTransaccionInfo.Tipo = RemoteTransaccionInfo.Tipo;
                _LocalTransaccionInfo.IdCorte = RemoteTransaccionInfo.IdCorte;
                _LocalTransaccionInfo.EsEfectivo = RemoteTransaccionInfo.EsEfectivo;
                _LocalTransaccionInfo.IdCaja = RemoteTransaccionInfo.IdCaja;
                _LocalTransaccionInfo.ClaveCaja = RemoteTransaccionInfo.ClaveCaja;
                _LocalTransaccionInfo.NumeroCuenta = RemoteTransaccionInfo.NumeroCuenta;
                _LocalTransaccionInfo.Referencia = RemoteTransaccionInfo.Referencia;
                _LocalTransaccionInfo.Divisa = RemoteTransaccionInfo.Divisa;
                _LocalTransaccionInfo.Acreditado = RemoteTransaccionInfo.Acreditado;
                _LocalTransaccionInfo.rowID = RemoteTransaccionInfo.rowID;

                _LocalTransaccionInfo.Observaciones = RemoteTransaccionInfo.Observaciones;
                _LocalTransaccionInfo.NumeroRemision = RemoteTransaccionInfo.NumeroRemision;
                _LocalTransaccionInfo.NumeroEnvase = RemoteTransaccionInfo.NumeroEnvase;
                _LocalTransaccionInfo.ImporteOtros = RemoteTransaccionInfo.ImporteOtros;
                _LocalTransaccionInfo.ImporteOtrosD = RemoteTransaccionInfo.ImporteOtrosD;

                if (RemoteTransaccionInfo.Movimientos != null && RemoteTransaccionInfo.Movimientos.Count > 0)
                { 
                    foreach (TransaccionDetailEntityObject remoteDatial in RemoteTransaccionInfo.Movimientos)
                    {
                        _LocalDetail = new SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject();
                        _LocalDetail.IdTransaccion = remoteDatial.IdTransaccion;
                        _LocalDetail.ClaveSucursal = remoteDatial.ClaveSucursal;
                        _LocalDetail.SerieCasete = remoteDatial.SerieCasete;
                        _LocalDetail.ClaveDenominacion = remoteDatial.ClaveDenominacion;
                        _LocalDetail.Denominacion = remoteDatial.Denominacion;
                        _LocalDetail.CantidadPiezas = remoteDatial.CantidadPiezas;
                        _LocalDetail.Importe = remoteDatial.Importe;
                        _LocalDetail.ClaveMoneda = remoteDatial.ClaveMoneda;
                        _LocalDetail.SerieValidador = remoteDatial.SerieValidador;
                        _LocalDetail.NumeroValidador = remoteDatial.NumeroValidador;

                        _LocalTransaccionInfo.Movimientos.Add(_LocalDetail);
                    }
                }
            }

            return _LocalTransaccionInfo;
        }

        private ResponseLocal ValidateAcreditaciones(eArchivo RemoteeArchivo)
        {
            //variables de respuesta
            ResponseLocal Correcto, varRespuestaCRIFD;
            Correcto = new ResponseLocal();
            Correcto.EXITOSO = true;
            bool exito = true;
            string mensajerespuesta = string.Empty;

            
            try
            {
                //Variables para guardado de archivo de texto
                String NameFile = string.Empty, ExtentionFile = String.Empty, NameFileC = string.Empty;
                System.IO.StreamReader file = null;
                String line = String.Empty, CRC = String.Empty, fileFullPath = String.Empty, fileFullPathBack = String.Empty;
                String[] NameDiscompose;
                DateTime fecha = DateTime.Now;
                String fechaString = fecha.ToString("ddMMyyyy_HHmmss");
                String fechaDiscompose;
                DateTime fechaUp;
                Decimal MasterAditivo = 0, MasterTotal = 0;
                string[] param, paramb;
                bool encabezado = false;
                //Validando nombre del archivo
                NameFileC = RemoteeArchivo.NombreArchivoDeposito;
                NameDiscompose = NameFileC.Split('.');
                if (NameDiscompose.Length > 1)
                {
                    NameFile = NameDiscompose[0];
                    ExtentionFile = NameDiscompose[1];
                }
                else
                {
                    NameFile = NameDiscompose[0];
                    ExtentionFile = "txt";
                }

                //Se declara la ruta donde se guarda el archivo de texto plano y su backup
                //param = new string[] { System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "archivos", "D807290011210303_09-VA.txt" };//NameFile + "." + ExtentionFile };
                param = new string[] { System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "archivos", NameFile+ "-VA." + ExtentionFile };
                paramb = new string[] { System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "archivos", "AcreditacionesBack", NameFile + "_" + fechaString + "." + ExtentionFile };
                fileFullPath = string.Format(@"{0}\{1}\{2}", param);
                fileFullPathBack = string.Format(@"{0}\{1}\{2}\{3}", paramb);

                //Se elimina el archivo en caso de existencia en la carpeta de archivos
                try
                {
                    File.Delete(fileFullPath);
                }
                catch (Exception ex)
                { }


                //Validar que la variable eArchivo no este vacio
                if (RemoteeArchivo != null)
                {
                    //Se guardan los archivos en directorio
                    using (StreamWriter writer = new StreamWriter(fileFullPath))
                    {
                        writer.Write(RemoteeArchivo.ContenidoDeposito);
                    }
                    //Realizar respaldo de archivo
                    File.Copy(fileFullPath, fileFullPathBack);

                    //Lectura del archivo txt guardado
                    file = new System.IO.StreamReader(fileFullPath);
                    Int64 contadorlinea = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        //leer la linea de encabezado el archivo
                        if (line.Contains("D") && encabezado == false)
                        {
                            string phrase = line;
                            string[] words = phrase.Split(',');
                            string dia = string.Empty;
                            string mes = string.Empty;
                            string anio = string.Empty;

                            if (words[0] == "D")
                            { encabezado = true; }
                            else
                            { exito = false; mensajerespuesta += "ME0131-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba la constante D, 57" + "|"; }
                            //validar CRC
                            try
                            { Int64.Parse(words[1]); }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0132-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba un numero de caja" + "|"; }
                            //validar proveedor
                            try
                            { Int64.Parse(words[2]); }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0133-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba un numero de proveedor" + "|"; }
                            //validar fecha
                            fechaDiscompose = words[3];
                            for (int i = 0; i < fechaDiscompose.Length; i++)
                            {
                                if (i <= 3)
                                {
                                    anio += fechaDiscompose[i];
                                }
                                if (i > 3 && i <= 5)
                                {
                                    mes += fechaDiscompose[i];
                                }
                                if (i > 5 && i <= 7)
                                { dia += fechaDiscompose[i]; }
                            }
                            try
                            { fechaUp = Convert.ToDateTime(dia + "/" + mes + "/" + anio + " 00:00:00"); }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0133-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba dato de fecha" + "|"; }
                            //validar secuencia
                            try
                            {
                                Int64.Parse(words[4]);
                                if (Convert.ToInt64(words[4]) == 0 || Convert.ToInt64(words[4]) < 0)
                                { exito = false; mensajerespuesta += "ME0136-" + "Contenido del Archivo, [#" + contadorlinea + "],  Secuencia igual o menor a cero" + "|"; }
                                
                            }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0135-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba numero de registros" + "|"; }
                            //validar cantidad registros
                            try
                            {
                                Int64.Parse(words[5]);
                                if (Convert.ToInt64(words[5]) == 0 || Convert.ToInt64(words[5]) < 0)
                                { exito = false; mensajerespuesta += "ME0134-" + "Contenido del Archivo, [#" + contadorlinea + "],  Cantidad de registros igual o menor a cero" + "|"; }
                            }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0137-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba numero de registros" + "|"; }
                            //validar importe total del documento
                            try
                            {
                                decimal.Parse(words[6]);
                                MasterTotal = Convert.ToDecimal(words[6]);
                                if (Convert.ToDecimal(words[6]) == 0 || Convert.ToDecimal(words[6]) < 0)
                                { exito = false; mensajerespuesta += "ME0138-" + "Contenido del Archivo, [#" + contadorlinea + "],  Importe igual a cero" + "|"; }
                            }
                            catch (Exception ex)
                            { exito = false; mensajerespuesta += "ME0139-" + "Contenido del Archivo, [#" + contadorlinea + "],  Se esperaba el importe total del documento" + "|"; }
                        }
                        else
                        {
                            //desglose del detalle del archivo
                            string phrase = line;
                            string[] wordsd = phrase.Split(',');
                            varRespuestaCRIFD = new ResponseLocal();
                            varRespuestaCRIFD = EqualsImpfichxDenomination(wordsd);

                            if (varRespuestaCRIFD.EXITOSO == false)
                            {
                                exito = false;
                                mensajerespuesta += varRespuestaCRIFD.MESSAGERETURN;
                            }
                            else
                            { MasterAditivo += Convert.ToDecimal(wordsd[8]); }
                            contadorlinea++;
                        }
                    }
                    file.Close();
                    File.Delete(fileFullPath);

                    if (MasterTotal != MasterAditivo)
                    { exito = false; mensajerespuesta += "ME015-" + "Contenido del Archivo,  El Importe Total del Encabezado no coincide con la suma de los importes de las fichas de los depósitos, 18" + "|"; }
                }
                else
                { exito = false; mensajerespuesta += "ME013-" + "Contenido del Archivo,  Archivo en blanco, 14" + "|"; }

                Correcto.EXITOSO = exito;
                Correcto.MESSAGERETURN = mensajerespuesta;
                return Correcto;
            }
            catch (Exception ex)
            {
                Correcto.EXITOSO = false;
                Correcto.MESSAGERETURN = "Erro Inesperado:" + ex.Message + "|" + mensajerespuesta;
                return Correcto;
            }
        }

        //Validar archivo transacción generado
        private SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject ConvertRemoteRecoleccionToLocalRecoleccionObject(eArchivo RemoteeArchivo)
        {
            //Declaracion de variables de entidad
            SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject _localRecoleccionInfo = new SISSA.Cashflow.Web.Core.Entities.RecoleccionEntityObject();
            SISSA.Cashflow.Web.Core.Entities.RecoleccionDetailEntityObject _localDetail = new SISSA.Cashflow.Web.Core.Entities.RecoleccionDetailEntityObject();
            SISSA.Cashflow.Web.Core.Entities.RecoleccionResponseEntityObject _localResponse = new SISSA.Cashflow.Web.Core.Entities.RecoleccionResponseEntityObject();
            //variables de respuesta
            ResponseLocal varRespuestaCRIFD;
            ResponseLocal varRespuestaCRQC;
            try
            {
                //Variables para guardado de archivo de texto
                String NameFile = string.Empty, ExtentionFile = String.Empty, NameFileC = string.Empty;
                System.IO.StreamReader file = null;
                String line = String.Empty, CRC = String.Empty, fileFullPath = String.Empty, fileFullPathBack = String.Empty;
                String[] NameDiscompose;
                int inconsistinfo = 0;
                DateTime fecha = DateTime.Now;
                String fechaString = fecha.ToString("ddMMyyyy_HHmmss");
                String fechaDiscompose;
                DateTime fechaUp;
                Decimal MasterAditivo = 0, MasterTotal = 0;
                string[] param, paramb;
                bool encabezado = false;

                //Validando nombre del archivo
                NameFileC = RemoteeArchivo.NombreArchivoDeposito;
                NameDiscompose = NameFileC.Split('.');

                if (NameDiscompose.Length > 1)
                {
                    NameFile = NameDiscompose[0];
                    ExtentionFile = NameDiscompose[1];
                }
                else
                {
                    NameFile = NameDiscompose[0];
                    ExtentionFile = "txt";
                }

                //Se declara la ruta donde se guarda el archivo de texto plano y su backup
                param = new string[] { System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "archivos", NameFile + "." + ExtentionFile };
                paramb = new string[] { System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "archivos", "RecoleccionesBack", NameFile + "_" + fechaString + "." + ExtentionFile };
                fileFullPath = string.Format(@"{0}\{1}\{2}", param);
                fileFullPathBack = string.Format(@"{0}\{1}\{2}\{3}", paramb);

                //Se elimina el archivo en caso de existencia en la carpeta de archivos
                //try
                //{
                //    File.Delete(fileFullPath);
                //}
                //catch (Exception ex)
                //{ }


                //Validar que la variable eArchivo no este vacio
                if (RemoteeArchivo != null)
                {
                    //Se guardan los archivos en directorio
                    using (StreamWriter writer = new StreamWriter(fileFullPath))
                    {
                        writer.Write(RemoteeArchivo.ContenidoDeposito);
                    }

                    //Realizar respaldo de archivo
                    File.Copy(fileFullPath, fileFullPathBack);

                    //Lectura del archivo txt guardado
                    file = new System.IO.StreamReader(fileFullPath);
                    while ((line = file.ReadLine()) != null)
                    {
                        //leer la linea de encabezado el archivo
                        if (line.Contains("R") && encabezado == false)
                        {
                            encabezado = true;
                            string phrase = line;
                            string[] words = phrase.Split(',');
                            string dia = string.Empty;
                            string mes = string.Empty;
                            string anio = string.Empty;
                            CRC = words[1];
                            fechaDiscompose = words[3];
                            for (int i = 0; i < fechaDiscompose.Length; i++)
                            {
                                if (i <= 3)
                                {
                                    anio += fechaDiscompose[i];
                                }
                                if (i > 3 && i <= 5)
                                {
                                    mes += fechaDiscompose[i];
                                }
                                if (i > 5 && i <= 7)
                                { dia += fechaDiscompose[i]; }
                            }
                            fechaUp = Convert.ToDateTime(dia + "/" + mes + "/" + anio + " 00:00:00");
                            _localRecoleccionInfo.Folio = 1;
                            _localRecoleccionInfo.Fecha = fecha;
                            _localRecoleccionInfo.Fecha_Transmision = fecha;
                            _localRecoleccionInfo.Fecha_Acreditacion = fechaUp; //fecha contenida en el archivo
                            _localRecoleccionInfo.Importe = Convert.ToDecimal(words[6]);
                            _localRecoleccionInfo.Archivo = NameFile + "." + ExtentionFile;
                            MasterTotal = Convert.ToDecimal(words[6]);
                        }
                        else
                        {
                            //desglose del detalle del archivo
                            string phrase = line;
                            string[] wordsd = phrase.Split(',');
                            varRespuestaCRIFD = new ResponseLocal();
                            varRespuestaCRIFD = EqualsImpfichxDenomination(wordsd);

                            if (varRespuestaCRIFD.EXITOSO)
                            {
                                varRespuestaCRQC = QueryClient(wordsd[2]);

                                if (varRespuestaCRQC.EXITOSO)
                                {
                                    string result = varRespuestaCRQC.VALUERETURN;
                                    string[] wsintegracion = result.Split('-');

                                    _localDetail = new SISSA.Cashflow.Web.Core.Entities.RecoleccionDetailEntityObject();
                                    _localDetail.Id_Recoleccion = 0;
                                    if (wsintegracion.Length > 1)
                                    {
                                        wsintegracion[0] = Regex.Replace(wsintegracion[0], @" ", "");
                                        wsintegracion[1] = Regex.Replace(wsintegracion[1], @" ", "");
                                        _localDetail.Clave_Cliente = wsintegracion[0];
                                        _localDetail.Clave_Sucursal = wsintegracion[0] + "-" + wsintegracion[1];
                                        _localDetail.Clave_Banco = "N" + wsintegracion[0] + wsintegracion[1];
                                    }
                                    else
                                    {
                                        wsintegracion[0] = Regex.Replace(wsintegracion[0], @" ", "");
                                        _localDetail.Clave_Cliente = wsintegracion[0];
                                        _localDetail.Clave_Sucursal = wsintegracion[0];
                                        _localDetail.Clave_Banco = "N" + wsintegracion[0];
                                    }
                                    _localDetail.CRC = CRC;
                                    _localDetail.Cuenta = wordsd[1];
                                    if (wordsd[4] == "01")
                                    { _localDetail.Divisa = "MXN"; }
                                    else
                                    { _localDetail.Divisa = wordsd[4]; }
                                    _localDetail.Tipo = "R";
                                    _localDetail.Monto = Convert.ToDecimal(wordsd[8]);
                                    _localDetail.Remision = wordsd[2];
                                    _localRecoleccionInfo.Depositos.Add(_localDetail);
                                    MasterAditivo += Convert.ToDecimal(wordsd[8]);
                                }
                                else
                                {
                                    inconsistinfo++;
                                    _localResponse.MessageReturn = varRespuestaCRQC.MESSAGERETURN;
                                    _localRecoleccionInfo.Mensages.Add(_localResponse);
                                }
                            }
                            else
                            {
                                inconsistinfo++;
                                _localResponse.MessageReturn = varRespuestaCRIFD.MESSAGERETURN;
                                _localRecoleccionInfo.Mensages.Add(_localResponse);
                            }
                        }
                    }
                    file.Close();
                    File.Delete(fileFullPath);
                }
                if (inconsistinfo == 0 && MasterTotal == MasterAditivo)
                { return _localRecoleccionInfo; }
                else
                { _localRecoleccionInfo.Folio = 0; return _localRecoleccionInfo; }
            }
            catch (Exception ex)
            {
                _localRecoleccionInfo.Folio = 0;
                _localResponse.MessageReturn = ex.Message;
                _localRecoleccionInfo.Mensages.Add(_localResponse);
                return _localRecoleccionInfo;
            }
        }


        private ResponseLocal EqualsImpfichxDenomination(string[] detail)
        {
            //Variables de operacion
            SISSA.Cashflow.Web.Core.Business.RecoleccionesBusinessObject _RecoleccionesBAL;
            string conn = ConfigurationManager.ConnectionStrings["ConexionCajerosEmpresariales"].ConnectionString;
            _RecoleccionesBAL = new SISSA.Cashflow.Web.Core.Business.RecoleccionesBusinessObject(conn);
            //variables operativas para retorno
            bool igual = true, correctDivisa = false;
            string mensageretorno = string.Empty, valorretorno = string.Empty;
            int d = 0;
            double importeTotal = 0, aditivo = 0;
            //variable de retorno
            ResponseLocal varreturnIFXD = new ResponseLocal();
            //Asignacion de valores
            d = detail.Length;
            d--;
            importeTotal = Convert.ToDouble(detail[8]);

            //validacion de la divisa directamente en la BD
            correctDivisa = _RecoleccionesBAL.CheckDivisafromCuenta(detail[1], Convert.ToInt32(detail[4]));
            if (correctDivisa)
            {
                //Realiza la validacion de las denominaciones en importe ficha y sus detalles sean igual al importe ficha
                for (int i = 0; i < d; i++)
                {
                    if (detail[i] == "B1000")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 1000 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }

                    }
                    if (detail[i] == "B500")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 500 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }

                    }
                    if (detail[i] == "B200")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 200 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }

                    }
                    if (detail[i] == "B100")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 100 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }

                    }
                    if (detail[i] == "B50")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 50 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break;
                        }
                        else
                        { aditivo += total; }

                    }
                    if (detail[i] == "B20")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 20 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M20")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 20 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M10")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 10 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M5")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 5 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M2")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 2 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M1")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = 1 * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M0.5")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = (0.50) * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M0.2")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = (0.20) * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M0.1")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = (0.10) * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                    if (detail[i] == "M0.05")
                    {
                        int positionCant, positionTotal;
                        positionCant = i + 1;
                        positionTotal = i + 2;
                        i = positionTotal;
                        double cantidad, total, result;
                        cantidad = Convert.ToDouble(detail[positionCant]);
                        total = Convert.ToDouble(detail[positionTotal]);
                        result = (0.05) * cantidad;
                        if (result != total)
                        {
                            mensageretorno += "ME035-" + "Importes, [#" + detail[0] + "], La cantidad de alguna denominación no coincide con el importe, 10" + "|";
                            igual = false; 
                            break; 
                        }
                        else
                        { aditivo += total; }
                    }
                }
            }
            else
            {
                //Mensaje de error, si la divisa del cliente no coincide con la del archivo
                mensageretorno += "ME029-" + "Divisa, [#"+detail[0]+"], Divisa Inválida, 16"+"|";
            }

            if (aditivo == importeTotal && aditivo != 0 && importeTotal != 0)
            {
                varreturnIFXD.EXITOSO = igual;
                varreturnIFXD.VALUERETURN = valorretorno;
                varreturnIFXD.MESSAGERETURN = mensageretorno;
                return varreturnIFXD; 
            }
            else
            {
                mensageretorno += "ME039-" + "Importes, [#" + detail[0] + "], La suma de los importes de las denominaciones no coincide con el importe en efectivo del depósito, 17" + "|";
                igual = false;
                varreturnIFXD.EXITOSO = igual;
                varreturnIFXD.VALUERETURN = valorretorno;
                varreturnIFXD.MESSAGERETURN = mensageretorno;
                return varreturnIFXD;
            }
        }

        //Solicita clave cliente desde un WS
        private ResponseLocal QueryClient(string remesa)
        {
            ResponseLocal RespuestaCliente = new ResponseLocal();
            string cliente = string.Empty;
            try
            {
                //Poner parametros de configuracion en AppSettings                 
                string _ClaveSucursal = ConfigurationManager.AppSettings["Sissa_terminal"];
                string _ClaveUsuario = ConfigurationManager.AppSettings["Sissa_user"];
                string _Contrasena= ConfigurationManager.AppSettings["Sissa_pass"];

                string _Remision = remesa;

                Remision remision = new Remision { ClaveSucursal = _ClaveSucursal, ClaveUsuario = _ClaveUsuario, Contrasena = _Contrasena, NumeroRemision = _Remision };
                WsCajerosEmpresariales.CajerosEmpresarialesClient cajerosEmpresariales = new WsCajerosEmpresariales.CajerosEmpresarialesClient();

                var datos = JsonConvert.SerializeObject(remision, Formatting.Indented);
                AlgoritmoCriptografico algoritmo = new AlgoritmoCriptografico();

                var datosEncriptados = algoritmo.Codificar(datos);
                var res = cajerosEmpresariales.GetClaveCliente(datosEncriptados);
                var respuestaDecodificada = algoritmo.Decodificar(res);

                Remision respuesta = JsonConvert.DeserializeObject<Remision>(respuestaDecodificada);

                if (respuesta.OK)
                {
                    RespuestaCliente.EXITOSO = true;
                    RespuestaCliente.VALUERETURN = respuesta.Respuesta;
                    return RespuestaCliente;
                }
                else
                {
                    RespuestaCliente.EXITOSO = false;
                    RespuestaCliente.MESSAGERETURN = respuesta.Respuesta;
                    return RespuestaCliente;
                }
            }
            catch (Exception ex1)
            {
                RespuestaCliente.EXITOSO = false;
                RespuestaCliente.MESSAGERETURN = ex1.Message;
                return RespuestaCliente;
            }
        }

        private SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject GetSampleDataDeposito()
        {
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _transaccionInfo;
            SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject _transaccionDetailInfo;

            //Se crea objeto de transaccion
            _transaccionInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject() {
                IdTransaccion = 5000
                , ClaveSucursal = "10"
                , Fecha = DateTime.Now
                , HoraInicio = TimeSpan.Zero
                ,HoraFin = TimeSpan.Zero
                ,ImporteTotal = 1000
                ,Status = 'F'
                ,UsuarioRegistro = "9999"
                ,TotalMXN = 700
                ,TotalUSD = 0
                ,TotalUSDConvert=0
                ,TipoCambio = 1
                ,Folio = "500"
                ,Tipo=2
                ,IdCorte=87
                ,EsEfectivo=true
                ,IdCaja=196
                ,ClaveCaja="001"
                ,NumeroCuenta= "51356632"
                ,Referencia= "63572212442"
                ,Divisa="MXN"
                ,Acreditado=false
            };

            _transaccionInfo.Movimientos = new List<SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject>();

            //Se crea objetos de detalle de transaccion
            _transaccionDetailInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject()
            {
                IdTransaccion = _transaccionInfo.IdTransaccion                    
                ,ClaveSucursal = _transaccionInfo.ClaveSucursal
                ,SerieCasete = "45890501205"
                ,ClaveDenominacion= "MXP500"
                ,Denominacion=500
                ,CantidadPiezas=1
                ,Importe=500
                ,ClaveMoneda="MXN"
                ,SerieValidador= "51791601891"
                ,NumeroValidador=1
            };

            _transaccionInfo.Movimientos.Add(_transaccionDetailInfo);

            //Se crea objetos de detalle de transaccion
            _transaccionDetailInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject()
            {
                IdTransaccion = _transaccionInfo.IdTransaccion                    
                ,ClaveSucursal = _transaccionInfo.ClaveSucursal
                ,SerieCasete = "45890501205"
                ,ClaveDenominacion= "MXP200"
                ,Denominacion=200
                ,CantidadPiezas=1
                ,Importe=200
                ,ClaveMoneda="MXN"
                ,SerieValidador= "51791601891"
                ,NumeroValidador=1
            };

            _transaccionInfo.Movimientos.Add(_transaccionDetailInfo);

            return _transaccionInfo;
        }

        private SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject GetSampleDataRetiro()
        {
            SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject _transaccionInfo;
            SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject _transaccionDetailInfo;
                        
            //Se crea objeto de transaccion
            _transaccionInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionEntityObject() {
                IdTransaccion = 7000
                , ClaveSucursal = "10"
                , Fecha = DateTime.Now
                , HoraInicio = TimeSpan.Zero
                ,HoraFin = TimeSpan.Zero
                ,ImporteTotal = 10000
                ,Status = 'V'
                ,UsuarioRegistro = "9999"
                ,TotalMXN = 10000
                ,TotalUSD = 0
                ,TotalUSDConvert=0
                ,TipoCambio = 1
                ,Folio = "700"
                ,Tipo=2
                ,IdCorte=87
                ,EsEfectivo=true
                ,IdCaja=196
                ,ClaveCaja="001"
                ,NumeroCuenta= "51356632"
                ,Referencia= "63572212442"
                ,Divisa="MXN"
                ,Acreditado=false
                ,Observaciones = "OBSERVACIONES"
                ,NumeroRemision = 123456
                ,NumeroEnvase="11111"
                ,ImporteOtros=0
                ,ImporteOtrosD=0                
            };

            //Se crea objetos de detalle de transaccion
            _transaccionDetailInfo = new SISSA.Cashflow.Web.Core.Entities.TransaccionDetailEntityObject()
            {
                IdTransaccion = _transaccionInfo.IdTransaccion                    
                ,ClaveSucursal = _transaccionInfo.ClaveSucursal
                ,SerieCasete = "45890501205"
                ,ClaveDenominacion= "MXP500"
                ,Denominacion=500
                ,CantidadPiezas=20
                ,Importe=10000
                ,ClaveMoneda="MXN"
                ,SerieValidador= "51791601891"
                ,NumeroValidador=1
            };

            _transaccionInfo.Movimientos.Add(_transaccionDetailInfo);

            return _transaccionInfo;
        }

        #endregion
    }
}
