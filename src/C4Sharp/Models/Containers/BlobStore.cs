namespace C4Sharp.Models.Containers;

public record BlobStore<T>(string Technology, string Description)
    : Container<T>(ContainerType.Blob, Technology, Description);