document.addEventListener('DOMContentLoaded', function () {

    const table = document.getElementById('table-js-product');

    // Função para buscar o preço do produto via AJAX
    function buscarPrecoProduto(produtoId, callback) {
        fetch(`/Estoque/GetProduto?id=${produtoId}`)  // Corrigido para usar o produtoId dinamicamente
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erro ao buscar o preço do produto.');
                }
                return response.json();
            })
            .then(valorProduto => {
                callback(data.valorProduto);  // Chama a função de callback com o preço
            })
            .catch(error => console.error(error));
    }

    // Evento de mudança no seletor de produto
    table.addEventListener('change', function (event) {
        if (event.target.classList.contains('select-produto')) {
            const select = event.target;
            const produtoId = select.value;
            const inputPreco = select.closest('tr').querySelector('.input-preco');

            if (produtoId !== "0") {
                debugger;
                buscarPrecoProduto(produtoId, function (precoUnitario) {
                    inputPreco.value = parseFloat(precoUnitario).toFixed(2);
                    atualizarSubtotal(select.closest('tr'));
                });
            } else {
                inputPreco.value = "0.00";
                atualizarSubtotal(select.closest('tr'));
            }
        }
    });

    table.addEventListener('input', function (event) {
        if (event.target.classList.contains('input-quantidade')) {
            atualizarSubtotal(event.target.closest('tr'));
        }
    });

    // Função para atualizar o subtotal da linha
    function atualizarSubtotal(row) {
        const quantidade = parseFloat(row.querySelector('.input-quantidade').value);
        const preco = parseFloat(row.querySelector('.input-preco').value);
        const subtotal = quantidade * preco;

        row.querySelector('.subtotal').textContent = subtotal.toFixed(2);
    }
});
