function atualizaGrid() {
    var cpfId = cpf.value;


    $.ajax({
        type: 'POST',
        url: "/Cliente/AtualizaGridIndex",
        cache: false,
        data: { "cpf": cpfId },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}
function atualizaGridProduto() {
    var nomeId = nome.value;
    $.ajax({
        type: 'POST',
        url: "/Produto/AtualizaGridIndex",
        cache: false,
        data: { "nome": nomeId },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}


function atualizaGridProjeto() {
    var nomeId = nome.value;
    $.ajax({
        type: 'POST',
        url: "/Projeto/AtualizaGridIndex",
        cache: false,
        data: { "nome": nomeId },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}

function mascara(t, mask) {
    var i = t6
}