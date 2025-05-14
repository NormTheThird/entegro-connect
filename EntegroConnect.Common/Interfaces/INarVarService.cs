namespace EntegroConnect.Common.Interfaces;

public interface INarVarService
{
    Task<NarVarOrderModel> GetOrderAsync(int orderNumber);
    Task CreateOrderAsync(NarVarOrderModel orderModel);
}