/*********************************************************
'* ENTIDAD PARA REPORTE STOCK MINIMO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 19/02/2016
**********************************************************/
namespace CMP.Entity
{
    using ALM.Entity;
    using ComputerSystems;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;

    [Serializable]
    public class ECMP_ReporteStockMinimo : CmpNotifyPropertyChanged
    {
        private EALM_Almacen _ObjEALM_Almacen;
        private EMNF_ArticuloClase _ObjEMNF_ArticuloClase;
        private EMNF_ArticuloMarca _ObjEMNF_ArticuloMarca;
        private EMNF_Articulo _ObjEMNF_Articulo;
        private ESGC_UsuarioEmpresaSucursal _ObjESGC_UsuarioEmpresaSucursal;

        public string SubCategoria { get; set; }
        public int IdEmpresa { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockActual { get; set; }
        public string Sucursal { get; set; }

        public EMNF_Articulo ObjEMNF_Articulo
        {
            get
            {
                if (_ObjEMNF_Articulo == null)
                {
                    _ObjEMNF_Articulo = new EMNF_Articulo();
                }
                return _ObjEMNF_Articulo;
            }
            set
            {
                _ObjEMNF_Articulo = value;
                OnPropertyChanged();
            }
        }
        public EALM_Almacen ObjEALM_Almacen
        {
            get
            {
                if (_ObjEALM_Almacen == null)
                {
                    _ObjEALM_Almacen = new EALM_Almacen();
                }
                return _ObjEALM_Almacen;
            }
            set
            {
                _ObjEALM_Almacen = value;
                OnPropertyChanged();
            }
        }
        public EMNF_ArticuloMarca ObjEMNF_ArticuloMarca
        {
            get
            {
                if (_ObjEMNF_ArticuloMarca == null)
                {
                    _ObjEMNF_ArticuloMarca = new EMNF_ArticuloMarca();
                }
                return _ObjEMNF_ArticuloMarca;
            }
            set
            {
                _ObjEMNF_ArticuloMarca = value;
                OnPropertyChanged();
            }
        }
        public EMNF_ArticuloClase ObjEMNF_ArticuloClase
        {
            get
            {
                if (_ObjEMNF_ArticuloClase == null)
                {
                    _ObjEMNF_ArticuloClase = new EMNF_ArticuloClase();
                }
                return _ObjEMNF_ArticuloClase;
            }
            set
            {
                _ObjEMNF_ArticuloClase = value;
                OnPropertyChanged();
            }
        }
        public ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal
        {
            get
            {
                if (_ObjESGC_UsuarioEmpresaSucursal == null)
                {
                    _ObjESGC_UsuarioEmpresaSucursal = new ESGC_UsuarioEmpresaSucursal();
                }
                return _ObjESGC_UsuarioEmpresaSucursal;
            }
            set
            {
                _ObjESGC_UsuarioEmpresaSucursal = value;
                OnPropertyChanged();
            }
        }
    }
}
