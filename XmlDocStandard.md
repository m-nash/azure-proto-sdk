## SDK XML documentation standard text:
### Note/Questions:
1. Async void return type such as `Task` does not need to be documented
2. One class per file unless same named generic and non-generic



### Commonly used

| Name       | Text     |
| :------------- | :----------- |
| Resource Group Name | `The name of Resource Group.` |
| `Subscription Id` | `/// <param name="resource">The id of the Azure subscription.</param>` |
| `Resource resource` | `/// <param name="resource">The resource that is the target of operations.</param>` |
| `ResourceIdentifier id` | `/// <param name="id">The identifier of the resource that is the target of operations.</param>`|
| `AzureResourceManagerClientOptions options` | `/// <param name="options">The client parameters to use in these operations.</param>` |
| `CancellationToken cancellationToken = default` | ``` /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.```<br>```/// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>``` |
| `ArmResponse<TOperations>` | ??_____ |

### TypeParam

```
    /// <typeparam name="T">The type of __________  </typeparam>	

Example:

    /// <typeparam name="T">The type of the underlying model this class wraps</typeparam>  

    /// <typeparam name="TOperations">The type of the operatiosn class for a specific resource.</typeparam>
```

### Constructor
```
    /// Initializes a new instance of the <see cref="______"/> class.	
```

### Inherited/Override Methods
```
    ///<inheritdoc/>
```

### Properties

| Name       | Text                      |
| - | - |
|Get & Set | `/// Gets or sets the ________` |
|Get Only | `/// Gets the ________`|
|Set Only | `/// Sets the ________`|
|Special case:<br>Boolean  | `/// Gets or sets a value indicating whether _______`|

### Sync and Async

There is no need to call out sync's blocking nature. ~~This call ...~~ strike thru do not show up in code block. Consider text below between ~~ to be removed.

        /// <summary>
        /// Get details for this resource from the service. ~~This call will block until a response is returned from the service~~
        /// </summary>
        /// <returns>A response with the operations for this resource</returns>
        public abstract ArmResponse<TOperations> Get();

For Async, 

        /// <returns>A <see cref="Task"/> that on completion returns the <see cref="TOperations"/> operation container for this resource. </returns>
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
 