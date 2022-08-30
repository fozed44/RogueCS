namespace RogueCS.Core.Interfaces {

    /// <summary>
    /// IFileAsset indicates that the implementor was created by or is otherwise associated
    /// with the file 'Filepath'. Filepath must be the absolute path to the file.
    /// </summary>
    public interface IFileAsset {
        public string Filepath { get; }
    }
}
