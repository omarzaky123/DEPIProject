document.getElementById('checkoutButton').addEventListener('click', function (e) {
    e.preventDefault();
    let quantityInStock = document.querySelector(".check-quantity-stokck");
    if (quantityInStock) {
        let quntityValue = quantityInStock.getAttribute("value");
        Swal.fire({
            title: "Out of Stock",
            text: "Sorry, the product are available now. Please try later.",
            icon: "error"
        });
    }
    else {
        let totalText = document.getElementById('Total').textContent.replace('$', '').trim();
        let totalValue = parseFloat(totalText);
        if (isNaN(totalValue)) {
            alert("Invalid total amount!");
            return;
        }
        const URl = `/Order/MangeOrder?TotalAmount=${totalValue}`;
        console.log("Navigating to:", URl); 
        window.location.href = URl;
    }
});
