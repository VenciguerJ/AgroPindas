document.addEventListener('DOMContentLoaded', function () {

    const table = document.getElementById('table-js-product');
    
    // Função para buscar o preço do produto via AJAX
    function buscarPrecoProduto(produtoId, callback) {
        fetch(`/Estoque/GetProduto?id=${produtoId}`)  // Corrigido para usar o produtoId dinamicamente
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erro ao buscar o preço do produto.');
                    console.log("Erro de resposta da controller");
                }
                return response.json();
            })
            .then(data => {
                callback(data.preco); // Usa 'data.preco' para acessar o valor do produto retornado
            })
            .catch(error => console.log(error));
    }

    // Evento de mudança no seletor de produto
    table.addEventListener('change', function (event) {
        if (event.target.classList.contains('select-produto')) {
            const select = event.target;
            const produtoId = select.value;
            const inputPreco = select.closest('tr').querySelector('.input-preco');

            if (produtoId != "0") {
                buscarPrecoProduto(produtoId, function (precoUnitario) {
                    inputPreco.value = parseFloat(precoUnitario).toFixed(2);
                    atualizarSubtotal(select.closest('tr'));
                });
            } else {
                inputPreco.value = "0.00";
                atualizarSubtotal(select.closest('tr'));
            }
        }
        atualizaValorTotal();
    });

    table.addEventListener('change', function (event) {
        if (event.target.classList.contains('input-quantidade')) {
            atualizarSubtotal(event.target.closest('tr'));
        }
        atualizaValorTotal();
    });
 
    // Função para atualizar o subtotal da linha
    function atualizarSubtotal(row) {
        const quantidade = parseFloat(row.querySelector('.input-quantidade').value);
        const preco = parseFloat(row.querySelector('.input-preco').value);
        const subtotal = quantidade * preco;
        subtotal.toFixed(2)
        row.querySelector('.subtotal').textContent = subtotal;
    }

    function atualizaValorTotal() {
        const $subtotal = table.querySelectorAll(".subtotal");
        for (let i = 0; i < $subtotal.length; i++) {
            console.log($subtotal[i].textContent);
            AddToValorTotal(parseFloat($subtotal[i].textContent));
        }
    }

    function AddToValorTotal(valor) {
        const valorTotal = document.getElementById('js-valor-total');
        valorTotal.value = parseFloat(valorTotal.Value);
        if (valorTotal.Value == 0) {
            valorTotal.value = valor;
        }
        else {
            valorTotal.value += parseFloat(valor);
        }
    }

    //--------------------------------------------
    //Adicionar produto na tela 
    var $addProd = document.getElementById("adicionarProduto");
    $addProd.addEventListener('click', function () {
        const template = table.innerHTML;
        var tr = document.createElement("tr");
        tr.innerHTML = template;

        table.appendChild(tr);
    })
});
