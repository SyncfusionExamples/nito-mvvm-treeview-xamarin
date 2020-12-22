﻿using Nito.Mvvm;
using Syncfusion.TreeView.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TreeViewXamarin
{
    /// <summary>
    /// ViewModel class that implements <see cref="Command"/> for load on demand. 
    /// </summary>
    public class MusicInfoRepository
    {
        private ObservableCollection<MusicInfo> menu;

        public ObservableCollection<MusicInfo> Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        public IAsyncCommand TreeViewOnDemandCommand { get; set; }

        public MusicInfoRepository()
        {
            this.Menu = this.GetMenuItems();
            TreeViewOnDemandCommand = new CustomAsyncCommand(ExecuteOnDemandLoading, CanExecuteOnDemandLoading);
        }

        /// <summary>
        /// CanExecute method is called before expanding and initialization of node. Returns whether the node has child nodes or not.
        /// Based on return value, expander visibility of the node is handled.  
        /// </summary>
        /// <param name="sender">TreeViewNode is passed as default parameter </param>
        /// <returns>Returns true, if the specified node has child items to load on demand and expander icon is displayed for that node, else returns false and icon is not displayed.</returns>
        private bool CanExecuteOnDemandLoading(object sender)
        {
            var hasChildNodes = ((sender as TreeViewNode).Content as MusicInfo).HasChildNodes;
            if (hasChildNodes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Execute method is called when any item is requested for load-on-demand items.
        /// </summary>
        /// <param name="obj">TreeViewNode is passed as default parameter </param>
        private async Task ExecuteOnDemandLoading(object obj)
        {
            var notifyTask = NotifyTask.Create(PopulateChildAsync(obj));
            await notifyTask.TaskCompleted;
            if(notifyTask.IsCompleted)
            {
                var items = notifyTask.Result as IEnumerable<MusicInfo>;
                if (items.Count() > 0)
                    //Expand the node after child items are added.
                    (obj as TreeViewNode).IsExpanded = true;
            }
        }

        private async Task<IEnumerable<MusicInfo>> PopulateChildAsync(object obj)
        {
            var node = obj as TreeViewNode;

            // Skip the repeated population of child items when every time the node expands.
            if (node.ChildNodes.Count > 0)
            {
                node.IsExpanded = true;
                return null;
            }

            //Animation starts for expander to show progressing of load on demand
            node.ShowExpanderAnimation = true;
            MusicInfo musicInfo = node.Content as MusicInfo;
            await Task.Delay(2000);

            //Fetching child items to add
            var items = GetSubMenu(musicInfo.ID);

            // Populating child items for the node in on-demand
            node.PopulateChildNodes(items);

            //Stop the animation after load on demand is executed, if animation not stopped, it remains still after execution of load on demand.
            node.ShowExpanderAnimation = false;
            return items;
        }

        private ObservableCollection<MusicInfo> GetMenuItems()
        {
            ObservableCollection<MusicInfo> menuItems = new ObservableCollection<MusicInfo>();
            menuItems.Add(new MusicInfo() { ItemName = "Discover Music", HasChildNodes = true, ID = 1 });
            menuItems.Add(new MusicInfo() { ItemName = "Sales and Events", HasChildNodes = true, ID = 2 });
            menuItems.Add(new MusicInfo() { ItemName = "Categories", HasChildNodes = true, ID = 3 });
            menuItems.Add(new MusicInfo() { ItemName = "MP3 Albums", HasChildNodes = true, ID = 4 });
            menuItems.Add(new MusicInfo() { ItemName = "Fiction Book Lists", HasChildNodes = true, ID = 5 });
            return menuItems;
        }

        public IEnumerable<MusicInfo> GetSubMenu(int iD)
        {
            ObservableCollection<MusicInfo> menuItems = new ObservableCollection<MusicInfo>();
            if (iD == 1)
            {
                menuItems.Add(new MusicInfo() { ItemName = "Hot Singles", HasChildNodes = false, ID = 11 });
                menuItems.Add(new MusicInfo() { ItemName = "Rising Artists", HasChildNodes = false, ID = 12 });
                menuItems.Add(new MusicInfo() { ItemName = "Live Music", HasChildNodes = false, ID = 13 });
                menuItems.Add(new MusicInfo() { ItemName = "More in Music", HasChildNodes = true, ID = 14 });
            }
            else if (iD == 2)
            {
                menuItems.Add(new MusicInfo() { ItemName = "100 Albums - $10 Each", HasChildNodes = false, ID = 21 });
                menuItems.Add(new MusicInfo() { ItemName = "Hip-Hop and R&B Sale", HasChildNodes = false, ID = 22 });
                menuItems.Add(new MusicInfo() { ItemName = "CD Deals", HasChildNodes = false, ID = 23 });
            }
            else if (iD == 3)
            {
                menuItems.Add(new MusicInfo() { ItemName = "Songs", HasChildNodes = false, ID = 31 });
                menuItems.Add(new MusicInfo() { ItemName = "Bestselling Albums", HasChildNodes = false, ID = 32 });
                menuItems.Add(new MusicInfo() { ItemName = "New Releases", HasChildNodes = false, ID = 33 });
                menuItems.Add(new MusicInfo() { ItemName = "MP3 Albums", HasChildNodes = false, ID = 34 });

            }
            else if (iD == 4)
            {
                menuItems.Add(new MusicInfo() { ItemName = "Rock Music", HasChildNodes = false, ID = 41 });
                menuItems.Add(new MusicInfo() { ItemName = "Gospel", HasChildNodes = false, ID = 42 });
                menuItems.Add(new MusicInfo() { ItemName = "Latin Music", HasChildNodes = false, ID = 43 });
                menuItems.Add(new MusicInfo() { ItemName = "Jazz", HasChildNodes = false, ID = 44 });
            }
            else if (iD == 5)
            {
                menuItems.Add(new MusicInfo() { ItemName = "Hunger Games", HasChildNodes = false, ID = 51 });
                menuItems.Add(new MusicInfo() { ItemName = "Pride and Prejudice", HasChildNodes = false, ID = 52 });
                menuItems.Add(new MusicInfo() { ItemName = "Harry Potter", HasChildNodes = false, ID = 53 });
                menuItems.Add(new MusicInfo() { ItemName = "Game Of Thrones", HasChildNodes = false, ID = 54 });
            }
            else if (iD == 14)
            {
                menuItems.Add(new MusicInfo() { ItemName = "Music Trade-In", HasChildNodes = false, ID = 141 });
                menuItems.Add(new MusicInfo() { ItemName = "Redeem a Gift card", HasChildNodes = false, ID = 142 });
                menuItems.Add(new MusicInfo() { ItemName = "Band T-Shirts", HasChildNodes = false, ID = 143 });
            }
            return menuItems;
        }
    }
}