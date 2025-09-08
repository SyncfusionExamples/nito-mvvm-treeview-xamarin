# nito-mvvm-treeview-xamarin

This repository demonstrates how to integrate and use the Nito.MVVM library with the Xamarin.Forms SfTreeView control. It provides a sample implementation that leverages Nito.MVVM's advanced MVVM features to manage hierarchical data, command binding, and property change notifications within the TreeView for a robust and maintainable application architecture.

## Sample

### ViewModel
```csharp
using Nito.Mvvm;

public class MusicInfoRepository
{

    public IAsyncCommand TreeViewOnDemandCommand { get; set; }

    public MusicInfoRepository()
    {
        this.Menu = this.GetMenuItems();
        TreeViewOnDemandCommand = new CustomAsyncCommand(ExecuteOnDemandLoading, CanExecuteOnDemandLoading);
    }

    private async Task ExecuteOnDemandLoading(object obj)
    {
        var notifyTask = NotifyTask.Create(PopulateChildAsync(obj));
        await notifyTask.TaskCompleted;
        if(notifyTask.IsCompleted)
        {
            var items = notifyTask.Result as IEnumerable<MusicInfo>;
            //...
        }
    }
}
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for Xamarin](https://help.syncfusion.com/xamarin/system-requirements)

## Troubleshooting
### Path too long exception
If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.
