document.addEventListener('DOMContentLoaded', function () {
    const cartTableBody = document.querySelector('.cart-page tbody');
    const cartSummary = document.querySelector('.cart-summary');

    function updateCartDisplay() {
        let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

        cartTableBody.innerHTML = '';
        let subTotal = 0;
        const shippingCost = 10; // Sabit kargo ücreti

        cartItems.forEach(product => {
            let totalProductPrice = product.productPrice * product.quantity;
            subTotal += totalProductPrice;

            const row = document.createElement('tr');
            row.innerHTML = `
                <td>
                    <div class="img">
                        <a href="/ProductDetail/Index/${product.productId}">
                            <img src="${product.productCoverImage}" alt="${product.productName}">
                        </a>
                        <a href="/ProductDetail/Index/${product.productId}">
                            <p>${product.productName} (Size: ${product.size})</p>
                        </a>
                    </div>
                </td>
                <td>$${product.productPrice}</td>
                <td>
                    <div class="qty">
                        <button class="btn-minus" data-id="${product.productId}" data-size="${product.size}"><i class="fa fa-minus"></i></button>
                        <input type="text" value="${product.quantity}" data-id="${product.productId}" data-size="${product.size}">
                        <button class="btn-plus" data-id="${product.productId}" data-size="${product.size}"><i class="fa fa-plus"></i></button>
                    </div>
                </td>
                <td>$${totalProductPrice}</td>
                <td><button class="remove" data-id="${product.productId}" data-size="${product.size}"><i class="fa fa-trash"></i></button></td>
            `;
            cartTableBody.appendChild(row);

            localStorage.setItem(`${product.productId}_${product.size}`, totalProductPrice);
        });

        const grandTotal = subTotal + shippingCost;
        cartSummary.querySelector('p span').innerText = `$${subTotal}`;
        cartSummary.querySelector('h2 span').innerText = `$${grandTotal}`;

 
        localStorage.setItem('cartTotal', subTotal);
        localStorage.setItem('grandTotal', grandTotal);

        attachEventListeners();
    }

    function attachEventListeners() {
        document.querySelectorAll('.btn-minus').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.dataset.id;
                const size = this.dataset.size;

                let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
                let product = cartItems.find(item => item.productId === productId && item.size === size);

                if (product && product.quantity > 1) {
                    product.quantity--;
                } else {
                    cartItems = cartItems.filter(item => !(item.productId === productId && item.size === size));
                }

                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay();
            });
        });

        document.querySelectorAll('.btn-plus').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.dataset.id;
                const size = this.dataset.size;

                let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
                let product = cartItems.find(item => item.productId === productId && item.size === size);

                if (product && product.quantity < product.stock) {
                    product.quantity++;
                } else {
                    alert(`You have exceeded the number of stocks of this product, please order a smaller quantity.`);
                }

                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay();
            });
        });

        document.querySelectorAll('.remove').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.dataset.id;
                const size = this.dataset.size;

                let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
                cartItems = cartItems.filter(item => !(item.productId === productId && item.size === size));

                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay();
            });
        });

        document.querySelectorAll('.qty input').forEach(input => {
            input.addEventListener('change', function () {
                const productId = this.dataset.id;
                const size = this.dataset.size;
                const newQuantity = parseInt(this.value);

                let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
                let product = cartItems.find(item => item.productId === productId && item.size === size);

                if (product) {
                    if (newQuantity <= product.stock) {
                        product.quantity = newQuantity;
                    } else {
                        alert(`You have exceeded the number of stocks of this product, please order a smaller quantity.`);
                        this.value = product.quantity;
                    }
                }

                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay();
            });
        });

        document.querySelector('.clear-cart').addEventListener('click', function () {
            fetch('/Cart/ClearCart', {
                method: 'POST'
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        localStorage.removeItem('cartItems');
                        updateCartDisplay();
                    } else {
                        alert('Failed to clear cart. Please try again later.');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('An error occurred while clearing the cart.');
                });
        });

        document.querySelector('.checkout').addEventListener('click', function () {
            let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

            fetch('/Cart/Checkout', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cartItems)
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        localStorage.removeItem('cartItems');
                        updateCartDisplay();
                        alert('Checkout successful!');
                    } else {
                        alert('Checkout failed. Please try again later.');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('An error occurred during checkout.');
                });
        });
    }

    updateCartDisplay();
});
