document.addEventListener('DOMContentLoaded', function () {
    // Product gallery thumbnail click
    let quntituInStock = 0;
    const thumbnails = document.querySelectorAll(".thumbnail");
    const mainImage = document.querySelector(".main-image img");

    if (thumbnails && mainImage) {
        thumbnails.forEach((thumbnail) => {
            thumbnail.addEventListener("click", () => {
                thumbnails.forEach((t) => t.classList.remove("active"));
                thumbnail.classList.add("active");
                const thumbSrc = thumbnail.querySelector("img").src;
                mainImage.src = thumbSrc.replace("150/100", "600/500");
            });
        });
    }

    // Size selector
    const Options = document.querySelectorAll(".option");
    if (Options) {
        Options.forEach((option) => {
            option.addEventListener("click", () => {
                quantityInput.value = 0;
                quntituInStock = option.getAttribute('quntity');
                document.querySelector(".quantity-for-option").textContent = quntituInStock;
                Options.forEach((opt) => opt.classList.remove("selected"));
                option.classList.add("selected");
            });
        });
    }

    // Color selector
    //const colorOptions = document.querySelectorAll(".color-option");
    //if (colorOptions) {
    //    colorOptions.forEach((option) => {
    //        option.addEventListener("click", () => {
    //            quantityInput.value = 0;
    //            quntituInStock = option.getAttribute('quntity');
    //            document.querySelector(".quantity-for-color").textContent = quntituInStock;
    //            colorOptions.forEach((opt) => opt.classList.remove("selected"));
    //            option.classList.add("selected");
    //        });
    //    });
    //}

    // Quantity selector
    const quantityInput = document.querySelector(".quantity-input");
    const minusBtn = document.querySelector(".quantity-control .quantity-btn:first-child");
    const plusBtn = document.querySelector(".quantity-control .quantity-btn:last-child");

    if (minusBtn && plusBtn && quantityInput) {
        minusBtn.addEventListener("click", () => {
            let value = parseInt(quantityInput.value);
            if (value > 1) {
                quantityInput.value = value - 1;
            }
        });

        plusBtn.addEventListener("click", () => {
            let value = parseInt(quantityInput.value);
            if (value < quntituInStock) {
                quantityInput.value = value + 1;
            }
        });
    }

    // Tab functionality
    const tabBtns = document.querySelectorAll(".tab-btn");
    const tabContents = document.querySelectorAll(".tab-content");

    if (tabBtns && tabContents) {
        tabBtns.forEach((btn) => {
            btn.addEventListener("click", () => {
                const tabId = btn.getAttribute("data-tab");
                tabBtns.forEach((b) => b.classList.remove("active"));
                tabContents.forEach((c) => c.classList.remove("active"));
                btn.classList.add("active");
                document.getElementById(tabId)?.classList.add("active");
            });
        });
    }


});