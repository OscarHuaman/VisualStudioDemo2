/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 17/02/2016
**********************************************************/
namespace CMP.ViewModels.NotaCreditoDebito.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using CMP.ViewModels.NotaCreditoDebito.Pages;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using MNF.Business;
    using MNF.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VCMP_ListarNotaCreditoDebito : CmpNavigationService, CmpINavigation
    {
        public VCMP_ListarNotaCreditoDebito()
        {
            SelectFechaInicio = DateTime.Now.AddDays(-7);
            SelectFechaFin = DateTime.Now;
          
            
        }

        public object Parameter
        {
            set
            {
                LoadHeader();
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                _MyFrame = value;
            }
        }

        #region COLECIONES
        private ObservableCollection<ECMP_NotaCreditoDebito> listaNotaCredito;
        public  ObservableCollection<ECMP_NotaCreditoDebito> ListaNotaCreditoDebito
        {
            get
            {
                if (listaNotaCredito == null)
                    listaNotaCredito = new ObservableCollection<ECMP_NotaCreditoDebito>();
                return listaNotaCredito;
            }

            set
            {
                listaNotaCredito = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ItemOpciones> _ListECMP_OpcionFiltrados;
        public  ObservableCollection<ItemOpciones> ListECMP_OpcionFiltrados
        {
            get
            {
                if (_ListECMP_OpcionFiltrados == null)
                    _ListECMP_OpcionFiltrados = new ObservableCollection<ItemOpciones>();
                return _ListECMP_OpcionFiltrados;
            }
            set
            {
                _ListECMP_OpcionFiltrados = value;
                OnPropertyChanged("ListECMP_OpcionFiltrados");
            }

        }
        private ObservableCollection<ECMP_NotaCreditoDebitoDetalle> _ListDocumento;
        public ObservableCollection<ECMP_NotaCreditoDebitoDetalle> ListDocumento
        {
            get
            {
                if (_ListDocumento == null)
                    _ListDocumento = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();
                return _ListDocumento;
            }
            set
            {
                _ListDocumento = value;
            }
        }
        #endregion

        #region OBJETO SECUNDARIO

        private ECMP_NotaCreditoDebito _SelectNotaCreditoDebito;
        public  ECMP_NotaCreditoDebito SelectNotaCreditoDebito
        {
            get
            {
                return _SelectNotaCreditoDebito;
            }
            set
            {
                _SelectNotaCreditoDebito = value;
                OnPropertyChanged();
            }
        }
        private DateTime _SelectFechaInicio;
        public  DateTime SelectFechaInicio
        {
            get
            {
                if (_SelectFechaInicio == null)
                    _SelectFechaInicio = DateTime.Now;
                return _SelectFechaInicio;
            }
            set
            {
                _SelectFechaInicio = value;
                OnPropertyChanged("SelectFechaInicio");
                GetData();
            }
        }
        private DateTime _SelectFechaFin;
        public  DateTime SelectFechaFin
        {
            get
            {
                if (_SelectFechaFin == null)
                    _SelectFechaFin = DateTime.Now;
                return _SelectFechaFin;
            }
            set
            {
                _SelectFechaFin = value;
                OnPropertyChanged("SelectFechaFin");
                GetData();
            }
        }
        private ItemOpciones _SelectOpcionFiltro;
        public  ItemOpciones SelectOpcionFiltro
        {
            get
            {
                if (_SelectOpcionFiltro == null)
                    _SelectOpcionFiltro = ListECMP_OpcionFiltrados.FirstOrDefault();
                return _SelectOpcionFiltro;
            }
            set
            {
                _SelectOpcionFiltro = value;
                if (value.Item == "Fecha")
                {
                    SelectFechaInicio = DateTime.Now.AddDays(-7);
                    SelectFechaFin = DateTime.Now;
                    IsVisibleFiltrarFecha = Visibility.Visible;
                    IsVisibleFiltrarDescripcion = Visibility.Collapsed;
                }
                else if (value.Item == "Proveedor")
                {
                    TitleBuscador = "Filtrar por Razón Social";
                    IsVisibleFiltrarFecha = Visibility.Collapsed;
                    IsVisibleFiltrarDescripcion = Visibility.Visible;
                    Filtro = string.Empty;
                }
                else if (value.Item == "Moneda")
                {
                    TitleBuscador = "Filtrar por Moneda";
                    IsVisibleFiltrarFecha = Visibility.Collapsed;
                    IsVisibleFiltrarDescripcion = Visibility.Visible;
                    Filtro = string.Empty;
                }
                else if (value.Item == "Documento")
                {
                    TitleBuscador = "Filtrar por Documento";
                    IsVisibleFiltrarFecha = Visibility.Collapsed;
                    IsVisibleFiltrarDescripcion = Visibility.Visible;
                    Filtro = string.Empty;
                }


                OnPropertyChanged("SelectOpcionFiltro");
            }
        }
        private string _TitleBuscador;
        public  string TitleBuscador
        {
            get
            {
                return _TitleBuscador;
            }
            set
            {
                _TitleBuscador = value;
                OnPropertyChanged("TitleBuscador");
            }
        }
        private Visibility _IsVisibleFiltrarFecha;
        public  Visibility IsVisibleFiltrarFecha
        {
            get
            {
                return _IsVisibleFiltrarFecha;
            }
            set
            {
                _IsVisibleFiltrarFecha = value;
                OnPropertyChanged("IsVisibleFiltrarFecha");
            }
        }
        private Visibility _IsVisibleFiltrarDescripcion;
        public  Visibility IsVisibleFiltrarDescripcion
        {
            get
            {
                return _IsVisibleFiltrarDescripcion;
            }
            set
            {
                _IsVisibleFiltrarDescripcion = value;
                OnPropertyChanged("IsVisibleFiltrarDescripcion");
            }
        }
        private string _Filtro;
        public  string Filtro
        {
            get
            {
                return _Filtro;
            }
            set
            {
                _Filtro = value;
                OnPropertyChanged("Filtro");
                GetData();
            }
        }
        private ECMP_NotaCreditoDebito _ObjECMP_NotaCreditoDebito;
        public  ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito
        {
            get
            {
                if (_ObjECMP_NotaCreditoDebito == null)
                    _ObjECMP_NotaCreditoDebito = new ECMP_NotaCreditoDebito();
                return _ObjECMP_NotaCreditoDebito;
            }
            set
            {
                _ObjECMP_NotaCreditoDebito = value;
                OnPropertyChanged();
            }

        }

        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Ir(new PCMP_NotaCreditoDebito(), _MyFrame, new ECMP_NotaCreditoDebito() {  Opcion = "I" });
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectNotaCreditoDebito == null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Debe seleccionar un registro para continuar con el proceso", CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        SelectNotaCreditoDebito.Opcion = "U";
                        Ir(new PCMP_NotaCreditoDebito(), _MyFrame, SelectNotaCreditoDebito);
                    }
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    _MyFrame.Close(TipoModulo.ManuFactura);
                });
            }
        }

        public ICommand IAnular
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectNotaCreditoDebito == null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Debe seleccionar un registro para continuar con el proceso", CmpButton.Aceptar);
                        return;
                    }
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "¿Desea Anular Nota Credito / Debito?", CmpButton.AceptarCancelar, () =>
                    {
                        try
                        {

                            SelectNotaCreditoDebito.Opcion = "N";
                            new BCMP_NotaCreditoDebito().TransNotaCreditoDebito(SelectNotaCreditoDebito);
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.DatoProcesados, CmpButton.Aceptar);
                            GetData();
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, ex.Message, CmpButton.Aceptar);
                        }
                    },
                    () =>
                    {
                        return;
                    });
                });
            }
        }

        public ICommand IVisualizar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                    {
                        string[] parametro;
                        
                        if (SelectNotaCreditoDebito == null || ListaNotaCreditoDebito.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Seleccione un registro para continuar con la operación", CmpButton.Aceptar);
                            return;
                        }
                        else
                        {
                            var ListECMP_NotaCreditoDetalle = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>(new BCMP_NotaCreditoDebitoDetalle().GetECMP_NotaCreditoDebitoDetalle(SelectNotaCreditoDebito));
                            var ListEMNF_DocumentoReferencia = new ObservableCollection<EMNF_DocumentoReferencia>(new BMNF_DocumentoReferencia().ListGetDocumentoReferencia(SelectNotaCreditoDebito.IdNotaCreDeb, "CCD"));
                            string strSerieNumero = string.Empty, strTempSerieNumero = string.Empty;
                            var varListECMP_NotaCreditoDetalle = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();
                            ListECMP_NotaCreditoDetalle.ToList().ForEach(x=>
                            {
                                varListECMP_NotaCreditoDetalle.Add(new ECMP_NotaCreditoDebitoDetalle()
                                    {
                                        ArticuloServicio = x.ObjECMP_CompraDetalle.ArticuloServicio,
                                        CodUndMedida = x.ObjECMP_CompraDetalle.CodUndMedida,
                                        Importe = x.ObjECMP_CompraDetalle.Importe,
                                        SerieNumero = x.ObjECMP_CompraDetalle.ObjECMP_Compra.SerieNumero,
                                        Cantidad = x.ObjECMP_CompraDetalle.Cantidad,
                                        PrcDscBonificacion = x.PrcDscBonificacion,
                                        ImpDscBonificacion = x.ImpDscBonificacion,
                                        PreDscBoniOmision = x.PreDscBoniOmision,
                                        PrecioOmitido = x.PrecioOmitido,
                                        PrecioUnitario=x.ObjECMP_CompraDetalle.PrecioUnitario,
                                        CantidaDevolver=x.CantidaDevolver

                                    }); 
                            });
                            ListEMNF_DocumentoReferencia.ToList().ForEach(x =>
                            {
                                strSerieNumero = x.SerieNumero + " - " + x.Fecha.ToShortDateString();
                                strTempSerieNumero += strSerieNumero + "\n";
                            });
                            var vrTotal=SelectNotaCreditoDebito.Gravada+SelectNotaCreditoDebito.ImporteIGV;
                            parametro=new string[]
                            {
                                "prmProveedorRZ|"       + SelectNotaCreditoDebito.ObjEMNF_ClienteProveedor.RazonSocial,
                                "prmDireccion|"         + SelectNotaCreditoDebito.ObjEMNF_ClienteProveedor.Direccion,
                                "prmRuc|"               + SelectNotaCreditoDebito.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                                "prmUsuario|"           + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                                "prmDireccionUsuario|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                                "prmRucUsuario|"        + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                                "prmDiaActual|"         + SelectNotaCreditoDebito.Fecha.ToLongDateString().Split(',').ElementAt(1),
                                "prmSerie|"             + SelectNotaCreditoDebito.Serie,
                                "prmNumero|"            + SelectNotaCreditoDebito.Numero,
                                "prmMoneda|"            + SelectNotaCreditoDebito.ObjESGC_Moneda.Descripcion,
                                "prmTipoCambio|"        + SelectNotaCreditoDebito.TipoCambio,
                                "prmSimbolo|"           + SelectNotaCreditoDebito.ObjESGC_Moneda.Simbolo,
                                "prmSerieNumero|"       + strTempSerieNumero,
                                "prmCodMotivo|"         + SelectNotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.CodMotivo,
                                "prmMotivo|"            + SelectNotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.Motivo,
                                "prmGlosa|"             + SelectNotaCreditoDebito.Glosa,
                                "prmSubTotal|"          + SelectNotaCreditoDebito.Gravada,
                                "prmIgv|"               + SelectNotaCreditoDebito.IGV,
                                "prmImporteIGV|"        + SelectNotaCreditoDebito.ImporteIGV,
                                "prmTotal|"             + (( ListECMP_NotaCreditoDetalle.FirstOrDefault().ObjECMP_CompraDetalle.ObjECMP_Compra.IncluyeIGV )? ListECMP_NotaCreditoDetalle.Sum(x=>x.Importe):vrTotal),
                                "prmTipo|"              + ((SelectNotaCreditoDebito.CodDocumento=="NCR")?"NOTA DE CRÉDITO":"NOTA DE DEBITO")
                            };
                            MainRerport ObjMainRerport = new MainRerport();
                            ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptNotaCredito.rdlc", "dtsNotaCreditoDebitoDetalle", varListECMP_NotaCreditoDetalle, parametro);
                            ObjMainRerport.ShowDialog();

                        }

                    });
            }
        }

        #endregion

        #region MÉTODOS

        private void LoadHeader()
        {
            CmpTask.Process(()=>
            {
                ListECMP_OpcionFiltrados = GetOpciones();
            },
            ()=>
            {
                SelectOpcionFiltro = ListECMP_OpcionFiltrados.FirstOrDefault();
            });
        }

        private void GetData()
        {
            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    if (SelectOpcionFiltro == null)
                        return;
                    ObjECMP_NotaCreditoDebito.FechaInicio = SelectFechaInicio;
                    ObjECMP_NotaCreditoDebito.FechaHasta = SelectFechaFin;
                    ObjECMP_NotaCreditoDebito.Filtro =( Filtro==null||Filtro==string.Empty)?"%":Filtro;
                    ListaNotaCreditoDebito = new BCMP_NotaCreditoDebito().GETNotaCreditoDebito(SelectOpcionFiltro.Item,ObjECMP_NotaCreditoDebito);
                }
                catch (Exception ex)
                {
                    strOutMessageError = ex.Message;
                }
            },
            () =>
            {
                if (strOutMessageError.Length > 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                }
            });
        }

        private ObservableCollection<ItemOpciones> GetOpciones()
        {
            ObservableCollection<ItemOpciones> ListECMP_OpcionFiltrado  = new ObservableCollection<ItemOpciones>();
            ListECMP_OpcionFiltrado.Add(new ItemOpciones() { Item = "Fecha", value = "Fecha" });
            ListECMP_OpcionFiltrado.Add(new ItemOpciones() { Item = "Proveedor", value = " Proveedor" });
            ListECMP_OpcionFiltrado.Add(new ItemOpciones() { Item = "Moneda", value = "Moneda" });
            ListECMP_OpcionFiltrado.Add(new ItemOpciones() { Item = "Documento", value = " Documento" });
            return ListECMP_OpcionFiltrado;
        }

        #endregion

        #region CLASE AUXILIAR

        public class ItemOpciones
        {
            public string Item { get; set; }
            public string value { get; set; }
        }
        #endregion

    }
}
