function dropDownAdmin() {
    let dropDownButtons = document.querySelectorAll(`.items .btn-menu`);
    let arrows = document.querySelectorAll(`.menu .items .btn-menu i:last-child`);
    let displayedItems = document.querySelectorAll(`.items .hidden-menu`);
    displayedItems.forEach((displayedItem) => {
        displayedItem.style.transform = "translateY(-10%)";
        displayedItem.style.zIndex = "-1";
    })

    dropDownButtons.forEach((dropDownButton, index) => {
        dropDownButton.addEventListener("click", function () {
            if (displayedItems[index].style.transform == "translateY(-10%)") {
                displayedItems[index].style.transform = "translateY(0%)";
                displayedItems[index].style.zIndex = "1";
                displayedItems[index].style.opacity = "1";
                displayedItems[index].style.position = "relative";
                arrows[index].style.transform = "rotate(180deg)";

            }
            else {
                displayedItems[index].style.opacity = "0";
                displayedItems[index].style.zIndex = "-1";
                displayedItems[index].style.transform = "translateY(-10%)";
                displayedItems[index].style.position = "absolute";
                arrows[index].style.transform = "rotate(0deg)";
            }
        })
    })


}


dropDownAdmin();