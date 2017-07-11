using ComputerSystems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP.Entity
{
    public class ECMP_AccountsByToPay : CmpNotifyPropertyChanged
    {
        public ECMP_AccountsByToPay()
        {
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now;
        }

        private ECMP_Provider _Provider;

        public ECMP_Provider Provider
        {
            get { return _Provider; }
            set
            {
                _Provider = value; OnPropertyChanged();
            }
        }

        private DateTime _DateStart;

        public DateTime DateStart
        {
            get { return _DateStart; }
            set { _DateStart = value; OnPropertyChanged(); }
        }

        private DateTime _DateEnd;

        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { _DateEnd = value; OnPropertyChanged(); }
        }

        private ECMP_BranchOffice _BranchOffice;

        public ECMP_BranchOffice BranchOffice
        {
            get { return _BranchOffice; }
            set
            {
                _BranchOffice = value; OnPropertyChanged();
                if (value != null)
                    AllProviders = true;
            }
        }

        private ObservableCollection<ECMP_DetailAccounts> _ListDetailAccounts;

        public ObservableCollection<ECMP_DetailAccounts> ListDetailAccounts
        {
            get
            {
                if (_ListDetailAccounts == null)
                    _ListDetailAccounts = new ObservableCollection<ECMP_DetailAccounts>();
                return _ListDetailAccounts;
            }
            set { _ListDetailAccounts = value; OnPropertyChanged(); }
        }

        private bool _AllProviders;

        public bool AllProviders
        {
            get { return _AllProviders; }
            set
            {
                _AllProviders = value; OnPropertyChanged();
                if (value == true)
                    Provider = new ECMP_Provider { Id = 0, Reason = "Todos los Proveedores" };
            }
        }
    }
}
