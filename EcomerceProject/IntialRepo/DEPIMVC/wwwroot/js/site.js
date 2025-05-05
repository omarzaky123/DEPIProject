// Mobile menu toggle
const mobileMenuBtn = document.querySelector(".mobile-menu-btn");
const closeMenuBtn = document.querySelector(".close-menu");
const mobileMenu = document.querySelector(".mobile-menu");
const mobileNav = document.querySelector(".mobile-nav");

mobileMenuBtn.addEventListener("click", () => {
    mobileMenu.classList.add("active");
    document.body.style.overflow = "hidden";
    mobileNav.style.display = "block";
    mobileNav.style.overflow = "hidden";
});

closeMenuBtn.addEventListener("click", () => {
    mobileMenu.classList.remove("active");
    document.body.style.overflow = "auto";
});

// Cart functionality
const addToCartButtons = document.querySelectorAll(".add-to-cart");
const cartCount = document.querySelector(".cart-count");
let count = 0;

addToCartButtons.forEach((button) => {
    button.addEventListener("click", (e) => {
        e.preventDefault();
        count++;
        cartCount.textContent = count;

        // Animation effect
        button.textContent = "Added to Cart";
        button.style.backgroundColor = "#5c6ac4";
        button.style.color = "white";

        setTimeout(() => {
            button.textContent = "Add to Cart";
            button.style.backgroundColor = "";
            button.style.color = "";
        }, 1500);
    });
});
