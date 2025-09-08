namespace MarstonX.Application.Wrappers;

public interface IResponseData<T>
where T : class
{
    T? Deserialize(string? successModel = null);

    List<T>? DeserializeListJson(string? successModel);

    ResponseMessage ActionUpdateResponse(DBResponse response);

    ResponseMessage ActionDeleteResponse(DBResponse response);

    T? ActionResponse(DBResponse response);
}
