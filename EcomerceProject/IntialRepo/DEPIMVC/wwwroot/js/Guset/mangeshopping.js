const TotalElement = document.getElementById("Total");
let productPrices = document.getElementsByClassName("productprice");
let quantityControls = document.getElementsByClassName("quantity-controls");

let total = 0;

for (let i = 0; i < productPrices.length; i++) {
    const priceText = productPrices[i].textContent.trim();
    const price = parseFloat(priceText.replace(/[^\d.-]/g, ''));

    const quantityText = quantityControls[i].textContent.trim();
    const quantity = parseInt(quantityText, 10);

    if (!isNaN(price) && !isNaN(quantity)) {
        total += price * quantity;
    }
}

if (TotalElement) {
    TotalElement.textContent = total.toFixed(2); // Format to 2 decimal places
}