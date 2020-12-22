using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace TreeViewXamarin
{
    public class MusicInfo : INotifyPropertyChanged
    {
        #region Fields

        public string itemName;
        public int id;
        public bool hasChildNodes;

        #endregion

        #region Properties

        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged("ItemName");
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public bool HasChildNodes
        {
            get { return hasChildNodes; }
            set
            {
                hasChildNodes = value;
                OnPropertyChanged("HasChildNodes");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}