using Order.Delivery.Email.Service.Models.Order;

namespace Order.Delivery.Email.Service.Builders;

public static class EmailBodyBuilder
{
    public static string GetNewOrderCreatedBody(OrderMessage message)
    {
        string action = message.Action;
        string[] words = action.Split(' ');
        string thirdWord = words[2].ToUpper();

        switch (thirdWord)
        {
            case "NOVO":
                return $"Parabéns {message.Order!.Customer!.Name.ToUpper()} ! Você agora está cadastrado em nosso Portal Delivery e é o nosso novo cliente";
            case "ATUALIZAÇÂO":
                return $"Olá {message.Order!.Customer!.Name.ToUpper()} ! Você recebeu esse e-mail porque você atualizou seus dados cadastrais em nosso portal!";
            case "PEDIDO":
                return $"Parabéns {message.Order!.Customer!.Name.ToUpper()} ! Seu pedido, de identificação {message.Order.OrderId}, totalizando R$ {message.Order.TotalPrice} foi criado com sucesso!";
            default:
                return $"Parabéns {message.Order!.Customer!.Name.ToUpper()} ! Atualização do pedido {message.Order.OrderId} para o status de {message.Order.OrderSituation} foi criado";
        }
    }
}
