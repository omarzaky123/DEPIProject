
document.querySelector('.Add-To-Cart').addEventListener('click', async function (e) {
    e.preventDefault(); 
    const quantity = document.querySelector('.quantity-input').value;
    const selectedOption = document.querySelector('.option.selected');
    const Id = selectedOption ? selectedOption.getAttribute('value') : '';
    try {
        const response = await fetch(`/ShoppingCart/MangeShopping?ProductVartionID=${Id}&Quantity=${quantity}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            window.location.href = response.url; // Redirect to cart page
        } else {
            console.error('Error adding to cart');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});
