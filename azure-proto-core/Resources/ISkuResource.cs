namespace azure_proto_core
{
    /// <summary>
    /// Resource that uses SKU
    /// TODO: Note that it is unclear whether we get value from these interfaces, using these as a placeholder, this
    /// could just be generated code with supporting types for entities like 'plan'.  The determinitive factor should be
    /// if the additional type structure enables a desirable generic treatment of resources.  Initial thought is that 
    /// Managed Identity, Private Link and Entity Tags would beneft fromt he extra OM here, but that ManagedBy and Sku do not.
    /// </summary>
    public interface ISkuResource
    {
        Sku Sku { get; set; }
        Plan Plan { get; set; }
        string Kind { get; set; }
    }
}
