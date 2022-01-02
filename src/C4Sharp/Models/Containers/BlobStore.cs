namespace C4Sharp.Models.Containers;

public record BlobStore(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.Blob, Technology, Description);

public record BlobStore<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.Blob, Technology, Description);