namespace Shared.Rest.IssueBoard;

public enum IssueStatus
{
    /// <summary>未着手</summary>
    NotStarted = 0,

    /// <summary>着手中</summary>
    InProgress = 1,

    /// <summary>解決失敗</summary>
    ResolutionFailed = 2,

    /// <summary>課題確認不能</summary>
    CannotConfirm = 3,

    /// <summary>解決済み</summary>
    Resolved = 4
}
