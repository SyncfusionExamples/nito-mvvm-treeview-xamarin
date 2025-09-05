# nito-mvvm-treeview-xamarin

This repository demonstrates how to integrate and use the Nito.MVVM library with the Xamarin.Forms SfTreeView control. It provides a sample implementation that leverages Nito.MVVM's advanced MVVM features to manage hierarchical data, command binding, and property change notifications within the TreeView for a robust and maintainable application architecture.

## Sample

### XAML
```xaml
<ContentPage.BindingContext>
    <local:MusicInfoRepository x:Name="viewModel"/>
</ContentPage.BindingContext>
<ContentPage.Content>
    <StackLayout>
        <sfTreeView:SfTreeView x:Name="treeView" Indentation="15" ExpanderWidth="40" LoadOnDemandCommand="{Binding TreeViewOnDemandCommand}" ItemsSource="{Binding Menu}">
        </sfTreeView:SfTreeView>
    </StackLayout>
</ContentPage.Content>
```

### ViewModel
```csharp
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

    private bool CanExecuteOnDemandLoading(object sender)
    {
        var hasChildNodes = ((sender as TreeViewNode).Content as MusicInfo).HasChildNodes;
        if (hasChildNodes)
            return true;
        else
            return false;
    }

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
}
```

## Requirements to run the demo
Visual Studio 2017 or Visual Studio for Mac.
Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting
### Path too long exception
If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.
