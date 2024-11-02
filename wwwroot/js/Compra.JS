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
                inputPreco.value = "00.00";
                atualizarSubtotal(select.closest('tr'));
            }
        }
        if (event.target.classList.contains('input-quantidade')) {
            atualizarSubtotal(event.target.closest('tr'));
        }
    });

    //table.addEventListener('change', function (event) {
    //});

    // Função para atualizar o subtotal da linha
    function atualizarSubtotal(row) {
        const quantidade = parseFloat(row.querySelector('.input-quantidade').value);
        const preco = parseFloat(row.querySelector('.input-preco').value);
        const subtotal = quantidade * preco;
        subtotal.toFixed(2)
        row.querySelector('.subtotal').textContent = subtotal;
        atualizaValorTotal();
    }

    function AddToValorTotal(valor) {
        const $valorTotal = document.getElementById('js-valor-total');
        let valorTotal = parseFloat($valorTotal.value);
        if (valorTotal == 0) {
            valorTotal = parseFloat(valor);
        }
        else {
            valorTotal += parseFloat(valor);
        }
        $valorTotal.value = parseFloat(valorTotal.toFixed(2));
    }
    function atualizaValorTotal() {
        const $subtotal = table.querySelectorAll(".subtotal");
        const $valorTotal = document.getElementById('js-valor-total');
        $valorTotal.value = 0;
        for (let i = 0; i < $subtotal.length; i++) {
            AddToValorTotal(parseFloat($subtotal[i].textContent));
        }
    }

    //--------------------------------------------
    //Adicionar produto na tela

    var $addProd = document.getElementById("adicionarProduto");
    var contador = table.querySelectorAll(".js-row").length;
    $addProd.addEventListener("click", function () {
        const newRow = table.rows[0].cloneNode(true);

        // Atualizar os índices dos campos
        const index = table.rows.length;
        newRow.querySelectorAll("input, select").forEach((input) => {
            const name = input.getAttribute("name");
            if (name) {
                input.setAttribute("name", name.replace(/\[0\]/, `[${index}]`));
            }
            
            input.value = ""; // Limpar valores dos novos campos
        });

        newRow.querySelector(".input-quantidade").value = 1;

        table.appendChild(newRow);
    });
});
