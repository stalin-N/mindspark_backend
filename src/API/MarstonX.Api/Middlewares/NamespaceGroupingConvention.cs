namespace MarstonX.Api.Middlewares;

public class NamespaceGroupingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        //var namespaceSegments = controller.Application.Controllers.ToList();
        var namespaceSegments = controller.ControllerType.Namespace?.Split('.');
        var groupName = namespaceSegments?.LastOrDefault()?.ToLower() ?? "default";
        controller.ApiExplorer.GroupName = groupName;
    }
}
