## SDK XML documentation standard text:
### Note/Questions:
1. Async void return type such as `Task` does not need to be documented
2. One class per file unless same named generic and non-generic



### Commonly used

| Name       | Text     |
| :------------- | :----------- |
| Resource Group Name | `The name of Resource Group.` |
| `Subscription Id` | `/// <param name="resource"> The id of the Azure subscription. </param>` |
| `top` | `/// <param name="top"> The number of results to return. </param>` |
| `Resource resource` | `/// <param name="resource"> The resource that is the target of operations. </param>` |
| `ResourceIdentifier id` | `/// <param name="id"> The identifier of the resource that is the target of operations. </param>`|
| `ResourceIdentifier parentId` | `/// <param name="parentId"> The resource Id of the parent resource. </param>` |
| `AzureResourceManagerClientOptions options` | `/// <param name="options"> The client parameters to use in these operations. </param>` |
| `CancellationToken cancellationToken = default` | ``` /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.```<br>```/// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>``` |
| `ArmResponse<TOperations>` | ??_____ |
| `ArmResourceOperations` | /// <param name="genericOperations"> An instance of <see cref="ArmResourceOperations"/> that has an id for a virtual machine. </param> |

### Standard class documentation

```
xxxContainer.cs
    /// <summary>
    /// A class representing collection of XXX and their operations over a ___ResourceGroup|Subscription??__
    /// </summary>

xxx.cs
    /// <summary>
    /// A class representing a XXX along with the instance operations that can be performed on it.
    /// </summary>

xxxOperations.cs
    /// <summary>
    /// A class representing the operations that can be performed over a specific XXX.
    /// </summary>

xxxData.cs
    /// <summary>
    /// A class representing the xxx data model.
    /// </summary>
```

### TypeParam

```
    /// <typeparam name="T"> The type of __________. </typeparam>	

Example:

    /// <typeparam name="T"> The type of the underlying model this class wraps. </typeparam>  

    /// <typeparam name="TOperations"> The type of the operations class for a specific resource. </typeparam>

    /// <typeparam name="TResource"> The type of the class containing properties for the underlying resource. </typeparam>
```

### Constructor
```
    /// Initializes a new instance of the <see cref="______"/> class.	
```

### Inherited/Override Methods
```
    /// <inheritdoc/>
```

### Properties

| Name       | Text                      |
| - | - |
|Get & Set | `/// Gets or sets the ________.` |
|Get Only | `/// Gets the ________.`|
|Set Only | `/// Sets the ________.`|
|Special case:<br>Boolean  | `/// Gets or sets a value indicating whether _______.`|

### \<return\>
```
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static Pageable<TOperations> ...

        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public static AsyncPageable<TOperations> ListAtContextAsync ...

        /// <returns>An <see cref="ArmOperation{TOperations}"/> that allows polling for completion
        /// of the Create operation.</returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract ArmOperation<TOperations> StartCreate( ...

        /// <returns>
        /// A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{TOperations}"/>
        ///  that allows polling for completion of the Create operation.
        /// </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync( ...
```

### \<exception>

```
    /// <exception cref="ArgumentNullException"> <paramref name="id"/> is null. </exception>
    /// <exception cref="ArgumentException"> <paramref name="id"/> is not valid to list at context. </exception>
```

### sync, async and LRO

* For sync and async of same operations, the description should be the same. There is no need to call out sync's blocking nature.
* The async return should starts with `A <see cref="Task"/> that on completion returns ` and followed by text for sync version.

        /// <returns> A response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract ArmResponse<TOperations> Get(CancellationToken cancellationToken = default);

        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);

* For methods returning LRO (typically `StartXXX`), add following in remarks section.

```
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
```
### ResourceType

```
        /// <summary>
        /// Gets the resource type definition for a XXX.
        /// </summary>
```

