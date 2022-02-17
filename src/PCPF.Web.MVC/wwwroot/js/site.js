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
function EnviarSMSPedido() {
    $.ajax({
        url: "/Admin/Pedido/EnviarSMSPedido/",
        type: 'POST',
        data: { pedidoId: $('#PedidoIdSMS').val(), mensagem: $('#mensagem').val() },
        success: function (result) {
            alert(result);
        },
        error: function (result) {
            alert("Ocorreu um erro: " + result);
        }
    });
}
function ConcluirPedido() {
    $.ajax({
        url: "/Admin/Pedido/ConcluirPedido/",
        type: 'POST',
        data: { id: $('#PedidoId').val(), observacao: $('#Observacao').val() },
        success: function (result) {
            alert(result);
            window.location = "/Admin/Pedido/Lista";
        },
        error: function (result) {
            alert("Ocorreu um erro: " + result);
        }
    });
}
function SetPedidoId(valor) {
    var Id = valor;
    $('#PedidoId').val(Id);
}
function SetStockId(valor) {
    var Id = valor;
    $('#StockId').val(Id);
}
function CreditarStock() {
    $.ajax({
        url: "/Admin/Stock/CreditarStock/",
        type: 'POST',
        data: { id: $('#StockId').val(), quantidade: $('#Quantidade').val() },
        success: function (result) {
            alert(result);
            window.location = "/Admin/Stock/Lista";
        },
        error: function (result) {
            alert("Ocorreu um erro: " + result);
        }
    });
}
function SetProdutoId(valor) {
    var Id = valor;
    $('#ProdutoId').val(Id);
}
function InactivarProduto() {
    $.ajax({
        url: "/Admin/Produto/inactivar/",
        type: 'POST',
        data: { id: $('#ProdutoId').val() },
        success: function (result) {
            alert(result);
            window.location = "/Admin/Pedido/Lista";
        },
        error: function (result) {
            alert("Ocorreu um erro: " + result);
        }
    });
}