document.addEventListener('DOMContentLoaded', function () {
    const addToCartButtons = document.querySelectorAll('.add-to-cart');

    addToCartButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();

            const productId = this.dataset.id;
            const productName = this.dataset.name;
            const productCoverImage = this.dataset.coverImage;
            const productPrice = parseFloat(this.dataset.price);
            const quantity = parseInt(document.getElementById('quantityInput_' + productId).value);
            const sizeButtons = document.querySelectorAll('.product-size-btn[data-product-id="' + productId + '"]');
            let selectedSize = '';
            let selectedStock = 0;
            let selectedStockId = '';

            sizeButtons.forEach(btn => {
                if (btn.classList.contains('active')) {
                    selectedSize = btn.dataset.productSize;
                    selectedStock = parseInt(btn.dataset.productStock);
                    selectedStockId = btn.dataset.stockId;
                }
            });

            if (selectedSize === '') {
                alert('Please select a size!');
                return;
            }

            addToCart(productId, productName, productCoverImage, productPrice, quantity, selectedSize, selectedStock, selectedStockId);
        });
    });

    document.querySelectorAll('.product-size-btn').forEach(sizeButton => {
        sizeButton.addEventListener('click', function () {
            document.querySelectorAll('.product-size-btn[data-product-id="' + this.dataset.productId + '"]').forEach(btn => {
                btn.classList.remove('active');
            });
            this.classList.add('active');
        });
    });
});

function addToCart(productId, productName, productCoverImage, productPrice, quantity, size, stock, stockId) {
    console.log(`Adding ${quantity} ${size} of ${productName} (ID: ${productId}, Stock ID: ${stockId}) to cart with price $${productPrice} each.`);

    let cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

    let existingProduct = cartItems.find(item => item.productId === productId && item.size === size);
    if (existingProduct) {
        if (existingProduct.quantity + quantity > stock) {
            alert(`You can only add up to ${stock - existingProduct.quantity} more items of this size.`);
            return;
        }
        existingProduct.quantity += quantity;
    } else {
        if (quantity > stock) {
            alert(`You can only add up to ${stock} items of this size.`);
            return;
        }
        cartItems.push({ productId, productName, productCoverImage, productPrice, quantity, size, stock, stockId });
    }

    localStorage.setItem('cartItems', JSON.stringify(cartItems));

    alert('Product added to cart!');
}