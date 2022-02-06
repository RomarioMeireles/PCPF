function SetPedidoId(valor) {
    var Id = valor;
    $('#PedidoId').val(Id);
}
function CancelarPedido() {
    $.ajax({
        url: "/Pedido/CancelarPedido/",
        type: 'POST',
        data: { id: $('#PedidoId').val(), observacao: $('#Observacao').val() },
        success: function (result) {
            alert(result);
            window.location = "/Pedido/MeusPedidos";
        },
        error: function (result) {
            alert("Ocorreu um erro: " + result);
        }
    });
}